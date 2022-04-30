

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WinCorePalette.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
using IGK.ICore;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:WinCorePalette.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
namespace IGK.DrSStudio
{
    /// <summary>
    /// represent the system palette
    /// </summary>
    public class WinCorePalette : ICoreWorkingObject 
    {
        private string m_Name;
        private Colorf[] m_colors;
        private string m_FileName;
        private enuWinCorePaletteSavingMode  m_SavingMode;
        public enuWinCorePaletteSavingMode  SavingMode
        {
            get { return m_SavingMode; }
            set
            {
                if (m_SavingMode != value)
                {
                    m_SavingMode = value;
                }
            }
        }
        /// <summary>
        /// get or set the filename of the palette
        /// </summary>
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
        #region ICoreIdentifier Membres
        public string Id
        {
            get { return this.m_Name; }
            set { this.m_Name = value; }
        }
        #endregion
        public WinCorePalette(string name)
        {
            this.m_Name = name;
            this.m_colors = new Colorf[0];
        }
        public WinCorePalette(string name, Colorf[] colors)
        {
            if (colors == null)
                throw new CoreException(enuExceptionType.ArgumentIsNull, "colors");
            this.m_Name = name;
            this.m_colors = colors;
        }
        public Colorf[] Colors {
            get {
                return this.m_colors;
            }
            set {
                this.m_colors = value;
            }
        }
        public void SaveTo(string filename)
        {
            XmlWriterSettings setting = new XmlWriterSettings();
            XmlWriter xwriter = null;
            int i = 0;
            try
            {
                setting.Indent = true;
                xwriter = XmlWriter.Create(filename, setting );
                xwriter.WriteStartElement("palette");
                xwriter.WriteAttributeString("Name", this.Id);
                xwriter.WriteAttributeString("SavingMode", this.SavingMode.ToString ());
                foreach (Colorf item in this.Colors)
                {
                    xwriter.WriteStartElement("item");
                    xwriter.WriteAttributeString("name", "color_"+i);
                    if (this.m_SavingMode == enuWinCorePaletteSavingMode.Native)
                    {
                        xwriter.WriteAttributeString("color", Colorf.ConvertToString(item));
                    }
                    else {
                        xwriter.WriteAttributeString("color", Colorf.ConvertToString(item, true));
                    }
                    xwriter.WriteEndElement();
                    i++;
                }
                xwriter.WriteEndElement();
            }
            catch(Exception ex)
            {
                CoreLog.WriteDebug(ex.Message);
            }
            finally {
                if (xwriter != null)
                {
                    xwriter.Flush();
                    xwriter.Close();
                }
            }
        }
        /// <summary>
        /// load file
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static WinCorePalette LoadFile(string filename)
        {
            List<Colorf> cl = new List<Colorf>();
            WinCorePalette pal = null;
            XmlReader xreader = null;
            string v_name = "pal";
            string v = null;
            if (System.IO.File.Exists(filename))
            {
                try
                {
                    xreader = XmlReader.Create(filename);
                    if (xreader.ReadToDescendant("palette"))
                    {
                        v = xreader.GetAttribute("Name");
                        if (!string.IsNullOrEmpty(v))
                            v_name = v;
                        while (xreader.Read())
                        {
                            switch (xreader.NodeType)
                            { 
                                case XmlNodeType.Element :
                                    if (xreader.Name == "item")
                                    {
                                        v = xreader.GetAttribute("color");
                                        if (v != null)
                                        {
                                            Colorf c = Colorf.FromString(v);
                                            if (!cl.Contains(c))
                                                cl.Add(c);
                                        }
                                    }
                                    break;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    CoreLog.WriteDebug(ex.Message);
                }
                finally {
                    if (xreader != null)
                    {
                        xreader.Close();
                        xreader = null;
                    }
                }
            }
            if (cl.Count > 0)
                pal = new WinCorePalette(v_name, cl.ToArray());
            return pal;
        }
    }
}

