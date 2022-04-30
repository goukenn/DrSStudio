

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: PathUtils.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:PathUtils.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
namespace IGK.ICore.IO
{
	using IGK.ICore;using IGK.ICore.Settings ;
    using System.Net;
    public static class PathUtils
    {
        public static string GetTempFileWithExtension(string extension)
        {
            string tfile = Path.GetTempFileName();            
            if (File.Exists(tfile) && !string.IsNullOrEmpty (extension))
            {
                string f = tfile +"."+ extension;
                File.Move(tfile, f);
                return f;
            }
            return tfile;
        }

        public static string GetTempFileWithExtension(string directory, string extension)
        {
            string tfile = Path.GetTempFileName();
            if (File.Exists(tfile) && !string.IsNullOrEmpty(extension))
            {
                string f = Path.Combine (directory, Path.GetFileNameWithoutExtension ( tfile) + "." + extension);
                File.Move(tfile, f);
                return f;
            }
            return tfile;
        }

        /// <summary>
        /// wget file from net work.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static Stream WgetFile(string filename)
        {
            filename = filename.Replace("\0", "");
            MemoryStream mem = new MemoryStream();
            WebClient webClient = new WebClient();
            string temp = Path.GetTempFileName();
            webClient.DownloadFile(filename,temp);
            Byte[] t = File.ReadAllBytes (temp);
            File.Delete (temp);
            mem.Write(t, 0, t.Length);
            mem.Seek(0, SeekOrigin.Begin);
            return mem;
        }
        /// <summary>
        /// return the extension of the name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetExtension(string name)
        {
            if (string.IsNullOrEmpty(name)) return null;
            StringBuilder sb = new StringBuilder();
            int c = name.Length;
            while (c > 0) {
                if (name[c - 1] == '.')
                    break;
                sb.Insert(0, name[c - 1]);
                c--;
            }
            return sb.ToString();
        }

        public static string GetStartupFullPath(string filename)
        {
            string str = string.Format (
                "{0}{1}{2}",
                CoreApplicationManager.StartupPath ,
                System.IO.Path.DirectorySeparatorChar ,
                System.IO.Path.GetFileName(filename));
            return str;
        }
        /// <summary>
        /// check if the current folder exits. if not try to create a new one
        /// </summary>
        /// <param name="folder">folder</param>
        /// <returns>failed</returns>
        public static bool CreateDir(string folder)
        {            
            if (string.IsNullOrEmpty (folder) || Directory.Exists(folder))
                return true;
            DirectoryInfo dirInfo = Directory.CreateDirectory(folder);
            if (dirInfo == null)
            {  
                return false;
            }
            return true ;
        }
        /// <summary>
        /// Resolv the path to system implementation
        /// </summary>
        /// <param name="path">path to be analysed</param>
        /// <returns></returns>
        public static string GetPath(string path)
        {
            if (string.IsNullOrEmpty(path))
                return string.Empty;
            string folder = string.Empty;
            Regex rg = new Regex(@"%(?<name>(\w+))%");
            folder = rg.Replace(path, new MatchEvaluator(RMatch));
            try
            {
                return Path.GetFullPath(folder);
            }
            catch {
                return CoreApplicationManager.StartupPath;
            }
        }
        private static string RMatch(Match m)
        {
            string s = m.Groups["name"].Value;
            return CoreSystemEnvironment.GetEnvironmentValue(s);
           
        }
        /// <summary>
        /// empty the directory
        /// </summary>
        /// <param name="path"></param>
        internal static void EmptyDirectory(string path)
        {
            string[] d = Directory.GetDirectories(path);
            foreach (string item in d)
            {
                try
                {
                    Directory.Delete(item, true);
                }
                catch {
                    CoreLog.WriteDebug("Can't delete folder : " + item);
                }
            }
        }
        public static string GetDisplayName(Environment.SpecialFolder specialFolder)
        {
            IntPtr pidl = IntPtr.Zero;
            try
            {
                HResult hr = SHGetFolderLocation(IntPtr.Zero, (int)specialFolder, IntPtr.Zero, 0, out pidl);
                if (hr.IsFailure)
                    return null;
                //SHFILEINFO shfi;
                if (0 != SHGetFileInfo(
                            pidl,
                            FILE_ATTRIBUTE_NORMAL,
                            out SHFILEINFO shfi,
                            (uint)Marshal.SizeOf(typeof(SHFILEINFO)),
                            SHGFI_PIDL | SHGFI_DISPLAYNAME))
                {
                    return shfi.szDisplayName;
                }
                return null;
            }
            finally
            {
                if (pidl != IntPtr.Zero)
                    ILFree(pidl);
            }
        }
        public static string GetDisplayName(string path)
        {
            //SHFILEINFO shfi;
            if (0 != SHGetFileInfo(
                        Marshal.StringToHGlobalAnsi (path),
                        FILE_ATTRIBUTE_NORMAL,
                        out SHFILEINFO shfi,
                        (uint)Marshal.SizeOf(typeof(SHFILEINFO)),
                        SHGFI_DISPLAYNAME))
            {
                return shfi.szDisplayName;
            }
            return null;
        }
        private const uint FILE_ATTRIBUTE_NORMAL = 0x00000080;
        private const uint SHGFI_DISPLAYNAME = 0x000000200;     // get display name
        private const uint SHGFI_PIDL = 0x000000008;     // pszPath is a pidl
        [DllImport("shell32")]
        private static extern int SHGetFileInfo(IntPtr pidl, uint dwFileAttributes, out SHFILEINFO psfi, uint cbFileInfo, uint flags);
        [DllImport("shell32")]
        private static extern HResult SHGetFolderLocation(IntPtr hwnd, int nFolder, IntPtr token, int dwReserved, out IntPtr pidl);
        [DllImport("shell32")]
        private static extern void ILFree(IntPtr pidl);
        [StructLayout(LayoutKind.Sequential)]
        private struct SHFILEINFO
        {
            public IntPtr hIcon;
            public int iIcon;
            public uint dwAttributes;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szDisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct HResult
        {
            private int _value;
            public int Value
            {
                get { return _value; }
            }
            public Exception Exception
            {
                get { return Marshal.GetExceptionForHR(_value); }
            }
            public bool IsSuccess
            {
                get { return _value >= 0; }
            }
            public bool IsFailure
            {
                get { return _value < 0; }
            }
        }

