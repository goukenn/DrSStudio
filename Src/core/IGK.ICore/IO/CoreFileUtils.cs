using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace IGK.ICore.IO
{
    public class CoreFileUtils
    {
        /// <summary>
        /// read all byte from stream
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static byte[] ReadAllBytes(Stream stream) {
            BinaryReader binR = new BinaryReader(stream);
            List<byte> g = new List<byte>();
            int buffsize = 4096;
            byte[] t = null;
            while ((t = binR.ReadBytes(buffsize)) != null && (t.Length > 0))
            {
                g.AddRange(t);
            }
            binR.Close();
            return g.ToArray();
        }

        public static void WriteTo(string file,  string content)
        {
            string dir = Path.GetDirectoryName(file);
            if (Directory.Exists(dir)) {
                File.WriteAllText(file, content);

            }
        }

        public static byte[] ReadAllBytes(string file)
        {
            if (File.Exists(file))
                return File.ReadAllBytes(file);
            return null;
        }
        public static string ReadAllFile(string file)
        {
            if (File.Exists(file))
                return File.ReadAllText(file);
            return null;
        }
    }
}
