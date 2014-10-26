using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Dia2Lib;

namespace pbdcstest.Blocks
{
    class Exe
    {
        public IDiaSymbol symbol;

        public uint age;
        public Guid guid;
        public bool isCTypes;
        public bool isStripped;
        public uint machineType;
        public string name;
        public uint signature;
        public string symbolsFileName;
        public uint symIndexId;
        public uint symTag;

        public Exe(IDiaSymbol sym)
        {
            symbol = sym;

            age = sym.age;
            guid = sym.guid;
            isCTypes = sym.isCTypes!=0;
            isStripped = sym.isStripped!=0;
            machineType = sym.machineType;
            name = sym.name;
            signature = sym.signature;
            symbolsFileName = sym.symbolsFileName;
            symIndexId = sym.symIndexId;
            symTag = sym.symTag;
        }
    }
}
