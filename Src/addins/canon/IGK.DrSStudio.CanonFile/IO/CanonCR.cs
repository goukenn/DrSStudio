using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.IO
{

    /// <summary>
    /// represent a 
    /// </summary>
    public class CanonCR
    {
        ///<summary>
        ///public .ctr
        ///</summary>
        private CanonCR()
        {

        }

        public void Save(string outfile) {
        }
        public  static CanonCR OpenFile(string file) {
            CanonCR v_cr = null;

            if (File.Exists(file)) {
                v_cr = new CanonCR();

                return v_cr;

            }
            return null;
        }
    }
}
