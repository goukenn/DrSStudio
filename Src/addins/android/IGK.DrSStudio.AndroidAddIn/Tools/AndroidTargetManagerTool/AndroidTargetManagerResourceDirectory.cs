

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AndroidTargetManagerResourceDirectory.cs
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
using System.Threading.Tasks;

namespace IGK.DrSStudio.Android.Tools
{
    public class AndroidTargetManagerResourceDirectory
    {
        private Dictionary<string, AndroidTargetManagerResourceItem> m_items;
        private string m_Name;

        public string Name
        {
            get { return m_Name; }
        }
        public AndroidTargetManagerResourceItem this[string key] {
            get {
                return this.m_items[key];
            }
        }
        public AndroidTargetManagerResourceDirectory(string name)
        {
            this.m_Name = name;
            this.m_items = new Dictionary<string, AndroidTargetManagerResourceItem>();
        }
        internal void Register(string name, AndroidTargetManagerResourceItem item)
        {
            if (!this.m_items.ContainsKey (name ))
                this.m_items.Add(name, item);   
        }
    }
}
