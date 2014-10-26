using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Dia2Lib;

namespace pbdcstest
{
    enum CV_access_e
    {
        CV_private = 1,
        CV_protected = 2,
        CV_public = 3
    }

    enum BasicType
    {
        btNoType = 0,
        btvoid = 1,
        btChar = 2,
        btWChar = 3,
        btInt = 6,
        btUInt = 7,
        btFloat = 8,
        btBCD = 9,
        btBool = 10,
        btLong = 13,
        btULong = 14,
        btCurrency = 25,
        btDate = 26,
        btVariant = 27,
        btComplex = 28,
        btBit = 29,
        btBSTR = 30,
        btHresult = 31
    }

    class PrintSymbolWrapper
    {
        #region String constants
        #region Basic types
        static public string[] rgBaseType = new string[]
        {
          "<NoType>",                         // btNoType = 0,
          "void",                             // btpublic void = 1,
          "char",                             // btChar = 2,
          "wchar_t",                          // btWChar = 3,
          "signed char",
          "unsigned char",
          "int",                              // btInt = 6,
          "unsigned int",                     // btUInt = 7,
          "float",                            // btFloat = 8,
          "<BCD>",                            // btBCD = 9,
          "bool",                             // btbool = 10,
          "short",
          "unsigned short",
          "long",                             // btLong = 13,
          "unsigned long",                    // btulong = 14,
          "__int8",
          "__int16",
          "__int32",
          "__int64",
          "__int128",
          "unsigned __int8",
          "unsigned __int16",
          "unsigned __int32",
          "unsigned __int64",
          "unsigned __int128",
          "<currency>",                       // btCurrency = 25,
          "<date>",                           // btDate = 26,
          "VARIANT",                          // btVariant = 27,
          "<complex>",                        // btComplex = 28,
          "<bit>",                            // btBit = 29,
          "string",                             // btstring = 30,
          "HRESULT"                           // btHresult = 31
        };
        #endregion
        
        #region Tags returned by Dia
        string[] rgTags = new string[]
        {
          "(SymTagnull)",                     // SymTagnull
          "Executable (Global)",              // SymTagExe
          "Compiland",                        // SymTagCompiland
          "CompilandDetails",                 // SymTagCompilandDetails
          "CompilandEnv",                     // SymTagCompilandEnv
          "Function",                         // SymTagFunction
          "Block",                            // SymTagBlock
          "Data",                             // SymTagData
          "Annotation",                       // SymTagAnnotation
          "Label",                            // SymTagLabel
          "PublicSymbol",                     // SymTagPublicSymbol
          "UserDefinedType",                  // SymTagUDT
          "Enum",                             // SymTagEnum
          "FunctionType",                     // SymTagFunctionType
          "PointerType",                      // SymTagPointerType
          "ArrayType",                        // SymTagArrayType
          "BaseType",                         // SymTagBaseType
          "Typedef",                          // SymTagTypedef
          "BaseClass",                        // SymTagBaseClass
          "Friend",                           // SymTagFriend
          "FunctionArgType",                  // SymTagFunctionArgType
          "FuncDebugStart",                   // SymTagFuncDebugStart
          "FuncDebugEnd",                     // SymTagFuncDebugEnd
          "UsingNamespace",                   // SymTagUsingNamespace
          "VTableShape",                      // SymTagVTableShape
          "VTable",                           // SymTagVTable
          "Custom",                           // SymTagCustom
          "Thunk",                            // SymTagThunk
          "CustomType",                       // SymTagCustomType
          "ManagedType",                      // SymTagManagedType
          "Dimension",                        // SymTagDimension
          "CallSite",                         // SymTagCallSite
        };
        #endregion
        
        #region Processors
        string[] rgFloatPackageStrings = new string[]
        {
          "hardware processor (80x87 for Intel processors)",    // CV_CFL_NDP
          "emulator",                                           // CV_CFL_EMU
          "altmath",                                            // CV_CFL_ALT
          "???"
        };
        #endregion
        
        #region ProcessorsNames
        string[] rgProcessorStrings = new string[]
        {
          "8080",                             //  CV_CFL_8080
          "8086",                             //  CV_CFL_8086
          "80286",                            //  CV_CFL_80286
          "80386",                            //  CV_CFL_80386
          "80486",                            //  CV_CFL_80486
          "Pentium",                          //  CV_CFL_PENTIUM
          "Pentium Pro/Pentium II",           //  CV_CFL_PENTIUMII/CV_CFL_PENTIUMPRO
          "Pentium III",                      //  CV_CFL_PENTIUMIII
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "MIPS (Generic)",                   //  CV_CFL_MIPSR4000
          "MIPS16",                           //  CV_CFL_MIPS16
          "MIPS32",                           //  CV_CFL_MIPS32
          "MIPS64",                           //  CV_CFL_MIPS64
          "MIPS I",                           //  CV_CFL_MIPSI
          "MIPS II",                          //  CV_CFL_MIPSII
          "MIPS III",                         //  CV_CFL_MIPSIII
          "MIPS IV",                          //  CV_CFL_MIPSIV
          "MIPS V",                           //  CV_CFL_MIPSV
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "M68000",                           //  CV_CFL_M68000
          "M68010",                           //  CV_CFL_M68010
          "M68020",                           //  CV_CFL_M68020
          "M68030",                           //  CV_CFL_M68030
          "M68040",                           //  CV_CFL_M68040
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "Alpha 21064",                      // CV_CFL_ALPHA, CV_CFL_ALPHA_21064
          "Alpha 21164",                      // CV_CFL_ALPHA_21164
          "Alpha 21164A",                     // CV_CFL_ALPHA_21164A
          "Alpha 21264",                      // CV_CFL_ALPHA_21264
          "Alpha 21364",                      // CV_CFL_ALPHA_21364
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "PPC 601",                          // CV_CFL_PPC601
          "PPC 603",                          // CV_CFL_PPC603
          "PPC 604",                          // CV_CFL_PPC604
          "PPC 620",                          // CV_CFL_PPC620
          "PPC w/FP",                         // CV_CFL_PPCFP
          "PPC (Big Endian)",                 // CV_CFL_PPCBE
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "SH3",                              // CV_CFL_SH3
          "SH3E",                             // CV_CFL_SH3E
          "SH3DSP",                           // CV_CFL_SH3DSP
          "SH4",                              // CV_CFL_SH4
          "SHmedia",                          // CV_CFL_SHMEDIA
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "ARM3",                             // CV_CFL_ARM3
          "ARM4",                             // CV_CFL_ARM4
          "ARM4T",                            // CV_CFL_ARM4T
          "ARM5",                             // CV_CFL_ARM5
          "ARM5T",                            // CV_CFL_ARM5T
          "ARM6",                             // CV_CFL_ARM6
          "ARM (XMAC)",                       // CV_CFL_ARM_XMAC
          "ARM (WMMX)",                       // CV_CFL_ARM_WMMX
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "Omni",                             // CV_CFL_OMNI
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "Itanium",                          // CV_CFL_IA64, CV_CFL_IA64_1
          "Itanium (McKinley)",               // CV_CFL_IA64_2
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "CEE",                              // CV_CFL_CEE
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "AM33",                             // CV_CFL_AM33
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "M32R",                             // CV_CFL_M32R
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "TriCore",                          // CV_CFL_TRICORE
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "x64",                              // CV_CFL_X64
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "EBC",                              // CV_CFL_EBC
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "???",
          "Thumb",                            // CV_CFL_THUMB
        };
        #endregion
        
        string[] rgDataKind =new string[]
        {
          "Unknown",
          "Local",
          "Static Local",
          "Param",
          "Object Ptr",
          "File Static",
          "Global",
          "Member",
          "Static Member",
          "Constant",
        };
        
        string[] rgUdtKind =new string[]
        {
          "struct",
          "class",
          "union",
          "enum",
        };
        
        static public string[] rgAccess =new string[]
        {
          "",                     // No access specifier
          "private",
          "protected",
          "public"
        };
        
        public static string[] rgCallingConvention =new string[]
        {
          "__cdecl",
          "CV_CALL_FAR_C",
          "CV_CALL_NEAR_PASCAL",
          "CV_CALL_FAR_PASCAL",
          "CV_CALL_NEAR_FAST",
          "CV_CALL_FAR_FAST",
          "CV_CALL_SKIPPED",
          "__stdcall",
          "CV_CALL_FAR_STD",
          "CV_CALL_NEAR_SYS",
          "CV_CALL_FAR_SYS",
          "__thiscall",
          "CV_CALL_MIPSCALL",
          "CV_CALL_GENERIC",
          "CV_CALL_ALPHACALL",
          "CV_CALL_PPCCALL",
          "CV_CALL_SHCALL",
          "CV_CALL_ARMCALL",
          "CV_CALL_AM33CALL",
          "CV_CALL_TRICALL",
          "CV_CALL_SH5CALL",
          "CV_CALL_M32RCALL",
          "CV_CALL_RESERVED"
        };
        
        string[] rgLanguage =new string[]
        {
          "C",                                // CV_CFL_C
          "C++",                              // CV_CFL_CXX
          "FORTRAN",                          // CV_CFL_FORTRAN
          "MASM",                             // CV_CFL_MASM
          "Pascal",                           // CV_CFL_PASCAL
          "Basic",                            // CV_CFL_BASIC
          "COBOL",                            // CV_CFL_COBOL
          "LINK",                             // CV_CFL_LINK
          "CVTRES",                           // CV_CFL_CVTRES
          "CVTPGD",                           // CV_CFL_CVTPGD
          "C#",                               // CV_CFL_CSHARP
          "Visual Basic",                     // CV_CFL_VB
          "ILASM",                            // CV_CFL_ILASM
          "Java",                             // CV_CFL_JAVA
          "JScript",                          // CV_CFL_JSCRIPT
          "MSIL",                             // CV_CFL_MSIL
        };
        
