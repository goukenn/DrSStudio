

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreWorkingObjectAttribute.cs
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
file:CoreWorkingObjectAttribute.cs
*/

﻿using IGK.ICore;using IGK.ICore.WorkingObjects;
using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore
{
    /// <summary>
    /// represent the default working object 
    /// </summary>
    [AttributeUsage (AttributeTargets.Class ,
        AllowMultiple=false,
        Inherited=false )]
    public class CoreWorkingObjectAttribute :
        CoreAttribute ,
        ICoreWorkingObjectAttribute 
    {
        private string m_Name;
        private string m_ImageKey;
        private string m_CaptionKey;
        public virtual string NameSpace
        {
            get { return CoreConstant.DRAWING2D_NAMESPACE; }        
        }
        /// <summary>
        /// represent the caption key of this object. for globalization
        /// </summary>
        public string CaptionKey
        {
            get { return m_CaptionKey; }
            set
            {
                if (m_CaptionKey != value)
                {
                    m_CaptionKey = value;
                }
            }
        }
        /// <summary>
        /// get or set the image key of the working object
        /// </summary>
        public virtual string ImageKey
        {
            get { return m_ImageKey; }
            set
            {
                if (m_ImageKey != value)
                {
                    m_ImageKey = value;
                }
            }
        }

        /// <summary>
        /// full qualified name
        /// </summary>
        public string FullName =>$"{NameSpace}/{Name}";
        /// <summary>
        /// unique name of the object in the namespace
        /// </summary>
        public string Name => m_Name;

        public CoreWorkingObjectAttribute(string name)
        {
            this.m_Name = name;
            this.m_CaptionKey = $"DE_{name}";
            this.m_ImageKey = $"DE_{name}_gkds";
        }
        /// <summary>
        /// retrieve the name of a custom attribute type
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string GetObjectName(Type t)
        {
            if (t == null)
                return null;
            CoreWorkingObjectAttribute obj =
           Attribute.GetCustomAttribute(t,
           typeof(CoreWorkingObjectAttribute)) as CoreWorkingObjectAttribute;
            if (obj != null)
                return obj.Name;
            return null;
        }
        /// <summary>
        /// get object name
        /// </summary>
        /// <param name="object"></param>
        /// <returns></returns>
        public static string GetObjectName(object @object)
        {
            if (@object!=null)
                return GetObjectName(@object.GetType());
            return null;
        }
    }
}

