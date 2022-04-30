

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XMLEditorSurface.cs
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
file:XMLEditorSurface.cs
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
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using IGK.ICore.WinCore.WinUI.Controls;
namespace IGK.DrSStudio.XMLEditorAddIn.WinUI
{
    [CoreSurface ("XMLTextEditor")]
    /// <summary>
    /// represent the text user surface
    /// </summary>
    class XMLEditorSurface : 
        IGKXWinCoreWorkingSurface , 
        IXMLEditorSurface 
    {
        private XMLEditorTextArea c_area;
        private string m_fileName;
        private bool m_needToSave;
        private IXMLEditorMarginCollections m_Margins;
        private IXMLEditorDocument m_document;
        private IGKXHorizontalScrollBar c_hScroll;
        private IGKXVerticalScrollBar  c_vScroll;
        public event MarginEventHandler MarginAdded;
        public event MarginEventHandler MarginRemoved;

        private bool m_Saving;

        /// <summary>
        /// get if this surface is in saving mode
        /// </summary>
        public bool Saving
        {
            get { return m_Saving; }
            protected set
            {
                if (m_Saving != value)
                {
                    m_Saving = value;
                }
            }
        }

        public event EventHandler Saved;

        /// <summary>
        /// raise the saved eventhandler
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnSaved(EventArgs e)
        {
            this.NeedToSave = false;
            if (this.Saved != null)
                this.Saved(this, e);
        }

        public int ScrollX { get { return this.c_hScroll.Value; } }
        public int ScrollY { get { return this.c_vScroll.Value; } }
        public event EventHandler ScrollChanged;
        protected virtual void OnScrollChanged(EventArgs e)
        {
            if (this.ScrollChanged != null)
                this.ScrollChanged(this, e);
        }
     
        /// <summary>
        /// event raisend when document need to be save
        /// </summary>
        public event EventHandler NeedToSaveChanged;
        /// <summary>
        /// get or set if this document need to save
        /// </summary>
        public bool NeedToSave {
            get { return this.m_needToSave; }
            set { this.m_needToSave = value; OnNeedToSaveChanged(EventArgs.Empty); }
        }
        /// <summary>
        /// represent the document to edit
        /// </summary>
        public IXMLEditorDocument Document { get { return this.m_document; } }
        protected virtual void OnNeedToSaveChanged(EventArgs e)
        {
            if (this.NeedToSaveChanged != null)
                this.NeedToSaveChanged(this, e);
        }
        /// <summary>
        /// get or set the filename
        /// </summary>
        public string FileName {
            get { return this.m_fileName; }
            set { this.m_fileName = value; OnFileNameChanged(EventArgs.Empty); }
        }
        public virtual void RenameTo(string newfilename)
        {
            if (string.IsNullOrEmpty(newfilename))
                return;
            string p = this.FileName;
            System.IO.File.Delete(p);
            this.FileName = newfilename;
            this.Save();
        }
        private void OnFileNameChanged(EventArgs eventArgs)
        {
            if (this.FileNameChanged != null)
                this.FileNameChanged(this, eventArgs);
        }
        public XMLEditorSurface()
        {
            InitControl();
        }
        private void InitControl()
        {
            c_area = new XMLEditorTextArea(this);
            c_hScroll = new IGKXHorizontalScrollBar();
            c_vScroll = new IGKXVerticalScrollBar ();
            this.m_fileName = "empty.xml";            
            this.c_hScroll.Visible = true;
            this.c_vScroll.Visible = true;
            this.m_document = CreateNewDocument();
            this.Controls.Add(c_area);
            this.Controls.Add(c_vScroll);
            this.Controls.Add(c_hScroll);
            this.c_vScroll.ValueChanged += c_vScroll_Scroll;
            this.c_hScroll.ValueChanged  += c_hScroll_Scroll;
            this.Margins.Add(new Margin.MarkerMargin(this));
            this.InitControlBound();
        }
        protected override void OnMouseWheel(System.Windows.Forms.MouseEventArgs e)
        {
            base.OnMouseWheel(e);
        }
        void c_hScroll_Scroll(object sender, EventArgs e)
        {
            this.OnScrollChanged(EventArgs.Empty);
        }
        void c_vScroll_Scroll(object sender, EventArgs e)
        {
            this.OnScrollChanged(EventArgs.Empty);
        }
        protected override void OnSizeChanged(EventArgs e)
        {
            this.InitControlBound();
            base.OnSizeChanged(e);
        }
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
        }
        private void InitControlBound()
        {
            //set scroll bound
            this.SuspendLayout();
            int h = System.Windows.Forms.SystemInformation.HorizontalScrollBarHeight ;
            int w = System.Windows.Forms.SystemInformation.VerticalScrollBarWidth ;
            this.c_vScroll.Bounds = new System.Drawing.Rectangle(this.Width - w,
                0, w, this.Height - h);
            this.c_hScroll.Bounds = new System.Drawing.Rectangle(0,
                this.Height -h, this.Width - w, h);
            int x = 0;
            int H = this.Height - h;
            foreach (IXMLEditorMargin  margin in this.Margins)
            {
                margin.Bounds = new System.Drawing.Rectangle(x,
                    0,
                    margin.GetPreferredWidth(),
                    H);
                x += margin.Bounds.Width;
            }
            this.c_area.Bounds = new System.Drawing.Rectangle(
                x, 0,
                this.Width - (x + w) ,
                H
                );
            this.ResumeLayout();
            this.Dock = System.Windows.Forms.DockStyle.Fill;
        }
        private IXMLEditorDocument CreateNewDocument()
        {
            XMLEditorDocument v_doc = new XMLEditorDocument();            
            return v_doc;
        }
        public void Save()
        {
            this.c_area.Save();
        }
        public void SaveAs(string filename)
        {
            this.c_area.SaveAs(filename );
        }
        public void Print()
        { }
        public void PrintPreview()
        { }
     
