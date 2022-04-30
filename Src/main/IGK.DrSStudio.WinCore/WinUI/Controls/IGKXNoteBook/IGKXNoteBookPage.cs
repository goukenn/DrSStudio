

using IGK.ICore.WinCore.WinUI.Controls;
/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKXNoteBookPage.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.WinUI
{
    /// <summary>
    /// represent a note book page
    /// </summary>
    public class IGKXNoteBookPage : IGKXUserControl 
    {
        private string m_ImageKey;
        [Category("Resources")]
        public string ImageKey
        {
            get { return m_ImageKey; }
            set
            {
                if (m_ImageKey != value)
                {
                    m_ImageKey = value;
                }
            }
        }
        public override System.Drawing.Rectangle DisplayRectangle
        {
            get
            {
                return base.DisplayRectangle;
            }
        }
        public override System.Windows.Forms.DockStyle Dock
        {
            get
            {
                if (this.DesignMode)
                {
                    return System.Windows.Forms.DockStyle.None;
                }
                return base.Dock;
            }
            set
            {
                base.Dock = value;
            }
        }
        public IGKXNoteBookPage()
        {
            this.AutoSize = false;
            this.AutoScroll = true;
            this.Padding = System.Windows.Forms.Padding.Empty;
            this.Margin  = System.Windows.Forms.Padding.Empty;
            this.Dock = System.Windows.Forms.DockStyle.None;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
        }
    }
}
