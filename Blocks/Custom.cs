using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Dia2Lib;

namespace pbdcstest.Blocks
{
    class Custom
    {
        public IDiaSymbol symbol;

        public byte[] dataBytes;
        public uint symIndexId;
        public uint symTag;

        public Custom(IDiaSymbol sym)
        {
            symbol = sym;

            uint size=0;
            uint count = 0;

            sym.get_dataBytes(size, out count, out dataBytes[0]);
            symIndexId = sym.symIndexId;
            symTag = sym.symTag;
        }
    }
}
