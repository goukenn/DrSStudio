

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: FileBrowser_SettingAction.cs
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
file:FileBrowser_SettingAction.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using IGK.ICore.WinCore;
namespace IGK.DrSStudio.Drawing3D.FileBrowser.Actions
{
    
using IGK.DrSStudio.WinUI;
    
    using IGK.ICore.WinUI.Configuration;
    using IGK.ICore;
    using IGK.ICore.WinUI;
    
    using IGK.ICore.Actions;
    using IGK.ICore.WinCore;


    sealed class FileBrowser_SettingAction :
        FBAction,
        ICoreWorkingConfigurableObject 
    {
        public bool ShowFileName
        {
            get { return this.FileBrowser.ShowText; }
            set
            {
                this.FileBrowser.ShowText = value;
            }
        }
        public Colorf BackgroundColor
        {
            get { return this.FileBrowser.BackgroundColor; }
            set
            {
                this.FileBrowser.BackgroundColor = value;             
            }
        }
        protected override bool PerformAction()
        {
          var g = CoreSystem.GetWorkbench();
          if (g != null)
              g.ConfigureWorkingObject(this, "title.FBSettings".R(), false, Size2i.Empty);
          return false;
        }
        #region ICoreWorkingConfigurableObject Members
        public enuParamConfigType GetConfigType()
        {
            return enuParamConfigType.ParameterConfig;
        }
        public ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
        {
            Type t = GetType();
            parameters.AddGroup(CoreConstant.PARAM_DEFINITION).AddItem(t.GetProperty("ShowFileName"));
            ICoreParameterGroup group =  parameters.AddGroup("Actions");
            group.AddActions(new CoreParameterActionBase("GetColor", FBConstant.ASSEMBLY_NAME+".GetColor", new GetColorAction(this)));
            return parameters;
        }
        public  ICoreControl GetConfigControl()
        {
            return null;
        }
        #endregion
        #region ICoreIdentifier Members
        public override  string Id
        {
            get { return FBConstant .FILEBROWSER_TITLE; }
        }
        #endregion
        sealed class GetColorAction : CoreActionBase
        {
            FileBrowser_SettingAction m_browser;
            public GetColorAction(FileBrowser_SettingAction browser)
            {
                this.m_browser = browser;
            }
            public override string Id
            {
                get { return null; }
            }
            protected override bool PerformAction()
            {
                using (ColorDialog cld = new ColorDialog())
                {
                    cld.Color = this.m_browser.BackgroundColor.ToGdiColor();
                    if (cld.ShowDialog() == DialogResult.OK)
                    {
                        this.m_browser.BackgroundColor = cld.Color.ToIGKColor();
                        return true;
                    }
                }
                return false;
            }
        }
    }
}

