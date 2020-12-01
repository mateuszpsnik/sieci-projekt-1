using System;
using System.Collections.Generic;
using System.Text;

namespace SieciProjekt1.Model
{
    public class UploadedFile
    {
        public byte[] Data { get; set; }

        List<Packet> packets;

        public byte[] ChecksumCRC { get; set; }

        public List<Packet> Packets => packets;

        public void DivideDataIntoPackets(uint packetSize)
        {
            packets = new List<Packet>();
            uint elementsLeft;
            uint i = 0;
            uint countPackets = 1;

            while (i < Data.Length)
            {
                elementsLeft = (uint)Data.Length - i;

                Packet packet;

                if (elementsLeft > packetSize)
                {
                    packet = new Packet(packetSize, countPackets);
                    countPackets++;
                }
                else
                {
                    packet = new Packet(elementsLeft, countPackets);
                    countPackets++;
                }

                for (int j = 0; j < packetSize && i < Data.Length; j++)
                {
                    packet.Bytes[j] = Data[i];
                    i++;
                }

                packets.Add(packet);
            }
        }

        public PacketRefStruct SendPacket(Packet p)
        {
            PacketRefStruct packet = new PacketRefStruct(p);

            return packet;
        }

        /*
        public Span<byte> SendPacket(Packet p)
        {
            int spanLenght = System.Runtime.InteropServices.Marshal.SizeOf(p);
            Span<byte> spanPacket = stackalloc byte[spanLenght];

            int i = 0;

            byte[] idBytes = new byte[4];
            byte[] sizeBytes = new byte[4];

            for (int j = 0; j < idBytes.Length; j++)
            {
                spanPacket[i] = idBytes[j];
                i++;
            }

            for (int k = 0; k < sizeBytes.Length; k++)
            {
                spanPacket[i] = sizeBytes[k];
                i++;
            }

            for (int m = 0; m < p.Bytes.Length; m++)
            {
                spanPacket[i] = p.Bytes[m];
                i++;
            }

            return spanPacket;
        }
         */

        public void AddErrors(bool withoutRepeats, double amountOfErrors)
        {
            if (withoutRepeats)
                GenerateErrors.WithoutRepeats(this, amountOfErrors);
            else
                GenerateErrors.WithRepeats(this, amountOfErrors);
        }

        public void CalculateChecksum(ChecksumTypes checksumType, int modulusCRCDivisor)
        {
            if (checksumType == ChecksumTypes.ParityBit)
                ChecksumCRC = ChecksumCalculator.ParityBit(Data);
            else if (checksumType == ChecksumTypes.Modulo)
                ChecksumCRC = ChecksumCalculator.Modulo(Data, modulusCRCDivisor);
            else
                ChecksumCRC = ChecksumCalculator.CRC(Data, modulusCRCDivisor);
        }
    }
}
