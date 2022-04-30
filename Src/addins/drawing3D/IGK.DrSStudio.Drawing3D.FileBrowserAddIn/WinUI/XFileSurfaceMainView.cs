

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XFileSurfaceMainView.cs
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
file:XFileSurfaceMainView.cs
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
using System.Windows.Forms ;
using System.Collections;
using System.Threading;
namespace IGK.DrSStudio.FBAddIn.WinUI
{
    
using IGK.ICore;using IGK.DrSStudio.WinUI;
    using IGK.OGLGame.Graphics;
    using IGK.DrSStudio.Drawing2D.WinUI;
    using IGK.DrSStudio.FBAddIn.AnimationModel;
    using IGK.DrSStudio.Drawing3D.FileBrowserAddIn;
    /// <summary>
    /// represent the surface main view
    /// </summary>
    public class XFileSurfaceMainView : XOGL3DControl, IFileMainView
    {
        private FBControlSurface m_ownerSurface;
        private AnimationModelBase m_CurrentAnimModel;
        private AnimModelCollection  m_AnimationModels;
        private SpriteBatch m_bacth;
        private SpriteFont m_font; //to render text   
        /// <summary>
        /// get the animation models collection
        /// </summary>
        public AnimModelCollection  AnimationModels
        {
            get {
                if (this.m_AnimationModels  == null)
                    this.m_AnimationModels = new AnimModelCollection(this);
                return m_AnimationModels;
            }
        }
        public AnimationModelBase CurrentAnimModel
        {
            get {
                return m_CurrentAnimModel; 
            }
            set
            {
                if (m_CurrentAnimModel != value)
                {
                    if (this.m_CurrentAnimModel != null) m_CurrentAnimModel.Dispose();
                    m_CurrentAnimModel = value;
                    if (this.m_CurrentAnimModel != null) InitModel();
                    OnAnimationModelChanged(EventArgs.Empty);
                }
            }
        }
        private void InitModel()
        {
            if (this.CurrentAnimModel is I3DModel)
            {
                (this.CurrentAnimModel as I3DModel ).Initialize (this.Device, this.ClientRectangle  );
            }            
        }
        public event EventHandler AnimationModelChanged;
        ///<summary>
        ///raise the AnimationModelChanged 
        ///</summary>
        protected virtual void OnAnimationModelChanged(EventArgs e)
        {
            if (AnimationModelChanged != null)
                AnimationModelChanged(this, e);
        }
        /// <summary>
        /// get the owner surface
        /// </summary>
        public FBControlSurface OwnerSurface {
            get {
                return this.m_ownerSurface;
            }
        }
        public XFileSurfaceMainView(FBControlSurface ownerSurface)
        {
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.Selectable, true);
            this.m_ownerSurface = ownerSurface;
            this.m_ownerSurface.SelectedFileIndexChanged += new EventHandler(m_ownerSurface_SelectedFileIndexChanged);
            this.m_ownerSurface.SelectedFolderChanged += new EventHandler(m_ownerSurface_SelectedFolderChanged);
        }
        void m_ownerSurface_SelectedFolderChanged(object sender, EventArgs e)
        {
            if (this.CurrentAnimModel !=null)
            this.CurrentAnimModel.OrganiseTextures(this.Device );
        }
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            if (!this.Focused)
                this.Focus();
        }
        void m_ownerSurface_SelectedFileIndexChanged(object sender, EventArgs e)
        {
            if (this.CurrentAnimModel !=null)
            this.CurrentAnimModel.OrganiseTextures(this.Device );
        }
        protected override void Dispose(bool disposing)
        {
            try
            {
                Device.MakeCurrent();
                Device.Dispose();
                if (this.m_font != null)
                {
                    this.m_font.Dispose();
                    this.m_font = null;
                }
            }
            catch { 
            }
            base.Dispose(disposing);
        }
        /// <summary>
        /// init device setting
        /// </summary>
        protected override void InitDevice()
        {
            Device.MakeCurrent();
            m_bacth = new SpriteBatch (Device , this );
            this.InitFont();
            if (this.m_CurrentAnimModel == null)
            {
                if (this.AnimationModels.Count == 0)
                {
                    AnimationModelBase m = null;
                    foreach (Type t in AnimationModelBase.GetModels())
                    {
                        m = t.Assembly.CreateInstance(t.FullName) as AnimationModelBase ;
                        if (m!=null)
                        this.AnimationModels.Add(m);
                    }
                    this.CurrentAnimModel = this.AnimationModels[0];
                }
            }
        }
        private void InitFont()
        {
            m_font = SpriteFont.Create(Device, "Arial", 12, IGK.OGLGame.enuGLFontStyle.Regular, 5,12);
        }
        internal void InitFontSetting(FBSettingObject configFontElement)
        {
            SpriteFont c =SpriteFont.Create(Device, this.Font.FontFamily.Name, 
                configFontElement.FontSize  , IGK.OGLGame.enuGLFontStyle.Regular, configFontElement.FontWidth ,
                configFontElement.FontHeight );
            if (c != null)
            {
                this.m_font = c;
                this.Render();
            }
        }
        protected override void InitView()
        {
            base.InitView();
            if (this.Device != null)
            {
                this.CurrentAnimModel.InitView(this.Device ,this.ClientRectangle.ToRectanglei());
            }
        }
        public string SelectedFile
        {
            get {
                if (this.m_ownerSurface.SelectedFileIndex == -1)
                    return string.Empty;
                return this.m_ownerSurface .Files[this.m_ownerSurface.SelectedFileIndex]; 
            }
        }
        public override void  Render()
        {
            if (this.Device == null) return;
            this.Device.MakeCurrent();
            this.Device.Clear(enuBufferBit.Accum | enuBufferBit.Depth,
                this.m_ownerSurface.BackgroundColor);
            this.CurrentAnimModel.Render(this.Device );
            m_bacth.Begin();
            if (this.m_ownerSurface.ShowText)
            {
                m_bacth.DrawString(m_font, this.SelectedFile, new Vector2f(1, 1), Colorf.FromFloat(0.3f));
                m_bacth.DrawString(m_font, this.SelectedFile, Vector2f.Zero, Colorf.White);
            }
            m_bacth.End();
            Device.EndScene();
        }
        #region IFileMainView Members
        public virtual bool CanGoLeft
        {
            get {
                return this.CurrentAnimModel.CanGoLeft;                
            }
        }
        public virtual bool CanGoRight
        {
            get
            {
                return this.CurrentAnimModel.CanGoRight;
            }
        }
        public  bool CanGoUp
        {
            get{
                return this.CurrentAnimModel.CanGoUp;
            }
        }
        public bool CanGoDown
        {
            get{
                return this.CurrentAnimModel.CanGoDown;            
            }
        }
        public virtual void GoLeft()
        {
            this.CurrentAnimModel.GoLeft();
        }
        public void GoRight()
        {
            this.CurrentAnimModel.GoRight();
        }
        public void GoUp()
        {
            this.CurrentAnimModel.GoUp();
        }
        public void GoDown()
        {
            this.CurrentAnimModel.GoDown();
        }
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            if (e.Delta > 0)
                GoUp();
            else
                GoDown();
        }
        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            switch (e.KeyCode)
            { 
                case Keys.Up :
                    GoUp();
                    break;
                case Keys.Down :
                    GoDown();
                    break;
                case Keys.Left :
                    GoLeft();
                    break;
                case Keys.Right :
                    GoRight();
                    break;
            }
        }
        #endregion
        protected override void OnMouseClick(System.Windows.Forms.MouseEventArgs e)
        {
            switch (e.Button)
            { 
                case System.Windows.Forms.MouseButtons.Left :
                    GoLeft();
                    break;
                case MouseButtons.Right :
                    GoRight();
                    break;
            }
            base.OnMouseClick(e);
        }
        protected override void OnPaintBackground(PaintEventArgs pevent)
        {        
            //this.Render();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.DrawString("Sample",
                this.Font, 
                Brushes.Black ,
                Vector2f.Empty);
        }
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg )
            { 
                case 0x5 : //size change
                    if (m.WParam == IntPtr.Zero)
                    {
                        if (m.LParam == IntPtr.Zero)
                        {
                            this.Animated = false;
                        }
                        else
                            this.Animated = IsAnimated;
                    }
                    break;
                case 0x8://killfocus
                    //this.Animated = false;
                    break;
                case 0x7://set focuse
                    //this.Animated = IsAnimated;
                    break;
                case 0xf://Paint
                    //this.Render();
                    break;// return;
            }
            base.WndProc(ref m);
         //   Console.WriteLine(m);
        }
        public void RefreshView()
        {
            this.CurrentAnimModel.Refresh();
            this.Render();
        }
        /// <summary>
        /// represent the animation model collection
        /// </summary>
        public class AnimModelCollection : IEnumerable
        {
            List<AnimationModelBase> m_models;
            XFileSurfaceMainView m_surfaceMainView;
            public AnimModelCollection(XFileSurfaceMainView  surface)
            {
                m_surfaceMainView = surface;
                m_models = new List<AnimationModelBase>();
            }
            public AnimationModelBase this[int index] {
                get {
                    return this.m_models[index];
                }
            }
            /// <summary>
            /// get the count model
            /// </summary>
            public int Count {
                get { return this.m_models.Count; }
            }
            public void Add(AnimationModelBase anim)
            {
                if ((anim == null) || this.m_models.Contains(anim))
                {
                    return;
                }
                this.m_models.Add(anim);
                anim.Surface = this.m_surfaceMainView.OwnerSurface;
                this.m_surfaceMainView.OnAnimModelAdded(new AnimModelEventArgs(anim));
            }
            public void Remove(AnimationModelBase anim)
            {
                if ((anim == null) || !this.m_models.Contains(anim))
                {
                    return;
                }
                this.m_models.Remove (anim);
                anim.Surface = null;
                this.m_surfaceMainView.OnAnimModelRemoved(new AnimModelEventArgs(anim));
            }
            #region IEnumerable Members
            public IEnumerator GetEnumerator()
            {
                return this.m_models.GetEnumerator();
            }
            #endregion
        }
        public event AnimModelEventHandler ModelAdded;
        public event AnimModelEventHandler ModelRemoved;
        internal void OnAnimModelAdded(AnimModelEventArgs e)
        {
            if (this.ModelAdded !=null)
                this.ModelAdded (this, e);
        }
        internal void OnAnimModelRemoved(AnimModelEventArgs e)
        {
            if (this.ModelRemoved !=null)
                this.ModelRemoved (this, e);
        }
    }
}

