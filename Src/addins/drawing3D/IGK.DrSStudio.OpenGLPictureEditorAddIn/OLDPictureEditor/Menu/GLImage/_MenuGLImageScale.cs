

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _MenuGLImageScale.cs
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
file:_MenuGLImageScale.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace IGK.DrSStudio.GLPictureEditorAddIn.Menu.GLImage
{
    
using IGK.ICore;using IGK.DrSStudio.GLPictureEditorAddIn.Effect;
    using IGK.DrSStudio.WinUI;
    [IGK.DrSStudio.Menu.CoreMenu ("GLImage.ScaleColor", 0)]
    class _MenuGLImageScale : GLEditorMenuBase 
    {
        protected override bool PerformAction()
        {
            GLScaleEffect c = new GLScaleEffect ();
            this.CurrentSurface.Effects.Add(c);
            c.PropertyChanged += update;
            if (Workbench.ConfigureWorkingObject(c) != enuDialogResult.OK)
            {
                this.CurrentSurface.Effects.Remove(c);
                this.CurrentSurface.Render();
            }
            return base.PerformAction();
        }
        void update(object sender, IGK.OGLGame.GLPropertyChangedEventArgs e) {
            this.CurrentSurface.BeginInvoke((MethodInvoker)delegate() { this.CurrentSurface.Render(); });
        }
    }
}

