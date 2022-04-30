

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CorePathSegmentBase.cs
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
file:CorePathSegmentBase.cs
*/
using IGK.ICore;using IGK.ICore.GraphicModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.ICore.Drawing2D.Segments
{
    public abstract class CorePathSegmentBase : ICoreGraphicsSegment
    {
        public abstract enuSegmentType SegmentType
        {
            get;
        }
        public virtual bool IsEmpty {
            get {
                var P = GetPathPoints();
                return ((P == null)||(P.Length ==0));
            }
        }
        public bool IsOutilineVisible(Vector2f point, float width) {
            ICoreD2DPathUtils c =  CoreApplicationManager.Application.GraphicsPathUtils;
            if (c != null)
            {
             return c.IsOutlineVisible(
                 GetPathPoints(),
                 GetPathTypes(),
                 enuFillMode.Alternate,
                 point,
                 width 
                 );
            }
            return false;
        }
        public abstract Vector2f[] GetPathPoints();
        public abstract byte[] GetPathTypes();
        /// <summary>
        /// transform the current matrix
        /// </summary>
        /// <param name="matrix"></param>
        public abstract void Transform(Matrix matrix);
        /// <summary>
        /// invert the order of this segments
        /// </summary>
        public abstract void Invert();
        public abstract void CloseFigure();

        public abstract void SetPoint(int index, int def);

        public abstract bool IsClosed { get; }
    }
}

