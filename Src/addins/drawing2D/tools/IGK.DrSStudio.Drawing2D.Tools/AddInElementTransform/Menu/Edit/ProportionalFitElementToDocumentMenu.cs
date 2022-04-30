using IGK.ICore;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Menu;
using IGK.ICore.WinCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Drawing2D.AddInElementTransform.Menu.Edit
{
    [CoreMenu("Edit.ProportionalFitToDocument", 0x30)]
    class ProportionalFitElementToDocumentMenu : Core2DDrawingMenuBase
    {
        protected override bool PerformAction()
        {
            var g = this.CurrentSurface.CurrentLayer.SelectedElements.ToArray();
            if (g.Length > 0)
            {
                Rectanglef c = new Rectanglef(0, 0,
                    this.CurrentSurface.CurrentDocument.Width,
                    this.CurrentSurface.CurrentDocument.Height);
                for (int i = 0; i < g.Length; i++)
                {
                   var  v_mat=  WinCoreGraphicsPathUtils.ProportionalFitMatrix(
                        g[i].GetPath(),
                        c);

                    g[i].MultTransform(v_mat, enuMatrixOrder.Append);
                }
            }
            return base.PerformAction();
        }
    }
}
