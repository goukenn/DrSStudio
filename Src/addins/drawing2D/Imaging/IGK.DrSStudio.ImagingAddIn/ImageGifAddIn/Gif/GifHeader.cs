

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GifHeader.cs
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
using IGK.DrSStudio.Drawing2D;
using IGK.DrSStudio.Drawing2D.WinUI;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:GifHeader.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.GifAddIn.Gif
{
    class GifHeader : GifDataEntityBase, ICoreDataChainHeader
    {
        private string m_Version;
        private string m_Type;
        public GifHeader()
        {
            this.m_Version = "87a";
            this.m_Type = "GIF";
        }
        public string Type
        {
            get { return m_Type; }
            set
            {
                if (m_Type != value)
                {
                    m_Type = value;
                }
            }
        }
        public string Version
        {
            get { return m_Version; }
            set
            {
                if (m_Version != value)
                {
                    m_Version = value;
                }
            }
        }
        public override string Name
        {
            get
            {
                return GifConstant.GIF_HEADER;
            }
        }
        public override string Render()
        {
            return string.Format("{0}: [Type :{1} ; Version: {2}]", this.Name, this.Type, this.Version);
        }
        public override void Read(System.IO.Stream stream)
        {
            byte[] data = new byte[3];
            stream.Read(data, 0, data.Length);
            this.m_Type = ConvertToString(data);
            stream.Read(data, 0, data.Length);
            this.m_Version  = ConvertToString(data);
        }
        private string ConvertToString(byte[] data)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sb.Append((char)data[i]);
            }
            return sb.ToString();
        }
        public override void Write(System.IO.Stream stream)
        {
            System.IO.BinaryWriter binW = new System.IO.BinaryWriter(stream);
            binW.Write(this.Type.ToCharArray());
            binW.Write(this.Version.ToCharArray());
            binW.Flush();
        }
        public bool IsValid
        {
            get { return (this.Type == "GIF"); }
        }
        public override void Copy(ICoreDataEntity dataEntity)
        {
            GifHeader h = dataEntity as GifHeader;
            if (h == null)
                return;
            this.m_Version = h.Version;
            this.m_Type = h.m_Type;
        }
    }
}

