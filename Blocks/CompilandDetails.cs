using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Dia2Lib;

namespace pbdcstest.Blocks
{
    class CompilandDetails
    {
        public IDiaSymbol symbol;

        public uint backEndBuild;
        public uint backEndMajor;
        public uint backEndMinor;
        public string compilerName;
        public bool editAndContinueEnabled;
        public uint frontEndBuild;
        public uint frontEndMajor;
        public uint frontEndMinor;
        public bool hasDebugInfo;
        public bool hasManagedCode;
        public bool hasSecurityChecks;
        public bool isCVTCIL;
        public bool isDataAligned;
        public bool isHotpatchable;
        public bool isLTCG;
        public bool isMSILNetmodule;
        public uint language;
        public IDiaSymbol lexicalParent;
        public uint lexicalParentId;
        public uint platform;
        public uint symIndexId;
        public uint symTag;

        public CompilandDetails(IDiaSymbol sym)
        {
            symbol = sym;

            backEndBuild = sym.backEndBuild;
            backEndMajor = sym.backEndMajor;
            backEndMinor = sym.backEndMinor;
            compilerName = sym.compilerName;
            editAndContinueEnabled = sym.editAndContinueEnabled != 0;
            frontEndBuild = sym.frontEndBuild;
            frontEndMajor = sym.frontEndMajor;
            frontEndMinor = sym.frontEndMinor;
            hasDebugInfo = sym.hasDebugInfo != 0;
            hasManagedCode = sym.hasManagedCode != 0;
            hasSecurityChecks = sym.hasSecurityChecks != 0;
            isCVTCIL = sym.isCVTCIL != 0;
            isDataAligned = sym.isDataAligned != 0;
            isHotpatchable = sym.isHotpatchable != 0;
            isLTCG = sym.isLTCG != 0;
            isMSILNetmodule = sym.isMSILNetmodule != 0;
            language = sym.language;
            lexicalParent = sym.lexicalParent;
            lexicalParentId = sym.lexicalParentId;
            platform = sym.platform;
            symIndexId = sym.symIndexId;
            symTag = sym.symTag;
        }
    }
}
