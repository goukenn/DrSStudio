

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GifScreenDescription.cs
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
file:GifScreenDescription.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
namespace IGK.DrSStudio.GifAddIn.Gif
{
    class GifScreenDescription : GifDataEntityBase
    {
        STRUCTGifLogicalHeaderDescription mv;
        [StructLayout(LayoutKind.Sequential,Size=7)]
        struct STRUCTGifLogicalHeaderDescription
        {
            internal short width;
            internal short height;
            internal byte packedfield;
            internal byte backgroundColorIndex;
            internal byte pixelRatio;
            public void setGlobalColorFlags(bool t) { if (t) { this.packedfield = (byte)(this.packedfield | 0x80); } else this.packedfield = (byte)(this.packedfield - 0x80); }
            public void setColorResolution(byte resolution) { }
            public void setSortFlag(bool t) { }
            public void setSizeOfGlobalColorTable(byte tab) { this.packedfield = (byte)(this.packedfield | (tab & 0x7)); }
            public byte BackgroundColorIndex { get { return this.backgroundColorIndex; } }
            public byte SizeOfGlobalColorTable { get{return (byte)(this.packedfield & 0x7); }}
            public byte ColorResolution { get { return (byte)((this.packedfield & 0x70) >> 4); } }
            public void setBackgroundColorIndex(byte value) { this.backgroundColorIndex = value; }
            public bool UseGlobalColor { get { return (this.packedfield & 0x80) > 0; } }
        }
        public int NumberOfColor {
            get {
                return (int) Math.Pow(2, this.mv.SizeOfGlobalColorTable + 1);
            }
        }
        public override void Read(System.IO.Stream stream)
        {
           this.mv = (STRUCTGifLogicalHeaderDescription) GifConstant.ReadStruct(stream, mv.GetType());
        }
        public override void Write(System.IO.Stream stream)
        {
            GifConstant.WriteStruct(stream, mv);
        }
        public override string Render()
        {
            return base.Render();
        }
        public override string Name
        {
            get { return GifConstant.GIF_SCREEN_DESC; }
        }
        public override void Copy(ICoreDataEntity dataEntity)
        {
            GifScreenDescription desc = dataEntity as GifScreenDescription;
            if (desc == null) return;
            this.mv = desc.mv;
        }
    }
}

