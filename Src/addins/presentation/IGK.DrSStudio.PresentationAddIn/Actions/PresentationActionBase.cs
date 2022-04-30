

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: PresentationActionBase.cs
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
file:PresentationActionBase.cs
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
using System.Windows.Forms;
namespace IGK.DrSStudio.Presentation.Actions
{
    using IGK.ICore.WinCore;
    using IGK.ICore;
    using IGK.DrSStudio.Actions;
    using IGK.DrSStudio.Presentation.WinUI;
    using IGK.ICore.MecanismActions;
    using IGK.ICore.Actions;

    public abstract class PresentationActionBase :
        CoreMecanismActionBase,
        IPresentationActions ,
        ICoreMecanismAction 
    {
        public PresentationForm PresentationForm
        {
            get { 
                if (this.Mecanism == null)
                    return null;
                return this.Mecanism.PresentationForm; 
            }
        }
        public PresentationActionBase()
        {
        }
        public new PresentationMecanism  Mecanism
        {
            get
            {
                return base.Mecanism as PresentationMecanism;
            }
            set
            {
                base.Mecanism =  value;
            }
        }
        public PresentationSurface Surface {
            get {
                return this.Mecanism.Surface as PresentationSurface;
            }
        }
        public override string Id
        {
            get { return string.Format ( PresentationConstant.PRESENTATION_ACTION, GetType ().Name);  }
        }
    }
}

