using IGK.ICore;
using IGK.ICore.Codec;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.ICore.WinUI.Configuration;
using IGK.ICore.Settings;






﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.GS.Html
{
    public interface IHtmlController
    {
        IHtmlControllerCollection Childs { get; }
        IHtmlController Parent { get; }
        bool Visible { get; }
        void View();
    }
}