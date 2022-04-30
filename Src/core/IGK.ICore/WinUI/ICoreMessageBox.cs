

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreMessageBox.cs
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
file:ICoreMessageBox.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
namespace IGK.ICore.WinUI
{
    /// <summary>
    /// represent a message box
    /// </summary>
    public interface ICoreMessageBox
    {
        /// <summary>
        /// show message
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        enuDialogResult Show(string messageScript);
        /// <summary>
        /// show Core Error
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        enuDialogResult ShowError(CoreException ex);
        /// <summary>
        /// show default exception
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        enuDialogResult Show(Exception ex);

        /// <summary>
        /// show message with title
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        enuDialogResult Show(string message, string title);
        /// <summary>
        /// show exception 
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        enuDialogResult Show(Exception ex, string title);
        /// <summary>
        /// confirm message box
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        enuDialogResult Confirm(string message);

        enuDialogResult ShowError(string title, string message);
        enuDialogResult ShowInfo(string title, string message);
        enuDialogResult ShowWarning(string title, string message);
        /// <summary>
        /// used to informati on item
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <param name="boxbutton"></param>
        /// <returns></returns>
        enuDialogResult Show(string message, string title, enuCoreMessageBoxButtons boxbutton);
        /// <summary>
        /// notify error message
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message">html string </param>
        void NotifyMessage(string title, string message);
    }
}

