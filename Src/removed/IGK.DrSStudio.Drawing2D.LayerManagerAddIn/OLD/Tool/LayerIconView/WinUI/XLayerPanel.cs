

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XLayerPanel.cs
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
file:XLayerPanel.cs
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
/* 
-------------------------------------------------------------
This file is part of iGK-DEV-DrawingStudio
-------------------------------------------------------------
-------------------------------------------------------------
-------------------------------------------------------------
view license file in Documentation folder to get more info
Copyright (c) 2008-2010 
Author  : Charles A.D. BONDJE DOUE 
mail : bondje.doue@hotmail.com
-------------------------------------------------------------
-------------------------------------------------------------
*/
﻿using System;
using System.ComponentModel;
using System.Design;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms ;
using System.Windows.Forms .Design ;
namespace IGK.DrSStudio.Drawing2D.WinUI
{
    using IGK.ICore;using IGK.DrSStudio.Menu;
    using IGK.DrSStudio.Tools;
    using IGK.DrSStudio.Drawing2D;
    using IGK.DrSStudio.WinUI;
    [Designer(typeof(XLayerPanel.XLayerPanelDesigner))]    
    sealed class XLayerPanel : XUserControl, ILayerPanel
    {
        private LayerBlockCollection m_layersblock;
        private ICore2DDrawingDocument m_document; //document to configure
        private int m_selectedIndex;
        private UIXDocumentLayerOutlineControl owner;
        private const int BLOC_VSPACE = 2;
        /// <summary>
        /// get the layer block collection
        /// </summary>
        public LayerBlockCollection Layers {
            get {
                return m_layersblock;
            }
        }
        /// <summary>
        /// get or set the selected layer index
        /// </summary>
        [Browsable (false )]
        public int SelectedIndex { 
            get {
                return m_selectedIndex;
               }
            set {
                m_selectedIndex = value;
                OnSelectedIndexChanged(EventArgs.Empty);
            }
        }
        internal ICore2DDrawingDocument Document
        {
            get
            {
                return m_document;
            }
        }
        public event EventHandler SelectedIndexChanged;
        private void OnSelectedIndexChanged(EventArgs eventArgs)
        {
            if (this.m_document != null)
            {
                ICore2DDrawingLayer l = this.m_layersblock[SelectedIndex].Layer;
                this.m_document.CurrentLayer = l;
                if (this.SelectedIndexChanged != null)
                    this.SelectedIndexChanged(this, eventArgs);
            }
        }
        protected override void OnSizeChanged(EventArgs e)
        {
            this.SuspendLayout();
            foreach (ILayerBlock l in this.m_layersblock)
            {
                SetLayerBlockLocation(l);
            }
            this.ResumeLayout();
            base.OnSizeChanged(e);
        }
        void SetLayerBlockLocation(ILayerBlock l)
        {
            int offsety = (this.VerticalScroll.Visible) ? VerticalScroll.Value : 0;
            int w = (this.VerticalScroll.Visible) ? SystemInformation.VerticalScrollBarWidth  : 0;
            l.Location = new System.Drawing.Point(2 ,
                              5 - offsety + (XLayerBlock.DEFAULT_HEIGHT+  BLOC_VSPACE) * this.m_layersblock.IndexOf(l));
            l.Width = this.Width - 7 -w;
            l.Height = XLayerBlock.DEFAULT_HEIGHT;
        }
        #region ILayerPanel Members
        ILayerBlockCollection ILayerPanel.Layers
        {
            get { return m_layersblock; }
        }
        #endregion
        //.ctr
        public XLayerPanel(UIXDocumentLayerOutlineControl owner)
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            m_selectedIndex = -1;
            m_layersblock = new LayerBlockCollection(this);
            this.owner = owner;
        }
        public UIXDocumentLayerOutlineControl OwnerControl {
            get {
                return this.owner;
            }
        }
        public override string ToString()
        {
            return "LayerPanel : " + this.m_layersblock.Count;
        }
        internal void Configure(ICore2DDrawingDocument document)
        {
            if (document == m_document) return;
            UnRegisterDocumentEvent();
            m_document = document;
            RegisterDocumentEvent();
            ConfigurePanel();
        }
        private void ConfigurePanel()
        {
            this.m_layersblock.Clear();
            if (m_document == null) return;
            foreach (Core2DDrawingLayer l in m_document.Layers)
            {
                this.m_layersblock.Add(new XLayerBlock(this,l));
            }
            this.SelectedIndex = this.m_document.CurrentLayer.ZIndex;
        }
        private void RegisterDocumentEvent()
        {
            if (m_document == null) return;
            m_document.LayerAdded += new Core2DDrawingLayerEventHandler (m_document_LayerAdded);
        }
        private void UnRegisterDocumentEvent()
        {
            if (m_document == null) return;
            m_document.LayerAdded -= new Core2DDrawingLayerEventHandler(m_document_LayerAdded);
        }
        void m_document_LayerAdded(object o, Core2DDrawingLayerEventArgs e)
        {
            Core2DDrawingLayer l = e.Layer as Core2DDrawingLayer;
            this.m_layersblock.Add(new XLayerBlock(this, l));
        }
        //represent layer collection
        public class LayerBlockCollection : 
            ILayerBlockCollection
        {
            private List<ILayerBlock> m_list;
            private XLayerPanel m_owner;            
            //.ctr
            internal LayerBlockCollection(XLayerPanel m_owner)
            {
                this.m_owner = m_owner;
                m_list = new List<ILayerBlock>();                
            }
            #region ILayerBlockCollection Members
            public int Count
            {
                get { return m_list.Count; }
            }
            public System.Collections.IEnumerator GetEnumerator()
            {
                return m_list.GetEnumerator();
            }
            public void Add(ILayerBlock layerBlock)
            {
                if (layerBlock == null)
                    return;                
                m_list.Add(layerBlock);
                this.m_owner.SetLayerBlockLocation(layerBlock);
                this.m_owner.Controls.Add(layerBlock as Control);
            }
            public void Remove(ILayerBlock layerBlock)
            {
                if ((layerBlock == null) || (!Contains(layerBlock)))
                    return;                
                int v_index = IndexOf(layerBlock);
                m_list.Remove(layerBlock);
                for (int i = v_index; i < Count; i++)
                {
                    this.m_owner.SetLayerBlockLocation(m_list[i]);
                }
                this.m_owner.Controls.Remove(layerBlock as Control);
            }
            public bool Contains(ILayerBlock layerBlock)
            {
                return m_list.Contains(layerBlock);
            }
            public int IndexOf(ILayerBlock layerBlock)
            {
                return this.m_list.IndexOf(layerBlock);
            }
            public ILayerBlock this[int index]
            {
                get
                {
                    if ((index < 0) || (index >= Count))
                        return null;
                    return this.m_list[index];
                }
            }
            #endregion
            internal void Clear()
            {
                foreach (Control l in this.m_list)
                {
                    if (l != null)
                    {
                        this.m_owner.Controls.Remove(l);
                        l.Dispose();
                    }
                }
                m_list.Clear();                
            }
            internal void Insert(int p, XLayerBlock xLayerBlock)
            {
                int index = this.m_list.IndexOf(xLayerBlock);
                this.m_list.Remove(xLayerBlock);
                this.m_list.Insert(p, xLayerBlock);
                xLayerBlock.Y =
                        5 + (XLayerBlock.DEFAULT_HEIGHT + BLOC_VSPACE) * p;
                if (p < index)
                {
                        //5 + p * XLayerBlock.DEFAULT_HEIGHT;
                    //move the nex index front
                    this.m_list[p + 1].Y =
                            5 + (XLayerBlock.DEFAULT_HEIGHT + BLOC_VSPACE) * (p + 1);
                        //5 + (p + 1) * XLayerBlock.DEFAULT_HEIGHT;
                }
                else
                {
                    //move the next index back
                    this.m_list[p - 1].Y =
                        5 + (XLayerBlock.DEFAULT_HEIGHT + BLOC_VSPACE) * (p - 1);
                        //5 + (p - 1) * XLayerBlock.DEFAULT_HEIGHT;
                }
            }
        }
        /// <summary>
        /// layer desiser
        /// </summary>
        class XLayerPanelDesigner : ControlDesigner 
        {
            protected override void PreFilterProperties(System.Collections.IDictionary properties)
            {
                properties.Remove("Controls");                
                base.PreFilterProperties(properties);
            }
        }
    }
}

