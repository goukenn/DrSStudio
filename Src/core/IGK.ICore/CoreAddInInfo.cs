

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreAddInInfo.cs
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
file:CoreAddInInfo.cs
*/

﻿using IGK.ICore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
[assembly :IGK.ICore.CoreAddIn ("ICore",
    Date="08.08.13",
    Description="IGK ICore Core System",
    Status = IGK.ICore.enuCoreAddInStatus.ReleaseVersion)]
// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("IGK.ICore")]
#if DEBUG
[assembly: AssemblyDescription("IGKDEV - DRSSTUDIO "+ CoreConstant.ASSEMBLYVERSION + " DEBUG Version")]
#else
[assembly: AssemblyDescription("IGKDEV - DRSSTUDIO "+ CoreConstant.ASSEMBLYVERSION + " Release Version")]
#endif
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("IGKDEV")]
#if DEBUG
[assembly: AssemblyProduct("IGKDEV - ICore Debug")]
#else
[assembly: AssemblyProduct("IGKDEV - ICore Release")]
#endif
[assembly: AssemblyCopyright(CoreConstant.COPYRIGHT)]
[assembly: AssemblyTrademark(CoreConstant.COMPANY)]
[assembly: AssemblyCulture("")]
// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]
// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("27d9a4fb-d0d8-4e1e-a31b-27f6e50912eb")]
// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion(IGK.ICore.CoreConstant.ASSEMBLYVERSION)]
[assembly: AssemblyFileVersion(IGK.ICore.CoreConstant.ASSEMBLYFILEVERSION)]

