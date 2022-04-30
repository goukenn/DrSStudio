

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKD2DAnimatedSnippetLayer.cs
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
file:IGKD2DAnimatedSnippetLayer.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.ICore.Drawing2D.WinUI
{
    using IGK.ICore.WinCore;
    using IGK.ICore;
    using IGK.ICore.WinUI;
    /// <summary>
    /// represent a animated snippet layers;
    /// </summary>
    public class IGKD2DAnimatedSnippetLayer 
    {
        List<IGK2DDAnimatedSnippetLayerItem> m_snippets;
        protected List<IGK2DDAnimatedSnippetLayerItem> Snippets {
            get {
                return this.m_snippets;
            }
        }
        public IGKD2DAnimatedSnippetLayer()
        {
            this.m_snippets = new List<IGK2DDAnimatedSnippetLayerItem>();
        }
        internal void Add(IGK2DDAnimatedSnippetLayerItem v_snippet)
        {
            if ((v_snippet == null) || (this.m_snippets.Contains(v_snippet)))
                return;
            this.m_snippets.Add(v_snippet);
            v_snippet.Layer = this;
        }
        internal void Remove(IGK2DDAnimatedSnippetLayerItem snippet)
        {
            if (snippet == null)
                return;
            this.m_snippets.Remove(snippet);
            snippet.Layer = null;
        }
    }
}

