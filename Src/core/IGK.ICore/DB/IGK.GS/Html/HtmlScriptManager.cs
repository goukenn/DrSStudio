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
    /// represet the document script manager
    /// </summary>
    public class HtmlScriptManager
    {
        private GSDocument m_document;
        List<HtmlScript> m_scripts;
        public HtmlScriptManager(GSDocument document)
        {
            this.m_document = document;
            this.m_scripts = new List<HtmlScript>();
        }
        public bool Contains(string filename)
        {
            return false;
        }
    }
}