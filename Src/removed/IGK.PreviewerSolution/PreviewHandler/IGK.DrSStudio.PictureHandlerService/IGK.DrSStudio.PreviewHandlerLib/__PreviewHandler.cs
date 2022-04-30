

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: __PreviewHandler.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV - [2008 - 2014]
author: C.A.D . BONDJE DOUE
file:__PreviewHandler.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections ;
using Microsoft.Win32 ;
//[assembly:System.Security.Permissions.RegistryPermission (System.Security.Permissions.SecurityAction.RequestMinimum,
//    Unrestricted =true )]
namespace IGK.RegisterPreviewerHandler
{
    /// <summary>
    /// represent the previewer handler
    /// </summary>
      class __PreviewHandler
    {
        public static readonly string HKEYLM_RegisterPreviewer = string.Format("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\PreviewHandlers");
        public const string PREVIEWER_PROG = "8895b1c6-b41f-4c1c-a562-0d564250836f";
        private string m_Extension;
        private string m_ApplicationName;
        private Guid m_PreviewerGUID;
        private string m_DisplayName;
        private string m_Icon;
        private string m_Description;
        private string m_FilteType;
        private Guid m_AppID;
        public Guid AppID
        {
            get { return m_AppID; }
            set
            {
                if (m_AppID != value)
                {
                    m_AppID = value;
                }
            }
        }
        public string FilteType
        {
            get { return m_FilteType; }
            set
            {
                if (m_FilteType != value)
                {
                    m_FilteType = value;
                }
            }
        }
public string Description{
get{ return m_Description;}
set{ 
if (m_Description !=value)
{
m_Description =value;
}
}
}
public string Icon{
get{ return m_Icon;}
set{ 
if (m_Icon !=value)
{
m_Icon =value;
}
}
}
public string DisplayName{
get{ return m_DisplayName;}
set{ 
if (m_DisplayName !=value)
{
m_DisplayName =value;
}
}
}
public Guid PreviewerGUID{
get{ return m_PreviewerGUID;}
set{ 
if (m_PreviewerGUID !=value)
{
m_PreviewerGUID =value;
}
}
}
public string ApplicationName{
get{ return m_ApplicationName;}
set{ 
if (m_ApplicationName !=value)
{
m_ApplicationName =value;
}
}
}
public string Extension{
get{ return m_Extension;}
set{ 
if (m_Extension !=value)
{
m_Extension =value;
}
}
}
        public __PreviewHandler(string extension, Guid progId, string filetype)
        {
            this.m_Extension = extension;
            this.m_PreviewerGUID = progId;
            this.m_FilteType = filetype;
        }
        public string getPreviewerName()
        {
            return "{" + this.PreviewerGUID.ToString().ToLower() + "}";
        }
        public void Register()
        { 
            //register previewer
            //RegistryKey k = Registry.LocalMachine.OpenSubKey §.OpenRemoteBaseKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, ".");
            RegistryKey subKey = null;
            //step 1: register extension
            try
            {
                //create extension key
                subKey = Registry.ClassesRoot.CreateSubKey(this.Extension);
                subKey.SetValue(null, this.FilteType);
                subKey.Close();
                subKey = Registry.ClassesRoot.CreateSubKey(this.FilteType);
                RegistryKey shellex = subKey.CreateSubKey("shellex\\{" + PREVIEWER_PROG + "}");
                //RegistryKey c = subKey.CreateSubKey(, RegistryKeyPermissionCheck.ReadWriteSubTree);
                shellex.SetValue(null, this.getPreviewerName());
                shellex.Close();
            }
            catch { }
            finally {
                if (subKey != null)
                    subKey.Close();
            }
            //step 2: register CLSID
            try
            {
                subKey = Registry.ClassesRoot.CreateSubKey ("CLSID\\"+this.getPreviewerName(), RegistryKeyPermissionCheck.ReadWriteSubTree );
                if (!string.IsNullOrEmpty (this.Description)) subKey.SetValue(null, this.Description);
                if (!string.IsNullOrEmpty(this.DisplayName )) subKey.SetValue("DisplayName", this.DisplayName);
                if (!string.IsNullOrEmpty(this.Icon)) subKey.SetValue("Icon", this.Icon);
                subKey.SetValue("APPID", "{6d2b5079-2f0b-48dd-ab7f-97cec514d30b}");
                RegistryKey ckey = subKey.CreateSubKey("InprocServer32");
                ckey.SetValue(null, GetType().Assembly.Location);
                ckey.SetValue("ThreadingModel", "Apartement");
                subKey.CreateSubKey ("ProgID").SetValue (null, this.FilteType);
                subKey.CreateSubKey ("VersionIndependentProgID").SetValue (null, "Version IndependentProgID");
                ckey.Close();
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            finally {
                if (subKey != null)
                    subKey.Close();
            }
            //step 3 register preview handler
            try
            {
                subKey = Registry.LocalMachine.OpenSubKey(HKEYLM_RegisterPreviewer, RegistryKeyPermissionCheck.ReadWriteSubTree);
                subKey.SetValue (getPreviewerName(), this.DisplayName , RegistryValueKind.String );
                //v_ksubKey.SetValue(null, this.DisplayName);
            }
            catch
            {
            }
            finally
            {
                if (subKey !=null)
                subKey.Close();
            }
        }
        public void UnRegister()
        { 
        }
    }
}

