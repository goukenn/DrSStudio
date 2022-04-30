

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreParameterTab.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
using IGK.ICore;using IGK.ICore.WinUI;
using IGK.ICore.WinUI.Configuration;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:CoreParameterTab.cs
*/
using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore.WinCore.Configuration
{
    public class CoreParameterTab : ICoreParameterTab 
    {
        private ICoreWorkingConfigurableObject m_Target;
        #region ICoreParameterTab Members
        /// <summary>
        /// get the parameter groups
        /// </summary>
        ICoreParameterGroupCollections m_Groups;
        /// <summary>
        /// get or set the group name
        /// </summary>
        public ICoreParameterGroupCollections Groups
        {
            get { return this.m_Groups; }
        }
        private string m_Name;
        private string m_CaptionKey;
        public string CaptionKey
        {
            get { return m_CaptionKey; }
            set
            {
                if (m_CaptionKey != value)
                {
                    m_CaptionKey = value;
                }
            }
        }
        public string Name
        {
            get { return m_Name; }
        }
        public CoreParameterTab(object Target, string name, string captionKey)
        {
            this.m_Groups = new GroupCollections(this);
            this.m_Target = (ICoreWorkingConfigurableObject )Target;
            this.m_Name = name;
            this.m_CaptionKey = captionKey;
        }
        public ICoreParameterGroup AddGroup(string groupName, string groupCaptionKey)
        {
            if (string.IsNullOrEmpty(groupName))
                return null;
            CoreParameterGroup tab = new CoreParameterGroup(this.Target, groupName, groupCaptionKey);
            this.m_Groups.Add(tab);
            return tab;
        }
        #endregion
        class GroupCollections : ICoreParameterGroupCollections 
        {
            public void Remove(string name)
            {
                if (ContainsKey(name))
                    m_groups.Remove(name);
            }
            public bool ContainsKey(string name)
            {
                return this.m_groups.ContainsKey(name);
            }
            CoreParameterTab m_tab;
            Dictionary<string,ICoreParameterGroup> m_groups;
            public GroupCollections(CoreParameterTab tab)
            {
                this.m_tab = tab;
                this.m_groups = new Dictionary<string, ICoreParameterGroup>();
            }
            #region ICoreParameterGroupCollections Members
            public int Count
            {
                get { return this.m_groups.Count; }
            }
            public ICoreParameterGroup this[string index]
            {
                get { return this.m_groups[index]; }
            }
            public void Add(ICoreParameterGroup group)
            {
                if ((group == null) || (m_groups.ContainsKey (group.Name )))
                    return;
                this.m_groups.Add(group.Name , group );
            }
            #endregion
            #region IEnumerable Members
            public System.Collections.IEnumerator GetEnumerator()
            {
                return this.m_groups.Values.GetEnumerator();
            }
            #endregion
            #region ICoreParameterGroupCollections Members
            public void Clear()
            {
                this.m_groups.Clear();
            }
            #endregion
        }
        #region ICoreParameterTab Members
        public ICoreParameterGroup  AddConfigObject(ICoreWorkingConfigurableObject element)
        {            
            if (this.Groups.Count == 0)
            {
                CoreParameterTabGroup group = new CoreParameterTabGroup(element, "Default", "lb.Default.caption");
                group.AddConfigObject(element);
                this.Groups.Add(group);
                return group;
            }
            return null;
        }
        #endregion
        internal void AddGroup(ICoreParameterGroup  p)
        {
            this.m_Groups.Add(p);
        }
        #region ICoreParameterTab Members
        public ICoreWorkingConfigurableObject Target
        {
            get { return m_Target; }
        }
        #endregion
    }
}

