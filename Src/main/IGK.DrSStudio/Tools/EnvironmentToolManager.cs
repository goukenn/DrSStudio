

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: EnvironmentToolManager.cs
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
file:EnvironmentToolManager.cs
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
using System.Collections;
namespace IGK.DrSStudio.Settings
{
    
    using IGK.ICore.WinCore;
using IGK.ICore.WinUI;
    using IGK.ICore.Tools;
    using IGK.ICore.Drawing2D;
    using System.IO;
    using System.Windows.Forms;
    using IGK.ICore.IO;
    using IGK.ICore.IO.Files;
    using IGK.DrSStudio.Tools;
    using IGK.ICore;
    using IGK.DrSStudio.WinUI;
    [DrSStudioTool("Tool.WinLauncherEnvironmentManagerTool")]
    sealed class WinLauncherEnvironmentManagerTool : DrSStudioToolBase
    {
        const string CONF_FILE = "layout.ini";
        private static WinLauncherEnvironmentManagerTool sm_instance;
        private string m_CurrentEnvironmentName;
        private WinLauncherEnvironmentSettingCollections m_envSettingCollection;
        private WinLauncherEnvironementSelectedPage m_selectedPageCollection;
        /// <summary>
        /// get the current tool environment name
        /// </summary>
        public string CurrentEnvironment
        {
            get { return m_CurrentEnvironmentName; }
            private set
            {
                if (m_CurrentEnvironmentName != value)
                {
                    WinLauncherEnvironmentChangedEventArgs e = new WinLauncherEnvironmentChangedEventArgs (m_CurrentEnvironmentName , value );
                    m_CurrentEnvironmentName = value;
                    OnEnvironmentChanged(e);
                }
            }
        }
        private void OnEnvironmentChanged(WinLauncherEnvironmentChangedEventArgs e)
        {
            this.LayoutManager.SuspendLayout ();
            if (!string.IsNullOrEmpty (e.OldEnvironment ))
            this.m_envSettingCollection.Hide(e.OldEnvironment);
            if (!string.IsNullOrEmpty(e.NewEnvironment))
            {
                this.m_envSettingCollection.Show(e.NewEnvironment);
            }
            this.LayoutManager.ResumeLayout();
        }
        private WinLauncherEnvironmentManagerTool()
        {
            m_envSettingCollection = new WinLauncherEnvironmentSettingCollections(this);
            m_selectedPageCollection =  new WinLauncherEnvironementSelectedPage(this);
        }
        public static WinLauncherEnvironmentManagerTool Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static WinLauncherEnvironmentManagerTool()
        {
            sm_instance = new WinLauncherEnvironmentManagerTool();
        }        
        void SaveSetting()
        {
            string f =Path.Combine ( Application.UserAppDataPath,  CONF_FILE);
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("#DrSStudio Layout Enviromnent Setting");
                foreach (KeyValuePair<string, WinLauncherEnvironmentToolSetting> item in this.m_envSettingCollection)
                {
                    sb.AppendLine(string.Format("[{0}]", item.Key));
                    ICoreTool[] t = item.Value.GetTools();
                    foreach (ICoreTool  tool in t)
                    {
                        sb.AppendLine(string.Format("{0}={1}", tool.Id, this.GetSettingDefinition(tool, item.Key)));
                    }
                }
                File.WriteAllText(f, sb.ToString());
            }
            catch { 
            }
        }
        string GetSettingDefinition(ICoreTool tool, string environmentName)
        {
            IGK.DrSStudio.Tools.ToolManager.WinLauncherToolPanelProperty p = this.LayoutManager.ToolManager.GetPanelProperty (tool, environmentName );
            try
            {
                return string.Join(",",
                    p.ToolDisplay
                    ,""//p.IsDockingHostedControl ? p.Size.ToString() : string.Empty,
                    ,""//p.IsDockingHostedControl ? p.Location.ToString() : string.Empty
                    ,m_selectedPageCollection.IsSelected(p)?"1":""
                    );
            }
            catch
            {
            }
            return string.Empty;
        }

        /// <summary>
        /// load ini file
        /// </summary>
        /// <param name="filename"></param>
        void LoadIni(string filename)
        {
            IniReader r =  IniReader.LoadIni(filename);
            if ((r !=null) && (r.GroupCount > 0))
            {
                string[] t = r.GetKeys();
                foreach (string name in t)
                {
                    this.m_envSettingCollection.Register(name);                    
                    foreach(IniReader.IniValue v in r[name])
                    {
                        ICoreTool v_t = CoreToolBase.GetTool(v.Name );
                        if (v_t != null)
                        {
                            this.m_envSettingCollection.RegisterTool(name, v_t);
                            this.SetSettingDefinition(v_t,name, v.Value);
                        }
                    }
                }              
            }
        }
        private void SetSettingDefinition(ICoreTool tool,string environmentName, string p)
        {
            if (string.IsNullOrEmpty(p))
                return;
            string[] t = p.Split(',');
            IGK.DrSStudio.Tools.ToolManager.WinLauncherToolPanelProperty pan = this.LayoutManager.ToolManager.GetPanelProperty(tool, environmentName);
            for (int i = 0; i < t.Length; i++)
            {
                switch (i)
                {
                    case 0:
                        pan.ToolDisplay = (enuLayoutToolDisplay)Enum.Parse(typeof(enuLayoutToolDisplay), t[i]);
                        break;
                    case 1:
                        pan.Location = Vector2i.ConvertFromString (t[i]);
                        break;
                    case 2:
                        pan.Size = Size2i.ConvertFromString (t[i]);
                        break;
                    default:
                        break;
                }
            }
        }
        protected override void RegisterBenchEvent(ICoreSystemWorkbench workbench)
        {
            base.RegisterBenchEvent(workbench);     
            RegisterLayoutManagerEvent((workbench as ICoreLayoutManagerWorkbench )?.LayoutManager);            
        }


