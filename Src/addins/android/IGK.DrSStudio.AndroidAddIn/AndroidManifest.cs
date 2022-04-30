

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AndroidManifest.cs
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
file:AndroidManifest.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;

using IGK.ICore;using IGK.ICore.Xml;
namespace IGK.DrSStudio.Android
{
    /// <summary>
    /// represent a android manifest files
    /// </summary>
    public sealed class AndroidManifest : IAndroidManifest 
    {
        private bool m_IsValid;
        
        IGK.ICore.Xml.CoreXmlElement m_manifestNode;
        public bool IsValid
        {
            get { return m_IsValid; }
        }
        public AndroidManifest()
        {
            this.m_IsValid = true;
            this.m_manifestNode = CoreXmlElement.CreateXmlNode(AndroidConstant.MANIFEST_TAG);
            this.m_manifestNode["xmlns:android"] = AndroidConstant.ANDROID_NAMESPACE;
            this.m_manifestNode.Add("uses-sdk");
            this.m_manifestNode.Add("application");
        }
        /// <summary>
        /// save the manifest file to directory
        /// </summary>
        /// <param name="directory"></param>
        public void SaveToDirectory(string directory)
        {
            if (!Directory.Exists (directory ))
                throw new DirectoryNotFoundException (directory );

            string v_file = Path.Combine ( directory, AndroidConstant.MANIFEST_FILE);
            this.SaveToFile(v_file);
          
        }

        public void SaveToFile(string v_file)
        {
            //serialize manifest
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
            sb.Append(this.m_manifestNode.RenderXML(new CoreXmlSettingOptions()
            {
                Indent = true,
                Context = null
            }));
            File.WriteAllText(v_file, sb.ToString());
        }
        
        public static AndroidManifest LoadManifest(string file)
        {
            if (!File.Exists(file))
                return null;
            AndroidManifest v_manifest = new AndroidManifest();
            v_manifest.Open(file);        
            return v_manifest;
        }
        /// <summary>
        /// open the manifest files 
        /// </summary>
        /// <param name="file"></param>
        internal void Open(string file)
        {
            if (File.Exists(file))
                OpenString(File.ReadAllText(file));
        }

        public bool OpenString(string content)
        {
            XmlReader v_reader = null;
            try
            {

                v_reader = XmlReader.Create(new StringReader(content));
                this.m_manifestNode.Clear();
                if (v_reader.ReadToDescendant("manifest"))
                {
                    this.m_IsValid = true;
                    if (v_reader.HasAttributes)
                    {
                        while (v_reader.ReadAttributeValue())
                            this.m_manifestNode[v_reader.Name] = v_reader.Value;
                    }
                    v_reader.MoveToElement();
                    this.m_manifestNode.LoadString(v_reader.ReadInnerXml());
                    return true;
                }
                else
                {
                    this.m_IsValid = false;
                }
            }
            catch (XmlException xmlException)
            {
                CoreLog.WriteLine(xmlException.Message);
            }
            catch(Exception ex)
            {
                this.m_IsValid = false;
                CoreLog.WriteLine(ex.Message);
            }
            return false;
        }

        internal string Render()
        {
            return this.m_manifestNode.RenderXML(new CoreXmlSettingOptions()
            {
                Indent = true 
            });
        }

        internal static bool CheckManisfest(string text)
        {
            AndroidManifest man = new AndroidManifest();
            man.OpenString(text);
            return man.IsValid;
        }

        internal void LoadContent(string p)
        {
            this.m_manifestNode.ClearChilds();
            this.OpenString(p);
        }
    }
}

