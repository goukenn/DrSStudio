

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AndroidToolMecanismBase.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.WinCore;
using IGK.ICore;
using IGK.ICore.Actions;
using IGK.ICore.Drawing2D;
using IGK.ICore.GraphicModels;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.DrSStudio.Android.WinUI
{
    public class AndroidToolMecanismBase : ICoreWorkingMecanism, ICore2DDrawingFrameRenderer
    {
        private AndroidLayoutDesignSurface m_CurrentSurface;
        private bool m_isFreezed;
        private bool m_allowContextMenu;
        private bool m_designMode;
        private Vector2f m_StartPoint;
        private Vector2f m_EndPoint;

        public AndroidLayoutDesignSurface CurrentSurface {
            get {
                return this.m_CurrentSurface;
            }
        }
        public void Invalidate() {
            this.m_CurrentSurface.RefreshScene();
        }
        /// <summary>
        /// get or set the endpoint
        /// </summary>
        public Vector2f EndPoint
        {
            get { return m_EndPoint; }
            set
            {
                if (m_EndPoint != value)
                {
                    m_EndPoint = value;
                }
            }
        }
        /// <summary>
        /// Get or set the start point
        /// </summary>
        public Vector2f StartPoint
        {
            get { return m_StartPoint; }
            set
            {
                if (m_StartPoint != value)
                {
                    m_StartPoint = value;
                }
            }
        }

        public event EventHandler StateChanged;
        ///<summary>
        ///raise the StateChanged 
        ///</summary>
        protected virtual void OnStateChanged(EventArgs e)
        {
            if (StateChanged != null)
                StateChanged(this, e);
        }


        public AndroidToolMecanismBase()
        {

        }

        public virtual void Render(ICoreGraphics g)
        {
            Type t = this.GetType().DeclaringType;
            string n = string.Empty;
            if (t != null)
                n = t.Name;
            CoreFont ft = WinCoreFont.CreateFont("FontName:arial:Size:12pt");
            g.DrawString(n,
                ft.ToGdiFont(),
                WinCoreBrushRegister.GetBrush(Colorf.Black),
                0, 0);
            ft.Dispose();
        }

        public bool AllowContextMenu
        {
            get { return this.m_allowContextMenu; }
            protected set {
                this.m_allowContextMenu = value;
            }
        }

        public bool DesignMode
        {
            get { return this.m_designMode; }
            protected set { this.m_designMode = value; }
        }

        public void Edit(ICoreWorkingObject e)
        {
            Edit(e as AndroidToolElement);
        }

        private void Edit(AndroidToolElement androidToolElement)
        {
            
        }

        public void Freeze()
        {
            if (this.UnRegister())
            {
                this.m_isFreezed = true;
            }
        }

        public bool IsFreezed
        {
            get { return this.m_isFreezed; }
        }

        public ICoreSnippetCollections RegSnippets
        {
            get { return null; }
        }

        bool ICoreWorkingMecanism.Register(ICoreWorkingSurface surface)
        {
            if (surface is AndroidLayoutDesignSurface )
                return Register(surface as AndroidLayoutDesignSurface);
            return false;
        }

        protected virtual bool Register(AndroidLayoutDesignSurface surface)
        {
            if (surface == null) return false;

            surface.MouseDown += _MouseDown;
            surface.MouseMove += _MouseMove;
            surface.MouseUp += _MouseUp;
            surface.MouseClick += _MouseClick;
            surface.MouseDoubleClick += _MouseDoubleClick;
            this.m_CurrentSurface = surface;
            this.GenerateActions();
            return true;
            
        }

     
        protected virtual void UnRegister(AndroidLayoutDesignSurface surface)
        {
            surface.MouseDown -= _MouseDown;
            surface.MouseMove -= _MouseMove;
            surface.MouseUp -= _MouseUp;
            surface.MouseClick -= _MouseClick;
            surface.MouseDoubleClick -= _MouseDoubleClick;
        }

        private void _MouseDoubleClick(object sender, CoreMouseEventArgs e)
        {
            OnMouseDoubleClick(e);
        }

        protected virtual void OnMouseDoubleClick(CoreMouseEventArgs e)
        {
            
        }
        private void _MouseClick(object sender, CoreMouseEventArgs e)
        {
            OnMouseClick(e);
        }

        protected virtual void OnMouseClick(CoreMouseEventArgs e)
        {

        }
        private void _MouseDown(object sender, CoreMouseEventArgs e)
        {
            OnMouseDown(e);
        }
        protected virtual void OnMouseDown(CoreMouseEventArgs e)
        {

        }
        private void _MouseMove(object sender, CoreMouseEventArgs e)
        {
            OnMouseMove(e);
        }
        protected virtual void OnMouseMove(CoreMouseEventArgs e)
        {

        }
        private void _MouseUp(object sender, CoreMouseEventArgs e)
        {
            OnMouseUp(e);
        }
        protected virtual void OnMouseUp(CoreMouseEventArgs e)
        {

        }
     
        protected virtual void GenerateActions()
        {
            
        }
        private ICoreSnippet m_Snippet;

        public ICoreSnippet Snippet
        {
            get { return m_Snippet; }
            set
            {
                if (m_Snippet != value)
                {
                    m_Snippet = value;
                }
            }
        }


        public event EventHandler SnippetChanged;
        ///<summary>
        ///raise the SnippetChanged 
        ///</summary>
        protected virtual void OnSnippetChanged(EventArgs e)
        {
            if (SnippetChanged != null)
                SnippetChanged(this, e);
        }


        private int m_State;

        public int State
        {
            get { return m_State; }
            set
            {
                if (m_State != value)
                {
                    m_State = value;
                }
            }
        }


        public void UnFreeze()
        {
            if (this.m_isFreezed)
            {
                this.Register(this.m_CurrentSurface);
                this.m_isFreezed = false;
            }
        }

        public bool UnRegister()
        {
            if (this.m_CurrentSurface != null)
            {
                this.UnRegister(this.m_CurrentSurface);
            }
            return true;
        }

    
        public ICoreMecanismActionCollections Actions
        {
            get {
                return null;
            }
        }

        //ICoreWorkingSurface ICoreWorkingMecanism.CurrentSurface
        //{
        //    get {
        //        return this.m_CurrentSurface;
        //    }
        //}

        /// <summary>
        /// dispose android tool resources
        /// </summary>
        public virtual  void Dispose()
        {
            //
        }

        ICoreWorkingSurface ICoreWorkingSurfaceHost.CurrentSurface
        {
            get {
                return this.CurrentSurface;
            }
        }


        public bool CanProcessActionMessage(ICoreMessage m)
        {
            throw new NotImplementedException();
        }
    }
}
