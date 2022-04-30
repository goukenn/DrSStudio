using IGK.ICore.GraphicModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace IGK.DrSStudio.AudioVideo.FX
{
    public abstract class AVISequenceFXBase : IAVISequenceFX 
    {
        private TimeSpan m_From;
        private TimeSpan m_Duration;
        private bool m_isActive;
        private AVISequenceChain m_chain;
        /// <summary>
        /// get the chain where this AVISequence is attached to
        /// </summary>
        public AVISequenceChain Chain { get { return this.m_chain; } internal set { this.m_chain = value; } }
        public bool Infinite { get; set; }

        public TimeSpan Duration
        {
            get { return m_Duration; }
            set
            {
                if (m_Duration != value)
                {
                    m_Duration = value;
                }
            }
        }
        public TimeSpan From
        {
            get { return m_From; }
            set
            {
                if (m_From != value)
                {
                    m_From = value;
                }
            }
        }
        public virtual string Name
        {
            get { return GetType().Name.Replace(AVISequenceConstant.FXPREFIXNAME, string.Empty ); }
        }
        public virtual bool IsActive { get {
            return this.m_isActive;
            }
            protected set {
                this.m_isActive = value;
            }
        }
     

        public virtual void Update(IAVISequenceUpdateInfo update)
        {
            this.IsActive = ((this.m_From.CompareTo (this.Duration) ==0) && (this.Duration.CompareTo (TimeSpan.Zero)==0) && Infinite)
                || IsInInterval(update.TotalTimeSpan);
        }
        protected bool IsInInterval(TimeSpan timeSpan)
        {
            TimeSpan f = this.From;
            TimeSpan c = this.From + this.Duration;
            double m = timeSpan.TotalMilliseconds;
            bool r = (m>=f.TotalMilliseconds ) &&
                 (m <=c.TotalMilliseconds);
            return r;
        }
        public virtual void Render(ICoreGraphics graphics)
        {
        }

        public static AVISequenceFXBase CreateFX(string fxName)
        {
            Type t = MethodInfo.GetCurrentMethod ().DeclaringType ;
            Type g = Type.GetType (t.Namespace + "."+fxName , false, true);
            if (g!=null){
                return g.Assembly.CreateInstance (g.FullName ) as AVISequenceFXBase ;
            }
            return null;
        }
    }
}
