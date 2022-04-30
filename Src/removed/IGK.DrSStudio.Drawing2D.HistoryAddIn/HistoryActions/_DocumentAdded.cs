

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _DocumentAdded.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV - [2008 - 2014]
author: C.A.D . BONDJE DOUE
file:_DocumentAdded.cs
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
/* 
-------------------------------------------------------------
This file is part of iGK-DEV-DrawingStudio
-------------------------------------------------------------
-------------------------------------------------------------
-------------------------------------------------------------
view license file in Documentation folder to get more info
Copyright (c) 2008-2010 
Author  : Charles A.D. BONDJE DOUE 
mail : bondje.doue@hotmail.com
-------------------------------------------------------------
-------------------------------------------------------------
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.Drawing2D.HistoryActions
{
    using IGK.ICore;using IGK.DrSStudio.History;
    using IGK.DrSStudio.Drawing2D.WinUI ;
    class _DocumentAdded : HistoryActionBase 
    {
         private ICore2DDrawingDocument  m_Document;
        private ICore2DDrawingSurface  m_Surface;
        private int m_ZIndex;
        public int ZIndex
        {
            get { return m_ZIndex; }
        }
        public ICore2DDrawingSurface  Surface
        {
            get { return m_Surface; }
        }
        public ICore2DDrawingDocument  Document
        {
            get { return m_Document; }
        }
        public _DocumentAdded(ICore2DDrawingSurface surface, ICore2DDrawingDocument document)
        {
            if (surface == null)
                throw new CoreException(enuExceptionType.ArgumentIsNull, "surface");
            if (document == null)
                throw new CoreException(enuExceptionType.ArgumentIsNull, "document");
            this.m_Document = document;
            this.m_Surface = surface;
            this.m_ZIndex = document.ZIndex;
        }
        public override void Undo()
        {
            this.m_Surface.Documents.Remove(Document);
        }
        public override void Redo()
        {
            this.m_Surface.Documents.InsertAt(this.ZIndex, Document);
        }
        public override string Info
        {
            get { return CoreSystem.GetString(HistoryConstant.HISTORY_DOCADDED); }
        }
        public override string ImgKey
        {
            get { return CoreSystem .GetString (HistoryConstant.IMG_DOCUMENT_ADDED);  }
        }
    }
}
