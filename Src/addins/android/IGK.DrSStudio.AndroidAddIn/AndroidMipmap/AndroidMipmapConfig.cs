using IGK.DrSStudio.Android.AndroidMipmap.WinUI;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Android.AndroidMipmap
{
    /// <summary>
    /// classe use to configuration the working item project
    /// </summary>
    class AndroidMipmapConfig : ICoreWorkingProjectWizard
    {
        readonly ICoreWorkingSurface m_surface; 

        public ICoreWorkingSurface Surface => m_surface;

        public bool IsWellConfigured => true;

        public enuDialogResult RunConfigurationWizzard(ICoreSystemWorkbench bench) =>
            enuDialogResult.OK;


        ///<summary>
        ///public .ctr
        ///</summary>
        public AndroidMipmapConfig()
        {
            this.m_surface = new AndroidMipmapSurface();
        }
    }
}
