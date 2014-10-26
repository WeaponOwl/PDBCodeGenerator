using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Dia2Lib;

namespace pbdcstest.Classes
{
    class Dimension
    {
        public IDiaSymbol symbol;

        public IDiaSymbol lowerBound;
        public uint lowerBoundId;
        public uint symIndexId;
        public uint symTag;
        public IDiaSymbol upperBound;
        public uint upperBoundId;

        public Dimension(IDiaSymbol sym)
        {
            symbol = sym;

            lowerBound = sym.lowerBound;
            lowerBoundId = sym.lowerBoundId;
            symIndexId = sym.symIndexId;
            symTag = sym.symTag;
            upperBound = sym.upperBound;
            upperBoundId = sym.upperBoundId;
        }
    }
}
