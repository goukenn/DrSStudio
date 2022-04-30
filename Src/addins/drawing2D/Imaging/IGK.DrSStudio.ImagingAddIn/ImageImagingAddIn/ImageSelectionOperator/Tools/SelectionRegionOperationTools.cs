

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: SelectionRegionOperationTools.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
using IGK.ICore;
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
IGKDEV - [2008 - 2014]
author: C.A.D . BONDJE DOUE
file:SelectionRegionOperationTools.cs
*/
/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igkdev.be
Application : DrSStudio
powered by IGK - DEV &copy; 2008-2012
THIS FILE IS A PART OF THE DRSSTUDIO APPLICATION. SEE "License.txt"
FOR MORE INFORMATION ABOUT THE LICENSE
------------------------------------------------------------------- 
*/
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.Drawing2D.ImageSelection.Tools
{
    using IGK.DrSStudio.Drawing2D.Tools;
    using IGK.DrSStudio.Settings;
    [CoreTools ("Tools.SelectionOperand")]
    sealed class SelectionRegionOperationTools :
        Core2DDrawingToolBase 
    {
                private static SelectionRegionOperationTools sm_instance;
        private SelectionRegionOperationTools(){
        }
        public static SelectionRegionOperationTools Instance{
        get{
        return sm_instance;
        }
        }
        static SelectionRegionOperationTools(){ 
        sm_instance = new SelectionRegionOperationTools();
        }
        private enuSelectionOperationType m_RegionSelectionType;
        private enuCoreRegionOperation  m_RegionOperation;
        /// <summary>
        /// Get or set region operation
        /// </summary>
        public enuCoreRegionOperation  RegionOperation
        {
            get { return m_RegionOperation; }
            set
            {
                if (m_RegionOperation != value)
                {
                    m_RegionOperation = value;
                    OnRegionOperationChanged(EventArgs.Empty);
                }
            }
        }
        public event EventHandler RegionOperationChanged;
        private void OnRegionOperationChanged(EventArgs eventArgs)
        {
            if (this.RegionOperationChanged != null)
                this.RegionOperationChanged(this, eventArgs);
        }
        /// <summary>
        /// get or set how to render
        /// </summary>
        public enuSelectionOperationType RegionSelectionOperationType
        {
            get { return m_RegionSelectionType; }
            set
            {
                if (m_RegionSelectionType != value)
                {
                    m_RegionSelectionType = value;
                    OnRegionSelectionOperationTypeChanged(EventArgs.Empty);
                }
            }
        }
        public event EventHandler RegionSelectionOperationTypeChanged;
        private void OnRegionSelectionOperationTypeChanged(EventArgs e)
        {
            if (this.RegionSelectionOperationTypeChanged != null)
                this.RegionSelectionOperationTypeChanged(this, e);
        }
        protected override void GenerateHostedControl()
        {
            this.HostedControl = null;
        }
        [CoreAppSetting(Name = "ImageSelectionOperator")]
        sealed class SelectionOperationSetting : IGK.DrSStudio.Settings.CoreSettingBase
        {
            [IGK.DrSStudio.Settings.CoreSettingDefaultValue ( enuSelectionOperationType.Invert , typeof (enuSelectionOperationType))]
            public enuSelectionOperationType RegionSelectionType {
                get { return (enuSelectionOperationType)this["RegionSelectionType"].Value; }
            }
            [IGK.DrSStudio.Settings.CoreSettingDefaultValue(enuCoreRegionOperation.Union, typeof(enuCoreRegionOperation))]
            public enuCoreRegionOperation RegionOperation
            {
                get { return (enuCoreRegionOperation)this["RegionOperation"].Value; }
            }
            protected override void InitDefaultProperty(System.Reflection.PropertyInfo prInfo, IGK.DrSStudio.Settings.CoreSettingDefaultValueAttribute attrib)
            {
                base.InitDefaultProperty(prInfo, attrib);
            }
        }
    }
}

