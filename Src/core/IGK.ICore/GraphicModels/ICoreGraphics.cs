

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreGraphics.cs
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
file:ICoreGraphics.cs
*/
using IGK.ICore;using IGK.ICore.Drawing2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.ICore.GraphicModels
{
    /// <summary>
    /// represent a basics graphics 
    /// </summary>
    public interface ICoreGraphics : 
        IDisposable ,
        ICoreWorkingObjectVisitor 
    {
        enuSmoothingMode SmoothingMode { get; set; }
        enuInterpolationMode InterpolationMode { get; set; }
        enuCompositingMode CompositingMode { get; set; }
        enuTextRenderingMode TextRenderingMode { get; set; }
        Rectanglef ClipBounds { get; }
        void SetClip(Rectanglef rectangle);
        void ResetClip();
        void IntersectClip(Rectanglef rectangle);
        void ExcludeClip(Rectanglef rectangle);


        void ResetTransform();
        void RotateTransform(float angle, enuMatrixOrder order);
        void TranslateTransform(float dx, float dy, enuMatrixOrder order);
        void ScaleTransform(float ex, float ey, enuMatrixOrder order);
        void MultiplyTransform(Matrix matrix, enuMatrixOrder order);
        /// <summary>
        /// return the current new cloned matrix transform
        /// </summary>
        /// <returns></returns>
        Matrix GetCurrentTransform();
        void Clear(Colorf color);
        /// <summary>
        /// accept the working object
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        bool Accept(ICoreWorkingObject obj);
        void Flush();
        void Draw(ICoreWorkingObject d, bool proportional, Rectanglei rectangle, enuFlipMode flipMode);
        void FillRectangle(ICoreBrush brush, float x, float y, float width, float height);
        void FillRectangle(Colorf color, Rectanglef rectangle);
        void FillRectangle(Colorf color, float x, float y, float width, float height);
        void DrawRectangle(ICorePen pen, float x, float y, float width, float height);
        void DrawRectangle(Colorf  color, float x, float y, float width, float height);
        void DrawRectangle(Colorf color, float penwidth, enuDashStyle style, float x, float y, float width, float height);
        void FillEllipse(ICoreBrush brush, float x, float y, float width, float height);
        void FillEllipse(Colorf color, float x, float y, float width, float height);
        void FillPolygon(Colorf colorf, Vector2f[] t);
        void DrawEllipse(Colorf color, float x, float y, float width, float height);
        void DrawEllipse(ICorePen pen, float x, float y, float width, float height);
        void DrawLine(Colorf color, float x1, float y1, float x2, float y2);
        void DrawLine(Colorf color, float penwidth, enuDashStyle style, float x1, float y1, float x2, float y2);
        void DrawLine(ICorePen  color, float x1, float y1, float x2, float y2);
        void DrawLine(ICorePen color, Vector2f start, Vector2f end);
        void DrawLine(Colorf color, Vector2f start, Vector2f end);
        /// <summary>
        /// get the object state
        /// </summary>
        /// <returns></returns>
        object Save();
        void Restore(object state);
        void SetupGraphicsDevice(ICoreWorkingObject obj);
        /// <summary>
        /// release resource without disposing
        /// </summary>
        void Release();
        enuPixelOffset PixelOffsetMode { get; set; }
        //void FillPath(Colorf colorf, ICoreGraphicsPath p);
        void FillPath(Colorf colorf, CoreGraphicsPath p);
        void DrawPath(Colorf colorf, CoreGraphicsPath p);
        void FillPath(ICoreBrush br, CoreGraphicsPath p);
        void DrawPath(ICorePen pen, CoreGraphicsPath p);

        void Draw(ICore2DDrawingObject obj);
        /// <summary>
        /// draw core bitmap 
        /// </summary>
        /// <param name="inBmp">bitmap to draw </param>
        /// <param name="rectanglei">retanglei</param>
        void Draw(ICoreBitmap inBmp, Rectanglei rectanglei);

        /// <summary>
        /// draw string at the position
        /// </summary>
        /// <param name="text"></param>
        /// <param name="font"></param>
        /// <param name="color"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        void DrawString(string text, ICoreFont font, Colorf color, float x, float y);
        /// <summary>
        /// draw string 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="font"></param>
        /// <param name="coreBrush"></param>
        /// <param name="rectangle"></param>
        void DrawString(string text, ICoreFont font, ICoreBrush coreBrush, Rectanglef rectangle);
        /// <summary>
        /// draw string
        /// </summary>
        /// <param name="text">text to render</param>
        /// <param name="coreFont">font</param>
        /// <param name="coreBrush">brush used</param>
        /// <param name="x">x position</param>
        /// <param name="y">y position</param>
        void DrawString(string text, ICoreFont coreFont, ICoreBrush coreBrush, float x, float y);
        /// <summary>
        /// draw string
        /// </summary>
        /// <param name="text"></param>
        /// <param name="font"></param>
        /// <param name="Brush"></param>
        /// <param name="bound"></param>
        void DrawString(string text,
            CoreFont font,
            ICoreBrush Brush,
            Rectanglef bound
            );

        /// <summary>
        /// draw string
        /// </summary>
        /// <param name="text"></param>
        /// <param name="font"></param>
        /// <param name="Brush"></param>
        /// <param name="bound"></param>
        void DrawString(string text,
            ICoreFont font,
            ICoreBrush Brush,
            Rectanglef bound,
            enuTextTrimming trimming,
            bool isMnemocic
            );
        /// <summary>
        /// draw the string to the targetr bounds
        /// </summary>
        /// <param name="text">text to draw</param>
        /// <param name="font">font definition used to draw</param>
        /// <param name="color">color</param>
        /// <param name="bounds">bound</param>
        void DrawString(string text, CoreFont font, Colorf color, Rectanglef bounds);

        ICoreBitmap Copy(Rectanglei rectanglei);

        ICoreBitmap CopyFromScreen( Rectanglei rc, enuCopyMode mode);

        void DrawRectangle(Colorf colorf, Rectanglef brc);
        void DrawCurve(Colorf color, IEnumerable<IVector2f> points);
    }
}

