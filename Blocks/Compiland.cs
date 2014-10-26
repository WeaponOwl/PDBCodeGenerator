using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Dia2Lib;

namespace pbdcstest.Blocks
{
    class Compiland
    {
        public IDiaSymbol symbol;

        public bool editAndContinueEnabled;
        public IDiaSymbol lexicalParent;
        public uint lexicalParentId;
        public string libraryName;
        public string name;
        public string sourceFileName;
        public uint symIndexId;
        public uint symTag;

        public Compiland(IDiaSymbol sym)
        {
            symbol = sym;

            editAndContinueEnabled = sym.editAndContinueEnabled != 0;
            lexicalParent = sym.lexicalParent;
            lexicalParentId = sym.lexicalParentId;
            libraryName = sym.libraryName;
            name = sym.name;
            sourceFileName = sym.sourceFileName;
            symIndexId = sym.symIndexId;
            symTag = sym.symTag;

        }
    }
}
