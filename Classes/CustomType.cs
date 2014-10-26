using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Dia2Lib;

namespace pbdcstest.Classes
{
    class CustomType
    {
        public IDiaSymbol symbol;

        public uint oemId;
        public uint oemSymbolId;
        public uint symIndexId;
        public uint symTag;
        public IDiaSymbol type;
        public uint typeId;
        public IDiaSymbol[] types;

        public CustomType(IDiaSymbol sym)
        {
            symbol = sym;

            oemId = sym.oemId;
            oemSymbolId = sym.oemSymbolId;
            symIndexId = sym.symIndexId;
            symTag = sym.symTag;
            type = sym.type;
            typeId = sym.typeId;

            uint size = 0;
            uint count = 0;
            sym.get_types(size, out count, out types[0]);
        }
    }
}
