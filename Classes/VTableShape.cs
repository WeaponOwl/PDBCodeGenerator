using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Dia2Lib;

namespace pbdcstest.Classes
{
    class VTableShape
    {
        public IDiaSymbol symbol;

        public bool constType;
        public uint count;
        public IDiaSymbol lexicalParent;
        public uint lexicalParentId;
        public uint symIndexId;
        public uint symTag;
        public bool unalignedType;
        public bool volatileType;

        public VTableShape(IDiaSymbol sym)
        {
            symbol = sym;

            constType = sym.constType != 0;
            count = sym.count;
            lexicalParent = sym.lexicalParent;
            lexicalParentId = sym.lexicalParentId;
            symIndexId = sym.symIndexId;
            symTag = sym.symTag;
            unalignedType = sym.unalignedType != 0;
            volatileType = sym.volatileType != 0;
        }
    }
}
