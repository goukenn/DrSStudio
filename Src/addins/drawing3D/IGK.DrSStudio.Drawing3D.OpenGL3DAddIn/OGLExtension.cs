

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: OGLExtension.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/

using IGK.ICore;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.DrSStudio;
using IGK.DrSStudio.Menu;
using IGK.DrSStudio.WinUI;
using IGK.DrSStudio.Drawing2D;
using IGK.DrSStudio.Drawing2D.WinUI;
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore
{
    public static class OGLExtension
    {
        public static IntPtr GetPinnedAddress(this Array tab)
        {
            if ((tab!=null) && (tab.Length > 0))
                return Marshal.UnsafeAddrOfPinnedArrayElement(tab, 0);
            return IntPtr.Zero;
        }
        public static IntPtr GetPinnedAddress(this Array tab, int index)
        {
            if ((tab != null) && (tab.Length > 0))
                return Marshal.UnsafeAddrOfPinnedArrayElement(tab, index);
            return IntPtr.Zero;
        }

        public static float[] ReadFloatData(this IntPtr buffer, int size)
        { 
            float[] t = new float[size];
            int v_size = Marshal.SizeOf(typeof(float));
            Byte[] tab = new byte[v_size];
            for (int i = 0; i < size; i++)
            {
                t[i] =(float) Marshal.PtrToStructure(IntPtr.Add (buffer, v_size * i), typeof(float));
            }

            return t;
        }
        public static double[] ReadDoubleData(this IntPtr buffer, int size)
        {
            double[] t = new double[size];
            int v_size = Marshal.SizeOf(typeof(double));            
            for (int i = 0; i < size; i++)
            {
                t[i] = (double)Marshal.PtrToStructure(IntPtr.Add (buffer, v_size * i), typeof(double));
            }
            return t;
        }
        /// <summary>
        /// read data in buffer.
        /// </summary>
        /// <typeparam name="T">return type</typeparam>
        /// <param name="buffer">buffer to read</param>
        /// <param name="size">size of data to read</param>
        /// <returns></returns>
        public static T[] ReadData<T>(this IntPtr buffer, int size)
        {
            T[] t = new T[size];
            int v_size = Marshal.SizeOf(typeof(T));
            for (int i = 0; i < size; i++)
            {
                t[i] = (T)Marshal.PtrToStructure(IntPtr.Add (buffer, i * v_size ), typeof(T));
            }
            return t;
        }

        public static T ReadData<T>(this IntPtr buffer)
        {
             return (T)Marshal.PtrToStructure(buffer, typeof(T));
        }
    }
}
