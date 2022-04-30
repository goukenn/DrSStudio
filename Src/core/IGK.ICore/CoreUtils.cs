

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreUtils.cs
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
file:CoreUtils.cs
*/
using System;
using System.Collections.Generic;
using System.Text;
using IGK.ICore;using IGK.ICore.WinUI;
using System.IO;
using IGK.ICore.Resources;
using System.Text.RegularExpressions;
using System.Reflection;

namespace IGK.ICore
{
    /// <summary>
    /// represent a base core utility class. this is a static class.
    /// </summary>
    public static class CoreUtils
    {
        /// <summary>
        /// get if this application is running in a console mode
        /// </summary>
        /// <returns></returns>
        public static bool IsInConsoleMode()
        {
            bool v_consMode = false;
            try
            {
#if !__ANDROID__
                int i = Console.BufferHeight;
                v_consMode = i > 0;
#endif
            }
            catch
            {
                CoreLog.WriteLine("Not in console mode");
            }
            return v_consMode;
        }

        /// <summary>
        /// get the id of the current surface
        /// </summary>
        /// <param name="surface"></param>
        /// <returns></returns>
        public static string GetSurfaceId(IGK.ICore.WinUI.ICoreWorkingSurface surface)
        {
            if (surface == null)
                return null;
            if (Attribute.GetCustomAttribute(surface.GetType(), typeof(CoreSurfaceAttribute)) is CoreSurfaceAttribute attr)
                return attr.Name;
            return null;
        }

        public static int GetMaxId(string regex, IEnumerable<string> keys, CoreMaxIdCallBack callback)
        {
            int c = 1;
            foreach (var item in keys)
            {
                var m = Regex.Match(item, regex,
                     RegexOptions.IgnoreCase);
                if (m.Success)
                {
                    if (callback != null)
                    {
                        c = callback(c, m);
                    }
                    else
                        c = Math.Max(c, m.Groups["v"].Value.CoreGetValue<int>());
                    c++;
                }
            }
            return c;
        }
        /// <summary>
        /// get the shortcut string presentation
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public static string GetShortcutText(enuKeys keys)
        {
            StringBuilder sb = new StringBuilder();
            if ((keys & enuKeys.Control) == enuKeys.Control)
            {
                if (sb.Length > 0)
                    sb.Append("+");
                sb.Append("Ctrl");
                keys -= enuKeys.Control;
            }
            if ((keys & enuKeys.Alt) == enuKeys.Alt)
            {
                if (sb.Length > 0)
                    sb.Append("+");
                sb.Append("Alt");
                keys -= enuKeys.Alt;
            }
            if ((keys & enuKeys.Shift) == enuKeys.Shift)
            {
                if (sb.Length > 0)
                    sb.Append("+");
                sb.Append("Shift");
                keys -= enuKeys.Shift;
            }
            if (sb.Length > 0)
                sb.Append("+");
            if (keys != enuKeys.None)
                sb.Append(keys.ToString());
            return sb.ToString();
        }

        public static string GetFileName(this ICoreWorkingSurface surface)
        {
            if (surface == null)
                return string.Empty;
            if (surface is ICoreWorkingFilemanagerSurface)
                return (surface as ICoreWorkingFilemanagerSurface).FileName;
            return surface.Title;
        }

        public static byte[] GetRessourceData(string filename)
        {
#if DEBUG
            if (File.Exists(filename))
            {
                return File.ReadAllBytes(filename);
            }
            return null;
#else
            return CoreResources.GetResource (Path.GetFileNameWithoutExtension(filename));
#endif         
        }

        public static void LoadEmbededLibrary(Type type)
        {
            Assembly v_asm = type.Assembly;
            if (Assembly.GetEntryAssembly() == v_asm)
            {
                //init entry assembly
                string[] t = v_asm.GetManifestResourceNames();
                //extract zlib to output folder
                string n = string.Empty;
                int subidx = type.Namespace.Length + 1;
                string fn = string.Empty;
                for (int i = 0; i < t.Length; i++)
                {
                    n = t[i];

                    if (n.EndsWith(".dll") && (n.Length > subidx) &&
                        !File.Exists(fn = Path.Combine(Path.GetDirectoryName(v_asm.Location), n.Substring(subidx))))
                    {

                        //fn = Path.Combine(v_asm.Location, n.Substring(GetType().Namespace.Length + 1));


                        byte[] buffer = new byte[4096];
                        int c = 0;
                        using (BinaryWriter bw = new BinaryWriter(File.OpenWrite(
                            fn)))
                        {
                            using (BinaryReader sm = new BinaryReader(v_asm.GetManifestResourceStream(t[i])))
                            {
                                while ((c = sm.Read(buffer, 0, buffer.Length)) > 0)
                                {
                                    bw.Write(buffer, 0, c);
                                }
                            }
                        }
                    }
                }

            }
        }
    }
}

