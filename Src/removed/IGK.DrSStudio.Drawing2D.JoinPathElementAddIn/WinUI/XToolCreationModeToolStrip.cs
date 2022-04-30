

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XToolCreationModeToolStrip.cs
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
file:XToolCreationModeToolStrip.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.Drawing2D.WinUI
{
    using IGK.ICore;using IGK.DrSStudio.WinUI ;
    /// <summary>
    /// represent a path creation mode toolstrip
    /// </summary>
    sealed class XToolCreationModeToolStrip : XToolStripCoreToolHost 
    {
        XToolStripButton c_btn_line;
        XToolStripButton c_btn_arc;
        XToolStripButton c_btn_bezier;
        PathElement.Mecanism m_mecanism;
        public XToolCreationModeToolStrip(IGK.DrSStudio.Drawing2D.Tools.PathCreationModeTool tool):base(tool)
        {
            this.InitializeComponent();
        }
        private void InitializeComponent()
        {
            this.c_btn_line = new XToolStripButton();
            this.c_btn_bezier = new XToolStripButton();
            this.c_btn_arc = new XToolStripButton();
            this.c_btn_arc.Tag = enuPathMode.Arc;
            this.c_btn_bezier.Tag = enuPathMode.Bezier;
            this.c_btn_line.Tag = enuPathMode.Line;
            this.c_btn_line.Click += new EventHandler(c_btn_Click);
            this.c_btn_arc.Click += new EventHandler(c_btn_Click);
            this.c_btn_bezier.Click += new EventHandler(c_btn_Click);
            this.Items.Add(c_btn_line);
            this.Items.Add(c_btn_arc);
            this.Items.Add(c_btn_bezier);
        }
        void c_btn_Click(object sender, EventArgs e)
        {
            if (this.m_mecanism == null)
                return;
            enuPathMode v_mode = (enuPathMode)((XToolStripButton)sender).Tag;
            this.m_mecanism.PathMode = (int)v_mode;
        }
        protected override void InitLayout()
        {
            base.InitLayout();
        }
        public override void LoadDisplayText()
        {
            base.LoadDisplayText();
            c_btn_arc.ToolTipText = CoreSystem.GetString("tooltip.patharc");
            c_btn_bezier.ToolTipText = CoreSystem.GetString("tooltip.pathbezier");
            c_btn_line.ToolTipText = CoreSystem.GetString("tooltip.pathline");
        }
        /// <summary>
        /// represent the mecanism
        /// </summary>
        public ICoreWorkingMecanism Mecanism { get { return this.m_mecanism; } set { this.m_mecanism = value as PathElement.Mecanism; } }
    }
}

