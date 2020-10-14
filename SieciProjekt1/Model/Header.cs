using System;
using System.Collections.Generic;
using System.Text;

namespace SieciProjekt1.Model
{
    public struct Header
    {
        public Header(uint id, uint size)
        {
            this.id = id;
            this.size = size;
        }

        uint id;
        uint size;
    }
}
