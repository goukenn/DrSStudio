

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _NoiseEffect.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
using IGK.ICore;
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
file:_NoiseEffect.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.Imaging.Effect
{
    using WinUI;
    using IGK.DrSStudio.Drawing2D.Menu;
    [DrSStudioMenu("Image.Effects.NoiseEffet",
        0)]
    public sealed class _NoiseEffect :  ImageMenuBase
    {
        protected override bool PerformAction()
        {
            using (NoiseEffect effect = new NoiseEffect())
            {
                effect.ImageElement = this.ImageElement;
                effect.Surface = this.CurrentSurface;
                if (Workbench.ConfigureWorkingObject(effect,
                    "title.editNoizeEffet".R(), false, Size2i.Empty).Equals(enuDialogResult.OK))
                {
                    effect.ApplyEffect();
                    this.CurrentSurface.RefreshScene();
                }
            }
            return base.PerformAction();
        }
    }
}

