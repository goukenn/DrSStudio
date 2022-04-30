

using IGK.ICore;
/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AndroidTargetInfo.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Android
{
    /// <summary>
    /// represent a target info
    /// </summary>
    public class  AndroidTargetInfo
    {
        private int m_TargetId;
        private string m_TargetName;
        private Dictionary<string, string> m_info;

        public int APILevel {
            get {
                const string APILEVEL = "API level";
                if (m_info.ContainsKey(APILEVEL))
                { 
                    var s = m_info[APILEVEL];
                    return Convert.ToInt32 (s);
                }
                return 0;
            }
        }
        /// <summary>
        /// Get the tname
        /// </summary>
        public string Name
        {
            get { return GetInfo("Name"); }
        }

        public override string ToString()
        {
            return string.Format("AndroidTarget [{0}:{1}:{2}]", this.TargetId, this.TargetName, this.Name);
        }
        public string TargetName
        {
            get { return m_TargetName; }
            set
            {
                if (m_TargetName != value)
                {
                    m_TargetName = value;
                }
            }
        }
        /// <summary>
        /// get or set the target id
        /// </summary>
        public int TargetId
        {
            get { return m_TargetId; }
            set
            {
                if (m_TargetId != value)
                {
                    m_TargetId = value;
                }
            }
        }
        /// <summary>
        /// .ctr target info
        /// </summary>
        internal AndroidTargetInfo()
        {
            this.m_info = new Dictionary<string, string>();
        }
        public string this[string key] {
            get {
                if (this.m_info.ContainsKey (key))
                    return this.m_info[key];
                return string.Empty;
            }
            set {
                if (string.IsNullOrEmpty(key))
                    return;
                if (this.m_info.ContainsKey(key))
                {
                    if (value == null)
                        this.m_info.Remove(key);
                    else
                        this.m_info[key] = value;
                }
                else {
                    if (value != null)
                        this.m_info.Add(key, value);
                }
            }
        }

        /// <summary>
        /// get the api name information
        /// </summary>
        /// <returns></returns>
        public string GetAPIName()
        {
            if (Regex.IsMatch(this.TargetName, "android-[0-9]+", RegexOptions.IgnoreCase))
            {
                return this.TargetName;
            }
            string[] h = this.TargetName.Trim().Split(':');
            return "android-" + h[h.Length-1];
        }
        /// <summary>
        /// get adnroid version name
        /// </summary>
        /// <returns></returns>
        public AndroidVersion GetVersion() 
        {
            string p = this.GetAPIName();
            if (!string.IsNullOrEmpty(p))
            {
                try
                {
                    var number = p.Split('-')[1];
                    if (number == "L")
                    {
                        return AndroidVersion.LPreviewVersion;
                    }
                    int i = Convert.ToInt32(number);
                    return AndroidVersion.GetVersion(i);
                }
                catch (FormatException)
                {
                    return AndroidVersion.GetVersion(-1);
                }
                catch (Exception ex){
                    CoreLog.WriteError(ex.Message);
                    return null;
                }
               
            }
            else {
                return AndroidVersion.GetVersion(0);
            }
        }

        /// <summary>
        /// get the info name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string GetInfo(string name)
        {
            if (this.m_info.ContainsKey(name))
                return this.m_info[name];
            return string.Empty;
        }
    }
}
