

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WinCoreGraphicsPath.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/

using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;

namespace IGK.ICore.WinCore
{
    ﻿using IGK.ICore.WinCore;
    using IGK.ICore;
    using IGK.ICore.Drawing2D;

    /// <summary>
    /// represent a wincore graphics path
    /// </summary>
    public class WinCoreGraphicsPath : ICoreDrawingPath
    {
        private GraphicsPath m_path;
        public WinCoreGraphicsPath()
        {
            this.m_path = new GraphicsPath();
        }
        public void AddEllipse(Vector2f Center, Vector2f radius)
        {
            float w = Math.Abs (radius.X ) *2;
            float h = Math.Abs (radius.X ) *2;
            this.m_path.AddEllipse(Center.X - w / 2, Center.Y - h / 2, w, h);
        }

        public void GetPathDefinition(out Vector2f[] points, out byte[] Data)
        {
            if (this.m_path.PointCount == 0)
            {
                points = new Vector2f[0];
                Data = new byte[0];
                return;
            }
            points = new Vector2f[this.m_path.PointCount];
            for (int i = 0; i < this.m_path.PointCount; i++)
            {
                points[i] = new Vector2f(this.m_path.PathPoints[i].X,
                     this.m_path.PathPoints[i].Y);
            }
            Data = this.m_path.PathTypes;
        }

        public void Dispose()
        {
            this.m_path.Dispose();
        }


        public void AddArc(Rectanglef rc, float startAngle, float sweepAngle, bool closed)
        {
            if (rc.IsEmpty)
                return;
            this.m_path.AddArc(rc.X, rc.Y, rc.Width, rc.Height, startAngle, sweepAngle);
            if (closed )
            this.m_path.CloseFigure();
        }
    }
}
