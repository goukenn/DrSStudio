

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: TexCoord2D.cs
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
file:TexCoord2D.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
namespace IGK.ICore
{
    /// <summary>
    /// represent texture 2D coordinate
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct TexCoord2D
    {
        private float m_S;
        private float m_T;
        public float T
        {
            get { return m_T; }
            set
            {
                if (m_T != value)
                {
                    m_T = value;
                }
            }
        }
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
        static readonly TexCoord2D m_Zero;
        static readonly TexCoord2D m_UpX;
        static readonly TexCoord2D m_UpY;
        static readonly TexCoord2D m_UpXY;
        public static TexCoord2D Zero{
            get { return m_Zero; }
        }
        public static TexCoord2D UpX
        {
            get { return m_UpX; }
        }
        public static TexCoord2D UpY
        {
            get { return m_UpY; }
        }
        public static  TexCoord2D UpXY
        {
            get { return m_UpXY; }
        }
    static  TexCoord2D(){
        m_UpX = new TexCoord2D(1, 0);
        m_UpY = new TexCoord2D(0, 1);
        m_UpXY = new TexCoord2D(1, 1);
        }
        public TexCoord2D(float s, float t)
        {
            this.m_S = s;
            this.m_T = t;
        }
        public static bool operator !=(TexCoord2D coord1, TexCoord2D coord2)
        {
            return !((coord1.m_S == coord2.m_S) &&
                (coord1.m_T == coord2.m_T));
        }
        public static bool operator ==(TexCoord2D coord1, TexCoord2D coord2)
        {
            return !((coord1.m_S == coord2.m_S) &&
                (coord1.m_T == coord2.m_T));
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

