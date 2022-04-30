

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKD2DDrawingSelectionLayerFrame.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
using IGK.ICore.Drawing2D;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:IGKD2DDrawingSelectionLayerFrame.cs
*/
using IGK.ICore.GraphicModels;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.ICore.Drawing2D.WinUI
{
    using IGK.ICore;

    /// <summary>
    /// used to render selection element
    /// </summary>
    public class IGKD2DDrawingSelectionLayerFrame :  ICore2DDrawingFrameRenderer, ICore2DDrawingSelectionHost
    {
   private IGKD2DDrawingSurfaceBase.IGKD2DDrawingScene m_scene;
   private ICoreGraphics m_device;
        public IGKD2DDrawingSelectionLayerFrame(IGKD2DDrawingSurfaceBase.IGKD2DDrawingScene scene)
            : base()
        {
            this.m_scene = scene;
        }
        public void Render(ICoreGraphics device)
        {
            //if (!this.m_scene.Focused)
            //    return;
            Rectanglei v_r = Rectanglei.Empty;
            var obj = device.Save();
            device.SmoothingMode = enuSmoothingMode.None;
            this.m_device = device;
            foreach (ICore2DDrawingLayeredElement  item in this.m_scene.CurrentDocument.CurrentLayer.SelectedElements)
            {
                if (!item.View) continue;

                if (item is ICore2DDrawingSelectionView)
                {
                    (item as ICore2DDrawingSelectionView).RenderSelection(this);
                }
                else
                {

                    Rectanglef ibound = item.GetSelectionBound();
                    //Rectanglef ibound = item.GetBound();
                    v_r = Rectanglei.Round(m_scene.GetScreenBound(ibound));
                    device.DrawRectangle(Colorf.Black,
                        v_r.X,
                        v_r.Y,
                        v_r.Width,
                        v_r.Height);
                    device.DrawRectangle(Colorf.White,
                        1.0f,
                        enuDashStyle.Dash,
                        v_r.X,
                        v_r.Y,
                        v_r.Width,
                        v_r.Height);
                }
            }
            device.Restore(obj);
        }

        public ICoreGraphics Device
        {
            get { return this.m_device; }
        }

        public Rectanglef GetScreenBound(Rectanglef rc)
        {
            return m_scene.GetScreenBound(rc);
        }

        public Vector2f GetScreenLocation(Vector2f loc)
        {
            return m_scene.GetScreenLocation(loc);
        }
    }
}

