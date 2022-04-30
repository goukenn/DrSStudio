using IGK.ICore.Codec;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Menu;
using IGK.ICore.WinCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Menu.Tools.Document
{
    /// <summary>
    /// perform a document copy to append 
    /// </summary>
    [CoreMenu("Tools.Document.CopyDocumentToAppend", 0x5)]
    class CopyDocumentToAppendMenu : Core2DDrawingMenuBase
    {
        protected override bool PerformAction()
        {
            string g = CoreXMLSerializer.Serialize(this.CurrentSurface.CurrentDocument);            

            WinCoreClipBoard.Copy("igk://drawing2D/docToAppend", g);
            return base.PerformAction();
        }
    }
}
