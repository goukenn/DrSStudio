

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _Properties.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV - [2008 - 2014]
author: C.A.D . BONDJE DOUE
file:_Properties.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.GLPictureEditorAddIn.Menu.GLImage
{
    
using IGK.ICore;using IGK.DrSStudio.WinUI;
    using IGK.DrSStudio.WinUI.Configuration;
    [IGK.DrSStudio.Menu.CoreMenu("GLImage.Properties", 1000, SeparatorBefore=true)]  
    class _Properties : GLEditorMenuBase , ICoreWorkingConfigurableObject 
    {
        protected override bool PerformAction()
        {
            this.Workbench.ConfigureWorkingObject(this);
            return base.PerformAction();
        }
        #region ICoreWorkingConfigurableObject Members
        public  ICoreControl GetConfigControl()
        {
            throw new NotImplementedException();
        }
        public enuParamConfigType GetConfigType()
        {
            return enuParamConfigType.ParameterConfig;
        }
        public ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
        {
            var group = parameters.AddGroup("Default");
            group.AddItem(GetType().GetProperty("RenderMode"));
            return parameters;
        }
        #endregion
        public enuRenderMode RenderMode
        {
            get { return this.CurrentSurface.RenderMode; }
            set
            {
                this.CurrentSurface.RenderMode = value;
            }
        }
    }
}

