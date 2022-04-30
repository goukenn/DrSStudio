

using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Actions;
/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreParameterConfigUtility.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace IGK.ICore.WinUI.Configuration
{
    /// <summary>
    /// represent a parameter config utility class
    /// </summary>
    public static  class CoreParameterConfigUtility
    {
        public  static void LoadConfigurationUtility(ICoreWorkingObject owner, ICoreParameterConfigCollections parameters, Type type)
        {
            foreach (PropertyInfo prInfo in CoreConfigurablePropertyAttribute.ConfigurableProperties(type))
            {
                CoreConfigurablePropertyAttribute pt = CoreConfigurablePropertyAttribute.GetCustomAttribute(prInfo);
                if (pt.IsConfigurable)
                {
                    var  group = parameters.AddGroup(pt.Group);
                    group.AddItem(prInfo);
                }
            }

            if (owner  is ICoreBrushOwner)
            {
                var g = parameters.AddGroup("Brushes");
                ICoreBrushOwner br = owner  as ICoreBrushOwner;
                ICoreBrush v_fb = br.GetBrush(enuBrushMode.Fill);
                ICoreBrush v_sb = br.GetBrush(enuBrushMode.Stroke);
                if (((br.BrushSupport & enuBrushSupport.Fill) == enuBrushSupport.Fill)
                    && (v_fb != null))
                    g.AddActions(new EditElementBrushActions("EditFillBrush", br, v_fb, br.BrushSupport));
                if (((br.BrushSupport & enuBrushSupport.Stroke) == enuBrushSupport.Stroke) && (v_sb != null))
                    g.AddActions(new EditElementBrushActions("EditStrokeBrush",
                        br,
                        v_sb,
                        br.BrushSupport));
            }
        }
    }
}
