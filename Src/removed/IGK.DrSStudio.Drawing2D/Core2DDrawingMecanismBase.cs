

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Core2DDrawingMecanismBase.cs
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
file:Core2DDrawingMecanismBase.cs
*/
using IGK.ICore;using IGK.DrSStudio.Drawing2D.WinUI;
using IGK.DrSStudio.Mecanism;
using IGK.DrSStudio.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.Drawing2D
{
    /// <summary>
    /// on element changed
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Core2DDrawingMecanismBase<T> : CoreWorkingMecanismBase<T> where T : class
    {
        private Vector2f  m_StartPoint;
        private Vector2f m_EndPoint;
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
        public Vector2f  StartPoint
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
        public override bool Register(ICoreWorkingSurface surface)
        {
            if (surface == null)
                return false;
            surface.MouseDown += _mouseDown;
            surface.MouseMove += _mouseMove;
            surface.MouseLeave += _mouseLeave;
            surface.MouseClick += _mouseClick;
            surface.MouseDoubleClick += _mouseDoubleClick;
            surface.MouseEnter += _mouseEnter;
            this.CurrentSurface = surface;
            return true;
        }
        public override bool UnRegister()
        {
            if (this.CurrentSurface == null)
                return false;
            ICoreWorkingSurface surface = this.CurrentSurface;
            surface.MouseDown -= _mouseDown;
            surface.MouseMove -= _mouseMove;
            surface.MouseLeave -= _mouseLeave;
            surface.MouseClick -= _mouseClick;
            surface.MouseDoubleClick -= _mouseDoubleClick;
            surface.MouseEnter -= _mouseEnter;
            return true;
        }
        private void _mouseDoubleClick(object sender, CoreMouseEventArgs e)
        {
            OnMouseClick(e);
        }
        private void _mouseClick(object sender, CoreMouseEventArgs e)
        {
            OnMouseClick(e);
        }
        private void _mouseLeave(object sender, EventArgs e)
        {
            OnMouseLeave(e);
        }
        protected virtual void OnMouseLeave(EventArgs e)
        {
        }
        private void _mouseMove(object sender, CoreMouseEventArgs e)
        {
            OnMouseMove(e);
        }
        private void _mouseEnter(object sender, EventArgs e)
        {
            OnMouseEnter(e);
        }
        private void _mouseDown(object sender, CoreMouseEventArgs e)
        {
            OnMouseDown(e);
        }
        protected virtual void OnMouseEnter(EventArgs e)
        {
        }
        protected virtual void OnMouseClick(CoreMouseEventArgs e)
        {
        }
        protected virtual void OnMouseDown(CoreMouseEventArgs e)
        {
        }
        protected virtual void OnMouseUp(CoreMouseEventArgs e)
        {
        }
        protected virtual void OnMouseMove(CoreMouseEventArgs e)
        {
        }
        public override void Edit(T element)
        {
        }
        public override void EndEdition()
        {
            //this.Element.EndEdition();
        }
        protected override void GenerateActions()
        {
            //this.Actions.Add(Keys.Control | Keys.E, CoreSystem.GetAction ("Core.EditElement"));
        }
        protected virtual void BeginEdit(CoreMouseEventArgs e)
        { }
        protected virtual void UpdateEdit(CoreMouseEventArgs e)
        { }
        protected virtual void EndEdit(CoreMouseEventArgs e)
        { }
        protected virtual void BeginUpdate(CoreMouseEventArgs e)
        { }
        protected virtual void UpdateUpdate(CoreMouseEventArgs e)
        { }
        protected virtual void EndUpdate(CoreMouseEventArgs e)
        { }
    }
}

