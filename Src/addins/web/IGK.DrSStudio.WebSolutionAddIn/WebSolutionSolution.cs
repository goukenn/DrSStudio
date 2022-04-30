

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WebSolutionSolution.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/

using IGK.ICore;
using IGK.ICore.Codec;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.DrSStudio;
using IGK.DrSStudio.Menu;
using IGK.DrSStudio.WinUI;
using IGK.DrSStudio.Drawing2D;
using IGK.DrSStudio.Drawing2D.WinUI;
﻿
using System;
using System.Net;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace IGK.DrSStudio.Web
{
    using IGK.ICore.IO; 
    using IGK.ICore.Xml;
    using System.Configuration;
    using System.Net.Configuration;
    using IGK.DrSStudio.Web.WorkingObjects;
    using IGK.DrSStudio.Web.WinUI;
    using IGK.DrSStudio.TextCodeEditorAddIn.WinUI;    
    /// <summary>
    /// represetn the web solution . 
    /// </summary>
    public class WebSolutionSolution :
        CoreWorkingObjectBase ,
        ICoreWorkingProjectSolution, 
        ICoreSerializable ,
        ICoreDeserializable
    {

        private CoreWorkingSolutionOpenedManager m_manager;
        private string m_Name;
        private String m_FileName; //solution filename
        private string  m_OutFolder;
        private WebSolutionServerManager m_solutionServerManager;

        /// <summary>
        /// get default uri used to preview the web solution
        /// </summary>
        public Uri Uri
        {
            get
            {
                return this.m_solutionServerManager.Uri;
            }
        }


        /// <summary>
        /// Get or set the output folder to deploy locally
        /// </summary>
        public string  OutFolder
        {
            get { return m_OutFolder; }
            set
            {
                if (m_OutFolder != value)
                {
                    m_OutFolder = value;
                }
            }
        }

        public String FileName
        {
            get { return m_FileName; }
        }
        /// <summary>
        /// get or set the name of this solution
        /// </summary>
        public string Name
        {
            get { return m_Name; }
            set
            {
                if (m_Name != value)
                {
                    m_Name = value;
                    OnNameChanged(EventArgs.Empty);
                }
            }
        }
        public event EventHandler NameChanged;
        private WebSolutionItemCollection m_items;
        private string m_ImageKey;


        private WebSolutionDocument m_Document;

        /// <summary>
        /// get the web document
        /// </summary>
        [CoreXMLElement()]
        public WebSolutionDocument Document
        {
            get { 
                return m_Document; 
            }
            internal set {
                this.m_Document = value;
            }
        }
        ///<summary>
        ///raise the NameChanged 
        ///</summary>
        protected virtual void OnNameChanged(EventArgs e)
        {
            if (NameChanged != null)
                NameChanged(this, e);
        }


        private string m_CompanyPrefix;
        [CoreXMLAttribute()]
        [CoreXMLDefaultAttributeValue("igk")]
        public string CompanyPrefix
        {
            get { return m_CompanyPrefix; }
            set
            {
                if (m_CompanyPrefix != value)
                {
                    m_CompanyPrefix = value;
                }
            }
        }

        private string m_Author;

        [CoreXMLAttribute()]
        public string Author
        {
            get { return m_Author; }
            set
            {
                if (m_Author != value)
                {
                    m_Author = value;
                }
            }
        }

        private DateTime m_Create;
        private string m_sessionId;
        /// <summary>
        /// date of the creation
        /// </summary>
        [CoreXMLAttribute()]
        public DateTime Create
        {
            get { return m_Create; }
            set
            {
                if (m_Create != value)
                {
                    m_Create = value;
                }
            }
        }

        

        [CoreXMLElement()]
        public ICoreWorkingProjectSolutionItemCollections Items
        {
            get {
                return this.m_items;
            }
        }

        private WebSolutionSolution(string name, string outputFolder)
        {
            this.m_CompanyPrefix = CoreConstant.IGK_PREFIX;
            this.m_Name = name;
            this.m_OutFolder = outputFolder;
            this.m_manager = new CoreWorkingSolutionOpenedManager(this);
            this.m_solutionServerManager = new WebSolutionServerManager(this);
            this.m_FileName = Path.Combine(outputFolder, name + "." + WebSolutionConstant.EXT);
            this.m_Create = DateTime.Now;
            //init solutio collection
            this.m_Document = new WebSolutionDocument();
            this.m_items = CreateSolutionCollection();
            this.m_items.Add(new WebSolutionPackage("iGKWeb"));
            this.m_items.Add(new WebSolutionPackage("Res"));
            this.m_items.Add(new WebSolutionPackage("pages"));
            this.m_items.Add(new WebSolutionFolder("data"));
            this.m_items.Add(new WebSolutionFolder("Styles"));
            this.m_items.Add(new WebSolutionFolder("Scripts"));
            this.m_items.Add(new WebSolutionFolder("Mods"));
            this.m_items.Add(new WebSolutionFolder("Layouts"));
            this.m_items.Add(new WebSolutionFolder("Pages"));
            this.m_items.Add(new WebSolutionFile("index.php", Path.Combine(this.OutFolder , "index.php")));
        }

        /// <summary>
        /// create solution item
        /// </summary>
        /// <returns></returns>
        protected virtual WebSolutionItemCollection CreateSolutionCollection()
        {
            return new WebSolutionItemCollection(this);
        }
        /// <summary>
        /// represent a solution item collection
        /// </summary>
        public class WebSolutionItemCollection : ICoreWorkingProjectSolutionItemCollections
        {
            private List<WebSolutionItemBase> m_solutionItems;
            private WebSolutionSolution m_owner;

            public WebSolutionItemCollection(WebSolutionSolution webSolutionSolution)
            {
                this.m_owner = webSolutionSolution;
                this.m_solutionItems = new List<WebSolutionItemBase>();
            }

            public int Count
            {
                get { return this.m_solutionItems.Count; }
            }
            /// <summary>
            /// add a item to solution
            /// </summary>
            /// <param name="item"></param>
            public void Add(WebSolutionItem item)
            {
                if (item == null) return;
                this.m_solutionItems.Add(item);
                item.Solution = this.m_owner;
            }
            public void Remove(WebSolutionItem item)
            {
                if (item == null) return;
                this.m_solutionItems.Remove(item);
                item.Solution = null;
            }

            public System.Collections.IEnumerator GetEnumerator()
            {
                return this.m_solutionItems.GetEnumerator();
            }
        }
        /// <summary>
        /// save the web projet file for
        /// </summary>
        /// <param name="filename"></param>
        internal void Save(string filename)
        {
           
            System.Xml.XmlWriterSettings v_setting = new System.Xml.XmlWriterSettings ();

            v_setting.Indent = true;
            v_setting.CloseOutput = true ;
            v_setting.OmitXmlDeclaration = true ;            
            System.Xml.XmlWriter sw = System.Xml.XmlWriter.Create(filename, v_setting);            
            CoreXMLSerializer v_seri = CoreXMLSerializer.Create (sw);
            try
            {
                //serialize the web solution in this.
                v_seri.WriteStartElement("websolution");
                this.WriteAttributes(v_seri);
                this.WriteElements(v_seri);
                v_seri.WriteEndElement();
                v_seri.Flush();
            }
            catch(Exception ex) {
                CoreLog.WriteDebug("Exception : " + ex.Message);
            }
            finally
            {                
                v_seri.Close();
            }
            sw.Close();
        }
        /// <summary>
        /// open a web file solution
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static WebSolutionSolution Open(string filename)
        {
            if (!File.Exists(filename))
                return null;
            FileStream fs = File.Open(filename, FileMode.Open, FileAccess.Read);
            CoreXMLDeserializer v_deseri = null;

            try
            {
                v_deseri = (CoreXMLDeserializer)CoreXMLDeserializer.Create(fs);
                if (v_deseri.ReadToDescendant("websolution"))
                {
                    WebSolutionSolution v_sln = new WebSolutionSolution(Path.GetFileNameWithoutExtension(filename),
                        PathUtils.GetDirectoryName(filename));
                    ((ICoreDeserializable) v_sln).Deserialize(v_deseri);
                    return v_sln;
                }
            }
            catch
            {
            }
            finally
            {
                if (v_deseri !=null)
                v_deseri.Close();
            }
            return null;
        }

        /// <summary>
        /// get or set the image key
        /// </summary>
        public string ImageKey
        {
            get
            {
                return this.m_ImageKey ?? "img_dash";
            }
            set
            {
                this.m_ImageKey = value;
            }
        }

     

        public void Open(ICoreSystemWorkbench workbench, ICoreWorkingProjectSolutionItem item)
        {
            if (item.Solution != this)
            {
                return;
            }
            if (m_manager.Contains (item))
            {
                 workbench.SetCurrentSurface ( m_manager.GetSurface(item));
            }
            else {
                if (item is WebSolutionFile f)
                {
                    WebSolutionItemSurface v_surface = new WebSolutionItemSurface(this,
                        new TCEditorSurface());
                    v_surface.Open(f);
                    workbench.AddSurface(v_surface, true);
                    m_manager.Add(f, v_surface);
                }
            }
        }


        public void SaveAs(string filename)
        {
            this.Save(filename);         
        }

        public void Save()
        {
            string file = this.FileName;
            this.SaveAs(file);
        }


        public IEnumerable GetSolutionToolActions()
        {
            return null;
        }

        public void Build()
        {
            this.Build(this.OutFolder);
        }

        private void Build(string destinationFolder)
        {
            
        }

        /// <summary>
        /// create a web solution
        /// </summary>
        /// <param name="name"></param>
        /// <param name="outputFolder"></param>
        /// <returns></returns>
        public  static WebSolutionSolution CreateSolution(string name, string outputFolder)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(outputFolder))
                return null;
            if (PathUtils.CreateDir(outputFolder) == false)
                return null;

            //extract lib to output folder
            WebSolutionSolution v_sf = new WebSolutionSolution(name,outputFolder);
       
            WebSolutionUtility.ExtractiGKWebFrameWorkTo(outputFolder);
            v_sf.Init();
            v_sf.Save();
            return v_sf;
        }

        private void Init()
        {
            CoreLog.WriteDebug("Init igkweb framework");
            string r = string.Empty;
            r = WebSolutionUtility.SetRequest(this, WebSolutionConstant.INITURI);

            this.ApiInit();
        
            //CoreLog.WriteDebug("Init igkweb default controller");

            //WebSolutionUtility.SetRequest(this, WebSolutionConstant.ADDCTRL_URI);
        }
        internal void SetHeader(HttpWebRequest request)
        {
            try
            {          
                request.KeepAlive = true;
                if (this.m_sessionId != null)
                {
                    var d = new CookieContainer();
                    var h = this.m_sessionId.Split(new char[] { ';', '=' }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < h.Length; i+=2)
                    {
                        d.Add(
                            this.Uri, new Cookie(h[i].Trim(), h[i+1].Trim())
                            );                       
                    }
                    request.CookieContainer = d;
                    
                }
            }
            catch (Exception Exception){
                CoreLog.WriteDebug(Exception.Message);
            }
        }
        internal void GetHeader(HttpWebResponse response)
        { 
            if (string.IsNullOrEmpty (this.m_sessionId ))
            this.m_sessionId = response.Headers.Get("Set-Cookie");
        }
        internal  void ApiInit()
        {
            CoreLog.WriteDebug("API Connect");
           string r = WebSolutionUtility.SetRequest(this, "?c=API&f=beginRequest&u=admin&pwd=admin");

            string q = string.Empty;

            string[] tq = {
                              string.Format ("?c=igkctrlmanager&f=add_ctrl&clName={0}&clCtrlType=DefaultPage&nodefaultarticle=1&clVisiblePages=*", this.CompanyPrefix+"Default"),
                              string.Format ("?c=igkctrlmanager&f=add_ctrl&clName={0}&clCtrlType=View&nodefaultarticle=1&clVisiblePages=*&clDataAdapterName=XML&clParentCtrl={1}", this.CompanyPrefix+"Page",this.CompanyPrefix+"Default"),
                              string.Format ("?c=igkctrlmanager&f=add_ctrl&clName={0}&clCtrlType=View&nodefaultarticle=1&clVisiblePages=*", this.CompanyPrefix+"Header"),                              
                              string.Format ("?c=igkctrlmanager&f=add_ctrl&clName={0}&clCtrlType=View&nodefaultarticle=1&clVisiblePages=*", this.CompanyPrefix+"Content"),
                              string.Format ("?c=igkctrlmanager&f=add_ctrl&clName={0}&clCtrlType=View&nodefaultarticle=1&clVisiblePages=*", this.CompanyPrefix+"Footer"),
                              string.Format ("?c=igkctrlmanager&f=add_ctrl&clName={0}&clCtrlType=View&nodefaultarticle=1&clVisiblePages=*", this.CompanyPrefix+"Default"),
                              string.Format ("?c=igkctrlmanager&f=init_env")
            };
            foreach (string query in tq)
            {
                q = Convert.ToBase64String(UTF8Encoding.Default.GetBytes(query));
                r = WebSolutionUtility.SetRequest(this, "?c=API&f=sendRequest&q=" + q);
            }

            CoreLog.WriteDebug("API close connect");
            r = WebSolutionUtility.SetRequest(this, "?c=API&f=endRequest");
            this.m_sessionId = null;
        }

        public void Close()
        {
            this.m_solutionServerManager.Close();
        }



      


        public ICoreSaveAsInfo GetSolutionSaveAsInfo()
        {
            return new CoreSaveAsInfo(
                "title.SaveWebSolutionAs".R(),
                string.Format("{0}|{1}",
                "WebSolutionsFilterString".R(),
                "*." + WebSolutionConstant.EXT),
                this.FileName);
        }

   
    }

    }

