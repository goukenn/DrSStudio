

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: BuildFile.cs
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
file:BuildFile.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.VideoEditionTableAddIn.Menu.Edit
{
    using IGK.ICore;using IGK.DrSStudio.WinUI;
    using IGK.DrSStudio.WinUI.Configuration;
    [VideoMenu(VideoConstant.MENU_EDIT_BUILD, 1)]
    sealed class BuildFile : VideoMenuBase , ICoreWorkingConfigurableObject 
    {
        private string m_TargetFileName;
        public string TargetFileName
        {
            get { return m_TargetFileName; }
            set
            {
                if (m_TargetFileName != value)
                {
                    m_TargetFileName = value;
                }
            }
        }
        protected override bool PerformAction()
        {
            if (Workbench.ConfigureWorkingObject(this) == System.Windows.Forms.DialogResult.OK)
            {
                this.CurrentSurface.BuildVideoProject(
                    this.TargetFileName);
            }
            return base.PerformAction();
        }
        #region ICoreWorkingConfigurableObject Members
        public  ICoreControl GetConfigControl()
        {
            return null;
        }
        public enuParamConfigType GetConfigType()
        {
            return enuParamConfigType.ParameterConfig;
        }
        public IGK.DrSStudio.WinUI.ICoreParameterConfigCollections GetParameters(IGK.DrSStudio.WinUI.ICoreParameterConfigCollections parameters)
        {
            var group = parameters.AddGroup("Primary");
            IGK.DrSStudio.Actions.SelectFileAction v_a = new IGK.DrSStudio.Actions.SelectFileAction("Select", "video file|*.avi;");
            v_a.SelectedFileChanged += new EventHandler(v_a_SelectedFileChanged);
            group.AddActions(v_a);
            return parameters;
        }
        void v_a_SelectedFileChanged(object sender, EventArgs e)
        {
            this.TargetFileName = (sender as IGK.DrSStudio.Actions.SelectFileAction).SelectedFile;
        }
        #endregion
    }
}

