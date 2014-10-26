using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Dia2Lib;

namespace pbdcstest.Blocks
{
    class Thunk
    {
        public IDiaSymbol symbol;

        public uint access;
        public uint addressOffset;
        public uint addressSection;
        public IDiaSymbol classParent;
        public uint classParentId;
        public bool constType;
        public bool intro;
        public bool isStatic;
        public ulong length;
        public IDiaSymbol lexicalParent;
        public uint lexicalParentId;
        public uint locationType;
        public string name;
        public bool pure;
        public uint relativeVirtualAddress;
        public uint symIndexId;
        public uint symTag;
        public uint targetOffset;
        public uint targetRelativeVirtualAddress;
        public uint targetSection;
        public ulong targetVirtualAddress;
        public uint thunkOrdinal;
        public IDiaSymbol type;
        public uint typeId;
        public bool unalignedType;
        public bool @virtual;
        public ulong virtualAddress;
        public uint virtualBaseOffset;
        public bool volatileType;

        public Thunk(IDiaSymbol sym)
        {
            symbol = sym;

            access                          =sym.access;
            addressOffset                   =sym.addressOffset;
            addressSection                  =sym.addressSection;
            classParent                     =sym.classParent;
            classParentId                   =sym.classParentId;
            constType                       =sym.constType!=0;
            intro                           =sym.intro!=0;
            isStatic                        =sym.isStatic!=0;
            length                          =sym.length;
            lexicalParent                   =sym.lexicalParent;
            lexicalParentId                 =sym.lexicalParentId;
            locationType                    =sym.locationType;
            name                            =sym.name;
            pure                            =sym.pure!=0;
            relativeVirtualAddress          =sym.relativeVirtualAddress;
            symIndexId                      =sym.symIndexId;
            symTag                          =sym.symTag;
            targetOffset                    =sym.targetOffset;
            targetRelativeVirtualAddress    =sym.targetRelativeVirtualAddress;
            targetSection                   =sym.targetSection;
            targetVirtualAddress            =sym.targetVirtualAddress;
            thunkOrdinal                    =sym.thunkOrdinal;
            type                            =sym.type;
            typeId                          =sym.typeId;
            unalignedType                   =sym.unalignedType!=0;
            @virtual                        =sym.@virtual!=0;
            virtualAddress                  =sym.virtualAddress;
            virtualBaseOffset               =sym.virtualBaseOffset;
            volatileType                    =sym.volatileType!=0;
        }
    }
}
