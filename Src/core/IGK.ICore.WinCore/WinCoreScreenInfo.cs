

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WinCoreScreenInfo.cs
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
file:WinCoreScreenInfo.cs
*/
using IGK.ICore.WinCore;
using IGK.ICore;

using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.ICore.WinCore
{
    /// <summary>
    /// represent a wincore screen info
    /// </summary>
    public class WinCoreScreenInfo : ICoreScreenInfo
    {
        private static WinCoreScreenInfo sm_instance;
        private float m_dpiX;
        private float m_dpiY;
        private byte m_bitCount;
        private int m_numberOfScreen;
        private int m_Width;
        private int m_Height;
        private WinCoreScreenInfo()
        {
        }
        public static WinCoreScreenInfo Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static WinCoreScreenInfo()
        {
            sm_instance = new WinCoreScreenInfo();
            global::System.Drawing.Graphics g = global::System.Drawing.Graphics.FromHwnd(IntPtr.Zero);
            sm_instance.m_dpiX  = g.DpiX;
            sm_instance.m_dpiY = g.DpiY;
            sm_instance.m_bitCount =(byte) System.Windows.Forms.Screen.PrimaryScreen.BitsPerPixel;
            sm_instance.m_numberOfScreen = System.Windows.Forms.Screen.AllScreens.Length;
            sm_instance.m_Width = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width;
            sm_instance.m_Height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height ;
            g.Dispose();
        }
        public float DpiX
        {
            get { return m_dpiX; }
        }
        public float DpiY
        {
            get { return m_dpiY; }
        }
        public byte BitCount
        {
            get { return m_bitCount; }
        }
        public int NumberOrScreen
        {
            get { return m_numberOfScreen; }
        }
        public int Width
        {
            get { return m_Width; }
        }
        public int Height
        {
            get { return m_Height; }
        }
    }
}

