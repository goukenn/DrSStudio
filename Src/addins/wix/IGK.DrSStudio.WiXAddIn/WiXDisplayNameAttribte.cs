

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WiXDisplayNameAttribte.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
using IGK.ICore;
using IGK.ICore.Codec;
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
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:WiXDisplayNameAttribte.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.WiXAddIn
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class , AllowMultiple=false, Inherited=false)]
    public class WiXDisplayNameAttribte: Attribute 
    {
        private string m_DisplayName;
        private string m_URI;
        /// <summary>
        /// get or set the uri
        /// </summary>
        public string URI
        {
            get { return m_URI; }
            set
            {
                if (m_URI != value)
                {
                    m_URI = value;
                }
            }
        }
        public string DisplayName
        {
            get { return m_DisplayName; }
            set
            {
                if (m_DisplayName != value)
                {
                    m_DisplayName = value;
                }
            }
        }
        public WiXDisplayNameAttribte(string displayname)
        {
            this.m_DisplayName = displayname;
        }
        public  static string GetName(System.Reflection.PropertyInfo prInfo)
        {
            WiXDisplayNameAttribte t = GetCustomAttribute(prInfo, MethodInfo.GetCurrentMethod().DeclaringType) as WiXDisplayNameAttribte;
            if (t != null)
            {                
                return t.DisplayName;
            }
            return prInfo.Name;
        }
        public static string GetName(Type type)
        {
              WiXDisplayNameAttribte t = GetCustomAttribute(type, MethodInfo.GetCurrentMethod().DeclaringType) as WiXDisplayNameAttribte;
              if (t != null)
              {
                  return t.DisplayName;
              }
              return type.Name;

        }
        internal static string GetName(Type type, WiXWriter writer)
        {
            WiXDisplayNameAttribte t = GetCustomAttribute(type, MethodInfo.GetCurrentMethod().DeclaringType) as WiXDisplayNameAttribte;
            if (t != null)
            {
                if (string.IsNullOrEmpty(t.URI))
                {
                    return t.DisplayName;
                }
                return writer.GetNameSpacePrefix(t.URI )+":"+t.DisplayName;
            }
            return type.Name;
        }
    }
}

