

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreParameterTrackbar.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
using IGK.ICore;using IGK.ICore.WinUI;
using IGK.ICore.WinUI.Configuration;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:CoreParameterTrackbar.cs
*/
/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igkdev.be
Application : DrSStudio
powered by IGK - DEV &copy; 2008-2012
THIS FILE IS A PART OF THE DRSSTUDIO APPLICATION. SEE "License.txt"
FOR MORE INFORMATION ABOUT THE LICENSE
------------------------------------------------------------------- 
*/
﻿using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore.WinCore.Configuration
{
    /// <summary>
    /// represent the base Parameter track item
    /// </summary>
    public class CoreParameterTrackbar : 
        CoreParameterGroupItem,
        ICoreParameterTrackbarItem
    {
        private int m_min;
        private int m_max;
        private bool m_ShowCaption;
        private bool m_ShowValue;
        public bool ShowValue
        {
            get { return m_ShowValue; }
            set
            {
                if (m_ShowValue != value)
                {
                    m_ShowValue = value;
                }
            }
        }
        public bool ShowCaption
        {
            get { return m_ShowCaption; }
            set
            {
                if (m_ShowCaption != value)
                {
                    m_ShowCaption = value;
                }
            }
        }
        public override enuParameterType ParamType
        {
            get
            {
                return enuParameterType.Interval;
            }
            protected set
            {
                base.ParamType = value;
            }
        }
        public int max
        {
            get { return m_max; }
            set
            {
                if (m_max != value)
                {
                    m_max = value;
                }
            }
        }
        public int min
        {
            get { return m_min; }
            set
            {
                if (m_min != value)
                {
                    m_min = value;
                }
            }
        }
        public new int DefaultValue {
            get {
                return Convert.ToInt32(base.DefaultValue);
            }
            protected set {
                base.DefaultValue = value;
            }
        }
        public CoreParameterTrackbar(string name, 
            string captionkey, 
            ICoreParameterGroup group, 
            int defaultValue,
            CoreParameterChangedEventHandler PROC)
            : base(
            name, 
            captionkey,
            group
            )
        {
            this.DefaultValue = defaultValue;
            this.Event = PROC ;
            this.m_ShowCaption = true;
            this.m_ShowValue = true;
        }
    }
}

