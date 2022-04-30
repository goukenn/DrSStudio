

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: vect3f.cs
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
file:vect3f.cs
*/
/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igkdev.be
Application : DrSStudio
powered by IGK - DEV &copy; 2008-2011
THIS FILE IS A PART OF THE DRSSTUDIO APPLICATION. SEE "License.txt"
FOR MORE INFORMATION ABOUT THE LICENSE
------------------------------------------------------------------- 
*/
/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igk-dev.be
App : DrSStudio
powered by IGK - DEV 2008-2011
------------------------------------------------------------------- 
*/
/* This file is part of iGK-DrawingSoft.
*    iGK-DrawingSoft is free software: you can redistribute it and/or modify
*    it under the terms of the GNU Lesser General Public License as published by
*    the Free Software Foundation, either version 3 of the License, or
*    (at your option) any later version.
*    iGK-DrawingSoft is distributed in the hope that it will be useful,
*    but WITHOUT ANY WARRANTY; without even the implied warranty of
*    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
*    GNU General Public License for more details.
*    You should have received a copy of the GNU Lesser General Public License
*    along with IGK-DRAWING SOFT.  If not, see <http://www.gnu.org/licenses/>.
*
*    Copyright (c) 2008-2009 
*    Author : C.A.D. BONDJE DOUE
*    mail : bondje.doue@hotmail.com
*/
﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Drawing;
namespace IGK.GLLib
{
    [TypeConverter(typeof(vecfTypeConverter))]
    [StructLayout(LayoutKind.Sequential)]
    public struct vect3f
    {
        internal vect2f vec2f;
        internal float m_z;
        public float X { get { return vec2f.m_x; } set { vec2f.m_x = value; } }
        public float Y { get { return vec2f.m_y; } set {vec2f. m_y = value; } }
        public float Z { get { return m_z; } set { m_z = value; } }
        public  static vect3f Empty;
        static vect3f()
        {
            Empty = new vect3f(0.0f, 0.0f,0.0f);
        }
        public vect3f(float x, float y, float z)
        {
            vec2f = new vect2f(x, y);
            m_z = z;
        }
        public override string ToString()
        {
            return string.Format ("X:{0} Y:{1} Z:{2}",X,Y,Z) ;
        }
        public static implicit operator vect3f(Point point)
        {
            return new vect3f(point.X, point.Y, 0);
        }
        public static implicit operator Point(vect3f point)
        {
            return new Point((int)point.X,(int) point.Y);
        }
    }
}

