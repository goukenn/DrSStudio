

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WinCoreRenderer.cs
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
file:WinCoreRenderer.cs
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
using System.Text;
using System.Xml;
using System.Runtime.InteropServices;
using System.ComponentModel;
using Microsoft.Win32;
/*[assembly: System.Security.Permissions.FileIOPermission(System.Security.Permissions.SecurityAction.RequestMinimum, 
    ViewAndModify="FullTrust")]*/
//[assembly: System.Security.Permissions.RegistryPermission(System.Security.Permissions.SecurityAction.RequestMinimum,
//    ViewAndModify="FullTrust")]
namespace IGK.ICore.WinUI
{
    using System.IO;
    using IGK.ICore.WinUI;
    using IGK.ICore;
    using IGK.ICore.ComponentModel;
    using IGK.ICore.Settings;
using System.Collections;
    using IGK.ICore.Drawing2D;
    using IGK.ICore.Resources;
    /// <summary>
    /// represent the Renderer setting
    /// </summary>
    public static class CoreRenderer 
    {
        private static Dictionary<string, ICoreRendererSetting> sm_renderingAttributes;
        public static event EventHandler RenderingValueChanged;

        public static IEnumerable GetRendereringEnumerator() {
            return sm_renderingAttributes;
        }

        private static void OnRenderingValueChanged(ICoreRendererSetting setting, EventArgs e)
        {
            if (RenderingValueChanged != null)
                RenderingValueChanged(setting, e);
        }

        internal static void SetValue(string name, object value)
        {
            if (sm_renderingAttributes.ContainsKey (name ))
            {
                sm_renderingAttributes[name].Value = value;
            }
        }
        private static void SetValue<T>(string name, T value)
        {
            if (sm_renderingAttributes.ContainsKey(name))
            {
                if (sm_renderingAttributes[name].Value is T)
                {
                    sm_renderingAttributes[name].Value = value;
                    return;
                }
            }            
        }       
        /// <summary>
        /// .ctr 
        /// </summary>
        static CoreRenderer()
        {            
            sm_renderingAttributes = new Dictionary<string, ICoreRendererSetting>();
            CoreRendererBase.InitRenderer(System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType);
            LoadCurrentSkin();
            CoreApplicationManager.ApplicationExit  += _ApplicationExit;
        }
#if DEBUG
        /// <summary>
        /// reset the display skins
        /// </summary>
        public static void ResetStyle() {
            sm_renderingAttributes.Clear();

        }
#endif

        static void _ApplicationExit(object sender, EventArgs e)
        {
            SaveCurrentSkin();
        }
        internal static void LoadCurrentSkin()
        {
            if (CoreApplicationManager.Application == null)
            {
                return;
            }
            string p = CoreApplicationManager.Application.UserAppDataPath;
            string file = Path.Combine (p , CoreConstant.SKIN_FILE);
            if (System.IO.File.Exists(file))
            {
                Configure(file);
            }
            else {
                byte[] data = CoreResources.GetResource("app_skin");
                if ((data !=null) && (data.Length > 0))
                    Configure (data );
            }
        }
        internal static void SaveCurrentSkin()
        {
            SaveSkin();
        }

        public static Colorf BezierCurveSelectionColor { get { return CoreRenderer.GetColor(nameof(BezierCurveSelectionColor), Colorf.FromByteRgb(0, 157, 236)); } }
        public static Colorf BackgroundColor { get {return GetColor("BackgroundColor", Colorf.FromFloat (0.25f));} }
        public static Colorf ForeColor { get { return GetColor("ForeColor", Colorf.White); } }

        public static Colorf SeparatorDark { get { return GetColor("MenuSeparatorDark", Colorf.Orange); } }
        public static Colorf SeparatorLight { get { return GetColor("MenuSeparatorLight", Colorf.Red); } }
        //ruler info
        public static Colorf RuleSeparatorDark { get { return GetColor("RuleSeparatorDark", Colorf.FromFloat (0.2f)); } }
        public static Colorf RuleSeparatorLight { get { return GetColor("RuleSeparatorLight", Colorf.FromFloat (0.40f)); } }
        //disabble menu
        public static Colorf MenuStripDisableColor { get { return CoreRenderer.GetColor("MenuStripDisableColor", Colorf.FromFloat(0.5f)); } }
        public static Colorf SplitterBackgroundColor { get { return CoreRenderer.GetColor("SplitterBackgroundColor", Colorf.FromFloat (0.7f)); } }
        //global fore color
        
