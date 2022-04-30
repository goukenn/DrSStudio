

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: PDFDictionaryData.cs
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
file:PDFDictionaryData.cs
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
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.PDFApi
{
    public class PDFDictionaryData : PDFDataBase 
    {
        PDFDictionaryEntries m_entries;
        PDFDataBase m_owner;
        public PDFDictionaryEntries Entries {
            get {
                return this.m_entries;
            }
        }
        public PDFDictionaryData()
        {
            m_entries = new PDFDictionaryEntries();
        }
        public PDFDictionaryData(PDFDataBase owner):this()
        {            
            this.m_owner = owner;
        }
        public PDFDataBase GetEntry(string key)        
        {
            if (this.m_entries.Contains(key))
            {
                return this.m_entries[key].Data as PDFDataBase;
            }
            return null;
        }
        public PDFDataBase GetEntry(string key, Type defaultType)
        {
            if (this.m_entries.Contains(key))
            {
                return this.m_entries[key].Data as PDFDataBase;
            }
            else{
                PDFDataBase b = PDFObject.CreatePDFObject(defaultType);
                if (b != null)
                {
                    this.m_entries.Add(key, b);
                    return null;
                }
            }
            return null;
        }
        public override void Render(System.IO.Stream stream)
        {
            if (this.m_entries.Count <= 0)
                return;
            Utils.TextUtils.WriteString(stream, "<<" + PDFConstant.EOF);
            foreach (KeyValuePair<PDFIdentifierStruct , IPDFDictionaryEntry  > item in this.m_entries)
            {
                item.Value.Identifier.Render(stream);
                stream.WriteByte(PDFConstant.SPACE);
                item.Value.Data.Render (stream);
                Utils.TextUtils.WriteString(stream, PDFConstant.EOF);
            }
            Utils.TextUtils.WriteString(stream, string.Format (">>{0}", PDFConstant.EOF));
        }
        public class PDFDictionaryEntries : IPDFDictionaryCollections
        {
            Dictionary<PDFIdentifierStruct, IPDFDictionaryEntry> m_dic;
            public PDFDictionaryEntries()
            {
                m_dic = new Dictionary<PDFIdentifierStruct, IPDFDictionaryEntry>();
            }
            #region IPDFDictionaryCollections Members
            public int Count
            {
                get { return this.m_dic.Count; }
            }
            public void Add(IPDFDictionaryEntry entry)
            {
                m_dic.Add(new  PDFIdentifierStruct (entry.Identifier.Value ), entry);
            }
            public void Remove(IPDFDictionaryEntry entry)
            {
                throw new NotImplementedException();
            }
            #endregion
            #region IEnumerable Members
            public System.Collections.IEnumerator GetEnumerator()
            {
                return this.m_dic.GetEnumerator();
            }
            #endregion
            #region IPDFDictionaryCollections Members
            public void Add(string Identifier, PDFDataBase value)
            {
                PDFIdentifierStruct v_i = new PDFIdentifierStruct(Identifier );                
                if (!this.m_dic.ContainsKey(v_i))
                {
                    m_dic.Add(v_i, new PDFDictionaryEntry(new PDFIdentifier(Identifier), value));
                }
            }
            public void Add(string Identifier, string value)
            {
                PDFIdentifierStruct v_i = new PDFIdentifierStruct(Identifier);                
                if (!this.m_dic.ContainsKey(v_i))
                {
                    m_dic.Add(v_i, new PDFDictionaryEntry(new PDFIdentifier(Identifier) , (PDFStringData)value));
                }
            }
            #endregion
            #region IPDFDictionaryCollections Members
            public IPDFDictionaryEntry this[string key]
            {
                get {
                    PDFIdentifierStruct v_i = new PDFIdentifierStruct(key);
                    if (m_dic.ContainsKey(v_i))
                    {
                        return m_dic [v_i ];
                     }
                     return null;
                }
                set {
                    PDFIdentifierStruct v_i = new PDFIdentifierStruct(key);
                    if (m_dic.ContainsKey(v_i))
                    {
                        m_dic[v_i] = value;
                    }
                }
            }
            #endregion
            public bool Contains(string key)
            {
                if (string.IsNullOrEmpty(key))
                    return false;
                PDFIdentifierStruct v_i = new PDFIdentifierStruct(key);
                return (m_dic.ContainsKey(v_i));
            }
            public void Remove(string key)
            {
                PDFIdentifierStruct v_i = new PDFIdentifierStruct(key);
                if (m_dic.ContainsKey(v_i))
                    m_dic.Remove(v_i);
            }
        }
    }
}

