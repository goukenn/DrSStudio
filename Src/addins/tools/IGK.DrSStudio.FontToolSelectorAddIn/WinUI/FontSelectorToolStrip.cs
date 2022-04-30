

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: FontSelectorToolStrip.cs
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
file:FontSelectorToolStrip.cs
*/
/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igkdev.be
Application : DrSStudio
powered by IGK - DEV &copy; 2008-2012
THIS FILE IS A PART OF THE DRSSTUDIO APPLICATION. SEE "License.txt"
FOR MORE INFORMATION ABOUT THE LICENSE
------------------------------------------------------------------- 
*/
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D ;
namespace IGK.DrSStudio.WinUI
{
    using IGK.ICore.WinCore;
using IGK.ICore;using IGK.ICore.Tools ;
    using IGK ;
    using IGK.DrSStudio.Tools;
    /// <summary>
    /// preprsent a font definition tools strip
    /// </summary>
    internal class FontSelectorToolStrip : 
        FontSelectorBase                  
    {

    
        public FontSelectorToolStrip():base()
        {
            this.InitSetting();
        }
        
        private void InitSetting()
        {

        }
       public string Id
        {
            get { return "Host_" + Tool.Id; }
        }
     

        internal void InitTools()
        {
            CoreFontToolSelector c = this.Tool as CoreFontToolSelector;
            this.FontComboBox.SelectedItem = c.Setting.DefaultFont;
            this.SizeComboBox.Items.AddRange(c.Setting.DefaultFontSizes);
            this.SizeComboBox.Text = "12";
        }
    }
}

