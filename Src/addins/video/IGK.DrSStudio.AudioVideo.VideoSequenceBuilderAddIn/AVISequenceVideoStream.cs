using IGK.DrSStudio.AudioVideo.Transitions;
using IGK.ICore.Codec;
using IGK.ICore.Drawing2D;
using IGK.ICore.GraphicModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.AudioVideo
{
    using IGK.ICore;
    using IGK.ICore.WinCore ;

    /// <summary>
    /// represent a sequence video stream
    /// </summary>
    public class AVISequenceVideoStream : AVISequenceBase
    {
        private TimeSpan m_duration;
        private string m_BaseSCript;
        private string m_Document;
        private IAVISequenceTransition m_Transition;

        public AVISequenceVideoStream(TimeSpan span)
        {
            m_duration = span;
        }
        public override void Update(IAVISequenceUpdateInfo update)
        {
            base.Update(update);
        }
  
        public IAVISequenceTransition Transition
        {
            get { return m_Transition; }
            set
            {
                if (m_Transition != value)
                {
                    m_Transition = value;
                }
            }
        }
        public string Document
        {
            get { return m_Document; }
            set
            {
                if (m_Document != value)
                {
                    m_Document = value;
                }
            }
        }
        /// <summary>
        /// get or set the base script
        /// </summary>
        public string BaseSCript
        {
            get { return m_BaseSCript; }
            set
            {
                if (m_BaseSCript != value)
                {
                    m_BaseSCript = value;
                }
            }
        }
        /// <summary>
        /// get the duration of this video sequence
        /// </summary>
        public override TimeSpan Duration
        {
            get {
                return this.m_duration;
            }         
        }

        ICore2DDrawingDocument m_document;
        public override void Render(ICoreGraphics device)
        {
            m_document.Draw (device,
                new Rectanglei (
                    0,0,
                    this.Project.OutputFormat.Width,
                    this.Project.OutputFormat.Height 
                    ));
        }
        public override void InitSequence()
        {
            string src = GetSourceFile();
            this.m_document = File.Exists(src) ? 
                CoreDecoder.Instance.Open2DDocument(src)
                .CoreGetValue<ICore2DDrawingDocument>(0): 
                null;
                
        }

        private string GetSourceFile()
        {
            string g = this.Document;
            if (File.Exists(g))
                return g;
            g = Path.Combine(this.Project.ResFolder, Path.GetFileName(this.Document+".gkds"));
            return g;
                
        }
        
      
    }
}
