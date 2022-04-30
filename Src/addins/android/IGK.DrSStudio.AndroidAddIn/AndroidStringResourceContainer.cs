

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AndroidStringResourceContainer.cs
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
file:AndroidStringResourceContainer.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
namespace IGK.DrSStudio.Android
{
    class AndroidStringResourceContainer : AndroidResourceContainerBase 
    {
        StringResourceEntries m_resourceEntries;
        public AndroidStringResourceContainer()
        {
            m_resourceEntries = new StringResourceEntries(this);
        }
        public override void SaveTo(string directory)
        {
            XmlWriterSettings v_settings = new XmlWriterSettings();
            v_settings.Indent = true;
            v_settings.CloseOutput = true;
            XmlWriter xwriter = null;
            try
            {
                xwriter = XmlWriter.Create(string.Format("{0}/values/strings.xml", directory), v_settings);
                xwriter.WriteStartElement(AndroidConstant.RESOURCE_TAG);
                foreach (IAndroidResourceItem item in this.m_resourceEntries)
                {
                    xwriter.WriteStartElement(item.ResourceType.ToString ());
                    xwriter.WriteAttributeString("name", item.Name);
                    xwriter.WriteString(item.Value.ToString());
                    xwriter.WriteEndElement();
                }
                xwriter.WriteEndElement();
            }
            catch
            {
            }
            finally
            {
                if (xwriter != null)
                    xwriter.Close();
            }
        }
        class StringResourceEntries : System.Collections.IEnumerable
        {
            AndroidStringResourceContainer owner;
            Dictionary<string, IAndroidResourceItem> m_entries;
            public StringResourceEntries(AndroidStringResourceContainer owner)
            {
                this.owner = owner;
                this.m_entries = new Dictionary<string, IAndroidResourceItem>();
            }
            public int Count { get { return this.m_entries.Count; } }
            public void ChangeId(string oldid, string newid)
            { 
                if (( oldid == newid ) || (
                    string.IsNullOrWhiteSpace(newid)))
                    return ;
                IAndroidResourceItem i = this.m_entries[oldid];
                this.m_entries.Remove(oldid);
                this.m_entries.Add(newid, i);
            }
            /// <summary>
            /// Get the enumerable of this items.
            /// </summary>
            /// <returns></returns>
            public System.Collections.IEnumerator GetEnumerator()
            {
                return this.m_entries.Values.GetEnumerator();
            }
            internal void Add(string name, string v)
            {
                IAndroidResourceItem item = null;
                if (this.m_entries.ContainsKey(name))
                {
                    item = this.m_entries[name];
                }
                else
                {
                    item = new AndroidResourceItem(name, v, enuAndroidResourceType.@string);
                    this.m_entries.Add(name, item);
                }
                item.Value = v;
            }
            public IAndroidResourceItem this[string name] {
                get {
                    return this.m_entries[name];
                }
            }
        }
        public override void setValue(string name, object o)
        {
            string v = o as string;
            if (string.IsNullOrEmpty(v))
                return;
            this.m_resourceEntries.Add(name, v);
        }
        public override object getValue(string name)
        {
            return this.m_resourceEntries[name].Value;
        }
    }
}

