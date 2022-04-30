

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ControlDesigner.cs
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
file:ControlDesigner.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.ComponentModel.Design;

namespace IGK.DrSStudio.WinUI.Design
{
    using WinFormDesign = System.Windows.Forms.Design;
    public class ControlDesigner : WinFormDesign.ControlDesigner
    {
        static ControlDesigner() {
            WinCoreDesigner.Init();
        }

        public ControlDesigner()
        {
        }
        public override void Initialize(System.ComponentModel.IComponent component)
        {
            base.Initialize(component);
        }
    }
}

