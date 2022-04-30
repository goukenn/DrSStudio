

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: PresentationDocument.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.WinCore;
using IGK.ICore;
using IGK.ICore.Codec;
using IGK.ICore.Drawing2D;
using IGK.ICore.GraphicModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace IGK.DrSStudio.Presentation
{
    /// <summary>
    /// represent a presentation document
    /// </summary>
    public class PresentationDocument 
    {
        private ICoreWorkingDocument m_document;//document host
        private enuPresentationType m_PresentationType;//type of the document
        private PresentationPageCollections m_Pages;
        private int m_selectedIndex;

        public event EventHandler DocumentChanged;

        public enuPresentationType PresentationType
        {
            get { return m_PresentationType; }
        }

        internal void MoveBack()
        {
            if (this.SelectedIndex > 0)
                this.SelectedIndex--;
        }

        internal void MoveNext()
        {
            this.SelectedIndex++;
        }

        internal void GoToBegin()
        {
            this.SelectedIndex = 0;
        }

        internal void GoToEnd()
        {
            if (this.m_Pages.Count > 0)
            {
                this.SelectedIndex = this.m_Pages.Count - 1;                
            }
        }

        public int SelectedIndex {
            get {
                return this.m_selectedIndex;
            }
            set {
                if (this.m_selectedIndex != value) {
                    if ((value >= 0) && (value < this.m_Pages.Count)) {
                        this.Document = this.m_Pages[value];
                        this.m_selectedIndex = value;
                        OnSelectedIndexChanged(EventArgs.Empty);
                    }

                }
            }
        }

        private void OnSelectedIndexChanged(EventArgs empty)
        {

        }

        /// <summary>
        /// get or set the present dtocument
        /// </summary>
        public ICoreWorkingDocument Document
        {
            get { return m_document; }
            private set
            {
                if (m_document != value)
                {
                    m_document = value;
                    OnDocumentChanged(EventArgs.Empty);
                }
            }
        }

        private void OnDocumentChanged(EventArgs empty)
        {
            DocumentChanged?.Invoke(this, empty);
        }

        private PresentationDocument(){
            this.m_Pages = new PresentationPageCollections(this);
            this.m_selectedIndex = -1;
        }

        internal ICore2DDrawingDocument GetPage(int pageIndex)
        {
            return this.m_Pages[pageIndex];
        }


        /// <summary>
        /// create from a single document
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public static PresentationDocument CreateFrom(ICore2DDrawingDocument document)
        {
            if (document == null)
            {
                return null;
            }
            PresentationDocument doc = new PresentationDocument
            {
                Document = document,
                m_PresentationType = enuPresentationType.Drawing2DScene
            };
            doc.m_Pages.Add(document);
            return doc;
        }
        public void Render(ICoreGraphics graphics)
        { 
            switch(this.PresentationType)
            {
                case enuPresentationType.Drawing2DScene:
                    this.m_document.Draw(graphics);
                break;
                case enuPresentationType.Drawing3DScene :
                break;
                case enuPresentationType.Video :
                break;
            }

        }

        internal bool HasMorePage(int pageIndex)
        {
            return this.m_Pages.Count > pageIndex;
        }

        internal void Update() { //update the object state
        }

        internal void Render(ICoreGraphics device, Rectanglei clientRectangle){//render to graphics
            switch (this.PresentationType)
            {
                case enuPresentationType.Drawing2DScene:
                    this.m_document.Draw(device, clientRectangle, true, enuFlipMode.None);
                    break;
                case enuPresentationType.Drawing3DScene:
                    break;
                case enuPresentationType.Video:
                    break;
            }
        }
        /// <summary>
        /// open a presentation documenet from file
        /// </summary>
        /// <param name="file">file name of the presentation</param>
        /// <returns></returns>
        public static PresentationDocument Open(string file)
        {
            ICoreWorkingDocument[] docs =  CoreDecoder.Instance.OpenFileDocument(file);
            if ((docs != null) && (docs.Length > 0))
            {
                var v_presentation = CreateFrom(docs[0] as ICore2DDrawingDocument);
                if (docs.Length > 1) {

                    for (int i = 1; i < docs.Length; i++)
                    {
                        if (docs[i] is ICore2DDrawingDocument m) {
                            v_presentation.LoadPage(m);
                        }
                    }
                }
                v_presentation.m_selectedIndex = 0;
                return v_presentation;
            }

            return null;
        }

        private void LoadPage(ICore2DDrawingDocument m)
        {
            this.m_Pages.Add(m);
        }


        sealed class PresentationPageCollections : IEnumerable
        {
            private PresentationDocument m_owner;
            private List<ICore2DDrawingDocument> m_pages;
            internal ICore2DDrawingDocument this[int index] => m_pages[index];
            ///<summary>
            ///public .ctr
            ///</summary>
            public PresentationPageCollections(PresentationDocument owner)
            {
                this.m_owner = owner;
                this.m_pages = new List<ICore2DDrawingDocument>();
            }
            internal void Add(ICore2DDrawingDocument document)
            {
                this.m_pages.Add(document);
            }

            public IEnumerator GetEnumerator()
            {
                return this.m_pages.GetEnumerator();
            }

            public int Count => this.m_pages.Count;
        }
    }
}
