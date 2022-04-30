using IGK.ICore.Web;
using IGK.ICore.Web.WinUI;
using IGK.ICore.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

#pragma warning disable

namespace IGK.DrSStudio.Balafon.DataBaseBuilder.WinUI
{
    using IGK.ICore;
    using IGK.DrSStudio.Balafon.DataBaseBuilder.XML;
    using System.IO;
    using System.Windows.Forms;
    using IGK.ICore.IO;
    using IGK.ICore.WinUI.Common;
    using IGK.ICore.Resources;
    using IGK.ICore.WebSdk;
    /// <summary>
    /// class used for scripting
    /// </summary>
    [ComVisible(true)]
    public class BalafonDBBScriptViewerProfile : 
        CoreWebScriptObjectBase, 
        ICoreWebDialogProvider,
        ICoreWebDialogFileManagerProvider
    {

        private BalafonDBBSchemaDocument m_documentSchema;
        private string m_TableName;//get the selected table
        private string m_viewentry;
        private Utility m_utility;
        //private BalafonDBBSchemaDocument balafonDBBSchemaDocument;
        private BalafonDBBSurface balafonDBBSurface;
        /// <summary>
        /// get or set the current table name
        /// </summary>
        public string TableName
        {
            get { return m_TableName; }
            set
            {
                if (m_TableName != value)
                {
                    m_TableName = value;
                }
            }
        }
        public string getTableName()
        {            
            return this.TableName;
        }
        public string getTableDescription() {
            if (string.IsNullOrEmpty (this.TableName) == false )
            {
                var table = this.m_documentSchema.GetTableDefinition(this.m_TableName);
                if (table !=null)
                return table["Description"];
            }
            return null;
        }
        public BalafonDBBSchemaDocument DocumentSchema { get { return this.m_documentSchema; } }

        internal BalafonDBBScriptViewerProfile(BalafonDBBSchemaDocument document, BalafonDBBSurface balafonDBBSurface)
        {
            this.m_documentSchema = document;
            this.m_viewentry = "definition";
            this.m_utility = new Utility(this);
            this.balafonDBBSurface = balafonDBBSurface;
        }


        public ICoreWebScriptObject OjectForScripting
        {
            get { return this; }
        }
		
		public string getCustomTableLink(){		
		
			string s = CoreResources.GetResourceString(                 
               string.Format (BalafonDBBConstant.RES_DATA_1 ,
               "balafon_tablelink.html"), BalafonDBBConstant.BLFADDIN_FOLDER,
               GetType().Assembly 
            );
            if (s == null)
                return null;
			var d = CoreCommonDialogUtility.BuildWebDialog(
			  CoreSystem.Instance.Workbench,
			  "title.balafon.customLinkTable".R(), CoreWebUtils.EvalWebStringExpression(s, this));
			if (d != null)
			{
				return d.ToString();
			}
			return string.Empty;
		}
        public string getTables()
        {
            var ul = CoreXmlElement.CreateXmlNode("ul");
        
            foreach (CoreXmlElement b in this.DocumentSchema.getElementsByTagName(BalafonDBBConstant.DATADEFINITION_TAG))
            {
               var li =  ul.add("li");
               li.Tag = b["TableName"].ToString();

                   var a = li.setClass("fith fitw")                 
                   .add("a")
                   .setAttribute("href", "#")
                   .setClass ("dispb fitw fith")
                   .setAttribute("onclick",
                    string.Format("javascript: ns_igk.ext.call('setTable', '{0}');return false;", b["TableName"]));
               
               var d = a.addDiv().setClass("igk-list-item igk-roll-owner igk-pull-right fitw");
               var lnk = d.addDiv().setClass("igk-roll-in igk-btn igk-btn-default dispib igk-darkbg-40")
                   .setAttribute("style", "width:32px; padding:0px; marging:0px;")
                   .setAttribute("onclick",
                   string.Format("javascript: ns_igk.ext.call('rmTable', '{0}');  event.stopPropagation(); return false;", b["TableName"]))
                   ;

               lnk
                   .addDiv().setClass("disptable fitw fith")
                   .addDiv().setClass("disptabc alignm alignc")
                   //.Content = "drop";
                   .add("img")
                   .setAttribute("src",
                   CoreWebUtils.GetImgUri("drop")
                   );

                   //addInput("del_btn", "checkbox");
               d.addDiv().Content = b["TableName"];
               
               // a.Content  = b["TableName"];
            }
            ul.Childs.Sort(CompareTag);
            return ul.RenderXML(new CoreXmlWebOptions());
        }
        private int CompareTag(CoreXmlElementBase a, CoreXmlElementBase b) {
            if (a.Tag is string g)
            {
                return g.CompareTo(b.Tag);
            }
            return 0;// ((string)a.Tag).CompareTo(b.Tag);
        }
        public void rmTable(string table)        
        {
            var c = this.m_utility.GetDataTableEntry(table);
            if (c != null) {
                c.Parent.Remove(c);
            }
            var t = this.m_documentSchema.GetTableDefinition(table);
            if (t != null)
            {
                t.Parent.Remove(t);
            }
            this.ReloadView();
            //this.Reload();
        }
        public string getViewInfo()
        {
            var ul = CoreXmlElement.CreateXmlNode("div");
            string e = this.m_viewentry;
            var m = GetType().GetMethod("viewInfo" + e,
                System.Reflection.BindingFlags.NonPublic |
                System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.IgnoreCase);
            if (m != null)
            {
              m.Invoke(this, new object[1] { ul });
            }
            else
            {
                ul.add("div").setClass("igk-panel").Content = "msg.NoDefinitionFor_1".R();
            }
            string v_out = ul.RenderXML(new CoreXmlWebOptions());
            return v_out; // ul.RenderXML(new CoreXmlWebOptions());
        }
        private void viewInfoEntries(CoreXmlElement e)
        {
            bool error = false;
            if (string.IsNullOrEmpty(this.m_TableName))
            {
                error = true;
            }

            var table = this.m_documentSchema.GetTableDefinition(this.m_TableName);
            if (table == null)
            {
                error = true;
            }
            if (error)
            {
                e.addDiv().setClass("igk-panel igk-panel-error")
                       .setContent("e.balafon.NoEntriesFound".R());
                return;
            }
            var v_ett = e.addDiv();
            var v_frm = v_ett.addForm();
#if DEBUG

            string source = CoreResources.GetResourceString(
               string.Format(BalafonDBBConstant.RES_DATA_1,
               "balafon_view_entries.js"), BalafonDBBConstant.BLFADDIN_FOLDER,
               GetType().Assembly
            );



            //string file = BalafonDBBConstant.BLFADDIN_FOLDER+"\balafon_view_entries.js";
            //string source = string.Empty;
            //if (File.Exists(file))
            //{
            //    source = CoreFileUtils.ReadAllFile(file);
            //}
            //else {
            //    source = CoreResources.GetResourceString(
            //    string.Format(BalafonDBBConstant.RES_DATA_1, "balafon_view_entries.js"),
            //     GetType().Assembly);
            //}

            v_frm.add("script").Content = source;

                //CoreFileUtils.ReadAllFile(CoreConstant.DRS_SRC + @"\addins\web\IGK.DrSStudio.BalafonAddIn\DataBaseBuilder\Resources\balafon_view_entries.js") ??
                //CoreResources.GetResourceString(
                //string.Format(BalafonDBBConstant.RES_DATA_1, "balafon_view_entries.js"),
                // GetType().Assembly);
#else
            v_frm.add("script").Content = CoreResources.GetResourceString(
                string.Format (BalafonDBBConstant.RES_DATA_1, "balafon_view_entries.js"),
                 GetType().Assembly);
#endif

            var v_et = v_frm.addDiv().setClass("overflow-x-a").addTable();
            CoreXmlElement tr = null;
            //load header columns
            tr = v_et.add("tr");
            var ts = table.GetColumnKeys();
            tr.add("th").AddInput("", "checkbox")
              .setAttribute("value", "togglevalue")
              .setClass("box_16x16")
               .setAttribute("onchange", "javascript: ns_igk.html.ctrl.checkbox.init(this,'#clRows'); return false;");

            foreach (string s in ts)
            {
                tr.add("th").Content = s;
            }
            tr.add("th").addSpace();
            tr.add("th").addSpace();
            foreach (var c in this.m_documentSchema.Entries.GetEntries(this.m_TableName))//this.m_documentSchema.Entries.getElementsByTagName(this.m_TableName))
            {
                var tts = c.getElementsByTagName("Row");
                foreach (var ttr in tts)
                {
                    var r = ttr as BalafonDBBSchemaRow;
                    if (r == null)
                        continue;
                    this._addRow(v_et, r.GetIdentifierKey(), ts, r);
                }
            }
            //add hidden tr
            this._addRow(v_et, null, ts, null).setClass("dispn");

            var bar = v_frm.addDiv().setClass("igk-action-bar");
            addActionButton(bar, "insertEntry", "ns_igk.balafon.insertNewEntry(this);");
            addActionButton(bar, "deleteEntry", "ns_igk.ext.call('deleteEntry', '\"'+ns_igk.form.form_tojson(this.form)+'\"')");
            addActionButton(bar, "clearEntries","ns_igk.ext.call('deleteClearEntries')");
          //  addActionButton(bar, "savedocument","ns_igk.invoke('savedocument')");
          //  addActionButton(bar, "savetable", "ns_igk.invoke('savetable', {data:$igk('table').first().o.outerHTML})");

        }

#if DEBUG
        public void savetable(string s) {
            File.WriteAllText("d://temp/tablebalafon.html", s);
        }
        public void savedocument() {
            File.WriteAllText("d://temp/balafon.html", Document.RenderXML(null));
        }
#endif

        public void setDatabaseName(string name)
        {
           // CoreLog.WriteDebug($"Set database name : {name}");
            this.m_documentSchema["Database"] = name;

        }
        public string getDatabaseName() {
            return this.m_documentSchema["Database"]?.GetValue() as string;
        }

        private CoreXmlElement _addRow(CoreXmlElement v_et, string key, IEnumerable<string> ts, BalafonDBBSchemaRow r)
        {
            var tr = v_et.add("tr");
            tr["clRow"] = key;
            var td = tr.add("td");
            td.AddInput("clRows[]", "checkbox");
            td.setClass("bal-chb");
            td.setAttribute("value", key);
          //      .setAttribute("onchange",
          //string.Format(BalafonDBBConstant.BTN_SCRIPT_1, "ns_igk.ext.call('storeEntries')"));

            foreach (string s in ts)
            {
                td = tr.add("td").setAttribute("cl", s);
                if (r!=null)
                    td.Content = r[s];
            }
            addImgLnk(tr.add("td"), "edit", "ns_igk.balafon.editrow($igk(this).select('^tr').getItemAt(0))");
            addImgLnk(tr.add("td"), "drop", "(function(q){ ns_igk.ext.call('deleteEntry', q.getAttribute('clRow')); q.remove(); })($igk(this).select('^tr').getItemAt(0))");
            return tr;
            
        }

        private void addImgLnk(CoreXmlElement e, string p, string action)
        {
            e.addA("#")
                        .setAttribute("onclick", "javascript: " + action + "; return false;")
                        .add("img").setAttribute("src",
                        PathUtils.GetPath("%startup%/sdk/img/" + p + ".png")
                        );
        }
       

       
        /// <summary>
        /// utility class
        /// </summary>
        class Utility
        {
            private BalafonDBBScriptViewerProfile m_owner;

            public Utility(BalafonDBBScriptViewerProfile owner)
            {
                this.m_owner = owner;
            }
            /// <summary>
            /// get table entries
            /// </summary>
            /// <returns></returns>
            public CoreXmlElement GetDataTableEntry()
            {
                var s = this.m_owner.m_documentSchema.Entries.GetEntries (this.m_owner.m_TableName);
                if (s != null)
                {
                    return s.CoreGetValue(0) as CoreXmlElement;
                }
                return null;
            }
            public CoreXmlElement GetDataTableEntry(string table)
            {
                var s = this.m_owner.m_documentSchema.Entries.GetEntries(table);
                if (s != null)
                {
                    return s.CoreGetValue(0) as CoreXmlElement;
                }
                return null;
            }
        }
        public bool insertNewEntry()
        {
            var r = this.m_utility.GetDataTableEntry();

            if (r == null)
            {
                //no entry found
                r = this.m_documentSchema.Entries.add(this.m_TableName);
            }
            var row  = this.m_documentSchema.Entries.CreateChildNode("Row") as BalafonDBBSchemaRow;
         
            //update value
            if (row == null)
            {
                this.JSInvokeMsBox("warning", "Row not created");
                return false ;
            }
            r.AddChild(row);
            var table = this.m_documentSchema.GetTableDefinition(this.m_TableName);
            var ts = table.GetColumnKeys();
            foreach (string s in ts)
            {
                var clinfo = table.GetColumn(s);
                var p = clinfo.GetAttributeValue <string>("clDefault");
                row[s] = p;
            }
            this.InvokeScript(string.Format("ns_igk.balafon.setNewEntryKey('{0}');",
                row.GetIdentifierKey ()));

            return true;
        }


        public void deleteEntry(string data)
        {
            var st = this.m_utility.GetDataTableEntry();
            Dictionary<string, CoreXmlElement> mb = new Dictionary<string, CoreXmlElement>();
            foreach (BalafonDBBSchemaRow item in st.getElementsByTagName("Row"))
            {
                mb.Add(item.GetIdentifierKey(), item);
            }
            var t = new IGK.ICore.JSon.CoreJSon();
            var d = t.ToDictionary(data);

            if ((d.Count == 0) || mb.ContainsKey(data))
            {

                st.Remove(mb[data]);
                this.Reload();
                return;


            }


            var tab = d.ContainsKey("clRows") ? d["clRows"] as object[] : null;
            if (tab != null)
            {

                bool v = false;
                for (int i = 0; i < tab.Length; i++)
                {
                    var s = tab[i].CoreGetValue<string>();
                    if (!string.IsNullOrEmpty(s) && mb.ContainsKey(s))
                    {
                        st.Remove(mb[s]);
                        v = true;
                    }

                }
                if (v)
                    this.Reload();
            }
            else
            {
                var g = d.ContainsKey("clRow") ? d["clRow"] : null;
                string key = g.ToStringCore();
                if (!string.IsNullOrEmpty(key))
                {
                    key = key.Replace("[", "").Replace("]", "");
                    if (mb.ContainsKey(key))
                    {
                        st.Remove(mb[key]);
                    }
                    //this.Reload();
                }
            }
        }
        public void updateEntry(string data)
        {
            var t = new IGK.ICore.JSon.CoreJSon();
            var d = t.ToDictionary(data);
            if (!d.ContainsKey("rid"))
                return;

            var s = this.m_utility.GetDataTableEntry();


            var k = d["rid"].ToString();

            if ((k != null) && (s != null))
            {
                foreach (BalafonDBBSchemaRow es in s.getElementsByTagName("Row"))
                {
                    if (es.GetIdentifierKey() == k)
                    {
                        //foreach (var item in ds)
                        //{
                        //    // es[item] = item;
                        //}
                        foreach (KeyValuePair<string, object> item in d)
                        {
                            if (item.Key == "rid") continue;
                            es[item.Key] = (string)item.Value;
                        }


                        break;
                    }
                }
            }

        }
        public void deleteClearEntries()
        {
            //this.InvokeScript("ns_igk.winui.notify.showMsg('not implement');");
            var s = this.m_utility.GetDataTableEntry();
            if (s != null) {
                s.ClearChilds();
                this.Reload();
            }
            else
                this.InvokeScript("ns_igk.winui.notify.showMsBox('Warning', 'no entry found');");
            
        }


        private void addActionButton(CoreXmlElement bar, string id, string script)
        {
            bar.addInput("btn_add", "button", ("btn.html." + id).R())
              .setClass("igk-btn")
              .setAttribute("onclick",
              string.Format(BalafonDBBConstant.BTN_SCRIPT_1, script));

        }
        private void viewInfoDefinition(CoreXmlElement e)
        {
            //error:
            e.setClass ("no-overflow fitw").setAttribute ("style", "overflow:hidden; border : 1px solid #aaa");
            bool error = false;
            if (string.IsNullOrEmpty(this.m_TableName))
            {
                error = true;
            }

            var table = this.m_documentSchema.GetTableDefinition(this.m_TableName);
            if (table == null)
            {
                error = true;
            }
            if (error)
            {
                e.addDiv().setClass("igk-panel igk-panel-error")
                       .setContent("e.balafon.NoDefFound".R());
                return;
            }
            //load table
            var ul = e.add("div").setClass("dispn").setId("clLinkType_data");
            string v_stable = null;
            foreach (CoreXmlElement b in this.DocumentSchema.getElementsByTagName(BalafonDBBConstant.DATADEFINITION_TAG))
            {
                v_stable = b["TableName"].ToString();
                ul.add("e")
                    .setAttribute("value", v_stable)
                    .setContent(v_stable).Tag = v_stable;
            }
            ul.Childs.Sort(CompareTag);
            ul.add("e")
                .setAttribute("value", "::custom")
                .Content = "<i>Custom link</i>";

            ul = e.add("div").setClass("dispn").setId("clType_data");
            foreach (var b in new Dictionary<string, string>() { 
                {"varchar", "VARCHAR"},
                {"int", "Int"},
                {"uint", "UInt"},
                {"bigint", "BIGInt"},
                {"ubigint", "UBIGInt"},
                {"text", "Text"},
                {"float", "Float"},
                {"ufloat", "UFloat"},                
                {"double", "Double"},
                {"udouble", "UDouble"},
                {"date", "Date"},
                {"datetime", "DateTime"},
                {"time", "Time"},
                {"timestamp", "TimeStamp"},
                {"blob", "Blob" },
                {"mblob", "MediumBlob" },
                {"lblob", "LongBlob" },
                {"binary", "Binary" },
                {"json", "JSON" }, 
            })
            {
                ul.add("e")
                    .setAttribute("value", b.Key.ToLower())
                    .Content = b.Value;
            }

            var frm = e.addDiv().setClass ("fitw").
                add("form");
            var et = frm.addDiv()
                .setClass("overflow-x-a").add("table").setClass("igk-table igk-table-stripped");
            frm.addInput("clTableName", "hidden", this.TableName);
            Dictionary<string, string> dic = new Dictionary<string, string>(){
                {"clName","string"},                
                {"clType","select"},
                {"clTypeLength","int"},
                {"clDefault","string"},
                {"clNotNull","bool"},
                {"clIsUniqueColumnMember","bool"},
                {"clColumnMemberIndex","string"},
                {"clAutoIncrement","bool"},
                {"clIsPrimary","bool"},
                {"clIsUnique","bool"},
                {"clIsIndex","bool"},
                {"clDescription","string"},
                {"clLinkType","editselect"},
                {"clLinkColumn","string"},
                {"clInsertFunction","string"},
                {"clUpdateFunction","string"},
                {"clInputType","string"}
            };
            var tr = et.add("tr");
            //  GSDataColumnDefinition
            //foreach (var y in d)
            //{
            //    tr.add("td").Content = item[y.Name];
            //}
            tr.add("th").AddInput("", "checkbox")
              .setAttribute("value", "togglevalue")
              .setClass("box_16x16")
               .setAttribute("onchange", "javascript: ns_igk.html.ctrl.checkbox.init(this,'#clColumns'); return false;");
            foreach (var y in dic)
            {

                var td = tr.add("th").addDiv()
                    .setContent(("lb." + y.Key).R());
                if (y.Key == "clName")
                {
                    td.setAttribute("style", "width:150px;");

                }
            }
            tr.add("th").addSpace();
            int v_c = 0;
            foreach (var item in table.getElementsByTagName("Column"))
            {
                tr = et.add("tr");
                //  GSDataColumnDefinition

                var p = item["clName"];
                if (p == null)//not valid attrib
                    continue;
                v_c++;
                string n = p.Value.CoreGetValue<string>();
                var td = tr.add("td").AddInput("clColumns[]", "checkbox")
              .setAttribute("value", n)
        //       .setAttribute("onchanged",
        //string.Format(BalafonDBBConstant.BTN_SCRIPT_1, "ns_igk.ext.call('storeColumn', ns_igk.form.form_tojson(this.form))"))
        ;


                foreach (var y in dic)
                {
                    object d = item[y.Key]?.Value;
                    switch (y.Value)
                    {
                        case "bool":
                            {
                                var c = tr.add("td").addInput(this.TableName + "@" + y.Key + "[]", "checkbox");
                                if (d.CoreGetValue<bool>())
                                {
                                    c.setAttribute("checked", "true");
                                }
                                c.setAttribute("value", "true");
                                c.setAttribute("onchange", "javascript: ns_igk.ext.call('storeColumn', ns_igk.form.form_tojson(this.form)); return false; ");
                            }
                            break;
                        case "string":
                        case "int":
                            {
                                var c = tr.add("td").addInput(this.TableName + "@" + y.Key + "[]", "text");
                                c.setClass("igk-form-control "+y.Key.ToLower ());
                                c.setAttribute("value", item[y.Key]);
                                if (y.Key == "clName")
                                {
                                    c.setAttribute("onchange", "javascript: ns_igk.ext.call('changeColumnName', {oldname:'" + p + "',newname:this.value}); return false; ");
                                }
                                else
                                {
                                    c.setAttribute("onchange", "javascript: ns_igk.ext.call('storeColumn', ns_igk.form.form_tojson(this.form)); return false; ");
                                }
                            }
                            break;
                        case "select":
                        case "editselect":
                            {
                                var c = tr.add("td").add("select");
                                c.setId(this.TableName + "@" + y.Key + "[]");
                                c.setClass("igk-form-control dispb");
                              //  c.setAttribute("value",null);// d.ToStringCore ());
                                c.setAttribute("onchange", "javascript: ns_igk.balafon.select_valuePost(this); return false; ");
                                int allow_empty = 1;
                                int v_usecustom = 0;
                                if (y.Key == "clType")
                                {
                                    if ((d == null) || (d?.ToString() == "null"))
                                        d = "Int";
                                    allow_empty = 0;
                                }
                                else {
                                    v_usecustom = 1;
                                }
                                d = d?.ToString().ToLower();
                                c.setAttribute("igk-js-bind-select-to", "{id:'#" + y.Key + "_data'," +
                                    "selected:'" + d 
                                    + "', allowempty:"+allow_empty
                                    +", usecustom: "+v_usecustom
                                    +",tag:'e'}");
                            }
                            break;
                        default:
                            tr.add("td").Content = item[y.Key];
                            break;
                    }

                }
                tr.add("td").addA("#")
                    .setAttribute("onclick", "javascript: var q = $igk( $igk(this).select('^#igk-tab-view'));  if (q) q.setHtml(ns_igk.ext.call('rmColumn', '" + item["clName"] + "')); return false;")
                    .add("img").setAttribute("src",
                    PathUtils.GetPath("%startup%/sdk/img/drop.png")
                    );
            }
            frm.addInput("clColumnCount", "hidden", v_c.ToString());
            var bar = frm.addDiv().setClass("igk-action-bar");
            bar.addInput("btn_add", "button", "btn.html.add".R())
                .setClass("igk-btn")
                .setAttribute("onclick",
                string.Format(BalafonDBBConstant.BTN_SCRIPT_1, "ns_igk.balafon.addNewColumn(this)"));

             bar.addInput("reload", "hidden", "0");
                
            bar.addInput("btn_add", "button", "btn.html.rmcolumn".R())
                .setClass("igk-btn")
                .setAttribute("onclick",
                string.Format(BalafonDBBConstant.BTN_SCRIPT_1, "ns_igk.balafon.rmColumn(this)"));
             



        }
        public void changeColumnName(string data)
        {
            var t = new IGK.ICore.JSon.CoreJSon();
            var def = this.m_documentSchema.GetTableDefinition(this.m_TableName);
            var d = t.ToDictionary(data);
            if (def != null)
            {
                var cd = def.GetColumn(d.CoreGetValue<string>("oldname"));
                if (cd != null)
                {
                    def.ChangeColumnName(cd, d["newname"].CoreGetValue<string>());
                    this.ReloadView();
                }
            }

        }
        /// <summary>
        /// used to store a column info
        /// </summary>
        /// <param name="data"></param>
        public void storeColumn(string data)
        {
            var t = new IGK.ICore.JSon.CoreJSon();
            var d = t.ToDictionary(data);
            if (d.Count == 0)
                return;
            var tn = CoreExtensions.CoreGetValue<string>(d["clTableName"]);
            //update all column info
            var def = this.m_documentSchema.GetTableDefinition(tn);

            if (def != null)
            {
                int clColumnCount = CoreExtensions.CoreGetValue<int>(d["clColumnCount"]);
                string key = tn + "@clName";
                if (d.ContainsKey(key) == false)
                    return;
                //update all column info
                //update table info
                object[] n = d[key] as object[];
                string pattern = tn + "@";
                for (int i = 0; i < clColumnCount; i++)
                {

                    var cl = def.GetColumn(n[i].ToString());
                    if (cl == null) {

                        continue;
                    }

                    foreach (var s in d)
                    {
                        if (s.Key.StartsWith(pattern))
                        {
                            var rp = (s.Value is object[]) ? s.Value as object[] : null;
                            if ((rp != null) && ((rp.Length >= 0) && (i < rp.Length)))
                            {
                                cl[s.Key.Substring(pattern.Length)] = CoreExtensions.CoreGetValue<string>(rp[i]);
                            }
                            else
                            {
                                CoreLog.WriteLine("value not set " + s.Key);
                            }
                        }
                    }
                }
                if (d.CoreGetValue<bool>("clReload"))
                    this.Reload();
            }

        
        }
        public string rmColumn(string data)
        {
            var t = new IGK.ICore.JSon.CoreJSon();
            var d = t.ToDictionary(data);
            if ((d != null) && (d.Count > 0))
            {
                var tab = d.ContainsKey("clColumns") ? d["clColumns"] as object[] : null;
                var tn = CoreExtensions.CoreGetValue<string>(d["clTableName"]);
                var def = this.m_documentSchema.GetTableDefinition(tn);
                if (tab != null)
                {


                    if ((tab != null) && (tab.Length > 0))
                    {

                        if (def != null)
                        {
                            for (int i = 0; i < tab.Length; i++)
                            {
                                if (tab[i] != null)
                                    def.RemoveColumn(tab[i].ToString());
                            }
                        }
                        if (d.ContainsKey("reload") && (d["reload"].ToString() != "0"))
                            this.Reload();
                    }
                }
            }
            else {
                var v_tt = this.m_documentSchema.GetTableDefinition(this.TableName);                
                v_tt.RemoveColumn(data);
            
            }
            return getViewInfo();
        }
        public void editTableName() {

            var t = this.m_documentSchema.GetTableDefinition(this.TableName);

            if (t == null)
            {
                return;
            }
            if (CoreSystem.Instance.Workbench != null)
            {

                string s = string.Empty;
#if DEBUG
                s = CoreFileUtils.ReadAllFile(CoreConstant.DRS_SRC +@"\addins\web\IGK.DrSStudio.BalafonAddIn\DataBaseBuilder\Resources\balafon_edit_table_name.html")??
                    CoreResources.GetResourceString(string.Format(BalafonDBBConstant.RES_DATA_1, BalafonDBBConstant.RES_EDIT_TABLE_NAME), GetType().Assembly);
#else
                s = CoreResources.GetResourceString(string.Format(BalafonDBBConstant.RES_DATA_1, BalafonDBBConstant.RES_EDIT_TABLE_NAME), GetType().Assembly);
#endif
                var d = CoreCommonDialogUtility.BuildWebDialog(
                  CoreSystem.Instance.Workbench,
                  "title.balafondbb.EditTableName".R(), CoreWebUtils.EvalWebStringExpression(s, this));
                if (d != null)
                {
                    IGK.ICore.JSon.CoreJSon c = new ICore.JSon.CoreJSon();
                    var trd  = c.ToDictionary(d.ToStringCore());
                    if ((trd!=null) && (trd.Count > 0))
                    {
                        this.m_documentSchema.ChangeTableName(t, trd["clTableName"].ToStringCore ());
                        t["Description"] = trd["clDescription"].ToStringCore();
                        this.m_TableName = t["TableName"];
                    }
                    this.ReloadView();//.Reload();
                }
            }            
        }

        public void UpdateData() {
        }
        public void IOGetFile() {

        }
        public override void Reload()
        {
            base.Reload();
        }
        public void ReloadView() {
            if (this.ReloadListener !=null) {
                this.ReloadListener.ReloadDocument(this.Document, false);
            }
            this.InvokeScript("ns_igk.balafon.reloadview()");
        }
        public string getBodyView() {

            return this.Document.Body.RenderXML (new CoreXmlWebOptions());//.RenderInnerHTML(null);
        }
        public string addNewColumn()
        {
            var t = this.m_documentSchema.GetTableDefinition(this.TableName);

            if (t == null)
            {
                return string.Empty;
            }
            /*
            if (CoreSystem.Instance.Workbench != null)
            {

                string s = string.Empty;
#if DEBUG
               s =  File.ReadAllText(CoreConstant.DRS_SRC+@"\addins\web\IGK.DrSStudio.BalafonAddIn\DataBaseBuilder\Resources\balafon_add_new_column.html");
#else 
                s = 
                  CoreResources.GetResourceString(
                  string.Format(BalafonDBBConstant.RES_DATA_1, BalafonDBBConstant.RES_NEW_COLUMN)
                  );
#endif
                var d = CoreCommonDialogUtility.BuildWebDialog(
                  CoreSystem.Instance.Workbench,
                  "title.balafondbb.addNewColumn".R(),s);
                if (d != null)
                {
                    t.AppendNewColumn(d != null ? d.ToString() : null);
                    this.Reload();
                }
            }
            else
            {            */
                    t.AppendNewColumn();
                    //this.Reload();
                    return this.getViewInfo();
            //}
        }
        public string addnColumn(string name)
        {
            var t = this.m_documentSchema.GetTableDefinition(this.TableName);
            if (t != null)
            {
                if (t.AddColumn(name))
                {
                    return this.getViewInfo();
                }
            }
            return null;
        }
        /// <summary>
        /// get view options
        /// </summary>
        /// <returns></returns>
        public string getOptions()
        {
            var ul = CoreXmlElement.CreateXmlNode("ul");
            string[] t = new string[] {
                "definition",
                "entries"
            };
            for (int i = 0; i < t.Length; i++)
            {
                var a = ul.Add("li").add("a")
                    .setClass("igk-btn igk-btn-default")
                    .setAttribute("onclick",
                    string.Format("javascript: ns_igk.ext.call('viewTab', '{0}'); return false;", t[i]))
                    .setContent(("lb." + t[i]).R());
                if (m_viewentry == t[i])
                {
                    a.setClass("igk-active");
                }
            }
            //var b = this.DocumentSchema.getElementsByTagName(BalafonDBBConstant.DATADEFINITION_TAG);
            //if (string.IsNullOrEmpty(this.TableName))
            //{
            //    if (b.Length > 0) {
            //        var e = b[0];
            //        this.TableName = e["TableName"];
            //        this.loadColumnInfo(e, ul);
            //    }
            //}
            //else
            //{
            //    foreach (CoreXmlElement bitem in this.DocumentSchema.getElementsByTagName(BalafonDBBConstant.DATADEFINITION_TAG))
            //    {
            //        if (bitem["TableName"] == this.TableName)
            //        {
            //            this.loadColumnInfo(bitem, ul);                       
            //            break;
            //        }
            //    }
            //}
            return ul.RenderXML(new CoreXmlWebOptions());
        }
        public string viewTab(string view)
        {
            m_viewentry = view;
            this.ReloadView();            
            return "1";
        }
        public string setTable(string table)
        {
            if (this.m_TableName != table)
            {
                this.m_TableName = table;
                this.ReloadView();
                //this.Reload();
                return "1";
            }
            return "0";
        }
        private void loadColumnInfo(CoreXmlElementBase e, CoreXmlElement ul)
        {
            //get columns data
            foreach (CoreXmlElement ritem in e.getElementsByTagName(BalafonDBBConstant.COLUMNDEF_TAG))
            {
                ul.add("li").add("a").setAttribute("href", "#")
                    .setAttribute("onclick", "").Content = ritem["clName"];
            }
        }


        //exported function
        public void addTable()
        {
            //get new table id count
            var b = this.m_documentSchema.AddTable("table_" + this.m_documentSchema.Count);
            if (b.AddColumn("clId")) {
                var c = b.GetColumn("clId");
                c["clAutoIncrement"] = "true";
                c["clIsPrimary"] = "true";
                c["clNotNull"] = "true";
            }
           

            this.ReloadView();
        }
        public void clearTables()
        {
            this.m_documentSchema.ClearTable();
            this.TableName = null;
            this.Reload();
        }

        public void saveSchemas()
        {
            if (File.Exists(this.DocumentSchema.FileName))
            {
                this.balafonDBBSurface.SaveAs(this.DocumentSchema.FileName);
            }
            else
            {
                using (SaveFileDialog sfd = new SaveFileDialog()
                {
                    Filter = "schema-data(*.xml)|*.xml|(*.*)|*.*",
                    AddExtension = true
                })
                {
                    string fn = this.DocumentSchema.FileName;
                    sfd.RestoreDirectory = true;
                    if (!string.IsNullOrEmpty(fn))
                    {
                        string dir = System.IO.Path.GetDirectoryName(fn);
                        if (Directory.Exists(dir))
                        {
                            sfd.InitialDirectory = dir;
                            Environment.CurrentDirectory = dir;
                        }
                    }
                    sfd.FileName = BalafonDBBConstant.FILE_NAME;



                    if (sfd.ShowDialog() == global::System.Windows.Forms.DialogResult.OK)
                    {
                        this.balafonDBBSurface.SaveAs(sfd.FileName);
                        this.DocumentSchema.FileName = sfd.FileName;
                    }
                }
            }
        }
        public void openSchemas()
        {
            openSchemasData(false);
        }
        public void importSchemas()
        {
            openSchemasData(true);
        }
        public void openSchemasData(bool import)
        {
            using (OpenFileDialog ofd = new OpenFileDialog()
            {
                Filter = "data.schema.xml|*.xml",
                FileName = BalafonDBBConstant.FILE_NAME
            })
            {
                if (ofd.ShowDialog() == global::System.Windows.Forms.DialogResult.OK)
                {
                    var s = CoreXmlElement.LoadFile(ofd.FileName);
                    if (s != null)
                    {
                        if (!import)
                            this.m_documentSchema.ClearTable();
                        this.m_documentSchema.Load(s);
                        this.m_documentSchema.FileName = ofd.FileName;
                        this.Reload();
                    }
                }
            }
        }
        private string getFolder()
        {
            var b = CoreSystem.GetWorkbench();
            if (b != null)
            {
                string g = Path.GetDirectoryName(this.m_documentSchema.FileName);
                string f = CoreCommonDialogUtility.PickFolder(CoreSystem.GetWorkbench(),
                    "title.choosefolder".R(), g);
                return f;
            }
            else
            {
                using (FolderBrowserDialog fbd = new FolderBrowserDialog())
                {
                    if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        return fbd.SelectedPath;
                    }
                }
            }
            return null;
        }
        public void ExportToCS()
        {
            var b = getFolder();
            if (b != null)
            {
                BalafonUtility.ExportSchemaToCSInterfaceFile(b, this.m_documentSchema);
                this.InvokeScript("ns_igk.winui.notify.showMsg('done')");
            }
        }
        public void ExportToPhp()
        {
            var b = getFolder();
            if (b != null)
            {
                BalafonUtility.ExportSchemaToPhpInterfaceFile(b, this.m_documentSchema);
                //this.InvokeScript("ns_igk.winui.notify.showMsg('done')");
            }
        }

        public string IOGetFileContent(string filename)
        { 
            string f =  IGK.ICore.Web.CoreWebUtils.GetFileContent(filename);
            return f;
        }
    }
}
