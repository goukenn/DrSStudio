

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AudioVideoAudioCodecTool.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/

using IGK.ICore;
using IGK.ICore.Codec;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;





﻿using IGK.AVIApi.ACM;
using IGK.AVIApi.VCM;

using IGK.ICore.Tools;
using IGK.ICore.WinUI.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.AudioVideo.Tools
{
    [CoreTools("Tool.AudioVideoChooseAudioEncoder")]
    class AudioVideoAudioCodecTool  : CoreToolBase, ICoreWorkingConfigurableObject
    {
        private static AudioVideoAudioCodecTool sm_instance;
        private ACMDriverInfo[] m_drivers;
        private ACMDriverInfo m_selectedCodec;
        private AudioVideoAudioCodecTool()
        {
        }

        public static AudioVideoAudioCodecTool Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static AudioVideoAudioCodecTool()
        {
            sm_instance = new AudioVideoAudioCodecTool();
        }
        protected override void GenerateHostedControl()
        {
            this.HostedControl = null;
            
        }

        public enuParamConfigType GetConfigType()
        {
            return enuParamConfigType.ParameterConfig;
        }

        public ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
        {
            this.m_drivers = AVIApi.ACM.ACMManager.GetDrivers(enuACMDriverFlag.All );
            CoreParameterConfigUtility.LoadConfigurationUtility(this, parameters, this.GetType());
            var g = parameters.AddGroup("codec");
            g.AddItem(new CodecSelector(this, this.m_drivers));
            return parameters;
        }

        public ICoreControl GetConfigControl()
        {
            return null;   
        }

        class CodecSelector :CoreParameterItemBase, ICoreParameterGroupEnumItem
        {
            private AudioVideoAudioCodecTool audioVideoCodecTool;
            private ACMDriverInfo[] vCMDriverInfo;

            public CodecSelector(AudioVideoAudioCodecTool audioVideoCodecTool, ACMDriverInfo[] vCMDriverInfo)
                : base(
                "Codecs", "lb.codec.caption")
            {
                base.ParamType = enuParameterType.EnumType;
                this.audioVideoCodecTool = audioVideoCodecTool;
                this.vCMDriverInfo = vCMDriverInfo;
            }

            public override void Invoke(ICoreWorkingConfigurableObject ctr, object value)
            {
                this.audioVideoCodecTool.SelectedCodec = (ACMDriverInfo)value;
            }
            public object GetSelectedItem()
            {
                return this.audioVideoCodecTool.SelectedCodec;
            }
            public override object GetDefaultValues()
            {
                return this.vCMDriverInfo;
            }
        }


        public ACMDriverInfo SelectedCodec { get { return this.m_selectedCodec; } set {
            if (this.m_selectedCodec != value)
            {
                this.m_selectedCodec = value;
            }
        } }
    }
}
