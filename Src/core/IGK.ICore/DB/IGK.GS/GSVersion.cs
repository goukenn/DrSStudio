using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.GS
{
    /// <summary>
    /// represent a gs version code
    /// </summary>
    public  class GSVersion
    {
        public static readonly GSVersion Demo;
        public static readonly GSVersion Free;
        public static readonly GSVersion Professional;
        public static readonly GSVersion Debugging;

        private int p;
        

        static GSVersion() {
            Demo = new GSVersion(-2);
            Professional = new GSVersion(5);
            Free = new GSVersion(1);
            Debugging = new GSVersion(0);
        }

        private GSVersion(int p)
        {
            this.p = p;
        }
        public int VersionNumber { get { return this.p; }  }

    }
}
