

/*
IGKDEV @ 2008-2016
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
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:DocumentBlockElement.cs
*/
using IGK.ICore;using IGK.ICore.Codec;
using IGK.ICore.WinUI.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.ICore.Drawing2D
{
    [Core2DDrawingStandardElement("DocumentBlock",
    null, IsVisible=false  )]
    public class DocumentBlockElement : 
        Core2DDrawingDualBrushElement,        
        ICore2DDocumentContainer
    {
        private Rectanglef m_Bounds;

       
        protected override void BuildBeforeResetTransform()
        {
            base.BuildBeforeResetTransform();
        }
        public override bool CanRenderShadow
        {
            get
            {
                return false;
            }
        }
        protected internal override void Translate(float dx, float dy, enuMatrixOrder order, bool temp)
        {
            base.Translate(dx, dy, order, temp);
        }
        public DocumentBlockElement():base()
        {
        }
        public static DocumentBlockElement FromFile(string filename)
        {
            DocumentBlockElement doc = null;
            ICore2DDrawingDocument[] v_docs = CoreDecoder.Instance.Open2DDocument(filename);
            if ((v_docs != null) && (v_docs.Length > 0))
            {
                Core2DDrawingDocumentBase v_tdoc = v_docs[0] as Core2DDrawingDocumentBase ;
                if (v_tdoc !=null)
                {
                    doc = new DocumentBlockElement();
                    doc.Document = v_tdoc;
                }
            }
            return doc;
        }
        public Core2DDrawingDocumentBase  Document
        {
            get { return m_Document; }
            private set
            {
                if (m_Document != value)
                {
                    if (this.m_Document != null)
                        UnregisterDocumentEvent();
                    this.m_Document = value;
                    if (this.m_Document != null)
                    {
                        //this.m_Document.Parent = this;
                        this.m_Bounds = new Rectanglef(0, 0, value.Width, value.Height);
                        if (!this.IsLoading)
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
        
        private Core2DDrawingDocumentBase m_Document;
        
        public override ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
        {
            parameters = base.GetParameters(parameters);
            var tab = parameters.AddTab("Document", "lb.Document.caption");
            tab.AddConfigObject(this.m_Document);
            var group = parameters.AddGroup("Document");
           // group.AddActions(new CoreParameterEditElementAction("EditElement", "lb.EditElement.caption", this.m_Document));
            return parameters;
        }
        public static DocumentBlockElement CreateElement(ICore2DDrawingDocument document)
        {
            if ((document == null) || (document.IsDisposed) && (document is Core2DDrawingDocumentBase))
            {
                return null;
            }
            DocumentBlockElement m_doc = new DocumentBlockElement();
            m_doc.Document = document as Core2DDrawingDocumentBase;
            return m_doc;
        }
        /// <summary>
        /// create document block from files
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static DocumentBlockElement[] CreateElementFromFile(string filename)
        {
            ICore2DDrawingDocument[] d = IGK.ICore.Codec.CoreDecoder.Instance.Open2DDocument(filename);
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

        protected override void WriteElements(IXMLSerializer xwriter)
        {
            base.WriteElements(xwriter);
            if (this.Document != null)
            {
                (this.Document as ICoreSerializerService).Serialize(xwriter);
            }
            else
            {
                //protected element
                ICore2DDrawingLayer p = this.ParentLayer;
                if (p != null)
                    p.Elements.Remove(this);
            }
        }
        protected override void ReadAttributes(IXMLDeserializer xreader)
        {
            base.ReadAttributes(xreader);
        }
        protected override void ReadElements(IXMLDeserializer xreader)
        {
            //base.ReadElements(xreader);
            xreader.MoveToElement();
            if (this.m_Document !=null)
                this.m_Document.Dispose();
            ICoreWorkingObject[] c = CoreXMLSerializerUtility.GetAllObjects(xreader);
            if ((c != null) && (c.Length == 1) && (c[0] is Core2DDrawingDocumentBase))
                this.m_Document = c[0] as Core2DDrawingDocumentBase;
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
        protected override void InitGraphicPath(CoreGraphicsPath path)
        {
            path.Reset();
            if (this.m_Document != null)
            {
                this.m_Bounds = new Rectanglef(0, 0, this.m_Document.Width, this.m_Document.Height);
                path.AddRectangle(this.m_Bounds);
            }
        }
        public override Rectanglef GetBound()
        {
            return  base.GetBound();
        }
        public Rectanglef Bounds
        {
            get { return m_Bounds; }
        }
        ICore2DDrawingDocument ICore2DDocumentContainer.Document
        {
            get { return this.Document; }
        }
    }
}

