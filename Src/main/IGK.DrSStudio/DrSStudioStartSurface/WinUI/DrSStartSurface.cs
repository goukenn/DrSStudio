

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: DrSStartSurface.cs
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
file:DrSStartSurface.cs
*/
using System;
using System.IO;
using System.Windows.Forms;
using IGK.ICore.WinCore.WinUI.Controls;
using IGK.ICore.Xml;

namespace IGK.DrSStudio.WinUI
{
    using IGK.ICore.WinCore.Web;
    using IGK.ICore;
    using IGK.ICore.Web;
    using IGK.ICore.IO;
    using IGK.ICore.WinUI;
    using IGK.ICore.Resources;


    [CoreSurface ("StartPageSurface",EnvironmentName= "StartPage") ]
    public class DrSStartSurface : IGKXWinCoreWorkingSurface,
        ICoreWorkingReloadViewSurface,
        ICoreWebReloadDocumentListener 
    {
        IGKXWebBrowserControl c_webBrowser;

        public override string Title
        {
            get
            {
                return R.ngets("title.startSurfacePage");
            }
            protected set
            {
                base.Title = value;
            }
        }
        public IGKXWebBrowserControl WebBrowser
        {
            get {
                return c_webBrowser;
            }
        }
        public DrSStartSurface()
        {
            this.InitializeComponent();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            //init custom properties
            this.c_webBrowser.AllowNavigation = false;
            this.c_webBrowser.ObjectForScripting = new DrSStartScriptProfile(this);
       
            this.BuildDocument();
         //   Reload();
        }
        private void BuildDocument() {
            var doc = CoreXmlWebDocument.CreateICoreDocument();
            doc.InitDocument();
            //doc.addScript(PathUtils.GetPath("%startup%/Doc/Scripts/drs.js"));
            var link = doc.Head.add("link");
            link["type"] = "text/css";
            link["rel"] = "stylesheet";
            link["href"] = CoreResources.GetResourceString("resources/drs_startpage.css")?.ToBase64Html("text/css");

          

#if !DEBUG
            var s = CoreResources.GetResourceString("resources/drs_startpage.html");
#else
            string file = CoreConstant.DRS_SRC+"/main/IGK.DrSStudio/Resources/drs_startpage.html";            
            var s = File.Exists(file) ? File.ReadAllText(file) : CoreResources.GetResourceString("resources/drs_startpage.html");
#endif

            if (s != null)
            {
               s = s.EvalWebStringExpression(this.c_webBrowser.ObjectForScripting);
                doc.Body.LoadString(s);
            }
            doc.ForWebBrowserDocument = true;
            var v_options = new CoreXmlWebOptions();
            if (this.c_webBrowser.Host is WebBrowser i)
                doc.AttachToWebbrowser(i, false);
            else {
                v_options.InlineData = true;
                this.c_webBrowser.SetDocumentString(doc.RenderXML(v_options));
            }

//#if DEBUG
    System.IO.File.WriteAllText(CoreConstant.DebugTempFolder+"\\outputdrs.html", doc.RenderXML(v_options));
//#endif
        }

    

        public void Reload()
        {
            this.WebBrowser.Reset();// = new WebBrowser ();//.Refresh(WebBrowserRefreshOption.Normal);
            this.BuildDocument();
            #region "comment"
            /*
            /*CoreXmlWebDocument doc = CoreXmlWebDocument.CreateICoreDocument();
            doc.addScript(PathUtils.GetPath("%startup%/Doc/Scripts/drs.js"));
            doc.Body["style"] = "margin:0px; padding:0px; overflow:hidden;";
            var rdiv = doc.Body.addDiv().setClass ("fitw fith overflow-y-a igk-powered-viewer");
            var d = rdiv.addDiv().addDiv().setClass ("igk-container");


            var v_table  = rdiv.addDiv().addDiv().setClass("disptable");

            rdiv = rdiv.addDiv().setAttribute ("style", "padding-left: 10px; padding-right: 10px;").addDiv().setClass ("igk-row fith");            
            d = rdiv.addDiv().setClass("igk-col-lg-4-1").addDiv();
            d.setClass ("igk-title-4").Content = "DrSStudio &copy; 2008-2014";
            d = rdiv.addDiv().setClass("igk-col-lg-4-3");
            d.addDiv().addA("http://www.igkdev.com/drsstudio").Content = "http://www.igkdev.com/drsstudio";

            rdiv = doc.Body.addDiv();
            rdiv.setClass("posfix posb posl").addA ("drs.showat_startup").Content = "Show at startup";


            doc.ForWebBrowserDocument = true;
            doc.AttachToWebbrowser(this.c_webBrowser);*/
           /* CoreXmlWebDocument doc = CoreXmlWebDocument.CreateICoreDocument();
            doc.AddScript(PathUtils.GetPath("%startup%/Sdk/Scripts/drs.js"));
            doc.AddLink(PathUtils.GetPath("%startup%/Sdk/lib/bootstrap/css/bootstrap.min.css"));

            doc.Body.AppendScript(PathUtils.GetPath("%startup%/Sdk/lib/jquery/jquery.min.js"));
            doc.Body.AppendScript(PathUtils.GetPath("%startup%/Sdk/lib/bootstrap/js/bootstrap.min.js"));


            var rdiv = doc.Body.addDiv().setClass("fitw fith overflow-y-a igk-powered-viewer");
            rdiv["igk-node-disable-selection"] = "true";

            //var d = rdiv.addDiv().addDiv().setClass("igk-container");

            var header = rdiv.addDiv();
            header.setClass("dispb posab fitw")
                .setAttribute("style", "height: 46px; background-image:url('file://" + PathUtils.GetPath("%startup%/sdk/img/bg.jpg").toUri() + "');");
            var ul = header.addDiv().setClass("floatr fith")
                .add("ul")
                .setAttribute("style", "")
                .setClass("igk-horizontal-menu fith");
            var m = new { key = "DevCenter", uri = "devcenter" };
            var tab = new[] { 
                new {key="DevCenter", uri="devcenter"},
                new {key="Support", uri="support"},
                new {key="Forum", uri="forum"},
                new {key="Login", uri="login"}
            };
            foreach (var i in tab)
            {
                // m = i as AnonymousType#1;
                //  var h = i as ({key , uri});
                var a = ul.add("li")
                     .setClass(" fith alignm")
                     .setAttribute("style", "")
                     .addA("javascript: ns_igk.invoke('openlink', 'http://www.igkdev.com/drsstudio/" + i.uri + "'); return false;");
                a.setClass("dispb fitw fith ");
                a.addDiv().setClass("dispib fith no-repeat")
                     .setAttribute("style", "background-image:url('file://" + PathUtils.GetPath("%startup%/sdk/img/barner_" + i.uri + ".png").toUri() + "');  width: 24px;").Content = " ";
                a.addDiv().setClass("dispib fith no-decoration")
                   .setAttribute("style", "color:white; vertical-align:top;")
                   .Content = ("lb." + i.key).R();//"iLogin";        
            }
            header.addDiv().setClass("disptable clearb").Content = " ";

            var v_table = rdiv.addDiv().setClass("disptable fitw fith");
            v_table["style"] = "padding-top:48px; padding-bottom:50px;margin-bottom:-64px;";
            var cell1 = v_table.addDiv().setClass("disptabc alignt");
            cell1["style"] = "min-width:256px; width: 512px; background-color: #aaa;";
            var cell2 = v_table.addDiv().setClass("disptabc");
            cell2["style"] = "width:100%; background-color: #eee; position:relative; text-align:center; vertical-align:top;";

            var rrdiv = rdiv.addDiv().setAttribute("style", "padding-left: 10px; padding-right: 10px;").addDiv().setClass("igk-row fith");
            var d = cell1.addDiv();
            d.setClass("igk-title-4").setAttribute("style", "color:#eee;").Content = "DrSStudio <span class=\"igk-xsmaller alignt\">&copy;</span>";
            d = cell2.addDiv();
            d.addDiv()
                .setClass("posfix loc_b loc_r igk-smaller")
                .Content = "powered by <a href=\"#\" onfocus=\"javascript: this.blur();\" onclick=\"javascript: ns_igk.invoke('openlink', 'http://www.igkdev.com/drsstudio'); this.blur(); return false;\" >IGKDEV</a>";

            rrdiv = cell1.addDiv();
            rrdiv["style"] = "padding-left:16px; padding-right:16px;";
            rrdiv.setClass("posab loc_b loc_l").addInput("clCheckStartup", "checkbox")
            .setAttribute("checked", true.ToString())
            .setAttribute("onchange", "javascript: ns_igk.invoke('showat_startup'); return false;").Content = "Show at startup";


            //buildcell 2
            var row = cell2.addDiv().setClass("dispb igk-black-border alignt");
            //row["style"] = "top:0px; bottom:-100px;";
            //row.Content = "dkjsdf ...";
            //cell2.addDiv().Content = "d";
            row.addDiv().setClass("dispib floatl fitw-2 fith");
            //row.addDiv().setClass("dispib floatl fitw-2 fith").add("iframe")
            //    .setClass("no-border fitw fith")
            //    .setAttribute("src", "http://www.igkdev.com/drsstudio/projecttemplate");

            row.addDiv().setClass("disptable clearb").Content = " ";

            doc.ForWebBrowserDocument = true;
            doc.AttachToWebbrowser(this.c_webBrowser);
            * */
#endregion
        }
        private void InitializeComponent()
        {
            this.c_webBrowser = new IGKXWebBrowserControl();
            this.SuspendLayout();
            // 
            // c_webBrowser
            // 
            this.c_webBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c_webBrowser.Location = new System.Drawing.Point(0, 0);
            this.c_webBrowser.Name = "c_webBrowser";
            this.c_webBrowser.Size = new System.Drawing.Size(507, 305);
            this.c_webBrowser.TabIndex = 0;
            
            // 
            // DrSStartSurface
            // 
            this.Controls.Add(this.c_webBrowser);
            this.Name = "DrSStartSurface";
            this.Size = new System.Drawing.Size(507, 305);
            this.ResumeLayout(false);
        }

        public void ReloadDocument(CoreXmlWebDocument document, bool attach)
        {
           //reload the document
        }
    }
}

