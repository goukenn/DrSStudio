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
    /// <summary>
    /// Represent a togo GS system Application
    /// </summary>
    public abstract class GSApplication : CoreApplicationBase, IDisposable 
    {
        public GSApplication()
        {
        }
        public override bool RegisterClientSystem(Func<CoreSystem> __initInstance)
        {
            __initInstance();
            return true;
        }
        public override bool RegisterServerSystem(Func<CoreSystem> __initInstance)
        {
            __initInstance();
            return true;
        }
        /// <summary>
        /// override this method to run your app. in a gs application system
        /// </summary>
        public virtual void Run() { 
        }

        public virtual void Dispose()
        {
            
        }
    }
}
