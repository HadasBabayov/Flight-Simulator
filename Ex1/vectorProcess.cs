using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace Ex1
{
    class vectorProcess
    {
        [DllImport("dllTest2.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr Create(float name);


        [DllImport("dllTest2.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Add(IntPtr v ,float x);


        [DllImport("dllTest2.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Clear(IntPtr v);


        [DllImport("dllTest2.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern float GetValue(IntPtr v);

    }
}
