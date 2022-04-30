

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: HistorySetting.cs
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
file:HistorySetting.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.Drawing2D.Settings
{
    using IGK.ICore;using IGK.DrSStudio.Settings;
    using IGK.DrSStudio.WinUI;
    using IGK.DrSStudio.WinUI.Configuration;
    /// <summary>
    /// represent setting for history
    /// </summary>
    [CoreAppSetting(Name = "Drawing2D.HistorySetting", Index=30)]
    class HistorySetting : CoreSettingBase
    {
        private static HistorySetting sm_instance;
        private HistorySetting()
        {
        }
        public static HistorySetting Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static HistorySetting()
        {
            sm_instance = new HistorySetting();
        }
        public override enuParamConfigType GetConfigType()
        {
            return enuParamConfigType.ParameterConfig;
        }
        public bool BindConfig(HistoryMemberSettings g)
        {
            return true;
        }
        public override void Load(ICoreSetting setting)
        {
            base.Load(setting);
        }
        protected override void InitDefaultProperty(System.Reflection.PropertyInfo prInfo, CoreSettingDefaultValueAttribute attrib)
        {
            base.InitDefaultProperty(prInfo, attrib);
            this.SaveColorChanged = false;
            this.SaveImageBitmapChanged = true;
            this.SaveItemAddedOrRemoved = true;
            this.SaveLayerAddedOrRemoved = true;
            this.SaveDocumentAddedOrRemoved = false;
            this.SaveElementPropertyChanged = false;
        }
        public override ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
        {            
            parameters = base.GetParameters(parameters);
            var group = parameters.AddGroup("Pictures");
            Type t = this.GetType();
            group.AddItem(t.GetProperty("SaveColorChanged"));
            group.AddItem(t.GetProperty("SaveItemAddedOrRemoved"));
            group.AddItem(t.GetProperty("SaveImageBitmapChanged"));
            group.AddItem(t.GetProperty("SaveLayerAddedOrRemoved"));
            group.AddItem(t.GetProperty("SaveDocumentAddedOrRemoved"));
            group.AddItem(t.GetProperty("SaveElementMatrixChanged"));
            group.AddItem(t.GetProperty("SaveElementPropertyChanged"));
            return parameters;
        }
        //history save setting
        public bool SaveColorChanged {
            get {
                return Convert.ToBoolean (this["SaveColorChanged"].Value );
            }
            set {
                this["SaveColorChanged"].Value  = value;
            }
        }
        public bool SaveItemAddedOrRemoved {
            get
            {
                return Convert.ToBoolean(this["SaveItemAddedOrRemoved"].Value);
            }
            set
            {
                this["SaveItemAddedOrRemoved"].Value   = value;
            }
        }
        public bool SaveImageBitmapChanged
        {
            get
            {
                return Convert.ToBoolean(this["SaveImageBitmapChanged"].Value);
            }
            set
            {
                this["SaveImageBitmapChanged"].Value = value;
            }
        }
        public bool SaveLayerAddedOrRemoved
        {
            get
            {
                return Convert.ToBoolean(this["SaveLayerAddedOrRemoved"].Value);
            }
            set
            {
                this["SaveLayerAddedOrRemoved"].Value = value;
            }
        }
        public bool SaveDocumentAddedOrRemoved
        {
            get
            {
                return Convert.ToBoolean(this["SaveDocumentAddedOrRemoved"].Value);
            }
            set
            {
                this["SaveDocumentAddedOrRemoved"].Value = value;
            }
        }
        public bool SaveElementMatrixChanged
        {
            get
            {
                return Convert.ToBoolean(this["SaveElementMatrixChanged"].Value);
            }
            set
            {
                this["SaveElementMatrixChanged"].Value = value;
            }
        }
        public bool SaveElementPropertyChanged
        {
            get
            {
                return Convert.ToBoolean(this["SaveElementPropertyChanged"].Value);
            }
            set
            {
                this["SaveElementPropertyChanged"].Value = value;
            }
        }
    }
}

