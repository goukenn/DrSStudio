

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Custom2DSurfaceObjectCreator.cs
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
file:Custom2DSurfaceObjectCreator.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudioTools
{
    using IGK.ICore.WinCore;
using IGK.DrSStudio;
    using IGK.ICore;
    using IGK.ICore.WinUI;
    using IGK.ICore.WinUI.Configuration;
    /// <summary>
    /// create a 2Dd surface object creator
    /// </summary>
    sealed class Custom2DSurfaceObjectCreator : ICoreWorkingConfigurableObject 
    {
        private CoreUnit m_Width;
        private CoreUnit m_Height;
        public CoreUnit Height
        {
            get { return m_Height; }
            set
            {
                if (m_Height != value)
                {
                    m_Height = value;
                }
            }
        }
        public CoreUnit Width
        {
            get { return m_Width; }
            set
            {
                if (m_Width != value)
                {
                    m_Width = value;
                }
            }
        }
        public Custom2DSurfaceObjectCreator()
        {
            this.Width = "400 px";
            this.Height = "300 px";
        }
        #region ICoreWorkingConfigurableObject Members
        public enuParamConfigType GetConfigType()
        {
            return enuParamConfigType.ParameterConfig;
        }
        public ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
        {
            Type t = this.GetType ();
            var g = parameters.AddGroup(CoreConstant.PARAM_DEFINITION);
            g.AddItem(t.GetProperty("Width"));
            g.AddItem(t.GetProperty("Height"));
            return parameters;
        }
        public  ICoreControl GetConfigControl()
        {
            throw new NotImplementedException();
        }
        #endregion
        #region ICoreIdentifier Members
        public string Id
        {
            get { return CoreSystem.GetString ("Custom2DSurface"); }
        }
        #endregion
    }
}

