using System;
using System.Collections.Generic;
using System.Text;

namespace SieciProjekt1.Model
{
    public ref struct PacketRefStruct
    {
        private Header header;
        public Span<byte> Data { get; set; }

        public Header Header => header;

        public PacketRefStruct(Packet packet)
        {
            this.header = packet.Header;
            Data = packet.Bytes;
            //constructSpan(packet.Bytes);
        }

        public PacketRefStruct(byte[] packetBytes)
        {
            int sizeOfUInt = 4;
            byte[] idBytes = new byte[sizeOfUInt];
            byte[] sizeBytes = new byte[sizeOfUInt];
            byte[] dataBytes = new byte[packetBytes.Length - 2 * sizeOfUInt];

            int i = 0;
            for (int j = 0; j < idBytes.Length; j++)
            {
                idBytes[j] = packetBytes[i];
                i++;
            }
            for (int k = 0; k < sizeBytes.Length; k++)
            {
                sizeBytes[k] = packetBytes[i];
                i++;
            }
            for (int l = 0; l < dataBytes.Length; l++)
            {
                dataBytes[l] = packetBytes[i];
                i++;
            }

            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(idBytes);
                Array.Reverse(sizeBytes);
            }

            uint id = BitConverter.ToUInt32(idBytes);
            uint size = BitConverter.ToUInt32(sizeBytes);

            header = new Header(id, size);
            Data = dataBytes;
            // constructSpan(dataBytes);
        }

        public PacketRefStruct(PacketRefStruct packet)
        {
            this.header = packet.Header;
            Data = packet.Data;
           // constructSpan(packet.Data);
        }

        /*
        // This code caused errors.
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
            string addresses = $"Packet {this.Header.ID}:{Environment.NewLine}";

            addresses += $"ID: {this.Header.IdAddress()}{Environment.NewLine}";
            addresses += $"Size: {this.Header.SizeAddress()}{Environment.NewLine}";

            fixed (byte* bptr = &this.Data[0])
            {
                addresses += $"Data: {new IntPtr(bptr)}{Environment.NewLine}";
            }

            return addresses;
        }
    }
}
