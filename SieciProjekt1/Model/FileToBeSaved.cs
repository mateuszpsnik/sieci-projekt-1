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

        /*
        public FileToBeSaved(List<Packet> packets, long length)
        {
            this.packets = packets;
            this.length = length;
            ConcatenatedPackets = new byte[length];
        }

        long length;
        */


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
            byte[] array3 = new byte[array1.Length + array2.Length];
            array1.CopyTo(array3, 0);
            array2.CopyTo(array3, array1.Length);

            return array3;
        }
         

        /*
        public void ConcatenatePackets()
        {
            long i = 0;
            while (i < length)
            {
                foreach (Packet packet in packets)
                {
                    byte[] bytes = packet.Bytes;

                    for (int j = 0; j < packet.Header.Size; j++)
                    {
                        ConcatenatedPackets[i] = bytes[j];
                        i++;
                    }
                }
            }
        }
         */

    }
}
