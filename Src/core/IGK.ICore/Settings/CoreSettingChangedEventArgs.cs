using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.Settings
{
    public class CoreSettingChangedEventArgs : EventArgs 
    {
        private ICoreSettingValue coreSettingValue;
        public ICoreSettingValue Value {
            get {
                return this.coreSettingValue;
            }
        }
        public CoreSettingChangedEventArgs(ICoreSettingValue coreSettingValue)
        {
            this.coreSettingValue = coreSettingValue;
        }
    }
}
