

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: LayerTransparencyKeyTypeConverter.cs
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
file:LayerTransparencyKeyTypeConverter.cs
*/
using IGK.ICore.WinCore;
using IGK.ICore.Drawing2D;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.ICore.WinCore.ComponentModel
{
    using IGK.ICore;
    /// <summary>
    /// layer transparency type converter
    /// </summary>
    internal class LayerTransparencyTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
                return true;
            return base.CanConvertFrom(context, sourceType);
        }
        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            if (value is string)
            {
                string[] v_str = (value as string).Split(' ');
                if (v_str.Length == 2)
                {
                    return new LayerTransparencyKey(
                           v_str[0].CoreConvertFrom<Colorf>(),
                           v_str[1].CoreConvertFrom<Colorf>());
                }
                return new LayerTransparencyKey();
            }
            return base.ConvertFrom(context, culture, value);
        }
    }
}

