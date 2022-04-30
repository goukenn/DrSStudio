

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKXContextMenuStrip.cs
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
file:XContextMenuStrip.cs
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace IGK.DrSStudio.WinUI
{
    
    using IGK.DrSStudio.Menu;
    using IGK.DrSStudio.Settings;
    using IGK.ICore;
    using IGK.ICore.ContextMenu;
    using IGK.ICore.Settings;
    using IGK.ICore.WinUI;
    using IGK.ICore.WinCore;
    using IGK.ICore.WinCore.WinUI;

    /// <summary>
    /// represent a win core context menu string
    /// </summary>
    public class IGKXContextMenuStrip : ContextMenuStrip, ICoreContextMenu
    {
        private object  m_syncObj = new object ();

        public IGKXContextMenuStrip()
        {
            this.Renderer = new XWinCoreContextMenuRenderer();
            this.Opening += _Opening;
            this.Closing += XContextMenuStrip_Closing;
            this.Closed += _Closed;
            this.ItemAdded += _ItemAdded;
            this.ItemRemoved += _ItemRemoved;

         
            this._initFont();
        }
        private void _initFont()
        {
            this.Font = WinCoreControlRenderer.ContextMenuFont.ToGdiFont();
            WinCoreControlRenderer.ContextMenuFont.FontDefinitionChanged += (o, e) =>
            {
                this.Font = WinCoreControlRenderer.ContextMenuFont.ToGdiFont();
            };
        }
        private void _ItemRemoved(object sender, ToolStripItemEventArgs e)
        {
            if ((m_itemRemoved != null)&&(e.Item is IGKXContextMenuItem))
            {
                CoreContextMenuBase ee = (e.Item as IGKXContextMenuItem).ContextMenuParent;
                m_itemRemoved(this, new CoreItemEventArgs<CoreContextMenuBase>(ee));
            }
        }

        private void _ItemAdded(object sender, ToolStripItemEventArgs e)
        {
            if ((m_itemRemoved != null) && (e.Item is IGKXContextMenuItem))
            { 
                CoreContextMenuBase ee = (e.Item as IGKXContextMenuItem ).ContextMenuParent;
                m_itemAdded (this, new CoreItemEventArgs<CoreContextMenuBase> (ee));
            }
        }
        private EventHandler<CoreContextClosedEventArgs> m_closed;
        private void _Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            if (m_closed != null)
            {
                CoreContextClosedEventArgs ee = new CoreContextClosedEventArgs((enuContextCloseReason)e.CloseReason);
                m_closed(this, ee);
            }
        }
        private EventHandler<CoreClosingEventArgs> m_closing;
        void XContextMenuStrip_Closing(object sender, ToolStripDropDownClosingEventArgs e)
        {
            if (this.m_closing != null)
            { 
                CoreClosingEventArgs ee = new CoreClosingEventArgs (e.Cancel ,(enuContextCloseReason) e.CloseReason );
                this.m_closing(this, ee);
            }
        }
        void _Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (this.m_opening != null)
            {
                var ee = new CoreCancelEventArgs(e.Cancel);
                this.m_opening(this, ee);
                e.Cancel = ee.Cancel ;
            }
        }

      
        public new ICoreContextMenuOwner ContextMenu
        {
            get { return null; }
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
                m_syncObj = null;
            base.Dispose(disposing);
        }
        public new ICoreControl SourceControl
        {
            get {
                lock (m_syncObj)
                {
                    try
                    {
                        return base.SourceControl as ICoreControl;
                    }
                    catch { 
                        
                    }
                }
                return null;

            }
        }
        public Vector2f MouseOpeningLocation
        {
            get { return m_mouseOpeningLocation; }
        }
        public new ICoreContextMenuCollections Items
        {
            get { return base.Items as ICoreContextMenuCollections; }
        }
        protected override Control.ControlCollection CreateControlsInstance()
        {
             Control.ControlCollection c = base.CreateControlsInstance();             
             return c;
        }
        public event EventHandler CheckForVisibility;
        ///<summary>
        ///raise the CheckForVisibility 
        ///</summary>
        protected virtual void OnCheckForVisibility(EventArgs e)
        {
            if (CheckForVisibility != null)
                CheckForVisibility(this, e);
        }
      
        protected override void OnOpening(System.ComponentModel.CancelEventArgs e)
        {
            this.m_mouseOpeningLocation = CoreApplicationManager.Application.ControlManager.MouseLocation;
            base.OnOpening(e);
        }
        //public new event EventHandler Closing;
        /////<summary>
        /////raise the Closing 
        /////</summary>
        //protected virtual void OnClosing(EventArgs e)
        //{
        //    if (Closing != null)
        //        Closing(this, e);
        //}
        //public new event EventHandler ItemAdded;
        /////<summary>
        /////raise the ItemAdded 
        /////</summary>
        //protected virtual void OnItemAdded(EventArgs e)
        //{
        //    if (ItemAdded != null)
        //        ItemAdded(this, e);
        //}
        //public new event EventHandler ItemRemoved;
        private Vector2i m_mouseOpeningLocation;
        /////<summary>
        /////raise the ItemRemoved 
        /////</summary>
        //protected virtual void OnItemRemoved(EventArgs e)
        //{
        //    if (ItemRemoved != null)
        //        ItemRemoved(this, e);
        //}
        /// <summary>
        /// get the number item present in this
        /// </summary>
        public int Count
        {
            get { return base.Items.Count ; }
        }
        public int IndexOf(IXCoreContextMenuItemContainer iXCoreContextMenuItem)
        {
            return base.Items.IndexOf(iXCoreContextMenuItem as ToolStripItem );
        }
        public void Insert(int i, IXCoreMenuItemSeparator item)
        {
            if (item is ToolStripItem)
            base.Items.Insert(i, item as ToolStripItem);
        }
        public void Add(CoreContextMenuBase menu)
        {
            if ((menu !=null) && (menu.MenuItem is ToolStripMenuItem))
            base.Items.Add(menu.MenuItem as ToolStripMenuItem);
        }
        public void Add(IXCoreMenuItemSeparator separator)
        {
            if (separator is ToolStripItem)
                base.Items.Add(separator as ToolStripItem);
        }
        public void Remove(CoreContextMenuBase menu)
        {
            base.Items.Remove(menu.MenuItem as ToolStripMenuItem);
        }
        protected override void SetVisibleCore(bool visible)
        {
            if (visible)
            {
                OnCheckForVisibility(EventArgs.Empty);
                bool v_s = false;
                foreach (ToolStripItem item in base.Items)
                {
                    if (item.Available)
                    {
                        v_s = true;
                        break;
                    }
                }
                if ((this.Bounds.Height < this.MaxItemSize.Height) || (!v_s && (this.SourceControl != null)))
                {
                    base.SetVisibleCore(false);
                    return;
                }
            }
            base.SetVisibleCore(visible);
        }


        public new ICoreContextMenu ContextMenuStrip
        {
            get {
                return null;
            }
        }

        ICoreMenuItemCollections ICoreContextMenu.Items
        {
            get { throw new NotImplementedException(); }
        }
        EventHandler<CoreCancelEventArgs> m_opening;
        event EventHandler<CoreCancelEventArgs> ICoreContextMenu.Opening {
            add {
                m_opening += value;
            }
            remove {
                m_opening -= value;
            }
        }
     
        EventHandler<CoreItemEventArgs<CoreContextMenuBase>> m_itemAdded;
        EventHandler<CoreItemEventArgs<CoreContextMenuBase>> m_itemRemoved;
        event EventHandler<CoreItemEventArgs<CoreContextMenuBase>> ICoreContextMenu.ItemAdded{
            add {
                m_itemAdded += value;
            }
            remove {
                m_itemAdded -= value;
            }
        }

        event EventHandler<CoreItemEventArgs<CoreContextMenuBase>> ICoreContextMenu.ItemRemoved
        {
            add
            {
                m_itemRemoved += value;
            }
            remove
            {
                m_itemRemoved -= value;
            }
        }


        event EventHandler<CoreClosingEventArgs> ICoreContextMenu.Closing { 
            add{
                m_closing +=value;
            }
            remove {
                m_closing -= value;
            }
        }

        event EventHandler<CoreContextClosedEventArgs> ICoreContextMenu.Closed {
            add {
                m_closed += value;
            }
            remove {
                m_closed -= value;
            }
        }


      
    }
}

