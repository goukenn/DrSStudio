

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: InstallerForm.Designer.cs
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
file:InstallerForm.Designer.cs
*/
namespace IGK.DrSStudio.PreviewInstaller
{
    partial class InstallerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.c_btn_installService = new System.Windows.Forms.Button();
            this.c_btn_RemoveService = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // c_btn_installService
            // 
            this.c_btn_installService.Location = new System.Drawing.Point(96, 198);
            this.c_btn_installService.Name = "c_btn_installService";
            this.c_btn_installService.Size = new System.Drawing.Size(135, 38);
            this.c_btn_installService.TabIndex = 0;
            this.c_btn_installService.Text = "Install Previewer";
            this.c_btn_installService.UseVisualStyleBackColor = true;
            this.c_btn_installService.Click += new System.EventHandler(this.c_btn_installService_Click);
            // 
            // c_btn_RemoveService
            // 
            this.c_btn_RemoveService.Location = new System.Drawing.Point(96, 242);
            this.c_btn_RemoveService.Name = "c_btn_RemoveService";
            this.c_btn_RemoveService.Size = new System.Drawing.Size(135, 38);
            this.c_btn_RemoveService.TabIndex = 1;
            this.c_btn_RemoveService.Text = "Remove  Previewer";
            this.c_btn_RemoveService.UseVisualStyleBackColor = true;
            this.c_btn_RemoveService.Click += new System.EventHandler(this.c_btn_RemoveService_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(96, 111);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(135, 38);
            this.button1.TabIndex = 3;
            this.button1.Text = "Remove Service";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(96, 67);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(135, 38);
            this.button2.TabIndex = 2;
            this.button2.Text = "Install Service";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // InstallerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(313, 363);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.c_btn_RemoveService);
            this.Controls.Add(this.c_btn_installService);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "InstallerForm";
            this.Text = "InstallerForm";
            this.ResumeLayout(false);
        }
        #endregion
        private System.Windows.Forms.Button c_btn_installService;
        private System.Windows.Forms.Button c_btn_RemoveService;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}

