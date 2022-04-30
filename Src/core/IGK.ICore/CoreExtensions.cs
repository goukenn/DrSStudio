

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreExtensions.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore;
using IGK.ICore.ComponentModel;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:CoreExtensions.cs
*/
using IGK.ICore.Drawing2D;
using IGK.ICore.Resources;
using IGK.ICore.Settings;
using IGK.ICore.Web;
using IGK.ICore.Web.WinUI;
using IGK.ICore.WinUI;
using IGK.ICore.WinUI.Common;
using IGK.ICore.WinUI.Registrable;
using IGK.ICore.Xml;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;


namespace IGK.ICore
{

    /// <summary>
    /// represent extension and utility fonction 
    /// </summary>
    public static class CoreExtensions
    {

        /// <summary>

        public static T CloneThis<T>(this T clone)
        {
            if (clone is ICloneable)
                return (T)(clone as ICloneable).Clone();
            return default(T);
        }
        public static string RValue(this ICoreXmlGetValueMethod item, string @default = null)
        {
            if (item == null)
                return @default;
            return item.GetValue();
        }
        public static T CoreGetValue<T>(this T[] data, int index, object defaultValue = null)
        {
            if ((data != null) && (index >= 0) && (index < data.Length))
                return data[index];
            return (T)defaultValue;
        }
        /// <summary>
        /// extenstion to convert object to string presentation
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToStringCore(this object obj) {
            return obj?.ToString();
        }



        /// <summary>
        /// used to convert data to other in core system
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static T CoreGetValue<T>(this object data, T defaultValue = default(T))
        {
            if (data == null)
                return defaultValue;
            return data.CoreConvertFrom<T>();
        }
        public static T CoreGetValue<T>(this ICoreSettingValue data, T defaultValue = default(T))
        {
            if (data == null)
                return defaultValue;
            return CoreGetValue<T>(data.Value, defaultValue);
        }
        /// <summary>
        /// get the resources form system
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string R(this string key, params object[] param)
        {
            if (string.IsNullOrEmpty(key))
                return string.Empty;
            return CoreResources.GetString(key, param);
        }

        public static string UCFirst(this string data){
            return char.ToUpper(data[0]) + data.Substring(1).ToLower();
        }
        public static string RFormat(this string key, params object[] param)
        {
            return string.Format(key, param);
        }
        public static bool MoveFront<T>(this IList l, T item)
        {
            if (l.Contains(item))
            {

            }
            return false;
        }
        public static bool MoveBack<T>(this IList l, T item)
        {
            if (l.Contains(item))
            {

            }
            return false;
        }
        public static bool MoveStart<T>(this IList l, T item)
        {
            if (l.Contains(item))
            {

            }
            return false;
        }
        public static bool MoveEnd<T>(this IList l, T item)
        {
            if (l.Contains(item))
            {

            }
            return false;
        }
        public static byte TrimByte(this float x)
        {

            if (x <= 0)
                return 0;
            if (x >= 1)
                return 255;
            return (byte)(x * 255);
        }


        /// <summary>
        /// find attribute in 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="asm"></param>
        /// <returns></returns>
        public static Type FindAttribute<T>(this Assembly asm) where T : class
        {
            if (CoreSystemEnvironment.DesignMode)
                return null;
            if (asm == null)
                return null;
            Type rt = typeof(T);
            T v_attr = default(T);
            foreach (Type v_t in asm.GetTypes())
            {
                v_attr = Attribute.GetCustomAttribute(v_t, rt) as T;
                if (v_attr != null)
                {
                    return v_t;
                }
            }
            return null;
        }
        public static bool ImpletementInterface(this Type t, Type interfaceType)
        {
            if ((t == null) || (interfaceType == null))
                return false;
            return t.GetInterface(interfaceType.FullName, true) != null;
        }

