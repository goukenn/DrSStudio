

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: PotSelectionElement.cs
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
file:PotSelectionElement.cs
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
/* 
-------------------------------------------------------------
This file is part of iGK-DEV-DrawingStudio
-------------------------------------------------------------
-------------------------------------------------------------
-------------------------------------------------------------
view license file in Documentation folder to get more info
Copyright (c) 2008-2010 
Author  : Charles A.D. BONDJE DOUE 
mail : bondje.doue@hotmail.com
-------------------------------------------------------------
-------------------------------------------------------------
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms ;
using System.Drawing ;
using System.Drawing .Drawing2D ;
using System.Drawing.Imaging  ;
using System.Runtime.InteropServices ;
using IGK.ICore;using IGK.DrSStudio.WinUI;
using IGK.DrSStudio.Drawing2D;
using IGK.DrSStudio.Menu;
using IGK.DrSStudio.Drawing2D.Menu;
namespace IGK.DrSStudio.Drawing2D.Imaging
{
    /// <summary>
    /// Represent pot render mecanism
    /// </summary>
    [Core2DDrawingImageItemAttribute("Pot", typeof(ImageElement_Pot.Mecanism))]
    sealed class ImageElement_Pot : Core2DDrawingLayeredDualBrushElement   
    {
        static Color sm_newColor;
        static Color sm_oldColor;
        static ImageElement  sm_bitmap;
        static Bitmap sm_odlBitmap;
        protected override void GeneratePath()
        {
            //do nothing
        }
        public override enuBrushSupport BrushSupport
        {
            get
            {
                return enuBrushSupport.Fill | enuBrushSupport.Solid;
            }
        }
        public override ICoreBrush GetBrush(enuBrushMode mode)
        {
            switch (mode)
            { 
                case enuBrushMode.Fill :
                    return this.FillBrush;
            }
            return null;            
        }
        private static void ApplyPot(ImageElement image, System.Drawing.Vector2f pointF , Color newColor)
        {
 	        sm_bitmap = image;
            sm_newColor = newColor ;
            //get real point in this image
            Matrix m = image .GetMatrix ().Clone () as Matrix ;
            Point pt = Point.Empty ;
            if (m.IsInvertible && !m.IsIdentity )
            {
                Vector2f[] tps = new Vector2f[]{pointF};
                m.Invert ();
                m.TransformPoints (tps);
                pt = new Point ((int)tps[0].X , (int)tps[0].Y );
            }
            else {
                pt = new Point ((int)pointF.X  , (int)pointF .Y );
            }
            m.Dispose ();   
            //return if image don't containt point
            if ((pt .X <0)||(pt.X >= sm_bitmap.Width ))return ;
            if ((pt .Y <0)||(pt.Y >= sm_bitmap.Height ))return ;
            int offset = 0;
            sm_odlBitmap = sm_bitmap.Bitmap.Clone () as Bitmap ;
            BitmapData data = sm_odlBitmap.LockBits (new Rectangle (Point.Empty, sm_odlBitmap.Size ),
                ImageLockMode.ReadWrite , PixelFormat.Format32bppPArgb );
            Byte[] dat = new byte[data.Stride * data.Height  ];
            Marshal.Copy (data.Scan0 , dat, 0, dat.Length );
            offset = (pt.Y * data.Stride + 4 * pt.X) ;
            sm_oldColor = Color .FromArgb (
                Marshal.ReadByte (data.Scan0 , offset +3),
                Marshal.ReadByte (data.Scan0 , offset+2),
                Marshal.ReadByte (data.Scan0 , offset+1),
                Marshal.ReadByte (data.Scan0 , offset)                
                );
            if (sm_newColor.ToArgb() == sm_oldColor.ToArgb ()) 
                return;
            int a = sm_oldColor .A ;
            int g = sm_oldColor .G ;
            int b = sm_oldColor .B ;
            int r = sm_oldColor .R ;
            //if color the same
            if (sm_newColor == sm_oldColor )return ;
            Queue<Point> v_queue = new Queue<Point> (4);
            v_queue.Enqueue (pt );
            Point v_tpt ;
            int w = sm_odlBitmap.Width ;
            int h = sm_odlBitmap .Height ;
            List<Point> voisin = new List<Point>(4);
            //colorie le premier point
            offset = (pt.Y * data.Stride + 4 * pt.X);
            dat[offset] = newColor.B;
            dat[offset + 1] = newColor.G;
            dat[offset + 2] = newColor.R;
            dat[offset + 3] = newColor.A;
            while (v_queue.Count > 0)
            {
                v_tpt = v_queue.Dequeue ();
                //enqueue new data
                if (v_tpt .X > 0) voisin .Add (new Point (v_tpt.X -1, v_tpt .Y) );
                if (v_tpt .Y > 0) voisin .Add  (new Point (v_tpt.X , v_tpt .Y -1));
                if (v_tpt .X < w -1 ) voisin .Add (new Point (v_tpt.X +1, v_tpt .Y ));
                if (v_tpt .Y < h -1 )voisin .Add (new Point (v_tpt.X, v_tpt .Y+1 ));
                for (int i = 0; i < voisin.Count; ++i)
                {
                    offset = voisin[i].Y * data.Stride + voisin[i].X * 4;
                    if ((dat[offset] == b)&&
                        (dat[offset+1] == g)&&
                        (dat[offset+2] == r)&&
                        (dat[offset+3] == a)
                        )
                    {
                        //colori le voisin
                        dat[offset] = newColor.B;
                        dat[offset + 1] = newColor.G;
                        dat[offset + 2] = newColor.R;
                        dat[offset + 3] = newColor.A;
                        v_queue.Enqueue(voisin[i]);
                    }
                }
                voisin.Clear();
            }
            Marshal.Copy (dat,0, data.Scan0 , dat.Length );
            //unloack bit
            sm_odlBitmap.UnlockBits (data);
            //apply bitmap
            sm_bitmap .SetBitmap (sm_odlBitmap,false );
        }
        /// <summary>
        /// mecanism to render
        /// </summary>
        new class Mecanism : Core2DDrawingMecanismBase 
        {
            ImageElement_Pot m_potElement;
            public new ImageElement Element {
                get {
                    return base.Element as ImageElement;
                }
                set {
                    base.Element = value;
                }
            }
            public Mecanism() {
                this.m_potElement = new ImageElement_Pot();
            }
            protected override void RegisterSurface(IGK.DrSStudio.Drawing2D.WinUI.ICore2DDrawingSurface surface)
            {
                base.RegisterSurface(surface);
                surface.ElementToConfigure = this.m_potElement;
            }
            public override void Dispose()
            {
                if (this.m_potElement != null)
                {
                    this.m_potElement.Dispose();
                    this.m_potElement = null;
                }
                base.Dispose();
            }
            public  void SetElementToConfigure(ICoreWorkingObject element)
            {                
                if (element is ImageElement)
                {
                    base.Element = element as ImageElement ;
                    //get the current layer
                    this.CurrentSurface.CurrentLayer.Select(this.Element);                    
                }
                else
                {                    
                    base.Element = null;
                    this.CurrentSurface.CurrentLayer.Select(null);
                }
                this.CurrentSurface.ElementToConfigure = this.m_potElement;
            }
            protected override void OnMouseDown(IGK.DrSStudio.Drawing2D.WinUI.CoreMouseEventArgs e)
            {
                switch (e.Button)
                {
                    case MouseButtons.Left :
                        //check selected element is image if not
                        if (this.Element == null)
                        {
                            if (!CheckElement(e))
                            {
                                this.SetElementToConfigure(null);
                            }
                        }
                        else {
                            if (!this.Element.Contains(e.FactorPoint))
                            {
                                if (!CheckElement(e))
                                {
                                    this.SetElementToConfigure(null);
                                }
                            }
                        }
                        break;
                }
            }
            private bool CheckElement(IGK.DrSStudio.Drawing2D.WinUI.CoreMouseEventArgs e)
            {
                //check first image element
                ICore2DDrawingLayer l = this.CurrentSurface.CurrentLayer;
                for (int i = l.Elements.Count - 1; i >= 0; --i)
                {
                    if (l.Elements[i].Contains (e.FactorPoint )&& (l.Elements[i] is ImageElement))
                    {
                        this.Element = l.Elements[i] as ImageElement;
                        l.Select(this.Element);
                        this.CurrentSurface.ElementToConfigure = m_potElement;
                        return true;
                    }
                }
                return false;
            }
            protected override void OnMouseMove(IGK.DrSStudio.Drawing2D.WinUI.CoreMouseEventArgs e)
            {                
                //do nothing
            }
            protected override void  OnMouseUp(IGK.DrSStudio.Drawing2D.WinUI.CoreMouseEventArgs e)
            {
                switch (e.Button)
                {
                    case MouseButtons.Left:
                        {
                            if (this.Element is ImageElement)
                            {
                                this.CurrentSurface.Cursor = Cursors.WaitCursor;
                                ApplyPot((this.Element as ImageElement), e.FactorPoint, this.CurrentSurface.CurrentColor);
                                //invalidate image
                                this.CurrentSurface.Invalidate(this.Element);
                                this.CurrentSurface.Cursor = this.GetCursor();
                            }
                        }
                        break;
                    case MouseButtons.Right:
                        this.AllowContextMenu  = false;
                        break;
                }
            }
        }
    }
}

