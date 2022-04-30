

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: SendMailMenu.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/

using IGK.ICore;
using IGK.ICore.Codec;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.DrSStudio;
using IGK.DrSStudio.Menu;
using IGK.DrSStudio.WinUI;
using IGK.DrSStudio.Drawing2D;
using IGK.DrSStudio.Drawing2D.WinUI;
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
namespace IGK.DrSStudio.WebAddIn.Mail.Menu
{
    //[DrSStudioMenu("Tools.Web.SendMail", 0x20 , ImageKey=CoreImageKeys.MENU_MAIL_GKDS)]
    //public class SendMailMenu : CoreApplicationMenu
    //{
    //    protected override bool PerformAction()
    //    {
    //        int port = CoreConstant.APP_CHANNEL_PORT + 5;
    //        HttpWebRequest request = HttpWebRequest.Create(Uri.UriSchemeHttp + "://localhost:" + port + "?c=mailCtrl&f=viewMailDialog")
    //            as HttpWebRequest ;
    //        HttpWebResponse rep =  null;
    //        bool error = true;
    //        try
    //        {
    //            rep = (HttpWebResponse)request.GetResponse();
    //        }
    //        catch{
    //        }
    //        finally {
    //            if (rep !=null)
    //            {                  
            
            
    //        rep.Close();
    //            }
    //        }

    //        if (error) {

    //            CoreMessageBox.NotifyMessage("Error", "Balafon Mail Message Failed");
    //        }
    //        return false;
    //    }
    //}
}
