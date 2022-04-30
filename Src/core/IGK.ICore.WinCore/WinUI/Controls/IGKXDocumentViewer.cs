using IGK.ICore.Drawing2D;
using IGK.ICore.Resources;
using IGK.ICore.WinUI;
using IGK.ICore.Xml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.WinCore.WinUI.Controls
{
    [DefaultProperty("DocumentName")]
    public class IGKXDocumentViewer : IGKXControl
    {
        private string m_DocumentName;
        private bool m_Proportional;
        private enuFlipMode m_FlipMode;

        protected override System.Drawing.Size DefaultSize
        {
            get
            {
                return new System.Drawing.Size(100, 100);
            }
        }
        [DefaultValue(enuFlipMode.None)]
        public enuFlipMode FlipMode
        {
            get { return m_FlipMode; }
            set
            {
                if (m_FlipMode != value)
                {
                    m_FlipMode = value;
                    this.Invalidate();
                    this.Update();
                }
            }
        }
        [DefaultValue(true)]
        public bool Proportional
        {
            get { return m_Proportional; }
            set
            {
                if (m_Proportional != value)
                {
                    m_Proportional = value;
                    this.Invalidate();
                    this.Update();
                }
            }
        }
        [Description("defaultresourcename of this document")]
        public string DocumentName
        {
            get { return m_DocumentName; }
            set
            {
                if (m_DocumentName != value)
                {
                    m_DocumentName = value;
                    OnDocumentNameChanged(EventArgs.Empty);
                }
            }
        }
        public event EventHandler DocumentNameChanged;
        private ICore2DDrawingDocument m_document;
        private CoreXmlElement m_dataStore;

        protected virtual void OnDocumentNameChanged(EventArgs e)
        {
            if (DocumentNameChanged != null)
            {
                DocumentNameChanged(this, e);
            }
        }


        public IGKXDocumentViewer()
        {
            this.DocumentNameChanged += _DocumentNameChanged;
            this.Paint += _Paint;
            this.SizeChanged += _SizeChanged;
            this.m_dataStore = CoreXmlElement.CreateXmlNode("Data");
        }

        void _SizeChanged(object sender, EventArgs e)
        {
            if (this.m_document == null)
                return;
            this._setupDocument();
        }

        private void _setupDocument()
        {
            if ((this.Width == 0) || (this.Height == 0))
                return;

            //float w = this.m_dataStore.GetAttributeValue<float>("docWidth", this.m_document.Width);
            //float h = this.m_dataStore.GetAttributeValue<float>("docHeight", this.m_document.Height);
            ////store initial document width
            //this.m_dataStore["docWidth"] = w;
            //this.m_dataStore["docHeight"] = h;

            float ex = (float)Math.Max(0.0005f, ((float)this.Width/(float)this.m_document.Width));
            float ey = (float)Math.Max(0.0005f, ((float)this.Height/ (float)this.m_document.Height) );

            this.m_document.Scale(ex, ey, enuMatrixOrder.Append);
            this.m_document.SetSize(this.Width, this.Height);


            this.Invalidate();
        }

        void _Paint(object sender, CorePaintEventArgs e)
        {
            if (this.DesignMode)
                return;

            if (this.m_document != null)
            {
                this.m_document.Draw(e.Graphics,
                    this.Proportional,
                    new Rectanglei(0, 0, this.Width, this.Height),
                    this.FlipMode);
            }
        }

        void _DocumentNameChanged(object sender, EventArgs e)
        {
            if (this.DesignMode)
                return;
            this._loadDocument();


        }

        private void _loadDocument()
        {
            var p = CoreResources.GetDocument(this.DocumentName);
            if (p != null)
            {
                this.m_document = p.Clone() as ICore2DDrawingDocument;
                this._setupDocument();
            }
            else
            {
                this.m_document = null;
            }
            this.Invalidate();

        }
    }
}
