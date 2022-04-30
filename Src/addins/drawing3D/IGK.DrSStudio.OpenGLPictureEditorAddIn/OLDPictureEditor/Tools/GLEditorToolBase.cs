

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GLEditorToolBase.cs
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
file:GLEditorToolBase.cs
*/

using IGK.ICore;using IGK.DrSStudio.GLPictureEditorAddIn.WinUI;
using IGK.DrSStudio.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.GLPictureEditorAddIn.Tools
{
    class GLEditorToolBase : CoreToolBase
    {
        /// <summary>
        /// get the current surface editor
        /// </summary>
        public new GLEditorSurface CurrentSurface {
            get {
                return base.CurrentSurface as GLEditorSurface ;
            }
        }
        /// <summary>
        /// get if this tool can be visible
        /// </summary>
        public override bool CanShow
        {
            get
            {
                return (this.CurrentSurface !=null) && (this.HostedControl !=null);
            }
        }
    }
}

