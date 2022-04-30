

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: BackKeyAction.cs
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
file:BackKeyAction.cs
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
namespace IGK.DrSStudio.Drawing2D.TextElementActions
{
    sealed class BackKeyAction : TextElementActionBase
    {
        //(x >0) remove char
        //(x ==0) : if (line >0) do nothing
        protected override bool PerformAction()
        {
            bool v = true;
            int x = this.CurX;
            int l = this.Element.Lines[this.CurY].Length;
            List<string> mt = new List<string>();
            mt.AddRange(this.Element.Lines);
            StringBuilder sb = null;
            bool v_updateElementbound = false;
            if (this.Mecanism.PosX > 0)
            {
                //on the line
                this.Mecanism.TextBuffer.Remove(this.Mecanism.PosX - 1, 1);
                this.Mecanism.PosX--;
                this.Mecanism.CharCount--;
                this.Mecanism.Selection--;
                this.Element.InitElement();
                this.Mecanism.CurrentSurface.Invalidate(this.Element);
                this.Mecanism.UpdateCaretPos();
                v = false;
            }
            else if (this.Mecanism.PosX == 0)
            {
                if (this.CurY > 0)
                {
                    //test if line is greater than 0
                    sb = new StringBuilder(mt [this.CurY -1]);
                    int npos = sb.Length;
                    sb.Append(mt[this.CurY]);
                    mt[this.CurY-1] = sb.ToString();
                    mt.RemoveAt(this.CurY);
                    //set the pos x
                    this.Mecanism.PosX = npos;
                    this.CurY--;
                    //update line count       
                    v_updateElementbound = true;
                }
            }
            else
                v = false;
            if (v)
            {
                sb = new StringBuilder();
                sb.Append(string.Join(TextElement.LINE_SEPARATOR.ToString() , mt.ToArray()));
                this.Element.Content = sb.ToString();
                if (v_updateElementbound)
                {
                    this.Mecanism.UpdateElementBound();
                    this.Mecanism.UpdateCaretPos();
                }
            }
            return false;
        }
    }
}

