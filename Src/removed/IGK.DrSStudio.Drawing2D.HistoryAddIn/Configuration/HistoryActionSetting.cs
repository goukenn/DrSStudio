

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: HistoryActionSetting.cs
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
file:HistoryActionSetting.cs
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
/* 
-------------------------------------------------------------
This file is part of iGK-DEV-DrawingStudio
-------------------------------------------------------------
-------------------------------------------------------------
-------------------------------------------------------------
view license file in Documentation folder to get more info
Copyright (c) 2008-2010 
Author  : Charles A.D. BONDJE DOUE 
mail : bondje.doue@hotmail.com
-------------------------------------------------------------
-------------------------------------------------------------
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
namespace IGK.DrSStudio.Drawing2D
{
    using IGK.ICore;using IGK.DrSStudio.Tools;
    using IGK.DrSStudio.Settings;
    [CoreAppSetting(Name = "HistoryActionSetting")]
    /// <summary>
    /// represent the history action setting
    /// </summary>
    sealed class HistoryActionSetting : CoreSettingBase 
    {
        private static HistoryActionSetting sm_instance;
        /// <summary>
        /// atomi instance on setting
        /// </summary>
        public static HistoryActionSetting Instance
        {
            get
            {
                if (sm_instance == null)
                    sm_instance = new HistoryActionSetting();
                return sm_instance;
            }
        }       
        [CoreSettingDefaultValueAttribute ("%startup%/Temp/History")]
        public string OutFolder
        {
            get { return this["OutFolder"].Value.ToString() ; }
            set {
                this["OutFolder"].Value  = value; 
            }
        }
        [CoreSettingDefaultValueAttribute("True")]
        public bool SaveBitmapChanged
        {
            get { return (bool)this["SaveBitmapChanged"].Value; }
            set { this["SaveBitmapChanged"].Value = value; }
        }
        [CoreSettingDefaultValueAttribute("True")]
        public bool SaveDocumentAddedOrRemove
        {
            get { return (bool)this["SaveDocumentAddedOrRemove"].Value ; }
            set { this["SaveDocumentAddedOrRemove"].Value  = value; }
        }
        [CoreSettingDefaultValueAttribute(true)]
        public bool SaveColorChanged
        {
            get { return (bool)this["SaveColorChanged"].Value ; }
            set { this["SaveColorChanged"].Value  = value; }
        }
        public HistoryActionSetting()
            : base()
        {
        }
        /// <summary>
        /// bind the surface hosting mangager
        /// </summary>
        /// <param name="historySurfaceManager"></param>
        internal void Bind(IGK.DrSStudio.Drawing2D.Tools.HistorySurfaceManager historySurfaceManager)
        {
        }
    }
}

