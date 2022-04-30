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
    /// <summary>
    /// represent a item attribute
    /// </summary>
    public sealed class HtmlItemAttribute
    {
        private string m_Name;
        private object m_Value;

        public HtmlItemAttribute(string name, object value)
        {
            this.m_Name = name;
            this.m_Value = value;
        }

        public object  Value
        {
            get { return m_Value; }
            set
            {
                if (m_Value != value)
                {
                    m_Value = value;
                }
            }
        }
        public string Name
        {
            get { return m_Name; }
        }

        public override string ToString()
        {
            return string.Format("{0}:{1}", this.Name, this.Value);
        }
    }
}
