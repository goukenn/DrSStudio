

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WiXNewConditionControl.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
using IGK.ICore;
using IGK.ICore.Codec;
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
file:WiXNewConditionControl.cs
*/
using IGK.ICore.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IGK.ICore.WinCore.WinUI.Controls;
namespace IGK.DrSStudio.WiXAddIn.WinUI
{
    [ComVisible(true)]
    /// <summary>
    /// represent a wix new condition control
    /// </summary>
    public class WiXNewConditionControl : IGKXUserControl 
    {
        private WebBrowser c_Browser;
        private WiXCondition m_Condition;
        public WiXCondition Condition
        {
            get { return m_Condition; }
            set
            {
                if (m_Condition != value)
                {
                    m_Condition = value;
                }
            }
        }
        /// <summary>
        /// represent a wix condition
        /// </summary>
        public WiXNewConditionControl()
        {
            this.InitializeComponent();
        }
        private void InitializeComponent()
        {
            c_Browser = new WebBrowser();   
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            c_Browser.DocumentText = this.GetBrowserString();
            c_Browser.AllowNavigation = false;
            c_Browser.AllowWebBrowserDrop = false;
            c_Browser.ScriptErrorsSuppressed = true;
            c_Browser.ObjectForScripting = this;
        }
        private string GetBrowserString()
        {
           CoreXmlWriter c = IGK.ICore.Xml.CoreXmlWriter.CreateWriter("HTML", new StringBuilder(), 
                new System.Xml.XmlWriterSettings() { 
                Indent = true 
            });
            if (c != null)
            {
                c.WriteStartElement("html");
                c.WriteStartElement("head");
                c.WriteStartElement("body");
                    c.WriteStartElement("form");
                    c.WriteAttributeString("method", "POST");
                    c.WriteAttributeString("action", "__self");
                c.WriteElementString ("label", "lb.Property".R());
                c.WriteElementString("label","lb.Conditions".R());
                c.WriteStartElement("input");
                    c.WriteAttributeString("type", "text");
                    c.WriteAttributeString("id", "clCondition");
                c.WriteEndElement();
                c.WriteElementString("label", "lb.Message".R());
                c.WriteStartElement("input");
                    c.WriteAttributeString("type", "text");
                    c.WriteAttributeString("id", "clMessage");
                c.WriteEndElement();
                c.WriteEndElement();
                c.WriteEndElement();
                c.WriteEndElement();
                c.Flush();
                return c.StringBuilder.ToString();
            }
            return null;
        }
    }
}

