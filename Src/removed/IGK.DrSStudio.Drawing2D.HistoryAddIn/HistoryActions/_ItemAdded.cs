

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _ItemAdded.cs
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
file:_ItemAdded.cs
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
    using IGK.ICore;using IGK.DrSStudio.Drawing2D;
    using IGK.DrSStudio.History;
    class _ItemAdded : HistoryActionImplement  
    {
        private Core2DDrawingLayer m_layer;
        private Core2DDrawingLayeredElement[] m_elements;
        public override string Info
        {
            get
            {
                switch (m_elements.Length)
                {
                    case 1:
                        return CoreSystem.GetString("History.ItemAdded", m_elements[0].Id);                        
                }
                return CoreSystem.GetString("History.ItemsAdded", m_elements.Length);
            }
        }
        public override string ImgKey
        {
            get
            {               
                return "add";
            }
        }
        public _ItemAdded(Core2DDrawingLayer layer, params Core2DDrawingLayeredElement[] element) 
        {
            this.m_elements = element;
            this.m_layer = layer;
        }
        public override void Undo()
        {
            m_layer.Elements.Remove(m_elements);
            this.InvalidateSurface();
        }
        public override void Redo()
        {
            m_layer.Elements.AddRange(m_elements);
            this.InvalidateSurface();
        }
    }
}

