using IGK.ICore.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace IGK.Management.WinUI
{
    using IGK.ICore;
    using IGK.ICore.Xml;
    using System.Management;

    [ComVisible(true)]
    public  class WMIScriptProfile : CoreWebScriptObjectBase
    {
        private WMIEditorSurface m_wMIEditorSurface;
        private string m_SelectedClass;
        private ManagementClass m_class;
        private string m_currentQuery;

        public string SelectedClass
        {
            get { return m_SelectedClass; }
            set
            {
                if (m_SelectedClass != value)
                {
                    m_SelectedClass = value;
                }
            }
        }
        private void openClass() {
            var scope = new System.Management.ManagementScope("\\\\.\\root\\cimv2");
            var cp = new System.Management.ManagementClass();
            cp.Path = new
                // ManagementPath("__SystemClass");
           System.Management.ManagementPath(this.SelectedClass);
            this.m_class = cp;
        }
        public WMIScriptProfile(WMIEditorSurface owner)
        {
            
            this.m_wMIEditorSurface = owner;
            this.m_SelectedClass =  WMIContants.PRIMARY_CLASS;
            //"Win32_LogicalDisk";//
            this.m_SelectedClass = "CIM_videoSetting";
            this.m_currentQuery = "";
            this.openClass();
        }
        public void getView() { 
        }
        public string getbodycontent() {
            this.m_wMIEditorSurface.ReloadDocument(this.m_wMIEditorSurface.Document, false);
            return this.m_wMIEditorSurface.Document.Body.RenderInnerHTML(null);
        }
        public void setClass(string cl) {
            if (!string.IsNullOrEmpty(cl))
            {
                this.m_SelectedClass = cl;
                //this.m_SelectedClass = WMIContants.PRIMARY_CLASS;
                this.openClass();
                this.InvokeScript("ns_igk.wmi.reloadview()");
            }
        }
        public string getClassProperty() {
            var dv = IGK.ICore.Xml.CoreXmlElement.CreateXmlNode("div");
            var cp = this.m_class;

            if (cp.Properties.Count == 0)
            {
                dv.Add("li").setContent("msg.noproperties".R());
            }
            else
            {
                foreach (var ts in cp.Properties)
                {
                    dv.Add("li").setContent(ts.Name);
                }
            }
            return dv.RenderXML(new IGK.ICore.Xml.CoreXmlWebOptions());
        }
        public string getClass() {
            var dv = IGK.ICore.Xml.CoreXmlElement.CreateXmlNode("div");
            var sl = dv.add("select");
            sl.setClass("igk-form-control");
            sl.setId("class_name");
            sl.setAttribute("onchange", "javascript:ns_igk.ext.call('setClass', this.value); return false;");
            foreach (var s in WMIUtility.GetAllManagementClass()) {
                var op = sl.add("option");
                if (this.m_SelectedClass == s) {
                    op.setAttribute("selected", "true");
                }
                op.setAttribute("value", s);
                op.setContent(s);
            }
             
            return dv.RenderXML(new IGK.ICore.Xml.CoreXmlWebOptions());
        }
        public string getMethods()
        {
            var dv = IGK.ICore.Xml.CoreXmlElement.CreateXmlNode("div");
         
            var cp = this.m_class;
            //ManagementPath("win32_logicaldisk");
            foreach (var ts in cp.Methods)
            {
                dv.Add("li").setContent(ts.Name);
            }
            return dv.RenderXML(new IGK.ICore.Xml.CoreXmlWebOptions());
        }
        public string getRelated()
        {
            var dv = IGK.ICore.Xml.CoreXmlElement.CreateXmlNode("div");            
            var cp = this.m_class;
            //ManagementPath("win32_logicaldisk");
            foreach (var ts in cp.GetRelated())
            {
                dv.Add("li").setContent(ts.ToString());
            }
            return dv.RenderXML(new IGK.ICore.Xml.CoreXmlWebOptions());
        }
        public string getSubclasses()
        {
            var dv = IGK.ICore.Xml.CoreXmlElement.CreateXmlNode("div");
            var cp = this.m_class;
            //ManagementPath("win32_logicaldisk");
            foreach (var ts in cp.GetSubclasses())
            {
                dv.Add("li").addA("javascript: ns_igk.ext.call('setClass', '" + ts.ClassPath.ClassName + "'); return false;").setContent(ts.ClassPath.ClassName.ToString());
            }
            return dv.RenderXML(new IGK.ICore.Xml.CoreXmlWebOptions());
        }
       
        public string getDerivations()
        {
            var dv = IGK.ICore.Xml.CoreXmlElement.CreateXmlNode("div");
            var cp = this.m_class;
            //ManagementPath("win32_logicaldisk");
            foreach (var ts in cp.Derivation)
            {
                dv.Add("li").addA("javascript: ns_igk.ext.call('setClass', '" + ts + "'); return false;").setContent(ts.ToString());
            }
            return dv.RenderXML(new IGK.ICore.Xml.CoreXmlWebOptions());
        }
        private CoreXmlWebOptions getOptions() {          
                return new IGK.ICore.Xml.CoreXmlWebOptions();            
        }
        public string invoke_method() { 
            return string.Empty;
        }
        public string getCurrentQuery() {
            return this.m_currentQuery;
        }
        public string getInstances() {
            StringBuilder sb = new StringBuilder();

            var dv = IGK.ICore.Xml.CoreXmlElement.CreateXmlNode("div");
            var cp = this.m_class;
            //ManagementPath("win32_logicaldisk");
            var tab = dv.add("table");
            tab["class"] = "igk-table";
            List<string> properties = new List<string>();
            if (cp.Properties.Count == 0)
            {
                dv.Add("li").setContent("msg.noproperties".R());
            }
            else
            {

                var tr = tab.add("tr");
                foreach (var ts in cp.Properties)
                {
                    properties.Add(ts.Name);
                    tr.add("td").Content = ts.Name;
                }

                foreach (var ts in cp.GetInstances())
                {
                    tr = tab.add("tr");
                    foreach (var item in properties)
                    {
                        tr.Add("td").Content = ts[item];
                    }
                    
                }
            }
            return dv.RenderXML(new IGK.ICore.Xml.CoreXmlWebOptions());
        }
        public string getActions() {
            var cp = this.m_class;
            //ManagementPath("win32_logicaldisk");
            var dv = IGK.ICore.Xml.CoreXmlElement.CreateXmlNode("div");
            dv.setClass("igk-action-bar");
            dv.AddInput("", "button", "btn.instanciated".R())
                .setAttribute("onclick", "javascript: ns_igk.wmi.instanciate(); return false")
                .setClass ("igk-btn igk-btn-default");
//            <div id="action-bar" class="igk-action-bar igk-pull-right posfix loc_b loc_l loc_r dispb no-margin" igk-js-fix-loc-scroll-width="1" style="z-index:10"  >
//<span class="sr-only">Tables actions</span>
//<input type="button" value="[lang:btn.addtable]" class="igk-btn igk-btn-default" onclick="javascript: ns_igk.ext.call('addTable'); return false; "/>
//<input type="button" value="[lang:btn.cleartable]" class="igk-btn igk-btn-default" onclick="javascript: ns_igk.ext.call('clearTables'); return false; " />
//<input type="button" value="[lang:btn.save]" class="igk-btn igk-btn-default" onclick="javascript: ns_igk.ext.call('saveSchemas'); return false; " />

//<input type="button" value="[lang:btn.open]" class="igk-btn igk-btn-default" onclick="javascript: ns_igk.ext.call('openSchemas'); return false; " />
//<input type="button" value="[lang:btn.import]" class="igk-btn igk-btn-default" onclick="javascript: ns_igk.ext.call('importSchemas'); return false; " />

//<input type="button" value="[lang:btn.exportphp]" class="igk-btn igk-btn-default" onclick="javascript: ns_igk.ext.call('exportToPhp'); return false; " />
//<input type="button" value="[lang:btn.exportcs]" class="igk-btn igk-btn-default" onclick="javascript: ns_igk.ext.call('exportToCS'); return false; " />
//</div>
//<div igk-js-fix-height="#action-bar" style="border:1px solid black; visibility:hidden; " > </div>
//</div>

            //dv.Add("li").addA("javascript: ns_igk.ext.call('setClass', '" + ts + "'); return false;").setContent(ts.ToString());
            
            return dv.RenderXML(new IGK.ICore.Xml.CoreXmlWebOptions());

        }
    }
}
