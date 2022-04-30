

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WiXUtils.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
using IGK.ICore;
using IGK.ICore.Codec;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.DrSStudio;
using IGK.DrSStudio.Menu;
using IGK.DrSStudio.WinUI;
using IGK.DrSStudio.Drawing2D;
using IGK.DrSStudio.Drawing2D.WinUI;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:WiXUtils.cs
*/
using IGK.DrSStudio.WiXAddIn.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace IGK.DrSStudio.WiXAddIn
{
    /// <summary>
    /// represent a wix utility class
    /// </summary>
    public static class WiXUtils
    {
        static readonly string[] EXTENSIONS = new string[]{
            "WixUIExtension",
            "WixUtilExtension",
            "WiXNetFxExtension"
        };
        internal static object GetStringValue(object p)
        {
            if (p == null)
                return null;
            if (p is IWiXValue)
                return (p as IWiXValue).getValue();
            return p.ToString();
        }
        public static string getCommandLine(string arg)
        { 
            StringBuilder sb = new StringBuilder ();
            for (int i = 0; i < EXTENSIONS.Length; i++)
			{
                sb.Append ("-ext "+EXTENSIONS[i] + " ");
			}
            sb.Append (arg);
            return sb.ToString ();
        }
        public static string CANDLE {
            get {
                return IGK.ICore.IO.PathUtils.GetPath(Path.Combine(WiXInstallerSetting.Instance.WiXFolder, "candle.exe"));
            }
        }
        public static string Light {
            get {
                return IGK.ICore.IO.PathUtils.GetPath(Path.Combine(WiXInstallerSetting.Instance.WiXFolder, "light.exe"));
            }
        }
        internal static bool CheckEnvironment()
        {
            string v_candle = CANDLE;
            string v_light = Light;
            if (!System.IO.File.Exists(v_candle) || !System.IO.File.Exists(v_light))
            {             
                return false;
            }
            return true;
        }
        internal static string GetSourceDir(string dir, string v_target)
        {
            v_target = v_target.Replace(dir, "");
            while (v_target.StartsWith(Path.DirectorySeparatorChar.ToString()))
            {
                v_target = v_target.Substring(1);
            }
            return v_target;
        }

        internal static void DirectoryAccess(WiXDirectoryComponent component,
            WiXFeatureEntry wiXFeatureEntry, WiXPermission wiXPermission)
        {
            //
            //- set permissions to the application folder permissions
            //
            WiXCreateFolder fr = new WiXCreateFolder();
            fr.Id = null;
            fr.Children.Add(wiXPermission);
            component.Children.Add(fr);
        }
    }
}

