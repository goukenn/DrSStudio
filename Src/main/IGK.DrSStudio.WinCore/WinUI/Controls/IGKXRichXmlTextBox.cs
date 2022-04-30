

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKXRichXmlTextBox.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGK.DrSStudio.WinUI
{
    public class IGKXRichXmlTextBox : RichTextBox
    {
        private string m_XmlText;

        public string XmlText
        {
            get { return m_XmlText; }
            set
            {
                if (m_XmlText != value)
                {
                    m_XmlText = value;
                    this.LoadRtfTextString();
                }
            }
        }
        public IGKXRichXmlTextBox():base()
        {

        }

      
        protected override void OnTextChanged(EventArgs e)
        {
            this.LoadXmlString();
            base.OnTextChanged(e);
        }

        /// <summary>
        /// load rtf text string
        /// </summary>
        private void LoadRtfTextString()
        {

        }
        /// <summary>
        /// load xml string
        /// </summary>
        void LoadXmlString()
        { 

        }
    }
}
