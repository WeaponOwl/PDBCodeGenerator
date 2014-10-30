using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Dia2Lib;

namespace pbdcstest
{
    class Program
    {
        public class StructSortClass : IComparer
        {
            int IComparer.Compare(Object x, Object y)
            {
                return (string.Compare(((Structure)x).name,((Structure)y).name));
            }
        }
        public class FileWithStructures
        {
            public string name;
            public List<int> structures;
            public List<string> includes;

            public FileWithStructures(string name)
            {
                this.name=name;
                structures = new List<int>();
                includes = new List<string>();
            }
        }

        static void PrintType(IDiaSymbol symbol, IDiaSession session,ref string s_type)
        {
            if (symbol.symTag != (int)SymTagEnum.SymTagPointerType)
            {
                if (symbol.constType == 1)
                    s_type += "const ";                
                if (symbol.volatileType == 1)
                    s_type += "volatile ";                
                if (symbol.unalignedType == 1)
                    s_type += "__unaligned ";
            }

            switch ((SymTagEnum)symbol.symTag)
            {
                case SymTagEnum.SymTagPointerType:
                    Classes.PointerType pointer = new Classes.PointerType(symbol);

                    PrintType(pointer.type, session, ref s_type);

                    s_type += pointer.reference ? "&" : "*";

                    if (symbol.constType == 1)
                        s_type += "const ";
                    if (symbol.volatileType == 1)
                        s_type += "volatile ";
                    if (symbol.unalignedType == 1)
                        s_type += "__unaligned ";

                    break;


                case SymTagEnum.SymTagBaseType:
                    Classes.BaseType basetype = new Classes.BaseType(symbol);
                    switch (basetype.baseType)
                    {
                        case (int)BasicType.btUInt:
                            s_type += "unsigned ";
                            switch (basetype.length)
                            {
                                case 1:s_type += "char ";break;
                                case 2:s_type += "short ";break;
                                case 4:s_type += "int ";break;
                                case 8:s_type += "__int64 ";break;
                            }
                            break;
                        case (int)BasicType.btInt:
                            s_type += "signed ";
                            switch (basetype.length)
                            {
                                case 1:s_type += "char ";break;
                                case 2:s_type += "short ";break;
                                case 4:s_type += "int ";break;
                                case 8:s_type += "__int64 ";break;
                            }
                            break;
                        case (int)BasicType.btFloat:
                            switch (basetype.length)
                            {
                                case 4:s_type += "float ";break;
                                case 8:s_type += "double ";break;
                            }
                            break;
                        default:s_type += PrintSymbolWrapper.rgBaseType[basetype.baseType];break;
                    }
                    break;

                case SymTagEnum.SymTagUDT:
                    Classes.UTD utd = new Classes.UTD(symbol);
                    s_type += utd.name;
                    break;

                case SymTagEnum.SymTagArrayType:
                    Classes.ArrayType array = new Classes.ArrayType(symbol);
                    PrintType(array.type, session, ref s_type);
                    s_type += "[" + array.count + "]";
                    break;

                case SymTagEnum.SymTagVTableShape:
                    Classes.VTableShape shape = new Classes.VTableShape(symbol);
                    s_type += "vtable[" + shape.count + "]";
                    break;

                case SymTagEnum.SymTagEnum:
                    Classes.Enum @enum = new Classes.Enum(symbol);
                    s_type += @enum.name;
                    break;

                case SymTagEnum.SymTagFunctionType:
                    Classes.FunctionType funktype = new Classes.FunctionType(symbol);
                    PrintType(funktype.type, session, ref s_type);
                    s_type += " " + PrintSymbolWrapper.rgCallingConvention[funktype.callingConvention] + " ";

                    IDiaEnumSymbols subsymbols;
                    symbol.findChildren(SymTagEnum.SymTagNull, null, 0, out subsymbols);
                    s_type += "(";
                    foreach (IDiaSymbol sym in subsymbols)
                    {
                        PrintType(sym, session, ref s_type);
                        s_type += ",";
                    }
                    s_type += ")";
                    s_type = s_type.Replace(",)", ")");
                    break;

                case SymTagEnum.SymTagFunctionArgType:
                    Classes.FunctionArgType arg = new Classes.FunctionArgType(symbol);
                    PrintType(arg.type, session, ref s_type);
                    break;

                default:
                    break;
            }
        }
        static void PrintData(Blocks.Data data, IDiaSession session,ref Member s_member)
        {
            SymTagEnum symTag = (SymTagEnum)data.type.symTag;

            s_member.access = PrintSymbolWrapper.rgAccess[(int)data.access];
            s_member.length = data.type.length;
            s_member.name = data.name;
            s_member.offcet = data.offset;
            s_member.type = "";
            s_member.id = data.type.symIndexId;

            PrintType(data.type, session, ref s_member.type);
        }
        static void PrintBaseClass(Classes.BaseClass baseclass, IDiaSession session,ref BaseClass s_baseclass)
        {
            s_baseclass.type = "";
            s_baseclass.offcet = baseclass.offset;
            s_baseclass.length = baseclass.length;
            s_baseclass.id = baseclass.type.symIndexId;

            PrintType(baseclass.type, session, ref s_baseclass.type);
        }
        static void PrintFunction(Blocks.Function function, IDiaSession session, ref Function s_function)
        {
            if (function.undecoratedName != null)
            {
                s_function.undname = function.undecoratedName;
            }

            s_function.name = function.name;
            s_function.type = function.isStatic ? "static " : "";
            s_function.access = PrintSymbolWrapper.rgAccess[function.access];

            PrintType(function.type, session, ref s_function.type);

            s_function.id = function.symIndexId;
            s_function.filename = "";

            uint addr = function.relativeVirtualAddress;
            IDiaEnumLineNumbers enumLines;
            session.findLinesByRVA(addr, (uint)function.length, out enumLines);
            if (enumLines.count > 0)
            {
                foreach (IDiaLineNumber line in enumLines)
                {
                    s_function.filename += ":" + line.sourceFile.fileName + "[" + enumLines.count + "]";
                    break;
                }
            }
        }
        static void PrintTypedef(Classes.Typedef type, IDiaSession session,ref Typedef s_typedef)
        {
            s_typedef.name = type.name;
            s_typedef.type = "";
            s_typedef.id = type.symIndexId;

            PrintType(type.type, session, ref s_typedef.type);
        }
        static void PrintUTD(Classes.UTD utd, IDiaSession session,ref SubStructure s_utd)
        {
            s_utd.name = utd.name;
            s_utd.id = utd.symIndexId;
        }
        static void PrintEnum(Classes.Enum @enum, IDiaSession session,ref Enum s_enum)
        {
            s_enum.name = @enum.name;
            s_enum.id = @enum.symIndexId;

            IDiaEnumSymbols childrens;
            @enum.symbol.findChildren(SymTagEnum.SymTagNull, null, 0, out childrens);
            s_enum.values = new SubEnum[childrens.count];

            int i=0;
            foreach (IDiaSymbol s in childrens)
            {
                s_enum.values[i] = new SubEnum();
                s_enum.values[i].id = s.symIndexId;
                s_enum.values[i].name = s.name;
                s_enum.values[i].value = s.value;
                i++;
            }
        }
        static void PrintVTable(Classes.VTable table, IDiaSession session,ref VTable s_vtable)
        {
            s_vtable.count = table.type.length;
            s_vtable.type = "";
            s_vtable.id = table.type.symIndexId;

            PrintType(table.type, session, ref s_vtable.type);
        }
        static void PrintStructEx(Classes.UTD utd, IDiaSession session, ref Structure structure)
        {
            structure.id = utd.symIndexId;
            structure.name = utd.name;

            int members = 0;
            int baseclasses = 0;
            int substructures = 0;
            int functions = 0;
            int typedefs = 0;
            int vtables = 0;
            int enums = 0;
            IDiaEnumSymbols sub;
            session.findChildren(utd.symbol, SymTagEnum.SymTagNull, null, 0, out sub);
            foreach (IDiaSymbol subsym in sub)
            {
                SymTagEnum tag = (SymTagEnum)subsym.symTag;
                switch (tag)
                {
                    case SymTagEnum.SymTagData: members++; break;
                    case SymTagEnum.SymTagBaseClass: baseclasses++; break;
                    case SymTagEnum.SymTagFunction: functions++; break;
                    case SymTagEnum.SymTagTypedef: typedefs++; break;
                    case SymTagEnum.SymTagUDT: substructures++; break;
                    case SymTagEnum.SymTagEnum: enums++; break;
                    case SymTagEnum.SymTagVTable: vtables++; break;
                    default: break;
                }
            }
            if (members > 0) structure.members = new Member[members];
            if (baseclasses > 0) structure.baseclass = new BaseClass[baseclasses];
            if (functions > 0) structure.functions = new Function[functions];
            if (typedefs > 0) structure.typedefs= new Typedef[typedefs];
            if (substructures > 0) structure.substructures = new SubStructure[substructures];
            if (enums > 0) structure.enums = new Enum[enums];
            if (vtables > 0) structure.vtables = new VTable[vtables];
            members = 0;
            baseclasses = 0;
            substructures = 0;
            functions = 0;
            typedefs = 0;
            vtables = 0;
            enums = 0;

            sub.Reset();
            foreach (IDiaSymbol subsym in sub)
            {
                SymTagEnum tag = (SymTagEnum)subsym.symTag;

                switch (tag)
                {
                    case SymTagEnum.SymTagData:

                        Blocks.Data data = new Blocks.Data(subsym);
                        structure.members[members] = new Member();
                        PrintData(data, session,ref structure.members[members]);
                        members++;
                        break;

                    case SymTagEnum.SymTagBaseClass:

                        Classes.BaseClass baseclass = new Classes.BaseClass(subsym);
                        structure.baseclass[baseclasses] = new BaseClass();
                        PrintBaseClass(baseclass, session, ref structure.baseclass[baseclasses]);
                        baseclasses++;
                        break;

                    case SymTagEnum.SymTagFunction:

                        Blocks.Function funk = new Blocks.Function(subsym);
                        structure.functions[functions] = new Function();
                        PrintFunction(funk, session, ref structure.functions[functions]);
                        functions++;
                        break;

                    case SymTagEnum.SymTagTypedef:

                        Classes.Typedef type = new Classes.Typedef(subsym);
                        structure.typedefs[typedefs] = new Typedef();
                        PrintTypedef(type, session, ref structure.typedefs[typedefs]);
                        typedefs++;
                        break;

                    case SymTagEnum.SymTagUDT:

                        Classes.UTD subutd = new Classes.UTD(subsym);
                        structure.substructures[substructures] = new SubStructure();
                        PrintUTD(subutd, session, ref structure.substructures[substructures]);
                        substructures++;
                        break;

                    case SymTagEnum.SymTagEnum:

                        Classes.Enum @enum = new Classes.Enum(subsym);
                        structure.enums[enums] = new Enum();
                        PrintEnum(@enum, session, ref structure.enums[enums]);
                        enums++;
                        break;

                    case SymTagEnum.SymTagVTable:

                        Classes.VTable vtable = new Classes.VTable(subsym);
                        structure.vtables[vtables] = new VTable();
                        PrintVTable(vtable, session, ref structure.vtables[vtables]);
                        vtables++;
                        break;

                    default:
                        break;
                }
            }
        }

