using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Dia2Lib;

namespace pbdcstest.Classes
{
    class VTable
    {
        public IDiaSymbol symbol;

        public IDiaSymbol classParent;
        public uint classParentId;
        public bool constType;
        public IDiaSymbol lexicalParent;
        public uint lexicalParentId;
        public uint symIndexId;
        public uint symTag;
        public IDiaSymbol type;
        public uint typeId;
        public bool unalignedType;
        public bool volatileType;

        public VTable(IDiaSymbol sym)
        {
            symbol = sym;

            classParent = sym.classParent;
            classParentId = sym.classParentId;
            constType = sym.constType != 0;
            lexicalParent = sym.lexicalParent;
            lexicalParentId = sym.lexicalParentId;
            symIndexId = sym.symIndexId;
            symTag = sym.symTag;
            type = sym.type;
            typeId = sym.typeId;
            unalignedType = sym.unalignedType != 0;
            volatileType = sym.volatileType != 0;
        }
    }
}
