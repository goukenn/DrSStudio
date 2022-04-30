

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKXToolHostedControl.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.WinCore;
using IGK.ICore;
using IGK.ICore.Drawing2D;
using IGK.ICore.Tools;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.ICore.WinCore.WinUI.Controls;

namespace IGK.ICore.WinCore.Controls
{
    /// <summary>
    /// Base UserControl that can be used as a Tool's HostedControl
    /// </summary>
    public class IGKXToolHostedControl : IGKXUserControl , ICoreToolHostedControl 
    {
        private ICoreTool m_tool;
        private ICore2DDrawingDocument m_toolDocument;

        public IGKXToolHostedControl()
        {

        }
#if DEBUG
        protected override void OnDoubleClick(EventArgs e)
        {
            this.ReloadLayout();
            base.OnDoubleClick(e);
        }
        protected virtual void ReloadLayout()
        {
            this.InitLayout();
        }
#endif
        public ICoreTool Tool
        {
            get { return m_tool; }
            set { m_tool = value; }
        }
        public ICore2DDrawingDocument ToolDocument
        {
            get { return this.m_toolDocument; }
            set { this.m_toolDocument = value; }
        }
        Size2i ICoreToolHostedControl.Size
        {
            get
            {
                return new Size2i(base.Size.Width, base.Size.Height);
            }
            set
            {
                base.Size = new System.Drawing.Size(value.Width, value.Height);
            }
        }

        Vector2i ICoreToolHostedControl.Location
        {
            get
            {
                return new Vector2i(base.Location.X,
                    base.Location.Y);
            }
            set
            {
                base.Location = new System.Drawing.Point(value.X, value.Y);
            }
        }
    }
}
