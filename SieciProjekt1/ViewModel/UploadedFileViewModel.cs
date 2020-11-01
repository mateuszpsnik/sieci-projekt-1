﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.IO;
using System.Reflection.Metadata;
using System.Text;
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

        int modulusCRCSize;
        public int ModulusCRCSize
        {
            get => modulusCRCSize;
            set
            {
                modulusCRCSize = value;
                OnPropertyChanged(nameof(ModulusCRCSize));
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
                uploadedFile.AddErrors(withoutRepeats);
            // checksum
            uploadedFile.CalculateChecksum(checksumType, modulusCRCSize);
            // packets
            uploadedFile.DivideDataIntoPackets(10); // it will be able to change packetSize later
        }

        public void SaveFile()
        {
            FileToBeSaved fileToBeSaved = SendFileToSave();

            fileToBeSaved.ConcatenatePackets(FileSize);

            SaveFileDialog dialog = new SaveFileDialog();
            dialog.InitialDirectory = FilePath;

            if (dialog.ShowDialog() == true)
            {
                File.WriteAllBytes(dialog.FileName, fileToBeSaved.FinalFile);
            }
        }

        public FileToBeSaved SendFileToSave()
        {
            FileToBeSaved fileToBeSaved = new FileToBeSaved(uploadedFile.Packets, uploadedFile.Checksum);

            return fileToBeSaved;
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
