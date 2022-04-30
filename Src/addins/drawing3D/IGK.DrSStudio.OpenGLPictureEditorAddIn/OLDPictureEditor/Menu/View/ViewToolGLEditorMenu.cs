

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ViewToolGLEditorMenu.cs
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
file:ViewToolGLEditorMenu.cs
*/

using IGK.ICore;using IGK.DrSStudio.GLPictureEditorAddIn.Tools;
using IGK.DrSStudio.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.GLPictureEditorAddIn.Menu.View
{
    [CoreMenu("View.GLEditorViewShaderTool", 500)]
    class ViewToolGLEditorMenu : IGK.DrSStudio.Menu.CoreMenuViewToolBase 
    {
        public ViewToolGLEditorMenu():base( GLLibCodeEditorTool.Instance)
        {
        }
        protected override bool PerformAction()
        {
            return base.PerformAction();
        }
        protected override void InitMenu()
        {
            base.InitMenu();            
        }
    }
}

