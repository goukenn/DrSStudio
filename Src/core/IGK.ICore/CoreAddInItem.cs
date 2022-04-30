

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreAddInItem.cs
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
file:CoreAddInItem.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime .InteropServices ;
using System.Reflection ;
using System.IO;
namespace IGK.ICore
{
    [Serializable ()]
    public class CoreAddInItem : MarshalByRefObject ,  ICoreAddIn 
    {
        [NonSerialized]
        private CoreAddInAttribute m_attrib;
        private Assembly m_assembly;
        private bool m_isVital;
        private string m_location;
        public CoreAddInAttribute Attribute { get { return this.m_attrib;  } }
        public CoreAddInItem(CoreAddInAttribute attrib, Assembly assembly)
        {
            this.m_attrib = attrib;
            this.m_assembly = assembly;
            this.m_location = assembly.Location;
        }
        #region ICoreAddIn Members
        public bool IsVital
        {
            get { return this.m_isVital; }
            internal set { this.m_isVital = value; }
        }
        /// <summary>
        /// get the assemblye
        /// </summary>
        public Assembly Assembly
        {
            get { return this.m_assembly; }
        }
        /// <summary>
        /// get the location
        /// </summary>
        public string Location {
            get { return this.m_location;  }
        }
        #endregion

        public override string ToString()
        {
            return base.ToString() + " ["+Path.GetFileName (this.Location )+"] ";
        }

        public string FriendlyName
        {
            get { return this.m_attrib.FriendlyName; }
        }
    }
}

