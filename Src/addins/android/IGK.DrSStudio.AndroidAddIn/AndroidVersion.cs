

using IGK.ICore;
using IGK.ICore.Resources;
using IGK.ICore.Xml;
/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AndroidVersions.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace IGK.DrSStudio.Android
{
    /*
     * 
     * Represent android class used to define android version.
     * require android.versions.xml resource file
     * 
     * */
    /// <summary>
    /// represent android version. sealed class 
    /// </summary>
    public sealed class AndroidVersion
    {
        private static List<AndroidVersion> sm_versions;
        public static readonly AndroidVersion LPreviewVersion;
        private string m_Name;
        private int m_Number;

        static AndroidVersion() {
            LPreviewVersion = new AndroidVersion();
            LPreviewVersion.m_Name = "L Developer Preview";
            LPreviewVersion.m_Number = -1;
            sm_versions = new List<AndroidVersion>();
            LoadVersions();
        }
        /// <summary>
        /// get the numbers
        /// </summary>
        public int Number
        {
            get { return m_Number; }
        }
        /// <summary>
        /// get the display name
        /// </summary>
        public string Name
        {
            get { return m_Name; }
        }
        /// <summary>
        /// get all version
        /// </summary>
        /// <returns></returns>
        public static AndroidVersion[] GetVersions() {
            return sm_versions.ToArray();
        }
        

        public static AndroidVersion GetVersion(int number)
        {
            foreach (var item in GetVersions())
            {
                if (item.m_Number  == number)
                    return item;
            }
            return null;
        }
        public override string ToString()
        {
            return string.Format("android:{0}-({1})", this.m_Number, this.m_Name);
        }

       /// <summary>
       /// load android version from resources files
       /// </summary>
        private static void LoadVersions()
        {
           var meth=  MethodInfo.GetCurrentMethod();
            string m = CoreResources.GetResourceString("resources/android.versions.xml", 
               meth.DeclaringType.Assembly);

            if (m == null)
                return;
            StringReader v_sreader = new StringReader(m);
            XmlReader v_reader = XmlReader.Create(v_sreader);
            bool v_good = false;
            try
            {               
                while (v_reader.Read())
                {
                    switch (v_reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            switch (v_reader.Name)
                            {
                                case "androidPlatformTargets":
                                    v_good = true;
                                    break;
                                default:
                                    if (v_good && (v_reader.Name == "item"))
                                    {
                                        AndroidVersion vr = new AndroidVersion()
                                        {
                                            m_Number = Convert.ToInt32(v_reader.GetAttribute("versionNumber")),
                                            m_Name = v_reader.GetAttribute("name")
                                        };
                                        sm_versions.Add(vr);
                                    }
                                    break;
                            }
                            break;
                    }

                }
            }
            catch(Exception ex)
            {
                CoreLog.WriteError(ex.Message);
            }
            finally
            {
                if (v_reader !=null)
                v_reader.Close();
            }
        }
       /// <summary>
       /// android version name
       /// </summary>
        private AndroidVersion() { 
        }

    }
}
