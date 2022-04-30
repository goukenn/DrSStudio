using IGK.ICore.IO;
using IGK.ICore.Settings;
using IGK.ICore.WebSdk.Setting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZLibNet;

namespace IGK.ICore.WebSdk
{
    [CoreAddInInitializer (true )]
    /// <summary>
    /// install the skd 
    /// </summary>
    class CoreWebSDKInstaller : ICoreInitializer 
    {
        private const string SDK_FOLDER = "Sdk";

        public bool Initialize(CoreApplicationContext context)
        {
            var s = CoreSettings.GetSetting("WebSdkSetting.Installed").CoreGetValue<bool>();
            var d = Path.Combine (context.StartupFolder, SDK_FOLDER);
#if !DEBUG
            //if (Directory.Exists(d) && s)
            //    return true;
#endif
            //if (!context.IsAssemblyLoaded(CoreConstant.ZIP_READER_ASM))
            //{
            //    return false ;
            //}

            //bool r = CoreZipFile.ExtractZipData(Properties.Resources.Sdk,
            //    context.StartupFolder,
            //    new ZipExtract()

            //    );

            bool r = ExtractZipData(Properties.Resources.Sdk,
              d,
              new ZipExtract()
              );
            CoreWebSdkInstallerSetting.Instance.Installed = true ;
            CoreWebSdkInstallerSetting.Store();
            return r;
        }

        public bool UnInitilize(CoreApplicationContext context)
        {
            return false;
        }

        class ZipExtract : ICoreZipFileExtractListener
        {
            public bool Extract(string filename)
            {
#if DEBUG
                return true;
#else 
                if (!File.Exists(filename))
                {
                    return true;
                }
                return false;
#endif
            }
        }


        public bool ExtractZipData(byte[] data, string outFolder, ICoreZipFileExtractListener callback = null)
        {
            if ((data == null) || (data.Length == 0) || !PathUtils.CreateDir (outFolder))
                return false;
            CoreLog.WriteDebug("ExtractZipData");
            string g = PathUtils.GetTempFileWithExtension("zip");// Path.GetTempFileName();
            File.WriteAllBytes(g, data);
            bool r = false;
            try
            {
                r = ExtractZipFile(g, outFolder, callback);

            }
            catch (Exception ex)
            {
                CoreLog.WriteDebug("Can't extract data : " + ex.Message);
            }
            finally
            {
                //delete t emp folder
                if (File.Exists(g))
                    File.Delete(g);
            }
            return r;
        }

        public bool ExtractZipFile(string filename, string outFolder)
        {
            return this.ExtractZipFile(filename, outFolder, null);
        }
        /// <summary>
        /// extract zip file
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="outFolder"></param>
        /// <returns></returns>
        public bool ExtractZipFile(string filename, string outFolder, ICoreZipFileExtractListener callback = null)
        {
            if ((File.Exists(filename) == false) || !Directory.Exists(outFolder))
                return false;
            CoreLog.WriteDebug("ExtractZipFile : " + filename);
          
            ZipReader reader = new ZipReader(filename);
            foreach (ZipEntry item in reader)
            {
                //   Console.WriteLine(string.Format("{0}:{1}", item.Name, item.IsDirectory));
                if (item.IsDirectory)
                {
                    PathUtils.CreateDir(Path.Combine(outFolder, item.Name));
                }
                else
                {
                    string f = Path.Combine(outFolder, item.Name);
                    if ((callback != null) && !callback.Extract(f))
                        continue;
                    if (PathUtils.CreateDir(PathUtils.GetDirectoryName(f)))
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


    }
}
