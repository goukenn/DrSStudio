

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GifDataChainEntity.cs
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
file:GifDataChainEntity.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.IO;
namespace IGK.DrSStudio.GifAddIn.Gif
{
    public class GifDataChainEntity : GifDataEntityBase
    {
        public class GifFrameEntity
        {
            internal STRUCTGifImageDescriptor imgDesc;
            internal byte lmzcode;
            internal byte[][] data;
            internal STRUCTGifColor[] color;
            public GifFrameEntity(STRUCTGifImageDescriptor desc, byte lmzcode, STRUCTGifColor[] color,  byte[][] data )
            {
                this.color = color ;
                this.imgDesc = desc;
                this.lmzcode = lmzcode;
                this.data = data;
            }
        }
        List<GifFrameEntity> m_frames;
        GifApplicationExtension m_appInfo;
        public bool getContainsApplicationExtension() {
            return (this.m_appInfo != null);
        }
        public GifApplicationExtension getApplicationExtension()
        {
            return this.m_appInfo;
        }
        public void AddApplicationExtension()
        {
            if (this.m_appInfo == null)
            this.m_appInfo = new GifApplicationExtension();
        }
        [StructLayout(LayoutKind.Sequential, Size= 8)]
        public struct STRUCTGifGraphicControlExtension : IGifExtension
        {
            internal byte introducer; // 0x21
            internal byte label;     // 0xf9 //for graphic control extension
            internal byte blocksize; // 0x04 //for this
            internal byte packedfield;
            // 6 .. 8 reserved
            // 3 .. 5 disposal mode
            // 2 use input flag
            // 1 transparent color flags
            internal short delayTime;
            internal byte transparentcolorIndex;
            internal byte blockTerminartor; //0x00    
            static STRUCTGifGraphicControlExtension sm_Default;
            public static STRUCTGifGraphicControlExtension Default
            {
                get
                {
                    return sm_Default;
                }
            }
            static STRUCTGifGraphicControlExtension()
            {
                sm_Default = new STRUCTGifGraphicControlExtension();
                sm_Default.introducer = 0x21;
                sm_Default.label = 0xf9;
                sm_Default.blocksize = 0x04;
                sm_Default.blockTerminartor = 0x00;
            }
            public byte Introducer
            {
                get { return this.introducer; }
            }
            public byte CommentLabel
            {
                get { return this.label; }
            }
            public int DisposalMethod
            {
                get
                {
                    return (packedfield & 0x1C) >> 2;
                }
            }
            public bool SupportTransparentColor
            {
                get
                {
                    return ((packedfield & 0x1) == 0x1);
                }
                set {
                    if (value)
                    {
                        packedfield = (byte)(packedfield | 0x1);
                    }
                    else {
                        packedfield = (byte)( ((packedfield & 0x1) > 0)?packedfield ^ 0x1 : packedfield);
                    }
                }
            }
        }
        [StructLayout(LayoutKind.Explicit, Size= 10)]
        public struct STRUCTGifImageDescriptor
        {
            [FieldOffset(0)]
            internal byte imageSeparator;
            [FieldOffset(1)]
            internal short leftPosition;
            [FieldOffset(3)]
            internal short topPosition;
            [FieldOffset(5)]
            internal short width;
            [FieldOffset(7)]
            internal short height;
            [FieldOffset(9)]
            internal byte packedfields;
            public static STRUCTGifImageDescriptor Create()
            {
                STRUCTGifImageDescriptor c = new STRUCTGifImageDescriptor();
                c.imageSeparator = 0x2C;
                return c;
            }
            public bool HasColorTable
            {
                get
                {
                    return Convert.ToBoolean((this.packedfields & 0x80));
                }
            }
            public void setColorTable(bool value)
            {
                if (value)
                    this.packedfields = (byte)(this.packedfields | 0x80);
                else
                    this.packedfields = (byte)(this.packedfields - 0x80);
            }
        }
        [StructLayout(LayoutKind.Sequential)]
        public class GifApplicationExtension : IGifExtension
        {
            private short m_loopCount;
            public byte Introducer
            {
                get { return 0x21; }
            }
            public byte CommentLabel
            {
                get { return 0xFF; }
            }
            public byte BlocSize
            {
                get { return 0x0b; }
            }
            public short LoopCount
            {
                get { return m_loopCount; }
                set { m_loopCount = value; }
            }
            public void SaveToStream(BinaryWriter binW)
            {
                binW.Write((byte)this.Introducer);
                binW.Write((byte)this.CommentLabel);
                binW.Write((byte)this.BlocSize);
                char[] b = "NETSCAPE2.0".ToCharArray();
                for (int i = 0; i < b.Length; i++)
                {
                    binW.Write((byte)b[i]);
                }
                binW.Write((byte)0x03);
                binW.Write((byte)0x01);
                binW.Write((short)LoopCount);
                //terminator
                binW.Write((byte)0);
            }
        }
        List<IGifExtension> m_extensions;
        public GifDataChainEntity()
        {
            this.m_extensions = new List<IGifExtension>();
            this.m_frames = new List<GifFrameEntity>();
        }
        public override string Name
        {
            get { return GifConstant.GIF_DATAENTITY; }
        }
        public override void Read(System.IO.Stream stream)
        {
            int r = 0;
            this.m_extensions.Clear();
            bool m_endofStream = false ;
            BinaryReader v_binR = new BinaryReader(stream);
            while (!m_endofStream && (r = stream.ReadByte()) != -1)
            {
                switch (r)
                {
                    case 0x2C:
                        stream.Seek(-1, System.IO.SeekOrigin.Current);
                        int pos =(int) stream.Position ;
                        STRUCTGifImageDescriptor v_imgDesc;
                        v_imgDesc = (STRUCTGifImageDescriptor)GifConstant.ReadStruct(stream, typeof(STRUCTGifImageDescriptor));
                        List<STRUCTGifColor> color = null;
                        if (v_imgDesc.HasColorTable)
                        {
                            color = new List<STRUCTGifColor>();
                            //read colors
                        }
                        byte v_lmzCode  =  v_binR.ReadByte();
                        //read data block
                        List<byte[]> v_blocks = new List<byte[]>();
                        byte vL  = 0;
                        while ((vL = v_binR.ReadByte()) != 0)
                        {
                            Byte[] data = new byte[vL];
                            v_binR.Read (data , 0, vL);
                            v_blocks.Add(data);
                        }
                        this.m_frames.Add ( new GifFrameEntity(v_imgDesc, v_lmzCode,(color==null)?null: color.ToArray() ,   v_blocks.ToArray()));
                        break;
                    case 0x21:
                        //extension introducer
                        byte t = (byte)stream.ReadByte();
                        switch (t)
                        { 
                            case 0xf9:
                                stream.Seek(-2, System.IO.SeekOrigin.Current);
                                STRUCTGifGraphicControlExtension ext = (STRUCTGifGraphicControlExtension )
                                    GifConstant.ReadStruct(stream, typeof(STRUCTGifGraphicControlExtension));
                                this.m_extensions.Add(ext);
                                break;
                             case 0xff: //application introducer
                                GifApplicationExtension sext = new GifApplicationExtension();
                                byte blockSize = v_binR.ReadByte();
                                byte[] v_tab = new byte[blockSize];
                                v_binR.Read(v_tab, 0, v_tab.Length);
                                byte cr = v_binR.ReadByte ();//Write((byte)0x03);
                                byte bc = v_binR.ReadByte (); //binW.Write((byte)0x01);
                                byte cloop = v_binR.ReadByte (); //binW.Write((short)LoopCount);
                                byte endl = v_binR.ReadByte();
                //terminator
                                //binW.Write((byte)0);
                                //new String(v_tab.To
                                //this.m_extensions.Add(sext);
                                this.m_appInfo = sext;
                                break ;
                        }
                        break;
                    case 0x3B:
                        m_endofStream =true;
                        break ;
                    default :
                        break;
                }
            }
            if (this.m_frames.Count > this.m_extensions.Count)
            {
                int c = this.m_frames.Count - this.m_extensions.Count;
                //complete
                for (int i = 0; i < c; i++)
                {
                    STRUCTGifGraphicControlExtension v_ext = STRUCTGifGraphicControlExtension.Default;
                    v_ext.introducer = 0x21;
                    v_ext.label = 0xf9;
                    v_ext .transparentcolorIndex = 0;
                    this.m_extensions .Add (v_ext);
                }
            }
        }
        public override void Write(Stream stream)
        {
            if (stream == null)return ;
            BinaryWriter binW = new BinaryWriter(stream);
            if (m_appInfo !=null)
                m_appInfo.SaveToStream(binW);
            foreach (var item in this.m_extensions)
            {
                    GifConstant.WriteStruct(stream, item);
            }
            foreach (GifFrameEntity item in this.m_frames)
            {
                GifConstant.WriteStruct(stream, item.imgDesc);
                //save colors
                if (item.color != null)
                { 
                    foreach (STRUCTGifColor cl in item.color )
                    {
                        GifConstant.WriteStruct (stream , cl);
                    }
                }
                 binW.Write((byte)item.lmzcode);
                 for (int i = 0; i < item.data.Length; i++)
                    {
                        binW.Write((byte)item.data[i].Length);
                        binW.Write(item.data[i], 0, item.data[i].Length);
                    }
                    binW.Write((byte)0);
            }
            binW.Write((byte)0x3B);
            binW.Flush();
        }
        internal GifFrameInfo GetFrameInfo(int index)
        {
            if ((index < 0) || (index >= this.m_frames .Count ))
                return null;
            GifFrameEntity e =  this.m_frames [index ];
            return GifFrameInfo.CreateInfo(e.imgDesc.width,
                e.imgDesc.height, e.data);
        }
        internal void LoadInfo(GifDataChainEntity entity, int p)
        {
            //bool found = false;
            //for (int i = 0; i < entity.m_extensions.Count; i++)
            //{
            //    IGifExtension ext = entity.m_extensions[i];
            //    if (ext is GifApplicationExtension) 
            //    {
            //        this.m_extensions.Add(entity.m_extensions[p+1]);
            //        found = true;
            //        break;
            //    }
            //}
            //if (!found)
            //{
            if ((p >= 0) && (p < entity.m_extensions.Count))
                this.m_extensions.Add(entity.m_extensions[p]);
            else { 
            }
            //}
            this.m_frames.Add(entity.m_frames[p]);
        }
        public override void Copy(ICoreDataEntity dataEntity)
        {
            GifDataChainEntity e = dataEntity as GifDataChainEntity ;
            if (e.m_appInfo != null)
            {
                this.m_appInfo = new GifApplicationExtension();
                this.m_appInfo.LoopCount = e.m_appInfo.LoopCount;
            }
            this.m_extensions.AddRange(e.m_extensions.ToArray());  
            this.m_frames.AddRange(e.m_frames.ToArray());
        }
        public int FrameCount { get { return this.m_frames.Count; } }
        /// <summary>
        /// set frame interval
        /// </summary>
        /// <param name="interval"></param>
        /// <param name="frameindex"></param>
        public void setInterval(int interval, int frameindex)
        {
            if ((frameindex >= 0) && (frameindex < this.m_extensions.Count))
            {
                STRUCTGifGraphicControlExtension g = (STRUCTGifGraphicControlExtension)this.m_extensions[frameindex];
                g.delayTime = (short)(interval / 10);
                this.m_extensions[frameindex] = g;
            }
        }
        /// <summary>
        /// set all interval
        /// </summary>
        /// <param name="interval"></param>
        public void setAllInterval(int interval)
        {
            for (int i = 0; i < this.FrameCount; i++)
            {
                this.setInterval(interval, i);
            }
        }
        internal void setAllSupportTransparentColor(bool p)
        {
            for (int i = 0; i < this.FrameCount && i < this.m_extensions.Count ; i++)
            {
                STRUCTGifGraphicControlExtension g = (STRUCTGifGraphicControlExtension)this.m_extensions[i];
                g.SupportTransparentColor = p;
                this.m_extensions[i] = g;
            }
        }
        internal void setAllTransparentColorIndex(int p)
        {
            for (int i = 0; i < this.FrameCount && i < this.m_extensions.Count; i++)
            {
                STRUCTGifGraphicControlExtension g = (STRUCTGifGraphicControlExtension)this.m_extensions[i];
                g.transparentcolorIndex = (byte)p;
                this.m_extensions[i] = g;
            }
        }
    }
}

