

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreFont.cs
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
file:CoreFont.cs
*/
///* 
//-------------------------------------------------------------------
//Company: IGK-DEV
//Author : C.A.D. BONDJE DOUE
//SITE : http://www.igkdev.be
//Application : ICore
//powered by IGK - DEV &copy; 2008-2012
//THIS FILE IS A PART OF THE DRSSTUDIO APPLICATION. SEE "License.txt"
//FOR MORE INFORMATION ABOUT THE LICENSE
//------------------------------------------------------------------- 
//*/
﻿using IGK.ICore;
using IGK.ICore.ComponentModel;
using IGK.ICore.Drawing2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.ICore.Drawing2D
{
    public class CoreFont : ICoreFont 
    {
        public static readonly string DefaultFontName = "Consolas";
        private ICoreWorkingObject m_owner;
        private ICoreFont m_font;
        private enuStringAlignment m_hAlignment;
        private enuStringAlignment m_vAlignment;
        static Dictionary<string, ICoreFontInfo> sm_Fonts;
        private bool m_isDisposed;
        public event EventHandler FontDefinitionChanged;
        private enuHotKeyPrefix m_HotKeyPrefix;

        public enuHotKeyPrefix HotKeyPrefix
        {
            get { return m_HotKeyPrefix; }
            set
            {
                if (m_HotKeyPrefix != value)
                {
                    m_HotKeyPrefix = value;
                    OnFontDefinitionChanged(EventArgs.Empty);
                }
            }
        }

        private bool m_WordWrap;
        /// <summary>
        /// Get or set font wordwrap
        /// </summary>
        public bool WordWrap
        {
            get { return m_WordWrap; }
            set
            {
                if (m_WordWrap != value)
                {
                    m_WordWrap = value;
                    OnFontDefinitionChanged(EventArgs.Empty);
                }
            }
        }

         /// <summary>
         /// convert from string
         /// </summary>
         /// <param name="name"></param>
         /// <returns></returns>
        public static implicit operator CoreFont(string name)
        {
            return name.CoreConvertFrom<CoreFont>();
        }
        private string m_Id;
        public static bool Exists(string name)
        {
            name = name.ToLower();
            if (sm_Fonts.ContainsKey(name))
                return true;
            return false;
        }
        public string Id
        {
            get {
                if (string.IsNullOrEmpty(this.m_Id))
                    this.m_Id = string.Format("Font_{0}", this.GetHashCode());
                return m_Id; }
        }
        /// <summary>
        /// raise the definition event
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnFontDefinitionChanged(EventArgs e)
        {
            this.GenerateFont();
            this.FontDefinitionChanged?.Invoke(this, e);
        }
        public static string[] GetInstalledFamilies()
        {
            return sm_Fonts.Keys.ToArray<string>();
        }
        public static ICoreFontInfo GetFonts(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                name = name.ToLower();
                if (sm_Fonts.ContainsKey(name))
                    return sm_Fonts[name];
            }
            return null;
        }
        static CoreFont()
        {
            sm_Fonts = new Dictionary<string, ICoreFontInfo>();
        }
        internal CoreFont()
        {
            this.m_FontSize = 12.0f;
            this.m_FontStyle = enuFontStyle.Regular;
            this.m_vAlignment = enuStringAlignment.Near;
            this.m_hAlignment = enuStringAlignment.Near; 
        }

        internal CoreFont(ICoreWorkingObject owner, string defaultFontName)
        {
            this.m_owner = owner;
            if (sm_Fonts.ContainsKey(defaultFontName.ToLower()))
                this.m_FontName = defaultFontName;
            else
            {
                if (sm_Fonts.Count > 0)
                    this.m_FontName = sm_Fonts.First<KeyValuePair<string, ICoreFontInfo>>().Key;
                else
                    throw new ArgumentException("font list is empty or not loaded");
            }
            this.m_FontSize = 1.0f;
            this.m_FontStyle = enuFontStyle.Regular;
            this.m_vAlignment = enuStringAlignment.Near;
            this.m_hAlignment = enuStringAlignment.Near;
            this.GenerateFont();
        }
        /// <summary>
        /// used to crreate an internally font
        /// </summary>
        /// <param name="name"></param>
        /// <param name="size"></param>
        /// <param name="enuFontStyle"></param>
        /// <param name="enuRenderingMode"></param>
        internal CoreFont(string name, float size, enuFontStyle enuFontStyle, enuRenderingMode enuRenderingMode)
        {
            this.m_FontName = name;
            this.m_FontSize = size;
            this.m_FontStyle = enuFontStyle;
            this.m_enuRenderingMode = enuRenderingMode;
        }
       
        /// <summary>
        /// call this method to register fonts
        /// </summary>
        /// <param name="Application">application context</param>
        /// <param name="names">list of names</param>
        public static void RegisterFonts(ICoreApplication Application,  params string[] names)        
        {
            string v_name = string.Empty;
            var v_rsManager = Application.ResourcesManager;
            if (v_rsManager == null)
                return;
            ICoreFontInfo v_ft = null;

            foreach (string ft in names  )
            {
                v_ft = v_rsManager.CreateFontInfo(ft);
                if (v_ft != null)
                {
                    v_name = ft.ToLower();
                    if ((v_name != null) && (!sm_Fonts.ContainsKey(v_name)))
                        sm_Fonts.Add(v_name, v_ft);
                }
            }
        }
      /// <summary>
        /// create font from string 
        /// </summary>
        /// <param name="value">definition string</param>
        /// <param name="owner"></param>
        /// <returns></returns>
        public static CoreFont CreateFrom(string value, ICoreWorkingObject owner)
        {
            CoreFont fm = new CoreFont(owner, CoreFont.DefaultFontName);
            fm.CopyDefinition(value);
            return fm;
        }
        public float GetFontSizeFrom(Rectanglef rc, int line)
        {
            if (line < 1)
                line = 1;
            return (rc.Height / line) * this.GetEmHeight() / this.GetLineSpacing();
        }
        public static float GetFontSize(CoreFont fm,enuFontStyle style, Rectanglef rc, int line)
        {
            if (fm == null) 
                return 0;
            if (!fm.Support (style))
            {
                style = GetAllAvailableStyle (fm.FontName);                
            }
            return (rc.Height / line) * fm.GetEmHeight() / fm.GetLineSpacing(); 
        }
        public float GetLineSpacing()
        {
            return this.GetFontFamily().GetLineSpacing(this);
        }
        public static enuFontStyle GetAllAvailableStyle(string fontname)
        {
            ICoreFontInfo ft = GetFonts(fontname);
            enuFontStyle v_fs = ft.IsStyleAvailable(enuFontStyle.Regular) ? enuFontStyle.Regular : 0;
            v_fs = ft.IsStyleAvailable(enuFontStyle.Regular) ? enuFontStyle.Regular : 0;
            v_fs |= ft.IsStyleAvailable(enuFontStyle.Bold) ? enuFontStyle.Bold : 0;
            v_fs |= ft.IsStyleAvailable(enuFontStyle.Italic) ? enuFontStyle.Italic : 0;
            v_fs |= ft.IsStyleAvailable(enuFontStyle.Underline) ? enuFontStyle.Underline : 0;
            v_fs |= ft.IsStyleAvailable(enuFontStyle.Strikeout) ? enuFontStyle.Strikeout : 0;
            return v_fs;
        }
        public enuFontStyle GetAllAvailableStyle()
        {
            ICoreFontInfo  fm = sm_Fonts[this.m_FontName];
            enuFontStyle v_fs = enuFontStyle.Regular;
            v_fs = fm.IsStyleAvailable(enuFontStyle.Regular) ? enuFontStyle.Regular : 0;
            v_fs |= fm.IsStyleAvailable(enuFontStyle.Bold ) ? enuFontStyle.Bold  : 0;
            v_fs |= fm.IsStyleAvailable(enuFontStyle.Italic ) ? enuFontStyle.Italic  : 0;
            v_fs |= fm.IsStyleAvailable(enuFontStyle.Underline ) ? enuFontStyle.Underline  : 0;
            v_fs |= fm.IsStyleAvailable(enuFontStyle.Strikeout ) ? enuFontStyle.Strikeout : 0;
            return v_fs;
        }
        public bool Support(enuFontStyle style)
        {
            ICoreFontInfo fm = GetFonts(this.m_FontName); ;
            if (fm!=null)
                return fm.IsStyleAvailable(style);
            return false;
        }
        private string m_FontName;
        public string FontName
        {
            get { return m_FontName; }
            set
            {
                string deF =( m_FontName ?? string.Empty).ToLower();
                string deV = (value ?? string.Empty).ToLower();
                if ((deF  != deV) && (sm_Fonts .ContainsKey (deV)))
                {
                    m_FontName = sm_Fonts[deV].FontFamilyName;
                    OnFontDefinitionChanged(EventArgs.Empty);
                }
            }
        }
        private float m_FontSize;

        /// <summary>
        /// get or set the font size in pixel
        /// </summary>
        public float FontSize
        {
            get { return m_FontSize; }
            set
            {
                if ((m_FontSize != value) && (value > 0))
                {
                    m_FontSize = value;
                    OnFontDefinitionChanged(EventArgs.Empty);
                }
            }
        }
        #region ICoreFont Members
        private enuFontStyle m_FontStyle;                
        private enuRenderingMode m_enuRenderingMode;
        public enuFontStyle FontStyle
        {
            get { return m_FontStyle; }
            set
            {
                if ((m_FontStyle != value)&& (this.Support (value )))
                {
                    m_FontStyle = value;
                    OnFontDefinitionChanged(EventArgs.Empty);
                }
            }
        }
        //private bool IsStyleAvailable(enuFontStyle value)
        //{
        //    return false;
        //}
        #endregion
        #region ICoreWorkingDefinitionObject Members
        public string GetDefinition()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append (string.Format ("FontName:{0};", this.FontName ));
            if (this.FontStyle != enuFontStyle.Regular )
            sb.Append(string.Format("Style:{0};", this.FontStyle ));
            sb.Append(string.Format("Size:{0};", this.FontSize));
            if (VerticalAlignment != enuStringAlignment.Near) 
            sb.Append(string.Format("VerticalAlignment:{0};", this.VerticalAlignment ));
            if (HorizontalAlignment  != enuStringAlignment.Near) 
            sb.Append(string.Format("HorizontalAlignment:{0};", this.HorizontalAlignment ));
            if (this.WordWrap)
            {
                sb.Append(string.Format("WordWrap:{0};", this.WordWrap));
            }
            if (this.HotKeyPrefix != enuHotKeyPrefix.Disabled)
            {
                sb.Append(string.Format("HotKeyPrefix:{0};", this.HotKeyPrefix));
            }
            return sb.ToString();
        }
        public override string ToString()
        {
            return string.Format ("CoreFont[{0}]",this.GetDefinition());
        }
        public void CopyDefinition(string str)
        {
            if (str == null)
                return ;
            string[] v_tab = str.Split(';', ':');

            this.m_FontStyle = enuFontStyle.Regular;
            this.m_hAlignment = enuStringAlignment.Near;
            this.m_vAlignment = enuStringAlignment.Near;
            this.m_WordWrap = false;
            this.m_HotKeyPrefix = enuHotKeyPrefix.Disabled;
            if (v_tab.Length == 1)
            {

                this.m_FontName = v_tab[0];
            }
            else
            {

                for (int i = 0; i < v_tab.Length; i += 2)
                {
                    if ((i + 1) >= v_tab.Length)
                        break;
                    switch (v_tab[i].Trim().ToLower())
                    {
                        case "fontname":
                            string ft = v_tab[i + 1].ToLower();
                            if (sm_Fonts.ContainsKey(ft))
                                this.m_FontName = ft;
                            break;
                        case "horizontalalignment":
                            this.m_hAlignment = CoreExtensions.GetValue<enuStringAlignment>(v_tab[i + 1]);
                            break;
                        case "verticalalignment":
                            this.m_vAlignment = CoreExtensions.GetValue<enuStringAlignment>(v_tab[i + 1]);
                            break;
                        case "style":
                            this.m_FontStyle = CoreExtensions.GetValue<enuFontStyle>(v_tab[i + 1], enuFontStyle.Regular);
                            break;
                        case "size":
                            var s = v_tab[i + 1];
                            CoreUnit unit = ((CoreUnit)s);
                            this.m_FontSize = unit.GetPixel();
                            break;
                        case "wordwrap":
                            this.m_WordWrap = (bool)(new CoreBooleanTypeConverter().ConvertFromString(v_tab[i + 1]));
                            break;
                        case "hotkeyprefix":
                            this.m_HotKeyPrefix = CoreExtensions.GetValue<enuHotKeyPrefix>(v_tab[i + 1]);
                            break;
                    }
                }
            }
            CheckFontStyle();
            OnFontDefinitionChanged(EventArgs.Empty);
        }
        private void CheckFontStyle()
        {
            if (!this.Support(this.FontStyle))
            {
                this.m_FontStyle = GetAllAvailableStyle();
            }
        }
        #endregion
        #region ICoreDisposableObject Members
        public bool IsDisposed
        {
            get { return m_isDisposed; }
        }
        #endregion
        #region IDisposable Members
        public void Dispose()
        {
            this.m_isDisposed = true;
            DisposeFont();
        }
        #endregion
        public ICoreFont GetFont()
        {
            return this.m_font;
        }
    
        private void GenerateFont()
        {
            DisposeFont();
            if( (this.m_FontSize > 0)&&(!string.IsNullOrEmpty (this.FontName )))
            {
                //check font style before creatit
                CheckFontStyle();
                this.m_font = new CoreFont(this.FontName,
                    this.FontSize,
                    this.FontStyle,
                    enuRenderingMode.Pixel);
            }
        }
        private void DisposeFont()
        {
            if (this.m_font != null)
            {
                this.m_font.Dispose();
                this.m_font = null;
            }
        }

        /// <summary>
        /// get the em size of the font according to font family
        /// </summary>
        /// <returns></returns>
        public int GetEmHeight()
        {
            ICoreFontFamilyDefinition d = 
                this.GetFontFamily();
            return d!=null? d.GetEmHeight(this) : 0;
        }
        private ICoreFontFamilyDefinition GetFontFamily()
        {
            if (sm_Fonts.ContainsKey(this.FontName.ToLower()))
                return sm_Fonts[this.FontName.ToLower()];
            return null;
        }
       
        public int GetCellDescent() {
            return this.GetFontFamily().GetCellDescent(this); 
        }
        public int GetCellAscent() {
           return  this.GetFontFamily().GetCellAscent(this);
        }
        /// <summary>
        /// return the line height in pixel
        /// </summary>
        /// <returns></returns>
        public  float GetLineHeight()
        {
            float v_emheight = this.GetEmHeight();
            float v_lineSpacing = this.GetLineSpacing(); //get line spacing in em
            return this.FontSize * v_lineSpacing / v_emheight; //this.GetLineSpacing() / this.GetEmHeight();        
        }
        #region ICoreFont Members

        public enuStringAlignment HorizontalAlignment
        {
            get
            {
                return this.m_hAlignment;
            }
            set
            {
                if (this.m_hAlignment != value)
                {
                    this.m_hAlignment = value;
                    OnFontDefinitionChanged(EventArgs.Empty);
                }
            }
        }
        public enuStringAlignment VerticalAlignment
        {
            get
            {
                return this.m_vAlignment;
            }
            set
            {
                if (this.m_vAlignment != value)
                {
                    this.m_vAlignment = value;
                    OnFontDefinitionChanged(EventArgs.Empty);
                }
            }
        }
        #endregion
        class CoreFontTypeConverter : System.ComponentModel.TypeConverter
        {
            public override object ConvertFrom(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
            {
                if (value is CoreFont)
                    return value;
                if (value is string)
                {
                    string vp = value as string;
                    CoreFont ft = new CoreFont();
                    ft.CopyDefinition(vp);
                    return ft;
                }
                return base.ConvertFrom(context, culture, value);
            }
        }
        public string GetDefinition(IGK.ICore.Codec.IXMLSerializer seri)
        {
            return this.GetDefinition();
        }
       
        bool ICoreFont.IsStyleAvailable(enuFontStyle enuFontStyle)
        {
            return false;
        }

        /// <summary>
        /// register fonts
        /// </summary>
        /// <param name="c"></param>
        public static void RegisterFonts(ICoreFontInfo c)
        {
            if (!sm_Fonts.ContainsKey(c.FontFamilyName.ToLower()))
            {
                CoreLog.WriteLine("AddCustom font : " + c.FontFamilyName);
                sm_Fonts.Add(c.FontFamilyName.ToLower(), c);
            }
            else {
                CoreLog.WriteLine("Replace existing font");
                sm_Fonts[c.FontFamilyName.ToLower()] = c;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fontName"></param>
        /// <param name="size"></param>
        /// <param name="style"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        public static CoreFont CreateFont(string fontName, 
            float size, 
            enuFontStyle style, 
            enuRenderingMode mode)
        {
            CoreFont fm = new CoreFont(null, "Consolas");
            fm.FontName = fontName;
            fm.FontSize = size;
            fm.FontStyle = style;
            return fm;
        }

        public static CoreFont CreateFont(ICoreWorkingObject  owner, string p)
        {
            return new CoreFont(owner, p);
        }
        public void Copy(ICoreFont ft)
        {
            if (ft == null)
                return;
            this.CopyDefinition(ft.GetDefinition());
        }
    }
}

