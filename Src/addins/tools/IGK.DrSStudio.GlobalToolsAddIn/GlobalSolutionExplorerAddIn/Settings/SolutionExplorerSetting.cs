

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: SolutionExplorerSetting.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.WinCore;
using IGK.ICore;using IGK.ICore.Codec;
using IGK.ICore.Settings;
using IGK.ICore.WinUI;
using IGK.ICore.WinUI.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Settings
{
    [CoreAppSetting("SolutionExplorerSetting")]
    class SolutionExplorerSetting : CoreSettingBase, ICoreSerializerService
    {
        public bool IsValid
        {
            get { return true; }
        }
        private static SolutionExplorerSetting sm_instance;
        private SolutionCollection m_solutions;
        public SolutionCollection Solutions {
            
            get {
                return this.m_solutions;
            }
        }
        private SolutionExplorerSetting()
        {
            this.m_solutions = new SolutionCollection();
        
        }

        public static SolutionExplorerSetting Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static SolutionExplorerSetting()
        {
            sm_instance = new SolutionExplorerSetting();

        }

        protected override bool LoadDummyChildSetting(KeyValuePair<string, ICoreSettingValue> d)
        {//load dummy setting
            if (d.Value ==null)
                return false ;
            string t = d.Value.Value!= null ? d.Value.Value.ToString() : string.Empty ;
            if (System.IO.File.Exists(t))
            {
                string n = System.IO.Path.GetFileName(t);
                this.Solutions.Add(n, t);
                return true;
            }
            return false;
        }
      
        public override enuParamConfigType GetConfigType()
        {
            return enuParamConfigType.NoConfig;
        }
        public override ICoreControl GetConfigControl()
        {
            return null;
        }
        public override ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
        {
            
            return null;
        }

        public class SolutionCollection : IEnumerable 
        {
            private List<SolutionItem> m_list;
            private SolutionExplorerSetting m_owner;

            public SolutionCollection(SolutionExplorerSetting setting)
            {
                this.m_owner = setting;
            }
            public void Add(string name, string fileLocation) {
                SolutionItem i = new SolutionItem();
                  i.Name = name ;
                  i.Location = fileLocation;
                  if (!this.m_list.Contains(i))
                  {
                      this.m_list.Add(i);
                  }
            }
            public void Remove(string name, string fileLocation)
            {
                SolutionItem i = new SolutionItem();
                i.Name = name;
                i.Location = fileLocation;
                if (this.m_list.Contains(i))
                {
                    this.m_list.Remove(i);
                    this.m_owner.Add (new SolutionItmValue(i.Name, i));
                }
            }

            public SolutionCollection()
            {
                this.m_list = new List<SolutionItem>();
            }
            public IEnumerator GetEnumerator()
            {
                return this.m_list.GetEnumerator();
            }
        }
        public struct  SolutionItem
        {
            private string m_Name;
            private string m_Location;

            public string Location
            {
                get { return m_Location; }
                set
                {
                    if (m_Location != value)
                    {
                        m_Location = value;
                    }
                }
            }
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
          
        }


        public class  SolutionItmValue : ICoreSettingValue
        {

            public bool HasChild
            {
                get { return false; }
            }

            public ICoreSettingValue this[string name]
            {
                get { return null; }
            }

            public object Value
            {
                get
                {
                    return this.m_value;
                }
                set
                {
                    this.m_value = value;
                }
            }

            public string Name
            {
                get { return "Item"; }
            }

            private object m_value;
            private string m_name;

            public event EventHandler ValueChanged;
            ///<summary>
            ///raise the ValueChanged 
            ///</summary>
            protected virtual void OnValueChanged(EventArgs e)
            {
                if (ValueChanged != null)
                    ValueChanged(this, e);
            }


            public SolutionItmValue(string name, SolutionItem solutionItem)
            {
                
                this.m_name = name;
                this.m_value =  solutionItem;
            }

            public void Add(ICoreSettingValue setting)
            {
                
            }

            public void Remove(ICoreSettingValue setting)
            {               
            }

            public IEnumerator GetEnumerator()
            {
                return null;
            }
        }


        public void Serialize(IXMLSerializer xwriter)
        {
#if DEBUG
            //xwriter.WriteStartElement("item");
            //xwriter.WriteValue(CoreConstant.DEBUG_TEMP_FOLDER+"\\test");
            //xwriter.WriteEndElement();
#endif

            
            foreach (SolutionItem item in this.Solutions)
            {
                xwriter.WriteStartElement("item");
                xwriter.WriteValue(item.Location);
                xwriter.WriteEndElement();
            }
        }

        public void Deserialize(IXMLDeserializer xreader)
        {
            
        }
    }
}
