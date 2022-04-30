

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GotToLastDocument.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:GotToLastDocument.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.Drawing2D.Actions
{
    /// <summary>
    /// represent a go to last document action
    /// </summary>
    sealed class GotToLastDocument : CoreSufaceActionBase
    {
        protected override bool PerformAction()
        {
            int i = this.CurrentSurface.Documents.IndexOf(this.CurrentSurface.CurrentDocument);
            if (i < this.CurrentSurface.Documents.Count - 1)
                this.CurrentSurface.CurrentDocument = this.CurrentSurface.Documents[this.CurrentSurface.Documents.Count - 1];
            return false;
        }
    }
}

