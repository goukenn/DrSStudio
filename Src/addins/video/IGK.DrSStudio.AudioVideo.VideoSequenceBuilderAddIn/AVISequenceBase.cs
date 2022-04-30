using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.DrSStudio.AudioVideo
{

    /// <summary>
    /// represent a sequence base class
    /// </summary>
    public abstract class AVISequenceBase : IAVISequence 
    {
        private IAVISequenceProject m_Project;

        public IAVISequenceProject Project
        {
            get { return m_Project; }
            internal set
            {
                if (m_Project != value)
                {
                    m_Project = value;
                }
            }
        }
        public AVISequenceBase()
        {
        }

        public abstract TimeSpan Duration
        {
            get;
        }

        public virtual void InitSequence()
        {
        }
        public virtual void Update(IAVISequenceUpdateInfo update)
        {
        }
        public virtual void Render(ICore.GraphicModels.ICoreGraphics v_device)
        {
        }
    }
}
