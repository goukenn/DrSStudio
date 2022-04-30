

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreWorkingMenuCollections.cs
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
file:CoreWorkingMenuCollections.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
namespace IGK.ICore
{
    using IGK.ICore;using IGK.ICore.Menu ;
    using System.Xml;
    using IGK.ICore.WinUI;
    /// <summary>
    /// represent a DRSStudio Menu collections
    /// </summary>
    sealed class CoreWorkingMenuCollections : 
        CoreSystemCollections,
        ICoreMenuCollections 
    {
        List<ICoreMenuAction> m_rootsMenu;
        Dictionary<string, IGK.ICore.Menu.ICoreMenuAction> m_menus;
        public CoreWorkingMenuCollections(CoreSystem core):base(core )
        {
            this.m_menus = new Dictionary<string, IGK.ICore.Menu.ICoreMenuAction>();
            m_rootsMenu = new List<ICoreMenuAction>();
        }
        public IGK.ICore.Menu.ICoreMenuAction this[string key]
        {
            get {
                if (this.m_menus.ContainsKey(key))
                {
                    return this.m_menus[key];
                }
                return null;
            }
        }
        public int Count
        {
            get { return this.m_menus.Count; }
        }
        public System.Collections.IEnumerator GetEnumerator()
        {
            return this.m_menus.GetEnumerator ();
        }
        protected override  void RegisterType(Type t)
        {
            CoreMenuAttribute v_attr = (CoreMenuAttribute)
                Attribute.GetCustomAttribute(t, typeof(CoreMenuAttribute));
            if ((v_attr == null)||(string.IsNullOrEmpty (v_attr.Name)))
                return;
            ICoreMenuAction v_menu = 
                t.Assembly.CreateInstance (t.FullName ) as ICoreMenuAction ;
            if (v_menu == null) return;
            Register(v_attr, v_menu);
        }
        internal void Unregister(string name)
        {
            if (this.m_menus.ContainsKey(name))
            {
                var c = this.m_menus[name];
                this.m_menus.Remove(name);
                if (this.m_rootsMenu.Contains(c))
                    this.m_rootsMenu.Remove(c);
                
            }

        }
        private void RemoveSystemRootMenu(string v_parentName)
        {
            do
            {
                if (this.m_menus.ContainsKey(v_parentName))
                {
                    if (this.m_menus[v_parentName] is CoreSystemRootMenu)
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
            }
            while (!string.IsNullOrEmpty(v_parentName));
        }
        private void CopyAndReplaceRoot(CoreMenuAttribute attr,
            CoreSystemRootMenu rootMenu, ICoreMenuAction v_menu)
        {
            //backup child
            ICoreMenuAction[] v_tab = rootMenu.Childs.ToArray();
            rootMenu.Childs.Clear();
            //add child to menu
            v_menu.Childs.AddRange(v_tab);
            this.m_menus[attr.Name] = v_menu;
            //root menu register 
            if (this.m_rootsMenu.Contains(rootMenu))
            {
                this.m_rootsMenu.Remove(rootMenu);
            }
            else {
                //previous item wasn't a root menu.
                //1. get parent 
                string v_parentName = Path.GetFileNameWithoutExtension(v_menu.Id );
                if (v_menu.Id != v_parentName)
                {
                    ///add menu to menu collection
                    if (this.m_menus.ContainsKey(v_parentName))
                    {
                        ICoreMenuAction m_parent = this.m_menus[v_parentName];
                        //remove root from parent
                        m_parent.Childs.Remove(rootMenu);
                        //add the replacement menu
                        m_parent.Childs.Add(v_menu);
                    }
                }
            }
            if (v_menu.Parent == null)
            {
                this.m_rootsMenu.Add(v_menu);
            }
        }
        private void GenerateParent(string parentName)
        {
            string[] v = parentName.Split('.');
            string name =string.Empty ;
            string m_parent = string.Empty;
            for (int i = 0; i < v.Length; i++)
            {
                if (i != 0)
                    name += ".";
                name += v[i];
                if (!this.m_menus.ContainsKey(name))
                {
                    CoreSystemRootMenu rm = new CoreSystemRootMenu();
                    rm.Id = name;
                    rm.CaptionKey = string.Format (CoreConstant.MENU_FORMAT, name);
                    m_menus.Add(name, rm);
                    if (i > 0)
                    {
                            this.m_menus[m_parent].Childs.Add(rm);
                    }
                    else { 
                        // menu is root
                        this.m_rootsMenu.Add(rm);
                    }
                }
                m_parent = name;
            } 
        }
        public ICoreMenuAction[] GetRootMenus()
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
            foreach (ICoreMenuAction m in this.m_rootsMenu)
            {
                m.Childs.Sort();
            }
        }
        private bool m_registering;
        /// <summary>
        /// register menu
        /// </summary>
        /// <param name="attr"></param>
        /// <param name="menu"></param>
        /// <returns></returns>
        public bool Register(CoreMenuAttribute attr, ICoreMenuAction menu)
        {
            ///=====
            lock (this)
            {
                if (m_registering)
                    return false;
                m_registering = true;
                bool v_out = false;
                if (attr.Name == "-")
                {
                    m_registering = false;
                    return v_out;
                }
                CoreSystem.RegisterAction(attr, menu);
                if (menu.IsRootMenu)
                {
                    if (!this.m_menus.ContainsKey(attr.Name))
                    {
                        //add to rootmenu
                        this.m_menus.Add(attr.Name, menu);
                        this.m_rootsMenu.Add(menu);
                    }
                    else
                    {
                        //already contains replace if root menu is system menu
                        var r = this.m_menus[attr.Name];
                        CoreSystemRootMenu v_rootMenu = r
                      as CoreSystemRootMenu;
                        if (v_rootMenu != null)
                        {
                            //remove parent while is root menu
                            string v_parentName = Path.GetFileNameWithoutExtension(attr.Name);
                            RemoveSystemRootMenu(v_parentName);
                            //remplace the root system
                            CopyAndReplaceRoot(attr, v_rootMenu, menu);
                        }
                        else
                        {
                            throw new CoreException(enuExceptionType.OperationNotValid, "ERR.MENUNOTREGISTRATED".R());
                        }
                    }
                    v_out = true;
                }
                else
                {
                    if (!this.m_menus.ContainsKey(attr.Name))
                    {
                        string v_parentName = Path.GetFileNameWithoutExtension(attr.Name);
                        if (v_parentName == attr.Name)
                        {
                            //no parent
                            this.m_menus.Add(attr.Name, menu);
                            this.m_rootsMenu.Add(menu);
                        }
                        else
                        {//possible parent
                            ICoreMenuAction v_p = null;
                            if (this.m_menus.ContainsKey(v_parentName))
                            {
                                //this.Add to parent
                                v_p = this.m_menus[v_parentName];
                                v_p.Childs.Add(menu);
                                this.m_menus.Add(attr.Name, menu);
                            }
                            else
                            {
                                GenerateParent(v_parentName);
                                this.m_menus[v_parentName].Childs.Add(menu);
                                this.m_menus.Add(attr.Name, menu);
                            }
                        }
                    }
                    else
                    {
                        //already register
                        var m = this.m_menus[attr.Name];
                        CoreSystemRootMenu rootMenu = m as CoreSystemRootMenu;
                        if ((rootMenu != null) || m.IsRootMenu)
                        {
                            //copy and replace root
                            CopyAndReplaceRoot(attr, rootMenu, menu);
                        }
                        else
                        {
                            //present main non system
                           // throw new CoreException(enuExceptionType.OperationNotValid,
                           CoreLog.WriteDebug (CoreConstant.ERR_MENUNOTREGISTERED_1.R(attr.Name));
                        }
                    }
                }
                v_out = true;
                m_registering = false;
                return v_out;
            }
        }
        /// <summary>
        /// generate the menu by setting the Workbench
        /// </summary>
        /// <param name="v_mainMenu"></param>
        /// <param name="iCoreWorkBench"></param>
        public void GenerateMenu(
            ICoreMenu v_mainMenu, 
            ICoreWorkbench Workbench
            )
        {
            CoreMenuActionBase v_menu = null;
            this.Sort();
            ICoreMenuAction[] v_tmenus = CoreSystem.GetRootMenus();
            foreach (ICoreMenuAction a in v_tmenus)
            {
                v_menu = a as CoreMenuActionBase;
                if (v_menu != null)
                {
                    if (v_menu.Parent == null)
                    {                      
                        //init default menu
                        v_menu.Workbench = Workbench;
                        v_mainMenu.Add(v_menu);
                    }
                }
            }
        }
        public void ExportMenuAsXML(string filename)
        {
            XmlWriterSettings s = new XmlWriterSettings();
            s.Indent = true;
            XmlWriter xml = XmlWriter.Create(filename, s);
            List<ICoreMenuAction> lmenu = new List<ICoreMenuAction>();
            foreach (var item in this.m_menus)
            {
                lmenu.Add(item.Value );
            }
            lmenu.Sort(new CoreMenuComparer());
            xml.WriteStartElement ("drsMenu");
            xml.WriteAttributeString("count", this.Count.ToString());
            foreach (var item in lmenu )
            {
                xml.WriteStartElement("menu");
                xml.WriteAttributeString("id", item.Id);
                xml.WriteAttributeString("index", item.Index.ToString());
                if (item.IsRootMenu)
                {
                    xml.WriteAttributeString("isroot", "true");
                }
                if (item.CanShow )
                {
                    xml.WriteAttributeString("canshow", "true");
                }
                if (item.ShortCut != enuKeys.None)
                    xml.WriteAttributeString("shortcut", item.ShortCut.ToString());
                xml.WriteAttributeString("type", item.GetType().AssemblyQualifiedName);
                xml.WriteEndElement();
            }
            xml.WriteEndElement ();
            xml.Close();
        }
    }
}

