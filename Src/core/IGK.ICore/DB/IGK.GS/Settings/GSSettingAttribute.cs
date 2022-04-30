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

namespace IGK.GS.Settings
{
    [AttributeUsage (AttributeTargets.Class , Inherited =false , AllowMultiple =false )]
    public class GSSettingAttribute : CoreSettingAttributeBase
    {
        public override int GroupIndex
        {
            get { return 0x80; }
        }

        public override string GroupName
        {
            get { return "GSSetting"; }
        }

        public GSSettingAttribute()
        {

        }
    }
}
