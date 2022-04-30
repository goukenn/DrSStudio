

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ToogleToVertical.cs
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
file:ToogleToVertical.cs
*/
/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igkdev.be
Application : DrSStudio
powered by IGK - DEV &copy; 2008-2012
THIS FILE IS A PART OF THE DRSSTUDIO APPLICATION. SEE "License.txt"
FOR MORE INFORMATION ABOUT THE LICENSE
------------------------------------------------------------------- 
*/
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.Drawing2D.Actions
{
    /// <summary>
    /// represent a ToogleStyle Mecanism
    /// </summary>
    public sealed class ToogleToVertical : Core2DDrawingMecanismAction
    {
        protected override bool PerformAction()
        {
            ICoreHandStyleMecanism m = this.Mecanism as ICoreHandStyleMecanism;
            switch (this.ShortCutDemand)
            { 
                case System.Windows.Forms.Keys.F :
                    m.HandStyle = enuHandStyle.FreeHand;
                    break;
                case System.Windows.Forms.Keys.V :
                    m.HandStyle = enuHandStyle.Vertical;
                    break;
                case System.Windows.Forms.Keys.H :
                    m.HandStyle = enuHandStyle.Horizontal;
                    break;
            }
            return false;
        }
    }
}

