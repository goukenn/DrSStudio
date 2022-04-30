

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: __PotSelectionElement.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
using IGK.ICore;
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
///*
//IGKDEV @ 2008-2016
//author: C.A.D . BONDJE DOUE
//file:PotSelectionElement.cs
//*/
///* 
//-------------------------------------------------------------------
//Company: IGK-DEV
//Author : C.A.D. BONDJE DOUE
//SITE : http://www.igkdev.be
//Application : DrSStudio
//powered by IGK - DEV &copy; 2008-2012
//THIS FILE IS A PART OF THE DRSSTUDIO APPLICATION. SEE "License.txt"
//FOR MORE INFORMATION ABOUT THE LICENSE
//------------------------------------------------------------------- 
//*/
///* 
//-------------------------------------------------------------
//This file is part of iGK-DEV-DrawingStudio
//-------------------------------------------------------------
//-------------------------------------------------------------
//-------------------------------------------------------------
//view license file in Documentation folder to get more info
//Copyright (c) 2008-2010 
//Author  : Charles A.D. BONDJE DOUE 
//mail : bondje.doue@hotmail.com
//-------------------------------------------------------------
//-------------------------------------------------------------
//*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using IGK.ICore.Drawing2D.Mecanism;
using IGK.ICore.GraphicModels;
using IGK.DrSStudio.Imaging.Mecanism;


