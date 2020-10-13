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
        }
    }
}
