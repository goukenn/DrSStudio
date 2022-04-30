

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreScreenSetting.cs
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
file:CoreScreenSetting.cs
*/
/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igkdev.be
CoreApplicationManager.Instance : ICore
powered by IGK - DEV &copy; 2008-2012
THIS FILE IS A PART OF THE DRSSTUDIO APPLICATION. SEE "License.txt"
FOR MORE INFORMATION ABOUT THE LICENSE
------------------------------------------------------------------- 
*/
﻿using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore.WinUI
{
    /// <summary>
    /// represent a core screen display setting
    /// </summary>
    public class CoreScreenSetting 
    {
        private int m_Width;
        private int m_Height;
        private byte m_BitperPixel;
        private int m_frequency;
        internal  CoreScreen.LPDEVMODE m_devmode;
        public int Frequency
        {
            get { return m_frequency; }           
        }
        public byte BitperPixel
        {
            get { return m_BitperPixel; }
        }
        public int Height
        {
            get { return m_Height; }
        }
        public int Width
        {
            get { return m_Width; }
        }
        internal  CoreScreenSetting(CoreScreen.LPDEVMODE device)
        {
            this.m_BitperPixel = (byte)device.dmBitsPerPel;
            this.m_Height = device.dmPelsHeight;
            this.m_Width = device.dmPelsWidth;
            this.m_frequency = device.dmDisplayFrequency;
            this.m_devmode = device;
        }
        public override string ToString()
        {
            return string.Format("{0}x{1}, {2}Bpp, {3}Hz",
                this.Width, this.Height, this.BitperPixel, this.Frequency);
        }
        public enuCoreScreenResponse ChangeDisplay()
        {
            return ChangeDisplay(enuCoreScreenChangeRequest.Dynamic| enuCoreScreenChangeRequest.UpdateRegistry );
        }
        public enuCoreScreenResponse  ChangeDisplay(enuCoreScreenChangeRequest request)
        {
            return CoreScreen.ChangeDisplay(this, request);
        }
    }
}

