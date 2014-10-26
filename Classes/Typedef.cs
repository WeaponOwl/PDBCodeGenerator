using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Dia2Lib;

namespace pbdcstest.Classes
{
    class Typedef
    {
        public IDiaSymbol symbol;

        public uint baseType;
        public IDiaSymbol classParent;
        public uint classParentId;
        public bool constructor;
        public bool constType;
        public bool hasAssignmentOperator;
        public bool hasCastOperator;
        public bool hasNestedTypes;
        public ulong length;
        public IDiaSymbol lexicalParent;
        public uint lexicalParentId;
        public string name;
        public bool nested;
        public bool overloadedOperator;
        public bool packed;
        public bool reference;
        public bool scoped;
        public uint symIndexId;
        public uint symTag;
        public IDiaSymbol type;
        public uint typeId;
        public uint udtKind;
        public bool unalignedType;
        public IDiaSymbol virtualTableShape;
        public uint virtualTableShapeId;
        public bool volatileType;

        public Typedef(IDiaSymbol sym)
        {
            symbol = sym;

            baseType = sym.baseType;
            classParent = sym.classParent;
            classParentId = sym.classParentId;
            constructor = sym.constructor != 0;
            constType = sym.constType != 0;
            hasAssignmentOperator = sym.hasAssignmentOperator != 0;
            hasCastOperator = sym.hasCastOperator != 0;
            hasNestedTypes = sym.hasNestedTypes != 0;
            length = sym.length;
            lexicalParent = sym.lexicalParent;
            lexicalParentId = sym.lexicalParentId;
            name = sym.name;
            nested = sym.nested != 0;
            overloadedOperator = sym.overloadedOperator != 0;
            packed = sym.packed != 0;
            reference = sym.reference != 0;
            scoped = sym.scoped != 0;
            symIndexId = sym.symIndexId;
            symTag = sym.symTag;
            type = sym.type;
            typeId = sym.typeId;
            udtKind = sym.udtKind;
            unalignedType = sym.unalignedType != 0;
            virtualTableShape = sym.virtualTableShape;
            virtualTableShapeId = sym.virtualTableShapeId;
            volatileType = sym.volatileType != 0;
        }
    }
}