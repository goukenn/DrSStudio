
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZLibNet;

namespace IGK.testZLibNet
{
    class Program
    {
        static void Main(string[] args)
        {
            string f = @"d:\temp\corelib.zip";
            if (File.Exists(f))
            {
                ExtractZipFile(f, "d:\\temp");
            }

            Console.WriteLine("End");
            Console.ReadLine();
        }
        /// <summary>
        /// extract zip file
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="outFolder"></param>
        /// <returns></returns>
        public static bool ExtractZipFile(string filename, string outFolder)
        {
            if ((File.Exists(filename) == false) || !Directory.Exists(outFolder))
                return false;
            //CoreLog.WriteDebug("ExtractZipFile : " + filename);
            //AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
            //Environment.SetEnvironmentVariable("Path", Environment.GetEnvironmentVariable("Path") + ";D:\\DRSStudio 9.0 Src\\Bin\\x86\\Debug\\Lib");

            ZipReader reader = new ZipReader(filename);
            foreach (ZipEntry item in reader)
            {
                //   Console.WriteLine(string.Format("{0}:{1}", item.Name, item.IsDirectory));
                if (item.IsDirectory)
                {
                    CreateDir(Path.Combine(outFolder, item.Name));
                }
                else
                {
                    string f = Path.Combine(outFolder, item.Name);
                    if (CreateDir(Path.GetDirectoryName(f)))
                    {
                        //extract file
                        Stream fs = File.Create(f);
                        reader.Read(fs);
                        fs.Close();
                    }
                }
            }
            reader.Close();
            return true;
        }
        /// <summary>
        /// check if the current folder exits. if not try to create a new one
        /// </summary>
        /// <param name="folder">folder</param>
        /// <returns>failed</returns>
        public static bool CreateDir(string folder)
        {
            if (Directory.Exists(folder))
                return true;
            DirectoryInfo dirInfo = Directory.CreateDirectory(folder);
            if (dirInfo == null)
            {
                return false;
            }
            return true;
        }
    }
}
