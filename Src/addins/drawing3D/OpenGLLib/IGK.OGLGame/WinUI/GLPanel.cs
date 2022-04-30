

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GLPanel.cs
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
file:GLPanel.cs
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
namespace IGK.OGLGame.WinUI.GLControls
{
    
using IGK.ICore;using IGK.OGLGame.Graphics;
    /// <summary>
    /// Represnent a GLPanel
    /// </summary>
    public class GLPanel : GLControlBase, IGLControl, IOGLGGraphicsView  
    {
        SpriteBatch m_batch;
        protected SpriteBatch SpriteBatch {
            get {
                return m_batch;
            }
        }
        protected override System.Drawing.Size DefaultSize
        {
            get
            {
                return new System.Drawing.Size(300, 260);
            }
        }
        public GLPanel()
        {
        }
        protected internal override void Initialize()
        {
            base.Initialize();
            m_batch = new SpriteBatch(this.GraphicsDevice, this);
        }
        protected internal override void LoadContent()
        {
            base.LoadContent();
            //this.m_font = SpriteFont.Create(this.GraphicsDevice, "Time New Roman",
            //    12.0f, IGK.OGLGame.enuFontStyle.Regular, 12, 12);
        }
        protected internal override void UnloadContent()
        {
            base.UnloadContent();
        }        
        protected override void Render(IGK.OGLGame.Graphics.OGLGraphicsDevice device, IGLControlTime Time)
        {
            try
            {
                m_batch.Begin();
                base.Render(device, Time);
                m_batch.End();
            }
            catch (Exception ex)
            {
               System.Diagnostics.Debug.WriteLine ("Exception " + ex.Message );
            }
        }
        #region IGLControl Members
        #endregion
        #region IGraphicsView Members
        public override Rectanglei GetViewPort()
        {
            return new Rectanglei ( ClientRectangle.X,
                ClientRectangle.Y,
                ClientRectangle.Width ,
                ClientRectangle.Height );
        }
        #endregion
    }
}