        string[] rgLocationTypeString = new string[]
        {
          "null",
          "static",
          "TLS",
          "RegRel",
          "ThisRel",
          "Enregistered",
          "BitField",
          "Slot",
          "IL Relative",
          "In MetaData",
          "Constant"
        };
        #endregion        
        #region LocaEnum
        enum LocationType
        {
            LocIsNull,
            LocIsStatic,
            LocIsTLS,
            LocIsRegRel,
            LocIsThisRel,
            LocIsEnregistered,
            LocIsBitField,
            LocIsSlot,
            LocIsIlRel,
            LocInMetaData,
            LocIsConstant,
            LocTypeMax
        }
        #endregion
        #region checksum
        enum CV_SourceChksum_t
        {
            CHKSUM_TYPE_NONE = 0,        // indicates no checksum is available
            CHKSUM_TYPE_MD5,
            CHKSUM_TYPE_SHA1
        };
#endregion

        const int MAX_TYPE_IN_DETAIL = 5;

        string SafeDRef(string[] d, uint i)
        {
            if (d != null && d.Length > i)
                return d[i];
            return "...";
        }
        ushort SzNameC7Reg(ushort i)
        {
            return i;
        }

        // Print a public symbol info: name, VA, RVA, SEG:OFF
        public void PrintPublicSymbol(IDiaSymbol pSymbol)
        {
            uint dwSymTag;
            uint dwRVA;
            uint dwSeg;
            uint dwOff;
            string stringName;

            if ((dwSymTag = pSymbol.symTag) == null)
            {
                return;
            }

            if ((dwRVA = pSymbol.relativeVirtualAddress) == null)
            {
                dwRVA = 0xFFFFFFFF;
            }

            dwSeg = pSymbol.addressSection;
            dwOff = pSymbol.addressOffset;

            Console.Write(String.Format("{0}: [{1}][{2}:{3}] ", rgTags[dwSymTag], dwRVA, dwSeg, dwOff));
            //fwprintf(pFileout,L"%s: [%08X][%04X:%08X] ", rgTags[dwSymTag], dwRVA, dwSeg, dwOff);

            if (dwSymTag == (int)SymTagEnum.SymTagThunk)
            {
                if ((stringName = pSymbol.name) != null)
                {
                    Console.Write(String.Format("{0}\n", stringName));
                    //fwprintf(pFileout,L"%s\n", stringName);
                }

                else
                {
                    if (pSymbol.targetRelativeVirtualAddress == null)
                    {
                        dwRVA = 0xFFFFFFFF;
                    }

                    dwSeg = pSymbol.targetSection;
                    dwOff = pSymbol.targetOffset;

                    Console.Write(String.Format("target -> [{0}][{1}:{2}]\n", dwRVA, dwSeg, dwOff));
                    //fwprintf(pFileout,L"target -> [%08X][%04X:%08X]\n", dwRVA, dwSeg, dwOff);
                }
            }

            else
            {
                // must be a function or a data symbol

                string stringUndname;

                if ((stringName = pSymbol.name) != null)
                {
                    if ((stringUndname = pSymbol.undecoratedName) != null)
                    {
                        Console.Write(String.Format("{0}({1})\n", stringName, stringUndname));
                        //fwprintf(pFileout,L"%s(%s)\n", stringName, stringUndname);
                        //fwprintf(pFileout,L"%s\n", stringUndname);
                    }

                    else
                    {
                        Console.Write(String.Format("{0}\n", stringName));
                        //fwprintf(pFileout,L"%s\n", stringName);
                    }
                }
            }
        }
        
        // Print a global symbol info: name, VA, RVA, SEG:OFF
        public void PrintGlobalSymbol(IDiaSymbol pSymbol)
        {
            uint dwSymTag;
            uint dwRVA;
            uint dwSeg;
            uint dwOff;

            if ((dwSymTag = pSymbol.symTag) == null)
            {
                return;
            }

            if ((dwRVA = pSymbol.relativeVirtualAddress) == null)
            {
                dwRVA = 0xFFFFFFFF;
            }

            dwSeg = pSymbol.addressSection;
            dwOff = pSymbol.addressOffset;

            Console.Write(String.Format("{0}: [{1}][{2}:{3}] ", rgTags[dwSymTag], dwRVA, dwSeg, dwOff));
            //fwprintf(pFileout,L"%s: [%08X][%04X:%08X] ", rgTags[dwSymTag], dwRVA, dwSeg, dwOff);

            if (dwSymTag == (int)SymTagEnum.SymTagThunk)
            {
                string stringName;

                if ((stringName = pSymbol.name) != null)
                {
                    Console.Write(String.Format("{0}\n", stringName));
                    //fwprintf(pFileout,L"%s\n", stringName);
                }

                else
                {
                    if ((dwRVA = pSymbol.targetRelativeVirtualAddress) == null)
                    {
                        dwRVA = 0xFFFFFFFF;
                    }

                    dwSeg = pSymbol.targetSection;
                    dwOff = pSymbol.targetOffset;
                    Console.Write(String.Format("target -> [{0}][{1}:{2}]\n", dwRVA, dwSeg, dwOff));
                    //fwprintf(pFileout,L"target -> [%08X][%04X:%08X]\n", dwRVA, dwSeg, dwOff);
                }
            }

            else
            {
                string stringName;
                string stringUndname;

                if ((stringName = pSymbol.name) != null)
                {
                    if ((stringUndname = pSymbol.undecoratedName) != null)
                    {
                        Console.Write(String.Format("{0}({1})\n", stringName, stringUndname));
                        //fwprintf(pFileout,L"%s(%s)\n", stringName, stringUndname);
                    }

                    else
                    {
                        Console.Write(String.Format("{0}\n", stringName));
                    }
                }
            }
        }
        
        // Print a callsite symbol info: SEG:OFF, RVA, type
        public void PrintCallSiteInfo(IDiaSymbol pSymbol)
        {
            uint dwISect, dwOffset;
            if ((dwISect = pSymbol.addressSection) != null &&
              (dwOffset = pSymbol.addressOffset) != null)
            {
                Console.Write(String.Format("[{0}:{1}] ", dwISect, dwOffset));
                //fwprintf(pFileout,L"[0x%04x:0x%08x]  ", dwISect, dwOffset);
            }

            uint rva;
            if ((rva = pSymbol.relativeVirtualAddress) != null)
            {
                Console.Write(String.Format("{0}  ", rva));
                //fwprintf(pFileout,L"0x%08X  ", rva);
            }

            IDiaSymbol pFuncType;
            if ((pFuncType = pSymbol.type) != null)
            {
                uint tag;
                if ((tag = pFuncType.symTag) != null)
                {
                    switch (tag)
                    {
                        case (int)SymTagEnum.SymTagFunctionType:
                            PrintFunctionType(pSymbol);
                            break;
                        case (int)SymTagEnum.SymTagPointerType:
                            PrintFunctionType(pFuncType);
                            break;
                        default:
                            Console.WriteLine("???");
                            //fwprintf(pFileout,L"???\n");
                            break;
                    }
                }
            }
        }
        
