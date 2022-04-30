

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Program.cs
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
file:Program.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Diagnostics;
using System.Windows.Forms;
namespace IGK.DrSStudio.PreviewHandler
{
    class Program
    {
        static void Main(string[] args)
        {
            //UnregisterPreviewHandler(".gkds", "drstudio-picturefile",true );
            //RegisterPreviewHandler(".gkds", "drstudio-picturefile", true);
            using (MainForm frm = new MainForm())
            {
                Application.Run(frm);
            }
            Console.WriteLine("end");
        }
        const string ISPREVIEWHANDLER_CLSID = "{8895b1c6-b41f-4c1c-a562-0d564250836f}";
        const string GKDS_PREVIEW_CLSID = "{73E5E190-65AC-4E51-A47C-30093852D68B}";
        const string GKDS_APP_PREVIEWID = "{4D312754-42B6-45B3-A38E-854AC692F7E4}";
        const string FOR_32_BIT = "{534A1E02-D58F-44f0-B58B-36CBED287C7C}";
        public static RegistryKey CreateOrOpen(RegistryKey key, string name)
        {
            if (key == null)
                return null;
            string[] t = name.Split('/');
            if (t.Length == 1)
            {
                RegistryKey vkey = key.OpenSubKey(name, RegistryKeyPermissionCheck.ReadWriteSubTree, System.Security.AccessControl.RegistryRights.FullControl);
                if (vkey == null)
                {
                    vkey = key.CreateSubKey(name, RegistryKeyPermissionCheck.ReadWriteSubTree);
                }
                return vkey;
            }
            else { 
                RegistryKey ky = CreateOrOpen (key , t[0]);
                string[] g = new string[t.Length -1];
                Array.Copy (t,1, g, 0, g.Length );
                string vh = string.Join ("/",g);
                return CreateOrOpen(ky, vh);
            }
        }
        public static void UnregisterPreviewHandler(string extension, string name, bool foralluser)
        {
            RegistryKey key = null;
            if (foralluser)
            {
                key = Registry.LocalMachine;
            }
            else
            {
                key = Registry.CurrentUser;
            }
            key.DeleteSubKeyTree("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\PreviewHandlers\\" + GKDS_PREVIEW_CLSID,false );
            Registry.ClassesRoot.DeleteSubKeyTree("CLSID\\" + GKDS_PREVIEW_CLSID, false );
            Registry.ClassesRoot.DeleteSubKeyTree(name,false );
            Registry.ClassesRoot.DeleteSubKeyTree(extension,false );
        }
        /// <summary>
        /// manual register the preview handler
        /// </summary>
        public static void RegisterPreviewHandler(string extension, string name, string path, bool foralluser)
        {
            RegistryKey key = CreateOrOpen(Registry.ClassesRoot, extension);
           Debug.WriteLine (key);
           key.SetValue(null, name);
           key.Close();
           using (key =CreateOrOpen(Registry.ClassesRoot, name+"/shellex/" + ISPREVIEWHANDLER_CLSID))
           {
               key.SetValue(null, GKDS_PREVIEW_CLSID);
           }
           using (key = CreateOrOpen(Registry.ClassesRoot, "CLSID/" + GKDS_PREVIEW_CLSID))
           {
               key.SetValue(null, "IGKDS PREVEIEW HANDLER");
               key.SetValue("DisplayName", "MyPreviewHandler");
               key.SetValue("Icon", "");
               key.SetValue("AppID", "{6d2b5079-2f0b-48dd-ab7f-97cec514d30b}");
               using (RegistryKey ck = CreateOrOpen (key, "InprocServer32"))
               {
                   ck.SetValue(null, path);
                   ck.SetValue("ThreadingModel", "Apartment");
                   ck.SetValue("ProgID", name);
                   ck.SetValue("VersionIndependentProgID", "Version IndependentProgID");
               }
           }
            //Register all preview HANDLE FOR ALL USERS
           if (foralluser)
           {
               key = Registry.LocalMachine;
           }
           else {
               key = Registry.CurrentUser;
           }
           using (RegistryKey ckey = CreateOrOpen(key, "SOFTWARE/Microsoft/Windows/CurrentVersion/PreviewHandlers/" + GKDS_PREVIEW_CLSID))
           {
               ckey.SetValue(null, "IGKDS PREVEIEW HANDLER");
           }
        }
    }
}

