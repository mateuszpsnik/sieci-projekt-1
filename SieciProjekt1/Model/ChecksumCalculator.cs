using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using System.Collections;
using System.Printing.IndexedProperties;

namespace SieciProjekt1.Model
{
    public static class ChecksumCalculator
    {
        public static byte[] ParityBit(byte[] block)
        {
            BitArray bitArray = new BitArray(block);

            bool result = bitArray[0];

            for (int i = 0; i < bitArray.Count; i++)
            {
                result ^= bitArray[i];
            }

            return new byte[1] { Convert.ToByte(result) };
        }

        public static byte[] Modulo(byte[] block, int modulus)
        {
            throw new Exception();
        }

        public static byte[] CRC(byte[] block, int size)
        {
            throw new Exception();
        }
    }
}
