

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKXPictureViewBrowser.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.ICore.WinCore;
using IGK.DrSStudio.WinUI;
using System.ComponentModel;
using System.Windows.Forms.Layout;
using System.Drawing;
using System.Windows.Forms;
using IGK.DrSStudio.Drawing2D;
using IGK.ICore;
using IGK.ICore.Drawing2D;
using IGK.ICore.WinUI;
using IGK.ICore.WinCore.WinUI.Controls;

namespace IGK.DrSStudio.WinUI
{
    [Designer(
        WinCoreConstant.CTRL_PANEL_DESIGNER )]
    /// <summary>
    /// represent a picture view explorer
    /// </summary>
    public class IGKXPictureViewBrowser : IGKXUserControl 
    {
        private FileExplorerCollection  m_Files;
        private string m_Directory;
        
        /// <summary>
        /// get or set directory
        /// </summary>
        public string Directory
        {
            get { return m_Directory; }
            set
            {
                if (m_Directory != value)
                {
                    m_Directory = value;
                    LoadDirectoryFiles();
                    OnDirectoryChanged(EventArgs.Empty);
                }
            }
        }
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

        }
        private void LoadDirectoryFiles()
        {
            this.Files.Clear();
            if (System.IO.Directory.Exists(this.Directory))
            {
                //this.Cursor =  Cursors.Wait; 
                Bitmap v_bmp = null;
                ICoreBitmap bmp = null;
                foreach (string file in System.IO.Directory.GetFiles(this.Directory))
                {
                    try{
                    using (v_bmp = new Bitmap(file))
                    {
                     Image cimag= v_bmp.GetThumbnailImage(300, 300, null, IntPtr.Zero);
                     bmp =WinCoreBitmap.Create(cimag as Bitmap);
                        //ICoreBitmap bmp =  IGK.DrSStudio.Resources.CoreResources.GetBitmapResourcesFromFile(file);
                    }
                    if (bmp != null)
                    {
                        Core2DDrawingLayerDocument v_doc = Core2DDrawingLayerDocument.CreateFromBitmap(bmp);
                        IGKXPictureViewBrowserFileItem c = new IGKXPictureViewBrowserFileItem(file, v_doc);
                        this.Files.Add(c);
                    }
                    }
                    catch{
                        CoreLog.WriteLine("Can't create file " + file);
                    }
                }
                    //this.Cursor = Cursors.Default;
            }
        }
        public event EventHandler DirectoryChanged;
        ///<summary>
        ///raise the OnDirectoryChanged 
        ///</summary>
        protected virtual void OnDirectoryChanged(EventArgs e)
        {
            if (DirectoryChanged != null)
                DirectoryChanged(this, e);
        }

        public FileExplorerCollection  Files
        {
            get { return m_Files; }
        }
        public IGKXPictureViewBrowser()
        {
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.m_Files = new FileExplorerCollection(this);
            this.m_Factor = 4.0f;
            this.Margin = System.Windows.Forms.Padding.Empty;
            this.Padding = System.Windows.Forms.Padding.Empty;
            this.AutoSize = true;
#if DEBUG
            for (int i = 0; i < 20; i++)
            {
                this.m_Files.Add(new IGKXPictureViewBrowserFileItem("no file", null));
            }
            this.InitLayout();
#endif
            this.Paint += _Paint;
            this.InitializeComponent();
        }

        void _Paint(object sender, CorePaintEventArgs e)
        {
            if (this.BackgroundDocument != null)
            {
                this.BackgroundDocument.Draw(e.Graphics, true,                    
                    new Rectanglei( this.ClientRectangle.X,
                    this.ClientRectangle.Y,
                    this.ClientRectangle.Width,
                    this.ClientRectangle.Height),
                    enuFlipMode.None);
            }
        }


        private void InitializeComponent()
        {
            
        }

       


        /// <summary>
        /// 
        /// </summary>
        public class FileExplorerCollection : IEnumerable
        {
            List<IGKXPictureViewBrowserFileItem> m_items;
            private IGKXPictureViewBrowser m_picViewBox;

            public FileExplorerCollection(IGKXPictureViewBrowser picBrowser)
            {                
                this.m_picViewBox = picBrowser;
                this.m_items = new List<IGKXPictureViewBrowserFileItem>();
            }
            internal void Add(IGKXPictureViewBrowserFileItem item)
            {
                if (this.m_items.CanAdd(item))
                {
                    this.m_picViewBox.Controls.Add(item);
                    this.m_items.Add(item);
                }
            }
            internal void Remove(IGKXPictureViewBrowserFileItem item)
            {
                if (m_items.CanRemove(item))
                {
                    this.m_items.Remove(item);
                    this.m_picViewBox.Controls.Remove (item);
                }
            }
            internal void Clear()
            {
                var e = this.m_items.ToArray();
                for (int i = 0; i < e.Length; i++)
                {
                    this.Remove(e[i]);
                }
            }
            public int Count { get { return this.m_items.Count; } }

