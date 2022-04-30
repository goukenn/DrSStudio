

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKXToolStripButton.cs
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
file:IGKXToolStripButton.cs
*/
using IGK.ICore.WinCore;
using IGK.ICore.Actions;
using IGK.ICore.Drawing2D;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace IGK.ICore.WinCore.WinUI.Controls
{
    using IGK.ICore;

    public class IGKXToolStripButton : ToolStripButton
    {
        private ICore2DDrawingDocument m_ImageDocument;
        private ICoreAction m_Action;
        private string m_ToolTipKey;
        public override ToolStripItemDisplayStyle DisplayStyle
        {
            get
            {
                return base.DisplayStyle;
            }
            set
            {
                base.DisplayStyle = value;
            }
        }
        public override System.Drawing.Rectangle Bounds
        {
            get
            {
                return base.Bounds;
            }
        }
        public override System.Drawing.Size Size
        {
            get
            {
                return base.Size;
            }
            set
            {
                base.Size = value;
            }
        }
        /// <summary>
        /// get or set the key used to 
        /// </summary>
        public string ToolTipKey
        {
            get { return m_ToolTipKey; }
            set
            {
                if (m_ToolTipKey != value)
                {
                    m_ToolTipKey = value;
                    OnToolTipKeyChanged(EventArgs.Empty);
                    
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
        }
        

        private void OnToolTipKeyChanged(EventArgs eventArgs)
        {
            this.LoadDisplayText();
         
        }
        public void LoadDisplayText()
        {
            this.ToolTipText = this.ToolTipKey.R();
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden )]
        [Browsable (false)]
        [EditorBrowsable( EditorBrowsableState.Never )]
        public override System.Drawing.Image Image
        {
            get
            {
                if (m_ImageDocument != null)
                    return WinCoreConstant.DUMMY_MENU_PICTURE_32x32;
                return base.Image;
            }
            set
            {
                base.Image = value;
            }
        }
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden )]
        public ICoreAction Action
        {
            get { return m_Action; }
            set
            {
                if (m_Action != value)
                {
                    m_Action = value;
                }
            }
        }
        public ICore2DDrawingDocument ImageDocument
        {
            get { return m_ImageDocument; }
            set
            {
                if (m_ImageDocument != value)
                {
                    m_ImageDocument = value;
                }
            }
        }
        public IGKXToolStripButton()
        {
            this.Overflow = ToolStripItemOverflow.AsNeeded;
            this.Image = WinCoreConstant.DUMMY_MENU_PICTURE_32x32;
        }
      
        protected override void OnClick(EventArgs e)
        {
            if (this.Action != null)
            {
                this.Action.DoAction();
                return;
            }
            base.OnClick(e);
        }
    }
}