        static void FindAllSubstruct(Structure s, Structure[] structures, ref List<int> substructureslist)
        {
            foreach (int ss in s.substructures_new_ids)
            {
                if ((uint)ss != s.new_id)
                {
                    if (!substructureslist.Contains(ss))
                    {
                        substructureslist.Add(ss);
                        FindAllSubstruct(structures[ss], structures, ref substructureslist);
                    }
                }
            }
        }
        static void SetDepthForStructs(Structure[] structures, int[] structureids, ref int[] structuredepth)
        {
            for (int i = 0; i < structureids.Length; i++)
            {
                int id = structureids[i];
                List<int> sub = structures[id].substructures_new_ids;
                for (int q = 0; q < structureids.Length; q++)
                    if (q != i && sub.Contains(structureids[q])) structuredepth[q]++;
            }
        }

        static void WriteStruct(Structure s, System.IO.StreamWriter stream)
        {
            string sname = s.name;
            if (s.name.StartsWith("std::")) return;
            string defineline = "_" + s.name.ToUpper().Replace(':', '_') + "_";
            stream.WriteLine("#ifndef " + defineline + "\n#define " + defineline);

            string[] parents = s.name.Split(new string[] { "::" }, StringSplitOptions.RemoveEmptyEntries);
            if (parents.Length > 1)
            {
                stream.Write("namespace " + parents[0] + "\n{\n");
                sname = sname.Replace(parents[0] + "::", "");
            }

            stream.Write("struct " + sname);
            if (s.baseclass != null)
            {
                int k = 0;
                foreach (BaseClass b in s.baseclass)
                {
                    string type = b.type.Replace(parents[0] + "::", "");

                    stream.Write((k == 0 ? ":" : ",") + type);
                    k++;
                }
            }
            stream.Write("\n{\n");

            if (s.members != null)
            {
                foreach (Member m in s.members)
                {
                    string arraypartoftype = "";
                    string type = m.type;
                    if (m.type.Contains('['))
                    {
                        arraypartoftype = m.type.Substring(type.IndexOf('['));
                        type = type.Remove(m.type.IndexOf('['));
                    }
                    stream.Write("\t" + m.access + ": " + type + " " + m.name + arraypartoftype + ";\n");
                }
                stream.Write("\n");
            }

            if (s.enums != null)
            {
                foreach (Enum e in s.enums)
                {
                    stream.Write("\tenum" + e.name + "{\n");
                    foreach (SubEnum se in e.values)
                        stream.Write("\t\t" + se.name + "=" + se.value + "\n");
                    stream.Write("\t{\n");
                }
                stream.Write("\n");
            }

            if (s.functions != null)
            {
                foreach (Function f in s.functions)
                {
                    if (f.undname != null)
                    {
                        string name = f.undname;
                        if (name.Contains(s.name))
                            name = name.Replace(s.name + "::", "");

                        stream.Write("\t" + name + ";\n");
                    }
                    else
                    {
                        string name = f.name;
                        if (name.Contains(s.name))
                            name = name.Replace(s.name + "::", "");

                        int start = f.type.IndexOf('(');
                        int end = f.type.IndexOf(')');
                        string type = start < 0 ? "" : f.type.Substring(0, start);
                        string arg = start < 0 ? "" : f.type.Substring(start, end - start + 1);

                        stream.Write("\t" + f.access + ": " + type + " " + name + " " + arg + ";\n");
                    }
                }
                stream.Write("\n");
            }

            //if (s.substructures != null)
            //{
            //    foreach (SubStructure sub in s.substructures)
            //    {
            //        stream.Write("\tstructure /*id:" + sub.id + "*/ " + sub.name + ";\n");
            //    }
            //    stream.Write("\n");
            //}

            if (s.typedefs != null)
            {
                foreach (Typedef t in s.typedefs)
                {
                    stream.Write("\ttypedef " + t.type + " " + t.name + ";\n");
                }
                stream.Write("\n");
            }

            stream.Write("};\n");
            if (parents.Length > 1)
                stream.Write("}\n");
            stream.Write("#endif\n");
        }
        static void WriteStructData(Structure s,Structure[] structures,System.IO.StreamWriter stream)
        {
            List<int> substructures = new List<int>();
            FindAllSubstruct(s, structures, ref substructures);
            List<string> includelist = new List<string>();
            List<int> unincludedlist = new List<int>();
            substructures.Sort();

            foreach (int ss in substructures)
            {
                Structure s2 = structures[ss];
                if (s2.filenames.Count > 0)
                {
                    string filename = "";
                    foreach (string fn in s2.filenames)
                        if (fn.Contains(".h")) filename = fn;

                    if (filename.Length > 0)
                        includelist.Add(filename);
                        //stream.WriteLine("#include <" + filename.Substring(filename.LastIndexOf("\\") + 1) + ">");
                    else unincludedlist.Add(ss);
                }
                unincludedlist.Add(ss);
            }

            foreach (string include in includelist)
                stream.WriteLine("#include <" + include.Substring(include.LastIndexOf("\\") + 1) + ">");

            foreach (int un in unincludedlist)
            {
                stream.WriteLine("");
                WriteStruct(structures[un], stream);
            }

            stream.WriteLine("");
            WriteStruct(s, stream);
        }

