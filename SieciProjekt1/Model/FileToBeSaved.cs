using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Text;

namespace SieciProjekt1.Model
{
    public class FileToBeSaved
    {
        public FileToBeSaved(List<Packet> packets, byte[] checksumCRC)
        {
            this.packets = packets;
            ChecksumCRC = checksumCRC;
            FinalFile = new byte[0];
        }

        List<Packet> packets;

        public byte[] FinalFile;

        public byte[] ChecksumCRC;

        public void ConcatenatePackets(long fileSize)
        {
            byte[] concatenatedPackets  = new byte[fileSize];
            FinalFile = new byte[fileSize + ChecksumCRC.Length];

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
            ChecksumCRC.CopyTo(FinalFile, concatenatedPackets.Length);
        }
    }
}
