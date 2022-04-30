

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CubeAnimationModel.cs
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
file:CubeAnimationModel.cs
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

using IGK.ICore;using IGK.OGLGame.Graphics;
namespace IGK.DrSStudio.Drawing3D.FileBrowser.AnimationModel
{
    
    using IGK.ICore.WinUI;
    using IGK.DrSStudio.Drawing3D.FileBrowser.WinUI;
    /// <summary>
    /// represent a cube animation model
    /// </summary>
    public class CubeAnimationModel : AnimationModelBase, IDisposable, I3DModel
    {
        uint m_ListId;
        float m_angleX;
        float m_angleY;
        Texture2D Face1;
        Texture2D Face2;
        Texture2D Face3;
        Texture2D Face4;
        Texture2D Face5;
        Texture2D Face6;
        private bool rotating;
        private const int DEF_WIDTH = 1024;
        private const int DEF_HEIGHT = 1024;
        private enuTextureEnvironmentMode m_envMode;
        public uint ListId {
            get { return this.m_ListId; }
            private set { this.m_ListId = value; }
        }
        protected internal override void InitView(OGLGraphicsDevice Device, Rectanglei view)
        {
        //    Device.Projection.MatrixMode = MatrixMode.Projection;
        //    float w = view.Width;
        //    float h = view.Height ;
        //    Device.Projection.LoadIdentity();
        ////    Device.Projection.SetFrustum(-0.5f, 0.5f, -0.5f, 0.5f, 0.5f, 5.0f);
        //    if (h > w)
        //    {
        //        w = h;
        //        h = view.Width;
        //    }
        //    Device.Projection.SetFrustum(-w, w, -h, h, 0.5f, 5.0f);
        //    Device.Projection.MatrixMode = MatrixMode.ModelView;
        //    Device.Projection.LoadIdentity();
        //    Device.Projection.Translate(0, 0, -4.5f);
        //   // Device.Projection.Scale(4f, 4f, 4f);
        }
        public override void Initialize(OGLGraphicsDevice Device, Rectanglei view)
        {
            if (Device == null)
                throw new ArgumentNullException("device");
            if (ListId == 0)
            {
                ListId = Device.GenList(1);
            }
          
            Bitmap cbmp = new Bitmap(DEF_WIDTH, DEF_HEIGHT);
            //load 6 face
            Face1 = Texture2D.Load(Device, cbmp);
            Face2 = Texture2D.Load(Device, cbmp);
            Face3 = Texture2D.Load(Device, cbmp);
            Face4 = Texture2D.Load(Device, cbmp);
            Face5 = Texture2D.Load(Device, cbmp);
            Face6 = Texture2D.Load(Device, cbmp);
            cbmp.Dispose();
            enuTextureEnvironmentMode v_txt = EnvMode;
            Face1.TextureEnvironmentMode = v_txt;
            Face2.TextureEnvironmentMode = v_txt;
            Face3.TextureEnvironmentMode = v_txt;
            Face4.TextureEnvironmentMode = v_txt;
            Face5.TextureEnvironmentMode = v_txt;
            Face6.TextureEnvironmentMode = v_txt;
            OrganiseTextures(Device );
     
            //start list
            Device.BeginNewList(ListId, ListMode.Compile);

            Device.Capabilities.DepthTest = true;
            Device.Capabilities.Texture2D = true;
            Device.Capabilities.Normalize = false;
            Device.Capabilities.Lighting = false;
            Device.Capabilities.CullFace = false;
            Device.Blending.Enabled = false;
            Device.RenderState.CullFaceMode = PolygonCullFace.Front ;
            
            Device.RenderState.CullFrontFace = PolygonFrontFace.AntiClockWise;
           // Device.SetColor(Colorf.FromFloat(1.0f));
           // //setup projection
            Device.Projection.MatrixMode = MatrixMode.Projection;
            Device.Projection.PushMatrix();
            //Device.Projection.LoadIdentity();
            Device.Projection.SetOrtho(-1, 1, -0.5f, 0.5f, -1f, 10);
            Device.Projection.Translate(0, 0, -1.5f);
            Device.Projection.MatrixMode = MatrixMode.ModelView;
            Device.Projection.PushMatrix();
           //  Device.Projection.LoadIdentity();
            //Device.DrawRectangle(Colorf.Red, 0, 0, 1, 1);
            
            //Device.Capabilities.Texture2D = false;
            //Device.SetColor(Colorf.FromFloat (0.4f, Colorf.White  ));
            //Device.Begin(enuGraphicsPrimitives.Quads);            
            //Device.SetTexCoord(0, 0); Device.SetVertex(-0.5f, 0.5f, 0.5f);
            //Device.SetTexCoord(1, 0); Device.SetVertex(0.5f, 0.5f, 0.5f);
            //Device.SetTexCoord(1, 1); Device.SetVertex(0.5f, -0.5f, 0.5f);
            //Device.SetTexCoord(0, 1); Device.SetVertex(-0.5f, -0.5f, 0.5f);
            //Device.End();
            //Device.Capabilities.Texture2D = true;



            ///face 1:
            Face1.Bind();
            Device.Begin(enuGraphicsPrimitives.Quads);
            IGK.GLLib.GL.glNormal3f(0, 0, -1);
            Device.SetTexCoord(0, 0); Device.SetVertex(-0.5f, 0.5f, 0.5f);
            Device.SetTexCoord(1, 0); Device.SetVertex(0.5f, 0.5f, 0.5f);
            Device.SetTexCoord(1, 1); Device.SetVertex(0.5f, -0.5f, 0.5f);
            Device.SetTexCoord(0, 1); Device.SetVertex(-0.5f, -0.5f, 0.5f);
            Device.End();
            Face2.Bind();
            //face 2:
            Device.Begin(enuGraphicsPrimitives.Quads);
            IGK.GLLib.GL.glNormal3f(-1, 0, 0);
            Device.SetTexCoord(0, 0); Device.SetVertex(-0.5f, 0.5f, -0.5f);
            Device.SetTexCoord(1, 0); Device.SetVertex(-0.5f, 0.5f, 0.5f);
            Device.SetTexCoord(1, 1); Device.SetVertex(-0.5f, -0.5f, 0.5f);
            Device.SetTexCoord(0, 1); Device.SetVertex(-0.5f, -0.5f, -0.5f);
            Device.End();
            Face3.Bind();
            Device.Begin(enuGraphicsPrimitives.Quads);
            //face 3:
            IGK.GLLib.GL.glNormal3f(1, 0, 0);
            Device.SetTexCoord(0, 0); Device.SetVertex(0.5f, 0.5f, 0.5f);
            Device.SetTexCoord(1, 0); Device.SetVertex(0.5f, 0.5f, -0.5f);
            Device.SetTexCoord(1, 1); Device.SetVertex(0.5f, -0.5f, -0.5f);
            Device.SetTexCoord(0, 1); Device.SetVertex(0.5f, -0.5f, 0.5f);
            Device.End();
            Face4.Bind();
            Device.Begin(enuGraphicsPrimitives.Quads);
            //face 4:
            IGK.GLLib.GL.glNormal3f(0, 1, 0);
            Device.SetTexCoord(0, 0); Device.SetVertex(-0.5f, 0.5f, -0.5f);
            Device.SetTexCoord(1, 0); Device.SetVertex(0.5f, 0.5f, -0.5f);
            Device.SetTexCoord(1, 1); Device.SetVertex(0.5f, 0.5f, 0.5f);
            Device.SetTexCoord(0, 1); Device.SetVertex(-0.5f, 0.5f, 0.5f);
            Device.End();
            Face5.Bind();
            Device.Begin(enuGraphicsPrimitives.Quads);
            //face 5
            IGK.GLLib.GL.glNormal3f(0, -1, 0);
            Device.SetTexCoord(0, 0); Device.SetVertex(-0.5f, -0.5f, 0.5f);
            Device.SetTexCoord(1, 0); Device.SetVertex(0.5f, -0.5f, 0.5f);
            Device.SetTexCoord(1, 1); Device.SetVertex(0.5f, -0.5f, -0.5f);
            Device.SetTexCoord(0, 1); Device.SetVertex(-0.5f, -0.5f, -0.5f);
            Device.End();

            //restorer projection properties
            //setup projection
            Device.Projection.MatrixMode = MatrixMode.Projection;
            Device.Projection.PopMatrix();
            Device.Projection.MatrixMode = MatrixMode.ModelView;
            Device.Projection.PopMatrix();


            Device.Capabilities.Texture2D = false;
            Device.Capabilities.DepthTest = false;
            Device.Capabilities.Normalize = false;
            Device.Capabilities.Lighting = false;
            Device.Capabilities.CullFace = false;
            Device.Blending.Enabled = false;
            Device.EndList();
        }
        public CubeAnimationModel()
        {
            this.Columns = 10;
            this.EnvMode = enuTextureEnvironmentMode.Modulate;
        }
        public override void Render(OGLGraphicsDevice Device)
        {
            OGLGraphicsDevice d = Device;
            if (d == null) 
                return;
            if (!d.IsCurrent)
                d.MakeCurrent();
            d.PushAttrib(enuAttribBit.All);
            d.Projection.PushMatrix();           
            d.Projection.LoadIdentity();
    
            d.Projection.Rotate(m_angleX, 0, 1, 0);
            d.Projection.Rotate(m_angleY, 1, 0, 0);            
            d.Blending.Enabled = true;
            d.Blending.BlendColor = FBRenderer.FBCubeBlendColor;// CoreRenderer.GetColor("FBCubeBlendColor", Colorf.IndianRed);
            d.SetColor(FBRenderer.FBCubeColor);//CoreRenderer.GetColor("FBCubeColor", Colorf.Red));
            d.CallList(ListId);
            d.Projection.PopMatrix();
            d.PopAttrib();
        }
        public override void OrganiseTextures(OGLGraphicsDevice Device)
        {
            if (Device == null)
                return;
            int i = this.Surface.SelectedFileIndex;
            if (i == -1)
                return;
            int stride = this.Columns;
            int[] indices = new int[] { 
                i, i-1,i+1,
                i-stride , i+stride 
            };
            string file = this.Surface.Files[i];
            int DEF_WIDTH = 256;
            int DEF_HEIGHT = 256;

            XFileUtils.ReplaceTexture(Device, Face1, file, DEF_WIDTH, DEF_HEIGHT);
            XFileUtils.ReplaceTexture(Device, Face2, this.Surface.Files[indices[1]], DEF_WIDTH, DEF_HEIGHT);
            XFileUtils.ReplaceTexture(Device, Face3, this.Surface.Files[indices[2]] ,DEF_WIDTH , DEF_HEIGHT );
            //up face
            XFileUtils.ReplaceTexture(Device, Face4, this.Surface.Files[indices[3]], DEF_WIDTH, DEF_HEIGHT);
            //down face
           XFileUtils. ReplaceTexture(Device, Face5,   this.Surface.Files[indices[4]], DEF_WIDTH, DEF_HEIGHT);
        }
        internal override void Refresh()
        {
            //this.ReplaceTexture(this.Face1, this.Surface.Files[this.Surface.SelectedFileIndex]);
        }
        void anim_UpdateValueX(object sender, EventArgs e)
        {
            Animate anim = sender as Animate;
            this.m_angleX = anim.Value;
            this.rotating = anim.Animated;
            if (!this.rotating)
            {
                this.m_angleX = 0;
                switch (this.State)
                {
                    case enuFileViewState.GoLeft:
                        this.Surface.SelectedFileIndex++;
                        break;
                    case enuFileViewState.GoRight:
                        this.Surface.SelectedFileIndex--;
                        break;
                }
                this.State = enuFileViewState.Ready;
            }
            this.Surface.Render();
        }
        void anim_UpdateValueY(object sender, EventArgs e)
        {
            Animate anim = sender as Animate;
            this.m_angleY = anim.Value;
            this.rotating = anim.Animated;
            if (!this.rotating)
            {
                this.m_angleY = 0;
                switch (this.State)
                {
                    case enuFileViewState.GoUp:
                    this.Surface.SelectedFileIndex -= this.Columns;
                        break;
                    case enuFileViewState.GoDown:
                        this.Surface.SelectedFileIndex += this.Columns;
                        break;
                }
                this.State = enuFileViewState.Ready;
            }
            this.Surface.Render();
        }
        class Animate : IDisposable
        {
            global::System.Windows.Forms.Timer m_timer;
            int m_Step;
            float m_CVAlue;
            float m_OLDValue;
            float m_direction;
            public float Value
            {
                get { return this.m_OLDValue + m_CVAlue; }
            }
            public Animate(float oldValue, float step)
            {
                m_timer = new global::System.Windows.Forms.Timer();
                m_timer.Interval = 10;
                m_Step = (int)Math.Abs(step);
                m_direction = step / Math.Abs(step);
                m_CVAlue = 0;
                m_OLDValue = oldValue;
                m_timer.Tick += new EventHandler(m_timer_Tick);
            }
            public bool Animated
            {
                get { return this.m_timer.Enabled; }
                set { this.m_timer.Enabled = value; }
            }
            void m_timer_Tick(object sender, EventArgs e)
            {
                this.Update();
            }
            private void Update()
            {
                this.m_CVAlue += this.m_Step * m_direction;
                float c = Math.Abs(this.m_CVAlue);
                if (c <= 90.0f)
                    OnUpdate(EventArgs.Empty);
                else
                {
                    this.Animated = false;
                    if (c > 90)
                    {
                        this.m_CVAlue = 90 * m_direction;
                        OnUpdate(EventArgs.Empty);
                        this.Dispose();
                    }
                }
            }
            public event EventHandler UpdateValue;
            private void OnUpdate(EventArgs eventArgs)
            {
                if (this.UpdateValue != null)
                    this.UpdateValue(this, eventArgs);
            }
            #region IDisposable Members
            public void Dispose()
            {
                if (this.m_timer != null)
                    this.m_timer.Dispose();
            }
            #endregion
            private int m_SelectedIndex;
            /// <summary>
            /// get or set the selected index
            /// </summary>
            public int SelectedIndex
            {
                get { return m_SelectedIndex; }
                set
                {
                    if (m_SelectedIndex != value)
                    {
                        m_SelectedIndex = value;
                    }
                }
            }
        }
        public override void GoUp()
        {
            if ((rotating == false) && (CanGoUp))
            {
                rotating = true;
                Animate anim = new Animate(m_angleY, 5);
                anim.UpdateValue += new EventHandler(anim_UpdateValueY);
                this.State = enuFileViewState.GoUp;
                anim.Animated = true;
            }
        }
        public override void GoDown()
        {
            if ((rotating == false) && (CanGoDown))
            {
                rotating = true;
                Animate anim = new Animate(m_angleY, -5);
                anim.UpdateValue += new EventHandler(anim_UpdateValueY);
                this.State = enuFileViewState.GoDown;
                anim.Animated = true;
            }
        }
        public override void GoLeft()
        {
            if ((rotating == false) && CanGoLeft)
            {
                rotating = true;
                Animate anim = new Animate(m_angleX, -5);
                anim.UpdateValue += new EventHandler(anim_UpdateValueX);
                this.State = enuFileViewState.GoLeft;
                anim.Animated = true;
            }
        }
        public override void GoRight()
        {
            if ((rotating == false) && (this.CanGoRight))
            {
                rotating = true;
                Animate anim = new Animate(m_angleX, 5);
                anim.UpdateValue += new EventHandler(anim_UpdateValueX);
                this.State = enuFileViewState.GoRight;
                anim.Animated = true;
            }
        }
        #region IDisposable Members
        public override  void Dispose()
        {
            if (Face1 != null) Face1.Dispose();
            if (Face2 != null) Face2.Dispose();
            if (Face3 != null) Face3.Dispose();
            if (Face4 != null) Face4.Dispose();
            if (Face5 != null) Face5.Dispose();
            if (Face6 != null) Face6.Dispose();
        }
        #endregion

        public enuTextureEnvironmentMode EnvMode {
            get { return this.m_envMode;  }
            set {
                this.m_envMode = value;
                OnPropertyChanged(EventArgs.Empty);
            }
        }
     

      
    }
}

