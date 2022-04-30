

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: DeleteTextAction.cs
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
file:DeleteTextAction.cs
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
    /// hit on delete elelement
    /// </summary>
    sealed class DeleteTextAction : TextElementActionBase
    {
        //case 1 : curposx < slength: remove next char
        //case 2 : curposx == slength : if curY < (linecount-1), append next line to current line, remove next line
        //case 3 : 
        protected override bool PerformAction()
        {
            if (this.Element == null)
                return false ; 
            int x = this.CurX;
            int l = this.Element.Lines[this.CurY].Length;
            List<string> mt = new List<string>();
            mt.AddRange(this.Element.Lines);
            StringBuilder sb = null;
            bool v = true ;
            bool v_updateElementbound = false;
            if (x < l)
            {
                sb = new StringBuilder(mt[this.CurY]);
                sb.Remove(x, 1);
                mt[this.CurY ] = sb.ToString();
            }
            else if (x == l)
            {
                if (this.CurY < (this.Element.LineCount -1))
                {
                    //append next line. remove next line
                    sb = new StringBuilder(mt[this.CurY]);
                    sb.Append(this.Element.Lines[this.CurY + 1]);
                    mt.RemoveAt(this.CurY+1);
                    mt[this.CurY] = sb.ToString();
                    v_updateElementbound = true;
                }
                else
                    v = false;
            }
            if (v)
            {
                sb = new StringBuilder();
                sb.Append(string.Join(TextElement.LINE_SEPARATOR.ToString(), mt.ToArray()));
                this.Element.Content = sb.ToString();
                if (v_updateElementbound )
                this.Mecanism.UpdateElementBound();
            }
            return false;
        }
    }
}

