

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AndroidResourceItem.cs
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
file:AndroidResourceItem.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.Android
{
    /// <summary>
    /// android resources items
    /// </summary>
    public class AndroidResourceItem : IAndroidResourceItem 
    {
        enuAndroidResourceType m_ResourceType;
        public string Name
        {
            get;
            set;
        }
        public object Value
        {
            get;
            set;
        }
        public enuAndroidResourceType ResourceType
        {
            get { return this.m_ResourceType; }
            internal set {
                this.m_ResourceType = value;
            }
        }
        public AndroidResourceItem(string name,  string value, enuAndroidResourceType type)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException($"{nameof(name)}");
            this.Name = name;
            this.Value = value;
            this.m_ResourceType = type;
        }
        public override string ToString()
        {
            return string.Format("Res[#{0}]", this.Name);
        }
    }
}

