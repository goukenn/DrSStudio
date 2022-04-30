

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: VectorfConverter.cs
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
file:VectorfConverter.cs
*/

/* 
-------------------------------------------------------------
This file is part of iGK-DEV-DrawingStudio
-------------------------------------------------------------
-------------------------------------------------------------
-------------------------------------------------------------
view license file in Documentation folder to get more info
Copyright (c) 2008-2010 
Author  : Charles A.D. BONDJE DOUE 
mail : bondje.doue@hotmail.com
-------------------------------------------------------------
-------------------------------------------------------------
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
namespace IGK.ICore.ComponentModel
{
    /// <summary>
    /// represent the default vector converter
    /// </summary>
    class VectorfConverter : TypeConverter 
    {
        public VectorfConverter() {  }
        public override bool IsValid(ITypeDescriptorContext context, object value)
        {
            return base.IsValid(context, value);
        }
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
                return true;
            return base.CanConvertFrom(context, sourceType) ;
        }
        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            if (value is string)
            {
                Vector3f obj = new Vector3f();
                string[] t = value .ToString ().Split (new char[]{';','='});
                switch (t.Length)
                {
                    case 3:
                        obj = new Vector3f(float.Parse(t[0]),
                            float.Parse(t[1]),
                            float.Parse(t[2]));
                        break;
                    default:
                        obj = new Vector3f(float.Parse(t[1]),
                            float.Parse(t[3]),
                            float.Parse(t[5]));
                        break;
                }
                return obj;
            }
            return base.ConvertFrom(context, culture, value);
        }
        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                if (value is Vector3f)
                {
                    Vector3f vf = (Vector3f)value;
                    return string.Format("{0};{1};{2}", vf.X, vf.Y, vf.Z);
                }
                if (value is Vector4f)
                {
                    Vector4f vf = (Vector4f)value;
                    return string.Format("{0};{1};{2};{3}", vf.X, vf.Y, vf.Z, vf.W );
                }
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return base.CanConvertTo(context, destinationType);
        }
        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            PropertyDescriptorCollection c = TypeDescriptor.GetProperties(value);
            return c;
        }
    }

    /*
         /// <summary>
    /// represent the default vector converter
    /// </summary>
    class VectorfConverter : TypeConverter 
    {
        public VectorfConverter() {  }
        public override bool IsValid(ITypeDescriptorContext context, object value)
        {
            return base.IsValid(context, value);
        }
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
                return true;
            return base.CanConvertFrom(context, sourceType) ;
        }
        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            if (value is string)
            {
                Vector3f obj = new Vector3f();
                string[] t = value .ToString ().Split (new char[]{';','='});
                switch (t.Length)
                {
                    case 3:
                        obj = new Vector3f(float.Parse(t[0]),
                            float.Parse(t[1]),
                            float.Parse(t[2]));
                        break;
                    default:
                        obj = new Vector3f(float.Parse(t[1]),
                            float.Parse(t[3]),
                            float.Parse(t[5]));
                        break;
                }
                return obj;
            }
            return base.ConvertFrom(context, culture, value);
        }
        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                if (value is Vector3f)
                {
                    Vector3f vf = (Vector3f)value;
                    return string.Format("{0};{1};{2}", vf.X, vf.Y, vf.Z);
                }
                if (value is Vector4f)
                {
                    Vector4f vf = (Vector4f)value;
                    return string.Format("{0};{1};{2};{3}", vf.X, vf.Y, vf.Z, vf.W );
                }
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return base.CanConvertTo(context, destinationType);
        }
        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            PropertyDescriptorCollection c = TypeDescriptor.GetProperties(value);
            return c;
        }
    }*/
}

