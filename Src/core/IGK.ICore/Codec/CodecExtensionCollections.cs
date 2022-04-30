

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CodecExtensionCollections.cs
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
file:CodecExtensionCollections.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore.Codec
{
    using IGK.ICore;using IGK.ICore.Codec;
    /// <summary>
    /// represent the codec extension collection
    /// </summary>
    public sealed class CodecExtensionCollections : ICoreCodecExtensionCollections
    {
        List<string> m_ext;
        public CodecExtensionCollections()
        {
            this.m_ext = new List<string>();
        }
        #region ICoreCodecExtensionCollections Members
        public int Count
        {
            get { return this.m_ext.Count; }
        }
        public bool Contains(string ext)
        {
            if (string.IsNullOrEmpty(ext))
                return false;
            ext = ext .Replace (".","");
            return this.m_ext.Contains(ext.ToLower());
        }
        #endregion
        #region IEnumerable Members
        public System.Collections.IEnumerator GetEnumerator()
        {
            return this.m_ext.GetEnumerator();
        }
        #endregion
        /// <summary>
        /// add extension list. simi columns separated list
        /// </summary>
        /// <param name="extList"></param>
        public void Add(string extList)
        {
            if (string.IsNullOrEmpty(extList))
                return;
            string[] v_tab = extList.ToLower ().Split(';');
            for (int i = 0; i < v_tab.Length; i++)
            {
                if (!this.m_ext.Contains(v_tab[i]))
                    this.m_ext.Add(v_tab[i]);
            }
        }
        public void Remove(string extList)
        {
            if (string.IsNullOrEmpty(extList))
                return;
            string[] v_tab = extList.ToLower().Split(';');
            for (int i = 0; i < v_tab.Length; i++)
            {
                if (this.m_ext.Contains(v_tab[i]))
                    this.m_ext.Remove (v_tab[i]);
            }
        }
    }
}

