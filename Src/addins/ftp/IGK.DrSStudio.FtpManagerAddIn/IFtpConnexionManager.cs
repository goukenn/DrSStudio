

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IFtpConnexionManager.cs
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
file:IFtpConnexionManager.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
namespace IGK.DrSStudio.FtpManagerAddIn.WinUI
{
    public interface  IFtpConnexionManager
    {
        #region Socket Methods
        event EventHandler SocketCommandInfoUpdate;
        int SocketStatusCode { get; }
        string SocketStatusMessage { get; }
        Socket CreateNewSocket();
        bool ChangeDir(Socket socket, string newDir);
        string PrintWD(Socket socket);
        #endregion
        string GetCurrentDir();
        bool Rename(string oldname, string newname);
        bool Chmod(string name, string authorization);        
        bool MakeDirs(params string[] dirname);
        bool DeleteDirs(params string[] dirname);
        bool DeleteFiles(params string[] files);
        /// <summary>
        /// upload files
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        bool UploadFile(string file);
        /// <summary>
        /// download file
        /// </summary>
        /// <param name="file">fichier source a copier</param>
        /// /// <param name="destination">destion ou copier le fichier</param>
        /// <returns></returns>
        bool DownloadFile(string file, string destination);
    }
}

