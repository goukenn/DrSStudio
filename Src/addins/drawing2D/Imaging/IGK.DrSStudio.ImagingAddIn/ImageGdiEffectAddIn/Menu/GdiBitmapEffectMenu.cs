

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GdiBitmapEffectMenu.cs
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
﻿using IGK.ICore.WinUI.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IGK.DrSStudio.Drawing2D.Menu;

namespace IGK.DrSStudio.Imaging.Menu
{
    /// <summary>
    /// represent a base gdi bitmap effect Menu
    /// </summary>
    public class GdiBitmapEffectMenu : ImageMenuBase, ICoreWorkingConfigurableObject
    {

        protected ICoreBitmap OldBitmap { get; set; }


        protected void Restore()
        {
            this.ImageElement.SetBitmap(this.OldBitmap, false);
            this.CurrentSurface.RefreshScene();
        }

        /// <summary>
        /// override this method to application your effect
        /// </summary>
        /// <param name="temp"></param>
        protected virtual void ApplyEffect(bool temp)
        {
        }

        public virtual ICoreControl GetConfigControl()
        {
            return null;
        }

        public virtual enuParamConfigType GetConfigType()
        {
            return enuParamConfigType.NoConfig;
        }

        public virtual ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
        {
            return parameters;
        }
    }
}
