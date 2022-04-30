using IGK.ICore;
using IGK.ICore.WinCore.Controls;
using IGK.ICore.WinCore.WinUI.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.AudioVideo.WinUI
{
    public class UIDocumentVideoSequencePropertyExplorer : IGKXToolHostedControl
    {
        private System.Windows.Forms.ListBox listBox1;
    
        public UIDocumentVideoSequencePropertyExplorer()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(0, 0);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(532, 303);
            this.listBox1.TabIndex = 0;
            // 
            // UIDocumentVideoSequencePropertyExplorer
            // 
            this.Controls.Add(this.listBox1);
            this.Name = "UIDocumentVideoSequencePropertyExplorer";
            this.Size = new System.Drawing.Size(532, 303);
            this.ResumeLayout(false);

        }

        //internal void loadProperties(ICoreWorkingAttachedPropertyObject document)
        //{
        //    this.listBox1.Items.Clear();
        //    if (document != null)
        //    {
        //        var e = document.GetAttachedProperties();
        //        foreach (var item in e)
        //        {
        //            this.listBox1.Items.Add(item);
        //        }
           
        //    }
        //}
    }
}
