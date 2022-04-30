

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GLMatrix4x4f.cs
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
file:GLMatrix4x4f.cs
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
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices ;
namespace IGK.GLLib
{
    [StructLayout(LayoutKind.Sequential )]
    public class GLMatrix4x4f
    {
        private GLMatrix4x4f.GLSMatrix4x4f m_matrix;
        public float M00{get{return m_matrix.m00 ;}}
        public float M01{get{return m_matrix.m01 ;}}
        public float M02{get{return m_matrix.m02 ;}}
        public float M03{get{return m_matrix.m03 ;}}
        public float M10{get{return m_matrix.m10 ;}}
        public float M11{get{return m_matrix.m11 ;}}
        public float M12{get{return m_matrix.m12 ;}}
        public float M13{get{return m_matrix.m13 ;}}
        public float M20{get{return m_matrix.m20 ;}}
        public float M21{get{return m_matrix.m21 ;}}
        public float M22{get{return m_matrix.m22 ;}}
        public float M23{get{return m_matrix.m23 ;}}
        public float M30{get{return m_matrix.m30 ;}}
        public float M31{get{return m_matrix.m31 ;}}
        public float M32{get{return m_matrix.m32 ;}}
        public float M33{get{return m_matrix.m33 ;}}

        public float[] Elements {
            get { 
                IntPtr alloc = Marshal.AllocCoTaskMem (16 * Marshal.SizeOf (typeof(float)));
                Marshal.StructureToPtr (m_matrix, alloc, false );
                float[] o = new float[16];                
                Marshal.Copy (alloc, o,0,o.Length);
                Marshal.FreeCoTaskMem (alloc );
                return o;
            }
        }
        public GLMatrix4x4f ()
        {
            this.Reset();
        }
        public GLMatrix4x4f(float[][] elements)
        {
            if (elements.Length != 4)
                throw new Exception("Invalid Argument");
            m_matrix = new GLSMatrix4x4f();
            m_matrix.m00 = elements[0][0];
            m_matrix.m01 = elements[0][1];
            m_matrix.m02 = elements[0][2];
            m_matrix.m03 = elements[0][3];
            m_matrix.m10 = elements[1][0];
            m_matrix.m11 = elements[1][1];
            m_matrix.m12 = elements[1][2];
            m_matrix.m13 = elements[1][3];
            m_matrix.m20 = elements[2][0];
            m_matrix.m21 = elements[2][1];
            m_matrix.m22 = elements[2][2];
            m_matrix.m23 = elements[2][3];
            m_matrix.m30 = elements[3][0];
            m_matrix.m31 = elements[3][1];
            m_matrix.m32 = elements[3][2];
            m_matrix.m33 = elements[3][3];
        }
        public void Rotate(float angle, float x , float y, float z)
        {
            //
            
        }
        public void Translate(float x, float y, float z)
        {
            GLSMatrix4x4f v_matrix = this.m_matrix ;
            v_matrix.m03 +=x;
            v_matrix.m13 +=y;
            v_matrix.m23 +=z;
            this.m_matrix = v_matrix;
        }
        public void Scale(float x, float y , float z)
        {
            GLSMatrix4x4f v_matrix = this.m_matrix ;
            v_matrix.m00 *=x; 
            v_matrix.m11 *=y;
            v_matrix.m22 *=z;
            this.m_matrix = v_matrix;
        }
        private struct GLSMatrix4x4f
        {
            internal float m00, m01, m02, m03;
            internal float m10, m11, m12, m13;
            internal float m20, m21, m22, m23;
            internal float m30, m31, m32, m33;
        }

        public void Reset()
        {
            m_matrix = new GLSMatrix4x4f();
            //initialisation de la matrix indentit�
            m_matrix.m00 = 1;
            m_matrix.m11 = 1;
            m_matrix.m22 = 1;
            m_matrix.m33 = 1;
        }
    }
}

