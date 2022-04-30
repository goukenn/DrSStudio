

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICore2DScrollableWorkingSurface.cs
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
file:ICore2DScrollableWorkingSurface.cs
*/
using IGK.ICore;using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.ICore.Drawing2D.WinUI
{
    public interface  ICore2DScrollableWorkingSurface : ICoreWorkingSurface, ICoreWorkingScrollableSurface  , ICore2DDrawingScale
    {
        Rectanglei GetDocumentBound();
        /// <summary>
        /// get the display area
        /// </summary>
        Rectanglei DisplayArea { get; }
        /// <summary>
        /// get the zoom x 
        /// </summary>
        float PosX { get; }
        float PosY { get; }
        enuZoomMode ZoomMode { get; set; }

        void SetZoom(float zoomx, float zoomy);
        /// <summary>
        /// raised when zoom mode changed
        /// </summary>
        event EventHandler ZoomModeChanged;
        /// <summary>
        /// get tor the zoom x factor
        /// </summary>
        new float ZoomX { get; }
        /// <summary>
        /// Get the zoom y factor
        /// </summary>
        new float ZoomY { get; }
        /// <summary>
        /// get the zoom in float
        /// </summary>
        /// <returns></returns>
        float GetZoom();
        /// <summary>
        /// event raised when zoom changed. zoomX, zoomY, posX or posY changed.
        /// </summary>
        event EventHandler ZoomChanged;
        /// <summary>
        /// get the screen location. location on display surface
        /// </summary>
        /// <param name="factorLocation"></param>
        /// <returns></returns>
        Vector2f GetScreenLocation(Vector2f factorLocation);
        /// <summary>
        /// get the corresponding zoom location
        /// </summary>
        /// <param name="screenLocation"></param>
        /// <returns></returns>
        Vector2f GetFactorLocation(Vector2f screenLocation);
        /// <summary>
        /// get the zoom rectangle
        /// </summary>
        /// <param name="zoomRectangle"></param>
        /// <returns></returns>
        Rectanglef GetScreenBound(Rectanglef zoomRectangle);
    }
}

