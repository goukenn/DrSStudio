using IGK.DrSStudio.Balafon;
using IGK.DrSStudio.Balafon.WinUI;
using IGK.ICore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: CoreWorkingProjectTemplate(
    BalafonConstant.PROJECT_NAME,
    TargetSurfaceType = null)]
[assembly: CoreWorkingProjectItemTemplate(BalafonConstant.PROJECT_NAME + ".website",
    TargetSurfaceType = typeof(BalafonEditorSurface))]