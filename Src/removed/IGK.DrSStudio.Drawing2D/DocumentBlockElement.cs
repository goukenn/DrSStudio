

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: DocumentBlockElement.cs
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
file:DocumentBlockElement.cs
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
using System.Drawing.Drawing2D;
using System.Drawing;
using IGK.ICore;using IGK.DrSStudio.Codec;
using IGK.DrSStudio.WinUI.Configuration;
namespace IGK.DrSStudio.Drawing2D
{
    [Core2DDrawingStandardItem("DocumentBlock",
        typeof(Mecanism),
        IsVisible = false)]
    public class DocumentBlockElement :
        Core2DDrawingDualBrushBoundElement,
        ICore2DDocumentContainer
    {
        private ICore2DDrawingDocument m_Document;
        public override bool CanRenderShadow
        {
            get
            {
                return false;
            }
        }
        public DocumentBlockElement()
        {
            this.FillBrush.SetSolidColor(Colorf.Transparent);
            this.StrokeBrush.SetSolidColor(Color.Transparent);
        }
        public static DocumentBlockElement FromFile(string filename)
        {
            DocumentBlockElement doc = null;
            ICore2DDrawingDocument[] v_docs = CoreDecoder.Instance.Open2DDocument (filename);
            if ((v_docs != null) && (v_docs.Length > 0))            
            {
                doc = new DocumentBlockElement();
                doc.m_Document = v_docs[0];
            }
            return doc;
        }
        public ICore2DDrawingDocument Document
        {
            get { return m_Document; }
            private set {
                if (m_Document != value)
                {
                    if (this.m_Document != null)
                        UnregisterDocumentEvent();
                    this.m_Document = value;
                    if (this.m_Document != null)
                    {
                        this.m_Document.Parent  = this;
                        this.m_Bound = new Rectanglei(0, 0, value.Width, value.Height);
                        if (!this.IsLoading )
                        this.InitElement();
                        this.RegisterDocumentEvent();
                    }
                }
            }
        }
        private void UnregisterDocumentEvent()
        {
            this.Document.PropertyChanged -= new CoreWorkingObjectPropertyChangedEventHandler(m_Document_PropertyChanged);
        }
        private void RegisterDocumentEvent()
        {
            this.Document.PropertyChanged += new CoreWorkingObjectPropertyChangedEventHandler(m_Document_PropertyChanged);
        }
        void m_Document_PropertyChanged(object o, CoreWorkingObjectPropertyChangedEventArgs e)
        {
            this.OnPropertyChanged(e);
        }
        private Rectanglei m_Bound;
        public override Rectanglef GetBound()
        {
            return base.GetBound();
        }
        public new Rectanglef Bound
        {
            get { return m_Bound; }
        }
        protected override void GeneratePath()
        {
            if ((this.Document == null) || (this.Document.IsDisposed))
            {
                this.SetPath(null);
                return;
            }
            CoreGraphicsPath v_p = new CoreGraphicsPath();
            v_p.AddRectangle(m_Bound);
            this.SetPath(v_p);
        }
        protected override void BuildBeforeResetTransform()
        {
            base.BuildBeforeResetTransform();
        }
        public override IGK.DrSStudio.WinUI.ICoreParameterConfigCollections GetParameters(IGK.DrSStudio.WinUI.ICoreParameterConfigCollections parameters)
        {
            parameters = base.GetParameters(parameters);
            var tab = parameters.AddTab("Document", "lb.Document.caption");
            tab.AddConfigObject(this.m_Document);
            var group = parameters.AddGroup("Document");
            group.AddActions(new CoreParameterEditElementAction("EditElement", "lb.EditElement.caption", this.m_Document ));
            return parameters;
        }
        protected override void OnParentChanged(EventArgs eventArgs)
        {
            //propage the document parent
            if (this.Document != null)
                this.Document.Parent  = this;
            base.OnParentChanged(eventArgs);
        }
        public static DocumentBlockElement CreateElement(ICore2DDrawingDocument document)
        {
            if ((document == null) || (document.IsDisposed))
            {
                return null ;
            }
            DocumentBlockElement m_doc = new DocumentBlockElement();
            m_doc.Document = document;
            return m_doc;
        }
        /// <summary>
        /// create document block from files
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static DocumentBlockElement[] CreateElementFromFile(string filename)
        {
            ICore2DDrawingDocument[] d = IGK.DrSStudio.Codec.CoreDecoder.Instance.Open2DDocument(filename);
            if (d == null)
                return null;
            List<DocumentBlockElement> v_block = new List<DocumentBlockElement>();
            DocumentBlockElement l = null;
            for (int i = 0; i < d.Length; i++)
            {
                l = CreateElement(d[i]);
                if (l != null)
                    v_block.Add(l);
            }
            return v_block.ToArray();
        }
        protected override void WriteElements(IGK.DrSStudio.Codec.IXMLSerializer xwriter)
        {
            base.WriteElements(xwriter);
            if (this.Document != null)
            {
                this.Document.Serialize(xwriter);
            }
            else {
                //protected element
                ICore2DDrawingLayer p = this.ParentLayer;
                if (p !=null)
                    p.Elements.Remove(this);
            }
        }
        protected override void ReadElements(IGK.DrSStudio.Codec.IXMLDeserializer xreader)
        {            
            //base.ReadElements(xreader);
            xreader.MoveToElement();
            string v_txt = xreader.ReadOuterXml();
            System.IO.TextReader tx = new System.IO.StringReader(v_txt);
            System.IO.TextReader mx = new System.IO.StringReader(v_txt);
            IGK.DrSStudio.Codec.IXMLDeserializer h = IGK.DrSStudio.Codec.CoreXMLDeserializer.Create( System.Xml.XmlReader.Create (tx));
            IGK.DrSStudio.Codec.IXMLDeserializer t = IGK.DrSStudio.Codec.CoreXMLDeserializer.Create(System.Xml.XmlReader.Create(mx));
            IGK.DrSStudio.Codec.IXMLDeserializer v_sub = t;
            base.ReadElements(h);
            if (!v_sub.IsEmptyElement)
            {
                if (v_sub.NodeType == System.Xml.XmlNodeType.None)
                    v_sub.MoveToContent();
                bool found = false;
                while (!found && v_sub.Read())
                {
                    switch (v_sub.NodeType)
                    {
                        case System.Xml.XmlNodeType.Element:
                            ICore2DDrawingDocument
                                       obj = CoreSystem.CreateWorkingObject(v_sub.Name)
                                       as ICore2DDrawingDocument;
                            if (obj != null)
                            {
                                obj.Deserialize(v_sub.ReadSubtree());
                                this.Document = obj;
                                found = true;                              
                            }
                            else if (!v_sub.IsEmptyElement)
                            {
                                v_sub.Skip();
                            }
                            break;
                    }
                }
                h.Close();
                t.Close();
            }
        }
        public override void Dispose()
        {
            this.DisposeDocument();
            base.Dispose();
        }
        private void DisposeDocument()
        {
            if (this.Document != null)
            {
                this.Document.Dispose();
            }
        }
        public override void Draw(Graphics g)
        {
            if (this.Document == null)
                return;
            Matrix mat = GetMatrix();
            Rectanglef v_rc = this.m_Bound;// this.GetBound();
            GraphicsState s = g.Save();
            this.SetGraphicsProperty(g);
            Region rg = new Region(v_rc);
            //g.IntersectClip(rg);
           // rg.Transform(mat);
            //g.SetClip(rg, CombineMode.Replace);
            g.MultiplyTransform(mat);
            g.IntersectClip(rg);
            Brush  v_cb = this.FillBrush.GetBrush ();
            if (v_cb != null)
            {
                BrushCorrection vb = BrushCorrection.Correct(v_cb , g.Transform , mat );
                if (vb != null)
                {
                    vb.Save();
                    vb.InvertAndMult(mat);
                    g.FillRectangle(vb.Brush, v_rc);
                    vb.Restore();
                    vb.Dispose();
                }
                else
                    g.FillRectangle(v_cb, v_rc);
            }
            //if (v_cb !=null)
            //    g.FillRectangle(v_cb, this.m_Bound.X, this.m_Bound.Y, this.m_Bound.Width, this.m_Bound.Height);
            this.Document.Draw(g);
            Pen v_cp = this.StrokeBrush.GetPen();
            //g.FillRegion(Brushes.Red, rg);
            g.ExcludeClip(rg);
            g.ResetClip();
            //this.SetGraphicsProperty(g);
            if (v_cp !=null)
                g.DrawRectangle(v_cp, this.m_Bound.X, this.m_Bound.Y, this.m_Bound.Width, this.m_Bound.Height);
            g.Restore(s);
            rg.Dispose();
        }
        sealed new class Mecanism : Core2DDrawingMecanismBase
        {
        }
        public override enuBrushSupport BrushSupport
        {
            get { return enuBrushSupport.All ; }
        }
        public override ICoreBrush GetBrush(enuBrushMode mode)
        {
            return base.GetBrush(mode);
        }
    }
}

