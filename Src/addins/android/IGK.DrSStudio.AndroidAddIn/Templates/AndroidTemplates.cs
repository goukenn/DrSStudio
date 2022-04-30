

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AndroidTemplates.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.WinCore;
using IGK.ICore;
using IGK.DrSStudio.Android;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.DrSStudio.Android.WinUI;


///mark for root group category template /!\ important ;-)
[assembly: CoreWorkingProjectTemplate(
    AndroidConstant.PROJECT_NAME,
    TargetSurfaceType = null)]


//[assembly: CoreWorkingProjectItemTemplate(AndroidConstant.PROJECT_NAME + ".manifest",
//    TargetSurfaceType = typeof(AndroidManifestEditorSurface))]

//[assembly: CoreWorkingProjectItemTemplate(AndroidConstant.PROJECT_NAME + ".icon",
//    TargetSurfaceType = typeof(AndroidAppIconSurface))]

//[assembly: CoreWorkingProjectItemTemplate(AndroidConstant.PROJECT_NAME + ".Layout",
//    TargetSurfaceType = typeof(AndroidLayoutDesignSurface))]



//[assembly: CoreWorkingProjectItemTemplate(AndroidConstant.PROJECT_NAME + ".androidapplication",
//    TargetSurfaceType = typeof(AndroidProjectEditorSurface),
//    ConfigType=typeof(AndroidProjectWizard))]

//[assembly: CoreWorkingProjectItemTemplate(AndroidConstant.PROJECT_NAME + ".androidactivity",
//    TargetSurfaceType = typeof(AndroidActivityEditorSurface))]

//[assembly: CoreWorkingProjectItemTemplate(AndroidConstant.PROJECT_NAME + ".menuresources",
//    TargetSurfaceType = typeof(AndroidResourceEditorSurface), Params="menu")]

//[assembly: CoreWorkingProjectItemTemplate(AndroidConstant.PROJECT_NAME + ".resources",
//    TargetSurfaceType = typeof(AndroidResourceEditorSurface))]