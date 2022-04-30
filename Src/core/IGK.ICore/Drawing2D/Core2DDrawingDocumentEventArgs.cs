

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Core2DDrawingDocumentEventArgs.cs
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
file:Core2DDrawingDocumentEventArgs.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore.Drawing2D
{
    public delegate void Core2DDrawingDocumentEventHandler(object o, Core2DDrawingDocumentEventArgs e);
    public class Core2DDrawingDocumentEventArgs : EventArgs 
    {
        private ICore2DDrawingDocument m_Document;
        private Int32  m_ZIndex;
        public Int32  ZIndex
        {
            get { return m_ZIndex; }
        }
        public ICore2DDrawingDocument Document
        {
            get { return m_Document; }
        }
        public Core2DDrawingDocumentEventArgs(ICore2DDrawingDocument document)
        {
            this.m_Document = document;
            this.m_ZIndex = document.ZIndex ;
        }
        public Core2DDrawingDocumentEventArgs(ICore2DDrawingDocument document, int zIndex)
        {
            this.m_Document = document;
            this.m_ZIndex = zIndex ;
        }
    }
}

