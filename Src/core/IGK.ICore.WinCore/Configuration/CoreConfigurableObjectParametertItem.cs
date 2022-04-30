

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreConfigurableObjectParametertItem.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
using IGK.ICore;using IGK.ICore.WinUI.Configuration;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:CoreConfigurableObjectParametertItem.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.ICore.WinCore.Configuration
{
    /// <summary>
    /// represent a single paramenter cnofigurable item
    /// </summary>
    public sealed class CoreConfigurableObjectParameterItem :
        CoreParameterItemBase, 
        ICoreParameterItem,
        ICoreConfigurableObjectParameterItem
    {
        private ICoreWorkingConfigurableObject m_Target;
        /// <summary>
        /// get the configurable object
        /// </summary>
        public ICoreWorkingConfigurableObject Target
        {
            get { return m_Target; }
        }
        public CoreConfigurableObjectParameterItem(ICoreWorkingConfigurableObject target)
            : base(target.Id, target.Id)
        {
            this.m_Target = target;
        }
    }
}

