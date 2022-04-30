

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: TextEditorSegmentBase.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/

using IGK.ICore;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.DrSStudio;
using IGK.DrSStudio.Menu;
using IGK.DrSStudio.WinUI;
using IGK.DrSStudio.Drawing2D.WinUI;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:TextEditorSegmentBase.cs
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
namespace IGK.DrSStudio.XMLEditorAddIn.Segment
{
    public abstract class TextEditorSegmentBase : ITextSegment
    {
        private string m_Value;
        private string m_SegmentType;
        /// <summary>
        /// get or set the segment type
        /// </summary>
        public string SegmentType
        {
            get { return m_SegmentType; }
            set
            {
                if (m_SegmentType != value)
                {
                    m_SegmentType = value;
                    OnSegmentTypeChanged(EventArgs.Empty);
                }
            }
        }
        private void OnSegmentTypeChanged(EventArgs eventArgs)
        {
            if (this.SegmentTypeChanged != null)
            {
                this.SegmentTypeChanged(this, eventArgs);
            }
        }
        public event EventHandler SegmentTypeChanged;
        public event EventHandler SegmentValueChanged;
        /// <summary>
        /// get or set the balue of this segment
        /// </summary>
        public string Value
        {
            get { return m_Value; }
            set
            {
                if (m_Value != value)
                {
                    m_Value = value;
                    OnValueChanged(EventArgs.Empty);
                }
            }
        }
        protected virtual void OnValueChanged(EventArgs eventArgs)
        {
            if (this.SegmentValueChanged != null)
            {
                this.SegmentValueChanged(this, eventArgs);
            }
        }
        public TextEditorSegmentBase()
        {
        }
        protected TextEditorSegmentBase(string value)
        {
            this.m_Value = value;
        }
        #region ITextSegment Members
        public virtual Colorf Color
        {
            get { return Colorf.Black; }
        }
        public virtual System.Drawing.FontStyle FontStyle
        {
            get { return System.Drawing.FontStyle.Regular; }
        }
        #endregion
        public abstract void Draw(TextEditorRenderingEventArgs e);
    }
}

