using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Dia2Lib;

namespace pbdcstest.Blocks
{
    class Annotation
    {
        public IDiaSymbol symbol;

        public uint addressOffset;
        public uint addressSection;
        public uint dataKind;
        public uint relativeVirtualAddress;
        public uint symIndexId;
        public uint symTag;
        public dynamic value;
        public ulong virtualAddress;

        public Annotation(IDiaSymbol sym)
        {
            symbol = sym;

            addressOffset = sym.addressOffset;
            addressSection = sym.addressSection;
            dataKind = sym.dataKind;
            relativeVirtualAddress = sym.relativeVirtualAddress;
            symIndexId = sym.symIndexId;
            symTag = sym.symTag;
            value = sym.value;
            virtualAddress = sym.virtualAddress;
        }
    }
}
