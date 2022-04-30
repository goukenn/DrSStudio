using IGK.ICore.Drawing2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.DrSStudio.Balafon.Codec
{
    using IGK.ICore;
    using IGK.ICore.Reflection;
    using System.Reflection;
    /// <summary>
    /// used to save 
    /// </summary>
    class BalafonCanvaObjEncoderVisitor
    {
        StringBuilder m_sb;

        public BalafonCanvaObjEncoderVisitor()
        {
            this.m_sb = new StringBuilder();
        }
        public string Data {
            get {
                return this.m_sb.ToString();
            }
        }
        public void Visit(params ICore2DDrawingDocument[] documents)
        {
            this.m_sb.Length = 0;
            this.m_sb.Append("{");
            this.m_sb.Append("[");
            bool g = false;
            foreach (var item in documents)
            {
                if (g)
                    this.m_sb.Append(",");
                this.m_sb.Append(this.getDefinition(item));
            }
            this.m_sb.Append("]");
            this.m_sb.Append("}");
        }

        private string getDefinition(ICore2DDrawingDocument document)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{");
            sb.Append(string.Format ("id:'{0}',",document.Id ));
            sb.Append(string.Format("w:'{0}',", document.Width));
            sb.Append(string.Format("h:'{0}',", document.Height));

            sb.Append("layers:[");
            var bck = this.m_sb.ToString();
            this.m_sb.Length = 0;
            bool r = false;
            foreach (var item in document.Layers)
            {
                if (r)
                    this.m_sb.Append(",");
                this.Visit(item);
                r = true;
            }
            sb.Append(this.m_sb.ToString());
            sb.Append ("]");
            sb.Append("}");
            //restore
            this.m_sb.Length = 0;
            this.m_sb.Append(bck);

            return sb.ToString();
        }
        public void Visit(ICore2DDrawingLayer layer)
        {
            bool r = false;
            this.m_sb.Append("[");
            foreach (ICore2DDrawingLayeredElement item in layer.Elements)
            {
                if (item.View)
                {
                    if (r)
                        this.m_sb.Append(",");
                    this.Visit(item as object);
                    r = true;

                }
            }
            this.m_sb.Append("]");
        }

        private void Visit(object p)
        {
            MethodInfo.GetCurrentMethod().Visit (this, p);
            //IGK.ICore.Reflection
        }
        public void Visit(ICore2DDrawingLayeredElement element)
        {
           // this.m_sb.Append("//no rendering " + element.Id);
            this.start();
            var sb = this.m_sb;
            sb.Append(string.Format("id:'{0}',", element.Id));
            sb.Append(string.Format("p:'{0}'", BalafonHtmlCanvaUtils.GetPathDefinition(element)));
            this.end();
        }
        public void Visit(ICore2DDrawingElementContainer container) {
            var sb = this.m_sb;
            this.start();
            sb.Append(string.Format("id:'{0}',", (container as ICoreIdentifier ).Id));
            sb.Append(string.Format("items:"));
            bool r = false;
            sb.Append("[");
            foreach (ICore2DDrawingLayeredElement item in container.Elements)
            {
                if (item.View)
                {
                    if (r)
                        this.m_sb.Append(",");
                    this.Visit(item as object);
                    r = true;

                }
            }
            sb.Append("]");
            this.end();
        }
        public void Visit(GroupElement container)
        {
            var sb = this.m_sb;
            this.start();
            sb.Append(string.Format("id:'{0}',", container.Id));
            sb.Append(string.Format("items:"));
            bool r = false;
            sb.Append("[");
            foreach (ICore2DDrawingLayeredElement item in container.Elements)
            {
                if (item.View)
                {
                    if (r)
                        this.m_sb.Append(",");
                    this.Visit(item as object);
                    r = true;

                }
            }
            sb.Append("]");
            this.end();
        }

        private void start() {
            this.m_sb.Append("{");
        }
        private void end() {
            this.m_sb.Append("}");
        }
    }
}
