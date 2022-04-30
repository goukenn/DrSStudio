

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AudioVideoAnimationContext.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/

using IGK.ICore;
using IGK.ICore.Codec;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;





﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.AudioVideo
{
    /// <summary>
    /// represent a audio video animation context
    /// </summary>
    public class AudioVideoAnimationContext : CoreExtensionContextBase,  ICoreExtensionContext 
    {
        private TimeSpan m_Duration;
        private float m_FramePerSecond;

        public float FramePerSecond
        {
            get { return m_FramePerSecond; }
            set
            {
                if (m_FramePerSecond != value)
                {
                    m_FramePerSecond = value;
                }
            }
        }
        public TimeSpan Duration
        {
            get { return m_Duration; }
            set
            {
                if (m_Duration != value)
                {
                    m_Duration = value;
                }
            }
        }
        public static AudioVideoAnimationContext CreateNewContext(TimeSpan timeSpan, float framepersec)
        {
            AudioVideoAnimationContext c = new AudioVideoAnimationContext();
            c.Duration = timeSpan;
            c.FramePerSecond = framepersec;
            return c;
        }
    }
}
