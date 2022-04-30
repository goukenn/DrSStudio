

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GLUT.cs
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
file:GLUT.cs
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
using System.Runtime.InteropServices;
namespace IGK.GLLib
{
    /// <summary>
    /// reprenet a glut wrapper
    /// </summary>
    public static class GLUT 
    {
        [DllImport ("glut32.dll")]
        public static extern void glutSolidSphere(double size, int slice, int nice);
        [DllImport("glut32.dll")]
        public static extern void glutWireSphere(double size, int slice, int nice);
        [DllImport("glut32.dll")]
        public static extern void glutSolidTorus( double innerradius, double outerradius, int nsides, int rings);
        [DllImport("glut32.dll")]
        public static extern void glutWireTorus(double innerradius, double outerradius, int nsides, int rings);
        [DllImport("glut32.dll")]
        public static extern void glutWireTetrahedron();
        [DllImport("glut32.dll")]
        public static extern void glutSolidTetrahedron();
        [DllImport("glut32.dll")]
        public static extern void glutWireIcosahedron();
        [DllImport("glut32.dll")]
        public static extern void glutSolidIcosahedron();
        [DllImport("glut32.dll")]
        public static extern void glutWireOctahedron();
        [DllImport("glut32.dll")]
        public static extern void glutSolidOctahedron();
        [DllImport("glut32.dll")]
        public static extern void glutWireDodecahedron();
        [DllImport("glut32.dll")]
        public static extern void glutSolidDodecahedron();
        [DllImport("glut32.dll")]
        public static extern void glutWireTeapot(double  size);
        [DllImport("glut32.dll")]
        public static extern void glutSolidTeapot(double  size);
        [DllImport("glut32.dll")]
        public static extern void glutSolidCone(double radius, double height, int slices, int stacks);
        [DllImport("glut32.dll")]
        public static extern void glutWireCone(double radius, double height, int slices, int stacks);
        [DllImport("glut32.dll")]
        public static extern void glutWireCube(double size);
        [DllImport("glut32.dll")]
        public static extern void glutSolidCube(double size);
        [DllImport("glut32.dll")]
        public static extern void glutSetColor(int index, float r, float g, float b);
        //public static void glutSolidCylinder(double radius, double height, int slices, int stacks) { 
        //    IntPtr qobj = GLU.gluNewQuadric ();
        //    GLU.gluQuadricDrawStyle (qobj,GLU.GLU_FILL );
        //    GLU.gluCylinder(qobj,radius,radius,height,slices,stacks);
        //    GLU.gluDeleteQuadric(qobj);    
        //}
        //public static void glutWireCylinder(double radius, double height, int slices, int stacks) {
        //    IntPtr qobj = GLU.gluNewQuadric();
        //    GLU.gluQuadricDrawStyle(qobj, GLU.GLU_LINE);
        //    GLU.gluCylinder(qobj, radius, radius, height, slices, stacks);
        //    GLU.gluDeleteQuadric(qobj);    
        //}
    }
}

