using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.IO;
using System.Reflection.Metadata;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using Microsoft.Win32;
using SieciProjekt1.Model;


namespace SieciProjekt1.ViewModel
{
    public class UploadedFileViewModel : INotifyPropertyChanged
    {
        UploadedFile uploadedFile;

        public string FilePath { get; private set; }

        long fileSize;
        public long FileSize
        {
            get => fileSize;
            set
            {
                fileSize = value;
                OnPropertyChanged(nameof(FileSize));
            }
        }

        ChecksumTypes checksumType;
        public ChecksumTypes ChecksumType
        {
            get => checksumType;
            set
            {
                checksumType = value;
                OnPropertyChanged(nameof(ChecksumType));
            }
        }

        int modulusCRCDivisor = 3;
        public int ModulusCRCDivisor
        {
            get
            {
                try
                {
                    if (modulusCRCDivisor < 3)
                    {
                        throw new LessThanThreeException("Wartość modulo/dzielnika CRC musi być równa " +
                                "minimum 3");
                    }  
                    else
                    {
                        return modulusCRCDivisor;
                    }
                }
                catch (LessThanThreeException e)
                {
                    MessageBox.Show(e.Message, "Za niska wartość", MessageBoxButton.OK, MessageBoxImage.Error);
                    return 0;
                }
            }
            set
            {
                try
                {
                    if (value < 3)
                        throw new LessThanThreeException("Wartość modulo/dzielnika CRC musi być równa " +
                            "minimum 3");
                    else
                    {
                        modulusCRCDivisor = value;
                        OnPropertyChanged(nameof(ModulusCRCDivisor));
                    }
                }
                catch (LessThanThreeException e)
                {
                    MessageBox.Show(e.Message, "Za niska wartość", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                
            }
        }

        bool addErrors;
        public bool AddErrors
        {
            get => addErrors;
            set
            {
                addErrors = value;
                OnPropertyChanged(nameof(AddErrors));
            }
        }

        bool withoutRepeats;
        public bool WithoutRepeats
        {
            get => withoutRepeats;
            set
            {
                withoutRepeats = value;
                OnPropertyChanged(nameof(WithoutRepeats));
            }
        }

        double amountOfErrors = 1;
        public double AmountOfErrors
        {
            get => amountOfErrors;
            set
            {
                try
                {
                    if (value > 10)
                        throw new TooMuchErrorsException("Ilość błędów nie może być większa niż 10%");
                    else if (value < 0)
                        throw new NegativeAmountException("Ilość błędów nie może być ujemna");
                    else
                    {
                        amountOfErrors = value;
                        OnPropertyChanged(nameof(AmountOfErrors));
                    }
                }
                catch (TooMuchErrorsException e)
                {
                    MessageBox.Show(e.Message, "Za dużo błędów", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (NegativeAmountException e)
                {
                    MessageBox.Show(e.Message, "Wartość ujemna", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        int packetSize = 10;
        public int PacketSize
        {
            get => packetSize;
            set
            {
                packetSize = value;
                OnPropertyChanged(nameof(PacketSize));
            }
        }

        public void OpenFile()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            if (dialog.ShowDialog() == true)
            {
                FilePath = dialog.FileName;
                FileInfo fileInfo = new FileInfo(FilePath);
                FileSize = fileInfo.Length;

                uploadedFile = new UploadedFile();
                uploadedFile.Data = File.ReadAllBytes(FilePath);
            }
        }

        /*
         Generate errors (if selected),
         calculate the checksum and divide into packets.
         */
        public void ErrorsChecksumPackets()
        {
            // errors
            if (addErrors)
                uploadedFile.AddErrors(withoutRepeats, amountOfErrors * 0.01);
            // checksum
            uploadedFile.CalculateChecksum(checksumType, modulusCRCDivisor);
            // packets
            uploadedFile.DivideDataIntoPackets((uint)packetSize); 
        }

        public void SaveFile()
        {
            FileToBeSaved fileToBeSaved = new FileToBeSaved(uploadedFile.ChecksumCRC);

            using (StreamWriter sw = new StreamWriter("logs.txt"))
            {
                // Send a packet (PacketRefStruct) from uploadedFile to fileToBeSaved
                foreach (var packet in uploadedFile.Packets)
                    fileToBeSaved.ReceivePacket(uploadedFile.SendPacket(packet, sw));
            }

            fileToBeSaved.ConcatenatePackets(FileSize); 

            SaveFileDialog dialog = new SaveFileDialog();
            dialog.InitialDirectory = FilePath;

            if (dialog.ShowDialog() == true)
            {
                File.WriteAllBytes(dialog.FileName, fileToBeSaved.FinalFile);
            }

            System.Windows.Application.Current.Shutdown();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ICommand loadFileCommand;
        public ICommand LoadFileCommand
        {
            get
            {
                if (loadFileCommand == null)
                    loadFileCommand = new LoadFileCommand(this);
                return loadFileCommand;
            }
            set
            {
                loadFileCommand = value;
            }
        }

        private ICommand saveFileCommand;
        public ICommand SaveFileCommand
        {
            get
            {
                if (saveFileCommand == null)
                    saveFileCommand = new SaveFileCommand(this);
                return saveFileCommand;
            }
            set
            {
                saveFileCommand = value;
            }
        }

        private ICommand startCommand;
        public ICommand StartCommand
        {
            get
            {
                if (startCommand == null)
                    startCommand = new StartCommand(this);
                return startCommand;
            }
            set
            {
                startCommand = value;
            }
        }
    }
}
