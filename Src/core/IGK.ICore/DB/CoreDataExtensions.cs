using IGK.ICore;
using IGK.ICore.Codec;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.ICore.WinUI.Configuration;
using IGK.ICore.Settings;






using IGK;
using IGK.ICore.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using IGK.ICore.Xml;

namespace IGK.ICore.DB
{
    /// <summary>
    /// represent GSDB extension class utility.
    /// </summary>
    public static class CoreDataExtensions
    {

        //public static string FullName(this ITbGSHuman human)
        //{ 
        //    if (human == null)return string.Empty ;
        //    return string.Format("{0} {1}", human.clFirstName, human.clLastName.ToUpper());
        //}
        public static string Configs(this string config)
        {
            return config;
        }

        public static void AddOrReplace(this Dictionary<string, object> dictionary, string key, object @object)
        {
            if (dictionary.ContainsKey(key))
            {
                dictionary[key] = @object;
            }
            else
                dictionary.Add(key, @object);

        }
       
      
        public static ICoreDataRow ToRow(this ICoreDataTable tItem)
        {
            if (tItem == null) return null;
            var r = CoreDBManager.CreateNewRow(tItem.GetType());
            r.UpdateValue(tItem, CoreDBManager.Adapter);
            return r;
        }
        public static Dictionary<string, object> ToDictionary(this ICoreDataTable tItem,
            CoreDataAdapterBase  adapter = null)
        {
            adapter = adapter ?? CoreDBManager.Adapter;
            if ((tItem == null) || (adapter ==null))
                return null;
            ICoreDataRow r = CoreDBManager.CreateNewRow(tItem.GetType());
            r.UpdateValue(tItem, adapter);
            return r.ToDictionary();
        }
        public static ICore2DDrawingDocument ToDocument(this ICore2DDrawingLayeredElement l)
        {
            var v_rc = l.GetBound();

            Core2DDrawingDocumentBase v_doc = new Core2DDrawingLayerDocument((CoreUnit)v_rc.Width, (CoreUnit)v_rc.Height);
            v_doc.CurrentLayer.Elements.Add (l);
            return v_doc ;
        }

        /// <summary>
        /// calculate md5 of string input
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string CalculateMD5Hash(this string input)
        {
            if (input == null)
                return null;
            // step 1, calculate MD5 hash from input
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString().ToLower ();
        }

        public static void SetText(this ICore2DDrawingDocument doc, string name, string value)
        {
            ICoreTextValueElement v = doc.GetElementById(name) as ICoreTextValueElement;
            if (v != null)
                v.Text = value;
        }

       

        public static PropertyInfo[] GetFullPropertyInfo(this Type interfaceType)
        {
            if (interfaceType.IsInterface)
            {
                List<PropertyInfo> v_lt = new List<PropertyInfo>();
                foreach (Type t in interfaceType.GetInterfaces())
                {
                    v_lt.AddRange(t.GetProperties());
                }
                PropertyInfo[] tab = interfaceType.GetProperties();
                v_lt.AddRange(tab);
                return v_lt.ToArray();
            }
            return interfaceType.GetProperties();
        }

        /// <summary>
        /// extend xmlElement to add info as row
        /// </summary>
        /// <param name="e"></param>
        /// <param name="row"></param>
        public static void LoadDataAsAttribute(this CoreXmlElement e, ICoreDataRow row)
        {
            if (e == null) return;

            for (int i = 0; i < row.FieldCount; i++)
            {
                var n = row.GetName(i);

                e[n] = row.GetValue<string>(n);
            }
        }
    }
}