        // Print a symbol info: name, type etc.
        public void PrintSymbol(IDiaSymbol pSymbol, uint dwIndent)
        {
            IDiaSymbol pType;
            uint dwSymTag;
            ulong ulLen;

            if ((dwSymTag = pSymbol.symTag) == null)
            {
                Console.WriteLine("ERROR - PrintSymbol get_symTag() failed");
                //fwprintf(pFileout,L"ERROR - PrintSymbol get_symTag() failed\n");
                return;
            }

            if (dwSymTag == (int)SymTagEnum.SymTagFunction)
            {
                Console.WriteLine();
                //fputwc(L'\n',pFileout);
            }

            PrintSymTag(dwSymTag);

            for (uint i = 0; i < dwIndent; i++)
            {
                Console.Write(' ');
                //fputwc(L' ',pFileout);
            }

            switch (dwSymTag)
            {
                case (int)SymTagEnum.SymTagCompilandDetails:
                    PrintCompilandDetails(pSymbol);
                    break;

                case (int)SymTagEnum.SymTagCompilandEnv:
                    PrintCompilandEnv(pSymbol);
                    break;

                case (int)SymTagEnum.SymTagData:
                    PrintData(pSymbol, dwIndent + 2);
                    break;

                case (int)SymTagEnum.SymTagFunction:
                case (int)SymTagEnum.SymTagBlock:
                    PrintLocation(pSymbol);

                    if ((ulLen = pSymbol.length) != null)
                    {
                        Console.Write(String.Format(", len = {0}, ", ulLen));
                        //fwprintf(pFileout,L", len = %08X, ", ulLen);
                    }

                    if (dwSymTag == (int)SymTagEnum.SymTagFunction)
                    {
                        uint dwCall;

                        if ((dwCall = pSymbol.callingConvention) != null)
                        {
                            Console.Write(String.Format(", {0}", SafeDRef(rgCallingConvention, dwCall)));
                            //fwprintf(pFileout,L", %s", SafeDRef(rgCallingConvention, dwCall));
                        }
                    }

                    PrintUndName(pSymbol);
                    Console.WriteLine("");
                    //fputwc(L'\n',pFileout);

                    if (dwSymTag == (int)SymTagEnum.SymTagFunction)
                    {
                        int f;

                        for (uint i = 0; i < dwIndent; i++)
                        {
                            Console.Write(' ');
                            //fputwc(L' ',pFileout);
                        }
                        Console.Write("                 Function attribute:");
                        //fwprintf(pFileout,L"                 Function attribute:");

                        if (((f = pSymbol.isCxxReturnUdt) != null) && f != 0)
                        {
                            Console.Write(" return user defined type (C++ style)");
                            //fwprintf(pFileout,L" return user defined type (C++ style)");
                        }
                        if (((f = pSymbol.constructor) != null) && f != 0)
                        {
                            Console.Write(" instance constructor");
                            //fwprintf(pFileout,L" instance constructor");
                        }
                        if (((f = pSymbol.isConstructorVirtualBase) != null) && f != 0)
                        {
                            Console.Write(" instance constructor of a class with virtual base");
                            //fwprintf(pFileout,L" instance constructor of a class with virtual base");
                        }
                        Console.WriteLine("");
                        //fputwc(L'\n',pFileout);

                        for (uint i = 0; i < dwIndent; i++)
                        {
                            Console.Write(' ');
                            //fputwc(L' ',pFileout);
                        }
                        Console.Write("                 Function info:");
                        //fwprintf(pFileout,L"                 Function info:");

                        if (((f = pSymbol.hasAlloca) != null) && f != 0)
                        {
                            Console.Write(" alloca");
                            //fwprintf(pFileout,L" alloca");
                        }

                        if (((f = pSymbol.hasSetJump) != null) && f != 0)
                        {
                            Console.Write(" setjmp");
                            //fwprintf(pFileout,L" setjmp");
                        }

                        if (((f = pSymbol.hasLongJump) != null) && f != 0)
                        {
                            Console.Write(" longjmp");
                            //fwprintf(pFileout,L" longjmp");
                        }

                        if (((f = pSymbol.hasInlAsm) != null) && f != 0)
                        {
                            Console.Write(" inlasm");
                            //fwprintf(pFileout,L" inlasm");
                        }

                        if (((f = pSymbol.hasEH) != null) && f != 0)
                        {
                            Console.Write(" eh");
                            //fwprintf(pFileout,L" eh");
                        }

                        if (((f = pSymbol.inlSpec) != null) && f != 0)
                        {
                            Console.Write(" inl_specified");
                            //fwprintf(pFileout,L" inl_specified");
                        }

                        if (((f = pSymbol.hasSEH) != null) && f != 0)
                        {
                            Console.Write(" seh");
                            //fwprintf(pFileout,L" seh");
                        }

                        if (((f = pSymbol.isNaked) != null) && f != 0)
                        {
                            Console.Write(" naked");
                            //fwprintf(pFileout,L" naked");
                        }

                        if (((f = pSymbol.hasSecurityChecks) != null) && f != 0)
                        {
                            Console.Write(" gschecks");
                            //fwprintf(pFileout,L" gschecks");
                        }

                        if (((f = pSymbol.isSafeBuffers) != null) && f != 0)
                        {
                            Console.Write(" safebuffers");
                            //fwprintf(pFileout,L" safebuffers");
                        }

                        if (((f = pSymbol.hasEHa) != null) && f != 0)
                        {
                            Console.Write(" asyncheh");
                            //fwprintf(pFileout,L" asyncheh");
                        }

                        if (((f = pSymbol.noStackOrdering) != null) && f != 0)
                        {
                            Console.Write(" gsnostackordering");
                            //fwprintf(pFileout,L" gsnostackordering");
                        }

                        if (((f = pSymbol.wasInlined) != null) && f != 0)
                        {
                            Console.Write(" wasinlined");
                            //fwprintf(pFileout,L" wasinlined");
                        }

                        if (((f = pSymbol.strictGSCheck) != null) && f != 0)
                        {
                            Console.Write(" strict_gs_check");
                            //fwprintf(pFileout,L" strict_gs_check");
                        }

                        Console.WriteLine("");
                        //fputwc(L'\n',pFileout);
                    }

                    IDiaEnumSymbols pEnumChildren;
                    pSymbol.findChildren(SymTagEnum.SymTagNull, null, 0, out pEnumChildren);

                    if (pEnumChildren != null)
                    {
                        IDiaSymbol pChild;
                        uint celt = 0;

                        while (true)
                        {
                            pEnumChildren.Next(1, out pChild, out celt);
                            if (!(pChild != null && celt == 1))
                            {
                                PrintSymbol(pChild, dwIndent + 2);
                            }
                            else break;
                        }
                    }
                    return;

                case (int)SymTagEnum.SymTagAnnotation:
                    PrintLocation(pSymbol);
                    Console.WriteLine("");
                    //fputwc(L'\n',pFileout);
                    break;

                case (int)SymTagEnum.SymTagLabel:
                    PrintLocation(pSymbol);
                    Console.Write(", ");
                    //fwprintf(pFileout,L", ");
                    PrintName(pSymbol);
                    break;

                case (int)SymTagEnum.SymTagEnum:
                case (int)SymTagEnum.SymTagTypedef:
                case (int)SymTagEnum.SymTagUDT:
                case (int)SymTagEnum.SymTagBaseClass:
                    PrintUDT(pSymbol);
                    break;

                case (int)SymTagEnum.SymTagFuncDebugStart:
                case (int)SymTagEnum.SymTagFuncDebugEnd:
                    PrintLocation(pSymbol);
                    break;

                case (int)SymTagEnum.SymTagFunctionArgType:
                case (int)SymTagEnum.SymTagFunctionType:
                case (int)SymTagEnum.SymTagPointerType:
                case (int)SymTagEnum.SymTagArrayType:
                case (int)SymTagEnum.SymTagBaseType:
                    IDiaSymbol pType2;
                    if ((pType2 = pSymbol.type) != null)
                    {
                        PrintType(pType2);
                    }

                    Console.WriteLine("");
                    //fputwc(L'\n',pFileout);
                    break;

                case (int)SymTagEnum.SymTagThunk:
                    PrintThunk(pSymbol);
                    break;

                case (int)SymTagEnum.SymTagCallSite:
                    PrintCallSiteInfo(pSymbol);
                    break;

                default:
                    PrintName(pSymbol);

                    IDiaSymbol pType3;

                    if ((pType3 = pSymbol.type) != null)
                    {
                        Console.Write(" has type ");
                        //fwprintf(pFileout,L" has type ");
                        PrintType(pType3);
                    }
                    break;
            }

            if ((dwSymTag == (int)SymTagEnum.SymTagUDT) || (dwSymTag == (int)SymTagEnum.SymTagAnnotation))
            {
                IDiaEnumSymbols pEnumChildren;

                Console.WriteLine("");
                //fputwc(L'\n',pFileout);

                pSymbol.findChildren(SymTagEnum.SymTagNull, null, 0, out pEnumChildren);
                if (pEnumChildren != null)
                {
                    IDiaSymbol pChild;
                    uint celt = 0;

                    while (true)
                    {
                        pEnumChildren.Next(1, out pChild, out celt);
                        if (!(celt == 1 && pChild != null))
                        {
                            PrintSymbol(pChild, dwIndent + 2);
                        }
                        else break;
                    }
                }
                Console.WriteLine("");
                //fputwc(L'\n',pFileout);
            }
        }
        
        // Print the string coresponding to the symbol's tag property
        public void PrintSymTag(uint dwSymTag)
        {
            Console.Write(SafeDRef(rgTags, dwSymTag));
            //Console.Write(String.Format("{0}: ", SafeDRef(rgTags, dwSymTag)));
            //fwprintf(pFileout,L"%-15s: ", SafeDRef(rgTags, dwSymTag));
        }
        
        // Print the name of the symbol
        public void PrintName(IDiaSymbol pSymbol)
        {
            string stringName;
            string stringUndName;

            if ((stringName = pSymbol.name) == null)
            {
                Console.Write("(none)");
                //fwprintf(pFileout,L"(none)");
                return;
            }

            if ((stringUndName = pSymbol.undecoratedName) != null)
            {
                if (stringName == stringUndName)
                {
                    Console.Write(String.Format("{0}", stringName));
                    //fwprintf(pFileout,L"%s", stringName);
                }

                else
                {
                    Console.Write(String.Format("{0}({1})", stringUndName, stringName));
                    //fwprintf(pFileout,L"%s(%s)", stringUndName, stringName);
                }
            }

            else
            {
                Console.Write(String.Format("{0}", stringName));
                //fwprintf(pFileout,L"%s", stringName);
            }
        }
        
        // Print the undecorated name of the symbol
        //  - only SymTagFunction, SymTagData and SymTagPublicSymbol
        //    can have this property set
        public void PrintUndName(IDiaSymbol pSymbol)
        {
            string stringName;

            if ((stringName = pSymbol.undecoratedName) == null)
            {
                if ((stringName = pSymbol.name) != null)
                {
                    // Print the name of the symbol instead

                    Console.Write(String.Format("{0}", (stringName[0] != '\0') ? stringName : "(none)"));
                    //fwprintf(pFileout,L"%s", (stringName[0] != L'\0') ? stringName : L"(none)");
                }

                else
                {
                    Console.Write("(none)");
                    //fwprintf(pFileout,L"(none)");
                }

                return;
            }

            if (stringName[0] != '\0')
            {
                Console.Write(String.Format("{0}", stringName));
                //fwprintf(pFileout,L"%s", stringName);
            }
        }
        
        // Print a SymTagThunk symbol's info
        public void PrintThunk(IDiaSymbol pSymbol)
        {
            uint dwRVA;
            uint dwISect;
            uint dwOffset;

            if (((dwRVA = pSymbol.relativeVirtualAddress) != null) &&
                ((dwISect = pSymbol.addressSection) != null) &&
                ((dwOffset = pSymbol.addressOffset) != null))
            {
                Console.Write(String.Format("[{0}][{1}:{2}] ", dwRVA, dwISect, dwOffset));
                //fwprintf(pFileout,L"[%08X][%04X:%08X]", dwRVA, dwISect, dwOffset);
            }

            if (((dwISect = pSymbol.targetSection) != null) &&
                ((dwOffset = pSymbol.targetOffset) != null) &&
                ((dwRVA = pSymbol.targetRelativeVirtualAddress) != null))
            {
                Console.Write(String.Format(", target [{0}][{1}:{2}] ", dwRVA, dwISect, dwOffset));
                //fwprintf(pFileout,L", target [%08X][%04X:%08X] ", dwRVA, dwISect, dwOffset);
            }

            else
            {
                Console.Write(", target ");
                //fwprintf(pFileout,L", target ");

                PrintName(pSymbol);
            }
        }
        
