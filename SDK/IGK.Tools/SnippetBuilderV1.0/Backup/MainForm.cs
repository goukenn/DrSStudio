/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igkdev.be
Application : DrSStudio
powered by IGK - DEV &copy; 2008-2011
THIS FILE IS A PART OF THE DRSSTUDIO APPLICATION. SEE "License.txt"
FOR MORE INFORMATION ABOUT THE LICENSE
------------------------------------------------------------------- 
*/
/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igk-dev.be
App : DrSStudio
powered by IGK - DEV 2008-2011
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
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace WinSnippetBuilder
{
    public partial class MainForm : Form
    {
        VSSnippetBuilder v_snippetToEdit;
        SnippetBuilder m_Builder;
        private string m_fileName;


        public VSSnippetBuilder SnippetToEdit
        {
            get { return v_snippetToEdit; }
            set { v_snippetToEdit = value; }
        }
        public MainForm()
        {
            InitializeComponent();
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Snippets|*.snippet";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    v_snippetToEdit = VSSnippetBuilder.OpenFromFile (ofd.FileName);
                    m_Builder = new SnippetBuilder(v_snippetToEdit);
                    this.propertyGrid1.SelectedObject = m_Builder;
                    this.m_fileName = ofd.FileName;
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (v_snippetToEdit == null)
                return;
            if (string.IsNullOrEmpty(this.m_fileName) || !System.IO.File.Exists (this.m_fileName ))
            {
                this.SaveAs();
            }
            else
            {
                v_snippetToEdit.Save(this.m_fileName);
                this.SetText();
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            v_snippetToEdit = VSSnippetBuilder.CreateNew();
            m_Builder = new SnippetBuilder(v_snippetToEdit);
            this.propertyGrid1.SelectedObject = m_Builder;
            this.m_fileName ="New";
            this.SetText();
        }

        private void SetText()
        {
            this.Text = "Snippet Builder v1.0 - [" + this.m_fileName + "]";
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.SaveAs();
        
        }

        private void SaveAs()
        {
            if (v_snippetToEdit == null)
                return;
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Snippets|*.snippet";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    v_snippetToEdit.Save(sfd.FileName);
                    this.m_fileName = sfd.FileName;
                    this.SetText();
                }

            }
        }

        private void mySnippetGenerator10ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (AboutBox about = new AboutBox())
            {
                about.Owner = this;
                about.ShowInTaskbar = false;
                about.ShowDialog();
            }
        }
    }
}
