

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _ConvertToPath.cs
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
file:_ConvertToPath.cs
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
using System.Drawing.Drawing2D;
namespace IGK.DrSStudio.Drawing2D.Menu
{
    using IGK.ICore;using IGK.DrSStudio.Menu;
    [CoreMenu(ConverterConstant.MENU_CONVERTTO_PATH, 0)]
    class _ConvertToPath : Core2DMenuBase 
    {
        protected override bool PerformAction()
        {
           ICore2DDrawingLayeredElement[] l =  this.CurrentSurface.CurrentLayer.SelectedElements.ToArray();
           ICoreBrush fbrush = null;
           ICoreBrush sBrush = null;
           switch (l.Length)
           {
               case 0:
                   return false;
               case 1:
                   if (l[0] is PathElement)
                   {
                       return false;
                   }
                   goto default;                   
               default:
                   GraphicsPath vp = new GraphicsPath();
                   GraphicsPath cp = null;
                   for (int i = 0; i < l.Length; i++)
                   {
                       if (i == 0)
                       {
                           ICore2DDrawingBrushSupportElement br = l[0] as
                               ICore2DDrawingBrushSupportElement;
                           if (br != null)
                           {
                               if ((br.BrushSupport & enuBrushSupport.Fill) == enuBrushSupport.Fill)
                               {
                                   fbrush = br.GetBrush(enuBrushMode.Fill);
                               }
                               if ((br.BrushSupport & enuBrushSupport.Stroke) == enuBrushSupport.Stroke)
                               {
                                   sBrush = br.GetBrush(enuBrushMode.Stroke);
                               }
                           }
                       }
                       cp = l[i].GetPath();
                       if ((cp != null)&& (cp.PointCount > 0))
                           vp.AddPath(cp, false);
                   }
                   if (vp.PointCount > 0)
                   {
                       PathElement cl = PathElement.Create(vp);
                       if (fbrush != null)
                           cl.FillBrush.Copy(fbrush);
                       if (sBrush != null)
                           cl.StrokeBrush.Copy(sBrush);
                       this.CurrentSurface.CurrentLayer.Select(null);
                       this.CurrentSurface.CurrentLayer.Elements.Remove(l);
                       this.CurrentSurface.CurrentLayer.Elements.Add(cl);
                       vp.Dispose();
                       return true;
                   }
                   break;
           }
           return false;
        }
    }
}

