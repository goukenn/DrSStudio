using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.IO
{
    public static class CoreIOExtensions
    {
        public static MemoryStream ToMemoryStream(this byte[] t)
        {
            if ((t != null) && (t.Length > 0))
            {
                MemoryStream _s = new MemoryStream();
                BinaryWriter binW = new BinaryWriter(_s);
                binW.Write(t, 0, t.Length);
                binW.Flush();
                _s.Seek(0, SeekOrigin.Begin);//t.ToMemoryStream();
                return _s;
            }
            return null;

        }

        /// <summary>
        /// write to memory stream
        /// </summary>
        /// <param name="inS">input stream must be readable</param>
        /// <param name="oStream">out strteam must be writable</param>
        /// <param name="bufferSize"></param>
        public static void WriteTo(this Stream inS, Stream oStream, int bufferSize=4096)
        {
            if (!inS.CanRead)
                return;
            Byte[] t = new byte[bufferSize];
            int i = 0;
            while ((i = inS.Read(t, 0, t.Length)) > 0)
            {
                oStream.Write(t, 0, i);
            }
        }
        
    }
}