namespace IGK.DrSStudio.Imaging{
    [Core2DDrawingImageItem("Pot", typeof (Mecanism))]
    sealed class ImagePotElement : RectangleElement
    {
        public override enuBrushSupport BrushSupport
        {
            get
            {
                return enuBrushSupport.Fill | enuBrushSupport.Solid;
            }
        }
        new class Mecanism : ImageMecanismBase<ImagePotElement>
        {
            private ImagePotElement m_pot;

            public override void Render(ICoreGraphics device)
            {
                if (this.ImageElement == null)
                    return;

                Rectanglef v_rc = 
                    this.CurrentSurface.GetScreenBound (this.ImageElement.GetBound ());
                //render bounds
                device.DrawRectangle(m_pot.StrokeBrush,
                    v_rc.X, v_rc.Y, v_rc.Width, v_rc.Height);


            }
            public Mecanism()
            {
                this.m_pot = new ImagePotElement();
                
                this.m_pot.StrokeBrush.SetStrokeProperty(
                    5, enuPenAlignment.Center,
                     enuDashStyle.Solid,
                      enuLineCap.Square, enuLineCap.Square, enuDashCap.Round, enuLineJoin.Miter, 4);
                this.m_pot.StrokeBrush.SetHatchBrush(
                    Colorf.Transparent, Colorf.Black, enuHatchStyle.BackwardDiagonal);
            }
            public override bool Register(ICore2DDrawingSurface t)
            {
                return base.Register(t);
            }
            public override bool UnRegister()
            {
                return base.UnRegister();
            }
            public void RenderPot(Vector2i location, Colorf newColor)
            {
                if ((this.ImageElement == null) ||
                    (location.X < 0) || (location.X >= this.ImageElement.Width ) ||
                    (location.Y < 0) || (location.Y >= this.ImageElement.Height  ))

                    return;


                Bitmap v_bmp = this.ImageElement.Bitmap .ToGdiBitmap ();
                WinCoreBitmapData v_cbmp = WinCoreBitmapData.FromBitmap(v_bmp);
                bool v_effect = false;//indicate that the effect is applyied;
                Colorf oldColor =
                    v_cbmp.ReadPixel(location.X, location.Y).ToIGKColor();

                if (!oldColor.Equals (newColor))
                { 
                    //applying pot algorithm
                    Queue<Vector2i> v_queue = new Queue<Vector2i>(4);
                    v_queue.Enqueue(location);
                    Vector2i v_tpt = Vector2i.Zero;
                    int w = v_cbmp.Width;
                    int h = v_cbmp.Height;
                    int offset = 0;

                    int r = (oldColor.R).TrimByte();
                    int g = (oldColor.G).TrimByte();
                    int b = (oldColor.B).TrimByte();
                    int a = (oldColor.A).TrimByte();

                    byte nr = (newColor.R).TrimByte();
                    byte ng = (newColor.G).TrimByte();
                    byte nb = (newColor.B).TrimByte();
                    byte na = (newColor.A).TrimByte();

                    List<Vector2i> voisin = new List<Vector2i>(4);
                    //colore le premier point
                    byte[] dat = v_cbmp.Data;
                    int v_stride = (int)v_cbmp.Stride;
                    offset = (location.Y * v_stride + 4 * location.X);
                    dat[offset] = nb;
                    dat[offset + 1] = ng;
                    dat[offset + 2] = nr;
                    dat[offset + 3] = na;
                    int max_item = h * v_stride;
                    while ((v_queue.Count > 0)&&(v_queue.Count<max_item))
                    {
                        v_tpt = v_queue.Dequeue();
                        //en ueue new data
                        if (v_tpt.X-1 >= 0) voisin.Add(new Vector2i(v_tpt.X - 1, v_tpt.Y));
                        if (v_tpt.Y-1 >= 0) voisin.Add(new Vector2i(v_tpt.X, v_tpt.Y - 1));
                        if (v_tpt.X < w - 1) voisin.Add(new Vector2i(v_tpt.X + 1, v_tpt.Y));
                        if (v_tpt.Y < h - 1) voisin.Add(new Vector2i(v_tpt.X, v_tpt.Y + 1));
                        for (int i = 0; i < voisin.Count; ++i)
                        {
                            offset = voisin[i].Y * v_stride  + voisin[i].X * 4;
                            if ((dat[offset] == b) &&
                                (dat[offset + 1] == g) &&
                                (dat[offset + 2] == r) &&
                                (dat[offset + 3] == a)
                                )
                            {
                                //colori le voisin
                                dat[offset] = nb;// (newColor.B * 255).TrimByte();
                                dat[offset + 1] =ng;// (newColor.G * 255).TrimByte();
                                dat[offset + 2] = nr;
                                dat[offset + 3] = na;
                                v_queue.Enqueue(voisin[i]);
                            }
                        }
                        voisin.Clear();
                    }
                    v_effect = true;
                }
                v_bmp.Dispose();                
                if (v_effect)
                {
                    v_bmp = v_cbmp.ToBitmap();
                    this.ImageElement.SetBitmap(v_bmp, false);
                    this.CurrentSurface.RefreshScene();
                }
            }

            protected override void OnMouseClick(CoreMouseEventArgs e)
            {
                switch (e.Button)
                {
                    case enuMouseButtons.Left:
                        if (this.ImageElement != null)
                        {
                            RenderPot(
                                GetImageElementLocation(new Vector2i ((int)e.FactorPoint.X,(int) e.FactorPoint.Y) ),
                                this.m_pot.FillBrush.Colors[0]);
                        }
                        else { 
                            //pick a image element
                            CheckElement(e);
                        }
                        break;                
                }
            }



            protected override bool CheckElement(CoreMouseEventArgs e)
            {
                if (base.CheckElement(e))
                {
                    this.CurrentSurface.ElementToConfigure = m_pot;
                    return true;
                }
                return  false;
            }
            protected override void OnMouseDown(CoreMouseEventArgs e)
            {
                //do nothing
            }
            protected override void OnMouseMove(CoreMouseEventArgs e)
            {
                //do nothing;
            }
            protected override void OnMouseUp(CoreMouseEventArgs e)
            {
                //do nothing
            }
        }
    }
}
////////////namespace IGK.DrSStudio.Imaging
//{
//    /// <summary>
//    /// Represent pot render mecanism
//    /// </summary>
//    [Core2DDrawingImageItemAttribute("Pot", typeof(ImageElement_Pot.Mecanism))]
//    sealed class ImageElement_Pot : Core2DDrawingDualBrushElement   
//    {
//        static Colorf sm_newColor;
//        static Colorf sm_oldColor;
//        static ImageElement  sm_bitmap;
//        static Bitmap sm_odlBitmap;
      
        
//        public override enuBrushSupport BrushSupport
//        {
//            get
//            {
//                return enuBrushSupport.Fill | enuBrushSupport.Solid;
//            }
//        }
//        public override ICoreBrush GetBrush(enuBrushMode mode)
//        {
//            switch (mode)
//            { 
//                case enuBrushMode.Fill :
//                    return this.FillBrush;
//            }
//            return null;            
//        }
//        private static void ApplyPot(ImageElement image, Vector2f pointF , Colorf newColor)
//        {
//            sm_bitmap = image;
//            sm_newColor = newColor ;
//            //get real point in this image
//            Matrix m = image .GetMatrix ().Clone () as Matrix ;
//            Point pt = Point.Empty ;
//            int v_cmb = sm_oldColor.IntValue();
//            int nr = (int)(newColor.R * 255);
//            int ng = (int)(newColor.G * 255);
//            int nb = (int)(newColor.B * 255);
//            int na = (int)(newColor.A * 255);

