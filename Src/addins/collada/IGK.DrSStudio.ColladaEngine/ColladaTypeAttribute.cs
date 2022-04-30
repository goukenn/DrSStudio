using IGK.ICore.Xml;
using IGK.ICore.Xml.XSD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.ColladaEngine
{
    /// <summary>
    /// represent a collada attribute
    /// </summary>
    public class ColladaTypeAttribute : ColladaTypeBase, ICoreXSDAttribute
    {
        public ColladaTypeAttribute()
        {
        }

        public string Default { get; internal set; }
        public string Fixed { get; internal set; }
        public string Form { get; internal set; }
        public string Id { get; internal set; }
        public string Ref { get; internal set; }
        public string Type { get; internal set; }
        public string Use { get; internal set; }

        public override  bool IsRequired =>  this.Use == "required";
        
    }
}
