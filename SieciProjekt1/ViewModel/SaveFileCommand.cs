﻿using Microsoft.Win32;
using SieciProjekt1.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Input;

namespace SieciProjekt1.ViewModel
{
    class SaveFileCommand : ICommand
    {
        UploadedFileViewModel viewModel;
        public SaveFileCommand(UploadedFileViewModel viewModel)
        {
            this.viewModel = viewModel;
        }


        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            viewModel.SaveFile();
        }
    }
}
