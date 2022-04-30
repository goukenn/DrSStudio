

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CVDocument.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:CVDocument.cs
*/
/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igkdev.be
Application : DrSStudio
powered by IGK - DEV &copy; 2008-2012
THIS FILE IS A PART OF THE DRSSTUDIO APPLICATION. SEE "License.txt"
FOR MORE INFORMATION ABOUT THE LICENSE
------------------------------------------------------------------- 
*/
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D ;
namespace IGK.DrSStudio
{
    
using IGK.ICore; using IGK.ICore.Drawing2D; using IGK.DrSStudio.Drawing2D;
    using IGK.ICore.WinUI;
    using IGK.ICore.Codec;
    using IGK ;
    using IGK.ICore.Drawing2D.WinUI;
    [Core2DDrawingDocumentAttribute("CVDocument")]
    public class CVDocument : Core2DDrawingLayerDocument  
    {
        private enuCVModel m_CVModel;
        [CoreXMLAttribute ()]
        [CoreXMLDefaultAttributeValue (enuCVModel .Horizontal )]
        public enuCVModel CVModel
        {
            get { return m_CVModel; }
            set
            {
                if (m_CVModel != value)
                {
                    m_CVModel = value;
                    OnCVModelChanged(EventArgs.Empty);
                }
            }
        }
        public event EventHandler CVModelChanged;
        private void OnCVModelChanged(EventArgs eventArgs)
        {
            if (this.CVModelChanged != null)
                this.CVModelChanged(this, eventArgs);
        }
        public CVDocument():base("87 mm","56 mm")
        {
            this.m_CVModel = enuCVModel.Horizontal;
            this.CVModelChanged += new EventHandler(CVDocument_CVModelChanged);
        }
        void CVDocument_CVModelChanged(object sender, EventArgs e)
        {
            switch (this.CVModel)
            {
                case enuCVModel.Vertical:
                    this.SetPrimarySize("56 mm", "87 mm");
                    break;
                case enuCVModel.Horizontal:
                    this.SetPrimarySize("87 mm", "56 mm");
                    break;
                default:
                    break;
            }
            OnPropertyChanged(new Core2DDrawingElementPropertyChangeEventArgs(enu2DPropertyChangedType.DefinitionChanged));
        }
        public override bool CanResize
        {
            get
            {
                return false;
            }
        }
    }
}

