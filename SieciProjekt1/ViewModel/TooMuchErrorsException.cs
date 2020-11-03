using System;
using System.Collections.Generic;
using System.Text;

namespace SieciProjekt1.ViewModel
{
    class TooMuchErrorsException : Exception
    {
        public TooMuchErrorsException() : base() { }
        public TooMuchErrorsException(string message) : base(message) { }
        public TooMuchErrorsException(string message, Exception inner) : base(message, inner) { }
    }
}
