

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XTreeview_LayerDocumentOutLine.cs
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
file:XTreeview_LayerDocumentOutLine.cs
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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace IGK.DrSStudio.WinUI
{
    using IGK.ICore;using IGK.DrSStudio.Drawing2D;
    using IGK.DrSStudio.Menu;
    using IGK.DrSStudio.Tools;
    using IGK.DrSStudio.WinUI;
    using IGK.DrSStudio.Core;
    using IGK.DrSStudio.Core.Layers;
    class XTreeview_LayerDocumentOutLine : XTreeView 
    {
        private ICore2DDrawingLayer m_layer;
        private Dictionary<ICoreWorkingObject, TreeNode> m_rElements;
        private bool m_configure; //configure flags
        const int IMG_WIDTH = 16;
        const int IMG_HEIGHT = 16;
        const string IMG_LAYER = "DE_layer";
        public XTreeview_LayerDocumentOutLine()
            : base()
        {
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            m_rElements = new Dictionary<ICoreWorkingObject, TreeNode>();            
            this.ImageList = new ImageList();
            this.ImageList.ImageSize = new System.Drawing.Size(IMG_WIDTH, IMG_HEIGHT);
            this.ImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.ImageList.ColorDepth = ColorDepth.Depth32Bit;
        }
        internal void SetLayer(ICore2DDrawingLayer layer)
        {
            ClearElement();
            UnregisterLayerEvent(m_layer);
            if (layer == null)
            {                
                m_layer = null;
            }
            else { 
                //polate node 
                if (m_layer != layer )
                {
                    m_configure = true;
                    m_layer = layer;
                    RegisterImageList(IMG_LAYER);
                    TreeNode topNode = CreateLayerNode(layer);
                    BuildLayerNode(topNode, layer);
                    topNode.ExpandAll();
                    m_rElements.Add(layer, topNode);
                    this.Nodes.Add(topNode);
                    if (layer.SelectedElements.Count == 1)
                    {
                        this.SelectedNode = topNode.Nodes[layer.SelectedElements[0].Id] ;
                    }
                    this.m_configure = false;
                }
            }
            RegisterLayerEvent(m_layer);
        }
        private void ClearElement()
        {
            Dictionary<ICoreWorkingObject, TreeNode>.Enumerator e = m_rElements.GetEnumerator();
            while (e.MoveNext())
            {
                //remove element property changed
                unRegisterPropertyChanged(e.Current.Key);
            }
            this.Nodes.Clear();
            this.m_rElements.Clear();
        }
        private void unRegisterPropertyChanged(ICoreWorkingObject obj)
        {
            if (obj is ICoreWorkingObjectPropertyEvent)
            {
                (obj as ICoreWorkingObjectPropertyEvent).PropertyChanged -= new CoreWorkingObjectPropertyChangedEventHandler(element_PropertyChanged);
            }
        }
        void layer_PropertyChanged(object o,CoreWorkingObjectPropertyChangedEventArgs e)
        {
            if (m_configure) return;
            m_configure = true;
            switch (e.ID )
            {
                case enuPropertyChanged.Definition:
                    if ((e.Params  != null)&& (e.Params.Length == 1))
                    {
                        int i = (int)e.Params[0];
                        switch (i)
                        {
                            case 0x10: //layer Clear
                                ICore2DDrawingLayer vl = m_layer;
                                m_layer = null;
                                SetLayer(vl);
                                break;
                        }
                    }
                    else { 
                        //check if id changed
                        TreeNode node = m_rElements[m_layer];
                        if (node != null)
                        {
                            if (m_layer.Id != node.Text )
                            {
                                node.Text = m_layer.Id;
                            }
                        }
                    }
                    break;
            }
            m_configure = false;
        }
        void element_PropertyChanged(object o, 
            CoreWorkingObjectPropertyChangedEventArgs  e)
        {
            if (m_configure) return;
            m_configure = true;
            switch (e.ID)
            {
                case enuPropertyChanged.Definition :
                case enuPropertyChanged.Id :
                    //check for id
                    ICore2DDrawingObject obj = o as ICore2DDrawingObject;
                    if ((obj != null) && (m_rElements.ContainsKey(obj)))
                    {
                        TreeNode node = m_rElements[obj];
                        if (node != null)
                        {
                            if (obj.Id != node.Text)
                            {
                                node.Text = obj.Id;
                            }
                        }
                    }
                    break;
            }
            m_configure = false;
        }
        private void RegisterLayerEvent(ICore2DDrawingLayer layer)
        {
            if (layer == null) return;
            layer.ElementAdded += new Core2DDrawingElementEventHandler(_layer_ElementAdded);
            layer.ElementRemoved += new Core2DDrawingElementEventHandler(_layer_ElementRemoved);
            layer.SelectedElementChanged += new EventHandler(m_layer_SelectedElementChanged);
            layer.ElementZIndexChanged += new CoreWorkingObjectZIndexChangedHandler(m_layer_ElementZIndexChanged);                
            layer.PropertyChanged += new CoreWorkingObjectPropertyChangedEventHandler(layer_PropertyChanged);
        }
        private void UnregisterLayerEvent(ICore2DDrawingLayer layer)
        {
            if (layer == null) return;
            layer.ElementAdded -= new Core2DDrawingElementEventHandler(_layer_ElementAdded);
            layer.ElementRemoved -= new Core2DDrawingElementEventHandler(_layer_ElementRemoved);
            layer.PropertyChanged -= new CoreWorkingObjectPropertyChangedEventHandler(layer_PropertyChanged);
            layer.SelectedElementChanged -= new EventHandler(m_layer_SelectedElementChanged);
            layer.ElementZIndexChanged -= new CoreWorkingObjectZIndexChangedHandler(m_layer_ElementZIndexChanged);
        }
        void m_layer_ElementZIndexChanged(object sender, CoreWorkingObjectZIndexChangedArgs e)
        {
            if (m_configure)
                return;
            m_configure = true;
            TreeNode node = m_rElements[e.Item];
            if (node != null)
            {
                TreeNode pNode = node.Parent;
                int v_index = node.Parent.Nodes.IndexOf(node);
                if (v_index > e.CurrentIndex )
                {
                    //move node down
                    pNode.Nodes.Remove(node);
                    pNode.Nodes.Insert(e.CurrentIndex, node);
                }
                else { 
                    //move node up
                    pNode.Nodes.Remove(node);
                    pNode.Nodes.Insert(v_index +1, node);
                }
            }
            this.SelectedNode = node;
            m_configure = false;   
        }
        protected override void OnAfterSelect(TreeViewEventArgs e)
        {
            base.OnAfterSelect(e);
            if (m_configure)
                return;
            m_configure = true;
            if (e.Node.Tag is Core2DDrawingObjectBase)
            {
                if (e.Node.Tag is ICore2DDrawingLayer)
                {
                    m_layer.Select(null);
                }
                else
                {
                    ICore2DDrawingLayeredElement v_el = e.Node.Tag as ICore2DDrawingLayeredElement;
                    if (this.m_layer.Elements.Contains(v_el))
                    {
                        m_layer.Select(v_el);
                    }
                    else {
                        m_layer.Select(null);
                        ICoreWorkingConfigElementSurface c = IGK.DrSStudio.Drawing2D.Tools.CoreTool_LayerOutline.Instance.CurrentSurface as ICoreWorkingConfigElementSurface;
                        if (c != null) c.ElementToConfigure = v_el;
                    }
                }
                this.LabelEdit = true;
            }
            else {
                this.LabelEdit = false;
                m_layer.Select(null);
            }
            m_configure = false;        
        }
        protected override void OnAfterLabelEdit(NodeLabelEditEventArgs e)
        {
            base.OnAfterLabelEdit(e);
            if (m_configure) 
                return;
            m_configure = true;
            if (e.Node.Tag is ICore2DDrawingLayeredElement)
            {
                if (string.IsNullOrEmpty(e.Label) == false)
                {
                    if (m_layer.ChangeIdOf(e.Node.Tag as ICore2DDrawingLayeredElement, e.Label) == false)
                    {
                        e.CancelEdit = true;
                    }
                }
                else
                {
                    e.CancelEdit = true;
                }
            }
            else if(e.Node .Tag == m_layer )
            {
                if (string.IsNullOrEmpty(e.Label) == false)
                {
                    if (m_layer.Parent.ChangeIdOf(m_layer, e.Label) == false) {
                        e.CancelEdit = true;
                    }
                }
                else {
                    e.CancelEdit = true;
                }
            }
            m_configure = false;
        }
        void m_layer_SelectedElementChanged(object sender, EventArgs e)
        {
            if ((!this.Visible) || (m_configure)) return;
            m_configure = true;
            if ((this.m_layer.SelectedElements.Count == 1)&&(m_rElements .ContainsKey (this.m_layer.SelectedElements[0])))
            {
                TreeNode node = m_rElements[this.m_layer.SelectedElements[0]];
                if (node != null)
                {
                    this.SelectedNode = node;
                }
            }
            else {
                this.SelectedNode = null;
            }
            m_configure = false;
        }
        void _layer_ElementRemoved(object o, Core2DDrawingElementEventArgs e)
        {
                if (m_rElements.ContainsKey(e.Element ))
                {
                    TreeNode node = m_rElements[e.Element ];
                    if (node.Parent != null)
                    {
                        node.Parent.Nodes.Remove(node);
                    }
                    else
                    {
                        this.Nodes.Remove(node);
                    }
                    for (int i = 0; i < node.Nodes.Count; i++)
                    {
                        if (node.Nodes[i].Tag is ICore2DDrawingElement)
                        {
                            ICore2DDrawingElement v_el = node.Nodes[i].Tag as ICore2DDrawingElement;
                            if (m_rElements.ContainsKey (v_el ))
                            {
                                m_rElements.Remove(v_el);
                            }
                        }                        
                    }
                    m_rElements.Remove(e.Element);
                }
                e.Element.PropertyChanged -= element_PropertyChanged;
        }
        //for element added
        void _layer_ElementAdded(object o, Core2DDrawingElementEventArgs e)
        {
                if (m_rElements.ContainsKey(e.Element ) == false)
                {
                    TreeNode node = CreateNode(e.Element );
                    if (node != null)
                    {
                        this.Nodes[0].Nodes.Insert(e.Element.ZIndex, node);
                    }
            }
        }
        private void BuildNode(TreeNode childNode, ICoreWorkingElementContainer  d)
        {
            TreeNode vlNode = null;            
            foreach (ICore2DDrawingLayeredElement  vl in d.Elements)
            {
                if (vl ==null)continue ;                
                vlNode = CreateNode(vl);    
                childNode.Nodes.Add(vlNode);             
            }
        }
        private void BuildNode(TreeNode node, ICore2DDocumentContainer document)
        {
            TreeNode childNode = null;
            ICore2DDrawingDocument v_layerDoc = document.Document;
            if (v_layerDoc !=null)
            foreach (Core2DDrawingLayer layer in v_layerDoc.Layers)
            {
                childNode = CreateLayerNode(layer);
                RegisterLayerEvent(layer);
                node.Nodes.Add(childNode);
                BuildLayerNode(childNode, layer);
            }
        }
        private TreeNode CreateLayerNode(ICore2DDrawingLayer layer)
        {
            TreeNode v_cnode = new TreeNode(layer.Id);            
            if (!this.ImageList.Images.ContainsKey(IMG_LAYER))
            {
                RegisterImageList(IMG_LAYER);
            }
            v_cnode.ImageKey = IMG_LAYER;
            v_cnode.SelectedImageKey = IMG_LAYER;
            v_cnode.Tag = layer;
            return v_cnode;
        }
        private void BuildLayerNode(TreeNode topNode, ICore2DDrawingLayer layer)
        {
            TreeNode childNode = null;
            foreach (ICore2DDrawingLayeredElement  l in layer.Elements)
            {
                childNode = CreateNode(l);
                if (childNode != null)
                {
                    topNode.Nodes.Add(childNode);
                }
            }
        }
        private void RegisterImageList(string p)
        {
            if (this.ImageList == null) 
                return;
            if (this.ImageList.Images.ContainsKey(p) == false)
            {
                System.Drawing.Bitmap bmp=  (System.Drawing.Bitmap)CoreResources.GetDocumentImage(p,
                    IMG_WIDTH, IMG_HEIGHT);
                if (bmp !=null)
                this.ImageList.Images.Add(p, bmp);
            }
        }
        /// <summary>
        /// create a tree nove view
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private TreeNode CreateNode(ICoreWorkingObject obj) 
        {
            if (obj == null)
                return null;
            CoreWorkingObjectAttribute  attrib = null;
            attrib =Attribute.GetCustomAttribute ( obj.GetType(),
                typeof (CoreWorkingObjectAttribute )) as CoreWorkingObjectAttribute ;          
            if (attrib == null) 
                return null;
            string imgKey = string.IsNullOrEmpty(attrib.ImageKey) ?
                string.Format("DE_{0}", attrib.Name) :
                attrib.ImageKey;
            TreeNode node = new TreeNode();
            node.Name = obj.Id;
            node.Text = obj.Id;
            node.Tag = obj;
            RegisterImageList(imgKey);
            node.ImageKey = imgKey;
            node.SelectedImageKey = imgKey;
            RegisterPropertyChanged(obj);
            if (obj is ICore2DDocumentContainer)
            {
                BuildNode(node, obj as ICore2DDocumentContainer);
            }
            else
            {
                if (obj is ICoreWorkingElementContainer)
                {
                    BuildNode(node, obj as ICoreWorkingElementContainer);
                }
                else if (obj is ICore2DDrawingSingleElementContainer)
                { 
                    TreeNode v_n = CreateNode( (obj as ICore2DDrawingSingleElementContainer).Element );    
                    node.Nodes.Add (v_n);
                }
            }
            if (m_rElements.ContainsKey (obj)==false )
            m_rElements.Add(obj, node);                            
            return node;
        }
        private void RegisterPropertyChanged(ICoreWorkingObject obj)
        {
            if (obj is ICoreWorkingObjectPropertyEvent)
            {
                (obj as ICoreWorkingObjectPropertyEvent).PropertyChanged += new CoreWorkingObjectPropertyChangedEventHandler(element_PropertyChanged);
            }
        }
        internal void RemoveItem(ICoreWorkingObject workingObject)
        {
            if (workingObject == null)
                return;
            if (this.m_rElements.ContainsKey(workingObject))
            {
                TreeNode node = this.m_rElements[workingObject];
                node.Parent.Nodes.Remove(node);
                this.m_rElements.Remove(workingObject);
                if (workingObject is ICore2DDrawingLayer)
                    this.UnregisterLayerEvent(workingObject as ICore2DDrawingLayer);
            }
        }
        internal void ToggleItem(CoreWorkingObjectZIndexChangedArgs e)
        {
            ICoreWorkingObject workingObject  =e.Item ;
            if (this.m_rElements.ContainsKey(workingObject))
            {
                TreeNode v_node = this.m_rElements[workingObject];
                TreeNode v_parent = v_node.Parent;
                if (v_parent != null)
                {
                    this.m_configure = true;
                    //this.SuspendLayout();
                    v_parent.Nodes.Remove(v_node);
                    v_parent.Nodes.Insert (e.CurrentIndex, v_node);
                    this.Refresh();
                    this.SelectedNode = v_node;
                    this.m_configure = false;
                }
            }
        }
    }
}

