using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Text;

namespace SieciProjekt1.Model
{
    public class FileToBeSaved
    {
        public FileToBeSaved(List<Packet> packets, byte[] checksum)
        {
            this.packets = packets;
            Checksum = checksum;
            FinalFile = new byte[0];
        }

        List<Packet> packets;

        public byte[] FinalFile;

        public byte[] Checksum;

        public void ConcatenatePackets(long fileSize)
        {
            byte[] concatenatedPackets  = new byte[fileSize];
            FinalFile = new byte[fileSize + Checksum.Length];

            int i = 0;

            foreach (var packet in packets)
            {
                for (int j = 0; j < packet.Header.Size; j++)
                {
                    concatenatedPackets[i] = packet.Bytes[j];
                    i++;
                }
            }

            concatenatedPackets.CopyTo(FinalFile, 0);
            Checksum.CopyTo(FinalFile, concatenatedPackets.Length);
        }
    }
}
