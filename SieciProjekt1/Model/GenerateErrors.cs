using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            Random random = new Random();

            BitArray data = new BitArray(file.Data);

            int numberOfErrors = (int)(data.Length * amount);

            Dictionary<int, int> randomInt = new Dictionary<int, int>();

            for (int i = 0; i < numberOfErrors; i++)
            {
                int randomKey = random.Next();
                if (randomInt.ContainsKey(randomKey))
                    i--;
                else
                    randomInt.Add(randomKey, random.Next(data.Length));
            }

            Dictionary<int, int> randomIntSortedByKey = new Dictionary<int, int>();

            foreach (var pair in randomInt.OrderBy(pair => pair.Key))
            {
                randomIntSortedByKey.Add(pair.Key, pair.Value);
            }

            List<int> randomNotRepearing = randomIntSortedByKey.Values.ToList();

            foreach (var i in randomNotRepearing)
            {
                data[i] = !data[i];
            }

            data.CopyTo(file.Data, 0);
        }
    }
}
