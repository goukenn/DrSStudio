

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreCodecTypeAttribute.cs
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
file:CoreCodecTypeAttribute.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.ICore.Codec
{
    [AttributeUsage (AttributeTargets.Class , Inherited=false , AllowMultiple = false )]
    /// <summary>
    /// represent a ICore Codec Type Attribute. used to register drsstudio codec type 
    /// </summary>
    public class CoreCodecTypeRendererAttribute : CoreAttribute 
    {
        private string m_Name;
        public string Name
        {
            get { return m_Name; }
            set
            {
                if (m_Name != value)
                {
                    m_Name = value;
                }
            }
        }
        /// <summary>
        /// .ctr
        /// </summary>
        /// <param name="name">Name of the codec type</param>
        public CoreCodecTypeRendererAttribute(string name)
        {
            this.Name = name;
        }
    }
}

