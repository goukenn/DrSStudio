using System;
using System.Text;

namespace IGK.DRSStudio.BalafonDesigner.Codec
{
    internal class BalafonViewDesignerEncoderVisitor 
    {
        private BalafonViewDesignerEncoder balafonViewDesignerEncoder;
        private StringBuilder m_sb;

        public string NewLine { get; set; }

        public BalafonViewDesignerEncoderVisitor(BalafonViewDesignerEncoder balafonViewDesignerEncoder)
        {
            this.balafonViewDesignerEncoder = balafonViewDesignerEncoder;
            this.NewLine = Environment.NewLine;
            m_sb = new StringBuilder();
        }
        public void Clear() {
            this.m_sb.Clear();
        }

        public void Append(string msg) {
            m_sb.Append(msg + this.NewLine);
        }

        public string Output => m_sb.ToString();
        

        public void Visit(BalafonViewDesignerDocument doc) {

            Append("<?php");

            //initialization


            Append("$t->clearChilds();");


            //actions

            Append("igk_view_handle_actions($fname, [], $params);");

            //view

            //foreach (var e in doc.Elements) {
            //}

        }
    }
}