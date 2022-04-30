

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GifFileDocument.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
using IGK.ICore;
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
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:GifFileDocument.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.Drawing2D.GifAddIn
{
    /// <summary>
    /// represent a gif document
    /// </summary>
    class GifFileDocument : ICoreWorkingObject, ICoreIdentifier
    {
        private GifFrameCollections m_Frames;
        private string m_Name;
        /// <summary>
        /// get or set the 
        /// </summary>
        public string Name
        {
            get { return m_Name; }
            set
            {
                if (m_Name != value)
                {
                    m_Name = value;
                }
            }
        }
        public GifFrameCollections Frames
        {
            get { return m_Frames; }
        }
        //.ctr
        public GifFileDocument()
        {
            this.m_Frames = new GifFrameCollections(this);
        }
        /// <summary>
        /// represent a frame collection
        /// </summary>
        public class GifFrameCollections
        {
            List<GifFrameDocument> m_frames;
            GifFileDocument m_owner;
            public GifFrameDocument[] ToArray() { return this.m_frames.ToArray(); }
            public GifFrameDocument this[int index]{
                get{
                    return this.m_frames[index];
                }
            }
            public void Add(GifFrameDocument document)
            {
                if (document == null)return ;
                    if (!this.m_frames.Contains (document ))
                        this.m_frames.Add (document );
            }
            public void Remove(GifFrameDocument document)
            {
                if (document == null)return ;
                if (this.m_frames.Contains (document ))
                        this.m_frames.Remove (document );
            }
            public GifFrameCollections(GifFileDocument document)
            {
                this.m_owner = document;
                this.m_frames = new List<GifFrameDocument>();
            }
            public int Count { get { return this.m_frames.Count; } }
        }
        /// <summary>
        /// save the current document
        /// </summary>
        /// <returns></returns>
        public bool Save()
        {
            return false;
        }
        string ICoreIdentifier.Id
        {
            get { return this.Name; }
        }
    }
}