//            int a =  (int)(sm_oldColor.A* 255);
//            int g =  (int)(sm_oldColor.G* 255);
//            int b =  (int)(sm_oldColor.B* 255);
//            int r = (int)(sm_oldColor.R * 255);


//            if (m.IsInvertible && !m.IsIdentity )
//            {
//                Vector2f[] tps = new Vector2f[] { pointF };
//                m.Invert ();
//                m.TransformPoints (tps);
//                pt = new Point ((int)tps[0].X , (int)tps[0].Y );
//            }
//            else {
//                pt = new Point ((int)pointF.X  , (int)pointF .Y );
//            }
//            m.Dispose ();   
//            //return if image don't containt point
//            if ((pt .X <0)||(pt.X >= sm_bitmap.Width ))return ;
//            if ((pt .Y <0)||(pt.Y >= sm_bitmap.Height ))return ;
//            int offset = 0;
//            sm_odlBitmap = (((WinCoreBitmap)sm_bitmap.Bitmap).Clone() as WinCoreBitmap).Bitmap;
//            BitmapData data = sm_odlBitmap.LockBits (new Rectangle (Point.Empty, sm_odlBitmap.Size ),
//                ImageLockMode.ReadWrite , PixelFormat.Format32bppPArgb );
//            Byte[] dat = new byte[data.Stride * data.Height  ];
//            Marshal.Copy (data.Scan0 , dat, 0, dat.Length );



//            offset = (pt.Y * data.Stride + 4 * pt.X) ;
//            sm_oldColor = Colorf.FromIntArgb (
//                Marshal.ReadByte (data.Scan0 , offset +3),
//                Marshal.ReadByte (data.Scan0 , offset+2),
//                Marshal.ReadByte (data.Scan0 , offset+1),
//                Marshal.ReadByte (data.Scan0 , offset)                
//                );
//            if (sm_newColor.IntValue() == v_cmb)
//            {

//                sm_odlBitmap.UnlockBits(data);
//                return;
//            }

//            //if color the same
//            if (sm_newColor == sm_oldColor)
//            {
//                sm_odlBitmap.UnlockBits(data);
//                return;
//            }
//            Queue<Point> v_queue = new Queue<Point> (4);
//            v_queue.Enqueue (pt );
//            Point v_tpt ;
//            int w = sm_odlBitmap.Width ;
//            int h = sm_odlBitmap .Height ;
//            List<Point> voisin = new List<Point>(4);
//            //colorie le premier point
//            offset = (pt.Y * data.Stride + 4 * pt.X);
//            dat[offset] = (newColor.B * 255).TrimByte();
//            dat[offset + 1] = (newColor.G* 255).TrimByte();
//            dat[offset + 2] = (newColor.R* 255).TrimByte();
//            dat[offset + 3] =(
//                newColor.A* 255).TrimByte();
//            while (v_queue.Count > 0)
//            {
//                v_tpt = v_queue.Dequeue ();
//                //en ueue new data
//                if (v_tpt .X > 0) voisin .Add (new Point (v_tpt.X -1, v_tpt .Y) );
//                if (v_tpt .Y > 0) voisin .Add  (new Point (v_tpt.X , v_tpt .Y -1));
//                if (v_tpt .X < w -1 ) voisin .Add (new Point (v_tpt.X +1, v_tpt .Y ));
//                if (v_tpt .Y < h -1 )voisin .Add (new Point (v_tpt.X, v_tpt .Y+1 ));
//                for (int i = 0; i < voisin.Count; ++i)
//                {
//                    offset = voisin[i].Y * data.Stride + voisin[i].X * 4;
//                    if ((dat[offset] == b)&&
//                        (dat[offset+1] == g)&&
//                        (dat[offset+2] == r)&&
//                        (dat[offset+3] == a)
//                        )
//                    {
//                        //colori le voisin
//                        dat[offset] = (newColor.B * 255).TrimByte ();
//                        dat[offset + 1] = (newColor.G* 255).TrimByte ();
//                        dat[offset + 2] = (newColor.R* 255).TrimByte ();
//                        dat[offset + 3] = (newColor.A* 255).TrimByte ();
//                        v_queue.Enqueue(voisin[i]);
//                    }
//                }
//                voisin.Clear();
//            }
//            Marshal.Copy (dat,0, data.Scan0 , dat.Length );
//            //unloack bit
//            sm_odlBitmap.UnlockBits (data);
//            //apply bitmap
//            sm_bitmap .SetBitmap (sm_odlBitmap,false );
           
