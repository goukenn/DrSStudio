

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AndroidLayout.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.WinCore;
using IGK.ICore;
using IGK.ICore.Drawing2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.DrSStudio.Android
{
    public class AndroidLayout : ViewBoxElement
    {
        /// <summary>
        /// layout called when element added of removed
        /// </summary>
        protected virtual void InitLayout()
        {
        }
        public AndroidLayout()
        {
        }
        protected override void InitializeElement()
        {
            base.InitializeElement();            
            this.ElementAdded += AndroidLayout_ElementAdded;
            this.ElementRemoved += AndroidLayout_ElementRemoved;
            
        }

        void AndroidLayout_ElementRemoved(object sender, CoreItemEventArgs<Core2DDrawingLayeredElement> e)
        {
            this.InitLayout();
        }

        void AndroidLayout_ElementAdded(object sender, CoreItemEventArgs<Core2DDrawingLayeredElement> e)
        {
            this.InitLayout();
        }
    }
}
