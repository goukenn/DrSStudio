

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AndroidLayoutResourceContainer.cs
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
file:AndroidLayoutResourceContainer.cs
*/
using System;
using System.Collections.Generic;
using System.Xml ;
using System.Text;
namespace IGK.DrSStudio.Android
{
    /// <summary>
    /// represent a android layout container resources
    /// </summary>
    public sealed class AndroidLayoutResourceContainer : AndroidResourceContainerBase 
    {
        class AndroidLayoutCollection : System.Collections.IEnumerable 
        {
            AndroidLayoutResourceContainer m_owner;
            List<IAndroidLayout> m_layouts;
            public AndroidLayoutCollection(AndroidLayoutResourceContainer owner)
            {
                this.m_owner = owner;
                m_layouts = new List<IAndroidLayout>();
            }
            public System.Collections.IEnumerator GetEnumerator()
            {
                return this.m_layouts.GetEnumerator();
            }
            public int Count { get { return this.m_layouts.Count; } }
            public override string ToString()
            {
                return string.Format("Layouts:[#{0}]", this.m_layouts.Count);
            }
        }
        AndroidLayoutCollection m_layouts;
        public AndroidLayoutResourceContainer()
        {
            m_layouts = new AndroidLayoutCollection(this);    
        }
        public override void SaveTo(string directory)
        {
            XmlWriterSettings v_settings = new XmlWriterSettings();
            v_settings.Indent = true;
            v_settings.CloseOutput = true;
            XmlWriter xwriter = null;
            try
            {
                foreach (IAndroidLayout item in this.m_layouts)
                {
                    item.SaveTo(directory);
                }
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
        public override void setValue(string name, object o)
        {
            throw new NotImplementedException();
        }
        public override object getValue(string name)
        {
            throw new NotImplementedException();
        }
    }
}