        public static bool RmFile(string f)
        {

            if (!File.Exists(f))
                return false;
            try
            {
                File.Delete(f);
                return true;
            }
            catch(Exception ex)
            {
                CoreLog.WriteDebug ("Can't remove file : "+ex.Message );
            }
            return false;
        }

        /// <summary>
        /// get the ICore Directory name
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static string GetDirectoryName(string filename)
        {
            string b = Path.GetDirectoryName(filename);
            if (string.IsNullOrEmpty(b))
            { 
#if __ANDROID__
                b = "/";
#else
                return null;
#endif

            }
            return b;
        }

        /// <summary>
        /// get relative path to target folder from source folder
        /// </summary>
        /// <param name="sourcePath"></param>
        /// <param name="targetPath"></param>
        /// <returns></returns>
        public static string GetRelativePath(string sourcePath, string targetPath)
        {
            if (Path.GetPathRoot(sourcePath) != Path.GetPathRoot(targetPath))
                return null;
            StringBuilder sb = new StringBuilder();
            if (targetPath.StartsWith(sourcePath))
            {
                sb.Append("."+ Path.DirectorySeparatorChar  + targetPath.Substring(sourcePath.Length));
            }
            else {
                DirectoryInfo d = new DirectoryInfo(Path.GetDirectoryName(targetPath));
                if (d.Exists)
                {
                    int i = 0;
                    while((d.Parent != null) && (d.Parent.FullName != sourcePath))
                    {
                        sb.Append(".." + Path.DirectorySeparatorChar);
                        d = d.Parent;
                        i++;
                    }
                    sb.Append(Path.GetFileName(targetPath));
                }
            }
            string txt = sb.ToString().Replace ("\\\\", "\\") ;
            
            return txt;
        }

