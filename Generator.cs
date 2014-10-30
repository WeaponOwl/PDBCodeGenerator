using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pbdcstest
{
    class Member
    {
        public string name;
        public int offcet;
        public ulong length;
        public string type;
        public string access;
        public uint id;
    }

    class BaseClass
    {
        public string type;
        public int offcet;
        public ulong length;
        public uint id;
        public uint new_id;
    }

    class Function
    {
        public string undname;
        public string name;
        public string type;
        public string access;
        public string filename;
        public uint id;
    }

    class Typedef
    {
        public string name;
        public string type;
        public uint id;
    }

    class Enum
    {
        public string name;
        public uint id;

        public SubEnum[] values;
    }

    class SubEnum
    {
        public string name;
        public dynamic value;
        public uint id;
    }

    class VTable
    {
        public ulong count;
        public string type;
        public uint id;
    }

    class SubStructure
    {
        public string name;
        public uint id;
        public uint new_id;
    }

    class Structure
    {
        public string name;
        public uint id;
        public uint new_id;

        public Member[] members;
        public BaseClass[] baseclass;
        public Function[] functions;
        public Typedef[] typedefs;
        public Enum[] enums;
        public VTable[] vtables;
        public SubStructure[] substructures;

        public List<string> filenames;
        public List<int> substructures_new_ids;
        public List<int> usedin_new_ids;
    }

    struct Compiland
    {
        public string source;
        public bool have_header;
        public string[] includes;
    }

    struct File
    {
        public string source;
        public bool have_header;
        public string[] includes;
    }
}
