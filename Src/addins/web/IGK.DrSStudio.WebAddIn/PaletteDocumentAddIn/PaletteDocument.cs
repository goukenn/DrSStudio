

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: PaletteDocument.cs
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
    [CoreWorkingDocument ("PaletteDocument")]
    public class PaletteDocument : Core2DDrawingLayerDocument
    {
        public override Type DefaultSurfaceType
        {
            get
            {
                return typeof(PaletteDrawingSurface);
            }
        }
        public PaletteDocument():base("40mm", "20mm")
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
            return new PaletteLayer();
        }

        [CoreWorkingObject("PaletteLayer")]
        public class PaletteLayer : Core2DDrawingLayer
        {
            protected override void ReadElements(IXMLDeserializer xreader)
            {
                this.Elements.Clear();
                base.ReadElements(xreader);
            }
            public PaletteLayer()
            {
                RectangleElement rc = null;
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


                int[] t = new int[] { 4, 2, 8 };
                float[] widths = new float[]{
                    "10mm".ToPixel(),                    
                    "20mm".ToPixel(),
                    "5mm".ToPixel()
                };
                float[] heights = new float[]{
                    "10mm".ToPixel(),
                    "5mm".ToPixel(),
                    "5mm".ToPixel(),
                };
                Colorf[] colors = new Colorf[]{
                    Colorf.DarkGray,
                    Colorf .Gray,
                    Colorf.WhiteSmoke ,
                    Colorf.FromFloat (0.7f),
                    Colorf.CornflowerBlue ,
                    Colorf.DarkBlue  ,
                    Colorf.Red, 
                    Colorf.Black,
                    Colorf.Orange
                };

                float x = 0.0f;
                float y = 0.0f;
                float width = 0.0f;
                float height = 0.0f;
                int c = 0;
                for (int i = 0; i < t.Length; i++)
                {
                    x = 0.0f;
                    width = widths[i];
                    height = heights[i];
                    

                    for (int j = 0; j < t[i]; j++)
                    {

                        rc = new PaletteBounds();
                        rc.SuspendLayout();
                        rc.Bounds = new Rectanglef(x, y, width, height);
                        rc.FillBrush.SetSolidColor(colors[c % (colors.Length)]);
                        rc.ResumeLayout();
                        this.Elements.Add(rc);
                        x += width;
                        c++;
                    }
                    y += height;
                }
                


            }
        }
    }
}
