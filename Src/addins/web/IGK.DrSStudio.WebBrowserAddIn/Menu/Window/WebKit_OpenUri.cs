

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WebKit_OpenUri.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/

using IGK.ICore;
using IGK.ICore.Codec;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.DrSStudio;
using IGK.DrSStudio.Menu;
using IGK.DrSStudio.WinUI;
using IGK.DrSStudio.Drawing2D;
using IGK.DrSStudio.Drawing2D.WinUI;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:WebKit_OpenUri.cs
*/
using IGK.ICore.WinUI.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.WebBrowserAddIn.Menu
{
    [DrSStudioMenu("Window.WebkitBrowser", 0)]
    class WebKit_OpenUri : CoreApplicationMenu , ICoreWorkingConfigurableObject
    {
        private string m_Uri;
        /// <summary>
        /// get or set the uri
        /// </summary>
        public string Uri
        {
            get { return m_Uri; }
            set
            {
                if (m_Uri != value)
                {
                    m_Uri = value;
                }
            }
        }
        protected override bool PerformAction()
        {
            if (Workbench.ConfigureWorkingObject(this, "title.openUri".R(), false, Size2i.Empty) == enuDialogResult.OK)
            {
                var s = WebBrowserAddIn.Tools.WebBrowserSurfaceManagerTool.Instance.GetSurface();
                s.OpenUri(this.Uri);
                this.Workbench.AddSurface (s, true );
            }
            return false;
        }
      
        public enuParamConfigType GetConfigType()
        {
            return enuParamConfigType.ParameterConfig;
        }
        public ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
        {
            var v_group = parameters.AddGroup("Uri");
            v_group.AddItem(this.GetType().GetProperty("Uri"));
            return parameters;
        }

        ICoreControl ICoreWorkingConfigurableObject.GetConfigControl()
        {
            return null;
        }
    }
}

