using IGK.ICore;
using IGK.ICore.Codec;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.ICore.WinUI.Configuration;
using IGK.ICore.Settings;







using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.ICore.Tools;

namespace IGK.GS
{    
    /// <summary>
    /// represent a gs tool base
    /// </summary>
    public class GSToolBase : CoreToolBase
    {       
        public GSToolBase()
        {
            GSSystem.Instance.MainFormChanged += _MainFormChanged;
        }
        protected override void GenerateHostedControl()
        {
            this.HostedControl = null;
        }

        void _MainFormChanged(object sender, EventArgs e)
        {
            OnGSMainFormChanged(EventArgs.Empty);
            this.GenerateHostedControl();
        }

        protected virtual void OnGSMainFormChanged(EventArgs eventArgs)
        {

        }
    }
}
