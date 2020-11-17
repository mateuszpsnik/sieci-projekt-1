using System;
using System.Collections.Generic;
using System.Text;

namespace SieciProjekt1.Model
{
    public ref struct PacketRefStruct
    {
        Header header;
        public Span<byte> Data { get; set; }

        public Header Header => header;

        public PacketRefStruct(Packet packet)
        {
            header = packet.Header;
            Data = packet.Bytes;
           // constructSpan(packet.Bytes);
        }

        public PacketRefStruct(PacketRefStruct packet)
        {
            header = packet.header;
            Data = packet.Data;
          //  constructSpan(packet.Data);
        }

        /*
        This code caused errors.
        private unsafe void constructSpan(Span<byte> span)
        {
            fixed (Header* hptr = &header) 
            {
                Header* ptr = hptr + 1;
                Data = new Span<byte>(ptr, span.Length);
                span.CopyTo(Data);
            }
        }
         */


        public unsafe string PrintAddresses()
        {
            string addresses = "";



            return "";
        }
    }
}
