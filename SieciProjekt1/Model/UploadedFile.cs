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

        public void DivideDataIntoPackets(int packetSize)
        {
            packets = new List<Packet>();
            int elementsLeft;

            for (int i = 0; i < Data.Length; i++)
            {
                elementsLeft = Data.Length - i;

                Packet packet;

                if (elementsLeft > packetSize)
                    packet = new Packet(packetSize, i);  
                else
                    packet = new Packet(elementsLeft, i); 

                for (int j = 0; j < packetSize && i < Data.Length; j++)
                {
                    packet.Bytes[j] = Data[i];
                    i++;
                }

                packets.Add(packet);
            }
        }
    }
}
