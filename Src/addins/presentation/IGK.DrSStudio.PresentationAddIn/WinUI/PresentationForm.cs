

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: PresentationForm.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore;
using System;
using System.Windows.Forms;
using IGK.ICore.WinUI;

namespace IGK.DrSStudio.Presentation.WinUI
{
    /// <summary>
    /// represent a presentation Form
    /// </summary>
    public class PresentationForm : Form, IXCoreForm, ICoreWorkbenchHost
    {
        private PresentationWorkbench m_workbench;
        PresentationSurface c_surface;

        protected override System.Drawing.Size DefaultSize
        {
            get
            {
                return new System.Drawing.Size(500, 380);
            }
        }


        
        public PresentationForm()
        {
            this.InitializeComponent();
            this.m_workbench = (CoreSystem.Instance.Workbench as PresentationWorkbench) ?? new PresentationWorkbench(this);
            this.c_surface = new PresentationSurface();
            this.Controls.Add(this.c_surface);
            this.Load += PresentationForm_Load;


            this.c_surface.CreateControl();
            
        }
        public PresentationDocument PresentationDocument {
            get {
                return this.c_surface.PresentationDocument;
            }
            set {
                this.c_surface.PresentationDocument = value;
            }
        }
        public event EventHandler PresentationDocumentChanged {
            add {
                this.c_surface.PresentationDocumentChanged += value;
            }
            remove {
                this.c_surface.PresentationDocumentChanged -= value;
            }
        }

        void PresentationForm_Load(object sender, EventArgs e)
        {
            this.Text  = "title.PresentationForm".R();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.c_surface.EnableMecanism();
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PresentationForm));
            this.SuspendLayout();
            // 
            // PresentationForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PresentationForm";
            this.ResumeLayout(false);

        }
        public string FileName { get; set; }

        public ICoreSystemWorkbench Workbench => this.m_workbench;

     
    }
}
