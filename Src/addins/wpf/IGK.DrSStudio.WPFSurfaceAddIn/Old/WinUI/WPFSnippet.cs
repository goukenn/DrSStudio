

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WPFSnippet.cs
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
file:WPFSnippet.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Shapes ;
using System.Windows.Controls;
namespace IGK.DrSStudio.WPFSurfaceAddIn.WinUI
{
    using IGK.ICore.WinCore;
using IGK.ICore;using IGK.DrSStudio.WinUI;
    public class WPFSnippet : IWPFSnippet, ICoreSnippet
    {
        private WPFBaseMecanism m_Mecanism;
        private int m_Demand;
        private int m_Index;
        private enuSnippetShape m_Shape;
        private Vector2d m_Location;
        private bool m_Visible;
        private bool m_Enabled;
        private Panel surface;
        private Shape m_view;
        private bool m_marked;
        /// <summary>
        /// get or set if this snippet is marked
        /// </summary>
        public bool Marked
        {
            get
            {
                return this.m_marked;
            }
            set
            {
                this.m_marked = value;
            }
        }
        public WPFSnippet(System.Windows.Controls.Panel surface, WPFBaseMecanism mecanism, int index, int Demand)
        {
            this.m_Mecanism = mecanism;
            this.m_Index = index;
            this.m_Demand = Demand;
            this.surface = surface;
            this.m_view = CreateShape(); 
            this.m_view.Width = 5;
            this.m_view.Height = 5;
            this.m_view.Fill = System.Windows.Media.Brushes.DarkBlue;
            this.m_view.Stroke = System.Windows.Media.Brushes.White;
            this.surface.Children.Add(this.m_view);
            this.Register();
              }
        protected virtual Shape CreateShape()
        {
            return new System.Windows.Shapes.Rectangle(); 
        }
        private void Register()
        {
            this.m_view.MouseEnter += new System.Windows.Input.MouseEventHandler(m_view_MouseEnter);
            this.m_view.MouseLeave += new System.Windows.Input.MouseEventHandler(m_view_MouseLeave);
            this.m_view.MouseDown += new System.Windows.Input.MouseButtonEventHandler(m_view_MouseDown);
            this.m_view.MouseUp += new System.Windows.Input.MouseButtonEventHandler(m_view_MouseUp);
        }
        void UnRegister()
        {
            this.m_view.MouseEnter -= new System.Windows.Input.MouseEventHandler(m_view_MouseEnter);
            this.m_view.MouseLeave -= new System.Windows.Input.MouseEventHandler(m_view_MouseLeave);
            this.m_view.MouseDown -= new System.Windows.Input.MouseButtonEventHandler(m_view_MouseDown);
            this.m_view.MouseUp -= new System.Windows.Input.MouseButtonEventHandler(m_view_MouseUp);
        }
        void m_view_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {           
            this.m_Mecanism.Snippet = null;
            this.m_view.Fill = System.Windows.Media.Brushes.DarkBlue;            
        }
        void m_view_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
        }
        void m_view_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Released)
            {
                if (this.m_Mecanism.Snippet == this)
                {
                    this.m_Mecanism.Snippet = null;
                    this.m_view.Fill = System.Windows.Media.Brushes.DarkBlue;
                }
            }
        }
        void m_view_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (this.m_Mecanism.Snippet == null)
            {
                //if 
                this.m_Mecanism.Snippet = this;
                this.m_view.Fill = System.Windows.Media.Brushes.Orange;
            }
        }
        private object  m_Tag;
        /// <summary>
        /// get or set the tag
        /// </summary>
        public object  Tag
        {
            get { return m_Tag; }
            set { m_Tag = value; }
        }
        public bool Enabled
        {
            get { return m_Enabled; }
            set
            {
                if (m_Enabled!= value)
                {
                    m_Enabled = value;
                }
            }
        }
        public bool Visible
        {
            get { return m_Visible; }
            set
            {
                if (m_Visible != value)
                {
                    m_Visible = value;
                    if (!value)
                    {
                        this.m_view.Visibility = Visibility.Hidden;
                    }
                    else
                        this.m_view.Visibility = Visibility.Visible;
                }
            }
        }
        public Vector2d Location
        {
            get { return m_Location; }
            set
            {
                if (m_Location != value)
                {
                    m_Location = value;
                    Rectangled rc = new Rectangled(value.X, value.Y, 0, 0);
                    rc.Inflate(4, 4);
                    this.m_view.SetValue(System.Windows.Controls.Canvas.LeftProperty, rc.X);
                    this.m_view.SetValue(System.Windows.Controls.Canvas.TopProperty , rc.Y);
                    this.m_view.Width = rc.Width;
                    this.m_view.Height = rc.Height;
                }
            }
        }
        public enuSnippetShape Shape
        {
            get { return m_Shape; }
            set
            {
                if (m_Shape != value)
                {
                    m_Shape = value;
                }
            }
        }
        public int Index
        {
            get { return m_Index; }
        }
        public int Demand
        {
            get { return m_Demand; }
        }
        public WPFBaseMecanism Mecanism
        {
            get { return m_Mecanism; }
        }
        #region ICoreSnippet Members
        ICoreWorkingMecanism ICoreSnippet.Mecanism
        {
            get { return this.m_Mecanism ; }
        }
        Vector2f  ICoreSnippet.Location 
        {
            get
            {
                return
                    new Vector2f((float)this.m_Location.X,
                  (float)this.m_Location.Y);
            }
            set {
                this.m_Location = new Vector2d(value.X, value.Y);
            }
        }
        public event System.Windows.Forms.MouseEventHandler SnippetBeginEdit;
        void  OnSnippetEdit()
        {
            if (this.SnippetBeginEdit != null)
                this.SnippetBeginEdit(this, null);
        }
        #endregion
        #region IDisposable Members
        public virtual void Dispose()
        {
            this.UnRegister();
            this.surface.Children.Remove(this.m_view);            
        }
        #endregion
    }
}

