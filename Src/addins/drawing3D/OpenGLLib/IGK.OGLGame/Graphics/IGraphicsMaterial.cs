


using IGK.ICore;
/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGraphicsMaterial.cs
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
file:IGraphicsMaterial.cs
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
namespace IGK.OGLGame.Graphics
{
    public interface IGraphicsMaterial
    {
        Colorf BackAmbient { get; set; }
        Colorf BackDiffuse { get; set; }
        Colorf BackEmission { get; set; }
        float BackShininess { get; set; }
        Colorf BackSpecular { get; set; }
        Colorf FrontAmbient { get; set; }
        Colorf FrontDiffuse { get; set; }
        Colorf FrontEmission { get; set; }
        float FrontShininess { get; set; }
        Colorf FrontSpecular { get; set; }
    }
}

