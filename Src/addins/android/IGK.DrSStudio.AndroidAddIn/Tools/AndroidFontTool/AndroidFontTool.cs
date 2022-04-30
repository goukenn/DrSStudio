

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AndroidFontTool.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.WinCore;
using IGK.ICore;
using IGK.ICore.Drawing2D;
using IGK.ICore.Tools;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Android.Tools
{
    [CoreTools("Tool.AndroidFont")]
    public class AndroidFontTool : AndroidToolBase 
    {
        private static AndroidFontTool sm_instance;
        private AndroidTargetInfo m_TargetInfo;

        public AndroidTargetInfo TargetInfo
        {
            get { return m_TargetInfo; }
            set
            {
                if (m_TargetInfo != value)
                {
                    UnregisterFonts();
                    m_TargetInfo = value;
                    RegisterFonts();
                }
            }
        }
        private AndroidFontTool()
        {
        }

        public static AndroidFontTool Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static AndroidFontTool()
        {
            sm_instance = new AndroidFontTool();
        }

        protected override void RegisterBenchEvent(ICoreSystemWorkbench Workbench)
        {
            base.RegisterBenchEvent(Workbench);
            RegisterFonts();
        }

        public void RegisterFonts()
        {//register additional font for platform
            AndroidTargetInfo v_info = this.TargetInfo;
            if (v_info == null)
                return;
            string v_dir = Path.GetFullPath (Path.Combine(SDK, "platforms", v_info.GetAPIName(), "data/fonts"));
            if (Directory.Exists(v_dir))
            {
                System.Drawing.Text.PrivateFontCollection privateFont = new System.Drawing.Text.PrivateFontCollection();
                int c = 0;//privateFont.Families ;

                foreach (string file in Directory.GetFiles(v_dir))
                {
                    privateFont.AddFontFile(file);
                    if (c != privateFont.Families.Length)
                    {
                        CoreApplicationManager.Application.ResourcesManager.RegisterPrivateFont(file);
                        c = privateFont.Families.Length;
                    }
                    else 
                    {
                        CoreLog.WriteLine("Font File Not Loaded : " + file);
                    }
                }
            }
           
            

        }
        public void UnregisterFonts()
        { }
    }
}
