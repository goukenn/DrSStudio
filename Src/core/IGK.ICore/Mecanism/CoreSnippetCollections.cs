

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreSnippetCollections.cs
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
file:CoreSnippetCollections.cs
*/
using IGK.ICore;using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.ICore.Mecanism
{
    /// <summary>
    /// represent a base snippet collection class
    /// </summary>
    public abstract class CoreSnippetCollectionsBase : MarshalByRefObject , ICoreSnippetCollections 
    {
        private Dictionary<int, ICoreSnippet> m_rsnippets;
        private bool m_disabled;
        public int Count { get { return this.m_rsnippets.Count; } }
        /// <summary>
        /// get if element is disable
        /// </summary>
        public bool IsDisabled { get { return this.m_disabled; } }
        public ICoreSnippet this[int index]
        {
            get
            {
                if (this.m_rsnippets.ContainsKey(index))
                    return m_rsnippets[index];
                return null;
            }
        }
        public CoreSnippetCollectionsBase()
        {
            this.m_rsnippets = new Dictionary<int, ICoreSnippet>();
        }
        /// <summary>
        /// add snippets 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="snippet"></param>
        public void Add(int index, ICoreSnippet snippet)
        {
            if (snippet == null) return;
            if (this.m_rsnippets.ContainsKey(index))
            {
                if (this.m_rsnippets[index] != snippet)
                    this.m_rsnippets[index].Dispose();
                this.m_rsnippets[index] = snippet;
            }
            else
                this.m_rsnippets.Add(index, snippet);
        }
        public void Add(ICoreSnippet snippet)
        {
            if (snippet == null) return;
            this.Add(snippet.Index, snippet);
        }
        public virtual void Dispose()
        {
            foreach (var item in this.m_rsnippets)
            {
                item.Value.Dispose();
            }
            this.m_rsnippets.Clear();
        }
        public void Enable()
        {
            foreach (KeyValuePair<int, ICoreSnippet> item in this.m_rsnippets)
            {
                item.Value.Enabled = true;
                item.Value.Visible = true;
            }
            this.m_disabled = false;
        }
        public void Disabled()
        {
            foreach (KeyValuePair<int, ICoreSnippet> item in this.m_rsnippets)
            {
                item.Value.Enabled = false;
                item.Value.Visible = false;
            }
            this.m_disabled = true;
        }
        public void Remove(ICoreSnippet iCoreSnippets)
        {
            if (this.m_rsnippets.ContainsKey(iCoreSnippets.Index))
            {
                this.m_rsnippets.Remove(iCoreSnippets.Index);
                iCoreSnippets.Dispose();
            }
        }
        public bool Contains(int i)
        {
            return this.m_rsnippets.ContainsKey(i);
        }
        public System.Collections.IEnumerator GetEnumerator()
        {
            return this.m_rsnippets.GetEnumerator();
        }
        public void Remove(int index)
        {
            if (this.m_rsnippets.ContainsKey(index))
            {
                this.Remove(this.m_rsnippets[index]);
            }
        }
    }
}

