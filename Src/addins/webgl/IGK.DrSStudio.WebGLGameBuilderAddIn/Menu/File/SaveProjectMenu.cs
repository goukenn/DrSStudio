using IGK.ICore.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGK.DrSStudio.WebGLEngine.Menu.File
{
    [CoreMenu("File.WebGLSaveProject", 0x20)]
    class SaveProjectMenu : WebGLGameMenuBase
    {
        protected override void InitMenu()
        {
            base.InitMenu();
        }
        protected override bool PerformAction()
        {
            var p = this.CurrentSurface.Project;
            if (p != null) {

                using (SaveFileDialog sfd = new SaveFileDialog())
                {

                    sfd.Filter = "WebGL Game Project Solution | *.ibgeproj";
                    sfd.FileName = "default";
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        p.Save(sfd.FileName);
                    }
                }

            }
            return base.PerformAction();
        }
    }
}
