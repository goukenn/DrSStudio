using IGK.ICore.IO;
using IGK.ICore.Xml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.DrSStudio.Balafon.DataBaseBuilder.WinUI;
using System.Text.RegularExpressions;
using IGK.ICore.DB;

namespace IGK.DrSStudio.Balafon.DataBaseBuilder
{

    class BalafonDBBUtility
    {

        internal static void ExportToCS(string folder, CoreXmlElement r)
        {
            if (!PathUtils.CreateDir(folder))
                return;

            StringBuilder sb = new StringBuilder();
            foreach (CoreXmlElement e in r.getElementsByTagName(BalafonDBBConstant .DATADEFINITION_TAG))
            {
                //store defitinon
                var tbName = e.GetAttributeValue<string>("TableName");
                if (string.IsNullOrEmpty(tbName))
                    continue;
                var ktb = "I" + tbName;
                string f = Path.Combine(folder, ktb + ".cs");

                sb.Length = 0;


                sb.AppendLine(string.Format(@"using System; 
using IGK.ICore;
using IGK.GS;
using IGK.GS.DataTable;

[GSDataTable(""{1}"")]
public interface {0} : IGSDataTable{{", ktb, tbName));

                //build all attributes

                foreach (CoreXmlElement cf in e.getElementsByTagName(BalafonDBBConstant.COLUMNDEF_TAG))
                {
                    string v = cf.GetAttributeValue<string>("clName");
                    //if (typeof(IGSDataTable).GetProperty(v) != null)
                    //    continue;
                    sb.AppendLine(
                        string.Format("{0} {1} {2}{{get;set;}}", __getAttribute(cf), __getType(cf), v
                      )
                    );
                }
                sb.AppendLine("}");
               // File.WriteAllText(f, sb.ToString());

                PathUtils.SaveAsUT8WBOM(f, sb.ToString());
            }
           
        }

        internal static void ExportToPhp(string folder, CoreXmlElement r)
        {
            StringBuilder sb = new StringBuilder();
            string f = Path.Combine(folder, ".dbdefinitions.php");
            foreach (CoreXmlElement e in r.getElementsByTagName(BalafonDBBConstant.DATADEFINITION_TAG))
            {
                //store defitinon
                var tbName = e.GetAttributeValue<string>("TableName");
                if (string.IsNullOrEmpty(tbName))
                    continue;
                var ktb = "I" + tbName;
                if (sb.Length == 0)
                {
                    sb.Append(string.Format("<?php\n"));
                }
                sb.Append(string.Format("///<summary>represent table definition for {1}</summary>\ninterface {0} extends IIGKDataTable{{\n", ktb, tbName));
                //build all attributes

                foreach (CoreXmlElement cf in e.getElementsByTagName("Column"))
                {
                    string v = cf.GetAttributeValue<string>("clName");
                    //if (typeof(IGSDataTable).GetProperty(v) != null)
                    //    continue;
                    sb.Append(
                        string.Format("function get{0}(); function set{0}($value);\n", v
                        ));
                }

                sb.Append("}\n");
                sb.Append(string.Format("igk_register_balafon_db_table(\"{0}\");\n", ktb));                
            }
            if (sb.Length > 0)
            {
                sb.Append("?>");
            }
            PathUtils.SaveAsUT8WBOM(f, sb.ToString());
        }


        private static string __getType(CoreXmlElement cf)
        {
            string link = cf.GetAttributeValue<string>("clLinkType");
            if (string.IsNullOrEmpty(link) == false)
            {
                return string.Format("I" + link);
            }
            string s = cf.GetAttributeValue<string>("clType", "int");
            switch (s.ToLower())
            {
                case "varchar":
                case "text":
                    return "string";
                case "datetime":
                    return "DateTime";
                case "float":
                    return "float";
                case "int":
                    return "int";
                default:
                    s = "object";
                    break;
            }

            return s;
        }

        private static string __getAttribute(CoreXmlElementBase cf)
        {
            var e = cf as CoreXmlElement;
            //GSDBColumnInfo vinf = new GSDBColumnInfo();
            enuCoreDataField field = enuCoreDataField.None;

            StringBuilder sb = new StringBuilder();

            string b = string.Empty;
            if (e.GetAttributeValue<bool>("clAutoIncrement", false)) field |= enuCoreDataField.AutoIncrement;
            if (e.GetAttributeValue<bool>("clIsUnique", false)) field |= enuCoreDataField.Unique;
            if (e.GetAttributeValue<bool>("clIsPrimary", false)) field |= enuCoreDataField.IsPrimaryKey;
            if (e.GetAttributeValue<bool>("clNotNull", false)) field |= enuCoreDataField.IsNotNull;
            if (field != enuCoreDataField.None)
            {
                sb.Append(string.Format("Binding=(enuCoreDataField){0}", (int)field));
                b = ",";
            }
            if (string.IsNullOrEmpty(e.GetAttributeValue<string>("clDescription", null)) == false)
            {
                sb.Append(b + " Description=\"" + e.GetAttributeValue<string>("clDescription") + "\"");
                b = ",";
            }
            var ln = e.GetAttributeValue<string>("clType", null);
            if (string.IsNullOrEmpty(ln) == false)
            {
                switch (ln.ToLower())
                {
                    case "varchar":
                        //case "int":
                        sb.Append(b + " TypeName=\"" + ln + "\"");
                        break;
                    default:
                        break;
                }
                b = ",";
            }
            ln = e.GetAttributeValue<string>("clTypeLength", null);
            if (string.IsNullOrEmpty(ln) == false)
                sb.Append(b + " Length=\"" + ln + "\"");

            string v_sbs = sb.ToString().Trim();
            if (string.IsNullOrEmpty(v_sbs) == false)
            {
                sb.Length = 0;
                sb.Append(string.Format("[GSDataField("));
                sb.Append(v_sbs);
                sb.AppendLine(")]");

                return sb.ToString();
            }
            return string.Empty;
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

            return Regex.IsMatch(newTableName, "^%prefix%([0-9a-z_]+)", RegexOptions.IgnoreCase);
        }
    }
}
