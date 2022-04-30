using IGK.ICore.Xml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.DB
{
    /// <summary>
    /// represent utility CoreData contract functions
    /// </summary>
    public static class CoreDataUtility
    {

        public static string  GetType(CoreXmlElement cf)
        {
             string link  = cf.GetAttributeValue<string>("clLinkType");
            if (string.IsNullOrEmpty (link ) == false )
            {
                return string.Format ("I"+link);
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
                    s= "object";
                    break;
            }

            return s;
        }

        public static string GetAttribute(CoreXmlElementBase cf)
        {
            var e = cf as CoreXmlElement ;
            CoreDataColumnInfo vinf = new CoreDataColumnInfo();
            enuCoreDataField field =  enuCoreDataField.None;
            
            StringBuilder sb = new StringBuilder();
     
            string b = string.Empty;
            if (e.GetAttributeValue<bool>("clAutoIncrement",false )) field |= enuCoreDataField.AutoIncrement ;
            if (e.GetAttributeValue<bool>("clIsUnique", false)) field |= enuCoreDataField.Unique;
            if (e.GetAttributeValue<bool>("clIsPrimary", false)) field |= enuCoreDataField.IsPrimaryKey ;
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
                        sb.Append(b+" TypeName=\"" + ln + "\"");
                        b = ",";
                        break;
                    default:
                        break;
                }
            }
            ln = e.GetAttributeValue<string>("clTypeLength", null);
            if (string.IsNullOrEmpty (ln) == false )
            sb.Append(((sb.Length>0)?",":"")+" Length=" + ln + "");

            string v_sbs = sb.ToString().Trim();
            if (string.IsNullOrEmpty(v_sbs) == false)
            {
                sb.Length = 0;
                sb.Append($"[{nameof(CoreDataTableFieldAttribute)}(");
                sb.Append(v_sbs);
                sb.AppendLine(")]");

                return sb.ToString();
            }            
            return string.Empty;
        }


        /// <summary>
        /// represent 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static T GetRowAt<T>(this ICoreDataQueryResult result, int index) {
            var s = result.Rows[index];
            return s != null ? CoreDBManager.CreateInterfaceInstance<T>(s) : default(T);
        }
    }
}
