

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XFileMultiview.cs
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
file:XFileMultiview.cs
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
using System.Drawing;
namespace IGK.DrSStudio.FBAddIn.WinUI
{
    
using IGK.ICore;using IGK.OGLGame.Graphics;    
    using IGK.DrSStudio.WinUI;
    public class XFileMultiview : XOGL3DControl, IFileMultifileView 
    {
        private int m_Rows;
        private int m_Columns;
        private Texture2D m_texture;
        private FBControlSurface m_surface;
        private uint m_List;
        private const int DEF_WIDTH = 128;
        private const int DEF_HEIGHT = 128;
        public int Columns
        {
            get { return m_Columns; }
            set
            {
                if (m_Columns != value)
                {
                    m_Columns = value;
                }
            }
        }
        public int Rows
        {
            get { return m_Rows; }
            set
            {
                if (m_Rows != value)
                {
                    m_Rows = value;
                }
            }
        }
        protected override bool RequireDoubleBuffer
        {
            get
            {
                return false;
            }
        }
        protected override void InitView()
        {
            base.InitView();
        }
        public XFileMultiview(FBControlSurface surface):base()
        {
            this.m_Columns = 5;
            this.m_Rows = 3;
            this.m_surface = surface;
            surface.SelectedFolderChanged += new EventHandler(surface_SelectedFolderChanged);
        }
        void surface_SelectedFolderChanged(object sender, EventArgs e)
        {
            BuildList();
            this.Invalidate();
        }
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            BuildList();
            this.Render();
        }
        protected override void InitDevice()
        {           
            base.InitDevice();
            Bitmap cbmp = new Bitmap(DEF_WIDTH, DEF_HEIGHT);
            this.m_texture = Texture2D.Load(Device, cbmp);
            cbmp.Dispose();
            this.m_texture.TextureEnvironmentMode = TextureEnvironmentMode.Replace;
        }
        protected override bool IsAnimated
        {
            get
            {
                return false;
            }
        }
        void BuildList()
        {
            if (Device == null)
                return;
            if (this.m_List == 0)
            {
                this.m_List = Device.GenList(1);
            }
            Device.MakeCurrent();
            Device.BeginNewList(this.m_List, ListMode.Compile);
            Device.Capabilities.ScissorTest = true;
            Device.Capabilities.Texture2D = true;
            int y = 0;
            int w = this.Width / this.Columns;
            int h = this.Height;
            this.m_texture.Bind();
            int v_index =0;
            for (int p = 0; p < this.Rows; p++)
            {
                y = p * h;
                for (int i = 0; i < this.Columns; i++)
                {
                    v_index = y * Columns + i;
                    if (v_index >= this.m_surface.Files.Count )
                    {
                        p = this.Rows;
                        break;
                    }
                    string str = this.m_surface.Files[p * Columns + i];
                    XFileUtils.ReplaceTexture(this.Device, this.m_texture, str, DEF_WIDTH, DEF_HEIGHT);
                    Device.Projection.PushMatrix();
                    Device.RenderState.ScissorBox = new System.Drawing.Rectangle(i * w, y, w, h);
                    Device.Projection.Translate(5 + i * w, 5 + y, 0);
                    Device.Begin(enuGraphicsPrimitives.Quads);
                    Device.SetColor(Colorf.Blue);
                    IGK.GLLib.GL.glNormal3f(0, 0, -1);
                    Device.SetTexCoord(0, 0); Device.SetVertex(0, 0, 0);
                    Device.SetTexCoord(1, 0); Device.SetVertex(w - 10, 0, 0);
                    Device.SetTexCoord(1, 1); Device.SetVertex(w - 10, h - 10, 0);
                    Device.SetTexCoord(0, 1); Device.SetVertex(0, h-10, 0);
                    Device.End();
                    Device.Projection.PopMatrix();
                }
            }
            Device.Capabilities.ScissorTest = false;
            Device.EndList();
        }
        public override  void Render() {
            if (!Device.IsCurrent )
            Device.MakeCurrent();
            Device.Viewport = this.ClientRectangle;
            Device.Projection.MatrixMode = MatrixMode.Projection ;
            Device.Projection .LoadIdentity ();
            Device.Projection.SetOrtho2D(0, this.Width , this.Height , 0);
            Device.Projection.MatrixMode = MatrixMode.ModelView;
            Device.Clear(enuBufferBit.Depth, Colorf.CornflowerBlue);
            if (m_List != 0)
            {
                //Device.CallList(this.m_List);
            }
            Device.EndScene();
        }
        #region IFileMultifileView Members
        public void RefreshView()
        {
            this.BuildList();
            this.Render();
        }
        #endregion
    }
}