        public static void MoveFile(string filename, string dest, bool overwrite=true)
        {
            if (!File.Exists(filename))
                return;
            if (File.Exists(dest))
                if (overwrite)
                    File.Delete(dest);
                else 
                    return ;
            
            File.Move(filename, dest);
        }
        public static bool CopyFile(string filename, string dest, bool overwrite = true)
        {
            if (!File.Exists(filename))
                return false ;
            if (File.Exists(dest))
                if (overwrite)
                    File.Delete(dest);
                else
                    return false ;

            File.Copy (filename, dest);
            return true;
        }

        /// <summary>
        /// get file content
        /// </summary>
        /// <param name="filename">filename</param>
        /// <param name="createNEw">create new file if file not exits</param>
        /// <returns></returns>
        public static string GetFileContent(string filename, bool createNew =false )
        {
            if (File.Exists(filename))
                return File.ReadAllText(filename);
            if (createNew)
                File.WriteAllText(filename, string.Empty);
            return string.Empty;
        }
        /// <summary>
        /// treat dir to match to 
        /// </summary>
        /// <param name="cwdir"></param>
        /// <returns></returns>
        public static string TreatDirPath(string cwdir)
        {
            if (string.IsNullOrEmpty(cwdir))
                return cwdir;
            if (Path.DirectorySeparatorChar == '\\')
            {
                return cwdir.Replace("/", "\\");
            }
            else {
                return cwdir.Replace("\\", "/");
            }
        }

        public static string FixRelativeDir(this string cwdir)
        {
            if (cwdir == null)
                return null;
            if (cwdir.StartsWith("." + Path.DirectorySeparatorChar))
            {
                cwdir = cwdir.Substring(2);
            }
            return cwdir;
        }

        public static void SaveAsUT8WBOM(string f, string data)
        {
            /*
            StreamWriter sm = new StreamWriter(File.Create(f), Encoding.UTF8);
            sm.Write(sb.ToString());
            sm.Flush();
            sm.BaseStream.Seek(0, SeekOrigin.Begin);

            //remove boom data

            sm.Close();*/
            //BinaryReader binr = new BinaryReader(File.Open(f, FileMode.Open));
            //int s = binr.ReadByte();
            //int o = binr.ReadByte();
            //int m = binr.ReadByte();
            //int sd = binr.ReadByte();


            ////binr.Close();
            //StreamWriter sm  =new StreamWriter (new MemoryStream(),
            //    Encoding.UTF8
            //    //Encoding.Default 
            //    );
            ////sm.Write(data);
            //byte[] tdata = Encoding.Default.GetBytes(data);
            //byte[] c = new byte[] { (byte)'é' };
            //sm.Write(Encoding.UTF8.GetString(new byte[]{ (byte)'é'}));// data);
            ////sm..Write(tdata, 0, tdata.Length);
            //sm.Flush();
            var d = Encoding.UTF8.GetBytes(data);
            BinaryWriter sm = new BinaryWriter(new MemoryStream());
            int offset = 0;
            if (d.Length == data.Length)
            {
                //ansi
                StreamWriter vsm  =new StreamWriter (sm.BaseStream ,
                Encoding.UTF8
            //    //Encoding.Default 
                    );
                vsm.Write(data);
                vsm.Flush();
                offset = 3;
       
            }
            else
            {
                //utf8 expression
                sm = new BinaryWriter(new MemoryStream());
                sm.Write(d);
                sm.Flush();
            }
            sm.Seek(offset, SeekOrigin.Begin);
            //offset for bom
            //3;// Encoding.Default == Encoding.UTF8 ? 3 : 0;//if encoding in utf 8 = offset is 3
           
            byte[] rdata = new byte [ Math.Max (0, sm.BaseStream .Length - offset)];            
            sm.BaseStream.Read(rdata, 0, rdata.Length);
            sm.Close();
            File.WriteAllBytes(f, rdata);

            /*

            byte[] tdata = Encoding.UTF8.GetBytes(data);
            try
            {
                var r = File.Open(f, FileMode.Create);
                BinaryWriter binw = new BinaryWriter(r, Encoding.UTF8);
                binw.Write(tdata, 0, tdata.Length);
                binw.Close();
            }
            catch(Exception ex) {
                CoreLog.WriteLine(ex.Message);
            }*/
        }
    }
}

