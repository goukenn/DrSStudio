using IGK.DrSStudio.Settings;
using IGK.DrSStudio.WinLauncher.Tools;
using IGK.DrSStudioTools;
using IGK.ICore;
using IGK.ICore.Drawing2D.WinUI;
using IGK.ICore.Web;
using IGK.ICore.WinCore.WinUI.Controls;
using IGK.ICore.WinUI;
using IGK.ICore.Xml;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace IGK.DrSStudio.WinUI
{

    [ComVisible(true)]
    public class DrSStartScriptProfile : CoreWebScriptObjectBase, 
        IGK.ICore.Web.ICoreWebDialogFileManagerProvider
    {
        private DrSStartSurface c_surface;
        
#if DEBUG
        const string BASEURI = "http://localhost/igkdev/drsstudio";
#else
        const string BASEURI = CoreConstant.WEBSITE+"/drsstudio";
#endif

        public DrSStartScriptProfile(DrSStartSurface drSStartSurface)
        {
            this.c_surface = drSStartSurface;
        }
        public StartDocumentManagerTool Tool
        {
            get { return StartDocumentManagerTool.Instance; }
        }
        /// <summary>
        /// get or set the webbrowser 
        /// </summary>
        public IGKXWebBrowserControl WebBrowser
        {
            get { return this.c_surface.WebBrowser ; }
        }
      
        public void Create(string name = null)
        {
     
            if (string.IsNullOrEmpty(name))
            {
                this.Tool.Workbench.CreateNewProject();
            }
            else {
                this.CreateProject(name, null);
            }

            
        }
        /// <summary>
        /// create new file item
        /// </summary>
        public void CreateNewFile()
        {
            throw new NotImplementedException(nameof(CreateNewFile) + " Not implement");
        }
        /// <summary>
        /// create new Project
        /// </summary>
        public void CreateNewProject()
        {
            throw new NotImplementedException(nameof(CreateNewFile) + " Not implement");
        }
        public string getMainMenu()
        {
            IGK.ICore.Xml.CoreXmlElement c = IGK.ICore.Xml.CoreXmlElement.CreateXmlNode("ul") as IGK.ICore.Xml.CoreXmlElement;          
            c.AddChild(GetScriptMenu("PriseEnMain", "#"));
            c.AddChild(GetScriptMenu("Information", "#"));
            return c.RenderXML(null);
        }

        public string IOGetFileContent(string name) {

            return IGK.ICore.Web.CoreWebUtils.GetFileContent(name);

        }
        private CoreXmlElementBase GetScriptMenu(string p1, string p2)
        {
            var c = CoreXmlElementBase.CreateXmlNode("li") as CoreXmlElement;
            var a = CoreXmlElementBase.CreateXmlNode("a") as CoreXmlElement;
            a["href"] = p2;
            a.Content = p1;
            c.AddChild (a);
            return c;
        }
        public void CreateSurface(string name, string width, string height)
        {
            CoreUnit w = width;
            CoreUnit h = height;
            ICoreWorkingSurface surface = 
                CoreSystem.CreateWorkingObject (name) as ICoreWorkingSurface ;
            if ((surface ==null) || (w == null) ||(h == null)||
                (w.UnitType == enuUnitType.percent) || 
                (h.UnitType == enuUnitType.percent ))
                return ;
            if (surface is ICore2DDrawingSurface )
            {
                (surface as ICore2DDrawingSurface ).CurrentDocument.SetSize (
                   (int)((ICoreUnitPixel) w ).Value , 
                   (int)((ICoreUnitPixel)  h).Value  );
            }
            this.Tool.Workbench.Surfaces.Add(surface);
            this.Tool.Workbench.CurrentSurface = surface;
        }
        public void opensite() {
            Process.Start(CoreConstant.WEBSITE );
        }
        public void openFile(string filename) {
            var f = RecentFileSetting.Instance.Files;
            int i = Int32.Parse(filename);
            string fname  = f[i];
            (CoreSystem.Instance.Workbench as DrSStudioWorkbench).OpenFile (fname);
        }


      
 


        public void CreateTemplate(string templateName)
        {
           
        }
        public void CreateProject(string name, string _params )
        {
            ICoreWorkingSurface surface =
                          CoreSystem.CreateWorkingObject(name) as ICoreWorkingSurface;
            if (surface == null)
                return;
            if (!string.IsNullOrEmpty(_params))
            {
                if (surface is ICoreWorkingProjectManagerSurface)
                {
                    ICoreInitializatorParam p = GetParams(_params);
                    (surface as ICoreWorkingProjectManagerSurface ).SetParam(p);
                }
            }
            this.Tool.Workbench.Surfaces.Add(surface);
            this.Tool.Workbench.CurrentSurface = surface;
        }
        public void CreateCustom2DSurface()
        {
            Custom2DSurfaceObjectCreator c = new Custom2DSurfaceObjectCreator ();
            if (this.Tool.Workbench.ConfigureWorkingObject(c, "title.2DSurfaceobjectCreator".R(), true, Size2i.Empty) == enuDialogResult.OK)
            {
                this.CreateSurface(CoreConstant.DRAWING2D_SURFACE_TYPE, c.Width, c.Height);
            }
        }
        public string GetAdditionalTemplate()
        {
            return null;// "<b>mirage</b>";
        }
        public  string getPageTitle()
        {
            return "IGKDEV - " + CoreConstant.VERSION;
        }
        static ICoreInitializatorParam GetParams(string nstring)
        {
            return new InitializatorParam(nstring);
        }
        public string getProjectContent() {
            StringBuilder sb = new StringBuilder();
            sb.Append("Project");
            return sb.ToString();
        }
        public string getInfoContent()
        {
            StringBuilder sb = new StringBuilder();         
            return sb.ToString();
        }
        internal class InitializatorParam : ICoreInitializatorParam
        {
            Dictionary<string, string> m_params;
            public override string ToString()
            {
                return "InitializaTor: Count: " + m_params.Count + "";
            }
            internal InitializatorParam(string _params)
            {
                //initialize param
                string[] v_str = _params.Split(':', ';');
                m_params = new Dictionary<string,string> ();
                string v_h = string.Empty ;
                for (int i = 0; i < v_str.Length; i+=2)
                {
                    if ((i+ 1)>= v_str.Length )
                        break ;
                    v_h = v_str[i].ToLower ();
                    if (!m_params.ContainsKey(v_h))
                    {
                        m_params.Add(v_h, v_str[i + 1]);
                    }
                    else
                        m_params[v_h] = v_str[i];
                }
            }
            #region ICoreInitiazatorParam Members
            public string this[string key]
            {
                get { return this.m_params[key.ToLower()]; }
            }
            public int Count
            {
                get
                {
                    return this.m_params.Count;
                }
            }
            public bool Contains(string key)
            {
                if (string.IsNullOrEmpty(key)) return false;
                return this.m_params.ContainsKey(key.ToLower ());
            }
            #endregion
            public System.Collections.IEnumerator GetEnumerator()
            {
                return this.m_params.GetEnumerator();
            }
        }

        public string getVersion() {
            return DrSSConstants.GetVersion();
        }
        public string getModelData() { //return model for drawing 2D
            var ul = CoreXmlElement.CreateXmlNode("ul");
            object param = null;
            if (param != null)
            {
                ul.add("li").add("a").setAttribute("href", "#")
                    .setAttribute ("onclick", "javascript: ns_igk.invoke('create', '');")
                    .Content = "Item1";

            }

            return ul.RenderInnerHTML(null);
        }
        public string getModelProject()
        { //return model for drawing 2D
            var ul = CoreXmlElement.CreateXmlNode("ul");
            object param = null;
            if (param != null)
            {
                ul.add("li").add("a").setAttribute("href", "#")
                    .setAttribute("onclick", "javascript: ns_igk.invoke('createproject', '');")
                    .Content = "Item1";
            }
            return ul.RenderInnerHTML(null);
        }
        public string getRecentData() {
            var ul = CoreXmlElement.CreateXmlNode("ul");
            var f = RecentFileSetting.Instance.Files;
            for (int i = 0; i < Math.Min (10,  f.Length) ; i++)
            {
                ul.add("li").add("a").setAttribute("href", "#").
                    setAttribute("onclick", "javascript: ns_igk.invoke('openFile', '"+i+"'); return false;")
                    .Content = Path.GetFileName(f[i]); 
            }            

            return ul.RenderInnerHTML(null);
        }
        public string update() {
            Thread th = new Thread(
            () =>
            {
                string h = IGK.ICore.Web.CoreWebUtils.PostData(BASEURI + "/download/apps", null);
                var f = Path.GetTempFileName();
                File.WriteAllText(f, h);
                System.Diagnostics.Process.Start(f);
            }
            );
            th.Start();
            return null;
        }
        public string getUptoDate() {
            string h = IGK.ICore.Web.CoreWebUtils.PostData(BASEURI + "/checkversion/"+this.getVersion(), null);
            if (h == "1")            
                return "enum.yes".R();
            return "<a href=\"#\" onclick=\"javascript:ns_igk.invoke('update'); \" class=\"igk-btn igk-btn-default\" >"+ 
                "btn.update".R() + 
                "</a>";
        }
        public string getNews() {
            if (!System.Windows.Forms.SystemInformation.Network)
            {
                return null;
            }

            return null;
            
            //string h = IGK.ICore.Web.CoreWebUtils.PostData(BASEURI + "/news/xml", null);
            //if (string.IsNullOrEmpty(h))
            //    return null;
            //var b = CoreXmlElement.CreateFromString(h);
            //if ((b == null) || (b.TagName != "drsstudio-news"))
            //    return null;

            //var ul = CoreXmlElement.CreateXmlNode("div");
            //ul.LoadString(b.RenderInnerHTML(null));
            
            //return ul.RenderXML(null);
        }
    }
}
