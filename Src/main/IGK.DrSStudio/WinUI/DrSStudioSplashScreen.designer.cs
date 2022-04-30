using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.WinUI
{
    public partial class DrSStudioSplashScreen
    {
        private void InitializeComponent()
        {
            this.c_lb_message = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // c_lb_message
            // 
            this.c_lb_message.AutoSize = true;
            this.c_lb_message.Location = new System.Drawing.Point(13, 172);
            this.c_lb_message.Name = "c_lb_message";
            this.c_lb_message.Size = new System.Drawing.Size(0, 13);
            this.c_lb_message.TabIndex = 0;
            // 
            // DrSStudioSplashScreen
            // 
            this.ClientSize = new System.Drawing.Size(620, 360);
            this.Controls.Add(this.c_lb_message);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "DrSStudioSplashScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

    }
}
