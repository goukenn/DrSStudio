

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ConfigFontElement.cs
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
file:ConfigFontElement.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IGK.ICore;

using IGK.DrSStudio;
using IGK.DrSStudio.WinUI;
using IGK.ICore.WinUI.Configuration;
using IGK.ICore.WinUI;
namespace IGK.DrSStudio.Drawing3D.FileBrowserAddIn
{
    class FBSettingObject : ICoreWorkingConfigurableObject
    {
        private int m_FontSize;
        private int m_FontWidth;
        private int m_FontHeight;
        public FBSettingObject()
        {
            this.m_FontSize = 12;
            this.m_FontHeight = 12;
            this.m_FontWidth = 5;
        }
        public int FontHeight
        {
            get { return m_FontHeight; }
            set
            {
                if ((m_FontHeight != value)&&(value > 0))
                {
                    m_FontHeight = value;
                    OnPropertyChanged(EventArgs.Empty);
                }
            }
        }
        public event EventHandler PropertyChanged;
        private void OnPropertyChanged(EventArgs eventArgs)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, eventArgs);
        }
        public int FontWidth
        {
            get { return m_FontWidth; }
            set
            {
                if ((m_FontWidth != value)&&(value > 0))
                {
                    m_FontWidth = value;
                    OnPropertyChanged(EventArgs.Empty);
                }
            }
        }
        public int FontSize
        {
            get { return m_FontSize; }
            set
            {
                if ((m_FontSize != value) && (value > 0))
                {
                    m_FontSize = value;
                    OnPropertyChanged(EventArgs.Empty);
                }
            }
        }
        public  ICoreControl GetConfigControl()
        {
            return null;
        }
        public enuParamConfigType GetConfigType()
        {
            return enuParamConfigType.ParameterConfig;
        }
        public ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
        {
            var g = parameters.AddGroup("FontDefinition");
            Type  t = this.GetType ();
            g.AddItem(t.GetProperty("FontSize"));
            g.AddItem(t.GetProperty("FontWidth"));
            g.AddItem(t.GetProperty("FontHeight"));
            return parameters;
        }
        public string Id
        {
            get { return "OPGLFontProperty"; }
        }
    }
}