        #region Jump
        static string[] reservedbegin = new string[]{
            "_LARGE_INTEGER",
            "se_translator",
            "sym_engine",
            "`anonymous-namespace'",
            "CSQL",
            "basic_debugbuf",
            //"S_FVector",
            //"S_BVECTOR",
            "ui",
            "tagPROPVARIANT",
            "tagVARIANT",
            "_ULARGE_INTEGER",
            "tagDEC",
            "tagTYPEDESC",
            "tagCY",
            "tagVARDESC"
        };
        static string[] reserved = new string[]{
            "_iobuf",
            "exception",
            "_Ctypevec",
            "_Collvec",
            "_Cvtvec",
            "lconv",
            "_Collvec",
            "_Cvtvec",
            "_Dconst",
            "bad_cast",
            "_GUID",
            "tagPROPVARIANT",
            "_LARGE_INTEGER",
            "_ULARGE_INTEGER",
            "tagCY",
            "_FILETIME",
            "tagCLIPDATA",
            "tagBSTRBLOB",
            "tagBLOB",
            "tagVersionedStream",
            "tagSAFEARRAY",
            "tagCAC",
            "tagCAUB",
            "tagCAI",
            "tagCAUI",
            "tagCAL",
            "tagCAUL",
            "tagCAH",
            "tagCAUH",
            "tagCAFLT",
            "tagCADBL",
            "tagCABOOL",
            "tagCASCODE",
            "tagCACY",
            "tagCADATE",
            "tagCAFILETIME",
            "tagCACLSID",
            "tagCACLIPDATA",
            "tagCABSTR",
            "tagCABSTRBLOB",
            "tagCALPSTR",
            "tagCALPWSTR",
            "tagCAPROPVARIANT",
            "tagDEC",
            "tagRECT",
            "se_translator",
            "_EXCEPTION_POINTERS",
            "_EXCEPTION_RECORD",
            "_CONTEXT",
            "unhandled_policy",
            "_tagADDRESS64",
            "_tagADDRESS",
            "_KDHELP64",
            "_KDHELP",
            "sym_engine",
            "_IMAGEHLP_LINE",
            "HINSTANCE__",
            "_tagSTACKFRAME",
            "unhandled_report",
            "basic_debugbuf",
            "exception2",
            "HWND__",
            "cfl_nil_mutex",
            "cfl_mt_mutex",
            "cfl_db",
            "cfl_rs",
            "friend_tbl",
            "friend_dat",
            "KCHAPc",
            "fifo_elem",
            "CPAI",
            "TDt",
            "tagSIZE",
            "HDC__",
            "HBITMAP__",
            "HFONT__",
            "tagPOINT",
            "tagPALETTEENTRY",
            "HMONITOR__",
            "tagDISPPARAMS",
            "tagVARIANT",
            "tagEXCEPINFO",
            "_RGNDATAHEADER",
            "tagSTATSTG",
            "tagTYPEATTR",
            "tagFUNCDESC",
            "tagVARDESC",
            "tagSAFEARRAYBOUND",
            "tagTYPEDESC",
            "tagIDLDESC",
            "tagTYPEATTR",
            "tagTLIBATTR",
            "tagARRAYDESC",
            "tagBINDPTR",
            "tagELEMDESC",
            "tagVARDESC",
            "__JUMP_BUFFER",
            "__lc_time_data",
            "__non_rtti_object",
            "__unnamed",
            "_BCD80",
            "_browseinfoA",
        };
        #endregion
        static bool FilterName(string name)
        {
            if (name.Contains("_RTL_CRITICAL_SECTION_DEBUG")) return false;
            if (name.Contains("IDirect3D8")) return false;
            if (name.Contains("IDirect3DDevice8")) return false;
            return true;
            if (name.StartsWith("std::")) return false;
            if (reserved.Contains<string>(name)) return false;
            foreach (string s in reservedbegin)
                if (name.StartsWith(s)) return false;
            return true;
        }

