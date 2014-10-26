using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Dia2Lib;

namespace pbdcstest.Classes
{
    class ManagedType
    {
        public IDiaSymbol symbol;

        public string name;
        public uint symIndexId;
        public uint symTag;

        public ManagedType(IDiaSymbol sym)
        {
            symbol = sym;

            name = sym.name;
            symIndexId = sym.symIndexId;
            symTag = sym.symTag;
        }
    }
}
