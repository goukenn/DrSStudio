

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WorkingItem.cs
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
file:WorkingItem.cs
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
using System.Drawing.Drawing2D;
using System.Windows .Forms ;
namespace IGK.DrSStudio
{
    using IGK.ICore;using IGK.DrSStudio.WinUI;
    using IGK.DrSStudio.Drawing2D;
    /// <summary>
    /// represent the implementation of the working item
    /// </summary>
    public class WorkingItem : IWorkingItem 
    {
        IWorkingItemGroup m_Group;
        Type m_Type;
        private enuButtonState m_mouseState;             
        private bool m_selected;
        private Keys m_Keys;
        private string m_ImageKey;
        /// <summary>
        /// get the image key
        /// </summary>
        public string ImageKey
        {
            get { return m_ImageKey; }
        }
        public IWorkingGroupOwner Owner { get { return this.m_Group.Owner; } }
        public int Index { get { return this.m_Group.Items.IndexOf(this); } }
        #region IWorkingItem Members
        private string m_Title;
        private string m_DefaultKey;
        public string DefaultKey
        {
            get { return m_DefaultKey; }
        }
        public string Title
        {
            get { return m_Title; }
        }
        public IWorkingItemGroup Group
        {
            get { return this.m_Group; }
        }
        public Type Type {
            get { return this.m_Type; }
        }
        public Rectanglei Bound { get { return this.GetBound(); } }
        private Rectanglei GetBound()
        {
            if (!this.m_Group.Visible ||this.m_Group.Collapsed )
                return Rectanglei.Empty;
            int i = this.m_Group.Items.IndexOf(this);
            int y = Owner.VScrollBar .Visible ?  
                this.Owner.VScrollBar.Value - this.Owner.VScrollBar.Minimum 
                : 0;
            return new Rectangle(0, m_Group.Bound.Y +
                 m_Group.Bound.Height +
                 i * WinToolSelectorConstant.DIM_ITEM_HEIGHT - y,
                 Owner.VScrollBar .Visible ? Owner.Width - Owner.VScrollBar.Width : Owner.Width ,
                 WinToolSelectorConstant.DIM_ITEM_HEIGHT);
        }
        public Keys Keys { get { return this.m_Keys; } }
        #endregion
        public override string ToString()
        {
            return string.Format ("WorkingItem [{0}]",this.m_Title);
        }
        public WorkingItem(
            IWorkingItemGroup Group ,             
            Type t ,
            string titleKey,
            string imgkey,
            Keys keys,
            string defaultKey)
        {
            this.m_Group = Group;
            this.m_Type = t;
            this.m_Title = titleKey;
            this.m_Keys = keys;
            this.m_DefaultKey = defaultKey;
            this.m_ImageKey = imgkey;
            this.m_Group.Owner.SizeChanged += new EventHandler(m_Group_SizeChanged);
            this.m_Group.Owner.MouseLeave += new EventHandler(m_Group_MouseLeave);
            this.m_Group.Owner.MouseMove += new MouseEventHandler(m_Group_MouseMove);
            this.m_Group.Owner.MouseClick += new MouseEventHandler(m_Group_MouseClick);
            this.m_Group.Owner.GroupExpandedChanged += new GroupExpandedChangedEventHandler(m_Group_GroupExpandedChanged);
            this.m_Group.Owner.SelectedWorkingTypeChanged += new EventHandler(m_Group_SelectedWorkingTypeChanged);
        }
        void m_Group_MouseLeave(object sender, EventArgs e)
        {
            if (this.MouseState == enuButtonState.Over)
            {
                this.MouseState = enuButtonState.None;
                this.m_Group.Owner.Invalidate(this.Bound);
            }
        }
        void m_Group_SelectedWorkingTypeChanged(object sender, EventArgs e)
        {
            if (this.m_Group.Visible)
            {//toggle visibility mode if item are visible
                bool v = (this.m_Group.Owner.SelectedWorkingType == this.Type);
                if (this.m_selected != v)
                {
                    this.m_selected = v;
                    this.m_Group.Owner.Invalidate(this.Bound);
                }
            }
        }
        void m_Group_GroupExpandedChanged(object sender, GroupExpandedChangedEventArgs e)
        {
        }
        void m_Group_MouseClick(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    if (this.Group.Visible && this.Contains(e.Location) && (this.m_Group.Owner.SelectedWorkingType != this.Type))
                    {
                        this.Group.Owner.SelectedWorkingType = this.Type;
                    }
                    break;
            }
        }
        void m_Group_MouseMove(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.None:
                    if (this.Contains(e.Location))
                    {
                        this.MouseState = enuButtonState.Over;                    
                    }
                    else
                    {
                        this.MouseState = enuButtonState.None;
                    }
                    break;
            }
        }
        void m_Group_SizeChanged(object sender, EventArgs e)
        {
        }
        public bool Contains(Point pts)
        {
            if (this.m_Group.Collapsed)
                return false;          
            return this.Bound .Contains(pts);
        }
        #region IWorkingItem Members
        public bool Selected
        {
            get { return this.m_selected ; }
        }
        public IGK.DrSStudio.WinUI.enuButtonState MouseState
        {
            get { return this.m_mouseState; }
            set {
                if (this.m_mouseState != value)
                {
                    this.m_mouseState = value;
                    if (this.m_Group.Visible)
                    {
                        this.m_Group.Owner.Invalidate(this.Bound);                    
                    }
                }
            }
        }
        /// <summary>
        /// get the document tool
        /// </summary>
        public IGK.DrSStudio.Drawing2D.ICore2DDrawingDocument DocumentTool
        {
            get { 
                return CoreResources.GetDocument(this.ImageKey);
            }
        }
        #endregion
    }
}