        static string stype;
        private static bool StructWithType(Structure s)
        {
            string type = stype.Replace("*", "").Replace(" ", "").Replace("&", "");
            if (type.Contains('['))
                type=type.Remove(type.IndexOf('['));
            if (s.name.Contains(type))
            {
                int comp = string.Compare(s.name, type);
                if (!s.name.StartsWith("std::")&&comp!=0)
                    comp = comp;
                return comp == 0;
            }
            return false;
        }
        static void Main(string[] args)
        {
            PrintSymbolWrapper psw = new PrintSymbolWrapper();

            DiaSource source = new DiaSource();
            source.loadDataFromPdb("MindPower3D_D8R.pdb");
            IDiaSession session;
            source.openSession(out session);

            IDiaEnumSymbols enumUTDs;
            IDiaEnumSymbols enumFunctions;
            IDiaEnumSymbols enumEnums;
            IDiaEnumSymbols enumTypedefs;
            IDiaEnumSymbols enumCompilands;
            session.globalScope.findChildren(SymTagEnum.SymTagUDT, null, 0, out enumUTDs);
            session.globalScope.findChildren(SymTagEnum.SymTagFunction, null, 0, out enumFunctions);
            session.globalScope.findChildren(SymTagEnum.SymTagEnum, null, 0, out enumEnums);
            session.globalScope.findChildren(SymTagEnum.SymTagTypedef, null, 0, out enumTypedefs);
            session.globalScope.findChildren(SymTagEnum.SymTagCompiland, null, 0, out enumCompilands);

            #region Load
            Console.WriteLine("Load");
            System.IO.StreamWriter stream = new System.IO.StreamWriter(System.IO.File.Open("dump.txt", System.IO.FileMode.Create), Encoding.ASCII);
            Compiland[] compilands = new Compiland[enumCompilands.count];
            int i = 0;
            foreach (IDiaSymbol comp in enumCompilands)
            {
                stream.WriteLine(comp.name);
                stream.WriteLine(comp.sourceFileName);

                string sourcename = comp.sourceFileName != null ? "e:"+comp.sourceFileName.ToLower() : null;
                string headername = sourcename == null ? null : sourcename.Replace(".cpp", ".h");
                compilands[i].source = sourcename;

                IDiaEnumSourceFiles files;
                session.findFile(comp, null, 0, out files);
                compilands[i].includes = new string[files.count];
                int j = 0;
                foreach (IDiaSourceFile f in files)
                {
                    if (headername != null&&f.fileName.Contains(headername)) compilands[i].have_header = true;
                    compilands[i].includes[j] = f.fileName;
                    j++;
                    stream.WriteLine("\t" + f.fileName);
                    
                    string filename = "c:\\project\\" + f.fileName[0] + f.fileName.Substring(2);
                    int id = filename.LastIndexOf('\\');
                    System.IO.Directory.CreateDirectory(filename.Substring(0, id));
                    System.IO.File.Create(filename).Close();
                }

                if (sourcename != null)
                {
                    System.IO.FileStream fs = System.IO.File.Create("c:\\project\\" + sourcename[0] + sourcename.Substring(2));
                    System.IO.StreamWriter sw = new System.IO.StreamWriter(fs);
                    foreach (string includefile in compilands[i].includes)
                    {
                        if (includefile.CompareTo(sourcename) != 0)
                        {
                            sw.WriteLine("#include <" + includefile.Substring(includefile.LastIndexOf("\\") + 1) + ">");
                        }
                    }
                    sw.Close();
                    fs.Close();
                }

                stream.WriteLine("");
                i++;
            }
            stream.WriteLine("");

            Structure[] structures = new Structure[enumUTDs.count];
            i = 0;
            foreach (IDiaSymbol sym in enumUTDs)
            {
                Classes.UTD utd = new Classes.UTD(sym);
                structures[i] = new Structure();
                PrintStructEx(utd, session, ref structures[i]);
                i++;
                if (i % 4 == 0) Console.Write('.');
                if (i % 200 == 0) Console.WriteLine(" :" + i + "\n");
            }

            i=0;
            foreach (IDiaSymbol sym in enumFunctions)
                if (sym.classParent == null)
                    i++;
            Function[] functions = new Function[i];
            enumFunctions.Reset();
            i = 0;
            foreach (IDiaSymbol sym in enumFunctions)
            {
                Blocks.Function funk = new Blocks.Function(sym);
                if (funk.classParent == null)
                {
                    functions[i] = new Function();
                    if (funk.undecoratedName != null)
                    {
                        functions[i].undname = funk.undecoratedName;
                    }
                    
                        functions[i].name = funk.name;
                        functions[i].access = PrintSymbolWrapper.rgAccess[funk.access];
                        functions[i].type = (funk.type != null ? funk.type.name : null);

                    functions[i].id = funk.symIndexId;
                    functions[i].filename = "";

                    uint addr = funk.relativeVirtualAddress;
                    IDiaEnumLineNumbers enumLines;
                    session.findLinesByRVA(addr, (uint)funk.length, out enumLines);
                    if (enumLines.count > 0)
                    {
                        foreach (IDiaLineNumber line in enumLines)
                        {
                            functions[i].filename += ":" + line.sourceFile.fileName + "[" + enumLines.count + "]";
                            break;
                        }
                    }
                    i++;
                }
            }

            Enum[] enums = new Enum[enumEnums.count];
            i = 0;
            foreach (IDiaSymbol sym in enumEnums)
            {
                enums[i] = new Enum();
                enums[i].name = sym.name;
                enums[i].id = sym.symIndexId;

                IDiaEnumSymbols childrens;
                sym.findChildren(SymTagEnum.SymTagNull, null, 0, out childrens);
                int j = 0;
                enums[i].values = new SubEnum[childrens.count];

                foreach (IDiaSymbol c in childrens)
                {
                    enums[i].values[j] = new SubEnum();
                    enums[i].values[j].value = c.value;
                    enums[i].values[j].name = c.name;
                    enums[i].values[j].id = c.symIndexId;
                    j++;
                }
                i++;
            }

            i = 0;
            foreach (IDiaSymbol sym in enumTypedefs)
                if (sym.classParent == null)
                    i++;
            Typedef[] typedefs = new Typedef[i];
            i = 0;
            enumTypedefs.Reset();
            foreach (IDiaSymbol sym in enumTypedefs)
                if (sym.classParent == null)
                {
                    typedefs[i] = new Typedef();
                    typedefs[i].id = sym.symIndexId;
                    typedefs[i].name = sym.name;
                    typedefs[i].type = sym.type.name;
                    i++;
                }

            stream.Close();
            #endregion

            Console.Write("\nSorting...");
            Array.Sort(structures, new StructSortClass());
            Console.Write("OK\n");

            #region First processing - process templates
            i = 0;
            Console.WriteLine("FP");
            foreach (Structure s in structures)
            {
                s.new_id = (uint)i;
                if (s.name.Contains('<'))
                {
                    int start = 0;
                    int end = 0;
                    int depth = 0;
                    int arg = 1;
                    for (int k = 0; k < s.name.Length; k++)
                    {
                        if (s.name[k] == '<')
                        {
                            if (depth == 0)
                                start = k;
                            depth++;
                        }
                        else if (s.name[k] == '>')
                        {
                            depth--;
                            if (depth == 0)
                                end = k;
                        }
                        else if (s.name[k] == ',' && depth == 1)
                            arg++;
                    }
                    string sourcepattern = s.name.Substring(start, end - start + 1);
                    string pattern = "<";
                    if (arg > 1)
                    {
                        for (int l = 1; l < arg; l++)
                            pattern += "T" + l.ToString() + ",";
                        pattern += "T" + arg.ToString() + ">";
                    }
                    else pattern += "T>";
                    s.name = s.name.Replace(sourcepattern, pattern);

                    if (s.members != null)
                        foreach (Member m in s.members)
                            m.name = m.name.Replace(sourcepattern, pattern);

                    if (s.functions != null)
                        foreach (Function f in s.functions)
                            f.name = f.name.Replace(sourcepattern, pattern);
                }

                s.filenames = new List<string>();
                if (s.functions != null)
                    foreach (Function f in s.functions)
                        if (f.filename.Length > 0)
                        {
                            string filename = f.filename.Substring(1, f.filename.IndexOf('[') - 1);
                            if (!s.filenames.Contains(filename)) s.filenames.Add(filename);
                        }

                s.usedin_new_ids = new List<int>();
                i++;
            }
            #endregion

            #region Second processing - process substructure
            Console.WriteLine("SP");
            i = 0;
            string paststruct = "";
            foreach (Structure s in structures)
            {
                s.substructures_new_ids = new List<int>();

                if (FilterName(s.name)&&paststruct!=s.name)
                {
                    paststruct=s.name;
                    if (s.members != null)
                        foreach (Member m in s.members)
                        {
                            stype = m.type;
                            int ind = Array.FindIndex<Structure>(structures, StructWithType);
                            if (ind >= 0 && (uint)ind != s.new_id)
                            {
                                if (!s.substructures_new_ids.Contains(ind))
                                    s.substructures_new_ids.Add(ind);
                                if (!structures[ind].usedin_new_ids.Contains((int)s.new_id))
                                    structures[ind].usedin_new_ids.Add((int)s.new_id);
                            }
                        }

                    if (s.baseclass != null)
                    {
                        foreach (BaseClass b in s.baseclass)
                        {
                            stype = b.type;
                            int ind = Array.FindIndex<Structure>(structures, StructWithType);
                            if (ind >= 0)
                            {
                                b.new_id = (uint)ind;
                                if (!s.substructures_new_ids.Contains(ind))
                                    s.substructures_new_ids.Add(ind);
                                if (!structures[ind].usedin_new_ids.Contains((int)s.new_id))
                                    structures[ind].usedin_new_ids.Add((int)s.new_id);
                            }
                        }
                    }

                    if (s.functions != null)
                        foreach (Function f in s.functions)
                        {
                            int start = f.type.IndexOf('(');
                            int end = f.type.IndexOf(')');
                            if (start >= 0)
                            {
                                string[] arg = f.type.Substring(start, end - start + 1).Split(new char[] { '(', ')', ',' });
                                foreach (string a in arg)
                                {
                                    if (a.Length > 0)
                                    {
                                        stype = a.Replace("const", "").Replace("*", "").Replace("&", "");
                                        int ind = Array.FindIndex<Structure>(structures, StructWithType);
                                        if (ind >= 0)
                                        {
                                            if (!s.substructures_new_ids.Contains(ind))
                                                s.substructures_new_ids.Add(ind);
                                            if (!structures[ind].usedin_new_ids.Contains((int)s.new_id))
                                                structures[ind].usedin_new_ids.Add((int)s.new_id);
                                        }
                                    }
                                }
                            }
                        }


                    i++;
                    Console.Write('.');
                    if (i % 50 == 0) Console.WriteLine(" :" + i + "\n");
                }
            }
            #endregion

            #region Third processing - Set files
            Console.WriteLine("\nTP");
            i = 0;
            paststruct = "";
            List<FileWithStructures> fileswithstructures = new List<FileWithStructures>();
            foreach (Structure s in structures)
            {
                if (FilterName(s.name) && s.filenames.Count > 0 && paststruct != s.name)
                {
                    paststruct = s.name;

                    string filename = "";
                    foreach (string fn in s.filenames)
                        if (fn.Contains(".h")) filename = fn;

                    if (filename.Length > 0)
                    {
                        FileWithStructures currentfws = null;
                        foreach (FileWithStructures fws in fileswithstructures)
                            if (fws.name == filename)
                                currentfws = fws;
                        if (currentfws == null)
                        {
                            currentfws = new FileWithStructures(filename);
                            fileswithstructures.Add(currentfws);
                        }

                        if (!currentfws.structures.Contains((int)s.new_id))
                            currentfws.structures.Add((int)s.new_id);
                        FindAllSubstruct(s, structures, ref currentfws.structures);

                        List<int> removelist = new List<int>();
                        foreach (int structure in currentfws.structures)
                        { 
                            Structure s2 = structures[structure];

                            string filename2 = "";
                            foreach (string fn in s2.filenames)
                                if (fn.Contains(".h")) filename2 = fn;
                            if (filename2.Length > 0&&filename2!=currentfws.name)
                            {
                                if (!currentfws.includes.Contains(filename2))
                                    currentfws.includes.Add(filename2);
                                removelist.Add(structure);
                            }
                        }
                        foreach (int rem in removelist)
                            currentfws.structures.Remove(rem);
                    }
                }

                i++;
                Console.Write('.');
                if (i % 50 == 0) Console.WriteLine(" :" + i + "\n");
            }
            #endregion

            #region Forth processing - Set headers
            Console.WriteLine("\n4P");
            i = 0;
            paststruct = "";
            foreach (FileWithStructures fws in fileswithstructures)
            {
                int[] structureids = fws.structures.ToArray();
                int[] structuredepth = new int[structureids.Length];
                SetDepthForStructs(structures, structureids, ref structuredepth);
                for (int q = 0; q < structuredepth.Length; q++)
                {
                    int max = structuredepth[q];
                    int max_id=q;
                    for (int j = q + 1; j < structuredepth.Length; j++)
                    {
                        if (structuredepth[j] > max)
                        {
                            max = structuredepth[j];
                            max_id = j;
                        }
                    }

                    if (max_id != q)
                    {
                        int w = structuredepth[max_id];
                        structuredepth[max_id] = structuredepth[q];
                        structuredepth[q] = w;

                        w = structureids[max_id];
                        structureids[max_id] = structureids[q];
                        structureids[q] = w;
                    }
                }

                System.IO.FileStream fs = System.IO.File.Open("c:\\project\\" + fws.name[0] + fws.name.Substring(2), System.IO.FileMode.Append);
                System.IO.StreamWriter fstream = new System.IO.StreamWriter(fs);

                string definename = "__" + fws.name.Substring(fws.name.LastIndexOf('\\') + 1).ToUpper().Replace('.', '_') + "__";
                fstream.WriteLine("#ifndef " + definename);
                fstream.WriteLine("#define " + definename);
                
                if(fws.includes.Count>0)
                    fstream.WriteLine("");
                foreach (string str in fws.includes)
                {
                    fstream.WriteLine("#include <" + str.Substring(str.LastIndexOf('\\') + 1) + ">");
                }
                fstream.WriteLine("");

                foreach (int ss in structureids)
                {
                    fstream.WriteLine("");
                    WriteStruct(structures[ss], fstream);
                }

                fstream.WriteLine("#endif");

                fstream.Close();
                fs.Close();

                i++;
                Console.Write('.');
                if (i % 50 == 0) Console.WriteLine(" :" + i + "\n");
            }
            #endregion

            #region Print
            System.IO.StreamWriter file = new System.IO.StreamWriter(System.IO.File.Open("sdump.txt", System.IO.FileMode.Create), Encoding.ASCII);
            #region Structs
            file.WriteLine("//----------------Structures");
            paststruct = "";
            foreach (Structure s in structures)
            {
                if (FilterName(s.name)&&paststruct!=s.name)
                {
                    paststruct = s.name;
                    foreach (string sourcefilename in s.filenames)
                        file.Write("//  f:" + sourcefilename + "\n");
                    foreach (int subid in s.substructures_new_ids)
                        file.Write("// ss:" + structures[subid].name + "\n");
                    foreach (int subid in s.usedin_new_ids)
                        file.Write("//  u:" + structures[subid].name + "\n");

                    file.Write("struct /*id:" + s.new_id + "*/ " + s.name);
                    if (s.baseclass != null)
                    {
                        int k = 0;
                        foreach (BaseClass b in s.baseclass)
                        {
                            file.Write((k == 0 ? ":" : ",") + "/*0x" + b.offcet.ToString("0") + " id:" + b.id + "*/ " + b.type);
                            k++;
                        }
                    }
                    file.Write("\n{\n");

                    if (s.members != null)
                    {
                        foreach (Member m in s.members)
                        {
                            file.Write("\t/*off 0x" + m.offcet.ToString("00000000") + " size:" + m.length.ToString("0000") + " id:" + m.id + "*/ " + m.access + ": " + m.type + " " + m.name + ";\n");
                        }
                        file.Write("\n");
                    }

                    if (s.enums != null)
                    {
                        foreach (Enum e in s.enums)
                        {
                            file.Write("\tenum /*id:" + e.id + "*/" + e.name + ";\n");
                        }
                        file.Write("\n");
                    }

                    if (s.functions != null)
                    {
                        foreach (Function f in s.functions)
                        {
                            if (f.filename.Length > 2)
                                file.Write("\t//" + f.filename + "\n");
                            if (f.type == null)
                                file.Write("\t/*id:" + f.id + " */" + f.name + ";\n");
                            else
                            {
                                int start = f.type.IndexOf('(');
                                int end = f.type.IndexOf(')');
                                string type = start < 0 ? "" : f.type.Substring(0, start);
                                string arg = start < 0 ? "" : f.type.Substring(start, end - start + 1);
                                file.Write("\t/*id:" + f.id + "*/ " + f.access + ": " + type + " " + f.name + " " + arg + ";\n");
                            }
                        }
                        file.Write("\n");
                    }

                    if (s.substructures != null)
                    {
                        foreach (SubStructure sub in s.substructures)
                        {
                            file.Write("\tstructure /*id:" + sub.id + "*/ " + sub.name + ";\n");
                        }
                        file.Write("\n");
                    }

                    if (s.typedefs != null)
                    {
                        foreach (Typedef t in s.typedefs)
                        {
                            file.Write("\ttypedef /*id:" + t.id + "*/" + t.type + " " + t.name + ";\n");
                        }
                        file.Write("\n");
                    }

                    if (s.vtables != null)
                    {
                        foreach (VTable v in s.vtables)
                        {
                            file.Write("\t//vtable x" + v.count + "\n");
                        }
                        file.Write("\n");
                    }

                    file.Write("};\n\n");
                    file.Write("\n\n");
                }
            }
            #endregion

            #region Free funks
            file.WriteLine("//----------------Functions without classes");
            foreach (Function f in functions)
            {
                file.Write("\t//" + f.filename + "\n");
                if (f.type == null)
                    file.Write(f.name + ";\n");
                else
                {
                    int start = f.type.IndexOf('(');
                    int end = f.type.IndexOf(')');
                    string type = f.type.Substring(0, start);
                    string arg = f.type.Substring(start, end - start + 1);
                    file.Write(f.access + ": " + type + " " + f.name + " " + arg + ";\n");
                }
            }
            #endregion

            #region Enums
            file.WriteLine("//----------------Enums");
            foreach (Enum e in enums)
            {
                file.Write("enum /*id:" + e.id + "*/" + e.name + "\n{\n");
                int j=0;
                foreach (SubEnum s in e.values)
                {
                    file.Write("\t/*id:" + s.id + "*/ " + s.name + " - " + s.value + (j < e.values.Length - 1 ? ",\n" : "\n"));
                    j++;
                }
                file.Write("}\n");
            }
            #endregion

            #region Typedefs
            file.WriteLine("//----------------Typedefs");
            foreach (Typedef t in typedefs)
            {
                file.Write("typedef /*id:" + t.id + "*/ " + t.type + " " + t.name + ";\n");
            }
            #endregion
            file.Close();
            #endregion
        }
    }
}
