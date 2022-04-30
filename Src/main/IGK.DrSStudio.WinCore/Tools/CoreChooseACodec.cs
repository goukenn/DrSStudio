

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreChooseACodec.cs
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
file:CoreChooseACodec.cs
*/
/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igkdev.be
Application : DrSStudio
powered by IGK - DEV &copy; 2008-2012
THIS FILE IS A PART OF THE DRSSTUDIO APPLICATION. SEE "License.txt"
FOR MORE INFORMATION ABOUT THE LICENSE
------------------------------------------------------------------- 
*/
﻿using System;
using System.Collections.Generic;
using System.Text;
using IGK.ICore.WinCore;

namespace IGK.DrSStudio.Tools
{
    using IGK.ICore.Codec;
    using IGK.DrSStudio.WinUI;
    using IGK.ICore.WinUI.Configuration;
    using IGK.ICore.WinUI;
    using IGK.ICore;
    public class CoreChooseACodecTool : ICoreWorkingConfigurableObject 
    {
        private ICoreCodec m_Codec;
        private ICoreCodec[] m_codecs;
        public ICoreCodec Codecs
        {
            get { return m_Codec; }
        }
        public CoreChooseACodecTool(ICoreCodec[] codecs)
        {
            if (codecs == null)
                throw new CoreException(enuExceptionType.ArgumentIsNull, "codec");
            if (codecs.Length <1)
                throw new CoreException(enuExceptionType.ArgumentNotValid , "codec");
            this.m_codecs = codecs;
            this.m_Codec = null;
        }
        #region ICoreWorkingConfigurableObject Members
        public enuParamConfigType GetConfigType()
        {
            return enuParamConfigType.ParameterConfig;
        }
        public ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
        {
            ICoreParameterConfigCollections p = parameters;
            ICoreParameterGroup group =  p.AddGroup("Codecs");
            group.AddItem(new CodecsItem(this));
            return p;
        }
        class CodecsItem : CoreParameterItemBase , ICoreParameterGroupEnumItem 
        {
            CoreChooseACodecTool m_owner;
            public override enuParameterType ParamType
            {
                get
                {
                    return base.ParamType;
                }
                protected set
                {
                    base.ParamType = value;
                }
            }
            public CodecsItem(CoreChooseACodecTool c):base("Codec", "lb.Codec.caption")
            {
                this.m_owner = c;
                base.ParamType = enuParameterType.EnumType;
                this.m_owner.m_Codec = GetSelectedItem() as ICoreCodec ;
            }
            public override object GetDefaultValues()
            {
                return this.m_owner.m_codecs;
            }
            #region ICoreParameterGroupEnumItem Members
            public object GetSelectedItem()
            {
                return m_owner.m_codecs[0];
            }
            #endregion
            public override void Invoke(ICoreWorkingConfigurableObject ctr, object value)
            {
                this.m_owner.m_Codec = value as ICoreCodec;
            }
        }
        public  ICoreControl GetConfigControl()
        {
            throw new NotImplementedException();
        }
        #endregion
        #region ICoreIdentifier Members
        public string Id
        {
            get { return "ChoseACodec"; }
        }
        #endregion
    }
}

