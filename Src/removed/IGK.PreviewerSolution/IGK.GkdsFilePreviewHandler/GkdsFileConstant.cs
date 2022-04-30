

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GkdsFileConstant.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV - [2008 - 2014]
author: C.A.D . BONDJE DOUE
file:GkdsFileConstant.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceProcess;
namespace IGK.GkdsFilePreviewHandler
{
    public static class GkdsFileConstant
    {
        public const string SERVICE_NAME = "GkdsFilePreviewService";
        public const string PIPE_NAME = "GkdsFilePreviewPIPE";
        public const string GOOD_PIPE_NAME = "this is GKDS server GkdsFilePreviewPIPE";
       public  static ServiceController GetService(string name)
        {
            foreach (ServiceController item in ServiceController.GetServices())
            {
                if (item.ServiceName == name)
                    return item;
            }
            return null;
        }
    }
}

