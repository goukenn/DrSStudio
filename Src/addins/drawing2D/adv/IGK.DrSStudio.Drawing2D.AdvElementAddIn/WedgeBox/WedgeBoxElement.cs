

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WedgeBoxElement.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿
using IGK.ICore.Drawing2D;
using System; using IGK.ICore; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.ICore.WinUI.Configuration;
using IGK.ICore.Actions;
using IGK.ICore.WinUI;
using IGK.ICore.Codec;
namespace IGK.DrSStudio.Drawing2D.WedgeBox
{
    [IGKD2DDrawingAdvancedElement("WedgeBox", typeof(Mecanism))]
    sealed class WedgeBoxElement : RectangleElement  , ICore2DDrawingVisitable
    {        
        private ICore2DDrawingDocument m_Document;

        [CoreXMLResourceElement()]
        public ICore2DDrawingDocument Document
        {
            get { return m_Document; }
            set
            {
                if (m_Document != value)
                {
                    m_Document = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        

        protected override void InitGraphicPath(CoreGraphicsPath p)
        {
            p.Reset();
            Rectanglef rc = new Rectanglef(0, 0, 32, 32);
            Rectanglef b = this.Bounds ;
            if (b.IsEmpty )
                return ;
            p.AddRectangle(rc);
            p.CloseFigure();
            p.AddLine(new Vector2f(2, 2), new Vector2f(30, 30));
            p.CloseFigure();
            p.AddLine(new Vector2f(2, 30), new Vector2f(30, 2));
            p.CloseFigure();
            Matrix c = new Matrix ();

            c.Scale(b.Width / rc.Width , b.Height / rc.Height , enuMatrixOrder.Append);
            c.Translate(b.X, b.Y, enuMatrixOrder.Append);
            p.Transform(c);
            c.Dispose();
        }
        new class Mecanism : RectangleElement.Mecanism
        { 
        }

        public bool Accept(ICore2DDrawingVisitor visitor)
        {
            if (visitor != null)
            {
                return true;
            }
            return false ;
        }

        public void Visit(ICore2DDrawingVisitor visitor)
        {            
            if (this.m_Document != null)
            {
                Rectanglei v_rc = Rectanglei.Round(this.GetBound());
                object v_state =   visitor.Save();
                visitor.Draw(this.m_Document, false , v_rc, enuFlipMode.None);
                visitor.Restore(v_state);
            }
            else
                visitor.Visit(this, typeof(RectangleElement));
        }

        protected override void ReadElements(IXMLDeserializer xreader)
        {
            base.ReadElements(xreader);
        }

        public override ICoreParameterConfigCollections GetParameters(ICore.WinUI.Configuration.ICoreParameterConfigCollections parameters)
        {
            var p = base.GetParameters(parameters);
            var g = parameters.AddGroup("Definition");
            g.AddActions(new WedgeBoxElementDocumentAction(this));

            return p;
        }

        sealed class WedgeBoxElementDocumentAction : CoreParameterActionBase
        {
            private WedgeBoxElement m_wedgeBoxElement;

            public WedgeBoxElementDocumentAction(WedgeBoxElement wedgeBoxElement)
            {
                this.m_wedgeBoxElement = wedgeBoxElement;
                this.Name = this.GetType().Name;                
                this.Action = new WebDocumentAction(this);
                this.CaptionKey = "webgebox:btn.document";
            }
            sealed class WebDocumentAction : CoreActionBase
            {
                private WedgeBoxElementDocumentAction m_documentAction;

                public WebDocumentAction(WedgeBoxElementDocumentAction wedgeBoxElementDocumentAction)
                {
                    this.m_documentAction = wedgeBoxElementDocumentAction;
                    this.Id = this.GetType().Name;
                    
                }
                protected override bool PerformAction()
                {
                    var b = CoreSystem.GetWorkbench();
                    if (b != null)
                    {
                        using (var g = b.CreateOpenFileDialog())
                        {
                            g.Filter = "gkds file | *.gkds";
                            if (g.ShowDialog() == enuDialogResult.OK)
                            {
                                var v_docs = CoreDecoder.Instance.Open2DDocument(g.FileName );
                                if ((v_docs != null) && (v_docs.Length >= 1))
                                {
                                    this.m_documentAction.m_wedgeBoxElement.Document =
                                        v_docs[0];
                                    return true;
                                }

                            }
                        }
                    }
                    return false ;
                }
            }
        }
    }
}
