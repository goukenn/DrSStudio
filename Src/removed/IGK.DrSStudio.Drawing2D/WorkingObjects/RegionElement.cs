

/*
IGKDEV @ 2008 - 2014
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
/*
IGKDEV - [2008 - 2014]
author: C.A.D . BONDJE DOUE
file:RegionElement.cs
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
using System.Drawing ;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices ;
using System.Reflection;
namespace IGK.DrSStudio.Drawing2D
{
    [Core2DDrawingStandardItem("Region",
        typeof(Mecanism), IsVisible= false)]
    public class RegionElement : Core2DDrawingLayeredDualBrushElement
    {
        private Region m_Region;
        public override void Dispose()
        {
            if (this.m_Region != null)
            {
                this.m_Region.Dispose();
            }
            base.Dispose();
        }
public Byte[] RegionData{
        get{ return m_Region.GetRegionData ().Data ;}
}
protected override void WriteElements(IGK.DrSStudio.Codec.IXMLSerializer xwriter)
{
    base.WriteElements(xwriter);
    xwriter.WriteStartElement("RegionData");
    Byte[] v_t = RegionData;
    if (v_t != null)
    {
        StringBuilder sb = new StringBuilder();
        {
            for (int i = 0; i < v_t.Length; i++)
            {
                if (i != 0)
                {
                    sb.Append(";");
                }
                sb.Append(v_t[i].ToString());
            }
        }
        xwriter.WriteValue(sb.ToString());
    }
    xwriter.WriteEndElement();
}
protected override void ReadElements(IGK.DrSStudio.Codec.IXMLDeserializer xreader)
{
    IGK.DrSStudio.Codec.CoreXMLSerializerUtility.ReadElements(this, xreader, ReadAdditionalData);
}
bool ReadAdditionalData(IGK.DrSStudio.Codec.IXMLDeserializer xreader)
{
    switch (xreader.Name)
    { 
        case "RegionData":
            string[] sm = xreader.ReadString().Split(';');
            byte[] t = new byte[sm.Length];
            for (int i = 0; i < sm.Length; i++)
            {
                t[i] = byte.Parse(sm[i]);
            }
            if (t.Length > 0)            
            {
                Region v_rg = new Region();
                RegionData d = v_rg.GetRegionData();
                d.Data = t;
                v_rg.Dispose();
                v_rg = new Region(d);
                this.m_Region = v_rg;
                return true;
            }
            break;
    }
    return true;
}
        public override bool CanEdit
        {
            get
            {
                return false;
            }
        }
        public override Rectanglef GetBound()
        {
            if (this.m_Region != null)
            {
                Graphics g = Graphics.FromHwnd(IntPtr.Zero);
                Rectanglef rg = this.m_Region.GetBounds(g);
                Matrix v_mat = this.GetMatrix();
                rg = CoreMathOperation.ApplyMatrix(rg, v_mat);
                g.Dispose();
                return rg;
            }
            return Rectangle.Empty;
        }
        public override bool Contains(Vector2f position)
        {            
            Matrix v_mat = this.GetMatrix ();
            Vector2f c = position ;
            if (v_mat.IsInvertible)
            {
                Matrix v_hmat = v_mat.Clone() as Matrix;
                v_hmat.Invert();
                c = CoreMathOperation.TransformVector2fPoint(v_hmat, position)[0];
            }
            bool v = this.m_Region.IsVisible (c);            
            return v;
        }
        public override void Draw(Graphics g)
        {
            GraphicsState v_s = g.Save();
            CoreGraphicsPath path = this.GetPath();
            this.SetGraphicsProperty(g);
            Matrix mat = this.GetMatrix();
            g.MultiplyTransform(mat);
            Brush v_br = this.FillBrush.GetBrush();
            Pen v_pen = this.StrokeBrush .GetPen ();
            if (v_br != null)
            {
                g.FillRegion(v_br, this.m_Region);
            }            
           if ((this.m_regionpath  != null) && (v_pen != null))
            {
                 g.DrawPath(v_pen, this.m_regionpath );
            }
           g.Restore(v_s);
        }
        public static RegionElement CreateElement(Region rg)
        {
            if (rg != null)
            { 
                Graphics g = Graphics.FromHwnd (IntPtr.Zero );
                if (rg.IsEmpty(g))
                {
                    g.Dispose();
                    return null;
                }
                RegionElement v_rgElement = new RegionElement();
                v_rgElement.m_Region = rg;
                v_rgElement.InitElement();
                return v_rgElement;
            }
            return null;
        }
        public override enuBrushSupport BrushSupport
        {
            get
            {
                return enuBrushSupport.All;
            }
        }
        new class Mecanism : Core2DDrawingLayeredDualBrushElement.Mecanism 
        { 
        }
        [System.Runtime.InteropServices.DllImport(@"gdiplus.dll")]
        public static extern int GdipWindingModeOutline(HandleRef path, IntPtr matrix, float flatness);
        protected override void GeneratePath()
        {
            Region rg = this.GetRegion();
            if (rg == null)
            {
                this.SetPath(null);
                return;
            }
            Graphics g = Graphics.FromHwnd(IntPtr.Zero);
            if (rg.IsEmpty(g))
            {
                g.Dispose();
                this.m_regionpath = null;
                this.SetPath(null);
                return;
            }
            this.m_regionpath = IGK.DrSStudio.
                Drawing2D.RegionUtils.GetPath(rg);
            CoreGraphicsPath path = new CoreGraphicsPath();
            path.AddRectangle(this.m_Region.GetBounds(g));
            g.Dispose();
            //path.AddRectangles (rg.GetRegionScans (new Matrix ()));
            //HandleRef handle = new HandleRef(path, (IntPtr)path.GetType().GetField("nativePath", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(path));
            //// Change path so it only contains the outline
            //GdipWindingModeOutline(handle, IntPtr.Zero, 0.25F);
            this.SetPath(path);
        }
        CoreGraphicsPath m_regionpath;
    }
}

