

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AndroidUtils.cs
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
file:AndroidUtils.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml ;

using IGK.DrSStudio.Android.Entities;
using IGK.DrSStudio.Android.Settings;
using IGK.ICore.IO;
using IGK.ICore.Xml;
using System.Windows.Forms;
using IGK.ICore;
namespace IGK.DrSStudio.Android
{
    /// <summary>
    /// internal utility class
    /// </summary>
    public static class AndroidUtils
    {
        public static string GetString(this StreamReader streamReader, int length)
        {
            Byte[] data = new Byte[length];
            int i = 0;
            StringBuilder sb = new StringBuilder();
            BinaryReader binR = new BinaryReader(streamReader.BaseStream);
            i = binR.Read(data, 0, data.Length);
            if (i>0)
            {
                sb.Append(ASCIIEncoding.Default.GetString(data, 0, i));
                return sb.ToString();
            }
            return null;
        }
        /// <summary>
        /// get all string from process stream reader
        /// </summary>
        /// <param name="streamReader"></param>
        /// <returns></returns>
        public static string GetString(this StreamReader streamReader)
        {
            BinaryReader binR = new BinaryReader(streamReader.BaseStream );
            Byte[] data = new Byte[4096];
            int i = 0;
            StringBuilder sb = new StringBuilder();
            while ((i = binR.Read(data, 0, data.Length)) > 0)
            {
                sb.Append(ASCIIEncoding.Default.GetString(data, 0, i));
            }
            binR.Close();
            return sb.ToString();
        }



        public static T[] LoadEntitities<T>(string file,
            string rootItem) where T : class,new()
        {
            List<T> v_tattr = new List<T> ();
            if (File.Exists(file))
            {
                IGK.ICore.Xml.CoreXmlElement x = IGK.ICore.Xml.CoreXmlElement.CreateXmlNode("dummy");
                x.LoadString(File.ReadAllText(file));
                foreach (var item in x.getElementsByTagName(rootItem ))
                {
                    var t = new T();

                    v_tattr.Add(t);
                }
            }
            return v_tattr.ToArray();
        }
   }
}

