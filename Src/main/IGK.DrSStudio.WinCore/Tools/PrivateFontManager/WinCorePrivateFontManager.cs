

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WinCorePrivateFontManager.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.WinCore;
using IGK.ICore;
using IGK.ICore.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Tools 
{
    /// <summary>
    /// use to manage private font
    /// </summary>
    [CoreTools("Tool.WinCorePrivateFontManager")]
    public class WinCorePrivateFontManager : CoreToolBase 
    {
        private static WinCorePrivateFontManager sm_instance;
        public Dictionary<string, FontFamily> m_fontFamilies;

        private WinCorePrivateFontManager()
        {
            RegisterPrivateFonts();
        }
        /// <summary>
        /// get registrated font familly
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public FontFamily GetFont(string name)
        {
            if (this.m_fontFamilies.ContainsKey(name))
                return m_fontFamilies[name];
            return null;
        }
        private void RegisterPrivateFonts()
        {
            m_fontFamilies = new Dictionary<string, FontFamily>();
            System.Drawing.Text.PrivateFontCollection privateFont = new System.Drawing.Text.PrivateFontCollection();
            string v_n = string.Empty;
            try
            {
                string dir = CoreApplicationManager.PrivateFontsDirectory;
                if (Directory.Exists(dir))
                {
                    foreach (string v_filename in Directory.GetFiles(dir))
                    {
                        privateFont.AddFontFile(v_filename);
                        FontFamily[] r = privateFont.Families;
                        if ((r != null) && (r.Length > 0))
                        {//register the layst one
                            v_n = r[r.Length - 1].Name;
                            if (!m_fontFamilies.ContainsKey(v_n))
                            {
                                m_fontFamilies.Add(v_n.ToLower(), r[r.Length - 1]);
                                CoreApplicationManager.Application.ResourcesManager.RegisterPrivateFont(v_filename);
                            }
                        }
                    }
                }
            }
            catch
            {
            }
        }
        /// <summary>
        /// get the instance of this WinCorePrivateFontManager
        /// </summary>
        public static WinCorePrivateFontManager Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static WinCorePrivateFontManager()
        {
            sm_instance = new WinCorePrivateFontManager();

        }
    }
}