        internal static XMLEditorSurface Open(string f)
        {
            XMLEditorDocument doc = XMLEditorDocument.Load(f);
            XMLEditorSurface v_surface= new XMLEditorSurface();
            v_surface.m_document = doc;
            v_surface.FileName = f;
            v_surface.NeedToSave = false;
            return v_surface;
        }
        public static XMLEditorSurface CreateSurface(CoreWorkingProjectTemplateAttribute project)
        {
            XMLEditorSurface editor = new XMLEditorSurface ();
            return editor;
        }
        /// <summary>
        /// set initialisator
        /// </summary>
        /// <param name="p"></param>
        public override  void SetParam(ICoreInitializatorParam p)
        {
            this.NeedToSave = false;
        }
        protected virtual IXMLEditorMarginCollections CreateMargins()
        {
            return new EditorMarginCollections(this);
        }
        /// <summary>
        /// represent the margin collections
        /// </summary>
        class EditorMarginCollections : IXMLEditorMarginCollections
        {
            private XMLEditorSurface m_owner;
            private List<IXMLEditorMargin> m_margins;
            internal EditorMarginCollections(XMLEditorSurface  owner)
            {
                this.m_margins = new List<IXMLEditorMargin>();
                this.m_owner = owner;
            }
            #region IXMLEditorMarginCollections Members
            public void Add(IXMLEditorMargin margin)
            {
                if ((margin != null) && (!this.m_margins.Contains(margin)))
                {
                    this.m_margins.Add(margin);
                    this.m_owner.OnMarginAdded(new MarginEventArgs(margin));
                }
            }
            public void Remove(IXMLEditorMargin margin)
            {
                if (this.m_margins.Contains(margin))
                {
                    this.m_margins.Remove(margin);
                    this.m_owner.OnMarginRemoved(new MarginEventArgs(margin));
                }
            }
            public int Count
            {
                get { return this.m_margins.Count; }
            }
            public IXMLEditorMargin[] ToArray()
            {
                return this.m_margins.ToArray();
            }
            #endregion
            #region IEnumerable Members
            public System.Collections.IEnumerator GetEnumerator()
            {
                return this.m_margins.GetEnumerator();
            }
            #endregion
        }
        protected virtual void OnMarginAdded(MarginEventArgs  e)
        {
            if (e.Margin is Control)
            {
                this.Controls.Add(e.Margin as Control );
            }
            if (this.MarginAdded != null)
                this.MarginAdded(this, e);
        }
        protected virtual void OnMarginRemoved(MarginEventArgs e)
        {
            if (e.Margin is Control)
            {
                this.Controls.Remove(e.Margin as Control);
            }
            if (this.MarginRemoved != null)
                this.MarginRemoved(this, e);
        }
        #region IXMLEditorSurface Members
        public IXMLEditorMarginCollections Margins
        {
            get {
                if (this.m_Margins == null)
                {
                    this.m_Margins = CreateMargins();
                }
                return this.m_Margins;
            }
        }
        #endregion
        #region ICoreWorkingFilemanagerSurface Members
        public event EventHandler FileNameChanged;
        #endregion
        public string GetDefaultFilter()
        {
            return "XML Files | .xml";
        }
        public ICoreSaveAsInfo GetSaveAsInfo()
        {
            return new CoreSaveAsInfo(
                "title.SaveXMLFile".R(),
                GetDefaultFilter(),
                this.FileName);
        }

        public bool CanPrint
        {
            get { return true; }
        }


        public void ReloadFileFromDisk()
        {
        }
    }
}

