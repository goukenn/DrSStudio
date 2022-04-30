using IGK.ICore;
using IGK.ICore.WebSdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly:CoreAddIn(
    Description="contain resources require by web sdk",
    Initializer=typeof (CoreWebSDKInstaller)
    )]


// USE balafon http://localhost/igkdev/api/zipsdk lib to get a zip file of the balfon zip sdk