

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AndroidCodeCompletionBase.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using ICSharpCode.AvalonEdit.CodeCompletion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Android.CodeCompletion
{

    
using IGK.ICore;


    public class AndroidCodeCompletionBase : ICompletionData
    {
        
        public AndroidCodeCompletionBase(string data)
        {
            this.m_Data = data;
        }
        private string m_Data;

        public string Data
        {
            get { return m_Data; }
            set
            {
                if (m_Data != value)
                {
                    m_Data = value;
                }
            }
        }
        /// <summary>
        /// get or set the image source
        /// </summary>
        public virtual System.Windows.Media.ImageSource Image
        {
            get {
                //sample to get image for main image this operation must be done one
                return AndroidImageResources.GetImage(AndroidConstant.ANDROID_IMG_RESFOLDER);
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
        public virtual  object Content
        {
            get { return this.Text; }
        }

        public virtual object Description
        {
            get { return "msg.nodescription".R(); }
        }

        public virtual double Priority
        {
            get { return 0.0; }
        }

        public virtual  void Complete(ICSharpCode.AvalonEdit.Editing.TextArea textArea, ICSharpCode.AvalonEdit.Document.ISegment completionSegment, EventArgs insertionRequestEventArgs)        
        {
            textArea.Document.Replace(completionSegment, this.Text); 
        }

    }
}
