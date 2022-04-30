

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GLVersion.cs
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
file:GLVersion.cs
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
/* This file is part of IGK-DRAWING SOFT.
*    IGK-DRAWING FOFT is free software: you can redistribute it and/or modify
*    it under the terms of the GNU General Public License as published by
*    the Free Software Foundation, either version 3 of the License, or
*    (at your option) any later version.
*    IGK-DRAWING FOFT is distributed in the hope that it will be useful,
*    but WITHOUT ANY WARRANTY; without even the implied warranty of
*    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
*    GNU General Public License for more details.
*    You should have received a copy of the GNU General Public License
*    along with IGK-DRAWING SOFT.  If not, see <http://www.gnu.org/licenses/>.
*
*    Copyright (c) 2008-2009 
*    Author : C.A.D. BONDJE DOUE
*    mail : bondje.doue@hotmail.com
*/
//GLVERSION.CS 
//FAIT PARTIE DE L'IMPLEMENTATION D'OPENGL DE LA LIBRAIRIE GKOPENGL
//GERE LA VERSION ASSOCIE 
//-----------------------------------------------------------------------------------------------
//AUTEUR : BONDJE DOUE CHARLES 
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
namespace IGK.GLLib
{
    /// <summary>
    /// Represent a gl Version
    /// </summary>
    public class GLVersion : IComparer 
    {
        internal int major;
        internal int minor;
        internal int revision;
        internal static GLVersion V_1_1;
        internal static GLVersion V_1_2;
        internal static GLVersion V_1_3;
        internal static GLVersion V_1_4;
        internal static GLVersion V_1_5;
        internal static GLVersion V_2_0;
        internal static GLVersion V_2_1;
        internal static GLVersion V_3_0;
        //CONSTRUCTEUR STATIC
        static GLVersion()
        {
            //initialisation des version par d�faut 
            V_1_1 = new GLVersion("1.1");
            V_1_2 = new GLVersion("1.2");
            V_1_3 = new GLVersion("1.3");
            V_1_4 = new GLVersion("1.4");
            V_1_5 = new GLVersion("1.5");
            V_2_0 = new GLVersion("2.0");
            V_2_1 = new GLVersion("2.1");
            V_3_0 = new GLVersion("3.0");            
        }
        public override string ToString()
        {
            //chaine de caract�re � retourner
            return "OpenGL."+ major + "." + minor;
        }
       //cr�ation d'un numero de version � partir d'une chaine
        public GLVersion(string version)
        {
            string[] tab = version.Split('.');
            if (tab.Length >= 2)
            {
                major = Convert.ToInt32(tab[0]);
                minor = Convert.ToInt32(tab[1]);
                revision = 0;
            }
        }
        #region IComparer Members
        public int Compare(object x, object y)
        {
            GLVersion obj1 = (GLVersion)x;
            GLVersion obj2 = (GLVersion)y;
            int v_result = 0;
            if (obj1.major > obj2.major)
                v_result = 1;
            else {
                if (obj1.major == obj2.major)
                {
                    if (obj1.minor > obj2.minor)
                        v_result = 1;
                    else if (obj1.minor < obj2.minor)
                        v_result = -1;
                }
            }
            return v_result;
        }
        #endregion
        //Operateur sur les versions
        public static bool operator <(GLVersion v1, GLVersion v2)
        {
            int i = v1.major << 8 + v1.minor;
            int j = v2.major << 8 + v2.minor;
            return (i < j);
        }
        public static bool operator <=(GLVersion v1, GLVersion v2)
        {
            int i = ((v1.major << 8) + v1.minor);
            int j = ((v2.major << 8) + v2.minor);
            return (i <= j);
        }
        public static bool operator >=(GLVersion v1, GLVersion v2)
        {
            int i = ((v1.major << 8) + v1.minor);
            int j = ((v2.major << 8) + v2.minor);
            return (i >= j);
        }
        public static bool operator >(GLVersion v1, GLVersion v2)
        {         
            return !(v1 < v2);
        }
        public static bool operator ==(GLVersion v1, GLVersion v2)
        {
            int i = v1.major << 8 + v1.minor;
            int j = v2.major << 8 + v2.minor;
            return (i == j);
        }
        public static bool operator !=(GLVersion v1, GLVersion v2)
        {
            return !(v1 == v2);
        }
        public override bool Equals(object obj)
        {
            return (this == (GLVersion)obj);
        }
        //fin operateur sur les vesions
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}

