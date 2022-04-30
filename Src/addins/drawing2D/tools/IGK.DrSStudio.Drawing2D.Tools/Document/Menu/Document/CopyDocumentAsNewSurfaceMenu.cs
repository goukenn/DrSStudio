using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.DrSStudio.Drawing2D.Menu;
using IGK.ICore.Drawing2D;

namespace IGK.DrSStudio.Drawing2D.Document.Menu.Document
{
    [IGKD2DDocumentMenuAttribute("CopyDocument.CopyAsNewSurface", 0x10)]
    class CopyDocumentAsNewSurfaceMenu : Core2DDrawingMenuBase
    {
        protected override bool PerformAction()
        {
            var doc = this.CurrentSurface.CurrentDocument.Clone() as ICore2DDrawingDocument;
            var t = this.CurrentSurface.GetType();
            ICore2DDrawingSurface surface = t.Assembly.CreateInstance(t.FullName) as ICore2DDrawingSurface;
            if (surface != null) {

                surface.Documents.Add(doc);
                surface.Documents.Remove(surface.CurrentDocument);
                this.Workbench.AddSurface(surface, true);
            }
            return false;
        }
    }
}
