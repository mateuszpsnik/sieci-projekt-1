using System;
using System.Collections.Generic;
using System.Text;

namespace SieciProjekt1.Model
{
    public struct Header
    {
        public Header(int id, int size)
        {
            ID = id;
            Size = size;
        }

        public int ID;
        public int Size;
    }
}
