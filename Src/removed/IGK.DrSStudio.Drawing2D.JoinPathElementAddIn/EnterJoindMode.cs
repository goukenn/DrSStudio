

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: EnterJoindMode.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV - [2008 - 2014]
author: C.A.D . BONDJE DOUE
file:EnterJoindMode.cs
*/
/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igkdev.be
Application : DrSStudio
powered by IGK - DEV &copy; 2008-2012
THIS FILE IS A PART OF THE DRSSTUDIO APPLICATION. SEE "License.txt"
FOR MORE INFORMATION ABOUT THE LICENSE
------------------------------------------------------------------- 
*/
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Drawing2D ;
using System.Drawing;
using System.Windows.Forms;
namespace IGK.DrSStudio.Drawing2D
{
    class EnterJoindMode
    {
        public static GraphicsPath Joint(GraphicsPath[] Path)
        {
            GraphicsPath p = new GraphicsPath();
            for (int i = 0; i < Path.Length ; i++)
			{
                p.AddPath(Path[i], true);
			}
            p.CloseFigure();
            return p;
        }
        public static GraphicsPath Joint(GraphicsPath path1, GraphicsPath path2)
        {
            List<Vector2f> v_points = new List<Vector2f> ();
            List<Byte> v_bytes = new List<byte>();
            v_points.AddRange(path1.PathPoints);
            v_points.AddRange(path2.PathPoints);
            v_bytes.AddRange(path1.PathTypes);
            v_bytes.AddRange(path2.PathTypes);
            GraphicsPath p = new GraphicsPath(v_points .ToArray (), v_bytes .ToArray ());
            return p;
        }
    }
}

