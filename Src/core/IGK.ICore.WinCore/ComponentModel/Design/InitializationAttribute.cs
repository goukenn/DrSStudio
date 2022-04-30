

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: InitializationAttribute.cs
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
file:InitializationAttribute.cs
*/
using IGK.ICore.WinCore;
using IGK.ICore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.ICore.WinCore.ComponentModel.Design;

[assembly: WinCoreInitializationAttribute()]

namespace IGK.ICore.WinCore.ComponentModel.Design
{
    [AttributeUsage (AttributeTargets.Assembly , AllowMultiple=false , Inherited = false )]
    class WinCoreInitializationAttribute : Attribute 
    {
        static WinCoreInitializationAttribute() {
            System.Windows.Forms.MessageBox.Show("Init Call");
        }
        public WinCoreInitializationAttribute()
        {
            System.Windows.Forms.MessageBox.Show("Init Call");
        }
    }
}

