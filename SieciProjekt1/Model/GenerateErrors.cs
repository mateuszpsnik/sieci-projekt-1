using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.Xml;
using System.Text;

namespace SieciProjekt1.Model
{
    public static class GenerateErrors
    {
        public static void WithRepeats(UploadedFile file, double amount)
        {
            Random random = new Random();

            BitArray data = new BitArray(file.Data);

            int numberOfErrors = (int)(data.Length * amount);

            for (int i = 0; i < numberOfErrors; i++)
            {
                int index = random.Next(data.Length);

                data[index] = !data[index];
            }

            data.CopyTo(file.Data, 0);
        }

        public static void WithoutRepeats(UploadedFile file, double amount)
        {
            throw new Exception();
        }
    }
}
