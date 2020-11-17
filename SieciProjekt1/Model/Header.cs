using System;
using System.Collections.Generic;
using System.Text;

namespace SieciProjekt1.Model
{
    public struct Header
    {
        public Header(uint id, uint size)
        {
            ID = id;
            Size = size;
            recountSize();
        }

        // Adds size of this header
        private void recountSize()
        {
            Size += (uint)System.Runtime.InteropServices.Marshal.SizeOf(this);
        }

        public uint ID;
        public uint Size;
    }
}
