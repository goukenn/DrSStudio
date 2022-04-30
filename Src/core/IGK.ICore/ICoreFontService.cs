using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore
{
   public  delegate void ICoreFontServiceCallback(Vector2f[] f, byte[] t);

    public interface ICoreFontService
    {
        bool ExtractGlyfFont(string filename,string outfile, bool bitmap, ICoreFontServiceCallback callback);
    }
}
