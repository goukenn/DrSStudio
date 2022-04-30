

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: SkinContexMenuEditor.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IGK.DrSStudio.SkinEditorAddin.WinUI
{

    enum enuMenuMode{
        MenuStrip,
        ContextMenuStrip
    }
    class SkinContexMenuEditor : Form, IDisposable 
    {
        private ToolStripRenderer m_renderer;

        class MyMenuDropDownViewer : ToolStripDropDownMenu 
        {
            public MyMenuDropDownViewer()
            {
                this.SetTopLevel(false);
                this.Bounds = new System.Drawing.Rectangle(0, 0, 300, 100);
            }
        }
        class MyContextViewer : ToolStripDropDown
        {
            public MyContextViewer()
            {
                this.SetTopLevel(false);
                this.Bounds = new System.Drawing.Rectangle(0, 0, 300, 100);
            }
        }

        public SkinContexMenuEditor(enuMenuMode mode,  ToolStripRenderer renderer)
        {
            
            this.m_renderer = renderer;
            if (mode == enuMenuMode.MenuStrip)
            {
                MyMenuDropDownViewer f = new MyMenuDropDownViewer();
                f.Renderer = renderer;
                f.Dock = DockStyle.Fill;
                f.Items.Add(new System.Windows.Forms.ToolStripMenuItem("Item 1"));
                f.Items.Add(new System.Windows.Forms.ToolStripMenuItem("Item 2"));
                f.Items.Add(new System.Windows.Forms.ToolStripMenuItem("Item 3"));
                f.Items.Add("-");
                f.Items.Add(new System.Windows.Forms.ToolStripMenuItem("Item 4") { Enabled = false });
                f.Items.Add(new System.Windows.Forms.ToolStripMenuItem("Item 5"));

                this.Controls.Add(f);
                f.Show(0, 0);
            }
            else {
                MyContextViewer f = new MyContextViewer();
                f.Renderer = renderer;
                f.Dock = DockStyle.Fill;
                f.Items.Add(new System.Windows.Forms.ToolStripMenuItem("Item 1"));
                f.Items.Add(new System.Windows.Forms.ToolStripMenuItem("Item 2"));
                f.Items.Add(new System.Windows.Forms.ToolStripMenuItem("Item 3"));
                f.Items.Add("-");
                f.Items.Add(new System.Windows.Forms.ToolStripMenuItem("Item 4") { Enabled = false });
                f.Items.Add(new System.Windows.Forms.ToolStripMenuItem("Item 5"));

                this.Controls.Add(f);
                f.Show(0, 0);
            }
        }
      

    }
}