        // Print the compiland/module details: language, platform...
        public void PrintCompilandDetails(IDiaSymbol pSymbol)
        {
            uint dwLanguage;

            if ((dwLanguage = pSymbol.language) != null)
            {
                Console.Write(String.Format("\n\tLanguage: {0}\n", SafeDRef(rgLanguage, dwLanguage)));
                //fwprintf(pFileout,L"\n\tLanguage: %s\n", SafeDRef(rgLanguage, dwLanguage));
            }

            uint dwPlatform;

            if ((dwPlatform = pSymbol.platform) != null)
            {
                Console.Write(String.Format("\tTarget processor: %s\n", SafeDRef(rgProcessorStrings, dwPlatform)));
                //fwprintf(pFileout,L"\tTarget processor: %s\n", SafeDRef(rgProcessorStrings, dwPlatform));
            }

            int fEC;

            if ((fEC = pSymbol.editAndContinueEnabled) != null)
            {
                if (fEC != 0)
                {
                    Console.Write("\tCompiled for edit and continue: yes\n");
                    //fwprintf(pFileout,L"\tCompiled for edit and continue: yes\n");
                }

                else
                {
                    Console.Write("\tCompiled for edit and continue: no\n");
                    //fwprintf(pFileout,L"\tCompiled for edit and continue: no\n");
                }
            }

            int fDbgInfo;

            if ((fDbgInfo = pSymbol.hasDebugInfo) != null)
            {
                if (fDbgInfo != 0)
                {
                    Console.Write("\tCompiled without debugging info: no\n");
                    //fwprintf(pFileout,L"\tCompiled without debugging info: no\n");
                }

                else
                {
                    Console.Write("\tCompiled without debugging info: yes\n");
                    //fwprintf(pFileout,L"\tCompiled without debugging info: yes\n");
                }
            }

            int fLTCG;

            if ((fLTCG = pSymbol.isLTCG) != null)
            {
                if (fLTCG != 0)
                {
                    Console.Write("\tCompiled with LTCG: yes\n");
                    //fwprintf(pFileout,L"\tCompiled with LTCG: yes\n");
                }

                else
                {
                    Console.Write("\tCompiled with LTCG: no\n");
                    //fwprintf(pFileout,L"\tCompiled with LTCG: no\n");
                }
            }

            int fDataAlign;

            if ((fDataAlign = pSymbol.isDataAligned) != null)
            {
                if (fDataAlign != 0)
                {
                    Console.Write("\tCompiled with /bzalign: no\n");
                    //fwprintf(pFileout,L"\tCompiled with /bzalign: no\n");
                }

                else
                {
                    Console.Write("\tCompiled with /bzalign: yes\n");
                    //fwprintf(pFileout,L"\tCompiled with /bzalign: yes\n");
                }
            }

            int fManagedPresent;

            if ((fManagedPresent = pSymbol.hasManagedCode) != null)
            {
                if (fManagedPresent != 0)
                {
                    Console.Write("\tManaged code present: yes\n");
                    //fwprintf(pFileout,L"\tManaged code present: yes\n");
                }

                else
                {
                    Console.Write("\tManaged code present: no\n");
                    //fwprintf(pFileout,L"\tManaged code present: no\n");
                }
            }

            int fSecurityChecks;

            if ((fSecurityChecks = pSymbol.hasSecurityChecks) != null)
            {
                if (fSecurityChecks != 0)
                {
                    Console.Write("\tCompiled with /GS: yes\n");
                    //fwprintf(pFileout,L"\tCompiled with /GS: yes\n");
                }

                else
                {
                    Console.Write("\tCompiled with /GS: no\n");
                    //fwprintf(pFileout,L"\tCompiled with /GS: no\n");
                }
            }

            int fHotPatch;

            if ((fHotPatch = pSymbol.isHotpatchable) != null)
            {
                if (fHotPatch != 0)
                {
                    Console.Write("\tCompiled with /hotpatch: yes\n");
                    //fwprintf(pFileout,L"\tCompiled with /hotpatch: yes\n");
                }

                else
                {
                    Console.Write("\tCompiled with /hotpatch: no\n");
                    //fwprintf(pFileout,L"\tCompiled with /hotpatch: no\n");
                }
            }

            int fCVTCIL;

            if ((fCVTCIL = pSymbol.isCVTCIL) != null)
            {
                if (fCVTCIL != 0)
                {
                    Console.Write("\tConverted by CVTCIL: yes\n");
                    //fwprintf(pFileout,L"\tConverted by CVTCIL: yes\n");
                }

                else
                {
                    Console.Write("\tConverted by CVTCIL: no\n");
                    //fwprintf(pFileout,L"\tConverted by CVTCIL: no\n");
                }
            }

            int fMSILModule;

            if ((fMSILModule = pSymbol.isMSILNetmodule) != null)
            {
                if (fMSILModule != 0)
                {
                    Console.Write("\tMSIL module: yes\n");
                    //fwprintf(pFileout,L"\tMSIL module: yes\n");
                }

                else
                {
                    Console.Write("\tMSIL module: no\n");
                    //fwprintf(pFileout,L"\tMSIL module: no\n");
                }
            }

            uint dwVerMajor;
            uint dwVerMinor;
            uint dwVerBuild;
            uint dwVerQFE;

            if (((dwVerMajor = pSymbol.frontEndMajor) != null) &&
                ((dwVerMinor = pSymbol.frontEndMinor) != null) &&
                ((dwVerBuild = pSymbol.frontEndBuild) != null))
            {
                Console.Write(String.Format("\tFrontend Version: Major = {0}, Minor = {1}, Build = {2}", dwVerMajor, dwVerMinor, dwVerBuild));
                //fwprintf(pFileout,L"\tFrontend Version: Major = %u, Minor = %u, Build = %u",
                //        dwVerMajor,
                //        dwVerMinor,
                //        dwVerBuild);

                if ((dwVerQFE = pSymbol.frontEndQFE) != null)
                {
                    Console.Write(String.Format(", QFE = {0}", dwVerQFE));
                    //fwprintf(pFileout,L", QFE = %u", dwVerQFE);
                }

                Console.WriteLine("");
                //fputwc(L'\n',pFileout);
            }

            if (((dwVerMajor = pSymbol.backEndMajor) != null) &&
                ((dwVerMinor = pSymbol.backEndMinor) != null) &&
                ((dwVerBuild = pSymbol.backEndBuild) != null))
            {
                Console.Write(String.Format("\tBackend Version: Major = {0}, Minor = {1}, Build = {2}", dwVerMajor, dwVerMinor, dwVerBuild));
                //fwprintf(pFileout,L"\tBackend Version: Major = %u, Minor = %u, Build = %u",
                //        dwVerMajor,
                //        dwVerMinor,
                //        dwVerBuild);

                if ((dwVerQFE = pSymbol.backEndQFE) != null)
                {
                    Console.Write(String.Format(", QFE = {0}", dwVerQFE));
                    //fwprintf(pFileout,L", QFE = %u", dwVerQFE);
                }

                Console.WriteLine("");
                //fputwc(L'\n',pFileout);
            }

            string stringCompilerName;

            if ((stringCompilerName = pSymbol.compilerName) != null)
            {
                if (stringCompilerName != null)
                {
                    Console.Write(String.Format("\tVersion string: {0}", stringCompilerName));
                    //fwprintf(pFileout,L"\tVersion string: %s", stringCompilerName);
                }
            }

            Console.WriteLine("");
            //fputwc(L'\n',pFileout);
        }
        
        // Print the compilan/module env
        public void PrintCompilandEnv(IDiaSymbol pSymbol)
        {
          PrintName(pSymbol);
          Console.Write(" =");
          //fwprintf(pFileout,L" =");

          dynamic vt;
          //VARIANT vt = { VT_EMPTY };

          if ((vt=pSymbol.value) != null)
          {
            PrintVariant(vt);
            //VariantClear((VARIANTARG *) &vt);
          }
        }
        
