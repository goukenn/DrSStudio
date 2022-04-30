

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKXWinCoreMenuItem.cs
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
file:XWinCoreMenuItem.cs
*/
using IGK.ICore.WinCore;
using IGK.ICore;
using IGK.ICore.Drawing2D;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGK.ICore.WinUI
{
    /// <summary>
    /// represent a wincore menu item
    /// </summary>
    public class IGKXWinCoreMenuItem : ToolStripMenuItem, IXCoreMenuItem, IXCoreMenuItemContainer
    {
        ICore2DDrawingDocument m_document;
        public IGKXWinCoreMenuItem()
        {
            
        }

        public override Size Size
        {
            get
            {
                return base.Size;
            }
            set
            {
                if (value.Height != 19)
                { 
                }
                base.Size = value;
            }
        }
        
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public override System.Drawing.Image Image
        {
            get
            {
                //to force image rendering
                if (this.m_document != null)
                    return WinCoreConstant.DUMMY_MENU_PICTURE_32x32;
                return base.Image;
            }
            set
            {
                base.Image = value;
            }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
        }
        public void Add(IXCoreMenuItem uICoremMenuItem)
        {
            ToolStripItem item = uICoremMenuItem as ToolStripItem;
            if (item != null)
                this.DropDownItems.Add(item);
        }
        public void Remove(IXCoreMenuItem uICoremMenuItem)
        {
            ToolStripItem item = uICoremMenuItem as ToolStripItem;
            if (item != null)
                this.DropDownItems.Remove(item);
        }

        public virtual void PerformHits()
        {
            var q = this.DropDown;
            while (q != null) {
                q.Hide();
                if (q.OwnerItem.IsOnDropDown)
                {
                    q = (q.OwnerItem.OwnerItem as ToolStripMenuItem)?.DropDown;//  as ToolStripDropDownMenu ;
                }
                else {
                    q = null;
                };
            }
        }

        public new ICoreMenu Owner
        {
            get { return base.Owner as ICoreMenu; }
        }
        public new enuKeys ShortcutKeys
        {
            get
            {
                return (enuKeys)base.ShortcutKeys;
            }
            set
            {
                base.ShortcutKeys =   (System.Windows.Forms.Keys)value;
            }
        }
        public ICoreMenuItemCollections Items
        {
            get {
                return null;
            }
        }
        public ICore2DDrawingDocument MenuDocument
        {
            get
            {
                return m_document;
            }
            set
            {
                m_document = value;
            }
        }
    }
}

