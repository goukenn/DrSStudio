

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: BMWDocument.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/

using IGK.ICore;
using IGK.ICore.Codec;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.DrSStudio;
using IGK.DrSStudio.Menu;
using IGK.DrSStudio.WinUI;
using IGK.DrSStudio.Drawing2D;
using IGK.DrSStudio.Drawing2D.WinUI;
﻿using IGK.ICore.WinUI.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.Drawing2D
{
    [CoreWorkingDocument ("BMWSymbolDocument")]
    public class BMWDocument : Core2DDrawingLayerDocument
    {
        public override Type DefaultSurfaceType
        {
            get
            {
                return typeof(PaletteDrawingSurface);
            }
        }
        public BMWDocument()
            : base("50mm", "50mm")
        {

        }

        public override enuParamConfigType GetConfigType()
        {
            return enuParamConfigType.NoConfig;
        }
        public override ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
        {
            parameters.Clear();
            return parameters;
        }
        protected override ICore2DDrawingLayer CreateNewLayer()
        {
            return new BMWLayer();
        }

        [CoreWorkingObject("BMWLayer")]
        public class BMWLayer : Core2DDrawingLayer
        {
            public BMWLayer()
            {
                BMWPie rc = null;
                Vector2f v_center =
                    new Vector2f("25mm".ToPixel(), "25mm".ToPixel());
                float radius = "24mm".ToPixel();
                //model
                //int[] t = new int[] { 4, 8, 2 };
                //float[] widths = new float[]{
                //    "10mm".ToPixel(),
                //    "5mm".ToPixel(),
                //    "20mm".ToPixel(),
                //};
                //float[] heights = new float[]{
                //    "10mm".ToPixel(),
                //    "5mm".ToPixel(),
                //    "5mm".ToPixel(),
                //};


                
                Colorf[] colors = new Colorf[]{
                    Colorf.FromString ("#FF63B3E5"),
                    Colorf.FromString("#FFF1F1F1"),
                    Colorf.FromString ("#FF63B3E5"),
                    Colorf.FromString("#FFF1F1F1")
                };

                int c = 0;
                Vector2f[] rd = new Vector2f[] { new Vector2f(radius, radius) };
                for (int i = 0; i < 4; i++)
                {
                        rc = new BMWPie();
                        rc.SuspendLayout();
                        rc.Center = v_center;
                        rc.StartAngle = i  *90;
                        rc.SweepAngle = 90;
                        rc.Radius = rd;
                        rc.FillBrush.SetSolidColor(colors[c % (colors.Length)]);
                        rc.StrokeBrush.SetSolidColor(Colorf.Black);

                        rc.ResumeLayout();
                        this.Elements.Add(rc);
                        c++;
                }
                


            }
        }
    }
}
