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
            int result = Convert.ToInt32(block[0]);

            int counter = 0;
            for (int i = 0; i < block.Length; i++)
            {
                result += Convert.ToInt32(block[i]);
                counter++;
                if (counter == 1000)
                {
                    result %= modulus;
                    counter = 0;
                }
            }

            result %= modulus;

            byte[] bytes = BitConverter.GetBytes(result);

            return bytes;
        }

        public static byte[] CRC(byte[] block, int divisor)
        {
            BitArray divArray = new BitArray(new int[] { divisor });
            BitArray divident = new BitArray(block);
            BitArray divisorArray = new BitArray(calculateBits(divisor));
            BitArray remainder = new BitArray(divisorArray.Length - 1);
            BitArray result = new BitArray(divident.Length + remainder.Length, false);

            for (int i = 0; i < divisorArray.Length; i++)
                divisorArray[i] = divArray[i];

            for (int i = divident.Length - 1; i >= 0; i--)
                result[i + remainder.Length] = divident[i];

            int iterator = result.Length - 1;
            while (iterator >= remainder.Length)
            {
                if (result[iterator] == false)
                    iterator--;
                else
                {
                    int length = divisorArray.Length;
                    for (int j = 0; j < length; j++)
                        result[iterator - j] ^= divisorArray[length - j - 1];

                    iterator--;
                }
            }

            while (iterator >= 0)
            {
                remainder[iterator] = result[iterator];

                iterator--;
            }

            byte[] bytes = new byte[calculateBytes(divisor)];
            
            remainder.CopyTo(bytes, 0);

            return bytes;
        }

        private static int calculateBits(int number)
        {
            BitArray array = new BitArray(new int[1] { number });

            int i = array.Length - 1;
            while (array[i] == false)
                i--;

            return i + 1;
        }

        private static int calculateBytes(int number)
        {
            byte[] bytes = BitConverter.GetBytes(number);

            return bytes.Length;
        }
    }
}
