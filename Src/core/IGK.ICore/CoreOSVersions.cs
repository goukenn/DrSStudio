using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore
{
    public class CoreOSVersions
    {
        public static readonly Version Windows8;
        public static readonly Version Windows7;

        static CoreOSVersions() {
            Windows7 = new Version("6.1");
            Windows8 = new Version("6.2");
        }
    }
}