        private void RegisterLayoutManagerEvent(ICoreWorkbenchLayoutManager coreLayoutManager)
        {
            if (coreLayoutManager == null) return;
            DrSStudioLayoutManager l = coreLayoutManager as DrSStudioLayoutManager;
            if (l != null)
            {
                l.LoadingToolComplete += l_LoadingToolComplete;
                l.EnvironmentChanged += _EnvironmentChanged;
                l.ToolAdded += coreLayoutManager_ToolAdded;
                l.ToolRemoved += coreLayoutManager_ToolRemoved;
                l.ToolManager.PanBottom.SelectedPageChanged += c_panBottom_SelectedPageChanged;
                l.ToolManager.PanLeft.SelectedPageChanged += c_panLeft_SelectedPageChanged;
                l.ToolManager.PanRight.SelectedPageChanged += c_panRight_SelectedPageChanged;
            }
            this.CurrentEnvironment = this.LayoutManager.Environment;
        }
        void c_panRight_SelectedPageChanged(object sender, EventArgs e)
        {
        }
        void c_panLeft_SelectedPageChanged(object sender, EventArgs e)
        {
        }
        void c_panBottom_SelectedPageChanged(object sender, EventArgs e)
        {
        }
        void l_LoadingToolComplete(object sender, EventArgs e)
        {
            Application.ApplicationExit += (o, se) => { sm_instance.SaveSetting(); };
            LoadIni(Path.Combine(Application.UserAppDataPath, CONF_FILE));
        }
        private void UnRegisterLayoutManagerEvent(ICoreWorkbenchLayoutManager coreLayoutManager)
        {
            if (coreLayoutManager == null) return;
            DrSStudioLayoutManager l = coreLayoutManager as DrSStudioLayoutManager;
            if (l != null)
            {
                l.LoadingToolComplete -= l_LoadingToolComplete;
                l.EnvironmentChanged -= _EnvironmentChanged;
                l.ToolAdded -= coreLayoutManager_ToolAdded;
                l.ToolRemoved -= coreLayoutManager_ToolRemoved;
            }
        }

