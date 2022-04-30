

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GLEditorBitmapElement.cs
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
file:GLEditorBitmapElement.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D ;
namespace IGK.DrSStudio.GLPictureEditorAddIn
{
    
using IGK.ICore;using IGK.OGLGame.Graphics;
    using Texture2D = IGK.OGLGame.Graphics.Texture2D;
    class GLEditorBitmapElement :GLEditorLayeredElementBase ,IGLEditorLayerElement 
    {
        Bitmap m_bmp;
       // Texture2D m_texture;
        uint m_bitmapList;
        public override void FreeResources(OGLGraphicsDevice device)
        {
            base.FreeResources(device);
            if (m_bitmapList != 0)
                device.DeleteList(m_bitmapList);
            m_bitmapList = 0;
        }
        public override void Dispose()
        {
            //dispose bitmap
            if (m_bmp != null)
            {
                m_bmp.Dispose();
                m_bmp = null;
            }
            base.Dispose();
        }
        public GLEditorBitmapElement()
        {
           // this.m_texture = null;
        }
        public void LoadBitmap(OGLGraphicsDevice device, Bitmap bitmap)
        {
            this.m_bmp = bitmap;
            if (this.m_bitmapList == 0)
            this.m_bitmapList = device.GenList(1);
            device.BeginNewList(this.m_bitmapList , IGK.OGLGame.Graphics.ListMode.Compile);
            device.DrawBitmap(this.m_bmp );
            device.EndList();
        }
        public override void Render(OGLGraphicsDevice device)
        {
            if (this.m_bitmapList == 0)
                return;
            device.CallList(this.m_bitmapList);
        }
    }
}

