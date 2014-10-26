using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Dia2Lib;

namespace pbdcstest.Classes
{
    class FunctionArgType
    {
        public IDiaSymbol symbol;

        public IDiaSymbol classParent;
        public uint classParentId;
        public IDiaSymbol lexicalParent;
        public uint lexicalParentId;
        public uint symIndexId;
        public uint symTag;
        public IDiaSymbol type;
        public uint typeId;

        public FunctionArgType(IDiaSymbol sym)
        {
            symbol = sym;

            classParent = sym.classParent;
            classParentId = sym.classParentId;
            lexicalParent = sym.lexicalParent;
            lexicalParentId = sym.lexicalParentId;
            symIndexId = sym.symIndexId;
            symTag = sym.symTag;
            type = sym.type;
            typeId = sym.typeId;
        }
    }
}
