

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKXContextMenuItem.cs
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
file:XContextMenuItem.cs
*/
using IGK.ICore.WinCore;
using IGK.ICore;
using IGK.ICore.ContextMenu;
using IGK.ICore.Drawing2D;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace IGK.ICore.WinUI
{
    /// <summary>
    /// represent a context menu item
    /// </summary>
    public class IGKXContextMenuItem : IGKXToolStripMenuItem, IXCoreContextMenuItemContainer
    {
        private ICore2DDrawingDocument m_document;
        private CoreContextMenuBase m_ContextMenuParent;
        public CoreContextMenuBase ContextMenuParent { get { return this.m_ContextMenuParent; } set { this.m_ContextMenuParent = value; } }
        public IGKXContextMenuItem()
        {
        }
        public void Add(object obj)
        {
            this.DropDownItems.Add(obj as ToolStripItem );
        }
        public void Remove(object obj)
        {
            this.DropDownItems.Remove (obj as ToolStripItem);
        }

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
        public new ICoreContextMenu Owner
        {
            get {
                return base.Owner as ICoreContextMenu;
                 }
        }
        public ICore2DDrawingDocument MenuDocument
        {
            get
            {
                return this.m_document;
            }
            set
            {
                this.m_document = value;
            }
        }
    }
}

