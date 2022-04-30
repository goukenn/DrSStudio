


using IGK.ICore;
/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WinCoreZipReader.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ZLibNet;

namespace IGK.DrSStudio
{
    public class WinCoreZipReader : ICoreZipReader
    {
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
                    if ((callback !=null) && ! callback.Extract (f))
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


        /// <summary>
        /// extract string
        /// </summary>
        /// <param name="inputStream"></param>
        /// <param name="outFolder"></param>
        /// <returns></returns>
        public bool ExtractZipStream(Stream inputStream, string outFolder)
        {
            if ((inputStream == null) || !Directory.Exists(outFolder))
                return false;
            CoreLog.WriteDebug("ExtractZipStream ");
            string g = Path.GetTempFileName();
            BinaryReader h = new BinaryReader(inputStream);
            Byte[] data = new byte[inputStream.Length];
            File.WriteAllBytes(g, data);
            bool r = ExtractZipFile(g, outFolder);
            try
            {
                File.Delete(g);
            }
            catch (Exception ex){
                CoreLog.WriteDebug("file not deleted : " + ex.Message);
            }
            return r;
        }


        public bool ExtractZipData(byte[] data, string outFolder, ICoreZipFileExtractListener callback=null)
        {
            if ((data == null) || (data.Length==0) || !Directory.Exists(outFolder))
                return false;
            CoreLog.WriteDebug("ExtractZipData");
            string g = PathUtils.GetTempFileWithExtension("zip");// Path.GetTempFileName();
            File.WriteAllBytes(g, data);
            bool r = false;
            try
            {
                r = ExtractZipFile(g, outFolder, callback );

            }
            catch (Exception ex)
            {
                CoreLog.WriteDebug("Can't extract data : " + ex.Message);
            }
            finally {
                //delete t emp folder
                if (File.Exists (g))
                    File.Delete(g);
            }
            return r;
        }

        public bool ExtractZipFile(string filename, string outFolder)
        {
            return this.ExtractZipFile(filename, outFolder, null);
        }
    }
}
