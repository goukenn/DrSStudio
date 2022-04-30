

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IPhpServer.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿

using System.Net;
namespace IGK.Net
{
    public interface IPhpServer {
        int Port { get; }
        string Version { get; }
        bool IsInWebContext { get;  }
        string DocumentRoot { get;  }
        /// <summary>
        /// get location uri
        /// </summary>
        string Location { get; }
        string FileName { get;  }

        bool IsInAppContext { get;  }

        EndPoint ServerAddress { get; }
        /// <summary>
        /// get mime type from extension
        /// </summary>
        /// <param name="extension"></param>
        /// <returns></returns>
        string GetMimeTypeFromExtension(string extension);

        bool HandleResponse { get; }

        void SendResponse(PhpResponseBase phpAsyncResponse);
    }
}