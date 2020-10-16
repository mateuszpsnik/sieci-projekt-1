using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Text;

namespace SieciProjekt1.Model
{
    public class FileToBeSaved
    {
        public FileToBeSaved(List<Packet> packets)
        {
            this.packets = packets;
            ConcatenatedPackets = new byte[0];
        }


        List<Packet> packets;

        public byte[] ConcatenatedPackets;

        /*
        public void ConcatenatePackets()
        {
            foreach (var packet in packets)
            {
                int previousLength = ConcatenatedPackets.Length;
                byte[] previousConcatenatedPackets = ConcatenatedPackets;

                ConcatenatedPackets = new byte[previousLength + packet.Bytes.Length];

                previousConcatenatedPackets.CopyTo(ConcatenatedPackets, 0);
                packet.Bytes.CopyTo(ConcatenatedPackets, previousLength);
            }
        }
         */


        public void ConcatenatePackets(long fileSize)
        {
            ConcatenatedPackets = new byte[fileSize];
            int i = 0;

            foreach (var packet in packets)
            {
                for (int j = 0; j < packet.Header.Size; j++)
                {
                    ConcatenatedPackets[i] = packet.Bytes[j];
                    i++;
                }
            }
        }
    }
}
