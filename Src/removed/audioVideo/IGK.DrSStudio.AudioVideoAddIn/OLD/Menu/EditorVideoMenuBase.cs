

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: EditorVideoMenuBase.cs
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
file:EditorVideoMenuBase.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.AudioVideo.Menu
{
    public abstract class EditorVideoMenuBase : VideoMenuBase
    {
        public new IGK.DrSStudio.AudioVideo.WinUI.XVideoSurfaceEditor CurrentSurface
        {
            get {
                return base.CurrentSurface as IGK.DrSStudio.AudioVideo.WinUI.XVideoSurfaceEditor;
            }
        }
    }
}

