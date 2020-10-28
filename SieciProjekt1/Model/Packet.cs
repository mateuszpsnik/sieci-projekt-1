using System;
using System.Collections.Generic;
using System.Text;

namespace SieciProjekt1.Model
{
    public class Packet
    {
        public Header Header;

        public byte[] Bytes { get; set; }

        public Packet(int size, int id)
        {
            Bytes = new byte[size];
            Header = new Header(id, size);
        }
    }
}
