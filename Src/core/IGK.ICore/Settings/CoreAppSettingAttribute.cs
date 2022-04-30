

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreAppSettingAttribute.cs
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
file:CoreAppSettingAttribute.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.ICore.Settings
{
    /// <summary>
    /// represent a the base core application setting
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited=false , AllowMultiple = false )]
    public class CoreAppSettingAttribute : CoreAttributeSettingBase 
    {
        public override int GroupIndex
        {
            get { return int.MinValue; }
        }
        public override string GroupName
        {
            get { return "CoreApp"; }
        }
        public CoreAppSettingAttribute()
        {

        }
        public CoreAppSettingAttribute(string name)
        {
            this.Name = name;
        }
    }
}