        /// <summary>
        /// retreive all current renderer settings
        /// </summary>
        /// <returns></returns>
        public static ICoreRendererSetting[] GetCurrentRendererSettings()
        {
            List<ICoreRendererSetting> m_settings = new List<ICoreRendererSetting>();
            foreach (ICoreRendererSetting s in sm_renderingAttributes.Values)
            {
                m_settings.Add(s);
            }
            return m_settings.ToArray();
        }

        /// <summary>
        /// get the current rendereing value
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public static T GetValue<T>(string key)
        {
            if (sm_renderingAttributes.ContainsKey(key))
            {
                ICoreRendererSetting v_t = sm_renderingAttributes[key];
                if ((v_t.Value != null) && (v_t.Value is T))
                    return (T)v_t.Value;
            }
            return default(T);
        }
        /// <summary>
        /// get color
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static Colorf GetColor(string key)
        {
            if (sm_renderingAttributes.ContainsKey(key))
            {
                ICoreRendererSetting v_t = sm_renderingAttributes[key];
                if ((v_t.Value !=null) &&  v_t.Value.GetType() == typeof ( Colorf ))
                    return (Colorf)v_t .Value ;
            }
            return Colorf.Empty;
        }
        /// <summary>
        /// get the color if not registrated
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultColor"></param>
        /// <returns></returns>
        public static Colorf GetColor(string key, Colorf defaultColor)
        {
            if (sm_renderingAttributes.ContainsKey(key))
            {
                return _GetColor(key, defaultColor );
            }
            else {
                ICoreRendererSetting c = new CoreRendererSetting(key, enuRendererSettingType.Color, defaultColor);
                sm_renderingAttributes.Add(c.Name, c);
                return defaultColor;
            }
        }
        private static Colorf _GetColor(string key, Colorf defaultColor)
        {          
                ICoreRendererSetting v_t = sm_renderingAttributes[key];
                if (v_t.Type == enuRendererSettingType.Color)
                {
                    if (v_t.Value == null)
                    {
                        v_t.Value = defaultColor;
                        return defaultColor;
                    }
                    if (v_t.Value.GetType() == typeof(Colorf))
                        return (Colorf)v_t.Value;
                    else { 
                        //convert the default value to colorf
                        if (v_t.Value != null)
                        {
                            TypeConverter v_conv = CoreTypeDescriptor.GetConverter(typeof(Colorf));
                            v_t.Value =(Colorf) v_conv.ConvertFromString (v_t.Value.ToString());
                            return (Colorf)v_t.Value;
                        }
                    }
                }
                if (v_t.Value != null)
                {
                    //check for match type 
                    if (v_t.Value.GetType() == typeof(Colorf))
                        return (Colorf)v_t.Value;
                    else
                        throw new CoreException(enuExceptionType.OperationNotValid, "ERR.CORERENDERER.TYPEDONTMATCH");
                }
                v_t.Value = defaultColor;
                return defaultColor;
        }
        /// <summary>
        /// get the float type
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultColor"></param>
        /// <returns></returns>
        public static float GetFloat(string key, float defaultValue)
        {
            if (sm_renderingAttributes.ContainsKey(key))
            {
                ICoreRendererSetting v_t = sm_renderingAttributes[key];
                if (v_t.Value != null)
                {                
                    //check for match type 
                    if (v_t.Value.GetType() == typeof(float))
                        return (float)v_t.Value;
                    else
                    {
                         throw new CoreException(enuExceptionType.OperationNotValid, "ERR.CORERENDERER.TYPEDONTMATCH".R());
                    }
                }
                v_t.Value = defaultValue;
                return defaultValue;
            }
            else
            {
                Add(new CoreRendererSetting(key, enuRendererSettingType.Float, defaultValue));
                return defaultValue;
            }
        }
        private static void Add(CoreRendererSetting setting)
        {
            if ((setting == null) || string.IsNullOrEmpty (setting.Name ))
                return;
            if (sm_renderingAttributes.ContainsKey(setting.Name))
            {
                //replact
                sm_renderingAttributes[setting.Name].ValueChanged -= setting_ValueChanged;
                sm_renderingAttributes[setting.Name] = setting;
            }
            else
            {
                sm_renderingAttributes.Add(setting.Name, setting);                
            }
            setting.ValueChanged += setting_ValueChanged;
        }

