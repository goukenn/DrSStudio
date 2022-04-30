

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WinCoreRendererSetting.cs
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
file:CoreRendererSetting.cs
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
namespace IGK.ICore.Settings
{
    using System.ComponentModel;
    using IGK.ICore.WinUI;
    using IGK.ICore.ComponentModel;
    using IGK.ICore;
    /// <summary>
    /// represent a Renderer setting Object
    /// </summary>
    public sealed class CoreRendererSetting : CorePropertySetting, ICoreRendererSetting
    {
        private enuRendererSettingType m_type;
        #region ICoreRendererSetting Members
        public enuRendererSettingType Type
        {
            get { return this.m_type ; }
        }
        #endregion
      
        public CoreRendererSetting(string name, enuRendererSettingType type, object defaultValue):base(name, 
            defaultValue?.GetType (), defaultValue )
        {            
            this.m_type = type;
            TypeConverter v_conv = CoreTypeDescriptor.GetConverter(typeof(Colorf ));
            switch (this.Type)
            {
                case enuRendererSettingType.Font:
                    ICoreFont ft = defaultValue as ICoreFont;
                    if (ft != null)
                    {
                        ft.FontDefinitionChanged += (o, e) =>
                        {
                            OnValueChanged(EventArgs.Empty);
                        };
                    }
                    break;
                case enuRendererSettingType.String :
                    try
                    {
                        Colorf c = (Colorf)v_conv.ConvertFrom(defaultValue);
                        this.m_type = enuRendererSettingType.Color;
                    }
                    catch { 
                    }


                    break;
                case enuRendererSettingType.Color :
                    if (!(defaultValue is Colorf))
                    {
                        defaultValue = (Colorf)v_conv.ConvertFromString(defaultValue.ToString());
                    }
                    break;
                case enuRendererSettingType.Float :
                    if (defaultValue == null)
                    {
                        defaultValue = 0.0f;
                    }
                    else
                    {
                        if (!(defaultValue is float))
                        {
                            defaultValue = Convert.ToSingle(defaultValue.ToString());
                        }
                    }
                    break;
                case enuRendererSettingType.Int :
                    if (defaultValue == null)
                    {
                        defaultValue = 0;
                    }
                    else
                    {
                        if (!(defaultValue is int))
                        {
                            defaultValue = Convert.ToInt32(defaultValue.ToString());
                        }
                    }
                    break;
                default :                   
                    break;
            }
            this.Value = defaultValue;
        }
       
        
        protected override void OnValueChanged(EventArgs eventArgs)
        {
            System.Diagnostics.StackTrace t =  new System.Diagnostics.StackTrace ();
            System.Diagnostics.StackFrame[] frames =  t.GetFrames();
            if (t.GetFrame (2).GetMethod ().IsConstructor )
            {//avoid from constructor
                return ;
            }            
            base.OnValueChanged(eventArgs);
        }
    }
}

