using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.ColladaEngine
{
    public interface IColladaEngine
    {
        void Import(ICoreWorkbench bench, string file);
        bool Export(string file);
    }
}
