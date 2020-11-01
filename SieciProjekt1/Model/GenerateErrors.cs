using System;
using System.Collections.Generic;
using System.Text;

namespace SieciProjekt1.Model
{
    public static class GenerateErrors
    {
        public static void WithRepeats(UploadedFile file)
        {
            throw new Exception();
        }

        public static void WithoutRepeats(UploadedFile file)
        {
            throw new Exception();
        }
    }
}
