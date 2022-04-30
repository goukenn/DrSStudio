using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Menu;
using IGK.ICore.WinUI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Android.AndroidMipmap.Menu
{
    [CoreMenu("Tools.Android.ExportAsMipmap", 20)]
    class AndroidMipmapExportAsMipmapItem : Core2DDrawingMenuBase
    {
        protected override bool PerformAction() {

            string folder = CoreCommonDialogUtility.PickFolder(this.Workbench);
            if (!string.IsNullOrEmpty(folder)) {
                // 
                System.Windows.Forms.MessageBox.Show("Generate mimp folder");
            }
            return false;
        } 
    
    }
}
