using IGK.DrSStudio.AudioVideo.FX;
using IGK.ICore.GraphicModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.DrSStudio.AudioVideo
{
    /// <summary>
    /// represent a chain sequence
    /// </summary>
    public class AVISequenceChain : IAVISequenceChain
    {
        private IAVISequence  m_CurrentSequence;
        private AVISequenceFXCollections m_fx;
        private AVISequenceBuilder m_aVISequenceBuilder;
        private AVISequenceOverLayCollections m_overLay;

        public AVISequenceOverLayCollections OverLay
        {
            get {
                return this.m_overLay;
            }
        }
        public class AVISequenceOverLayCollections : IAVISequenceUpdateItem, IAVISequenceRenderItem
        {
            private AVISequenceChain aVISequenceChain;

            public AVISequenceOverLayCollections(AVISequenceChain aVISequenceChain)
            { 
                this.aVISequenceChain = aVISequenceChain;
            }

            public void Update(IAVISequenceUpdateInfo update)
            {
                
            }

            public void Render(ICoreGraphics graphics)
            {
                
            }
        }
        /// <summary>
        /// get the fx collections
        /// </summary>
        public AVISequenceFXCollections FX {
            get {
                return this.m_fx;
            }
        }
        public AVISequenceChain()
        {
            this.m_fx = new AVISequenceFXCollections(this);
            this.m_overLay = new AVISequenceOverLayCollections(this);
        }

        public AVISequenceChain(AVISequenceBuilder aVISequenceBuilder):this()
        {
            this.m_aVISequenceBuilder = aVISequenceBuilder;
        }
        public class AVISequenceFXCollections
        {
            private List<IAVISequenceFX> m_fxs;
            private AVISequenceChain m_chain;
            public AVISequenceFXCollections(AVISequenceChain chain)
            {
                this.m_chain = chain;
                this.m_fxs = new List<IAVISequenceFX>();
            }
            public void Add(IAVISequenceFX fx)
            {
                if ((fx == null) || (this.m_fxs.Contains(fx)))
                    return;
                this.m_fxs.Add(fx);
                if (fx is AVISequenceFXBase)
                (fx as AVISequenceFXBase).Chain = this.m_chain;
            }
            public void Remove(IAVISequenceFX fx)
            {
                if ((fx == null)|| (this.m_fxs.Contains(fx)))
                    return;
                this.m_fxs.Remove(fx);
                if (fx is AVISequenceFXBase)
                (fx as AVISequenceFXBase).Chain = null;
            }
            public void Update(IAVISequenceUpdateInfo update)
            {
                foreach (var item in this.m_fxs)
                {
                    item.Update(update);
                }
            }
            public void Render(ICoreGraphics graphics)
            {
                foreach (var item in this.m_fxs)
                {
                    if (item.IsActive)
                    {
                        item.Render(graphics);
                    }
                }
            }
        }
        /// <summary>
        /// get or set the current sequence
        /// </summary>
        public IAVISequence  CurrentSequence
        {
            get { return m_CurrentSequence; }
            set
            {
                if (m_CurrentSequence != value)
                {
                    m_CurrentSequence = value;
                }
            }
        }

        public void Update(IAVISequenceUpdateInfo updateInfo)
        {
            if (this.m_CurrentSequence != null)
            {
                this.m_CurrentSequence.Update(updateInfo);
            }
            this.m_fx.Update(updateInfo);
            this.m_overLay.Update(updateInfo);
        }
        public void Render(ICoreGraphics graphics)
        {
            if (this.m_CurrentSequence != null)
            {
                this.m_CurrentSequence.Render(graphics);
            }
            this.m_fx.Render(graphics);
            this.m_overLay.Render(graphics);
        }

        public int DocumentWidth { get { return this.m_aVISequenceBuilder.Width; } }

        public int DocumentHeight { get { return this.m_aVISequenceBuilder.Height; } }
    }
}
