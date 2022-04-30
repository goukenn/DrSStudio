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
using System.Threading.Tasks;

namespace IGK.GS
{
    public interface  IGSLoginInfo
    {
        /// <summary>
        /// get the login info
        /// </summary>
        string Login { get; }
        /// <summary>
        /// get the password
        /// </summary>
        string Pwd { get; }
    }
}
