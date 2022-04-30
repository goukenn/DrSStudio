

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKNoteBookRenderer.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.WinCore;
using IGK.ICore;using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.ICore.WinCore.WinUI;

namespace IGK.DrSStudio.WinUI
{
    /// <summary>
    /// represent the base note book renderer
    /// </summary>
    public class IGKNoteBookRenderer
    {
        protected internal virtual void Render(IGKXNoteBook notebook, CorePaintEventArgs e)
        {

        }
        protected internal virtual void Render(IGKXNoteBookContentZone contentZone, CorePaintEventArgs e)
        {

        }
        protected internal virtual void Render(IGKXNoteBookTabBar tabBar, CorePaintEventArgs e)
        {
            e.Graphics.Clear(WinCoreControlRenderer.NoteBookBarBackgroundColor);
        }
    }
}
