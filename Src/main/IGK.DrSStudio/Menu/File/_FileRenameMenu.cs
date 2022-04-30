

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _FileRename.cs
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
file:_FileRename.cs
*/
using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.DrSStudio.Menu.File
{
    using IGK.ICore.WinCore;
using IGK.ICore;
    using IGK.ICore.Menu;
    using IGK.ICore.WinUI;
    using IGK.ICore.WinUI.Configuration;
    [DrSStudioMenu("File.Rename", 50)]
    public sealed class _FileRenameMenu : CoreApplicationMenu  ,
        ICoreWorkingConfigurableObject 
    {
        private string m_FileName;
        public string FileName
        {
            get { return m_FileName; }
            set
            {
                if (m_FileName != value)
                {
                    m_FileName = value;
                }
            }
        }
        public new ICoreWorkingFilemanagerSurface CurrentSurface {
            get { return base.CurrentSurface as ICoreWorkingFilemanagerSurface; }
        }
        protected override bool PerformAction()
        {
            if (this.CurrentSurface != null)
            {
                ICoreWorkingFilemanagerSurface s = this.CurrentSurface as ICoreWorkingFilemanagerSurface;
                if (s != null)
                {
                    this.FileName = s.FileName;
                    if (Workbench.ConfigureWorkingObject(this, "title.filerename".R(), false, Size2i.Empty) == enuDialogResult.OK)
                    {
                        s.RenameTo(this.FileName);                        
                    }
                }
            }
            return false;
        }
        protected override bool IsVisible()
        {
            return (this.CurrentSurface != null);
        }
        protected override bool IsEnabled()
        {
            return (this.CurrentSurface != null);
        }
        #region ICoreWorkingConfigurableObject Members
        public enuParamConfigType GetConfigType()
        {
            return enuParamConfigType.ParameterConfig;
        }
        public ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
        {
            parameters.AddGroup(CoreConstant.PARAM_DEFINITION).AddItem(GetType().GetProperty("FileName"));
            return parameters;
        }
        public  ICoreControl GetConfigControl()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}

