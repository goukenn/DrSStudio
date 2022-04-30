

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreExternalTools.cs
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
file:CoreExternalTools.cs
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
namespace IGK.DrSStudio.Tools
{
    using IGK.ICore.WinCore;
using IGK.DrSStudio.Settings;
    using IGK.DrSStudio.WinUI;
    
    using IGK.ICore;
    using IGK.ICore.Settings;
    using IGK.ICore.Tools;
    using IGK.ICore.WinUI;
    [CoreTools("Tool.ExternalToolManager")]
    class CoreExternalTools : CoreToolBase 
    {
        private static CoreExternalTools sm_instance;
        private CoreExternalTools()
        {
        }
        public static CoreExternalTools Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static CoreExternalTools()
        {
            sm_instance = new CoreExternalTools();
            RegisterExtraCodec();
        }
        internal  static void RegisterExtraCodec()
        {
            foreach (KeyValuePair<string, ICoreSettingValue> t in CoreExternalToolSetting.Instance )
            {
                CoreSystem.RegisterExtraDecoder (t.Value.Name, t.Value.Value as string );
            } 
        }
        internal static void ClearExtraCodec()
        {
            CoreSystem.ClearExtraDecoderList();
        }
        protected override void GenerateHostedControl()
        {
            this.HostedControl = null;
        }
       
    }
    }

