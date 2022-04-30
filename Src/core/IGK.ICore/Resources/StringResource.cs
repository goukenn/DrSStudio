

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: StringResource.cs
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
file:StringResource.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.ICore.Resources
{
    public class StringResource : CoreResourceItemBase, ICoreStringResource
    {
        private string m_Value;
        public string Value
        {
            get { return m_Value; }
            set
            {
                if (m_Value != value)
                {
                    m_Value = value;
                }
            }
        }
        public StringResource()
        {
        }
        public override enuCoreResourceType ResourceType
        {
            get { return enuCoreResourceType.String; }
        }
        public override object GetData()
        {
            return this.m_Value;
        }
        public override string GetDefinition()
        {
            return this.m_Value;
        }

        internal protected override void SetValue(string value)
        {
            this.m_Value = value;
        }

        
    }
}

