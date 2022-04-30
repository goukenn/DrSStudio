

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XWebEditorSurface.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/

using IGK.ICore;
using IGK.ICore.Codec;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.DrSStudio;
using IGK.DrSStudio.Menu;
using IGK.DrSStudio.WinUI;
using IGK.DrSStudio.Drawing2D;
using IGK.DrSStudio.Drawing2D.WinUI;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:WebEditorSurface.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IGK.ICore.WinCore;
using IGK.ICore.WinCore.WinUI.Controls;
namespace IGK.DrSStudio.WebAddIn.WinUI
{
    [CoreSurface ("WebEditorSurface",EnvironmentName=WebConstant.HTML_EDITOR_ENVIRONMENT )]
    class XWebEditorSurface :
        IGKXWinCoreWorkingSurface,
        ICoreWorkingEditableSurface 
    {
        public void Save() { 
        }
        public void SaveAs(string filename) { 
        }


        public bool CanCopy
        {
            get { return true; }
        }

        public bool CanCut
        {
            get { return true; }
        }

        public bool CanPaste
        {
            get { return true; }
        }

        public void Copy()
        {
        }

        public void Cut()
        {
        }

        public void Paste()
        {
        }
    }
}

