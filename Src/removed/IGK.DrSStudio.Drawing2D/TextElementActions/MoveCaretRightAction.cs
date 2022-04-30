

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: MoveCaretRightAction.cs
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
file:MoveCaretRightAction.cs
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
    /// <summary>
    /// move caret to right
    /// </summary>
    class MoveCaretRightAction : TextElementActionBase
    {
        protected override bool PerformAction()
        {
            int v_ln = this.Element.Lines.Length;
            if ((this.CurY >= v_ln) || (this.CurY < 0))
                return false;
            int x = this.Element.Lines[this.CurY].Length;
            if (this.CurX == x)
            {
                if (this.CurY < (v_ln-1))
                {
                    this.CurY++;
                    this.CurX = 0;
                }
            }
            else
            {
                this.CurX++;
            }
            this.Mecanism.UpdateCaretPos();
            return true;
        }
    }
}

