using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.DrSStudio.Drawing3D.FileBrowser
{

    class FBAddInTheChecker
    {
        static FBAddInTheChecker()
        {
        }
        public static bool Check(bool showLog)
        {
            try
            {
                return true;
            }
            catch
            {
            }
            return false;
        }
    }


}
