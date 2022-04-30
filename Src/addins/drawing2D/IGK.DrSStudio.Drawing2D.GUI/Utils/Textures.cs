

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Textures.cs
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
file:Textures.cs
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
using System.Xml;
using System.Windows.Forms;
using System.Drawing;
using IGK.ICore.WinCore;
using IGK.ICore;
using IGK.ICore.IO;
using IGK.ICore.Settings;
namespace IGK.DrSStudio.Drawing2D.Utils
{
    public class TextureInfoEventArgs : EventArgs 
    {
        private Textures.TexturesInfo  m_TextureInfo;
public Textures.TexturesInfo  TextureInfo{
get{return m_TextureInfo;}
}
        public TextureInfoEventArgs(Textures.TexturesInfo info)
        { 
            this.m_TextureInfo = info ; 
        }
        public override string  ToString()
        {
 	         return "TexureInfoEventArgs";
        }
    }
      public delegate void TextureInfoEventHandler(object sender, TextureInfoEventArgs e);
    /// <summary>
    /// represent the textures manager class
    /// </summary>
    public static class Textures
    {
        static Dictionary<string, TexturesInfo> m_rDics;
        static bool m_needToSave;
        const string TEXTURE_LIBFOLDER = "TextureFolder";
        const string TEXTURE_DEFAULTVALUE = "%startup%/Textures";
        public static event TextureInfoEventHandler  TextureAdded;
        /// <summary>
        /// get the number of the texture
        /// </summary>
        public static int Count {
            get {
                return m_rDics.Count;
            }
        }
        static Textures()
        {
            m_rDics = new Dictionary<string, TexturesInfo>();
            Application.ApplicationExit += new EventHandler(Application_ApplicationExit);
            Load();
        }
        static void Application_ApplicationExit(object sender, EventArgs e)
        {
            Save();
        }
        public static bool AddImage(string name, System.Drawing.Bitmap bitmap)
        {
            if (!m_rDics.ContainsKey(name))
            {
                TexturesInfo v_txInfo = new TexturesInfo(name, bitmap);
                v_txInfo.Save();
                m_rDics.Add(name, v_txInfo);
                m_needToSave =  true ;
                OnTextureAdded(new TextureInfoEventArgs(v_txInfo));
                return true;
            }
            return false;
        }
        private static void OnTextureAdded(TextureInfoEventArgs e)
        {
            if (TextureAdded !=null)
                TextureAdded(CoreSystem.Instance , e);
        }
        /// <summary>
        /// get an array of textures info
        /// </summary>
        /// <returns></returns>
        public static TexturesInfo[] GetRegisteredTexturesInfo()
        {
            List<TexturesInfo> m_textures = new List<TexturesInfo>();
            foreach (KeyValuePair<string, TexturesInfo> item in m_rDics )
            {
                m_textures.Add(item.Value);
            }
            return m_textures.ToArray();
        }
        /// <summary>
        /// load texture from texture library
        /// </summary>
        public static void Load()
        { 
            //load from texture lib
            XmlReaderSettings v_setting = new XmlReaderSettings();
            string t = PathUtils.GetPath(TextureFolder);
            string fname = PathUtils.GetPath(t + "/textures.infos");
            if (System.IO.File.Exists (fname ) == false )
                return ;
            XmlReader v_reader= XmlReader.Create(fname, v_setting );
            string v_bckdir = Environment.CurrentDirectory;
            Environment.CurrentDirectory = t;
            while (v_reader.Read())
            {
                switch (v_reader.NodeType)
                { 
                    case XmlNodeType.Element :
                        if (v_reader.Name.ToLower() == "item")
                        {
                            string name = v_reader.GetAttribute("Name");
                            string v_fname = v_reader.GetAttribute("FileName");
                            TexturesInfo v_txInfo = TexturesInfo.Create(name, v_fname);
                            if (v_txInfo != null)
                            {
                                m_rDics.Add(name, v_txInfo);
                            }
                        }
                        break;
                }
            }
            v_reader.Close();
            m_needToSave = false;
            //restore 
            Environment.CurrentDirectory = v_bckdir;
        }
        public static string TextureFolder{
            get {
                ICoreSettingValue d = CoreApplicationSetting.Instance["TextureFolder"];
                if ((d == null)|| (d is CoreSettingBase.DummySetting ))
                {
                    d = new CorePropertySetting (TEXTURE_LIBFOLDER , TEXTURE_DEFAULTVALUE);
                    CoreApplicationSetting.Instance[TEXTURE_LIBFOLDER ] = d;
                }
                return d.Value.ToString();
            }
        }
        public static void Save()
        {
            if (m_needToSave)
            {
                CoreLog.WriteDebug("Save Textures Info");
                XmlWriterSettings v_setting = new XmlWriterSettings();
                v_setting.Indent = true;
                string t = TextureFolder;
                PathUtils.CreateDir(PathUtils .GetPath (t));
                XmlWriter v_xwriter = XmlWriter.Create(PathUtils.GetPath(t+ "/textures.infos"), v_setting );
                v_xwriter.WriteStartElement("Textures");
                foreach (KeyValuePair<string, TexturesInfo> item in m_rDics)
                {
                    v_xwriter.WriteStartElement("Item");
                    v_xwriter.WriteAttributeString("Name", item.Value.Name);
                    v_xwriter.WriteAttributeString("Width", item.Value.Width.ToString() );
                    v_xwriter.WriteAttributeString("Height", item.Value.Height.ToString ());
                    v_xwriter.WriteAttributeString("FileName", item.Value.FileName);
                    v_xwriter.WriteEndElement();
                }
                v_xwriter.WriteEndElement();
                v_xwriter.Flush();
                v_xwriter.Close();
            }
        }
        /// <summary>
        /// represent the texture informations
        /// </summary>
        public class TexturesInfo
        {
            private string m_Name;
            private int m_Width;
            private int m_Height;
            private System.Drawing.Bitmap m_Bitmap;
            private string m_FileName;
            public string FileName
            {
                get { return m_FileName; }
                set
                {
                    if (m_FileName != value)
                    {
                        m_FileName = value;
                    }
                }
            }
            public System.Drawing.Bitmap Bitmap
            {
                get { return m_Bitmap; }
            }
            public int Height
            {
                get { return m_Height; }
                set
                {
                    if (m_Height != value)
                    {
                        m_Height = value;
                    }
                }
            }
            public int Width
            {
                get { return m_Width; }
                set
                {
                    if (m_Width != value)
                    {
                        m_Width = value;
                    }
                }
            }
            public string Name
            {
                get { return m_Name; }
                set
                {
                    if (m_Name != value)
                    {
                        m_Name = value;
                    }
                }
            }
            public TexturesInfo(string name, System .Drawing.Bitmap bitmap)
            {
                if (string.IsNullOrEmpty(name))
                    throw new ArgumentException($"{nameof(name)}");
                if (bitmap == null)
                    throw new ArgumentException($"{nameof(bitmap)}");
                this.m_Name = name;
                this.m_Bitmap = bitmap.Clone () as Bitmap;
                this.m_Width = bitmap.Width;
                this.m_Height = bitmap.Height;
                this.m_FileName = string.Format("{0}_{1}x{2}.data", name, this.Width, this.Height);
            }
            public void Save()
            {
                //------------
                //save texture
                //------------
                String str = WinCoreBitmapOperation.BitmapToBase64String(this.m_Bitmap , 1);
                string dir = TextureFolder;
                PathUtils.CreateDir(PathUtils.GetPath(dir));
                System.IO.File.WriteAllText(PathUtils.GetPath(dir + "/" + this.FileName), str);
            }
            public static TexturesInfo Create(string name, string filename)
            {
                if (System.IO.File.Exists(filename))
                {
                    Byte[] v_tab = Convert.FromBase64String(System.IO.File.ReadAllText(filename));
                    System.IO.MemoryStream mem = new System.IO.MemoryStream();
                    mem.Write(v_tab, 0, v_tab.Length);
                    mem.Seek(0, System.IO.SeekOrigin.Begin);
                    Bitmap bmp = null;
                    try
                    {
                        bmp = Bitmap.FromStream(mem) as Bitmap ;
                    }
                    catch
                    {
                    }
                    finally
                    {
                      mem.Dispose();
                    }
                    if (bmp != null)
                    {
                        return new TexturesInfo(name, bmp);
                    }
                }
                return null;
            }
        }
    }
}

