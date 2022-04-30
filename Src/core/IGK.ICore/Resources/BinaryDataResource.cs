

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: BinaryDataResource.cs
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
file:BinaryDataResource.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.ICore.Resources
{
    class BinaryDataResource : CoreResourceItemBase 
    {
        private byte[] m_Data;
        public byte[] Data
        {
            get { return m_Data; }
            set { m_Data = value; }
        }
        public override enuCoreResourceType ResourceType
        {
            get { return enuCoreResourceType.BinaryData; }
        }
        public override object GetData()
        {
            return this.m_Data;
        }
        public override string GetDefinition()
        {
            return Convert.ToBase64String(this.m_Data);
        }
        internal protected override  void SetValue(string value)
        {
            this.m_Data = Convert.FromBase64String(value);
        }
    }
}