        // Print a string corespondig to a location type
        public void PrintLocation(IDiaSymbol pSymbol)
        {
          uint dwLocType;
          uint dwRVA, dwSect, dwOff, dwReg, dwBitPos, dwSlot;
          long lOffset;
          ulong ulLen;
            dynamic vt;
          //VARIANT vt = { VT_EMPTY };
        
          if ((dwLocType=pSymbol.locationType) ==null) {
            // It must be a symbol in optimized code
        
              Console.Write("symbol in optmized code");
            //fwprintf(pFileout,L"symbol in optmized code");
            return;
          }
        
          switch (dwLocType) {
            case (int)LocationType.LocIsStatic:
              if (((dwRVA=pSymbol.relativeVirtualAddress) !=null) &&
                  ((dwSect=pSymbol.addressSection) !=null) &&
                  ((dwOff=pSymbol.addressOffset) !=null)) {
                  Console.Write(String.Format("{0}, [{1}][{2}:{3}] ", SafeDRef(rgLocationTypeString, dwLocType), dwRVA, dwSect, dwOff));
                //fwprintf(pFileout,L"%s, [%08X][%04X:%08X]", SafeDRef(rgLocationTypeString, dwLocType), dwRVA, dwSect, dwOff);
              }
              break;
        
            case (int)LocationType.LocIsTLS:
            case (int)LocationType.LocInMetaData:
            case (int)LocationType.LocIsIlRel:
              if (((dwRVA=pSymbol.relativeVirtualAddress) !=null) &&
                  ((dwSect=pSymbol.addressSection) !=null) &&
                  ((dwOff=pSymbol.addressOffset) !=null)) {
                  Console.Write(String.Format("{0}, [{1}][{2}:{3}] ", SafeDRef(rgLocationTypeString, dwLocType), dwRVA, dwSect, dwOff));
                //fwprintf(pFileout,L"%s, [%08X][%04X:%08X]", SafeDRef(rgLocationTypeString, dwLocType), dwRVA, dwSect, dwOff);
              }
              break;
        
            case (int)LocationType.LocIsRegRel:
              if (((dwReg=pSymbol.registerId) !=null) &&
                  ((lOffset=pSymbol.offset) !=null)) {
                  Console.Write(String.Format("{0} Relative, [{1}]", SzNameC7Reg((ushort) dwReg), lOffset));
                //fwprintf(pFileout,L"%s Relative, [%08X]", SzNameC7Reg((USHORT) dwReg), lOffset);
              }
              break;
        
            case (int)LocationType.LocIsThisRel:
              if ((lOffset=pSymbol.offset) !=null) {
                  Console.Write(String.Format("this+0x{0}", lOffset));
                //fwprintf(pFileout,L"this+0x%X", lOffset);
              }
              break;
        
            case (int)LocationType.LocIsBitField:
              if (((lOffset=pSymbol.offset) !=null) &&
                  ((dwBitPos=pSymbol.bitPosition) !=null) &&
                  ((ulLen=pSymbol.length) !=null)) {
                  Console.Write(String.Format("this(bf)+0x{0}:0x{1} len(0x{2})", lOffset, dwBitPos, ulLen));
                //fwprintf(pFileout,L"this(bf)+0x%X:0x%X len(0x%X)", lOffset, dwBitPos, ulLen);
              }
              break;
        
            case (int)LocationType.LocIsEnregistered:
              if ((dwReg=pSymbol.registerId) !=null) {
                  Console.Write(String.Format("enregistered {0}", SzNameC7Reg((ushort) dwReg)));
                //fwprintf(pFileout,L"enregistered %s", SzNameC7Reg((USHORT) dwReg));
              }
              break;
        
            case (int)LocationType.LocIsSlot:
              if ((dwSlot=pSymbol.slot) !=null) {
                  Console.Write(String.Format("{0}, [{1}]", SafeDRef(rgLocationTypeString, dwLocType), dwSlot));
                //fwprintf(pFileout,L"%s, [%08X]", SafeDRef(rgLocationTypeString, dwLocType), dwSlot);
              }
              break;
        
            case (int)LocationType.LocIsConstant:
                  Console.Write("constant");
              //fwprintf(pFileout,L"constant");
        
              if ((vt=pSymbol.value) !=null) {
                PrintVariant(vt);
                //VariantClear((VARIANTARG *) &vt);
              }
              break;
        
            case (int)LocationType.LocIsNull:
              break;
        
            default :
                  Console.Write(String.Format("Error - invalid location type: 0x{0}", dwLocType));
              //fwprintf(pFileout,L"Error - invalid location type: 0x%X", dwLocType);
              break;
            }
        }
        
        // Print the type, value and the name of a const symbol
        public void PrintConst(IDiaSymbol pSymbol)
        {
            PrintSymbolType(pSymbol);

            dynamic vt;
            //VARIANT vt = { VT_EMPTY };

            if ((vt = pSymbol.value) != null)
            {
                PrintVariant(vt);
                //VariantClear((VARIANTARG*)&vt);
            }

            PrintName(pSymbol);
        }
        
        // Print the name and the type of an user defined type
        public void PrintUDT(IDiaSymbol pSymbol)
        {
          PrintName(pSymbol);
          PrintSymbolType(pSymbol);
        }
        
        // Print a string representing the type of a symbol
        public void PrintSymbolType(IDiaSymbol pSymbol)
        {
          IDiaSymbol pType;
        
          if ((pType=pSymbol.type) !=null) {
              Console.Write(", Type: ");
            //fwprintf(pFileout,L", Type: ");
            PrintType(pType);
          }
        }
        
        // Print the information details for a type symbol
        public void PrintType(IDiaSymbol pSymbol)
        {
            IDiaSymbol pBaseType;
            IDiaEnumSymbols pEnumSym;
            IDiaSymbol pSym;
            uint dwTag;
            string stringName;
            uint dwInfo;
            int bSet;
            uint dwRank;
            long lCount = 0;
            uint celt = 1;

            if ((dwTag = pSymbol.symTag) == null)
            {
                Console.Write("ERROR - can't retrieve the symbol's SymTag\n");
                //fwprintf(pFileout,L"ERROR - can't retrieve the symbol's SymTag\n");
                return;
            }

            if ((stringName = pSymbol.name) == null)
            {
                stringName = null;
            }

            if (dwTag != (int)SymTagEnum.SymTagPointerType)
            {
                if (((bSet = pSymbol.constType) != null) && bSet != 0)
                {
                    Console.Write("const ");
                    //fwprintf(pFileout,L"const ");
                }

                if (((bSet = pSymbol.volatileType) != null) && bSet != 0)
                {
                    Console.Write("volatile ");
                    //fwprintf(pFileout,L"volatile ");
                }

                if (((bSet = pSymbol.unalignedType) != null) && bSet != 0)
                {
                    Console.Write("__unaligned ");
                    //fwprintf(pFileout,L"__unaligned ");
                }
            }

            ulong ulLen;

            ulLen = pSymbol.length;

            switch (dwTag)
            {
                case (int)SymTagEnum.SymTagUDT:
                    PrintUdtKind(pSymbol);
                    PrintName(pSymbol);
                    break;

                case (int)SymTagEnum.SymTagEnum:
                    Console.Write("enum ");
                    //fwprintf(pFileout,L"enum ");
                    PrintName(pSymbol);
                    break;

                case (int)SymTagEnum.SymTagFunctionType:
                    Console.Write("function ");
                    //fwprintf(pFileout,L"function ");
                    break;

                case (int)SymTagEnum.SymTagPointerType:
                    if ((pBaseType = pSymbol.type) == null)
                    {
                        Console.Write("ERROR - SymTagPointerType get_type ");
                        //fwprintf(pFileout,L"ERROR - SymTagPointerType get_type");
                        return;
                    }

                    PrintType(pBaseType);

                    if (((bSet = pSymbol.reference) != null) && bSet != 0)
                    {
                        Console.Write(" &");
                        //fwprintf(pFileout,L" &");
                    }

                    else
                    {
                        Console.Write(" *");
                        //fwprintf(pFileout,L" *");
                    }

                    if (((bSet = pSymbol.constType) != null) && bSet != 0)
                    {
                        Console.Write(" const");
                        //fwprintf(pFileout,L" const");
                    }

                    if (((bSet = pSymbol.volatileType) != null) && bSet != 0)
                    {
                        Console.Write(" volatile");
                        //fwprintf(pFileout,L" volatile");
                    }

                    if (((bSet = pSymbol.unalignedType) != null) && bSet != 0)
                    {
                        Console.Write(" __unaligned");
                        //fwprintf(pFileout,L" __unaligned");
                    }
                    break;

                case (int)SymTagEnum.SymTagArrayType:
                    if ((pBaseType = pSymbol.type) != null)
                    {
                        PrintType(pBaseType);

                        if ((dwRank = pSymbol.rank) != null)
                        {
                            pSymbol.findChildren(SymTagEnum.SymTagDimension, null, 0, out pEnumSym);
                            if (pEnumSym != null)
                            {
                                while (true)
                                {
                                    pEnumSym.Next(1, out pSym, out celt);
                                    if (pSym == null) break;

                                    if (!(pSym != null && celt == 1))
                                    {
                                        IDiaSymbol pBound;

                                        Console.Write("[");
                                        //fwprintf(pFileout,L"[");

                                        if ((pBound = pSym.lowerBound) != null)
                                        {
                                            PrintBound(pBound);

                                            Console.Write("..");
                                            //fwprintf(pFileout,L"..");

                                        }

                                        pBound = null;

                                        if ((pBound = pSym.upperBound) != null)
                                        {
                                            PrintBound(pBound);
                                        }

                                        pSym = null;

                                        Console.Write("]");
                                        //fwprintf(pFileout,L"]");
                                    }
                                    else break;
                                }
                            }

                            else
                            {
                                pSymbol.findChildren(SymTagEnum.SymTagCustomType, null, 0, out pEnumSym);
                                if ((pEnumSym != null) &&
                                   ((lCount = pEnumSym.count) != null) &&
                                   (lCount > 0))
                                {
                                    while (true)
                                    {
                                        pEnumSym.Next(1, out pSym, out celt);
                                        if (pSym != null && (celt == 1))
                                        {
                                            Console.Write("[");
                                            //fwprintf(pFileout,L"[");
                                            PrintType(pSym);
                                            Console.Write("]");
                                            //fwprintf(pFileout,L"]");

                                        }
                                        else break;

                                    }
                                }


                                else
                                {
                                    uint dwCountElems;
                                    ulong ulLenArray;
                                    ulong ulLenElem;

                                    if ((dwCountElems = pSymbol.count) != null)
                                    {
                                        Console.Write(String.Format("[0x{0}] ", dwCountElems));
                                        //fwprintf(pFileout,L"[0x%X]", dwCountElems);
                                    }

                                    else if (((ulLenArray = pSymbol.length) != null) &&
                                             ((ulLenElem = pBaseType.length) != null))
                                    {
                                        if (ulLenElem == 0)
                                        {
                                            Console.Write(String.Format("[0x{0}] ", ulLenArray));
                                            //fwprintf(pFileout,L"[0x%lX]", ulLenArray);
                                        }

                                        else
                                        {
                                            Console.Write(String.Format("[0x{0}] ", ulLenArray / ulLenElem));
                                            //fwprintf(pFileout,L"[0x%lX]", ulLenArray/ulLenElem);
                                        }
                                    }
                                }
                            }
                        }

                        else
                        {
                            Console.Write("ERROR - SymTagArrayType get_type\n");
                            //fwprintf(pFileout,L"ERROR - SymTagArrayType get_type\n");
                            return;
                        }
                    }
                    break;

                case (int)SymTagEnum.SymTagBaseType:
                    if ((dwInfo = pSymbol.baseType) == null)
                    {
                        Console.Write("SymTagBaseType get_baseType\n");
                        //fwprintf(pFileout,L"SymTagBaseType get_baseType\n");
                        return;
                    }

                    switch (dwInfo)
                    {
                        case (int)BasicType.btUInt:
                            Console.Write("unsigned ");

                            switch (ulLen)
                            {
                                case 1:
                                    Console.Write("char ");
                                    //fwprintf(pFileout,L"char");
                                    break;

                                case 2:
                                    Console.Write("short ");
                                    //fwprintf(pFileout,L"short");
                                    break;

                                case 4:
                                    Console.Write("int ");
                                    //fwprintf(pFileout,L"int");
                                    break;

                                case 8:
                                    Console.Write("__int64 ");
                                    //fwprintf(pFileout,L"__int64");
                                    break;
                            }

                            dwInfo = 0xFFFFFFFF;
                            break;
                        //fwprintf(pFileout,L"unsigned ");

                        // Fall through

                        case (int)BasicType.btInt:
                            switch (ulLen)
                            {
                                case 1:
                                    if (dwInfo == (int)BasicType.btInt)
                                    {
                                        Console.Write("signed ");
                                        //fwprintf(pFileout,L"signed ");
                                    }

                                    Console.Write("char ");
                                    //fwprintf(pFileout,L"char");
                                    break;

                                case 2:
                                    Console.Write("short ");
                                    //fwprintf(pFileout,L"short");
                                    break;

                                case 4:
                                    Console.Write("int ");
                                    //fwprintf(pFileout,L"int");
                                    break;

                                case 8:
                                    Console.Write("__int64 ");
                                    //fwprintf(pFileout,L"__int64");
                                    break;
                            }

                            dwInfo = 0xFFFFFFFF;
                            break;

                        case (int)BasicType.btFloat:
                            switch (ulLen)
                            {
                                case 4:
                                    Console.Write("float ");
                                    //fwprintf(pFileout,L"float");
                                    break;

                                case 8:
                                    Console.Write("double ");
                                    //fwprintf(pFileout,L"double");
                                    break;
                            }

                            dwInfo = 0xFFFFFFFF;
                            break;
                    }

                    if (dwInfo == 0xFFFFFFFF)
                    {
                        break;
                    }

                    Console.Write(rgBaseType[dwInfo]);
                    //fwprintf(pFileout,L"%s", rgBaseType[dwInfo]);
                    break;

                case (int)SymTagEnum.SymTagTypedef:
                    PrintName(pSymbol);
                    break;

                case (int)SymTagEnum.SymTagCustomType:
                    {
                        uint idOEM, idOEMSym;
                        uint cbData = 0;
                        uint count;

                        if ((idOEM = pSymbol.oemId) != null)
                        {
                            Console.Write(String.Format("OEMId = {0}", idOEM));
                            //fwprintf(pFileout,L"OEMId = %X, ", idOEM);
                        }

                        if ((idOEMSym = pSymbol.oemSymbolId) != null)
                        {
                            Console.Write(String.Format("SymbolId = {0}", idOEMSym));
                            //fwprintf(pFileout,L"SymbolId = %X, ", idOEMSym);
                        }

                        //if (pSymbol.get_types(0, out count, null) !=null) {
                        //  IDiaSymbol** rgpDiaSymbols = (IDiaSymbol**) _alloca(sizeof(IDiaSymbol *) * count);

                        //  if (pSymbol.types(count, &count, rgpDiaSymbols) !=null) {
                        //    for (ulong i = 0; i < count; i++) {
                        //      PrintType(rgpDiaSymbols[i]);
                        //      rgpDiaSymbols[i]->Release();
                        //    }
                        //  }
                        //}

                        //// print custom data

                        //if ((pSymbol.dataBytes(cbData, &cbData, null) !=null) && (cbData != 0)) {
                        //  fwprintf(pFileout,L", Data: ");

                        //  byte[] pbData = new byte[cbData];

                        //  pSymbol.get_dataBytes(cbData, out cbData, out pbData);

                        //  for (ulong i = 0; i < cbData; i++) {
                        //    fwprintf(pFileout,L"0x%02X ", pbData[i]);
                        //  }

                        //  delete [] pbData;
                        //}
                    }
                    break;

                case (int)SymTagEnum.SymTagData: // This really is member data, just print its location
                    PrintLocation(pSymbol);
                    break;
            }

        }
        
