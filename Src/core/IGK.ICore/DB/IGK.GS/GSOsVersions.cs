using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.GS
{
    public sealed class GSOsVersions
    {
        public static readonly Version Windows7;
        public static readonly Version Windows8;

        static GSOsVersions(){
            Windows7 = new Version("6.1");
            Windows8 = new Version("6.2");
        }
    }
}
