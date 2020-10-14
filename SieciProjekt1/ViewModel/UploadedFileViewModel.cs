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
                uploadedFile.DivideDataIntoPackets(10); // it will be able to change packetSize later
            }
            
        }

        public FileToBeSaved SendFileToSave()
        {
            FileToBeSaved fileToBeSaved = new FileToBeSaved(uploadedFile.Packets, (uint)fileSize);

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
    }
}
