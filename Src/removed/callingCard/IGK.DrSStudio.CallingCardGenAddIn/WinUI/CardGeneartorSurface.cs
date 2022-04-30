

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CardGeneartorSurface.cs
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
file:CardGeneartorSurface.cs
*/
using IGK.ICore;using IGK.DrSStudio.Drawing2D.WinUI;
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
namespace IGK.DrSStudio.WinUI
{
    [CoreSurface ("CallingCardSurface",
        EnvironmentName=CoreConstant.DRAWING2D_ENVIRONMENT )]
    sealed class CardGeneartorSurface : 
        IGKD2DDrawingSurface,
        ICardGeneratorSurface
    {
        public static CardGeneartorSurface CreateSurface(CoreProjectTemplateAttribute attr)
        {
            CardGeneartorSurface v_sr = new CardGeneartorSurface();
            return v_sr;
        }
        #region ICardGeneratorSurface Members
        public ICardDocument FrontDocument
        {
            get { throw new NotImplementedException(); }
        }
        public ICardDocument BackgroundDocument
        {
            get { throw new NotImplementedException(); }
        }
        #endregion
        #region ICore2DDrawingObject Members
        public new IGK.DrSStudio.Drawing2D.ICore2DDrawingObject Parent
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        #endregion
    }
}

