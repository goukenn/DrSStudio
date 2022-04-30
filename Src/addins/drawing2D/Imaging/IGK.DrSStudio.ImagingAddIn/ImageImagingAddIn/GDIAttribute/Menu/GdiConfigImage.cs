using IGK.DrSStudio.Drawing2D.Menu;
using IGK.DrSStudio.ImageAddIn.GDIAttribute.WinUI;
using IGK.ICore;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.WinUI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Imaging.ImageImagingAddIn.GDIAttribute.Menu
{
    [CoreMenu("Image.GdiAttribute", 10)]
    class GdiConfigImage : ImageMenuBase
    {
        protected override bool PerformAction()
        {
            using (XGdiConfigAttribute attr = new XGdiConfigAttribute(this.ImageElement)) {
                using (ICoreDialogForm frm = Workbench.CreateNewDialog(attr))
                {
                    frm.Title = "title.GdiAttribute.Caption".R();
                    frm.ShowDialog();
                }
            }
            return base.PerformAction();
        }
    }
}
