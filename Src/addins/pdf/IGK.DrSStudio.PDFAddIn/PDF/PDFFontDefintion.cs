using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.PDF
{
#pragma warning disable 649

    public struct PDFFontDefinition
    {
        internal int up;
        internal int ut;
        internal int[] cw;

        public static PDFFontDefinition Empty;
        static PDFFontDefinition() {
            Empty = new PDFFontDefinition();
            Empty.up = -100;
            Empty.ut = 30;
            Empty.cw = new int[256];
            for (int i = 0; i < 256; i++)
            {
                Empty.cw[i] = 500;
            }
        }
    }
}