        // Print bound information
        public void PrintBound(IDiaSymbol pSymbol)
        {
          uint dwTag = 0;
          uint dwKind;
        
          if ((dwTag=pSymbol.symTag) ==null) {
              Console.Write("ERROR - PrintBound() get_symTag");
            //fwprintf(pFileout,L"ERROR - PrintBound() get_symTag");
            return;
          }

          if ((dwKind=pSymbol.locationType) == null)
          {
              Console.Write("ERROR - PrintBound() get_locationType");
            //fwprintf(pFileout,L"ERROR - PrintBound() get_locationType");
            return;
          }
        
          if (dwTag == (int)SymTagEnum.SymTagData && dwKind == (int)LocationType.LocIsConstant) {
            //VARIANT v;
              dynamic v;
        
            if ((v=pSymbol.value) !=null) {
              PrintVariant(v);
              //VariantClear((VARIANTARG *) &v);
            }
          }
        
          else {
            PrintName(pSymbol);
          }
        }
        
        ////////////////////////////////////////////////////////////
        public void PrintData(IDiaSymbol pSymbol, uint dwIndent)
        {
            PrintLocation(pSymbol);

            uint dwDataKind;
            if ((dwDataKind = pSymbol.dataKind) == null)
            {
                Console.Write("ERROR - PrintData() get_dataKind");
                //fwprintf(pFileout,L"ERROR - PrintData() get_dataKind");
                return;
            }

            Console.Write(String.Format(", {0}", SafeDRef(rgDataKind, dwDataKind)));
            //fwprintf(pFileout,L", %s", SafeDRef(rgDataKind, dwDataKind));
            PrintSymbolType(pSymbol);

            Console.Write(", ");
            //fwprintf(pFileout,L", ");
            PrintName(pSymbol);
        }
        
        // Print a VARIANT
        //public void PrintVariant(VARIANT var)
        public void PrintVariant(dynamic var)
        {
            Console.Write(var);
          //switch (var.vt) {
          //  case VT_UI1:
          //  case VT_I1:
          //    fwprintf(pFileout,L" 0x%X", var.bVal);
          //    break;
        
          //  case VT_I2:
          //  case VT_UI2:
          //  case VT_bool:
          //    fwprintf(pFileout,L" 0x%X", var.iVal);
          //    break;
        
          //  case VT_I4:
          //  case VT_UI4:
          //  case VT_INT:
          //  case VT_UINT:
          //  case VT_ERROR:
          //    fwprintf(pFileout,L" 0x%X", var.lVal);
          //    break;
        
          //  case VT_R4:
          //    fwprintf(pFileout,L" %g", var.fltVal);
          //    break;
        
          //  case VT_R8:
          //    fwprintf(pFileout,L" %g", var.dblVal);
          //    break;
        
          //  case VT_string:
          //    fwprintf(pFileout,L" \"%s\"", var.stringVal);
          //    break;
        
          //  default:
          //    fwprintf(pFileout,L" ??");
          //  }
        }
        
        // Print a string corresponding to a UDT kind
        public void PrintUdtKind(IDiaSymbol pSymbol)
        {
          uint dwKind = 0;
        
          if ((dwKind=pSymbol.udtKind) !=null) {
              Console.Write(String.Format("{0} ", rgUdtKind[dwKind]));
            //fwprintf(pFileout,L"%s ", rgUdtKind[dwKind]);
          }
        }
        
