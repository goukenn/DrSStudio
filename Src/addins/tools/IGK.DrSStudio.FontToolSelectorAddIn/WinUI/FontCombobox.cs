

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: FontCombobox.cs
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
file:FontCombobox.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace IGK.DrSStudio.WinUI
{
    class FontCombobox : ToolStripComboBox
    {
        public FontCombobox()
            : base()
        {
            System.Reflection.MethodInfo meth = this.Control.GetType().GetMethod("SetStyle", 
                System.Reflection.BindingFlags.NonPublic | 
                System.Reflection.BindingFlags.Instance);
            if (meth != null)
            {
                meth.Invoke(this.Control, new object[]{
                    ControlStyles.ResizeRedraw|
                    ControlStyles.AllPaintingInWmPaint|
                    ControlStyles.OptimizedDoubleBuffer
                    ,
                    true 
                });
            }
        }
    }
}

