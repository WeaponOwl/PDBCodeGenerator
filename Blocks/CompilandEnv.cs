using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Dia2Lib;

namespace pbdcstest.Blocks
{
    class CompilandEnv
    {
        public IDiaSymbol symbol;

        public IDiaSymbol lexicalParent;
        public uint lexicalParentId;
        public string name;
        public uint symIndexId;
        public uint symTag;
        public dynamic value;

        public CompilandEnv(IDiaSymbol sym)
        {
            symbol = sym;

            lexicalParent = sym.lexicalParent;
            lexicalParentId = sym.lexicalParentId;
            name = sym.name;
            symIndexId = sym.symIndexId;
            symTag = sym.symTag;
            value = sym.value;
        }
    }
}