        public static T ParseEnum<T>(this IntPtr v) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new Exception("value must be an enum");
            }
            T c = default(T);
            if (Environment.Is64BitOperatingSystem)
            {
                c = (T)Enum.Parse(typeof(T), v.ToInt64().ToString());
            }
            else
                c = (T)Enum.Parse(typeof(T), v.ToInt32().ToString());
            return c;
        }



        /// <summary>
        /// return a keys form Pointer
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static enuKeys ToKey(this IntPtr v)
        {
            if (Environment.Is64BitOperatingSystem)
            {
                return (enuKeys)v.ToInt64();
            }
            return (enuKeys)v;
        }
        /// <summary>
        /// check if this matrix if valid for drawing
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
        public static bool IsGdiMatrixValid(this Matrix matrix)
        {
            if (matrix.IsIdentity)
                return true;
            if ((matrix.Elements[0] == 0) || (matrix.Elements[3] == 0))
                return false;
            return true;
        }
        public static bool CanAdd(this IList list, object obj)
        {
            if ((obj == null) || (list.Contains(obj)))
                return false;
            return true;
        }
        public static bool CanRemove(this IList list, object obj)
        {
            if ((obj == null) || (!list.Contains(obj)))
                return false;
            return true;
        }
        public static bool IndexExists(this ICollection list, int index)
        {
            return (index >= 0) && (index < list.Count);
        }
        public static bool IndexExists(this Array tab, int index)
        {
            if (tab == null)
                return false;
            return (index >= 0) && (index < tab.Length);
        }
        public static bool IsInRangeOf(this float v, float min, float max)
        {
            return (min <= v) && (max >= v);
        }
        /// <summary>
        /// used to convert child element class to base class array
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tab"></param>
        /// <returns>and empty array if not found</returns>
        public static T[] ConvertTo<T>(this Array tab)
        {
            List<T> t = new List<T>();
            if ((tab == null) || (tab.Length == 0) || !(tab.GetValue(0) is T))
            {
                return t.ToArray();
            }
            Object o = null;
            for (int i = 0; i < tab.Length; i++)
            {
                o = tab.GetValue(i);
                if (o is T)
                {
                    t.Add((T)o);
                }
            }
            return t.ToArray();
        }
        /// <summary>
        /// method used to cast object to requested type
        /// </summary>
        /// <typeparam name="T">to to cast</typeparam>
        /// <param name="item">object to cast</param>
        /// <param name="v">type</param>
        /// <returns></returns>
        public static T Cast<T>(this object item, T v)
        {

            return (T)item;
        }
        /// <summary>
        /// method used to cast object to requested type
        /// </summary>
        /// <typeparam name="item">object to cast</typeparam>
        public static T Cast<T>(this object item)
        {
            if (item == null)
                return default(T);
            return (T)item;
        }
        public static float ToPixel(this string s)
        {
            CoreUnit h = null;
            TypeConverter t = CoreTypeDescriptor.GetConverter(typeof(CoreUnit));
            if (t.CanConvertFrom(typeof(string)))
                h = (CoreUnit)t.ConvertFromString(s);
            else
                h = s;
            return ((ICoreUnitPixel)h).Value;
        }
        public static float ToPoint(this string s)
        {
            CoreUnit h = null;
            TypeConverter t = CoreTypeDescriptor.GetConverter(typeof(CoreUnit));
            if (t.CanConvertFrom(typeof(string)))
                h = (CoreUnit)t.ConvertFromString(s);
            else
                h = s;
            return ((ICoreUnitPoint)h).Value;
        }
        public static float ToPoint(this float s)
        {
            CoreUnit m = (CoreUnit)s.ToString();
            return m.GetValue(enuUnitType.pt);
        }
        public static float ToMm(this int s)
        {
            return ToMm((float)s);
        }
        public static float ToMm(this float s)
        {
            CoreUnit m = (CoreUnit)s.ToString();
            return m.GetValue(enuUnitType.mm);
        }
        public static float ToCm(this float s)
        {
            CoreUnit h = null;
            TypeConverter t = CoreTypeDescriptor.GetConverter(typeof(CoreUnit));
            if (t.CanConvertFrom(typeof(string)))
                h = (CoreUnit)t.ConvertFromString(s.ToString());
            else
                h = s;
            return h.GetValue(enuUnitType.cm);
        }

        public static T1 GetValue<T1>(string p)
        {
            return GetValue<T1>(p, default(T1));
        }

        public static T1 GetValue<T1>(string value, T1 defaultValue)
        {
            Type t = typeof(T1);
            if (t.IsEnum)
            {
                TypeConverter v_conv = CoreTypeDescriptor.GetConverter(typeof(T1));
                if (v_conv != null)
                {
                    try
                    {
                        return (T1)v_conv.ConvertFromString(value);
                    }
                    catch
                    {
                        return defaultValue;
                    }
                }
            }
            return default(T1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="M">Key</typeparam>
        /// <typeparam name="K">type of value</typeparam>
        /// <typeparam name="T">Return type</typeparam>
        /// <param name="dictionary"></param>
        /// <param name="key"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static T GetValue<M, K, T>(this Dictionary<M, K> dictionary, M key, T r = default(T))
        {
            if (dictionary.ContainsKey(key))
            { 
                var g = (object)dictionary[key];
                if (g is T )
                    return (T)g;
                return (T)g.CoreConvertTo<T>();                
            }
            return r;
        }
        public static T GetValue<M,T>(this Dictionary<M, T> dictionary, M key, T r = default(T))
        {
            if (dictionary.ContainsKey(key))
            {
                var g = (object)dictionary[key];
                if (g is T)
                    return (T)g;
                return (T)g.CoreConvertTo<T>();
            }
            return r;
        }


        private static string HexP(int r)
        {
            string g = (r >= 10) ? ((char)(((byte)('A')) + (r - 10))).ToString() : r.ToString();
            return g;
        }
        public static string ToBase(this int d, int @base, int length = -1)
        {
            string o = "";
            if (@base > 0)
            {
                int p = (int)(d / @base);
                int r = d % @base;
                if (p < @base)
                {
                    if (p != 0)
                        o = HexP(p) + "" + HexP(r);
                    else
                        o = HexP(r);
                }
                else
                {
                    o = HexP(r) + o;
                    o = ToBase(p, @base) + o;
                }
                if (length != -1)
                {
                    for (int i = o.Length; i < length; i++)
                    {
                        o = "0" + o;
                    }
                }
            }
            return o;

        }

        static string[] sizes = new string[] { "Bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };
        public static string GetFileSize(this long size)
	    {
		    if (size == 0)
			    return "0 Bytes";		   
            int i = 0;
		    return (Math.Round (size/Math.Pow(1024, (i = (int)Math.Floor(Math.Log(size, 1024)))), 2) + " " + sizes[i]);

	    }


        /// <summary>
        /// copy data byte to co task memory
        /// </summary>
        /// <param name="tb"></param>
        /// <returns></returns>
        /// <remarks>free handle with the Marshel.FreeCoTaskMemory method</remarks>
        public static IntPtr CopyToCoTaskMemory(this byte[] tb) {
            IntPtr c = Marshal.AllocCoTaskMem(tb.Length);
            Marshal.Copy(tb, 0, c, tb.Length);
            return c;

        }
    }
}