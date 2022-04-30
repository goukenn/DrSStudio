using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Android.IO
{
    /// <summary>
    /// represent a android saver utility class
    /// </summary>
    public static class AndroidResourceSaver
    {
        public static void WriteAllText(string filename, string content)
        {
            FileStream mem = File.Create (filename );
            StreamWriter sw = new StreamWriter(mem, Encoding.UTF8);
            sw.WriteLine(AndroidConstant.ANDROID_XMLDECLARATION);
            sw.Write(content);
            sw.Flush();
            sw.Close();
        }
    }
}
