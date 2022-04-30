

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WPFElementAttribute.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV - [2008 - 2014]
author: C.A.D . BONDJE DOUE
file:WPFElementAttribute.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.WPFSurfaceAddIn
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple=false , Inherited=false )]
    /// <summary>
    /// reprent the base wpf element
    /// </summary>
    public class WPFElementAttribute : 
        CoreWorkingObjectAttribute ,
        ICoreWorkingGroupObjectAttribute
    {
        private Type m_Mecanism;
        private bool m_isVisible;
        private string m_CursorKey;
        string m_GroupImageKey;
        public string GroupImageKey
        {
            get { return this.m_GroupImageKey; }
            set { this.m_GroupImageKey = value; }
        }
        public string CursorKey
        {
            get { return m_CursorKey; }
            set
            {
                if (m_CursorKey != value)
                {
                    m_CursorKey = value;
                }
            }
        }
        public Type MecanismType{
                get{return m_Mecanism;}
        }
        public WPFElementAttribute(string name, Type mecanism):base(
            "WPF"+name) 
        {
            this.m_Mecanism = mecanism;
            this.IsVisible = true;
            this.ImageKey = "DE_"+name;
            this.CursorKey = "cross_" + name;
            this.GroupImageKey = "WPFGroup";
        }
        #region ICoreWorkingGroupObjectAttribute Members
        public virtual  string GroupName
        {
            get { return WPFConstant.DEFAULT_GROUP; }
        }
        public string Environment
        {
            get { return WPFConstant.SURFACE_ENVIRONMENT; }
        }
        public bool IsVisible
        {
            get
            {
                return m_isVisible;
            }
            set
            {
                this.m_isVisible = value;
            }
        }
        public System.Windows.Forms.Keys Keys
        {
            get;
            set;
        }
        #endregion
    }
}

