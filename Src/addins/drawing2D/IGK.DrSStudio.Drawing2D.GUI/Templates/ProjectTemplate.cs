

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ProjectTemplate.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.WinCore;
using IGK.DrSStudio.Drawing2D.WinUI;
using IGK.ICore;
using IGK.ICore.Drawing2D.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


[assembly: CoreWorkingProjectTemplate(Name = CoreConstant.DRAWING2D_PROJECT,
TargetSurfaceType = typeof(IGKD2DDrawingSurface))]
[assembly: CoreWorkingProjectItemTemplate(CoreConstant.DRAWING2D_PROJECT + ".A4Document",
TargetSurfaceType = typeof(IGKD2DDrawingSurface),
Params="width: 210mm; height:297mm;")]
[assembly: CoreWorkingProjectItemTemplate(CoreConstant.DRAWING2D_PROJECT + ".Document32x32",
TargetSurfaceType = typeof(IGKD2DDrawingSurface),
Params = "width: 32px; height:32px;")]
[assembly: CoreWorkingProjectItemTemplate(CoreConstant.DRAWING2D_PROJECT + ".Document16x16",
TargetSurfaceType = typeof(IGKD2DDrawingSurface),
Params = "width: 16px; height:16px;")]
[assembly: CoreWorkingProjectItemTemplate(CoreConstant.DRAWING2D_PROJECT + ".Document48x48",
TargetSurfaceType = typeof(IGKD2DDrawingSurface),
Params = "width: 48px; height:48px;")]
[assembly: CoreWorkingProjectItemTemplate(CoreConstant.DRAWING2D_PROJECT + ".Document72x72",
TargetSurfaceType = typeof(IGKD2DDrawingSurface),
Params = "width: 72px; height:72px;")]
[assembly: CoreWorkingProjectItemTemplate(CoreConstant.DRAWING2D_PROJECT + ".Document512x512",
TargetSurfaceType = typeof(IGKD2DDrawingSurface),
Params = "width: 512px; height:512px;")]
[assembly: CoreWorkingProjectItemTemplate(CoreConstant.DRAWING2D_PROJECT + ".Document1024x768",
TargetSurfaceType = typeof(IGKD2DDrawingSurface),
Params = "width: 1024px; height:768px;")]
[assembly: CoreWorkingProjectItemTemplate(CoreConstant.DRAWING2D_PROJECT + ".Document1366x768",
TargetSurfaceType = typeof(IGKD2DDrawingSurface),
Params = "width: 1366px; height:768px;")]