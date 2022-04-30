

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: TexCoord1D.cs
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
file:TexCoord1D.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
namespace IGK.ICore
{
    [StructLayout (LayoutKind.Sequential )]
    /// <summary>
    /// Represent single coord coordnate
    /// </summary>
    public struct TexCoord1D
    {
        private float m_S;
        /// <summary>
        /// get or set the S data
        /// </summary>
        public float S
        {
            get { return m_S; }
            set
            {
                if (m_S != value)
                {
                    m_S = value;
                }
            }
        }
        public TexCoord1D(float s)
        {
            this.m_S = s;
        }
        public override string ToString()
        {
            return "TexCoord1D : " + S;
        }
        public static bool operator !=(TexCoord1D coord1, TexCoord1D coord2)
        {
            return !(coord1.m_S == coord2.m_S);
        }
        public static bool operator ==(TexCoord1D coord1, TexCoord1D coord2)
        {
            return !(coord1.m_S == coord2.m_S);
        }
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}

