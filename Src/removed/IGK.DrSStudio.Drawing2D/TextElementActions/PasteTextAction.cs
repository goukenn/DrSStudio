

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: PasteTextAction.cs
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
file:PasteTextAction.cs
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
namespace IGK.DrSStudio.Drawing2D.TextElementActions 
{
    using IGK.ICore;using IGK.DrSStudio.WinUI;
    public sealed class PasteTextAction : TextElementActionBase 
    {      
        protected override bool PerformAction()
        {
            if (Clipboard.ContainsText())
            {
                TextElement v_element = this.Element;
                string v_txt = Clipboard.GetText();
                System.IO.StringReader v_sr = new System.IO.StringReader(v_txt);
                StringBuilder sb = new StringBuilder();
                sb.Append(v_element.Content);
                int x = this.Mecanism.PosX;
                int l = this.Mecanism.PosLine;
                bool v_updateline = false;
                while ((v_txt = v_sr.ReadLine()) != null)
                {
                    if (v_updateline)
                    {
                        l++;
                        x = 0;
                        sb.AppendLine(string.Empty);
                    }
                    sb.Append(v_txt);
                    x += v_txt.Length;
                    if (!v_updateline)
                    {
                        v_updateline = true;
                    }
                }
                this.Mecanism.PosLine = l;
                this.Mecanism.PosX = x;
                this.Mecanism.Selection = x;
                this.Mecanism.SetStringContent(sb.ToString());
                this.Mecanism.SetText( sb.ToString());
                this.Mecanism.Element.InitElement();
                this.Mecanism.BuildAndInitFont();
                this.Mecanism.CurrentSurface.Invalidate();
            }
            return false;
        }
    }
}

