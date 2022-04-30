/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ConfigurationManager.cs
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
file:ConfigurationManager.cs
*/

﻿using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using Microsoft.Win32;
using IGK.ICore;using IGK.ICore.ComponentModel;
using IGK.ICore.Codec;
namespace IGK.ICore.Settings
{
    /// <summary>
    /// represent the base configuration manager
    /// </summary>
    public static class SettingManager  
    {
		static ICoreSystem sm_coreSystem;
		public static ICoreSystem CoreSystem{
			get{
				return sm_coreSystem;
			}
		}
		public static void Configure(ICoreSystem coreSystem)
		{
			sm_coreSystem = coreSystem ;
			Configure ();
		}        
        static SettingManager()
        {
            CoreApplicationManager.ApplicationExit += new EventHandler(_ApplicationExit);           
        }
        public static void LoadConfig(string file)
        {
            if (File.Exists(file))
            {
                XmlReader xreader = XmlReader.Create(file);
                try
                {
                    if (xreader.ReadToDescendant(CoreConstant.SETTING_TAG))
                    {
                        CoreSettingCollections v_i = IGK.ICore.CoreSystem.GetSettings();
                        //load setting
                        while (xreader.Read())
                        {
                            switch (xreader.NodeType)
                            {
                                case XmlNodeType.Element:
                                    XmlReader xvreader = xreader.ReadSubtree();
                                    CoreSettingBase.DummySetting v_of = new CoreSettingBase.DummySetting(xreader.Name);
                                    v_i.Add(xreader.Name, v_of);
                                    v_of.Load(xvreader);
                                    break;
                            }
                        }
                    }
                }
                catch (System.Xml.XmlException xmlEx)
                {
                    CoreLog.WriteDebug(xmlEx.Message);
                }
                catch (Exception ex)
                {
                    CoreLog.WriteDebug(ex.Message);
                }
                finally {
                    if (xreader !=null)
                    xreader.Close();
                }
            }
        }
        /// <summary>
        /// load setting from registry
        /// </summary>
        public static void LoadFromReg()
        {
            throw new NotImplementedException();
        }
        static void _ApplicationExit(object sender, EventArgs e)
        {
            SaveSetting();            
        }
        /// <summary>
        /// save the setting configuration file
        /// </summary>
        public static void SaveSetting()
        {
            XmlWriterSettings xwsetting = new XmlWriterSettings();
            xwsetting.Indent = true;
            xwsetting.OmitXmlDeclaration = true;
            xwsetting.CloseOutput = true;
            string f = Path.Combine (CoreApplicationManager.UserAppDataPath , CoreConstant.SETTING_FILE);
            CoreXMLSerializer xw = CoreXMLSerializer.Create(XmlWriter.Create (f, xwsetting));

            xw.WriteStartElement(CoreConstant.SETTING_TAG);
            foreach (KeyValuePair<string, ICoreSetting> v in IGK.ICore.CoreSystem.GetSettings())
            {
                xw.WriteStartElement(v.Key);

                if (v.Value is ICoreSerializerService)
                {
                    (v.Value as ICoreSerializerService).Serialize(xw);
                }
                else
                {
                    foreach (KeyValuePair<string, ICoreSettingValue> item in v.Value)
                    {
                        if (item.Value != null)
                        {
                            WriteValue(xw, item.Value);
                        }
                    }
                }
                xw.WriteEndElement();
            }
            xw.WriteEndElement();
            xw.Flush();
            xw.Close();
        }
        private static void WriteValue(CoreXMLSerializer xw, ICoreSettingValue app)
        {
            System.ComponentModel.TypeConverter v_conv =null;
            if ((app.Value == null) && !app.HasChild)
                return;

            xw.WriteStartElement(app.Name);
            if (app.Value != null)
            {
                if (app.Value is IGK.ICore.Codec.ICoreSerializerService )
                {
                    (app.Value as IGK.ICore.Codec.ICoreSerializerService).Serialize(xw);
                }
                if (app.Value is ICoreWorkingDefinitionObject)
                {
                    xw.WriteValue ( (app.Value as ICoreWorkingDefinitionObject).GetDefinition());
                }
                else
                {
                    if (app.Value.GetType () == typeof (string []))
                    {
                        v_conv = new CoreArrayStringConverter ();
                    }
                   else{
                        v_conv = CoreTypeDescriptor.GetConverter(app.Value);
                    }
                    xw.WriteValue (v_conv.ConvertToString(app.Value));
                }
            }

            if (app.HasChild)
            {
                //write child
                foreach (KeyValuePair<string, ICoreSettingValue>  item in app)
                {
                    WriteValue(xw, item.Value );
                }
            }
            xw.WriteEndElement();
        }
        internal static void Configure()
        {
            LoadConfig(Path.Combine (CoreApplicationManager.UserAppDataPath , CoreConstant.SETTING_FILE ));
        }
    }
}

