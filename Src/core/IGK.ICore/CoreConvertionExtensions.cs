

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreConvertionExtension.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Runtime.InteropServices;
using System.ComponentModel;
using IGK.ICore;using IGK.ICore.ComponentModel;

namespace IGK.ICore
{
    public static class CoreConvertionExtensions
    {
        /// <summary>
        /// Convert the cibling object To Destination. If type the founded type converter allow the convertion
        /// </summary>
        /// <typeparam name="T"> Destination Type</typeparam>
        /// <param name="obj">object to convert</param>
        /// <returns></returns>
        public static T CoreConvertTo<T>(this object obj)
        {
            if (obj == null)
                return default(T);
            Type v_sourceType = obj.GetType();
            TypeConverter t = null;
            if (v_sourceType.IsArray)
            {
                Array c = (Array)obj;
                if (c.Length > 0)
                    t = CoreTypeDescriptor.GetConverter(c.GetValue(0).GetType());
                else
                    return default (T);
            }
            else
                t = CoreTypeDescriptor.GetConverter(obj);
            if (t == null)
                throw new Exception("TypeConvertor not define for " + obj);
            bool canConvert = t.CanConvertTo(typeof(T));
            if ((t != null) && canConvert)
            {
                return (T)t.ConvertTo(obj, typeof(T));
            }
            return default(T);
        }
        /// <summary>
        /// Extension method used to convert object according to TypeConverter from T
        /// </summary>
        /// <typeparam name="T">Type convertor that will be used</typeparam>
        /// <param name="obj">cibling object</param>
        /// <example> Colorf c = global::System.Drawing.Color.FromArgb(33).CoreConvertFrom&gt;Colorf&lt;(); </example>
        /// <returns></returns>
        public static T CoreConvertFrom<T>(this object obj)
        {
            if (obj == null)
                return default(T);
            TypeConverter t = CoreTypeDescriptor.GetConverter(typeof(T));
            if ((t != null) && t.CanConvertFrom(obj.GetType()))
            {
                return (T)t.ConvertFrom(obj);
            }
            return default(T);
        }
    }
}
