

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKXRadioButton.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
using IGK.ICore;using IGK.DrSStudio.Drawing2D;
using IGK.ICore.Resources;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:IGKXRadioButton.cs
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace IGK.DrSStudio.WinUI
{
    /// <summary>
    /// represent the base radio button
    /// </summary>
    public class IGKXRadioButton : 
        RadioButton  ,
        IIGKXRadioButton
    {
     
        public IGKXRadioButton()
        {           
        }       
        protected override void OnPaint(PaintEventArgs e)
        {            
            base.OnPaint(e);
        }
     
    }
}

