

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GLLibCodeEditorTool.cs
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
file:GLLibCodeEditorTool.cs
*/

using IGK.ICore;using IGK.DrSStudio.GLPictureEditorAddIn.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.GLPictureEditorAddIn.Tools
{
    [CoreTools ("GLLibShaderCodeEditor")]
    class GLLibCodeEditorTool : GLEditorToolBase
    {
        private static GLLibCodeEditorTool sm_instance;
        private GLLibCodeEditorTool()
        {
        }
        public static GLLibCodeEditorTool Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static GLLibCodeEditorTool()
        {
            sm_instance = new GLLibCodeEditorTool();
        }
        protected override void GenerateHostedControl()
        {
            this.HostedControl = new XGLShaderCodeEditor(this);
        }
    }
}

