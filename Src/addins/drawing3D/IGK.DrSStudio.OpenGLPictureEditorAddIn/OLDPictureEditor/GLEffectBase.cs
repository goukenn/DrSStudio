

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GLEffectBase.cs
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
file:GLEffectBase.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.GLPictureEditorAddIn
{
    
using IGK.ICore;using IGK.DrSStudio.WinUI;
    using IGK.DrSStudio.WinUI.Configuration;
    using IGK.OGLGame.Graphics;
    /// <summary>
    /// reprenset a gl Effect
    /// </summary>
    public abstract class GLEffectBase : 
        ICoreWorkingConfigurableObject ,
        IDisposable 
    {
        #region ICoreWorkingConfigurableObject Members
        public  ICoreControl GetConfigControl()
        {
            throw new NotImplementedException();
        }
        public virtual enuParamConfigType GetConfigType()
        {
            return enuParamConfigType.ParameterConfig;
        }
        public virtual ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
        {
            return parameters;
        }
        #endregion
        #region ICoreIdentifier Members
        public virtual string Id
        {
            get { return this.GetType().Name; }
        }
        #endregion
        public abstract void Bind(OGLGraphicsDevice graphicsDevice);
        public abstract void UnBind(OGLGraphicsDevice graphicsDevice);
        #region IDisposable Members
        public virtual void Dispose()
        {
        }
        #endregion
    }
}

