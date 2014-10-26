using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Dia2Lib;

namespace pbdcstest.Blocks
{
    class Block
    {
        public IDiaSymbol symbol;

        public uint addressOffset;
        public uint addressSection;
        public ulong length;
        public IDiaSymbol lexicalParent;
        public uint lexicalParentId;
        public uint locationType;
        public string name;
        public uint relativeVirtualAddress;
        public uint symIndexId;
        public uint symTag;
        public ulong virtualAddress;

        public Block(IDiaSymbol sym)
        {
            symbol = sym;

            addressOffset = sym.addressOffset;
            addressSection = sym.addressSection;
            length = sym.length;
            lexicalParent = sym.lexicalParent;
            lexicalParentId = sym.lexicalParentId;
            locationType = sym.locationType;
            name = sym.name;
            relativeVirtualAddress = sym.relativeVirtualAddress;
            symIndexId = sym.symIndexId;
            symTag = sym.symTag;
            virtualAddress = sym.virtualAddress;
        }
    }
}
