

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IPhpResponse.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.Net
{

    /// <summary>
    /// php server query response
    /// </summary>
    public interface  IPhpResponse
    {

        string GetHeaderResponse(string responseTag);
        /// <summary>
        /// run the server response
        /// </summary>
        void Run();
        /// <summary>
        /// get the parent server
        /// </summary>
        IPhpServer Server { get; }

        /// <summary>
        /// get the requested Protocol
        /// </summary>
        string RequestProtocol { get; }
        /// <summary>
        /// get the post data length
        /// </summary>
        int PostDataLength { get; }
        /// <summary>
        /// get the param options
        /// </summary>
        /// <param name="paramKey"></param>
        /// <returns></returns>
        string GetParam(string paramKey);
        /// <summary>
        /// get all enum params actions
        /// </summary>
        /// <param name="data"></param>
        void EnumParams(Action<string, string> data);

        /// <summary>
        /// get the script name
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        string GetScriptName(string filename);
        string GetUriPath(string filename);
        string RequestQuery { get;  }
        string RequestUri { get; }

        bool ParamsContainsKey(string p);
    }
}
