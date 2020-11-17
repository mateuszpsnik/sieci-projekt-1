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

        public unsafe IntPtr IdAddress()
        {
            fixed (uint* ptr = &ID)
            {
                return new IntPtr(ptr);
            }
        }

        public unsafe IntPtr SizeAddress()
        {
            fixed (uint* ptr = &Size)
            {
                return new IntPtr(ptr);
            }
        }
    }
}
