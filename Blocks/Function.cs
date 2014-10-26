using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Dia2Lib;

namespace pbdcstest.Blocks
{
    class Function
    {
        public IDiaSymbol symbol;

        public uint access;
        public uint addressOffset;
        public uint addressSection;
        public IDiaSymbol classParent;
        public uint classParentId;
        public bool constType;
        public bool customCallingConvention;
        public bool farReturn;
        public bool hasAlloca;
        public bool hasEH;
        public bool hasEHa;
        public bool hasInlAsm;
        public bool hasLongJump;
        public bool hasSecurityChecks;
        public bool hasSEH;
        public bool hasSetJump;
        public bool interruptReturn;
        public bool intro;
        public bool InlSpec;
        public bool isNaked;
        public bool isStatic;
        public ulong length;
        public IDiaSymbol lexicalParent;
        public uint lexicalParentId;
        public uint locationType;
        public string name;
        public bool noInline;
        public bool notReached;
        public bool noReturn;
        public bool noStackOrdering;
        public bool optimizedCodeDebugInfo;
        public bool pure;
        public uint relativeVirtualAddress;
        public uint symIndexId;
        public uint symTag;
        public uint token;
        public IDiaSymbol type;
        public uint typeId;
        public bool unalignedType;
        public string undecoratedName;
        public string undecoratedNameEx;
        public bool @virtual;
        public ulong virtualAddress;
        public uint virtualBaseOffset;
        public bool volatileType;

        public Function(IDiaSymbol sym)
        {
            symbol = sym;

            access = sym.access;
            addressOffset = sym.addressOffset;
            addressSection = sym.addressSection;
            classParent = sym.classParent;
            classParentId = sym.classParentId;
            constType = sym.constType != 0;
            customCallingConvention = sym.customCallingConvention != 0;
            farReturn = sym.farReturn != 0;
            hasAlloca = sym.hasAlloca != 0;
            hasEH = sym.hasEH != 0;
            hasEHa = sym.hasEHa != 0;
            hasInlAsm = sym.hasInlAsm != 0;
            hasLongJump = sym.hasLongJump != 0;
            hasSecurityChecks = sym.hasSecurityChecks != 0;
            hasSEH = sym.hasSEH != 0;
            hasSetJump = sym.hasSetJump != 0;
            interruptReturn = sym.interruptReturn != 0;
            intro = sym.intro != 0;
            InlSpec = sym.inlSpec != 0;
            isNaked = sym.isNaked != 0;
            isStatic = sym.isStatic != 0;
            length = sym.length;
            lexicalParent = sym.lexicalParent;
            lexicalParentId = sym.lexicalParentId;
            locationType = sym.locationType;
            name = sym.name;
            noInline = sym.noInline != 0;
            notReached = sym.notReached != 0;
            noReturn = sym.noReturn != 0;
            noStackOrdering = sym.noStackOrdering != 0;
            optimizedCodeDebugInfo = sym.optimizedCodeDebugInfo != 0;
            pure = sym.pure != 0;
            relativeVirtualAddress = sym.relativeVirtualAddress;
            symIndexId = sym.symIndexId;
            symTag = sym.symTag;
            token = sym.token;
            type = sym.type;
            typeId = sym.typeId;
            unalignedType = sym.unalignedType != 0;
            undecoratedName = sym.undecoratedName;
            sym.get_undecoratedNameEx(0, out undecoratedNameEx);
            @virtual = sym.@virtual != 0;
            virtualAddress = sym.virtualAddress;
            virtualBaseOffset = sym.virtualBaseOffset;
            volatileType = sym.volatileType != 0;
        }
    }
}
