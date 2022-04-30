

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _RegionConverterMenu.cs
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
file:_RegionConverterMenu.cs
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
using System.Drawing.Drawing2D ;
using IGK.ICore.WinCore;
using IGK.ICore;using IGK.ICore.GraphicModels;
using IGK.ICore.Drawing2D;
namespace IGK.DrSStudio.Drawing2D.Menu
{
    abstract class _RegionConverterMenu : Drawing2DSelectedElementMenu
    {
        public abstract enuRegionConverter ConverterMode { get; }
        protected override bool PerformAction()
        {
            bool v = ConvertToRegion();
            return false;
        }
        protected override bool IsEnabled()
        {
            return (this.CurrentSurface != null) && (this.CurrentSurface.CurrentLayer.SelectedElements.Count > 0);
        }
        protected override bool IsVisible()
        {
            return (this.CurrentSurface != null);
        }
        public bool ConvertToRegion()
        {
            if (this.CurrentSurface.CurrentLayer.SelectedElements.Count > 1)
            {
                ICore2DDrawingLayeredElement[] tb = this.CurrentSurface.CurrentLayer.SelectedElements.ToArray();
                CoreRegionData rg = null;
                ICoreGraphicsPath p = null;
                List<ICore2DDrawingLayeredElement> v_marked = new List<ICore2DDrawingLayeredElement>();
                for (int i = 0; i < tb.Length; i++)
                {
                    p = tb[i].GetPath();
                    if (p != null)
                    {
                        if (rg == null)
                        {
                            rg = new CoreRegionData(p);
                        }
                        else {
                            switch (ConverterMode)
                            {
                                case enuRegionConverter.Complement:
                                    rg.Complement(p);
                                    break;
                                case enuRegionConverter.Union:
                                    rg.Union(p);
                                    break;
                                case enuRegionConverter.XOR:
                                    rg.Xor(p);
                                    break;
                                case enuRegionConverter.Intersect:
                                    rg.Intersect(p);
                                    break;
                                case enuRegionConverter.Exclude:
                                    rg.Exclude(p);
                                    break;
                                default:
                                    break;
                            }
                        }
                        v_marked.Add(tb[i]);
                    }
                }
                if (rg != null)
                {
                    RegionElement v_rg = RegionElement.CreateElement(rg);
                    if (v_rg != null)
                    {
                        this.CurrentSurface.CurrentLayer.Select(null);
                        this.CurrentSurface.CurrentLayer.Elements.RemoveAll (v_marked.ToArray());
                        this.CurrentSurface.CurrentLayer.Elements.Add(v_rg);
                        this.CurrentSurface.CurrentLayer.Select(v_rg);
                    }
                }
                return true;
            }
            return false;
        }
    }
}

