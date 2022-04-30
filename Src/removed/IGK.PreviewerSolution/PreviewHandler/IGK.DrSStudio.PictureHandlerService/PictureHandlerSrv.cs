

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: PictureHandlerSrv.cs
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
file:PictureHandlerSrv.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceProcess;
using System.Threading;
namespace IGK.DrSStudio.PictureHandlerService
{
    public class PictureHandlerSrv : ServiceBase 
    {
        PipeServerRuner service;
        public PictureHandlerSrv()
        {
            this.ServiceName = PictureHandlerConstant.SERVICE_NAME;
        }
        protected override void OnStart(string[] args)
        {
            base.OnStart(args);
        }
        protected override void OnCustomCommand(int command)
        {
            switch ((enuPictureHandlerCommand)command)
            { 
                case enuPictureHandlerCommand.GetBitmap :
                    service = new PipeServerRuner(this);
                    service.Run();
                    break;
                case enuPictureHandlerCommand.Get2DDocument :
                    break;
            }
        }
        protected override void OnShutdown()
        {
        }
    }
}

