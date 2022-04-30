

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WinLauncherStartForm.cs
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
file:WinLauncherStartForm.cs
*/
/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igkdev.be
Application : DrSStudio
powered by IGK - DEV &copy; 2008-2012
THIS FILE IS A PART OF THE DRSSTUDIO APPLICATION. SEE "License.txt"
FOR MORE INFORMATION ABOUT THE LICENSE
------------------------------------------------------------------- 
*/
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.WinLauncher
{
    using IGK.ICore;using IGK.DrSStudio.WinUI;
    /// <summary>
    /// Start form
    /// </summary>
    sealed class WinLaucherStartForm : 
        StartFormBase, 
        IXForm 
    {
        private IGKXLabel xLabel1;
        protected override void OnFormClosed(System.Windows.Forms.FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
        }
        public WinLaucherStartForm()
        {
            this.InitializeComponent();
            this.SplashScreenChanged += new EventHandler(StartForm_SplashScreenChanged);
            this.SplashScreen = CoreResources.GetDocument(CoreConstant.SPLASHSCREEN);
            this.Load += new EventHandler(StartForm_Load);            
            this.ShowInTaskbar = true;
        }      
      void  IXForm .Close()
      {
          if (this.InvokeRequired)
          {
              this.BeginInvoke(new System.Windows.Forms.MethodInvoker(this.Close));
          }
          else {
              this.Close();
          }
          //try
          //{
          //    if (this.m_startForm.Created)
          //    {
          //        if (this.m_startForm.InvokeRequired)
          //        {
          //            this.m_startForm.Invoke(new MethodInvoker(CloseStartForm));
          //            return;
          //        }
          //        else
          //        {
          //            //start form close 2
          //            this.m_startForm.Close();
          //        }
          //    }
          //}
          //catch
          //{
          //    CoreLog.WriteDebug("StartForm Close generate an error");
          //}
      }
        protected override void OnFormClosing(System.Windows.Forms.FormClosingEventArgs e)
        {
            switch (e.CloseReason)
            {
                case System.Windows.Forms.CloseReason.ApplicationExitCall:
                    break;
                case System.Windows.Forms.CloseReason.FormOwnerClosing:
                    break;
                case System.Windows.Forms.CloseReason.MdiFormClosing:
                    break;
                case System.Windows.Forms.CloseReason.None:
                    break;
                case System.Windows.Forms.CloseReason.TaskManagerClosing:
                    break;
                case System.Windows.Forms.CloseReason.UserClosing:
                    //e.Cancel = true;
                    //if (this.MainForm != null)
                    //{
                    //    e.Cancel = true;
                    //    this.Hide();
                    //    return;
                    //}
                    //this.Hide();
                    break;
                case System.Windows.Forms.CloseReason.WindowsShutDown:
                    break;
                default:
                    break;
            }
            base.OnFormClosing(e);
        }
        void StartForm_Load(object sender, EventArgs e)
        {
            if ((CoreSystem.Instance != null) && (CoreSystem.Instance.MainForm != null))
                this.Icon = CoreSystem.Instance.MainForm.Icon;
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }
        void StartForm_SplashScreenChanged(object sender, EventArgs e)
        {
            if (this.SplashScreen != null)
            {
                this.ClientSize =
                    new System.Drawing.Size(
                        this.SplashScreen.Width,
                        this.SplashScreen.Height
                        );
                if (this.SplashScreen.IsClipped)
                {
                    this.Region = this.SplashScreen.GetClippedRegion();
                }
            }
        }
        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            base.OnPaint(e);
            #if DEMOVERSION
                e.Graphics.DrawString(
                    string.Format ("{0} [{1}] DEMO VERSION", 
                    CoreConstant.APPTITLE ,
                    CoreConstant.VERSION),
                    this.Font ,
                    System.Drawing.SystemBrushes.ControlText ,
                     System.Drawing.Point.Empty );
            #endif 
        }
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WinLaucherStartForm));
            this.xLabel1 = new IGK.DrSStudio.WinUI.IGKXLabel();
            this.SuspendLayout();
            // 
            // xLabel1
            // 
            this.xLabel1.CaptionKey = "Loading ....";
            this.xLabel1.Location = new System.Drawing.Point(12, 208);
            this.xLabel1.Name = "xLabel1";
            this.xLabel1.Size = new System.Drawing.Size(433, 62);
            this.xLabel1.TabIndex = 0;
            // 
            // WinLaucherStartForm
            // 
            this.ClientSize = new System.Drawing.Size(550, 320);
            this.Controls.Add(this.xLabel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(550, 320);
            this.MinimumSize = new System.Drawing.Size(550, 320);
            this.Name = "WinLaucherStartForm";
            this.ResumeLayout(false);
        }
        public override void SendMessage(string message)
        {
            //set text label
            this.xLabel1.Text = message;
        }
    }
}

