

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _LayerRemoved.cs
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
file:_LayerRemoved.cs
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
    public class _LayerRemoved : HistoryActionBase 
    {
        private ICore2DDrawingLayer  m_Layer;
        private ICore2DDrawingDocument  m_Document;
        private int m_lIndex;
        public ICore2DDrawingDocument  Document
        {
            get { return m_Document; }
        }
        public ICore2DDrawingLayer  Layer
        {
            get { return m_Layer; }
        }
        public _LayerRemoved(ICore2DDrawingDocument document, ICore2DDrawingLayer layer, int zindex)
        {
            if (document == null)
                throw new CoreException(enuExceptionType.ArgumentIsNull, "document");
            if (layer == null)
                throw new CoreException(enuExceptionType.ArgumentIsNull, "layer");
            this.m_Layer = layer;
            this.m_Document = document;
            this.m_lIndex = zindex ;
        }
        public override void Undo()
        {
            //add layer
            this.m_Document.Layers.InsertAt(this.m_lIndex, this.m_Layer);
        }
        public override void Redo()
        {
            //remove layer
            this.m_Document.Layers.Remove(this.m_Layer);
        }
        public override string Info
        {
            get { return CoreSystem.GetString(HistoryConstant.HISTORY_LAYEDRM); }
        }
        public override string ImgKey
        {
            get { return CoreSystem.GetString (HistoryConstant.IMG_LAYERRM); }
        }
    }
}

