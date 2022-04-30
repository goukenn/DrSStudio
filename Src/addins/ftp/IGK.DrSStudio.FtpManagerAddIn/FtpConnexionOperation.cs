

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: FtpConnexionOperation.cs
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
file:FtpConnexionOperation.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net ;
namespace IGK.DrSStudio.FtpManagerAddIn.WinUI
{
    /// <summary>
    /// represent connexion operation
    /// </summary>
    public class FtpConnexionOperation
    {
        public static readonly string MkDir = WebRequestMethods.Ftp.MakeDirectory;
        public static readonly string ListDir = WebRequestMethods.Ftp.ListDirectory;
        public static readonly string DownloadFile = WebRequestMethods.Ftp.DownloadFile;
        public static readonly string DeleteFile = WebRequestMethods.Ftp.DeleteFile;        
        public static readonly string GetDateTimeStamp = WebRequestMethods.Ftp.GetDateTimestamp;
        public static readonly string FileSize = WebRequestMethods.Ftp.GetFileSize;
        public static readonly string Rename = WebRequestMethods.Ftp.Rename;
        public static readonly string ListDirectoryDetails = WebRequestMethods.Ftp.ListDirectoryDetails;
        public static readonly string CurrentWorkingDir = WebRequestMethods.Ftp.PrintWorkingDirectory;
        public static readonly string RmDir = WebRequestMethods.Ftp.RemoveDirectory;
        public static readonly string UploadFile = WebRequestMethods.Ftp.UploadFile;
        public static readonly string UploadFileUnique = WebRequestMethods.Ftp.UploadFileWithUniqueName;
    }
}

