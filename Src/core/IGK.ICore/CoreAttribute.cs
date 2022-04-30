

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreAttribute.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:CoreAttribute.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
namespace IGK.ICore
{
    /// <summary>
    /// represent the base attribute class
    /// </summary>
    public class CoreAttribute : Attribute 
    {
        public CoreAttribute()
        {
        }
        public static bool IsDefined(System.Reflection.PropertyInfo pr)
        {
            object[] tb = pr.GetCustomAttributes(typeof(CoreAttribute), true);
            if (tb.Length > 0)
                return true;
            Type t = pr.DeclaringType;
            Type[] interfaces = t.GetInterfaces();
            System.Reflection.PropertyInfo prInfo = null;
            for (int i = 0; i < interfaces.Length; i++)
            {
                prInfo = interfaces[i].GetProperty(pr.Name);
                if (prInfo != null)
                {
                    object[] obj = prInfo.GetCustomAttributes(typeof(CoreAttribute), false);
                    if (obj.Length == 1)
                    {
                        return true;
                    }
                }
            }
            return false;
        }


        public static bool IsDefined(object targetObject, PropertyInfo propertyInfo, Type attribute)
        {
            object[] tb = propertyInfo.GetCustomAttributes(attribute, true);
            if (tb.Length > 0)
                return true;
            Type t = null;
            if (targetObject != null)
                t = targetObject.GetType();
            else
                t = propertyInfo.DeclaringType;
            Type[] interfaces = t.GetInterfaces();
            System.Reflection.PropertyInfo prInfo = null;
            for (int i = 0; i < interfaces.Length; i++)
            {
                prInfo = interfaces[i].GetProperty(propertyInfo.Name);
                if (prInfo != null)
                {
                    object[] obj = prInfo.GetCustomAttributes(attribute, false);
                    if (obj.Length == 1)
                    {
                        return true;
                    }
                }
            }
            return false;
        }


        public  static T GetCustomAttribute<T>(PropertyInfo prInfo) where T : class
        {
            return GetCustomAttribute(prInfo, typeof(T)) as T;
        }
    }
}

