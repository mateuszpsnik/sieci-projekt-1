using System;
using System.Collections.Generic;
using System.Text;

namespace SieciProjekt1.Model
{
    public class Packet
    {
        Header header;

        public byte[] Bytes { get; set; }

        public Packet(int size)
        {
            Bytes = new byte[size];
        }
    }
}
