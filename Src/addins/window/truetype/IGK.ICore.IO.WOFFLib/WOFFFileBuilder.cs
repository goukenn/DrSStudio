using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.IO
{
    class WOFFFileBuilder
    {
        private WOFFFile m_file;

        ///<summary>
        ///public .ctr
        ///</summary>
        public WOFFFileBuilder(WOFFFile file)
        {
            this.m_file = file;
        }

        public void SetGaspRange(WOFFFileGasp[] r)
        {
            //add gasp range
        }
    }

 
}
