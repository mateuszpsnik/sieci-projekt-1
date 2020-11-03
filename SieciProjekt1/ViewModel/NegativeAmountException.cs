using System;
using System.Collections.Generic;
using System.Text;

namespace SieciProjekt1.ViewModel
{
    class NegativeAmountException : Exception
    {
        public NegativeAmountException() : base() { }
        public NegativeAmountException(string message) : base(message) { }
        public NegativeAmountException(string message, Exception inner) : base(message, inner) { }
    }
}
