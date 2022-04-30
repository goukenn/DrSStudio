

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreGraphicsPath.cs
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
file:ICoreGraphicsPath.cs
*/
using IGK.ICore;using IGK.ICore.Drawing2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.GraphicModels
{
    public interface ICoreGraphicsPath : IDisposable , ICloneable 
    {
        ICorePen StrokeBrush { get; }
        ICoreBrush FillBrush { get; }
        Vector2f[] PathPoints { get; }
        Byte[] PathTypes { get; }
        void Reset();
        bool IsVisible(Vector2f point);
        bool IsVisible(Vector3f point);
        bool IsOutilineVisible(Vector2f point);
        bool GetAllDefinition(out Vector2f[] points, out byte[] types);
        void Transform(Matrix matrix);
        //void SetMarker();
        //void SetMarker(ICoreBrush fillBrush, ICorePen strokeBrush);
        //void ResetAllMarker();
        /// <summary>
        /// invort orientation of this graphics path
        /// </summary>
        void Invert();
        /// <summary>
        /// get the segment collections
        /// </summary>
        ICoreGraphicsSegmentCollections Segments { get; }
        /// <summary>
        /// get or set if this segment is empty
        /// </summary>
        bool IsEmpty { get; }
        /// <summary>
        /// Get or set the global fill mode
        /// </summary>
        enuFillMode FillMode { get; set; }
        /// <summary>
        /// get or set the global tension
        /// </summary>
        float Tension { get; set; }
        void Add(ICoreGraphicsPath path);
        void AddLines(Vector2f[] tab);
        void AddLine(float x1, float y1, float x2, float y2);
        void AddLine(float x1, float y1, float z1, float x2, float y2, float z2);
        /// <summary>
        /// add Line
        /// </summary>
        /// <param name="startPoint"></param>
        /// <param name="endPoint"></param>
        void AddLine(Vector2f startPoint, Vector2f endPoint);
        /// <summary>
        /// add Line in 3d model collection
        /// </summary>
        /// <param name="startPoint"></param>
        /// <param name="endPoint"></param>
        void AddLine(Vector3f startPoint, Vector3f endPoint);
        /// <summary>
        /// add rectangle
        /// </summary>
        /// <param name="rectangle"></param>
        void AddRectangle(Rectanglef rectangle);
        void AddRectangle(int x, int y, int height, int width);
        void AddRectangle(float x, float y, float height, float width);


        void AddText(string text, Rectanglef bound, CoreFont font);
        void AddText(string text, string fontName, float fontSize, int fontStyle, Vector2f location);

        /// <summary>
        /// add ellipse
        /// </summary>
        /// <param name="center"></param>
        /// <param name="radius"></param>
        void AddEllipse(Vector2f center, Vector2f radius);
        void AddEllipse(Rectanglef rectangle);
        /// <summary>
        /// add arc
        /// </summary>
        /// <param name="center"></param>
        /// <param name="radius"></param>
        /// <param name="startAngle"></param>
        /// <param name="sweepAngle"></param>
        void AddArc(Vector2f center, Vector2f radius, float startAngle, float sweepAngle, bool closed);
        void AddPolygon(Vector2f[] points);
        void AddPolygon(Vector3f[] points);
        void AddDefinition(Vector2f[] points, byte[] types);
        void AddPie(Rectanglef bound, float StartAngle, float sweepAngle);
        Rectanglef GetBounds();
        void CloseAllFigures();
    }
}

