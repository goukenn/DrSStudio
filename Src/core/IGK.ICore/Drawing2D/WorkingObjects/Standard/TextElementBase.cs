

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: TextElementBase.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore;
using IGK.ICore.Codec;
using IGK.ICore.WinUI;
using IGK.ICore.WinUI.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.Drawing2D
{
    /// <summary>
    /// represent a text element base
    /// </summary>
    public abstract class TextElementBase : RectangleElement, ICoreTextElement
    {
        protected const string LINE_SEPARATOR = "\n";

        private string m_Text;
        private CoreFont m_Font;


        [Browsable(false)]
        /// <summary>
        /// get the lines  
        /// </summary>
        public string[] Lines
        {
            get
            {
                if (string.IsNullOrEmpty(this.m_Text))
                    return new string[0];
                return this.m_Text.Split(new String[] { LINE_SEPARATOR }, StringSplitOptions.None);
            }
        }
        [CoreXMLAttribute()]
        /// <summary>
        /// get or set the font
        /// </summary>
        public CoreFont Font
        {
            get { return m_Font; }
        }
        [CoreXMLElement()]
        public virtual string Text
        {
            get { return m_Text; }
            set
            {
                if (m_Text != value)
                {
                    m_Text = value;
                    OnPropertyChanged(Core2DDrawingChangement.Definition);
                }
            }
        }

        ICoreFont ICoreTextElement.Font
        {
            get { return this.Font; }
        }

        public enuStringAlignment HorizontalAlignment { get { return this.m_Font.HorizontalAlignment; } set { this.m_Font.HorizontalAlignment = value; } }

        public enuStringAlignment VerticalAlignment { get { return this.m_Font.VerticalAlignment; } set { this.m_Font.VerticalAlignment = value; } }

        public TextElementBase():base()
        {
        }
        protected override void InitializeElement()
        {
            base.InitializeElement();
            this.m_Text = "Text";
            this.m_Font = CoreFont.CreateFrom("FontName:consolas;Size:12pt;Style:Regular", this);
            this.m_Font.HorizontalAlignment = enuStringAlignment.Near;
            this.m_Font.VerticalAlignment = enuStringAlignment.Near;
            this.m_Font.WordWrap = true;
            this.m_Font.FontDefinitionChanged += _FontDefinitionChanged;
        }

        private void _FontDefinitionChanged(object sender, EventArgs e)
        {
            if (!this.IsLoading)
            {
                this.InitElement();
            }
            OnPropertyChanged(Core2DDrawingChangement.FontChanged);
        }

      
        public override ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
        {
            parameters = base.GetParameters(parameters);
            var g = parameters.AddGroup("text");
            g.AddItem("Text", "lb.Text", enuParameterType.MultiTextLine, (o, e) =>
            {
                this.Text = ((string)e.Value) ?? string.Empty;
            });
            return parameters;
        }
    }
}