            public IEnumerator GetEnumerator()
            {
                return this.m_items.GetEnumerator();
            }
        }
       
        
        private float m_Factor;

        
        [Category ("BrowserViewProperty")]
        public float Factor
        {
            get { return m_Factor; }
            set
            {
                if ((m_Factor != value) && ((value >= 1.0f)&& (value <=100.0f)))
                {

                    m_Factor = value;
                    this.InitLayout();
                    OnFatorChanged(EventArgs.Empty);
                }
            }
        }
        public event EventHandler FatorChanged;
        private PictureViewBrowserLayoutManager m_lEngine;
        ///<summary>
        ///raise the FatorChanged 
        ///</summary>
        protected virtual void OnFatorChanged(EventArgs e)
        {
            if (FatorChanged != null)
                FatorChanged(this, e);
        }

        public override LayoutEngine LayoutEngine
        {
            get
            {
                if (m_lEngine == null)
                    m_lEngine = new PictureViewBrowserLayoutManager(this);
                return m_lEngine;
            }
        }
        public PictureViewBrowserSelectedItemsCollection SelectedItems {
            get {
                if (m_selectedItem == null)
                    m_selectedItem = new PictureViewBrowserSelectedItemsCollection(this);
                return m_selectedItem;
            }
        }
        public class PictureViewBrowserSelectedItemsCollection
        {
            private IGKXPictureViewBrowser iGKXPictureViewBrowser;
            List<IGKXPictureViewBrowserFileItem> m_files;

            public PictureViewBrowserSelectedItemsCollection(IGKXPictureViewBrowser iGKXPictureViewBrowser)
            {                
                this.iGKXPictureViewBrowser = iGKXPictureViewBrowser;
                this.m_files = new List<IGKXPictureViewBrowserFileItem>();
            }
            /// <summary>
            /// get if this item contains in the collection
            /// </summary>
            /// <param name="item">item to check</param>
            /// <returns>true if element is in the collection or false</returns>
            public bool Contains(IGKXPictureViewBrowserFileItem item)
            {
                return this.m_files.Contains(item);
            }
        }
        protected class PictureViewBrowserLayoutManager  : LayoutEngine
        {
            private IGKXPictureViewBrowser m_owner;

            public PictureViewBrowserLayoutManager(IGKXPictureViewBrowser picBrowser)
            {
                this.m_owner = picBrowser;
            }
            public override void InitLayout(object child, BoundsSpecified specified)
            {
                base.InitLayout(child, specified);
            }
            public override bool Layout(object container, System.Windows.Forms.LayoutEventArgs layoutEventArgs)
            {
                int x = 5;
                int y = 5;
#pragma warning disable IDE0054 // Use compound assignment
                y = y - (this.m_owner.AutoScrollOffset.Y + this.m_owner.VerticalScroll.Value);
#pragma warning restore IDE0054 // Use compound assignment
                int fW =(int) Math.Ceiling ( this.m_owner.Factor * 32);
                int fH =(int) Math.Ceiling ( this.m_owner.Factor * 32);
                int max = this.m_owner.Width - fW -
                    (this.m_owner.VScroll ? - System.Windows.Forms.SystemInformation.VerticalScrollBarWidth +5:
                    5);
                foreach (IGKXPictureViewBrowserFileItem item in this.m_owner.Files)
                {
                    item.Margin = System.Windows.Forms.Padding.Empty;
                    item.Padding = System.Windows.Forms.Padding.Empty;
                    item.Bounds = new Rectangle(
                        x, y ,
                        fW,
                        fH
                        );
                    x += 5 + item.Width ;

                    if ( x > max )
                    {
                        x = 5;
                        y += 5 + fH;
                    }
                }
                return false;
            }
        }
        private ICore2DDrawingDocument m_BackgroundDocument;
        private PictureViewBrowserSelectedItemsCollection m_selectedItem;

        public ICore2DDrawingDocument BackgroundDocument
        {
            get { return m_BackgroundDocument; }
            set
            {
                if (m_BackgroundDocument != value)
                {
                    m_BackgroundDocument = value;
                    this.Invalidate();
                }
            }
        }

        public bool IsSelected(IGKXPictureViewBrowserFileItem item)
        {
            return (this.SelectedItems.Contains(item));
        }
    }
}
