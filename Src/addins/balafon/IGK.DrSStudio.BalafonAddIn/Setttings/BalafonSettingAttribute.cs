using IGK.ICore.Settings;
using System;

namespace IGK.DrSStudio.Balafon.Setttings
{
    /// <summary>
    /// represent a balafon setting group
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class BalafonSettingAttribute : CoreAttributeSettingBase
    {
        public override int GroupIndex=> 0x500;
        public override string GroupName => BalafonConstant.PROJECT_NAME;
    }
}