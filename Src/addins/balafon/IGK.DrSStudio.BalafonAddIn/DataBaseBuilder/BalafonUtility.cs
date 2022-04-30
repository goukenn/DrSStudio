using IGK.ICore;
using IGK.ICore.DB;
using IGK.ICore.Xml;
using IGK.VSLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

#pragma warning disable

namespace IGK.DrSStudio.Balafon.DataBaseBuilder
{
    /// <summary>
    /// represent balafon utility function
    /// </summary>
    static class BalafonUtility
    {
        public static void ExportSchemaToPhpInterfaceFile(string outputFolder, CoreXmlElement document, string @namespace = null)
        {
            var r = CoreDataContext.GetDataSchema(CoreDBManager.Adapter);
            if (r == null)
            {
                return;
            }

            StringBuilder sb = new StringBuilder();

            foreach (CoreXmlElement e in r.getElementsByTagName(CoreDataConstant.DATA_DEFINITON_TAG))
            {
                //store defitinon
                var tbName = e.GetAttributeValue<string>("TableName");
                if (string.IsNullOrEmpty(tbName))
                    continue;
                var ktb = "I" + tbName;
                string f = Path.Combine(outputFolder, ktb + ".php");

                sb.Length = 0;


                sb.AppendLine(string.Format(@"<?php
///<summary>represent table definition for {1}</summary>
interface {0} extends IIGKDataTable{{", ktb, tbName));

                //build all attributes

                foreach (CoreXmlElement cf in e.getElementsByTagName("Column"))
                {
                    string v = cf.GetAttributeValue<string>("clName");
                    if (typeof(ICoreDataTable).GetProperty(v) != null)
                        continue;
                    sb.AppendLine(
                        string.Format("function get{0}(); function set{0}($value);", v
                        ));
                }

                sb.AppendLine("}");
                sb.AppendLine(string.Format("igk_register_balafon_db_table(\"{0}\");", ktb));
                //sb.Append("?>");
                File.WriteAllText(f, sb.ToString(), Encoding.Default);
            }

        }

        /// <summary>
        /// get the schema
        /// </summary>
        /// <param name="outputFolder"></param>
        /// <param name="namespace"></param>
        public static void ExportSchemaToCSInterfaceFile(string outputFolder, CoreXmlElement document, string @namespace = null)
        {
            var r = document ?? CoreDataContext.GetDataSchema(CoreDBManager.Adapter);
            if (r == null)
            {
                return;
            }
            string f = string.Empty;
            StringBuilder sb = new StringBuilder();
            string atttribName = nameof(CoreDataTableAttribute);
            List<string> v_cFile = new List<string>();
            Dictionary<string, string> createlink = new Dictionary<string, string>(); // 

            List<string> decTable = new List<string>();
            foreach (CoreXmlElement e in r.getElementsByTagName(CoreDataConstant.DATA_DEFINITON_TAG))
            {
                //store defitinon
                var tbName = e.GetAttributeValue<string>("TableName");
                if (string.IsNullOrEmpty(tbName))
                    continue;

                decTable.Add(tbName);
                var ktb = "I" + tbName;
                f = Path.Combine(outputFolder, ktb + ".cs");
                v_cFile.Add(f);
                sb.Length = 0;
                if (!createlink.ContainsKey(ktb))
                    createlink.Add(ktb, "0");
                else
                    createlink.Remove(ktb);
                
                sb.AppendLine(string.Format(@"using System; 
using IGK.ICore;
using IGK.ICore.DB;

#pragma warning disable IDE1006 // Naming Styles

[{2}(""{1}"")]
public interface {0} : ICoreDataTable{{", ktb, tbName, atttribName));

                //build all attributes
                
                foreach (CoreXmlElement cf in e.getElementsByTagName("Column"))
                {
                    string v = cf.GetAttributeValue<string>("clName");
                    if (typeof(ICoreDataTable).GetProperty(v) != null)
                        continue;

                    string link = cf.GetAttributeValue<string>("clLinkType");
                    string ct = link;
                    if (string.IsNullOrEmpty(link) == false)
                    {
                        ct = string.Format("I" + link);
                        if (!createlink.ContainsKey(ct))
                            createlink.Add(ct, "1");
                  
                    }
                    else {
                        ct = CoreDataUtility.GetType(cf);
                    }

                    sb.AppendLine(
                        string.Format("{0} {1} {2}{{get;set;}}", CoreDataUtility.GetAttribute(cf), ct, v
                        ));
                }
                sb.AppendLine("}");
                File.WriteAllText(f, sb.ToString());
            }
            //add additionnal links
            foreach (KeyValuePair<string, string> item in createlink)
            {

                if (item.Value == "1")
                {
                    f = Path.Combine(outputFolder, item.Key + ".cs");
                    v_cFile.Add(f);
                    File.WriteAllText(f, $@"
using System; 
using IGK.ICore;
using IGK.ICore.DB;

#pragma warning disable IDE1006 // Naming Styles

[{atttribName}(""{item.Key}"")]
public interface {item.Key} : ICoreDataTable{{

}}
");

                }
            }


            //initialize file mane
            f = Path.Combine(outputFolder, "Initializer.cs");

            v_cFile.Add(f);
            sb.Length = 0;
            GetCSInitailerType(r, sb, decTable);
            File.WriteAllText(f, sb.ToString());


            //build visual studion lib file
            CSSolution sol = CSSolution.CreateCSharpLibrary();


            //reference
            CSItemGroup g = new CSItemGroup();
            string[] refs = { "System", "System.Xml", "IGK.ICore" };
            for (int i = 0; i < refs.Length; i++)
            {
                if (g.Add("Reference") is CSReference rt)
                {
                    rt["Include"] = refs[i];
                }
            }
            var v_asm = System.Reflection.Assembly.GetAssembly(typeof(CoreSystem));
            sol.AddReferenceProject(g, "IGK.ICore",
                v_asm.Location 
            );


            sol.AddChild(g);

            //src
            g = new CSItemGroup();
            string[] files = v_cFile.ToArray();// { "MainActivity.cs", "App.cs" };

            for (int i = 0; i < files.Length; i++)
            {
                if (g.Add("Compile") is CSReference rr)
                {
                    rr["Include"] = files[i].Substring(outputFolder.Length+1);
                }
            }
            sol.AddChild(g);


            ////reseources
            //g = new CSItemGroup();
            //string[] res = { @"Resources\layout\Main.axml", @"Resources\values\Strings.xml" };

            //for (int i = 0; i < files.Length; i++)
            //{
            //    AndroidResource c = new AndroidResource();
            //    c["Include"] = res[i];
            //    g.AddChild(c);
            //}
            //sol.AddChild(g);


            //add solution project reference 
            //var gt = sol.Add("ItemGroup").Add("ProjectReference");
            //gt["Include"] =  "";
            //gt.Add("Project").Content = "{95E437A5-4A87-4FAB-B15F-4A0FDE8B7DCF}";
            //gt.Add("Name").Content = "IGK.ICore";





            sol.Save(Path.Combine(outputFolder, "DataLib.csproj"));
        }

        private static void GetCSInitailerType(CoreXmlElement r, StringBuilder sb, List<string> decTable)
        {
            StringBuilder v = new StringBuilder();


            var entry = r.getElementsByTagName("Entries")?[0];
            if (entry?.Childs.Count > 0)
            {
                foreach (var row in entry.getElementsByTagName("Rows"))
                {
                    string tablen = row["For"];
                    if (string.IsNullOrEmpty(tablen) || !decTable.Contains(tablen))
                        continue;
                    string table = "I" + tablen;

                   


                    v.AppendLine($"public void Initialize({table} type){{");
                    foreach (CoreXmlElement irow in row.getElementsByTagName("Row"))
                    {
                        //irow.Attributes
                        v.Append("LoadData(type, @\"{");
                        bool v_csplit = false;
                        foreach (KeyValuePair<string, CoreXmlAttributeValue > attr in irow.Attributes) {

                            if (v_csplit)
                                v.Append(",");
                            v.Append(attr.Key + ":\"\"" + attr.Value+"\"\"");

                            v_csplit = true;
                        }

                        v.AppendLine("}\");");
                        //v.AppendLine("type.Insert();");
                    }
                    v.AppendLine("}");
                }
            }

            sb.AppendLine(string.Format(@"/*
Geneareted file 
*/
using System; 
using IGK.ICore;
using IGK.ICore.DB;

#pragma warning disable IDE1006 // Naming Styles);

[assembly:{0}(InitializerType=typeof(Initilizer))]

class Initilizer : {1}{{
//create initialize method to initialize every item on data
//public void Initialize([type_data_base] type)){{
//
//}}
{2}
}}
", typeof (CoreDataBaseInitializerAttribute).Name,
typeof(CoreDataInitializerBase).Name,
v.ToString()));
        }

        /// <summary>
        /// get if the name is a valid table name
        /// </summary>
        /// <param name="newTableName"></param>
        /// <returns></returns>
        public static bool ValidTableName(string newTableName)
        {
            if (CoreXmlUtility.ValidName(newTableName))
                return true;
            //reserverword
            //prefix
            Regex rg = new Regex("^%prefix%([0-9a-z_]+)", RegexOptions.IgnoreCase);
            return rg.IsMatch(newTableName);
        }
    }
}
