

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _ConvertToPathElementMenu.cs
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
file:_ConvertToPathElement.cs
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
using System.Drawing;
using System.Drawing.Drawing2D;
namespace IGK.DrSStudio.Drawing2D.Menu
{
    using IGK.ICore.WinCore;
using IGK.ICore.Menu;
    using IGK.ICore.GraphicModels;
    using IGK.ICore;
    using IGK.ICore.Drawing2D;
    using IGK.DrSStudio.Menu;
    using IGK.ICore.Drawing2D.Menu;
    [DrSStudioMenu(ConverterConstant.MENU_CONVERTTO_PATHELEMENENT , 0)]
    class _ConvertToPathElementMenu : Core2DDrawingMenuBase 
    {

        protected override bool IsEnabled()
        {
            return base.IsEnabled() && (this.CurrentSurface.CurrentLayer.SelectedElements.Count > 0);
        }
        protected override bool IsVisible()
        {
            return this.IsEnabled();// base.IsVisible();
        }
        protected override bool PerformAction()
        {
            ICore2DDrawingLayeredElement[] l = this.CurrentSurface.CurrentLayer.SelectedElements.ToArray();
            ICoreBrush fbrush = null;
            ICoreBrush sBrush = null;
            switch (l.Length)
            {
                case 0:
                    return false;
                case 1:
                    if (l[0] is SplineElement)
                    {
                        return false;
                    }
                    goto default;
                default:
                    ICoreGraphicsPath v_cp = null;
                    List<Vector2f> m_points = new List<Vector2f>();
                    for (int i = 0; i < l.Length; i++)
                    {
                        if (i == 0)
                        {
                           ICoreBrushOwner  br = l[0] as
                                ICoreBrushOwner;
                            if (br != null)
                            {
                                if ((br.BrushSupport & enuBrushSupport .Fill ) == enuBrushSupport.Fill)
                                {
                                    fbrush = br.GetBrush(enuBrushMode.Fill);
                                }
                                if ((br.BrushSupport & enuBrushSupport.Stroke ) == enuBrushSupport.Stroke )
                                {
                                    sBrush = br.GetBrush(enuBrushMode.Stroke);
                                }
                            }
                        }
                        v_cp = GetElementPath( l[i]);
                        if (v_cp != null)
                        {
                            m_points.AddRange(v_cp.PathPoints);
                        }
                    }
                    SplineElement cl = SplineElement.Create(m_points.ToArray());
                    if (fbrush != null)
                        cl.FillBrush.Copy(fbrush);
                    if (sBrush != null)
                        cl.StrokeBrush.Copy(sBrush);
                    this.CurrentSurface.CurrentLayer.Select(null);
                    this.CurrentSurface.CurrentLayer.Elements.RemoveAll(l);
                    this.CurrentSurface.CurrentLayer.Elements.Add(cl);
                    
                    break;
            }
            return false;
        }

        private ICoreGraphicsPath GetElementPath(ICore2DDrawingLayeredElement element)
        {
            if (element is ICoreInternalGraphicsGenerated) {
                return (element as ICoreInternalGraphicsGenerated).GetPath();
            }
            return element.GetPath();
        }
    }
}

