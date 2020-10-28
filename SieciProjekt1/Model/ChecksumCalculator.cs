using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using System.Collections;

namespace SieciProjekt1.Model
{
    public static class ChecksumCalculator
    {
        public static byte ParityBit(byte[] block)
        {
            BitArray bitArray = new BitArray(block);

            bool result = bitArray[0];

            for (int i = 0; i < bitArray.Count; i++)
            {
                result ^= bitArray[i];
            }

            return Convert.ToByte(result);
        }
    }
}