        static void setting_ValueChanged(object sender, EventArgs e)
        {
            OnRenderingValueChanged((ICoreRendererSetting)sender, e);
        }
        /// <summary>
        /// clear all renderer setting
        /// </summary>
        public static void Clear()
        {
            sm_renderingAttributes.Clear();
 
        }
        /// <summary>
        /// configure from file name
        /// </summary>
        /// <param name="filename"></param>
        public static void Configure(string filename)
        {
           Configure(File.ReadAllBytes (filename ));
        }
        public static void Configure(byte[] data)
        {
            MemoryStream mem = new MemoryStream ();
            mem.Write (data, 0, data.Length );
            XmlReaderSettings v_setting = new XmlReaderSettings();
          //  string b = Encoding.UTF8.GetString(data);

            v_setting.CloseInput = true;
            v_setting.IgnoreComments = true;
            v_setting.IgnoreProcessingInstructions = true;
            mem.Seek(0, SeekOrigin.Begin);

            XmlReader v_xreader = XmlReader.Create(mem, v_setting);
            string v_key = string.Empty;
            enuRendererSettingType v_settingType = enuRendererSettingType.Color;
            object v_value = null;
            TypeConverter v_conv = CoreTypeDescriptor.GetConverter (typeof(CoreFont));

            try
            {
                if (v_xreader.ReadToDescendant("Skins"))
                {
                    while (v_xreader.Read())
                    {
                        switch (v_xreader.NodeType)
                        {
                            case XmlNodeType.Element:
                                v_key = v_xreader.Name;
                                v_settingType = (enuRendererSettingType)Enum.Parse(typeof(enuRendererSettingType), v_xreader.GetAttribute("Type"));
                                v_value = v_xreader.GetAttribute("Value");
                                switch (v_settingType)
                                {
                                    case enuRendererSettingType.Unknow:
                                        break;
                                    case enuRendererSettingType.String:
                                        break;
                                    case enuRendererSettingType.Color:
                                        break;
                                    case enuRendererSettingType.Float:
                                        break;
                                    case enuRendererSettingType.Int:
                                        break;
                                    case enuRendererSettingType.File:
                                        break;
                                    case enuRendererSettingType.Font:
                                        v_value = v_conv.ConvertFrom(v_value);
                                        break;
                                    default:
                                        break;
                                }
                                CoreRendererSetting c = new CoreRendererSetting(
                                    v_key,
                                    v_settingType,
                                    v_value);
                                Add(c);

                                break;
                        }
                    }
                }
            }
            catch(Exception ex) {
                CoreMessageBox.Show("Error When reading Setting : "+ex.Message ,
                    "ERROR APPEND");
            }
            finally {
                v_xreader.Close();
            }
        }
        /// <summary>
        /// save skin to specified file
        /// </summary>
        /// <param name="filename"></param>
        public static void Save(string filename)
        {
            //Save global user defined skin
            XmlWriterSettings v_setting = new XmlWriterSettings();
            v_setting.Indent = true;
            v_setting.CloseOutput = true;
            XmlWriter xwriter = null;
            try
            {
                xwriter = XmlWriter.Create(filename,
                v_setting);
                xwriter.WriteStartElement("Skins");
                
                foreach (KeyValuePair<string, ICoreRendererSetting> k in sm_renderingAttributes)
                {
                    if (k.Value.Value != null)
                    {
                        var v_conv = CoreTypeDescriptor.GetConverter(k.Value.Value);
                        xwriter.WriteStartElement(k.Key);
                        xwriter.WriteAttributeString("Type", k.Value.Type.ToString());
                        xwriter.WriteAttributeString("Value", v_conv.ConvertToString ( k.Value.Value));
                        xwriter.WriteEndElement();
                    }
                }
                xwriter.WriteEndElement();
                xwriter.Flush();
            }
            catch (Exception ex)
            {
                CoreLog.WriteLine("Save Skin - Failed : " + ex.Message );
#if DEBUG
                CoreMessageBox.Show("Save Sking Failed.", "Error On WinCoreRenderer");
#endif
            }
            finally
            {
                xwriter.Close();
            }
        }

