

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XMLEditorSetting.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/

using IGK.ICore;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.DrSStudio;
using IGK.DrSStudio.Menu;
using IGK.DrSStudio.WinUI;
using IGK.DrSStudio.Drawing2D.WinUI;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:XMLEditorSetting.cs
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
namespace IGK.DrSStudio.XMLEditorAddIn.Configuration
{
    using IGK.ICore.Settings;
    [CoreAppSetting(Name="XMLEditorSetting")]
    class XMLEditorSetting : 
        CoreSettingBase ,
        IXMLEditorSetting 
    {
        [CoreSettingDefaultValue (true )]
        public bool ShowTab {
            get {
                return (bool)this["ShowTab"].Value;
            }
            set {
                this["ShowTab"].Value = value;
            }
        }
        /// <summary>
        /// get or set the font definition
        /// </summary>
        [CoreSettingDefaultValue("FontName:Courier New;Size:12;FontStyle:Regular", typeof(CoreFont))]
        public string Font {
            get {
                return (string)this["Font"].Value;
            }
            set {
                this["Font"].Value = value;
            }
        }
        private static XMLEditorSetting sm_instance;
        private XMLEditorSetting()
        {
            this.Add("Font", "FontName:Courier New;Size:12;FontStyle:Regular", null);
        }
        protected override void InitDefaultProperty(System.Reflection.PropertyInfo prInfo, CoreSettingDefaultValueAttribute attrib)
        {
            if (prInfo.PropertyType == typeof(CoreFont))
            {
              this[prInfo.Name] = new CorePropertySetting (prInfo.Name, typeof (CoreFont ), CoreFont.CreateFrom (attrib.Value .ToString (),this));
               return;
            }
            base.InitDefaultProperty(prInfo, attrib);
        }
        public static XMLEditorSetting Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static XMLEditorSetting()
        {
            sm_instance = new XMLEditorSetting();
        }
        #region IXMLEditorSetting Members
        [CoreSettingDefaultValue("Red", typeof (Colorf ))]
        public Colorf TiltColor
        {
            get { return (Colorf)this["TiltColor"].Value; }
        }
        [CoreSettingDefaultValue("Red", typeof(Colorf))]
        public Colorf DefaultColor
        {
            get { return (Colorf  ) this["DefaultColor"].Value ; }
        }
        [CoreSettingDefaultValue("Red", typeof(Colorf))]
        public Colorf ValueColor
        {
            get { return (Colorf)this["ValueColor"].Value; }
        }
        [CoreSettingDefaultValue("Red", typeof(Colorf))]
        public Colorf AttributeColor
        {
            get { return (Colorf)this["AttributeColor"].Value; }
        }
        [CoreSettingDefaultValue("Red", typeof(Colorf))]
        public Colorf CommentColor
        {
            get { return (Colorf)this["CommentColor"].Value; }
        }
        [CoreSettingDefaultValue("Red", typeof(Colorf))]
        public Colorf NodeColor
        {
            get { return (Colorf)this["NodeColor"].Value; }
        }
        [CoreSettingDefaultValue("Red", typeof(Colorf))]
        public Colorf DeclarationColor
        {
            get { return (Colorf)this["DeclarationColor"].Value; }
        }
        [CoreSettingDefaultValue("Red", typeof(Colorf))]
        public Colorf BranketColor
        {
            get { return (Colorf)this["BranketColor"].Value; }
        }
        #endregion
        #region IXMLEditorSetting Members
        [CoreSettingDefaultValue(4)]
        public int TabSpace
        {
            get { return (int)this["TabSpace"].Value; }
        }
        #endregion
    }
}

