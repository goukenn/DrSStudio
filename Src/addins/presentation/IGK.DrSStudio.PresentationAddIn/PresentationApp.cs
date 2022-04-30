

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: PresentationApp.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
using IGK.ICore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.ICore.IO;
using IGK.ICore.WinUI;
using IGK.DrSStudio.Presentation.WinUI;

namespace IGK.DrSStudio.Presentation
{
    [CoreApplication("title.DrSStudio.FilePresentation")]
    public class PresentationApp : DrSStudioWinCoreApp 
    {
        public override void Initialize()
        {
            base.Initialize();


        }
        public override ICoreSystemWorkbench CreateNewWorkbench()
        {
                 return new PresentationWorkbench();
        }
        public override void Register(CoreSystem instance)
        {
            base.Register(instance);

#if DEBUG
            CoreResourcesManager.LoadString("title.presentationform", "Presentation");

            if (PathUtils.CreateDir("Lang"))
            {
                CoreResourcesManager.SaveLangResources("Lang/fr.resources");
            }
            string dir = CoreConstant.DRS_SRC+@"\addins\presentation\IGK.DrSStudio.PresentationAddIn\Lang\fr.resources";
            if (PathUtils.CreateDir(System.IO.Path.GetDirectoryName(dir)))
            {
                CoreResourcesManager.SaveLangResources(dir);
            }
            CoreLog.WriteLine("title.presentationForm".R());

#endif

        }
    }
}
