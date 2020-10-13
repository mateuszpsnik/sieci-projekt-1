using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace SieciProjekt1.ViewModel
{
    class LoadFileCommand : ICommand
    {
        UploadedFileViewModel viewModel;
        public LoadFileCommand(UploadedFileViewModel viewModel)
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
            viewModel.OpenFile();
        }
    }
}
