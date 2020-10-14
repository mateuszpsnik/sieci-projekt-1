using System;
using System.Collections.Generic;
using System.Text;

namespace SieciProjekt1.Model
{
    public class UploadedFile
    {
        public byte[] Data { get; set; }

        List<Packet> packets;

        public List<Packet> Packets => packets;

        public void DivideDataIntoPackets(uint packetSize)
        {
            packets = new List<Packet>();
            uint elementsLeft;

            for (uint i = 0; i < (uint)Data.LongLength; i++)
            {
                elementsLeft = (uint)Data.LongLength - i;

                Packet packet;

                if (elementsLeft > packetSize)
                    packet = new Packet(packetSize, i);  
                else
                    packet = new Packet(elementsLeft, i); 

                for (int j = 0; j < packetSize && i < (uint)Data.LongLength; j++)
                {
                    packet.Bytes[j] = Data[i];
                    i++;
                }

                packets.Add(packet);
            }
        }
    }
}
