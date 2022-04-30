

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GifEncoder.cs
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
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:GifEncoder.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Runtime.InteropServices;
using System.Reflection;
namespace IGK.DrSStudio.Drawing2D.GifAddIn.Codec
{
    using IGK.ICore.Codec;
    using IGK.ICore.Drawing2D.Codec;
    using IGK.DrSStudio.GifAddIn;
    using IGK.DrSStudio.GifAddIn.Gif;
    using IGK.ICore.WinUI.Configuration;
    using IGK.DrSStudio.Drawing2D.Codec;
    [CoreCodec("gifEncoder", "picture/gif", GifConstant.FILE_EXTENSION, 
        Category = CoreConstant.CAT_PICTURE)]
    sealed class GifEncoder : IGKD2DBitmapEncoder
    {
        private int m_TimePerFrame;
        private bool m_EncodeWithGdi;
        private int m_TransparentColorIndex;
        private bool m_AllowTransparentColor;
        /// <summary>
        /// allow transparent color
        /// </summary>
        public bool AllowTransparentColor
        {
            get { return m_AllowTransparentColor; }
            set
            {
                if (m_AllowTransparentColor != value)
                {
                    m_AllowTransparentColor = value;
                }
            }
        }
        /// <summary>
        /// get or set the transparent color index
        /// </summary>
        public int TransparentColorIndex
        {
            get { return m_TransparentColorIndex; }
            set
            {
                if (m_TransparentColorIndex != value)
                {
                    m_TransparentColorIndex = value;
                }
            }
        }
        public bool EncodeWithGdi
        {
            get { return m_EncodeWithGdi; }
            set
            {
                if (m_EncodeWithGdi != value)
                {
                    m_EncodeWithGdi = value;
                }
            }
        }
        /// <summary>
        /// get or set the time per frame
        /// </summary>
        public int TimePerFrame
        {
            get { return m_TimePerFrame; }
            set
            {
                if ((m_TimePerFrame != value)&& (value > 0))
                {
                    m_TimePerFrame = value;
                }
            }
        }
        public override bool CanConfigure
        {
            get
            {
                return true;
            }
        }
        public override enuParamConfigType GetConfigType()
        {
            return enuParamConfigType.ParameterConfig;
        }
        public GifEncoder()
        {
            this.m_TimePerFrame = 1000;
            this.m_EncodeWithGdi = false;
            this.m_AllowTransparentColor = true;
            this.m_TransparentColorIndex = 0;
        }
        public override ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
        {
            parameters = base.GetParameters(parameters);
            var g = parameters.AddGroup("Property");
            Type t = this.GetType();
            g.AddItem(t.GetProperty("TimePerFrame"));
            g.AddItem(t.GetProperty("EncodeWithGdi"));
            g.AddItem(t.GetProperty("AllowTransparentColor"));
            g.AddItem(t.GetProperty("TransparentColorIndex"));
            return parameters;
        }
        public bool SaveGiffile(GifFileDocument document)
        {
            if (document == null)
                return false;
            return document.Save();
        }
        protected override bool SaveDocument(string filename, ICore2DDrawingDocument[] documents)
        {
            if ((documents == null) || (documents.Length == 0))
                return false;
            GifDataChain chain = GifDataChain.Create();
            ICore2DDrawingDocument doc = documents[0];
            Bitmap bmp = new Bitmap(doc.Width, doc.Height);
            Graphics g = Graphics.FromImage(bmp);
            doc.Draw(g);
            g.Flush();
            if (!this.EncodeWithGdi)
            {
                GifImageHistogram gram = GifImageHistogram.GetHistogram(bmp);
                Color[] v_palette = null;
                GifImageHistogram.ImageHistogramInfo[] v_hinfos = gram.getInfos();
                if (v_hinfos.Length < 256)
                {
                    //main palette is lower
                    Array.Sort(v_hinfos, v_hinfos[0]);
                    Array.Reverse(v_hinfos);
                    v_palette = new Color[v_hinfos.Length];
                    for (int i = 0; i < v_palette.Length; i++)
                    {
                        v_palette[i] = v_hinfos[i].Color.GetColor();
                    }
                    chain.getGlobalColorTable().ReplacePalette(v_palette);
                    //get the bitmap for this presentation
                    Bitmap cbmp = SaveBitmapAs8bpp(bmp, v_palette);
                    //System.Windows.Forms.Form frm = new System.Windows.Forms.Form();
                    //System.Windows.Forms.PictureBox box = new System.Windows.Forms.PictureBox ();
                    //box.Image = cbmp;
                    //box.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    //box.Dock = System.Windows.Forms.DockStyle.Fill;
                    //frm.Controls.Add(box);
                    //frm.ShowDialog();
                    chain.AddFrame(cbmp);
                    //chain.AddFrame(bmp);
                    cbmp.Dispose();
                }
                else 
                    chain.AddFrame(bmp);
            }
            else
                chain.AddFrame(bmp);
            //before add chain
            g.Flush();
              for(int i = 1; i < documents.Length ; i++)
              { 
                g.Clear (Color.Transparent );
                documents[i].Draw(g);
                g.Flush();
                chain.AddFrame(bmp);
             }
             chain.getChainEntity().setAllInterval(this.TimePerFrame);
             chain.getChainEntity().setAllSupportTransparentColor(this.AllowTransparentColor);
             chain.getChainEntity().setAllTransparentColorIndex(this.TransparentColorIndex );
             chain.SaveTo(filename);
             return true;
        }
        /// <summary>
        /// save as 8pbmp by generate the color palette
        /// </summary>
        /// <param name="bmp"></param>
        /// <returns></returns>
        public static Bitmap SaveBitmapAs8bpp(Bitmap bmp, Color[] palettes)
        {
            Color[] p = palettes;
            //out put bitmap
            Bitmap v_bmp = new Bitmap(bmp.Width, bmp.Height, PixelFormat.Format8bppIndexed);
            int v_w = bmp.Width;
            int v_h = bmp.Height;
            if (p != null)
            {//set the palettes
                //set the color palette
                ColorPalette pal = v_bmp.Palette;
                FieldInfo f = pal.GetType().GetField("entries", BindingFlags.NonPublic | BindingFlags.Instance);
                //set the palette
                f.SetValue(pal, p);
                v_bmp.Palette = pal;
                BitmapData v_data = bmp.LockBits(new Rectangle(Point.Empty, bmp.Size),
                  ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
                BitmapData v_data2 = v_bmp.LockBits(new Rectangle(Point.Empty, bmp.Size),
                              ImageLockMode.ReadWrite, PixelFormat.Format8bppIndexed);
                int offset = 0;
                int offset2 = 0;
                int r = 0;
                int g = 0;
                int b = 0;
                //allocate
                byte[] dic = new byte[256 * 256 * 256];
                int index = 0;
                for (int i = 0; i < p.Length; i++)
                {
                    index = (p[i].R << 16) + (p[i].G <<8) + p[i].B;
                    dic[index ] = (byte) i;
                }
              //  Color v_cl = Color.Empty;
                for (int h = 0; h < bmp.Height; h++)
                {
                    for (int w = 0; w < bmp.Width; w++)
                    {
                        offset = h * v_data.Stride + w * 3;
                        offset2 = h * v_data2.Stride + w;
                       b= Marshal.ReadByte(v_data.Scan0, offset);
                       g = Marshal.ReadByte(v_data.Scan0, offset + 1);
                       r = Marshal.ReadByte(v_data.Scan0, offset + 2);
                      //  v_cl = Color.FromArgb(r, g, b);
                        index = (r << 16) + (g << 8) + b;                      
                       Marshal.WriteByte(v_data2.Scan0, offset2, dic[index]);
                    }
                }
                bmp.UnlockBits(v_data);
                v_bmp.UnlockBits(v_data2);
            }
            return v_bmp;
        }
    }
}

