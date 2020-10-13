using System;
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
                FileInfo fileInfo = new FileInfo(dialog.FileName);
                FileSize = fileInfo.Length;
            }
            
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ICommand command;
        public ICommand Command
        {
            get
            {
                if (command == null)
                    command = new LoadFileCommand(this);
                return command;
            }
            set
            {
                command = value;
            }
        }
    }
}
