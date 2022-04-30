

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GifImageHistogram.cs
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
file:GifImageHistogram.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
namespace IGK.DrSStudio.Drawing2D.GifAddIn
{
    interface IGifImageCount{
        int Count{get;}
    }
    class GifImageHistogram
    {
        ImageHistogramInfo[] m_colors;
        public ImageHistogramInfo[] getInfos()
        {
            return m_colors;
        }
        private GifImageHistogram()
        {
            this.m_colors = null;
        }
        public static GifImageHistogram GetHistogram(System.Drawing.Bitmap bmp)
        {
            BitmapData v_bdata = bmp.LockBits(new Rectangle(Point.Empty, bmp.Size),
                 ImageLockMode.ReadOnly,
                  PixelFormat.Format24bppRgb);
            GifImageHistogram gram = new GifImageHistogram();
            Byte[] v_data = new byte[v_bdata.Stride * v_bdata.Height];
            Marshal.Copy(v_bdata.Scan0, v_data, 0, v_data.Length);
            //get or build histogram list
            byte r = 0, g = 0, b = 0;
            int v_offset = 0;
            int w = bmp.Width;
            int h = bmp.Height;
            int stride = v_bdata.Stride;
            ImageHistogramInfo[] rrTab = new ImageHistogramInfo[(256 * 256 * 256)];
            int index = 0;
            for (int j = 0; j < h; j++)
            {
                v_offset = (j * stride);
                for (int x = 0; x < w; x++)
                {
                    b = v_data[v_offset];
                    g = v_data[v_offset + 1];
                    r = v_data[v_offset + 2];
                    index = (r << 16) + (g << 8) + b;
                    rrTab[index].Count++;
                    v_offset += 3;
                }
            }
            bmp.UnlockBits(v_bdata);
            //filter
            List<ImageHistogramInfo> m_t = new List<ImageHistogramInfo>();
            for (int i = 0; i < rrTab.Length; i++)
            {
                if (rrTab[i].Count == 0) continue;
                ImageHistogramInfo p = rrTab[i];
                p.Color = ImageHistogramColor.FromInt(i);
                m_t.Add(p);
            }
            gram.m_colors = m_t.ToArray();
            return gram;
        }
        /// <summary>
        /// represent the image histogram info
        /// </summary>
        public struct ImageHistogramInfo : IGifImageCount, IComparer<ImageHistogramInfo>,
            System.Collections.IComparer
        {
            private int m_Count;
            private ImageHistogramColor m_Color;
            public override string ToString()
            {
                return m_Color.ToString() + " [ " + this.Count + " ] ";
            }
            public ImageHistogramColor Color
            {
                get { return m_Color; }
                set
                {
                    if (m_Color.Equals(value) == false)
                    {
                        m_Color = value;
                    }
                }
            }
            public int Count
            {
                get { return m_Count; }
                set
                {
                    if (m_Count != value)
                    {
                        m_Count = value;
                    }
                }
            }
            public ImageHistogramInfo(int count, ImageHistogramColor cl)
            {
                this.m_Color = cl;
                this.m_Count = count;
            }
            public int Compare(ImageHistogramInfo x, ImageHistogramInfo y)
            {
                return x.Count.CompareTo(y.Count);
            }
            public int Compare(object x, object y)
            {
                return Compare((ImageHistogramInfo)x, (ImageHistogramInfo)y);
            }
        }
        public struct ImageHistogramColor
        {
            internal byte iRED;
            internal byte iGreen;
            internal byte iBlue;
            public System.Drawing.Color GetColor()
            {
                return System.Drawing.Color.FromArgb(iRED, iGreen, iBlue);
            }
            public ImageHistogramColor(byte R, byte G, byte B)
            {
                this.iRED = R;                
                this.iGreen = G;
                this.iBlue = B;
            }
            public override string ToString()
            {
                return string.Format("[{0},{1},{2}]", this.iRED, this.iGreen, this.iBlue);
            }
            internal static ImageHistogramColor FromInt(int i)
            {
                byte r = (byte)((i & 0xFF0000 )>> 16);
                byte g = (byte)((i & 0x00FF00 )>> 8);                
                byte b = (byte) (i & 0x0000FF);
                return new ImageHistogramColor(r, g, b);
            }
        }
    }
}

