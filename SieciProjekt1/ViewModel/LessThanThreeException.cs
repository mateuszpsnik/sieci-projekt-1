using System;
using System.Collections.Generic;
using System.Text;

namespace SieciProjekt1.ViewModel
{
    class LessThanThreeException : Exception
    {
        public LessThanThreeException() : base() { }
        public LessThanThreeException(string message) : base(message) { }
        public LessThanThreeException(string message, Exception inner) : base(message, inner) { }
    }
}
