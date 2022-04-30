

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AndroiProjectWizard.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿

using IGK.ICore;using IGK.DrSStudio.Android.WinUI;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.ICore.WinUI.Common;

namespace IGK.DrSStudio.Android
{
    /// <summary>
    /// used to configure a adroind project
    /// </summary>
    public class AndroidProjectWizard : ICoreWorkingProjectWizard, IDisposable
    {
        private bool m_isWellConfigured;
        private ICoreWorkingSurface m_surface;
        private ICoreSystemWorkbench m_bench;
        private AndroidWizardGUI m_ctrl;

        public bool IsWellConfigured
        {
            get { return this.m_isWellConfigured; }
            private set { this.m_isWellConfigured = value; }
        }
        /// <summary>
        /// run configuration wizzard
        /// </summary>
        /// <param name="bench"></param>
        /// <returns></returns>
        public enuDialogResult RunConfigurationWizzard(ICoreSystemWorkbench bench)
        {
            this.m_bench = bench;
            m_ctrl = new AndroidWizardGUI();
            using (var dial = bench.CreateNewDialog(m_ctrl))
            {
                dial.Title = "title.androidconfigurationwizard".R();
                m_ctrl.Anchor = (System.Windows.Forms.AnchorStyles)15;
                if (dial.ShowDialog() == enuDialogResult.OK )
                {
                    m_surface = new AndroidProjectEditorSurface()
                    {
                        Project = m_ctrl.Project
                    };
                    m_isWellConfigured = true;
                    return enuDialogResult.OK;
                }
            }
            return enuDialogResult.None;
        }

        public ICoreWorkingSurface Surface
        {
            get { return this.m_surface; }
        }

        public void Dispose()
        {
            if (this.m_ctrl != null)
            {
                this.m_ctrl.Dispose();
                this.m_ctrl = null;
            }
        }
    }
}
