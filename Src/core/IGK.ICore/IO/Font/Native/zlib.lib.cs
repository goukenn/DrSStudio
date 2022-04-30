using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace IGK.IO.Font.Native
{
    internal class zlib
    {
        internal const int Z_OK = 0;

        [DllImport("zlib32.dll")]
        internal static extern int uncompress(byte[] data, ref int length, byte[] source, int slength);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dest"></param>
        /// <param name="destLen"></param>
        /// <param name="source"></param>
        /// <param name="sourceLen"></param>
        /// <param name="level">0 = no compreession, 9 height = compression</param>
        /// <returns></returns>
        [DllImport("zlib32.dll")]
        internal static extern int compress2(byte[] dest, ref int destLen, byte[] source, int sourceLen, int level);

    }
}
