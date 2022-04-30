using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IGK.ICore.WinUI;
using IGK.ICore.WinCore.WinUI.Controls;
using IGK.ICore;
using IGK.ICore.Xml;
using IGK.ICore.Resources;
using IGK.ICore.Web;
using IGK.ICore.IO;
using IGK.WebGLLib.WinUI;
using IGK.WebGLLib;

namespace IGK.DrSStudio.WebGLEngine.WinUI
{
    [CoreSurface("{CC13EB76-BDC9-46CB-9BAC-C82A94AA20BC}")]
    public class WebGLDesignSurface : 
        IGKXWinCoreWorkingSurface, 
        IWebGLDesignSurface,
        ICore.Web.ICoreWebReloadDocumentListener
    {
        IGKXWebBrowserControl c_webbrowser;
        IWebGLGameListener c_listener;


        public void SetGameListener(IWebGLGameListener gameListener) {

            this.c_listener = gameListener;
        }

        internal object  EvalCall(string v)
        {
            return this.c_webbrowser.InvokeScript(v);
        }

        private CoreXmlWebDocument m_document;
        private WebGLGameDesignScene m_Scene;
        private WebGLGameDesingProject m_Project;

        public WebGLGameDesingProject Project { get => m_Project; }
        public WebGLGameDesignScene CurrentScene { get => m_Scene; set { if (m_Scene != value) {
                    this.m_Scene = value;
                    OnCurrentSceneChanged(EventArgs.Empty);
                } } }

        public event EventHandler CurrentSceneChanged;

        protected virtual void OnCurrentSceneChanged(EventArgs e)
        {
            this.CurrentSceneChanged?.Invoke(this, e);
        }


        ///<summary>
        ///public .ctr
        ///</summary>
        public WebGLDesignSurface()
        {
            c_webbrowser = new IGKXWebBrowserControl()
            {
                Dock = System.Windows.Forms.DockStyle.Fill
            };
            this.Controls.Add(c_webbrowser);

            m_document = CoreXmlWebDocument.CreateICoreDocument();
            this.c_webbrowser.ObjectForScripting = new WebGLDesignScriptProxy(this);
            this.c_webbrowser.SetReloadDocumentListener(this);
            this.m_Project = new WebGLGameDesingProject();
            this.m_Scene = new WebGLGameDesignScene() {
                Name = WebGLGameConstant.DEFAULT_SCENE_NAME
            };


            this.m_Project.Scenes.Add(this.m_Scene);
        }


        public override string Title { get => this.m_Scene.Name; protected set => this.m_Scene.Name = value; }
        IWebGLGameScene IWebGLDesignSurface.CurrentScene { get => this.CurrentScene; set { this.CurrentScene = value as WebGLGameDesignScene; } }

        public void ReloadDocument(CoreXmlWebDocument document, bool attachDocument)
        {
            this.c_webbrowser.AttachDocument(this.m_document);
        }

        protected override void CreateHandle()
        {
            base.CreateHandle();
            InitDesignDocument();
        }
        protected override void DestroyHandle()
        {
            base.DestroyHandle();
        }
        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
        }
        void InitDesignDocument()
        {
          

            m_document.Body.ClearChilds();
            StringBuilder vertext = new StringBuilder();
            StringBuilder fragment = new StringBuilder();
            CoreXmlElement sc = null;
            StringBuilder sb = new StringBuilder();
            //BGE Lib
            //load platform script 
            CoreResources.LoadEmbededResources(this.GetType(), (inf) => {
            string v_ext = IGK.ICore.IO.PathUtils.GetExtension(inf.Name)?.ToLower();
                switch (v_ext) {
                    case "vs":
                    case "fs":
                        string K= PathUtils.GetExtension(inf.Name.Substring(0, inf.Name.Length - 3));
                        string N = "VS";
                        string g = WebGLUtils.GetShaderTreatScript(inf.GetString());
                        if (v_ext == "fs")
                            N = "FS";
                        vertext.AppendLine($"igk.bge.shaders.{K}{N} =\""+g+"\";");                            
                        break;
                    case "js":
                          if (inf.Name.Contains("JS.bge.")) {
                            CoreLog.WriteDebug("loading " + inf.Name);
                            sc = m_document.Body.Add("script");
                            sc["type"] = "text/javascript";
                            sc.Content = inf.GetString();
                            sb.AppendLine(inf.GetString());
                          }
                        break;
                }
            });


            //load Shaders scripts form program
            sc = m_document.Body.Add("script");
            sc["type"] = "text/javascript";
            sc.Content = vertext.ToString();


            var style = m_document.Body.Add("style");
            style["type"] = "text/css";
            style.Content = CoreWebUtils.EvalCssWebStringExpression(
                CoreResources.GetResourceString(WebGLGameResources.WEBGL_CSS), 
                new WebGLDesignSceneSetting(this)
                );

            var bbox = m_document.Body.SetAttribute("class", "igk-body game-app").Add("div").SetAttribute("class", "igk-bodybox fit no-overflow");
            var n = bbox.Add("div").SetAttribute("class", "fit").Add("div");
		    n["class"] = "igk-webgl-game-surface fit";
            //n["igk-webgl-game-attr-listener"] = "DrSStudio.WebGLGameBuilder.Debug";


            bbox.addDiv().setId("debug").setClass("posfix ").Content = "Info";
      

            this.c_listener?.InitGame(this.m_document, n);
            this.c_webbrowser.AttachDocument(this.m_document);


#if DEBUG
            System.IO.File.WriteAllText(CoreConstant.DebugTempFolder+"\\out.html", m_document.RenderXML(null));
#endif
        }


        /// <summary>
        /// add new scene
        /// </summary>
        public void AddNewScene()
        {
            this.m_Project.Scenes.Add(new WebGLGameDesignScene() {
                Name = string.Format(WebGLGameConstant.DEFAULT_SCENE_FROMAT_NAME, this.m_Project.Scenes.Count)
            });
        }   
    }
}
