using IGK.ICore;
using IGK.ICore.Codec;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.ICore.WinUI.Configuration;
using IGK.ICore.Settings;






﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.GS.Html
{
    [AttributeUsage(AttributeTargets.Class , AllowMultiple =false , Inherited =false )]
    public class HtmlControllerAttribute : Attribute 
    {
        private bool m_IsController;
        /// <summary>
        /// get if this 
        /// </summary>
        public bool IsController
        {
            get { return m_IsController; }
            set
            {
                if (m_IsController != value)
                {
                    m_IsController = value;
                }
            }
        }
        public HtmlControllerAttribute()
        {
            this.m_IsController = true;
        }
    }
}
