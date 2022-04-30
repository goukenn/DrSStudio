

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AudioVideoCodecTool.cs
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





﻿using IGK.AVIApi.VCM;

using IGK.ICore.Tools;
using IGK.ICore.WinUI.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.AudioVideo.Tools
{
    [CoreTools("Tool.AudioVideoChooseVideoEncoder")]
    class AudioVideoCodecTool  : CoreToolBase, ICoreWorkingConfigurableObject
    {
        private static AudioVideoCodecTool sm_instance;
        private AVIApi.VCM.VCMDriverInfo[] m_drivers;
        private VCMDriverInfo m_selectedCodec;
        private AudioVideoCodecTool()
        {
        }

        public static AudioVideoCodecTool Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static AudioVideoCodecTool()
        {
            sm_instance = new AudioVideoCodecTool();
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
            this.m_drivers = AVIApi.VCM.VCMManager.GetInstalledDrivers();
            CoreParameterConfigUtility.LoadConfigurationUtility(this, parameters, this.GetType());
            var g = parameters.AddGroup("codec");
            g.AddItem(new CodecSelector(this, this.m_drivers));
            return parameters;
        }

        public ICoreControl GetConfigControl()
        {
            return null;   
        }

        class CodecSelector : CoreParameterItemBase, ICoreParameterGroupEnumItem
        {
            private AudioVideoCodecTool audioVideoCodecTool;
            private AVIApi.VCM.VCMDriverInfo[] vCMDriverInfo;

            public CodecSelector(AudioVideoCodecTool audioVideoCodecTool, AVIApi.VCM.VCMDriverInfo[] vCMDriverInfo):base(
                "Codecs", "lb.codec.caption")
            {
                base.ParamType = enuParameterType.EnumType;
                this.audioVideoCodecTool = audioVideoCodecTool;
                this.vCMDriverInfo = vCMDriverInfo;
            }

            public override void Invoke(ICoreWorkingConfigurableObject ctr, object value)
            {
                this.audioVideoCodecTool.SelectedCodec = (VCMDriverInfo)value;
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


        public VCMDriverInfo SelectedCodec { get { return this.m_selectedCodec; } set {
            if (this.m_selectedCodec != value)
            {
                this.m_selectedCodec = value;
            }
        } }
    }
}
