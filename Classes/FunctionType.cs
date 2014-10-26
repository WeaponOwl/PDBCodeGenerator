using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Dia2Lib;

namespace pbdcstest.Classes
{
    class FunctionType
    {
        public IDiaSymbol symbol;

        public uint callingConvention;
        public IDiaSymbol classParent;
        public uint classParentId;
        public bool constType;
        public uint count;
        public IDiaSymbol lexicalParent;
        public uint lexicalParentId;
        public IDiaSymbol objectPointerType;
        public uint symIndexId;
        public uint symTag;
        public int thisAdjust;
        public IDiaSymbol type;
        public uint typeId;
        public bool unalignedType;
        public bool volatileType;

        public FunctionType(IDiaSymbol sym)
        {
            symbol = sym;

            callingConvention = sym.callingConvention;
            classParent = sym.classParent;
            classParentId = sym.classParentId;
            constType = sym.constType != 0;
            count = sym.count;
            lexicalParent = sym.lexicalParent;
            lexicalParentId = sym.lexicalParentId;
            objectPointerType = sym.objectPointerType;
            symIndexId = sym.symIndexId;
            symTag = sym.symTag;
            thisAdjust = sym.thisAdjust;
            type = sym.type;
            typeId = sym.typeId;
            unalignedType = sym.unalignedType != 0;
            volatileType = sym.volatileType != 0;
        }
    }
}
