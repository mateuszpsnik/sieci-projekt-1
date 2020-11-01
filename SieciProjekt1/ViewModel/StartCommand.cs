using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace SieciProjekt1.ViewModel
{
    class StartCommand : ICommand
    {
        UploadedFileViewModel viewModel;

        public StartCommand(UploadedFileViewModel viewModel)
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
            viewModel.ErrorsChecksumPackets();
        }
    }
}
