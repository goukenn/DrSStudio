

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreButtonDocument.cs
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
file:CoreButtonDocument.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore.WinUI
{
    using IGK.ICore;
    using IGK.ICore.WinUI;
    using IGK.ICore.Drawing2D;
    using Resources;    /// <summary>
                        /// represent the base document icon
                        /// </summary>
    public class CoreButtonDocument :
        ICoreButtonDocument
         
    {
        private bool m_IsDisposed;
        private ICore2DDrawingDocument m_Normal;
        private ICore2DDrawingDocument m_Hover;
        private ICore2DDrawingDocument m_Down;
        private ICore2DDrawingDocument m_Up;
        private ICore2DDrawingDocument m_Disabled;
        private CoreButtonDocument() { 
        }
        public bool IsDisposed       
        {
            get { return m_IsDisposed; }
        }
        /// <summary>
        /// save the button document
        /// </summary>
        /// <param name="filename"></param>
        public void Save(string filename)
        {
            IGK.ICore.Codec.CoreEncoder.Instance.Save(null, filename , new ICoreWorkingDocument[]{
                    this.Normal , 
                    this.Hover  , 
                    this.Down  ,                     
                    this.Up,
                    this.Disabled });
        }
        public ICore2DDrawingDocument Disabled
        {
            get { return m_Disabled; }
        }
        public ICore2DDrawingDocument Up
        {
            get { return m_Up; }
        }
        public ICore2DDrawingDocument Down
        {
            get { return m_Down; }
        }
        public ICore2DDrawingDocument Hover
        {
            get { return m_Hover; }
        }
        public ICore2DDrawingDocument Normal
        {
            get { return m_Normal; }
        }
        public static CoreButtonDocument Create(params ICore2DDrawingDocument[] document)
        {
            if (document != null)
            {
                CoreButtonDocument v_doc = null;
                switch (document.Length)
                {
                    case 1:
                        v_doc = new CoreButtonDocument();
                        v_doc.m_Normal = document[0].Clone() as ICore2DDrawingDocument;
                        v_doc.m_Up = document[0].Clone() as ICore2DDrawingDocument;
                        v_doc.m_Down = document[0].Clone() as ICore2DDrawingDocument;
                        v_doc.m_Hover = document[0].Clone() as ICore2DDrawingDocument;
                        v_doc.m_Disabled = document[0].Clone() as ICore2DDrawingDocument;
                        return v_doc;
                    case 5:
                        v_doc = new CoreButtonDocument();
                        v_doc.m_Normal = document[0].Clone() as ICore2DDrawingDocument;
                        v_doc.m_Hover = document[1].Clone() as ICore2DDrawingDocument;
                        v_doc.m_Up = document[3].Clone() as ICore2DDrawingDocument;
                        v_doc.m_Down = document[2].Clone() as ICore2DDrawingDocument;                        
                        v_doc.m_Disabled = document[4].Clone() as ICore2DDrawingDocument;                        
                        return v_doc;
                }
            }
            return null;
        }

        public static CoreButtonDocument CreateFromRes(string key) {
            return Create(CoreResources.GetAllDocuments (key));
        }

        public ICore2DDrawingDocument[] GetDocuments()
        {
            return new ICore2DDrawingDocument[] { 
                this.m_Normal ,
                this.m_Hover ,
                this.m_Down,
                this.m_Up,
                this.m_Disabled 
            };
        }
        public void Dispose()
        {
            if (this.m_IsDisposed == false)
            {
                this.m_Disabled.Dispose();
                this.m_Down.Dispose();
                this.m_Hover.Dispose();
                this.m_Normal.Dispose();
                this.m_Up.Dispose();
                this.m_IsDisposed = true;
            }
        }

        public ICore2DDrawingDocument GetDocument(enuButtonState state)
        {
            switch (state)
            {
                case enuButtonState.Hover:
                    return this.Hover;
             
                case enuButtonState.Down:
                    return this.Down;
             
                case enuButtonState.Up:
                    return this.Up;
             
                case enuButtonState.Disabled:
                    return this.Disabled;
             
                case enuButtonState.None:
                    break;
                default:
                    break;
            }
            return this.Normal;
            
        }
    }
}

