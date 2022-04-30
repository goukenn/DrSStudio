

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WebCssCmpletionData.cs
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
﻿using ICSharpCode.AvalonEdit.CodeCompletion;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Editing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.WebCssEditorAddIn
{
    class WebCssCmpletionData : ICompletionData
    {
        
        private WebCssAttributeDefinition m_attribute;
        private string m_Data;
        internal  WebCssCmpletionData(string data, WebCssAttributeDefinition property)
        {
            
            this.m_Data = data;
            this.m_attribute = property;
        }
        /// <summary>
        /// get or set the image source
        /// </summary>
        public virtual System.Windows.Media.ImageSource Image
        {
            get
            {
                //sample to get image for main image this operation must be done one
                return IGK.DrSStudio.Wpf.WpfResources.GetImage("css_properties");
            }

        }
        /// <summary>
        /// get the text context
        /// </summary>
        public virtual string Text
        {
            get { return this.m_Data; }
        }
        /// <summary>
        /// get the data content
        /// </summary>
        public virtual object Content
        {
            get { return this.Text; }
        }

        public virtual object Description
        {
            get {

                var t = this.m_attribute.GetValues();
                string j =null;
                if (t!=null)
                    j = "-"+string.Join("\n-", t);
                return string.Format("{0}\nType: {1}\n{2}",
                    this.m_attribute.Description,
                    this.m_attribute.PropertyType ,
                    string.IsNullOrEmpty (j)? string.Empty: "\nValues:\n"+j);
            }
        }

        public virtual double Priority
        {
            get { return 0.0; }
        }

        public virtual void Complete(ICSharpCode.AvalonEdit.Editing.TextArea textArea, ICSharpCode.AvalonEdit.Document.ISegment completionSegment, EventArgs insertionRequestEventArgs)
        {
            textArea.Document.Replace(completionSegment, this.Text);
        }

    }
}
