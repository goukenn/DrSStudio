using IGK.ICore;
using IGK.ICore.Xml;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.ColladaEngine
{
    public class ColladaTypeItem : ColladaTypeBase
    {
        public override string ToString()
        {
            return $"ColladaItem : {this.Name}" ;

        }
        public ColladaTypeItem()
        {
        }
    }
}
