using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace SieciProjekt1.Model
{
    public class FileToBeSaved
    {
        public FileToBeSaved(byte[] checksumCRC)
        {
            packets = new List<Packet>();
            ChecksumCRC = checksumCRC;
            FinalFile = new byte[0];
        }

        List<Packet> packets;

        public byte[] FinalFile;

        public byte[] ChecksumCRC;

       public void ReceivePacket(PacketRefStruct packetReceived) 
        {
            // Following line of code uses my copy constructor, because otherwise
            // (I don't know why) Span is passed by reference, not copied.
            PacketRefStruct packet = new PacketRefStruct(packetReceived);

            // Converts PacketRefStruct to Packet and then adds it to the list 
            Packet newPacket = new Packet(packet.Header.Size, packet.Header.ID);
            newPacket.Bytes = packet.Data.ToArray();
            packets.Add(newPacket);
        }

        public void ConcatenatePackets(long fileSize)
        {
            byte[] concatenatedPackets  = new byte[fileSize];
            FinalFile = new byte[fileSize + ChecksumCRC.Length];

            int i = 0;

            foreach (var packet in packets)
            {
                for (int j = 0; j < packet.Bytes.Length; j++)
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
