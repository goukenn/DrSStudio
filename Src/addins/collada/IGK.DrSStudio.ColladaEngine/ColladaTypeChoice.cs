using IGK.ICore.Xml.XSD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.ColladaEngine
{
    class ColladaTypeChoice : ColladaTypeBase, IColladaItemContainer, ICoreXSDChoice
    {
        public ColladaTypeChoice()
        {
        }
        public override string ToString()
        {
            return $"{nameof(ColladaTypeChoice)}";
        }
    }
}
