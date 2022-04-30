

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: DrSStudioSplashScreen.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinUI;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:DrSStudioSplashScreen.cs
*/
using System;
using System.Windows.Forms;
using System.Drawing;

namespace IGK.DrSStudio.WinUI
{
    /// <summary>
    /// DrSStudio Splash Screen
    /// </summary>
    public partial class DrSStudioSplashScreen : XForm, ICoreStartForm
    {
        private Label c_lb_message;
        private Type m_mainFormType;

        /// <summary>
        /// override default primary size
        /// </summary>
        protected override Size DefaultSize{
            get{
                return new System.Drawing.Size(620, 360);
            }
        }
        public DrSStudioSplashScreen()
        {
            this.InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackgroundImage = Resources.R.GetSplahScreen();
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.ShowInTaskbar = true;
        }
       public void SendMessage(string message)
        {
            if (c_lb_message.InvokeRequired) {
                Action d = (Action) delegate (){
                    c_lb_message.Text = message;
                };
                BeginInvoke(d);
            }else
                c_lb_message.Text = message;
        }
        /// <summary>
        /// main form type for initialization
        /// </summary>
        /// <param name="mainFormType"></param>
        void ICoreStartForm.Run(Type mainFormType)
        {
            m_mainFormType = mainFormType;
            Application.Run(this);
        }

        
    }
}

