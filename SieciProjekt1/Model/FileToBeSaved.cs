using System;
using System.Collections.Generic;
using System.Text;

namespace SieciProjekt1.Model
{
    public class FileToBeSaved
    {
        public FileToBeSaved(List<Packet> packets, uint fileSize)
        {
            this.packets = packets;
            ConcatenatedPackets = new byte[0];
        }

        List<Packet> packets;

        public byte[] ConcatenatedPackets;

        public void ConcatenatePackets()
        {
            foreach (var packet in packets)
            {
                ConcatenatedPackets = concatenate(ConcatenatedPackets, packet.Bytes);
            }
        }

        private byte[] concatenate(byte[] array1, byte[] array2)
        {
            byte[] array3 = new byte[array1.LongLength + array2.LongLength];
            array1.CopyTo(array3, 0);
            array2.CopyTo(array3, array1.LongLength);

            return array3;
        }
    }
}
