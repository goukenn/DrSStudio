

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: HtmlStyleProperty.cs
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
using IGK.DrSStudio;
using IGK.DrSStudio.Menu;
using IGK.DrSStudio.WinUI;
using IGK.DrSStudio.Drawing2D;
using IGK.DrSStudio.Drawing2D.WinUI;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:HtmlStyleProperty.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.WebProjectAddIn
{
    using IGK.DrSStudio.WebProjectAddIn.WorkingObjects;
    using IGK.ICore.Xml ;
    /// <summary>
    /// represent a style property
    /// </summary>
    public class HtmlStyleProperty
    {
        private WebMargin m_Margin;
        private WebMargin m_Padding;
        //get or set the web padding
        public WebMargin Padding
        {
            get { return m_Padding; }
            set
            {
                if (!m_Padding.Equals(value))
                {
                    m_Padding = value;
                }
            }
        }
        /// <summary>
        /// get or set the web margin
        /// </summary>
        public WebMargin Margin
        {
            get { return m_Margin; }
            set
            {
                if (!m_Margin.Equals(value))
                {
                    m_Margin = value;
                }
            }
        }
        public class HtmlStyleAttributeValue : CoreXmlAttributeValue
        {
            HtmlStyleProperty m_owner;
            internal HtmlStyleAttributeValue(HtmlStyleProperty owner)
            {
                this.m_owner = owner;
            }
            public override string GetValue()
            {
                return this.m_owner.GetCssDefinition();// "background-color:black; width:200px; height:200px; border:1px solid red;";
            }
        }
        private HtmlStyleAttributeValue  m_Attribute;
        private WebHtmlElementBase m_element;
        public HtmlStyleAttributeValue  Attribute
        {
            get { return m_Attribute; }
        }
        public WebHtmlElementBase Element {
            get { return this.m_element; }
        }
        private ICoreBrush m_FillBrush;
        private CoreHtmlPen m_BorderLeft;
        private CoreHtmlPen m_StrokeBrush;
        private CoreHtmlPen m_BorderRight;
        private CoreHtmlPen m_BorderBottom;
        private CoreHtmlPen m_BorderTop;
        public CoreHtmlPen BorderLeft
        {
            get { return m_BorderLeft; }
        }
        public ICoreBrush FillBrush
        {
            get { return m_FillBrush; }
        }
        public ICoreBrush StrokeBrush
        {
            get { return m_StrokeBrush; }
        }
        public CoreHtmlPen BorderRight
        {
            get { return m_BorderRight; }
        }
        public CoreHtmlPen BorderTop
        {
            get { return m_BorderTop; }
        }
        public CoreHtmlPen BorderBottom
        {
            get { return m_BorderBottom; }
        }
        public HtmlStyleProperty(WebHtmlElementBase element)
        {
            if (element == null)
                throw new CoreException(enuExceptionType.ArgumentIsNull,"element");
            this.m_Attribute = new HtmlStyleAttributeValue(this);
            this.m_element = element;
            this.m_Margin = new WebMargin();
            this.m_Padding = new WebMargin();
            m_BorderLeft = new CoreHtmlPen(element);
            m_BorderBottom = new CoreHtmlPen(element);
            m_BorderTop= new CoreHtmlPen(element);
            m_BorderRight = new CoreHtmlPen(element);
            m_StrokeBrush = new CoreHtmlPen(element);
            m_FillBrush = new CoreHtmlBrush(element);
            m_StrokeBrush.BrushDefinitionChanged += new EventHandler(m_Stroke_BrushDefinitionChanged);
            m_BorderLeft.BrushDefinitionChanged += new EventHandler(m_BrushDefinitionChanged);
            m_BorderRight.BrushDefinitionChanged += new EventHandler(m_BrushDefinitionChanged);
            m_BorderTop.BrushDefinitionChanged += new EventHandler(m_BrushDefinitionChanged);
            m_BorderBottom.BrushDefinitionChanged += new EventHandler(m_BrushDefinitionChanged);
            m_FillBrush.BrushDefinitionChanged += new EventHandler(m_BrushDefinitionChanged);
        }
        public HtmlStyleProperty()
        {
            this.m_Margin = new WebMargin();
            this.m_Padding = new WebMargin();
        }
        void m_BrushDefinitionChanged(object sender, EventArgs e)
        {
            OnStyleChanged(e);
        }
        void m_Stroke_BrushDefinitionChanged(object sender, EventArgs e)
        {
            //-------------------------------------------------------------------------
            //update all the other brush
            //-------------------------------------------------------------------------
            this.m_BorderBottom.Copy(this.m_StrokeBrush);
            this.m_BorderTop.Copy(this.m_StrokeBrush);
            this.m_BorderRight.Copy(this.m_StrokeBrush);
            this.m_BorderLeft.Copy(this.m_StrokeBrush);
            OnStyleChanged(EventArgs.Empty);
        }
        public event EventHandler StyleChanged;
        /// <summary>
        /// raise the style changed event
        /// </summary>
        /// <param name="e"></param>
        private void OnStyleChanged(EventArgs e)
        {
            if (StyleChanged != null)
            {
                StyleChanged(this, e);
            }
        }
        public string GetCssDefinition()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("display:block;"));
            sb.Append(string.Format("position:relative;"));
            WebRectangle v_rc = this.m_element.Bound;
            //
            sb.Append(string.Format("left: {0};", v_rc.X.ToString (enuWebUnitType.px )));
            sb.Append(string.Format("top: {0};", v_rc.X.ToString(enuWebUnitType.px)));
            sb.Append(string.Format("width: {0};", v_rc.X.ToString(enuWebUnitType.px)));
            sb.Append(string.Format("height: {0};", v_rc.X.ToString(enuWebUnitType.px)));
            sb.Append (string.Format ("background-color:{0};", this.FillBrush .Colors[0].ToString (true)));
            sb.Append(string.Format("border: {0} {1} {2};",((CoreUnit )(this.StrokeBrush as ICorePen ).Width ).ToString (enuUnitType.px ), this.StrokeBrush.Colors[0].ToString(true),"solid"));
            return sb.ToString();
        }
    }
}