        // Print type informations is details
        public void PrintTypeInDetail(IDiaSymbol pSymbol, uint dwIndent)
        {
            IDiaEnumSymbols pEnumChildren;
            IDiaSymbol pType;
            IDiaSymbol pChild;
            uint dwSymTag;
            uint dwSymTagType;
            uint celt = 0;
            int bFlag;

            if (dwIndent > MAX_TYPE_IN_DETAIL)
            {
                return;
            }

            if ((dwSymTag = pSymbol.symTag) == null)
            {
                Console.Write("ERROR - PrintTypeInDetail() get_symTag\n");
                //fwprintf(pFileout,L"ERROR - PrintTypeInDetail() get_symTag\n");
                return;
            }

            PrintSymTag(dwSymTag);

            for (uint i = 0; i < dwIndent; i++)
            {
                Console.Write(' ');
                //fputwc(L' ',pFileout);
            }

            switch (dwSymTag)
            {
                case (int)SymTagEnum.SymTagData:
                    PrintData(pSymbol, dwIndent);

                    if ((pType = pSymbol.type) != null)
                    {
                        if ((dwSymTagType = pType.symTag) != null)
                        {
                            if (dwSymTagType == (int)SymTagEnum.SymTagUDT)
                            {
                                Console.WriteLine("");
                                //fputwc(L'\n',pFileout);
                                PrintTypeInDetail(pType, dwIndent + 2);
                            }
                        }
                    }
                    break;

                case (int)SymTagEnum.SymTagTypedef:
                case (int)SymTagEnum.SymTagVTable:
                    PrintSymbolType(pSymbol);
                    break;

                case (int)SymTagEnum.SymTagEnum:
                case (int)SymTagEnum.SymTagUDT:
                    PrintUDT(pSymbol);
                    Console.WriteLine("");
                    //fputwc(L'\n',pFileout);

                    pSymbol.findChildren(SymTagEnum.SymTagNull, null, 0, out pEnumChildren);
                    if (pEnumChildren != null)
                    {
                        while (true)
                        {
                            pEnumChildren.Next(1, out pChild, out celt);
                            if (pChild != null && (celt == 1))
                            {
                                PrintTypeInDetail(pChild, dwIndent + 2);
                            }
                            else break;
                        }
                    }
                    return;
                    break;

                case (int)SymTagEnum.SymTagFunction:
                    PrintFunctionType(pSymbol);
                    return;
                    break;

                case (int)SymTagEnum.SymTagPointerType:
                    PrintName(pSymbol);
                    Console.Write(" has type ");
                    //fwprintf(pFileout,L" has type ");
                    PrintType(pSymbol);
                    break;

                case (int)SymTagEnum.SymTagArrayType:
                case (int)SymTagEnum.SymTagBaseType:
                case (int)SymTagEnum.SymTagFunctionArgType:
                case (int)SymTagEnum.SymTagUsingNamespace:
                case (int)SymTagEnum.SymTagCustom:
                case (int)SymTagEnum.SymTagFriend:
                    PrintName(pSymbol);
                    PrintSymbolType(pSymbol);
                    break;

                case (int)SymTagEnum.SymTagVTableShape:
                case (int)SymTagEnum.SymTagBaseClass:
                    PrintName(pSymbol);

                    if (((bFlag = pSymbol.virtualBaseClass) != null) && bFlag != 0)
                    {
                        IDiaSymbol pVBTableType;
                        long ptrOffset;
                        uint dispIndex;

                        if (((dispIndex = pSymbol.virtualBaseDispIndex) != null) &&
                            ((ptrOffset = pSymbol.virtualBasePointerOffset) != null))
                        {
                            Console.Write(String.Format(" virtual, offset = 0x{0}, pointer offset = {1}, virtual base pointer type = ", dispIndex, ptrOffset));
                            //fwprintf(pFileout,L" virtual, offset = 0x%X, pointer offset = %ld, virtual base pointer type = ", dispIndex, ptrOffset);

                            if ((pVBTableType = pSymbol.virtualBaseTableType) != null)
                            {
                                PrintType(pVBTableType);
                            }

                            else
                            {
                                Console.Write("(unknown)");
                                //fwprintf(pFileout,L"(unknown)");
                            }
                        }
                    }

                    else
                    {
                        long offset;

                        if ((offset = pSymbol.offset) != null)
                        {
                            Console.Write(String.Format(", offset = 0x{0}", offset));
                            //fwprintf(pFileout,L", offset = 0x%X", offset);
                        }
                    }

                    Console.WriteLine("");
                    //fputwc(L'\n',pFileout);

                    pSymbol.findChildren(SymTagEnum.SymTagNull, null, 0, out pEnumChildren);
                    if (pEnumChildren != null)
                    {
                        while (true)
                        {
                            pEnumChildren.Next(1, out pChild, out celt);
                            if (pChild != null && (celt == 1))
                            {
                                PrintTypeInDetail(pChild, dwIndent + 2);
                            }
                            else break;
                        }
                    }
                    break;

                case (int)SymTagEnum.SymTagFunctionType:
                    if ((pType = pSymbol.type) != null)
                    {
                        PrintType(pType);
                    }
                    break;

                case (int)SymTagEnum.SymTagThunk:
                    // Happens for functions which only have S_PROCREF
                    PrintThunk(pSymbol);
                    break;

                default:
                    Console.WriteLine("ERROR - PrintTypeInDetail() invalid SymTag\n");
                    break;
                //fwprintf(pFileout,L"ERROR - PrintTypeInDetail() invalid SymTag\n");
            }

            Console.WriteLine("");
            //fputwc(L'\n',pFileout);
        }
        
        // Print a function type
        public void PrintFunctionType(IDiaSymbol pSymbol)
        {
            uint dwAccess = 0;

            if ((dwAccess = pSymbol.access) != null)
            {
                Console.Write(String.Format("{0} ", SafeDRef(rgAccess, dwAccess)));
                //fwprintf(pFileout,L"%s ", SafeDRef(rgAccess, dwAccess));
            }

            int bIsStatic = 0;

            if (((bIsStatic = pSymbol.isStatic) != null) && bIsStatic != 0)
            {
                Console.Write("static ");
                //fwprintf(pFileout,L"static ");
            }

            IDiaSymbol pFuncType;

            if ((pFuncType = pSymbol.type) != null)
            {
                IDiaSymbol pReturnType;

                if ((pReturnType = pFuncType.type) != null)
                {
                    PrintType(pReturnType);
                    Console.Write(' ');
                    //fputwc(L' ',pFileout);

                    string stringName;

                    if ((stringName = pSymbol.name) != null)
                    {
                        Console.Write(stringName);
                        //fwprintf(pFileout,L"%s", stringName);
                    }

                    IDiaEnumSymbols pEnumChildren;

                    pFuncType.findChildren(SymTagEnum.SymTagNull, null, 0, out pEnumChildren);
                    if (pEnumChildren != null)
                    {
                        IDiaSymbol pChild;
                        uint celt = 0;
                        ulong nParam = 0;

                        Console.Write("(");
                        //fwprintf(pFileout,L"(");

                        while (true)
                        {
                            pEnumChildren.Next(1, out pChild, out celt);
                            if (pChild != null && (celt == 1))
                            {
                                IDiaSymbol pType;

                                if ((pType = pChild.type) != null)
                                {
                                    if (nParam++ != 0)
                                    {
                                        Console.Write(", ");
                                        //fwprintf(pFileout,L", ");
                                    }

                                    PrintType(pType);
                                }

                            }
                            else break;
                        }

                        Console.Write(")\n");
                        //fwprintf(pFileout,L")\n");
                    }

                }

            }
        }
        
        ////////////////////////////////////////////////////////////
        //
        public void PrintSourceFile(IDiaSourceFile pSource)
        {
            string stringSourceName;

            if ((stringSourceName = pSource.fileName) != null)
            {
                Console.Write(String.Format("\t{0}", stringSourceName));
                //fwprintf(pFileout,L"\t%s", stringSourceName);
            }

            else
            {
                Console.Write("ERROR - PrintSourceFile() get_fileName");
                //fwprintf(pFileout,L"ERROR - PrintSourceFile() get_fileName");
                return;
            }

            byte[] checksum = new byte[256];
            uint cbChecksum = 256;
            pSource.get_checksum(cbChecksum, out cbChecksum, out checksum[0]);

            if (checksum != null&&checksum[0]!=0)
            {
                Console.Write(" (");
                //fwprintf(pFileout,L" (");

                uint checksumType;

                if ((checksumType = pSource.checksumType) != null)
                {
                    switch (checksumType)
                    {
                        case (int)CV_SourceChksum_t.CHKSUM_TYPE_NONE:
                            Console.Write("None");
                            //fwprintf(pFileout,L"None");
                            break;

                        case (int)CV_SourceChksum_t.CHKSUM_TYPE_MD5:
                            Console.Write("MD5");
                            //fwprintf(pFileout,L"MD5");
                            break;

                        case (int)CV_SourceChksum_t.CHKSUM_TYPE_SHA1:
                            Console.Write("SHA1");
                            //fwprintf(pFileout,L"SHA1");
                            break;

                        default:
                            Console.Write(String.Format("0x{0}", checksumType));
                            //fwprintf(pFileout,L"0x%X", checksumType);
                            break;
                    }

                    if (cbChecksum != 0)
                    {
                        Console.Write(": ");
                        //fwprintf(pFileout,L": ");
                    }
                }

                for (uint ib = 0; ib < cbChecksum; ib++)
                {
                    Console.Write(String.Format("{0}", checksum[ib]));
                    //fwprintf(pFileout,L"%02X", checksum[ib]);
                }

                Console.Write(")");
                //fwprintf(pFileout,L")");
            }
        }
        
        ////////////////////////////////////////////////////////////
        //
        public void PrintLines(IDiaSession pSession, IDiaSymbol pFunction)
        {
            uint dwSymTag;

            if (((dwSymTag = pFunction.symTag) == null) || (dwSymTag != (int)SymTagEnum.SymTagFunction))
            {
                Console.Write("ERROR - PrintLines() dwSymTag != SymTagFunction");
                //fwprintf(pFileout,L"ERROR - PrintLines() dwSymTag != SymTagFunction");
                return;
            }

            string stringName;

            if ((stringName = pFunction.name) != null)
            {
                Console.Write(String.Format("\n** {0}\n\n", stringName));
                //fwprintf(pFileout,L"\n** %s\n\n", stringName);
            }

            ulong ulLength;

            if ((ulLength = pFunction.length) == null)
            {
                Console.Write("ERROR - PrintLines() get_length");
                //fwprintf(pFileout,L"ERROR - PrintLines() get_length");
                return;
            }

            uint dwRVA;
            IDiaEnumLineNumbers pLines;

            if ((dwRVA = pFunction.relativeVirtualAddress) != null)
            {
                pSession.findLinesByRVA(dwRVA, (uint)ulLength, out pLines);
                if (pLines != null)
                {
                    PrintLines(pLines);
                }
            }

            else
            {
                uint dwSect;
                uint dwOffset;

                if (((dwSect = pFunction.addressSection) != null) &&
                    ((dwOffset = pFunction.addressOffset) != null))
                {
                    pSession.findLinesByAddr(dwSect, dwOffset, (uint)ulLength, out pLines);
                    if (pLines != null)
                    {
                        PrintLines(pLines);
                    }
                }
            }
        }
        
