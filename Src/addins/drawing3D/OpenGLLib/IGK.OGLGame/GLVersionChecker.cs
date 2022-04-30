

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GLVersionChecker.cs
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
file:GLVersionChecker.cs
*/
using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.OGLGame
{
    /// <summary>
    /// 
    /// </summary>
    public class GLVersionChecker
    {
        static Version GetGLVersion(string s)
        {
            string[] d = s.Split('.');
            int major = Convert.ToInt32 (d[0]);
            int minor = Convert.ToInt32(d[1]);
            return new Version(major, minor);
        }
        /// <summary>
        /// check if the version is greater or equal to 
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static bool IsGreaterOrEqual(string v)
        {
            try
            {
                Version v_1 = GetGLVersion(v);
                Version v_2 = GetGLVersion(GLLib.GL.Version);
                return v_2 >= v_1;
            }
            catch(Exception)
            { 
            }
            return false;
        }
    }
}

