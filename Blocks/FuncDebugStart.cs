using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Dia2Lib;

namespace pbdcstest.Blocks
{
    class FuncDebugStart
    {
        public IDiaSymbol symbol;

        public uint addressOffset;
        public uint addressSection;
        public bool customCallingConvention;
        public bool farReturn;
        public bool interruptReturn;
        public bool isStatic;
        public IDiaSymbol lexicalParent;
        public uint lexicalParentId;
        public uint locationType;
        public bool noInline;
        public bool noReturn;
        public bool notReached;
        public int offset;
        public bool optimizedCodeDebugInfo;
        public uint relativeVirtualAddress;
        public uint symIndexId;
        public uint symTag;
        public ulong virtualAddress;

        public FuncDebugStart(IDiaSymbol sym)
        {
            symbol = sym;

            addressOffset = sym.addressOffset;
            addressSection = sym.addressSection;
            customCallingConvention = sym.customCallingConvention!=0;
            farReturn = sym.farReturn != 0;
            interruptReturn = sym.interruptReturn != 0;
            isStatic = sym.isStatic != 0;
            lexicalParent = sym.lexicalParent;
            lexicalParentId = sym.lexicalParentId;
            locationType = sym.locationType;
            noInline = sym.noInline != 0;
            noReturn = sym.noReturn != 0;
            notReached = sym.notReached != 0;
            offset = sym.offset;
            optimizedCodeDebugInfo = sym.optimizedCodeDebugInfo != 0;
            relativeVirtualAddress = sym.relativeVirtualAddress;
            symIndexId = sym.symIndexId;
            symTag = sym.symTag;
            virtualAddress = sym.virtualAddress;
        }
    }
}