        private void coreLayoutManager_ToolRemoved(object sender, CoreItemEventArgs<CoreToolBase> e)
        {
            this.m_envSettingCollection.UnRegisterTool(e.Item );
        }
        private void coreLayoutManager_ToolAdded(object sender, CoreItemEventArgs<CoreToolBase> e)
        {
            this.m_envSettingCollection.RegisterTool(e.Item);
        }
        private void _EnvironmentChanged(object sender, EventArgs e)
        {
            this.CurrentEnvironment = this.LayoutManager.Environment;
        }
        protected override void UnregisterBenchEvent(ICoreSystemWorkbench workbench)
        {
            UnRegisterLayoutManagerEvent((workbench as ICoreLayoutManagerWorkbench)?.LayoutManager);
            base.UnregisterBenchEvent(workbench);
        }
        /// <summary>
        /// get the layout manager
        /// </summary>
        public DrSStudioLayoutManager LayoutManager
        {
            get {
                return (base.Workbench as ICoreLayoutManagerWorkbench)?.LayoutManager as DrSStudioLayoutManager;
            }
        }
        class WinLauncherEnvironmentSettingCollections : IEnumerable 
        {
            private Dictionary<string, WinLauncherEnvironmentToolSetting> m_settings;
            private WinLauncherEnvironmentManagerTool m_tool;
            public WinLauncherEnvironmentSettingCollections(WinLauncherEnvironmentManagerTool tool )
            {
                this.m_tool = tool;
                this.m_settings = new Dictionary<string, WinLauncherEnvironmentToolSetting>();
            }
            public void Register(string name)
            {
                if (!string.IsNullOrEmpty (name ) && !this.m_settings.ContainsKey(name))
                {
                    this.m_settings.Add(name, new WinLauncherEnvironmentToolSetting(name, m_tool.LayoutManager));
                }
            }
            internal void Hide(string p)
            {
                if (this.m_settings.ContainsKey(p))
                {
                    this.m_settings[p].HideAll();
                }
            }
            internal void Show(string p)
            {
                if (this.m_settings.ContainsKey(p))
                {
                    this.m_settings[p].ShowAll();
                }
                else {
                    this.Register(p);
                }
            }
            internal void UnRegisterTool(ICoreTool coreTool)
            {
                string v_en = m_tool.CurrentEnvironment;
                if (!string.IsNullOrEmpty (v_en ))
                    this.Register(v_en);
                if (!string.IsNullOrEmpty (v_en))
                    this.m_settings[m_tool.CurrentEnvironment].UnRegister(coreTool);
            }
            internal void RegisterTool(ICoreTool coreTool)
            {
                this.Register(m_tool.CurrentEnvironment);
                this.m_settings[m_tool.CurrentEnvironment].Register(coreTool);
            }
            public IEnumerator GetEnumerator()
            {
                return this.m_settings.GetEnumerator();
            }
            internal void RegisterTool(string environmentName, ICoreTool Tool)
            {
                this.m_settings[environmentName].Register(Tool);
            }
        }
        class WinLauncherEnvironmentToolSetting
        {
            internal string m_name; //environement name
            private List<ICoreTool> m_tools; //visible tools
            private DrSStudioLayoutManager m_layoutManager;
            public WinLauncherEnvironmentToolSetting(string name, DrSStudioLayoutManager lmanager)
            {
                this.m_name = name;
                this.m_layoutManager = lmanager;
                this.m_tools = new List<ICoreTool>();
            }
            public override string ToString()
            {
                return "Environment : " + m_name;
            }
            public void HideAll()
            {
                IGK.DrSStudio.Tools.ToolManager.WinLauncherToolPanelProperty panProp = null;                
                foreach (ICoreTool  item in this.m_tools)
	            {
                    panProp = m_layoutManager.ToolManager.GetPanelProperty(item,this.m_name );
                    m_layoutManager.ToolManager.HideTool(panProp);//m_layoutManager, panProp.Tool );
	            }
            }
            internal void ShowAll()
            {
                IGK.DrSStudio.Tools.ToolManager.WinLauncherToolPanelProperty panProp = null;
                foreach (ICoreTool item in this.m_tools)
                {
                    panProp = m_layoutManager.ToolManager.GetPanelProperty(item, this.m_name );
                    m_layoutManager.ToolManager.ShowTool(panProp);//m_layoutManager, panProp.Tool );
                }
            }
            internal void UnRegister(ICoreTool coreTool)
            {
                if (this.m_tools.Contains(coreTool))
                {
                    this.m_tools.Remove(coreTool);
                }
            }
            internal void Register(ICoreTool coreTool)
            {
                if (!this.m_tools.Contains(coreTool))
                {
                    this.m_tools.Add (coreTool);
                }
            }
            internal ICoreTool[] GetTools()
            {
                return this.m_tools.ToArray();
            }
        }
        class WinLauncherEnvironmentChangedEventArgs : EventArgs
        {
            private string m_OldEnvironment;
            private string m_NewEnvironment;
            public string NewEnvironment
            {
                get { return m_NewEnvironment; }
            }
            public string OldEnvironment
            {
                get { return m_OldEnvironment; }
            }
            private System.Type m_name;         
            public System.Type name
            {
                get { return m_name; }
                set
                {
                    if (m_name != value)
                    {
                        m_name = value;
                    }
                }
            }
            public WinLauncherEnvironmentChangedEventArgs(string oldenv, string nenv)
            {
                this.m_OldEnvironment = oldenv;
                this.m_NewEnvironment = nenv;
            }
        }
        class WinLauncherEnvironementPanelSetting 
        {
            Dictionary<XDockingPanel, XDockingPage> m_info;
            public WinLauncherEnvironementPanelSetting()
            {
                m_info = new Dictionary<XDockingPanel, XDockingPage>();
            }
            public XDockingPage this[XDockingPanel panel]
            {
                get{
                    if (this.m_info.ContainsKey(panel))
                    {
                        return this.m_info[panel];
                    }
                    return null;
                }
                set {
                    if (this.m_info.ContainsKey(panel))
                    {
                        this.m_info[panel] = value;
                    }
                    else {
                        this.m_info.Add(panel, value);
                    }
                }
            }
        }
        class WinLauncherEnvironementSelectedPage : EventArgs
        {
            Dictionary<string, WinLauncherEnvironementPanelSetting> m_dics;
            private WinLauncherEnvironmentManagerTool winLauncherEnvironmentManagerTool;
            public WinLauncherEnvironementSelectedPage(WinLauncherEnvironmentManagerTool winLauncherEnvironmentManagerTool)
            {
                this.winLauncherEnvironmentManagerTool = winLauncherEnvironmentManagerTool;
                m_dics = new Dictionary<string,WinLauncherEnvironementPanelSetting> ();
            }
            public WinLauncherEnvironementPanelSetting this[string envName]
            {
                get{
                    if (this.m_dics.ContainsKey (envName ))
                    {
                            return this.m_dics[envName];
                    }
                    WinLauncherEnvironementPanelSetting p = new WinLauncherEnvironementPanelSetting ();
                    this.m_dics.Add (envName, p);
                    return p;
                }
            }
            internal bool IsSelected(IGK.DrSStudio.Tools.ToolManager.WinLauncherToolPanelProperty p)
            {
                return false;
            }
        }
    }
}

