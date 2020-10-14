using System;
using System.Collections.Generic;
using System.Text;

namespace SieciProjekt1.Model
{
    public struct Packet
    {
        Header header;

        public byte[] Bytes { get; set; }

        public Packet(uint size, uint id)
        {
            Bytes = new byte[size];
            header = new Header(id, size);
        }
    }
}
