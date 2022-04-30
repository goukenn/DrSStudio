

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: InstallerForm.cs
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
file:InstallerForm.cs
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ServiceProcess;
using System.Configuration;
using System.Configuration.Install ;
namespace IGK.DrSStudio.PreviewInstaller
{
    using util = IGK.DrSStudio.PictureHandlerService.Utility ;
    public partial class InstallerForm : Form
    {
        public InstallerForm()
        {
            InitializeComponent();
        }
        private void c_btn_RemoveService_Click(object sender, EventArgs e)
        {
            Program.UnInstallComponent();
        }
        private void c_btn_installService_Click(object sender, EventArgs e)
        {
            Program.InstallComponent();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            AppDomain dom = AppDomain.CreateDomain ("Service");
            dom.ExecuteAssemblyByName(typeof(
            PictureHandlerService.PictureHandlerInstaller).Assembly.GetName(), new string[0]);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            ServiceController srv = util.GetService(PictureHandlerService.PictureHandlerConstant.SERVICE_NAME);
            if (srv == null)
                return ;
            if (srv.Status == ServiceControllerStatus.Running )
            {
                srv.Stop();
                srv.WaitForStatus (ServiceControllerStatus.Stopped );
            }
            System.Reflection.Assembly v_asm = typeof(util).Assembly;
            IDictionary hash = new Hashtable();
            AssemblyInstaller v_asmInstaller1 = new AssemblyInstaller(v_asm.Location ,
                   new string[0]);
            v_asmInstaller1.Uninstall(hash);
        }
    }
}

