

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GifGlobalColorTable.cs
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
file:GifGlobalColorTable.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
namespace IGK.DrSStudio.GifAddIn.Gif
{
    public class GifGlobalColorTable : GifDataEntityBase
    {
        List<STRUCTGifColor> m_colors;
        /// <summary>
        /// get the colors
        /// </summary>
        public STRUCTGifColor[] Colors{ get{return this.m_colors.ToArray(); }}
        public GifGlobalColorTable()
        {
            this.m_colors = new List<STRUCTGifColor>();
        }
        public override string Name
        {
	        get { return GifConstant .GIF_GLOBAL_COLOR; }
        }
        public override void Read(System.IO.Stream stream)
        {
            if (Chain == null)
                return;
            GifScreenDescription desc = (GifScreenDescription)this.Chain.GetEntity(GifConstant.GIF_SCREEN_DESC);
            int Color = desc.NumberOfColor;
            STRUCTGifColor c;
            m_colors.Clear();
            for (int i = 0; i < Color; i++)
            {
                c = (STRUCTGifColor)GifConstant.ReadStruct(stream, typeof(STRUCTGifColor));
                m_colors.Add(c);
            }
        }
        public override void Write(System.IO.Stream stream)
        {
            if (this.m_colors.Count <= 0)
                return;
            GifScreenDescription desc = (GifScreenDescription)this.Chain.GetEntity(GifConstant.GIF_SCREEN_DESC);
            int Color = desc.NumberOfColor;
            if ((Color == 0)) return;
            int i = 0;
            for (; (i < Color) && (i<this.m_colors.Count); i++)
            {
                STRUCTGifColor item = this.m_colors[i];
                GifConstant.WriteStruct(stream, item);
            }
            if (i < Color)
            {
                STRUCTGifColor item = new STRUCTGifColor();
                for (; i < Color; i++)
                {
                    GifConstant.WriteStruct(stream, item);
                }
            }
            stream.Flush();
        }
        public override void Copy(ICoreDataEntity dataEntity)
        {
            GifGlobalColorTable cl = dataEntity as GifGlobalColorTable;
            if (cl == null) return;
            this.m_colors.Clear();
            List<STRUCTGifColor> m_colors = cl.m_colors ;
            foreach (var item in m_colors)
            {
                this.m_colors.Add(item);
            }
        }
        internal void ReplacePalette(System.Drawing.Color[] palette)
        {
            this.m_colors.Clear();
            foreach (System.Drawing.Color item in palette)
            {
                this.m_colors.Add( new STRUCTGifColor (item.R,item.G,item.B));
            }
        }
    }
}

