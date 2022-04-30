

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: NewPalette.cs
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
file:NewPalette.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.PaletteBuilder.Menu
{
    using IGK.DrSStudio.PaletteBuilder.WinUI;
    [DrSStudioMenu(PaletteBuilderConstant .NEW_PALETTE, 200, 
        ImageKey=CoreImageKeys.APP_PALETTE_GKDS)]
    class NewPalette : CoreApplicationMenu 
    {
        XPaletteEditorSurface m_surface;
        public XPaletteEditorSurface Surface
        {
            get { return m_surface; }
        }
        protected override bool PerformAction()
        {
            if (this.m_surface == null)
            {
                this.m_surface = new XPaletteEditorSurface();
                this.m_surface.Disposed += new EventHandler(m_surface_Disposed);
                this.Workbench.AddSurface(this.m_surface, true);
            }
            else
            {
                this.Workbench.SetCurrentSurface (this.m_surface);
            }
            return base.PerformAction();
        }
        public void Open(WinCorePalette pal)
        {
            if (this.m_surface == null)
                this.PerformAction();
            this.m_surface.Palette = pal;
          //  this.m_surface.Title = "title.PaletteEditorSurface".R();
        }
        void m_surface_Disposed(object sender, EventArgs e)
        {
            this.m_surface = null;
        }
    }
}

