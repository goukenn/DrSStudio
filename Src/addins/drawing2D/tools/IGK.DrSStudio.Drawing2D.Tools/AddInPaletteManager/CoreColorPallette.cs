

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreColorPallette.cs
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
using IGK.DrSStudio.Drawing2D.Menu;
using IGK.DrSStudio.Drawing2D.WinUI;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:CoreColorPallette.cs
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
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Xml;
using System.IO;
namespace IGK.DrSStudio
{
    /// <summary>
    /// Represent the palette class used in the palette editor control
    /// </summary>
    public sealed class CoreColorPalette
    {
        private string m_name;
        private Colorf[] m_colors;
        /// <summary>
        /// get the name of the palette color
        /// </summary>
        public string Name
        {
            get
            {
                return m_name;
            }
        }
        /// <summary>
        /// get the colors of this palette
        /// </summary>
        public Colorf[] Colors
        {
            get { return m_colors; }
        }
        public bool Contains(Colorf cl)
        {
            bool v = false;
            for (int i = 0; i < m_colors.Length; ++i)
            {
                if (m_colors[i] == cl)
                    return true;
            }
            return v;
        }
        public CoreColorPalette(string name, Colorf[] m_colors)
        {
            this.m_name = name;
            this.m_colors = m_colors;
        }
        public override string ToString()
        {
            return "Palette : " + this.Name;
        }
        public void Save(string filename)
        {
            if (string.IsNullOrEmpty(filename))
                throw new CoreException(enuExceptionType.ArgumentIsNull, "filename");
            XmlWriterSettings setting = new XmlWriterSettings();
            setting.Indent = true;
            setting.CloseOutput = true;
            setting.Encoding = Encoding.UTF8;
            XmlWriter xwriter = XmlWriter.Create(filename, setting);
            xwriter.WriteStartElement("gkds_palette");
            xwriter.WriteAttributeString("Name", this.Name);
            xwriter.WriteAttributeString("Count", this.m_colors.Length.ToString());
            xwriter.WriteStartElement("Color");
            for (int i = 0; i < this.m_colors.Length; i++)
            {
                xwriter.WriteElementString("Item", Colorf.ConvertToString (this.m_colors[i]));
            }
            xwriter.WriteEndElement();
            xwriter.WriteEndElement();
            xwriter.Close();
        }
        public static CoreColorPalette FromFile(string filename)
        {
            if (string.IsNullOrEmpty(filename))
                throw new CoreException(enuExceptionType.ArgumentIsNull, "filename");
            if (File.Exists(filename) == false)
                throw new CoreException(enuExceptionType.FileNotFound, "filename");
            CoreColorPalette pal = null;
            XmlReader xreader = XmlReader.Create(filename);
            bool v = true;
            bool v_goodfile = false;
            string v_name = string.Empty;
            int v_count = 0;
            Colorf[] v_color = null;
            while (v && xreader.Read())
            {
                switch (xreader.NodeType)
                {
                    case XmlNodeType.Element:
                        switch (xreader.Name.ToLower())
                        {
                            case "gkds_palette":
                                try
                                {
                                    v_name = xreader.GetAttribute("Name");
                                    v_count = int.Parse(xreader.GetAttribute("Count"));
                                    v_goodfile = true;
                                }
                                catch
                                {
                                    throw new CoreException(enuExceptionType.FileNotOnAValidFormat, filename);
                                }
                                v_color = new Colorf[v_count];
                                break;
                            case "color":
                                //readcolor
                                v_color = ReadColor(xreader, v_count);
                                break;
                        }
                        break;
                }
            }
            xreader.Close();
            if (v_goodfile)
            {
                pal = new CoreColorPalette(v_name, v_color);
            }
            return pal;
        }
        private static Colorf[] ReadColor(XmlReader xreader, int count)
        {
            Colorf[] cl = new Colorf[count];
             int i = 0;
            while (xreader.Read())
            {
                switch (xreader.NodeType)
                {
                    case XmlNodeType.Element:
                        switch (xreader.Name.ToLower())
                        {
                            case "item":
                                if (i < count)
                                {
                                    Colorf d = Colorf.Convert (xreader.ReadElementContentAsString());
                                    cl[i] = d;
                                    i++;
                                }
                                break;
                        }
                        break;
                }
            }
            return cl;
        }
    }
}

