

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ExtractAudioAsDataAction.cs
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
file:ExtractAudioAsDataAction.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.Actions
{
    using IGK.ICore;using IGK.DrSStudio.WinUI;
    class ExtractAudioAsDataAction : MergeActionBase
    {
        protected override void PerformAction()
        {
            XVideoFileItem i = this.SourceControl as XVideoFileItem;
            i.ExportTrackFile();
        }
        public override string Text
        {
            get { return CoreSystem.GetString("Menu.ExportAudioAsDataFile"); }
        }
    }
}

