using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Dia2Lib;

namespace pbdcstest.Classes
{
    class Friend
    {
        public IDiaSymbol symbol;

        public IDiaSymbol classParent;
        public uint classParentId;
        public string name;
        public uint symIndexId;
        public uint symTag;
        public IDiaSymbol type;
        public uint typeId;

        public Friend(IDiaSymbol sym)
        {
            symbol = sym;

            classParent = sym.classParent;
            classParentId = sym.classParentId;
            name = sym.name;
            symIndexId = sym.symIndexId;
            symTag = sym.symTag;
            type = sym.type;
            typeId = sym.typeId;
        }
    }
}
