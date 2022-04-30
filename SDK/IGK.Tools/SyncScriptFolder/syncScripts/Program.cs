using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;


/// <summary>
/// Syncronize lib 
/// </summary>
namespace syncScripts
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new MainForm());

            SyncFolder
                (@"D:\DRSStudio 9.0 Src\Src\core\IGK.ICore",
                @"D:\dev\2017\XboxOnee\IGK.ICoreWPF");

            Console.WriteLine("done");
            Console.ReadLine();
        }

        private static void SyncFolder(string srcFolder, string outFolder)
        {
            int s = srcFolder.Length;
            foreach (string i in Directory.GetFiles(srcFolder, "*.cs", SearchOption.AllDirectories )) {
                var h = Path.GetDirectoryName(i).Substring(s);
                if (!string.IsNullOrEmpty(h)) {
                    h = h.Replace("\\", "/");//.Remove("\\");
                    if (h[0] == '/')
                        h = h.Substring(1);
                }
                string od = Path.Combine(outFolder, h);
                if (!Directory.Exists (od))
                    Directory.CreateDirectory(od);

                string of = Path.Combine(od, Path.GetFileName(i));
                //Console.WriteLine(of);
                File.Copy(i, of, true);
                
            }


        }
    }
}
