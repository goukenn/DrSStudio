using IGK.ICore.Drawing2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.Codec
{

    [AttributeUsage( AttributeTargets.Property, AllowMultiple =false , Inherited = false )]
    public class CoreXMLDefaultAttributeBrushValueAttribute : CoreXMLDefaultAttributeValueAttribute 
    {
        public CoreXMLDefaultAttributeBrushValueAttribute(string definition):base(definition)
        {

        }

        public override bool IsDefaultValue(object value)
        {
            if (value is ICoreBrush)
            {
                string s = (value as ICoreBrush).GetDefinition();
                return this.Value.ToString() == s;
            }
            return false;// base.IsDefaultValue(value);
        }
    }
}
