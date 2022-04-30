using IGK.ICore;
using IGK.ICore.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.AudioVideo
{
    public class AVISequenceFormats : IAVISequenceVideoFormat, ICoreXmlGetValueMethod, ICoreWorkingDefinitionObject
    {
        public static readonly AVISequenceFormats HD_NTSC_720x480x30;
        public static readonly AVISequenceFormats HD_NTSC_1280x720x30;
        private int m_Height;
        private int m_Width;
        private int m_FramePerSec;
 

        static AVISequenceFormats() {
            HD_NTSC_1280x720x30 = new AVISequenceFormats(1280, 720, 30);
        }
        private AVISequenceFormats()
        {
        }

        public AVISequenceFormats(int width, int height, int framepersec):this()
        {
            this.m_Width = width;
            this.m_Height = height ;
            this.m_FramePerSec  = framepersec;
        }

        public int FramePerSec
        {
            get { return this.m_FramePerSec; }
        }

        public int Width
        {
            get { return this.m_Width; }
        }

        public int Height
        {
            get { return this.m_Height; }
        }
        public override string ToString()
        {
            return this.GetValue();
        }
        public string GetValue()
        {
            return string.Format("{0}x{1}x{2}", this.Width, this.Height, this.FramePerSec);
        }

        public string GetDefinition()
        {
            return GetValue();
        }

        public void CopyDefinition(string str)
        {
            var t = str.Split (new string[]{"x"},  StringSplitOptions.RemoveEmptyEntries );
            if (t.Length == 3)
            {
                this.m_Width = int.Parse(t[0]);
                this.m_Height = int.Parse(t[1]);
                this.m_FramePerSec  = int.Parse(t[2]);
            }
        }

        public string Id
        {
            get { throw new NotImplementedException(); }
        }
    }
}
