

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: OpenResources.cs
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
file:OpenResources.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.ResourcesManager.Menu
{
    using IGK.ICore.WinCore;
    using IGK.ICore;
    using IGK.ICore.Menu;
    using System.Windows.Forms;
    using System.IO;
    [CoreMenu("Tools.ExtractResourcesToPhpBalafonResource", 10)]
    class ExtractToPhpResource : CoreApplicationMenu 
    {
        protected override bool PerformAction()
        {
            StringBuilder sb = new StringBuilder();
            Dictionary<string, string>.Enumerator e = CoreResourcesManager.GetStrings();

            sb.AppendLine("<?php ");
            Dictionary<string, string> obj = new Dictionary<string, string>();
            List<string> keys = new List<string>();
            while (e.MoveNext())
            {
                var n = e.Current.Key;
                var m = e.Current.Value;

                obj.Add(n, m);
            }
            keys.Sort();
            foreach (string item in keys)
            {
                var n = item;
                var m = obj[item];
                sb.AppendLine(string.Format("R::AddLang(\"{0}\", \"{1}\");", n, m));
            }
            sb.Append("?>");

            using (SaveFileDialog ofd = new SaveFileDialog())
            {
                ofd.Title = "Balafon Lang resources";
                ofd.Filter  = "php file |*.php";
                ofd.FileName = IGK.ICore.Settings.CoreApplicationSetting.Instance.Lang + ".resources";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(ofd.FileName, sb.ToString(), Encoding.UTF8);
                }
            }
            Clipboard.SetText(sb.ToString());
                return true;
        }
    }
}

