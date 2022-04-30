

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: HtmlStringSegment.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore;using IGK.ICore.GraphicModels;
using IGK.ICore.Xml;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace IGK.ICore.Drawing2D
{
    public class HtmlStringSegment
    {
        //HtmlStringVirtualLineSegment VirtualLine { get; set; }
        private string m_Text;
        private float m_Width;
 
        private ICoreFont m_Font =null;
        private ICoreBrush m_Brush;
        public float VirtualLineHeight;
        private HtmlStringSegmentCollection m_Childs;
        public HtmlStringSegment m_parent;
        private HtmlStringElement m_owner;
        private enuFontStyle m_style;
        private string m_tagSegment;

        public override string ToString()
        {
            return "[Text: " + this.Text + "]";
        }
        public HtmlStringSegmentCollection Childs
        {
            get { return m_Childs; }
        }

        public class HtmlStringSegmentCollection : IEnumerable
        {
            private List<HtmlStringSegment> m_childs;
            private HtmlStringSegment m_owner;

            public HtmlStringSegmentCollection(HtmlStringSegment owner)
            {
                this.m_owner = owner;
                this.m_childs = new List<HtmlStringSegment>();
            }
            public int Count { get { return this.m_childs.Count; } }

            public IEnumerator GetEnumerator()
            {
                return this.m_childs.GetEnumerator();
            }

            internal void Add(HtmlStringSegment h)
            {
                this.m_childs.Add(h);
                h.m_parent = this.m_owner;
            }
            internal void Remove(HtmlStringSegment h)
            {
                this.m_childs.Remove(h);
                h.m_parent = null;
            }
        }


        public class HtmlStringBreakLineSegment : HtmlStringSegment
        {
            protected internal override void Render(ICore2DDrawingVisitor visitor, 
                HtmlStringElementRenderingEventArgs e)
            {
                e.X = e.Bounds.X;
                e.Y += e.VirtualLineHeight == 0 ? e.DefaultLineHeight :  e.VirtualLineHeight;
                //reset to default font height;
                e.VirtualLineHeight = 0;
            }
        }
        sealed class HtmlStringSpaceSegment : HtmlStringSegment
        {
            protected internal override void MeasureSegment()
            {                
                Size2f size = CoreApplicationManager.Application.ResourcesManager.MeasureString("_",
                    this.Font);
                this.Width = size.Width;

            }
            protected internal override void Render(ICore2DDrawingVisitor visitor, HtmlStringElementRenderingEventArgs e)
            {
                e.X += this.Width;
            }
        }

        sealed class HtmlStringTabSegment : HtmlStringSegment
        {
            protected internal override void MeasureSegment()
            {
                Size2f size = CoreApplicationManager.Application.ResourcesManager.MeasureString("_",
                    this.Font);
                this.Width = size.Width * 4;
            }
            protected internal override void Render(ICore2DDrawingVisitor visitor, HtmlStringElementRenderingEventArgs e)
            {
                visitor.DrawString(this.Text,
                    this.Font,
                    this.Brush,
                    e.X , e.Y );
                if (this.Width == 0)
                    MeasureSegment();
                e.X += this.Width;
            }
        }
        static HtmlStringSegment() {
            
        }
        internal HtmlStringSegment()
        {
            m_Childs = new HtmlStringSegmentCollection(this);
        }
        private float m_Height;

        public float Height
        {
            get { return m_Height; }
            private set
            {
                if (m_Height != value)
                {
                    m_Height = value;
                }
            }
        }
        /// <summary>
        /// get the width of this segment.
        /// </summary>
        public float Width
        {
            get { return m_Width; }
            private set
            {
                if (m_Width != value)
                {
                    m_Width = value;
                }
            }
        }
        protected internal virtual void MeasureSegment()
        {
            ICoreFont ft = GetFont();
            if ((ft == null) || string.IsNullOrEmpty(this.m_Text))
            {
                this.Width = 0;
                this.Height = 0;
                return;
            }
            //save value
            enuFontStyle fts = ft.FontStyle;
            float ftsize = ft.FontSize;
            ft.FontStyle = this.m_style;
            ICoreBrush br = this.Brush ?? this.m_owner.FillBrush;
            Size2f size = CoreApplicationManager.Application.ResourcesManager.MeasureString(this.Text,
                ft);
            //restore value
            ft.FontStyle = fts;
            ft.FontSize = ftsize;
            this.Width = size.Width;
            this.Height = size.Height;
        }
        /// <summary>
        /// create a font from style properties
        /// </summary>
        /// <returns></returns>
        private ICoreFont GetFont()
        {
            return CoreFont.CreateFrom((this.Font ?? this.m_owner.Font).GetDefinition(), null);
        }

        public string Text
        {
            get { return m_Text; }
            set
            {
                if (m_Text != value)
                {
                    m_Text = value;
                }
            }
        }
        public virtual ICoreFont Font { get { return this.m_Font; } }
        public virtual ICoreBrush Brush { get { return this.m_Brush; } set { this.m_Brush =value;  } }


        public void ResetStyle()
        {
            if (this.m_Brush != null)
            {
                this.m_Brush.Dispose();
                this.m_Brush = null;
            }
            if (this.m_Font != null)
            {
                this.m_Font.Dispose();
                this.m_Font = null;
            }
        }
        protected internal virtual void Render(ICore2DDrawingVisitor visitor,HtmlStringElementRenderingEventArgs e)
        {
            ICoreFont ft = CoreFont.CreateFrom ((this.Font ?? this.m_owner.Font).GetDefinition (),null);    
            ICoreBrush br = this.Brush ?? this.m_owner.FillBrush;
            if (!string.IsNullOrEmpty(this.m_Text) && (ft != null) && (br != null))
            {

                enuFontStyle fts = ft.FontStyle;
                float ftsize = ft.FontSize;

                ft.FontStyle = this.m_style;
                visitor.TextRenderingMode = enuTextRenderingMode.GridFitAntiAliazed;
                visitor.DrawString(this.Text,
                    ft,
                    br,
                    new Rectanglef(e.X, e.Y, 
                        e.Bounds.Width , e.Bounds.Height));

                ft.FontStyle = fts;
                ft.FontSize =ftsize;
                if (this.Width == 0) {
                    this.MeasureSegment();
                }
                //render on debug mode property
                //visitor.DrawRectangle(Colorf.Black,
                //e.X, e.Y,
                //this.Width,
                //this.Height);
                e.X += this.Width ;
                e.VirtualLineHeight = Math.Max(e.VirtualLineHeight, this.Height);
            }
            ft.Dispose();
            foreach (HtmlStringSegment item in this.Childs)
            {
                item.Render(visitor,e);
               // e.X += item.Width;
            }  
        }

        internal static HtmlStringSegment CreateSegment(HtmlStringElement owner, string tagSegment)
        {
            HtmlStringSegment v_sg = null;
            switch (tagSegment)
            {
                case "br":
                    v_sg = new HtmlStringBreakLineSegment();
                    break;
                default:
                    v_sg = new HtmlStringSegment();             
                    break;
            }
            v_sg.m_owner = owner;
            v_sg.m_tagSegment = tagSegment;
            v_sg.m_style = GetStyle(tagSegment);
            v_sg.MeasureSegment();
            v_sg.Register();
            return v_sg;
        }
        private void Register()
        {
            this.m_owner.MeasureRequired +=Font_FontDefinitionChanged;
        }
        internal void Release()
        {
            this.m_owner.MeasureRequired -= Font_FontDefinitionChanged;
            this.m_owner = null;
        }

        void Font_FontDefinitionChanged(object sender, EventArgs e)
        {
            this.MeasureSegment();
        }



        private static enuFontStyle GetStyle(string tagSegment)
        {
            switch (tagSegment)
            {
                case "u":
                    return enuFontStyle.Underline;                    
                case "i":
                    return enuFontStyle.Italic;
                    
                case "b":
                    return enuFontStyle.Bold;
                case "s":
                    return enuFontStyle.Strikeout;                    
                default:
                    return enuFontStyle.Regular;
                    
            }
        }
        private void Load(CoreXmlElement c)
        {
            if (c == null)
                return;
       
            if (c.HasAttributes)
            {
                var r = c.Attributes["font"];
                if (r != null)
                {
                    if (this.Font != null)
                        this.Font.CopyDefinition(r);
                    else
                    {
                        this.m_Font = CoreFont.CreateFrom(r, this.m_owner);
                    }
                }
                r = c.Attributes["brush"];
                if (!string.IsNullOrEmpty (r))
                {
                    if ((this.m_Brush == null)||(this.m_Brush.IsDisposed ))
                    {
                        this.m_Brush = new CoreBrush(this.m_owner);
                    }                    
                    this.m_Brush.CopyDefinition(r);
                }
            }
            this.m_Text = CoreXmlUtility.GetStringValue( c.Content);
            if (c.HasChild)
            {

                foreach (CoreXmlElementBase item in c.Childs)
                {
                    if (item == null)
                        continue;
                    CoreXmlElement r = item as CoreXmlElement;
                    if ((r != null) && !string.IsNullOrEmpty(r.TagName))
                    {
                        HtmlStringSegment h = HtmlStringSegment.CreateSegment(this.m_owner, r.TagName);
                        h.Load(r);
                        this.m_Childs.Add(h);
                        continue;
                    }
                    if (item is IGK.ICore.Xml.CoreXmlElementText)
                    {
                        IGK.ICore.Xml.CoreXmlElementText text = item as IGK.ICore.Xml.CoreXmlElementText;
                        HtmlStringSegment h = HtmlStringSegment.CreateSegment(this.m_owner, this.m_tagSegment);
                        h.m_Text = text.Text;
                        this.m_Childs.Add(h);
                        continue;
                    }
                }
            }
        }
        internal void Load(CoreXmlElementBase c)
        {
            if (c==null)
                return ;
            if (c is CoreXmlElement)
            {
                this.Load(c as CoreXmlElement);
            }
            else if (c is CoreXmlElementText )
            {
                IGK.ICore.Xml.CoreXmlElementText text = c as IGK.ICore.Xml.CoreXmlElementText;
                this.m_Text = text.Text;
            }
        }

     
    }
}
