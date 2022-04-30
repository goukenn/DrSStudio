

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AVIFrameStream.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:AVIFrameStream.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Imaging ;
using System.Drawing.Drawing2D;
namespace IGK.AVIApi.AVI
{
    public abstract class AVIFrameStream : AVIStream , IAVIGetFrame 
    {
        protected IntPtr m_frameOpen;
        /// <summary>
        /// get the frame open handle
        /// </summary>
        internal IntPtr HFrameOpen { get{return m_frameOpen ;}}
        public Bitmap GetFrame(int pos)
        {
            if (m_frameOpen == IntPtr.Zero)
                return null;
            IntPtr h = AVIApi.AVIStreamGetFrame(m_frameOpen, pos);
            if (h == IntPtr.Zero) return null;
            BITMAPINFOHEADER v_bmpHeader = (BITMAPINFOHEADER)Marshal.PtrToStructure(h, typeof(BITMAPINFOHEADER));
            int offset = 0;
            int size = v_bmpHeader.biSizeImage;
            if (size == 0)
                return null;
            byte[] t = new byte[size];
            byte[] tinfo = new byte[40];
            IntPtr h2 = new IntPtr(h.ToInt32() + v_bmpHeader.biSize + offset);
            Marshal.Copy(h2, t, 0, t.Length);
            Marshal.Copy(h, tinfo, 0, tinfo.Length);
            System.IO.MemoryStream mem = new System.IO.MemoryStream();
            System.IO.BinaryWriter bWriter = new System.IO.BinaryWriter(mem);
            BitmapHeader bmph = new BitmapHeader();
            bmph.offset = 54;
            bmph.bmpSize = t.Length + 54;
            bmph.bmpHeader = BitmapHeader.BMHEADER;
            bWriter.Write(bmph.bmpHeader);
            bWriter.Write(bmph.reserved);
            bWriter.Write(size);
            bWriter.Write(bmph.offset);
            bWriter.Write(tinfo, 0, tinfo.Length);
            bWriter.Write(t, 0, t.Length);
            bWriter.Flush();
            mem.Seek(0, System.IO.SeekOrigin.Begin);
            Bitmap vbmp = new Bitmap(mem);
            bWriter.Close();
            mem.Close();
            return vbmp;
        }
        /// <summary>
        /// add frame to your video stream
        /// </summary>
        /// <param name="pos">position of the new frame</param>
        /// <param name="frameLength">Length of current frame</param>
        /// <param name="bmp">Bitmap to add</param>
        public virtual void AddFrame(int pos, int frameLength, Bitmap bmp)
        {
            Bitmap v_bmp = bmp.Clone() as Bitmap;
            AVIStreamInfoStruct  info = AVIStream.GetStreamInfo (this.Handle );
                if (v_bmp.PixelFormat != PixelFormat.Format32bppArgb)
                {
                    Bitmap tbmp = new Bitmap(v_bmp.Width, v_bmp.Height, PixelFormat.Format32bppArgb);
                    Graphics g = Graphics.FromImage(tbmp);
                    g.DrawImage(tbmp, Point.Empty);
                    g.Flush();
                    v_bmp.Dispose();
                    v_bmp = tbmp;
                }
            BitmapData bmpDat = v_bmp.LockBits(
                new Rectangle(
                0, 0, v_bmp.Width, v_bmp.Height),
                ImageLockMode.ReadOnly, v_bmp.PixelFormat);
            int size = bmpDat.Stride * bmpDat.Height;
            if (info.dwSuggestedBufferSize == 0)
                info.dwSuggestedBufferSize = size;
            int v_bytes = 0;
            int v_c = 0;
            long result = AVIApi.AVIStreamWrite(
                this.Handle,
                pos,
                frameLength,
                bmpDat.Scan0,
                size,
                enuAviWriteMode.KeyFrame,
                ref v_bytes,
                ref v_c);
            if (result != 0)
            {
                throw new Exception("Exception in VideoStreamWrite: " + ((enuAviError)result).ToString());
            }
            v_bmp.UnlockBits(bmpDat);
            v_bmp.Dispose();
        }
        /// <summary>
        /// Begin getting frame
        /// </summary>
        /// <returns></returns>
        /// <remarks>increment a reference count </remarks>
        public bool BeginGetFrame()
        {
            m_frameOpen = AVIApi.AVIStreamGetFrameOpen(this.Handle , (IntPtr)AVIApi.AVIGETFRAMEF_BESTDISPLAYFMT);            
            return (m_frameOpen != IntPtr.Zero);
        }
        /// <summary>
        /// end get frame
        /// </summary>
        public void EndGetFrame()
        {
            if (m_frameOpen != IntPtr.Zero)
            {
                IntPtr h = AVIApi.AVIStreamGetFrameClose(m_frameOpen);
                m_frameOpen = IntPtr.Zero;
            }
        }
    }
}