//        }
//        /// <summary>
//        /// mecanism to render
//        /// </summary>
//        new class Mecanism : IGK.DrSStudio.Drawing2D.Mecanism.Core2DDrawingRectangleMecanismBase<ImageElement_Pot>
//        {
//            private ImageElement_Pot m_potElement;
          
//            public Mecanism() {
//                this.m_potElement = new ImageElement_Pot();
//            }
//            public override bool Register(ICore2DDrawingSurface surface)
//            {
//                bool r = base.Register(surface);
//                if (r)
//                {
//                    surface.ElementToConfigure = this.m_potElement;
//                }
//                return r;
//            }
//            public override void Dispose()
//            {
//                if (this.m_potElement != null)
//                {
//                    this.m_potElement.Dispose();
//                    this.m_potElement = null;
//                }
//                base.Dispose();
//            }
//            public void SetElementToConfigure(ICoreWorkingObject element)
//            {
//                if (element is ImageElement)
//                {
//                    this.m_ImageElement = element as ImageElement;
//                    //get the current layer
//                    this.CurrentSurface.CurrentLayer.Select(this.m_ImageElement);
//                }
//                else
//                {
//                    this.m_ImageElement = null;
//                    this.CurrentSurface.CurrentLayer.Select(null);
//                }
//                this.CurrentSurface.ElementToConfigure = this.m_potElement;
//            }
//            protected override void OnMouseDown(CoreMouseEventArgs e)
//            {
//                switch (e.Button)
//                {
//                    case enuMouseButtons.Left:
//                        //check selected element is image if not
//                        if (this.Element == null)
//                        {
//                            if (!CheckElement(e))
//                            {
//                                this.SetElementToConfigure(null);
//                            }
//                        }
//                        else
//                        {
//                            if (!this.Element.Contains(e.FactorPoint))
//                            {
//                                if (!CheckElement(e))
//                                {
//                                    this.SetElementToConfigure(null);
//                                }
//                            }
//                        }
//                        break;
//                }
//            }
//            private bool CheckElement(CoreMouseEventArgs e)
//            {
//                //check first image element
//                ICore2DDrawingLayer l = this.CurrentSurface.CurrentLayer;
//                for (int i = l.Elements.Count - 1; i >= 0; --i)
//                {
//                    if (l.Elements[i].Contains(e.FactorPoint) && (l.Elements[i] is ImageElement))
//                    {
//                        this.m_ImageElement = l.Elements[i] as ImageElement;
//                        //select the element in the layer
//                        l.Select(this.m_ImageElement);
//                        //toggle element to configure
//                        this.CurrentSurface.ElementToConfigure = m_potElement;
//                        return true;
//                    }
//                }
//                return false;
//            }
//            protected override void OnMouseMove(CoreMouseEventArgs e)
//            {
//                //do nothing
//            }
//            protected override void OnMouseUp(CoreMouseEventArgs e)
//            {
//                switch (e.Button)
//                {
//                    case enuMouseButtons.Left:
//                        {
//                            if (m_ImageElement !=null)
//                            {
//                                this.CurrentSurface.SetCursor (Cursors.WaitCursor);
//                                Colorf newColor = this.m_potElement.FillBrush.Colors[0];
//                                ApplyPot(m_ImageElement, e.FactorPoint, newColor);
//                                this.Invalidate();
//                                this.CurrentSurface.Cursor = this.GetCursor();
//                            }
//                        }
//                        break;
//                    case enuMouseButtons.Right:
//                        this.AllowContextMenu = false;
//                        break;
//                }
//            }

//            private  ImageElement m_ImageElement;
//        }

//        protected override void InitGraphicPath(CoreGraphicsPath path)
//        {
//            path.Reset();
//        }
//    }
//}

