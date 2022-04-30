

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: RegionElement.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore;using IGK.ICore.Codec;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.Drawing2D
{
         [Core2DDrawingStandardElement("Region",
        typeof(LineElement.Mecanism),
        Keys = enuKeys.R,
        IsVisible=false)]
    public class RegionElement : Core2DDrawingDualBrushElement  
    {
             CoreRegionData m_rg_data;

        public override enuBrushSupport BrushSupport
        {
            get
            {
                return enuBrushSupport.FillOnly | enuBrushSupport.Solid |
                     enuBrushSupport.Hatch | enuBrushSupport.LinearGradient | enuBrushSupport.PathGradient | enuBrushSupport.Texture;
            }
        }

        [CoreXMLElement]
             /// <summary>
             /// get the region data
             /// </summary>
             public CoreRegionData Data {
                 get {
                     return m_rg_data;
                 }
             }
             public RegionElement()
             {
                 m_rg_data = null;   
                     
             }
        protected override void InitializeElement()
        {
            base.InitializeElement();
            this.StrokeBrush.SetSolidColor(Colorf.Transparent);
        }
        protected override void OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs e)
        {
            if ((enu2DPropertyChangedType) e.ID == enu2DPropertyChangedType.BrushChanged) {

            }
            base.OnPropertyChanged(e);

        }
        public static RegionElement CreateElement(CoreRegionData regionData)
             {
            if (regionData == null || regionData.IsEmpty
            )
                return null;
                RegionElement c = new RegionElement();
                   c.m_rg_data = regionData;
                  c.InitElement();
                 return c;
             }

             protected override void InitGraphicPath(CoreGraphicsPath path)
             {
                 path.Reset();
                
                if ((m_rg_data ==null) || (m_rg_data.IsEmpty))
                    return;
                   var d =  m_rg_data.GetOutLinePath();
                   var def =  d.GetAllDefinition ();
                     path.AddPath(def.Points , def.Types);
             }
    }
}