        ////////////////////////////////////////////////////////////
        //
        public void PrintLines(IDiaEnumLineNumbers pLines)
        {
            IDiaLineNumber pLine;
            uint celt;
            uint dwRVA;
            uint dwSeg;
            uint dwOffset;
            uint dwLinenum;
            uint dwSrcId;
            uint dwLength;

            uint dwSrcIdLast = uint.MaxValue;

            while (true)
            {
                pLines.Next(1, out pLine, out celt);
                if (pLine != null && (celt == 1))
                {
                    if (((dwRVA = pLine.relativeVirtualAddress) != null) &&
                        ((dwSeg = pLine.addressSection) != null) &&
                        ((dwOffset = pLine.addressOffset) != null) &&
                        ((dwLinenum = pLine.lineNumber) != null) &&
                        ((dwSrcId = pLine.sourceFileId) != null) &&
                        ((dwLength = pLine.length) != null))
                    {

                        Console.Write(String.Format("\tline {0} at [{1}][{2}:{3}], len = 0x{4}", dwLinenum, dwRVA, dwSeg, dwOffset, dwLength));
                        //fwprintf(pFileout,L"\tline %u at [%08X][%04X:%08X], len = 0x%X", dwLinenum, dwRVA, dwSeg, dwOffset, dwLength);

                        if (dwSrcId != dwSrcIdLast)
                        {
                            IDiaSourceFile pSource;

                            if ((pSource = pLine.sourceFile) != null)
                            {
                                PrintSourceFile(pSource);

                                dwSrcIdLast = dwSrcId;

                            }
                        }
                        Console.Write("\n");
                    }
                }
                else break;
            }
        }

        // Print the section contribution data: name, Sec::Off, length
        #region cannt be updated
        //public void PrintSecContribs(IDiaSectionContrib pSegment)
        //{
        //  uint dwRVA;
        //  uint dwSect;
        //  uint dwOffset;
        //  uint dwLen;
        //  IDiaSymbol *pCompiland;
        //  string stringName;
        
        //  if ((pSegment.relativeVirtualAddress(&dwRVA) !=null) &&
        //      (pSegment.addressSection(&dwSect) !=null) &&
        //      (pSegment.addressOffset(&dwOffset) !=null) &&
        //      (pSegment.length(&dwLen) !=null) &&
        //      (pSegment.compiland(&pCompiland) !=null) &&
        //      (pCompiland.name(&stringName) !=null)) {
        //    fwprintf(pFileout,L"  %08X  %04X:%08X  %08X  %s\n", dwRVA, dwSect, dwOffset, dwLen, stringName);
        
        //    pCompiland->Release();
        
        //    SysFreeString(stringName);
        //  }
        //}
        #endregion

        // Print a debug stream data
        public void PrintStreamData(IDiaEnumDebugStreamData pStream)
        {
            string stringName;

            if ((stringName = pStream.name) == null)
            {
                Console.Write("ERROR - PrintStreamData() get_name\n");
                //fwprintf(pFileout,L"ERROR - PrintStreamData() get_name\n");
            }

            else
            {
                Console.Write(String.Format("Stream: {0}", stringName));
                //fwprintf(pFileout,L"Stream: %s", stringName);
            }

            long dwElem;

            if ((dwElem = pStream.count) == null)
            {
                Console.Write("ERROR - PrintStreamData() get_Count\n");
                //fwprintf(pFileout,L"ERROR - PrintStreamData() get_Count\n");
            }

            else
            {
                Console.Write(String.Format("({0})\n", dwElem));
                //fwprintf(pFileout,L"(%u)\n", dwElem);
            }

            uint cbTotal = 0;

            byte[] data = new byte[1024];
            uint cbData;
            uint celt = 0;

            while (true)
            {
                pStream.Next(1, 1024, out cbData, out data[0], out celt);
                if (data != null && (celt == 1))
                {
                    uint i;

                    for (i = 0; i < cbData; i++)
                    {
                        Console.Write(String.Format("{0} ", data[i]));
                        //fwprintf(pFileout,L"%02X ", data[i]);

                        if (i != 0 && (i % 8 == 7) && (i + 1 < cbData))
                        {
                            Console.Write("- ");
                            //fwprintf(pFileout,L"- ");
                        }
                    }

                    Console.Write("| ");
                    //fwprintf(pFileout,L"| ");

                    for (i = 0; i < cbData; i++)
                    {
                        Console.Write(String.Format("{0}", data[i]));
                        //fwprintf(pFileout,L"%c", iswprint(data[i]) ? data[i] : '.');
                    }


                    Console.Write("\n");
                    //fputwc(L'\n',pFileout);

                    cbTotal += cbData;
                }
                else break;
            }

            Console.Write(String.Format("Summary :\n\tNo of Elems = {0}\n", dwElem));
            //fwprintf(pFileout,L"Summary :\n\tNo of Elems = %u\n", dwElem);
            if (dwElem != 0)
            {
                Console.Write(String.Format("\tSizeof(Elem) = {0}\n", cbTotal / dwElem));
                //fwprintf(pFileout,L"\tSizeof(Elem) = %u\n", cbTotal / dwElem);
            }
            Console.Write("\n");
            //fputwc(L'\n',pFileout);
        }
        
        // Print the FPO info for a given symbol;
        public void PrintFrameData(IDiaFrameData pFrameData)
        {
            uint dwSect;
            uint dwOffset;
            uint cbBlock;
            uint cbLocals;                      // Number of bytes reserved for the function locals
            uint cbParams;                      // Number of bytes reserved for the function arguments
            uint cbMaxStack;
            uint cbProlog;
            uint cbSavedRegs;
            int bSEH;
            int bEH;
            int bStart;

            if (((dwSect = pFrameData.addressSection) != null) &&
                ((dwOffset = pFrameData.addressOffset) != null) &&
                ((cbBlock = pFrameData.lengthBlock) != null) &&
                ((cbLocals = pFrameData.lengthLocals) != null) &&
                ((cbParams = pFrameData.lengthParams) != null) &&
                ((cbMaxStack = pFrameData.maxStack) != null) &&
                ((cbProlog = pFrameData.lengthProlog) != null) &&
                ((cbSavedRegs = pFrameData.lengthSavedRegisters) != null) &&
                ((bSEH = pFrameData.systemExceptionHandling) != null) &&
                ((bEH = pFrameData.cplusplusExceptionHandling) != null) &&
                ((bStart = pFrameData.functionStart) != null))
            {

                Console.Write(String.Format("{0}:{1}   {2} {3} {4} {5} {6} {7} {8}   {9}   {10}",
                      dwSect, dwOffset, cbBlock, cbLocals, cbParams, cbMaxStack, cbProlog, cbSavedRegs,
                      (bSEH != 0 ? 'Y' : 'N'),
                      (bEH != 0 ? 'Y' : 'N'),
                      (bStart != 0 ? 'Y' : 'N')));
                //fwprintf(pFileout,L"%04X:%08X   %8X %8X %8X %8X %8X %8X %c   %c   %c",
                //        dwSect, dwOffset, cbBlock, cbLocals, cbParams, cbMaxStack, cbProlog, cbSavedRegs,
                //        bSEH ? L'Y' : L'N',
                //        bEH ? L'Y' : L'N',
                //        bStart ? L'Y' : L'N');

                string stringProgram;

                if ((stringProgram = pFrameData.program) != null)
                {

                    Console.Write(String.Format(" {0}", stringProgram));
                    //fwprintf(pFileout,L" %s", stringProgram);
                }

                Console.Write("\n");
                //fputwc(L'\n',pFileout);
            }
        }

        // Print all the valid properties associated to a symbol
        #region cannt be updated
        //public void PrintPropertyStorage(IDiaPropertyStorage *pPropertyStorage)
        //{
        //  IEnumSTATPROPSTG *pEnumProps;
        
        //  if (SUCCEEDED(pPropertyStorage->Enum(&pEnumProps))) {
        //    STATPROPSTG prop;
        //    uint celt = 1;
        
        //    while (SUCCEEDED(pEnumProps->Next(celt, &prop, &celt)) && (celt == 1)) {
        //      PROPSPEC pspec = { PRSPEC_PROPID, prop.propid };
        //      PROPVARIANT vt = { VT_EMPTY };
        
        //      if (SUCCEEDED(pPropertyStorage->ReadMultiple(1, &pspec, &vt))) {
        //        switch (vt.vt) {
        //          case VT_bool:
        //            fwprintf(pFileout,L"%32s:\t %s\n", prop.lpwstrName, vt.bVal ? L"true" : L"false");
        //            break;
        
        //          case VT_I2:
        //            fwprintf(pFileout,L"%32s:\t %d\n", prop.lpwstrName, vt.iVal);
        //            break;
        
        //          case VT_UI2:
        //            fwprintf(pFileout,L"%32s:\t %u\n", prop.lpwstrName, vt.uiVal);
        //            break;
        
        //          case VT_I4:
        //            fwprintf(pFileout,L"%32s:\t %d\n", prop.lpwstrName, vt.intVal);
        //            break;
        
        //          case VT_UI4:
        //            fwprintf(pFileout,L"%32s:\t 0x%0X\n", prop.lpwstrName, vt.uintVal);
        //            break;
        
        //          case VT_UI8:
        //            fwprintf(pFileout,L"%32s:\t 0x%X\n", prop.lpwstrName, vt.uhVal.QuadPart);
        //            break;
        
        //          case VT_string:
        //            fwprintf(pFileout,L"%32s:\t %s\n", prop.lpwstrName, vt.stringVal);
        //            break;
        
        //          case VT_UNKNOWN:
        //            fwprintf(pFileout,L"%32s:\t %p\n", prop.lpwstrName, vt.punkVal);
        //            break;
        
        //          case VT_SAFEARRAY:
        //            break;
        //        }
        
        //        VariantClear((VARIANTARG *) &vt);
        //      }
        
        //      SysFreeString( prop.lpwstrName );
        //    }
        
        //    pEnumProps->Release();
        //  }
        //}
        #endregion

    }
}
