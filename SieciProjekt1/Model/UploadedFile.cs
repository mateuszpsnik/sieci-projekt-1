﻿using System;
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

        public void DivideDataIntoPackets(int packetSize)
        {
            packets = new List<Packet>();
            int elementsLeft;
            int i = 0;

            while (i < Data.Length)
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