        public static void SaveToCompile(string filename)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("static class CoreRendererCode{");
            foreach (KeyValuePair<string, ICoreRendererSetting> k in sm_renderingAttributes)
            {
                switch (k.Value.Type)
                {
                    case enuRendererSettingType.Color:
                        sb.AppendLine(
string.Format("public static Colorf {0} {{ get {{ return WinCoreRenderer.GetColor(\"{0}\", Colorf.FromString(\"{1}\")); }} }}", k.Key, k.Value.Value)
);

                        break;
                    case enuRendererSettingType.Font :
 //                             public static CoreFont DefaultFont { get { return CoreThemeManager.GetValue<CoreFont>("DefaultFont", "FontName:Tahoma; size:8pt;"); } }
 
                        sb.AppendLine(
string.Format("public static CoreFont {0} {{ get {{ return CoreThemeManager.GetValue<CoreFont>(\"{0}\", \"{1}\"); }} }}", k.Key, ((IGK.ICore.Drawing2D.CoreFont)k.Value.Value).GetDefinition())
);
                        break;
                    case enuRendererSettingType.Float:
                        sb.AppendLine(
string.Format("public static float {0} {{ get {{ return (float)WinCoreRenderer.GetValue(\"{0}\", {1}f); }} }}", k.Key, k.Value.Value)
);
                        break;
                    case enuRendererSettingType.Int:
                        sb.AppendLine(
string.Format("public static int {0} {{ get {{ return (int)WinCoreRenderer.GetValue(\"{0}\", {1}); }} }}", k.Key, k.Value.Value)
);
                        break;
                    case enuRendererSettingType.File:
                        sb.AppendLine(
string.Format("public static string {0} {{ get {{ return (float)WinCoreRenderer.GetValue(\"{0}\", \"{1}\"); }} }}", k.Key, k.Value.Value)
);
                        break;
                    default:
                        sb.AppendLine(
string.Format("public static object {0} {{ get {{ return WinCoreRenderer.GetValue(\"{0}\", null); }} }}", k.Key)
);
                        break;
                }
          
            }
            sb.AppendLine("}");
            File.WriteAllText(filename, sb.ToString());
        }
        private static void SaveSkin()
        {
            string p = CoreApplicationManager.Application.UserAppDataPath;
            string file = Path.Combine(p, CoreConstant.SKIN_FILE);
            Save(file);                    
          
        }

      
        public static void Remove(ICoreRendererSetting pro)
        {
            if (sm_renderingAttributes.ContainsKey(pro.Name))
            {
                sm_renderingAttributes.Remove(pro.Name);
                pro.ValueChanged += setting_ValueChanged;
            }
        }
        /// <summary>
        /// set the color
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SetColor(string key, Colorf value)
        {
            if (sm_renderingAttributes.ContainsKey(key))
            {
                sm_renderingAttributes[key].Value = value;
            }
        }
        public static int TitleHeight { get {
            return GetValue<int>("TitleHeight");
        } set{
                SetValue<int>("TitleHeight", value);
        } }

      
       

        public static Colorf SceneBackgroundShadow { get { return CoreRenderer.GetColor("SceneBackgroundShadow", Colorf.FromFloat(0.4f, 0.2f)); } }
        public static Colorf SceneBackground { get { return CoreRenderer.GetColor("SceneBackground", Colorf.White); } }

        public static void LoadSettings(CoreRendererSetting[] coreRendererSetting)
        {
            sm_renderingAttributes.Clear();
            foreach (var item in coreRendererSetting )
            {
                if (!sm_renderingAttributes.ContainsKey(item.Name))
                    sm_renderingAttributes.Add(item.Name, item);
                else
                    sm_renderingAttributes[item.Name] = item;
            }
        }


        /// <summary>
        /// get the setting with default value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static ICoreRendererSetting GetSetting<T>(string name , T defaultValue = default (T))
        {
            if (sm_renderingAttributes.ContainsKey(name))
            {
                var s = sm_renderingAttributes[name] ;                
                return s;
            }
            enuRendererSettingType ft = _GetSettingType(defaultValue);
            if (ft == enuRendererSettingType.Unknow)
                return null;
            //register default value
            CoreRendererSetting rsetting = new CoreRendererSetting(name, ft, defaultValue);

            sm_renderingAttributes.Add(name, rsetting );
            return rsetting;

            

        }

        private static enuRendererSettingType _GetSettingType(object r)
        {
            if (r == null)
                return enuRendererSettingType.Unknow;

            Type t = r.GetType();
            if (t == typeof(Colorf))
                return enuRendererSettingType.Color;
            if (t.ImpletementInterface(typeof(ICoreFont)))
                return enuRendererSettingType.Font;
            if (t == typeof(float))
                return enuRendererSettingType.Float;
            if (t == typeof(int))
                return enuRendererSettingType.Float;

            return enuRendererSettingType.Unknow;
        }
    }
}

