

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreWorkingContextMenuCollections.cs
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
file:CoreWorkingContextMenuCollections.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
namespace IGK.ICore
{
    using IGK.ICore;using IGK.ICore.Menu;
    using IGK.ICore.ContextMenu;
    using System.Xml;
    using IGK.ICore.WinUI;
    class CoreWorkingContextMenuCollections :
        CoreSystemCollections,
        ICoreContextMenuCollections
    {
        Dictionary<string, IGK.ICore.ContextMenu.ICoreContextMenuAction> m_menus;
        List<ICoreContextMenuAction> m_rootsMenu;
        public CoreWorkingContextMenuCollections(CoreSystem core)
            : base(core)
        {
            this.m_menus = new Dictionary<string, IGK.ICore.ContextMenu.ICoreContextMenuAction>();
            this.m_rootsMenu = new List<ICoreContextMenuAction>();
        }
       public IGK.ICore.ContextMenu.ICoreContextMenuAction this[string key]
        {
            get { return this.m_menus[key]; }
        }
        public int Count
        {
            get { return this.m_menus.Count; }
        }
        public System.Collections.IEnumerator GetEnumerator()
        {
            return this.m_menus.GetEnumerator();
        }
        override protected void RegisterType(Type t)
        {
            CoreContextMenuAttribute v_attr =
                Attribute.GetCustomAttribute(
                t, typeof(CoreContextMenuAttribute)) as CoreContextMenuAttribute;
            if ((v_attr == null) || (string.IsNullOrEmpty(v_attr.Name)))
                return;
            ICoreContextMenuAction v_menu =
                        t.Assembly.CreateInstance(t.FullName) as ICoreContextMenuAction;
            if (v_menu == null) return;
            Register(v_attr, v_menu);
        }
        private void RemoveSystemRootMenu(string v_parentName)
        {
            do
            {
                if (this.m_menus.ContainsKey(v_parentName))
                {
                    if (this.m_menus[v_parentName] is CoreSystemContextRootMenu)
                    {
                        this.m_rootsMenu.Remove(this.m_menus[v_parentName]);
                        this.m_menus.Remove(v_parentName);
                        if (v_parentName.Contains("."))
                            v_parentName = Path.GetFileNameWithoutExtension(v_parentName);
                        else
                            v_parentName = null;
                    }
                    else
                        break;
                }
                else
                    break;
            }
            while (!string
                .IsNullOrEmpty(v_parentName));
        }
        private void ReplaceRootMenu(CoreSystemContextRootMenu rootMenu, ICoreContextMenuAction v_menu)
        {
            ICoreContextMenuAction[] v_tab = rootMenu.Childs.ToArray();
            rootMenu.Childs.Clear();
            v_menu.Childs.AddRange(v_tab);
            this.m_menus[v_menu.Id] = v_menu;
            //remove parent root
            if (this.m_rootsMenu.Contains(rootMenu))
                this.m_rootsMenu.Remove(rootMenu);
            if ((rootMenu.Parent != null)&&(rootMenu.Parent is CoreSystemContextRootMenu ))
            { //replace the child wit the current
                CoreSystemContextRootMenu v_parent = rootMenu.Parent as CoreSystemContextRootMenu;
                v_parent.Childs.Remove(rootMenu);                
            }
            if (v_menu.Parent == null)
            {
                this.m_rootsMenu.Add(v_menu);
            }
        }
        private void GenerateParent(string parentName)
        {
            string[] v = parentName.Split('.');
            string name = string.Empty;
            string m_parent = string.Empty;
            for (int i = 0; i < v.Length; i++)
            {
                if (i != 0)
                    name += ".";
                name += v[i];
                if (!this.m_menus.ContainsKey(name))
                {
                    CoreSystemContextRootMenu rm = new CoreSystemContextRootMenu();
                    rm.Id = name;
                    m_menus.Add(name, rm);
                    if (i > 0)
                    {
                        this.m_menus[m_parent].Childs.Add(rm);
                    }
                    else
                    {
                        // menu is root
                        this.m_rootsMenu.Add(rm);
                    }
                }
                m_parent = name;
            }
        }
        public ICoreContextMenuAction[] RootMenus()
        {
            return this.m_rootsMenu.ToArray();
        }
        /// <summary>
        /// sort the root items
        /// </summary>
        public void Sort()
        {
            this.m_rootsMenu.Sort(new CoreMenuComparer());
            //sort child
            foreach (ICoreContextMenuAction m in this.m_rootsMenu)
            {
                m.Childs.Sort();
            }
        }
        #region ICoreContextMenuCollections Members
        public bool Register(CoreContextMenuAttribute v_attr, ICoreContextMenuAction v_menu)
        {
            CoreSystem.RegisterAction(v_attr, v_menu);
            if (v_menu.IsRootMenu)
            {
                if (!this.m_menus.ContainsKey(v_attr.Name))
                {
                    this.m_menus.Add(v_attr.Name, v_menu);
                    this.m_rootsMenu.Add(v_menu);
                }
                else
                {
                    CoreSystemContextRootMenu rootMenu = this.m_menus[v_attr.Name]
                     as CoreSystemContextRootMenu;
                    if (rootMenu != null)
                    {
                        //remplace the root system
                        ReplaceRootMenu(rootMenu, v_menu);
                        string v_parentName = System.IO.Path.GetFileNameWithoutExtension(v_attr.Name);
                        RemoveSystemRootMenu(v_parentName);
                    }
#if DEBUG
                    //else
                    //{
                    //    throw new CoreException(enuExceptionType.OperationNotValid, CoreSystem.GetString(CoreConstant.ERR_MENUNOTREGISTERED, v_attr.Name));
                    //}
#endif
                }
                return true ;
            }
            if (!this.m_menus.ContainsKey(v_attr.Name))
            {
                string v_parentName = Path.GetFileNameWithoutExtension(v_attr.Name);
                if (v_parentName == v_attr.Name)
                {
                    //no parent
                    this.m_menus.Add(v_attr.Name, v_menu);
                    this.m_rootsMenu.Add(v_menu);
                }
                else
                {
                    ICoreContextMenuAction v_p = null;
                    if (this.m_menus.ContainsKey(v_parentName))
                    {
                        //this.Add to parent
                        v_p = this.m_menus[v_parentName];
                        v_p.Childs.Add(v_menu);
                        this.m_menus.Add(v_attr.Name, v_menu);
                    }
                    else
                    {
                        GenerateParent(v_parentName);
                        this.m_menus[v_parentName].Childs.Add(v_menu);
                        this.m_menus.Add(v_attr.Name, v_menu);
                    }
                }
            }
            else
            {
                //alregy register
                CoreSystemContextRootMenu rootMenu = this.m_menus[v_attr.Name]
                as CoreSystemContextRootMenu;
                if (rootMenu != null)
                {
                    //remplace the root system
                    ReplaceRootMenu(rootMenu, v_menu);
                }
                else
                {
                    throw new CoreException(enuExceptionType.OperationNotValid, CoreSystem.GetString("ERR.MENUNOTREGISTRATED"));
                }
            }
            return true;
        }
        #endregion
        public void ExportMenuAsXML(string filename)
        {
            XmlWriterSettings s = new XmlWriterSettings();
            s.Indent = true;
            XmlWriter xml = XmlWriter.Create(filename, s);
            xml.WriteStartElement("drsContextMenu");
            xml.WriteAttributeString("count", this.Count.ToString());
            foreach (var item in this.m_menus)
            {
                xml.WriteStartElement("menu");
                xml.WriteAttributeString("id", item.Value.Id);
                xml.WriteAttributeString("index", item.Value.Index.ToString());
                if (item.Value.IsRootMenu)
                {
                    xml.WriteAttributeString("isroot", "true");
                }
                if (item.Value.ShortCut != enuKeys.None )
                    xml.WriteAttributeString("shortcut", item.Value.ShortCut.ToString());
                xml.WriteAttributeString("type", item.Value.GetType().AssemblyQualifiedName);
                xml.WriteEndElement();
            }
            xml.WriteEndElement();
            xml.Close();
        }
        internal ICoreContextMenuAction[] GetRootMenus()
        {
            return this.m_rootsMenu.ToArray();
        }

        internal void Unregister(string  name)
        {
            if (this.m_menus.ContainsKey(name))
            {
                var c = this.m_menus[name];
                this.m_menus.Remove(name);
                if (this.m_rootsMenu.Contains(c))
                    this.m_rootsMenu.Remove(c);
                
            }

        }
        
    }
}

