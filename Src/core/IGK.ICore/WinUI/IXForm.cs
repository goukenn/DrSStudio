

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IXForm.cs
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
file:IXForm.cs
*/
/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igkdev.be
CoreApplicationManager.Instance : ICore
powered by IGK - DEV &copy; 2008-2012
THIS FILE IS A PART OF THE DRSSTUDIO APPLICATION. SEE "License.txt"
FOR MORE INFORMATION ABOUT THE LICENSE
------------------------------------------------------------------- 
*/
﻿using IGK.ICore;using IGK.ICore.Drawing2D;
using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore.WinUI
{
    /// <summary>
    /// represent the default form interface
    /// </summary>
    public interface IXForm : 
        IXCoreForm,
        ICoreIdentifier ,        
        IDisposable ,
        ICoreControl 
    {
        event EventHandler BackgroundDocumentChanged;
        event EventHandler Load;
        event EventHandler<CoreFormClosedEventArgs> Closed;
        event EventHandler<CoreFormClosingEventArgs> Closing;
        event EventHandler Shown;
        event EventHandler Activated;
        event EventHandler Deactivate;

        ICore2DDrawingDocument BackgroundDocument { get; set; }
        string Title { get; set; }
        event EventHandler TitleChanged;
        ICoreIcon Icon { get; set; }        
        bool Created { get; }
        bool Enabled { get; set; }
        bool ShowInTaskbar { get; set; }
        void Activate();
        void Show();
        void Hide();
        void Close();
        IXForm Owner { get; set; }
        enuDialogResult DialogResult { get; set; }
        enuDialogResult ShowDialog();
        enuFormStartPosition StartPosition { get; set; }
        ICoreMenu AppMenu { get; set; }
    }
}

