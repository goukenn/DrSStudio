using IGK.ICore;
using IGK.ICore.Codec;
using IGK.ICore.IO;
using IGK.ICore.Xml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.AudioVideo.Xml
{
    [CoreWorkingObject(AVISequenceConstant.PROJECT_TAG)]
    /// <summary>
    /// reprensent a avi sequence project
    /// </summary>
    public class AVISequenceProject : AVISequenceItemBase, IAVISequenceProject 
    {
        /// <summary>
        /// represent a project sequence collections
        /// </summary>
        class SequenceCollection : IAVISequenceCollections , ICoreSerializable 
        {
            private AVISequenceProject m_project;
            private List<IAVISequence> m_sequences;
            public SequenceCollection(AVISequenceProject project)
            {
                this.m_project = project;
                this.m_sequences = new List<IAVISequence> ();
            }

            public System.Collections.IEnumerator GetEnumerator()
            {
                return this.m_sequences.GetEnumerator();
            }
            public IAVISequence GetNext(IAVISequence seq)
            {
                int i = this.m_sequences.IndexOf(seq);
                if (i != -1)
                {
                    i++;
                    if (this.m_sequences.Count > i)
                    {
                        return this.m_sequences[i];
                    }
                }
                return null;
            }

            public int Count
            {
                get { return this.m_sequences.Count; }
            }
            public override string ToString()
            {
                return string.Format("{0}[Count:{1}]", this.GetType().Name, this.Count);
            }


            public void Add(IAVISequence sequence)
            {
               if( (sequence !=null) && (this.m_sequences.Contains (sequence )==false ))
                {                   
                    this.m_sequences.Add(sequence);
                    if (sequence is AVISequenceBase)
                        (sequence as AVISequenceBase).Project = this.m_project;
                    this.m_project.OnSequenceAdded(new CoreItemEventArgs<IAVISequence>(sequence));
                }
            }

            public void Remove(IAVISequence sequence)
            {
                if ((sequence != null) && (this.m_sequences.Contains(sequence)))
                {
                    this.m_sequences.Remove(sequence);
                    this.m_project.OnSequenceRemoved(new CoreItemEventArgs<IAVISequence>(sequence));
                }
            }

            public IAVISequence this[int index]
            {
                get {
                    return this.m_sequences.IndexExists (index)? this.m_sequences[index]: null ;
                }
            }

            void ICoreSerializable.Serialize(IXMLSerializer seri)
            {
                foreach (ICoreWorkingObject  item in this.m_project)
                {
                    CoreXMLSerializerUtility.WriteElements(item, seri);
                }
            }
        }

        private SequenceCollection m_sequences;
        private string m_Name;
        private string m_OutDir;
        private int m_FramePerSec;
        private IAVISequenceVideoFormat m_OutputFormat;
        private enuAviSequenceSplitJoinMethod m_JoinMethod;
        private int m_BufferFileSize;
        /// <summary>
        /// get or set the buffer file size . size in MB
        /// </summary>
        public int BufferFileSize
        {
            get { return m_BufferFileSize; }
            set
            {
                if (m_BufferFileSize != value)
                {
                    m_BufferFileSize = value;
                }
            }
        }
        /// <summary>
        /// get or set the sequence join method
        /// </summary>
        public enuAviSequenceSplitJoinMethod JoinMethod
        {
            get { return m_JoinMethod; }
            set
            {
                if (m_JoinMethod != value)
                {
                    m_JoinMethod = value;
                }
            }
        }
      

        public IAVISequenceVideoFormat OutputFormat
        {
            get { return m_OutputFormat; }
            set {
                this.m_OutputFormat = value;
            }
        }
        public string OutDir
        {
            get { return m_OutDir; }
            set
            {
                if (m_OutDir != value)
                {
                    m_OutDir = value;
                }
            }
        }
        public string Name
        {
            get { return m_Name; }
            set { this.m_Name = value; }
        }

        public AVISequenceProject():base(AVISequenceConstant.PROJECT_TAG)
        {
            this["xmlns:igkdevvids"] = AVISequenceConstant.NAMESPACE;
            this.m_sequences = new SequenceCollection(this);
            this.BufferFileSize =2000;
            this.JoinMethod = enuAviSequenceSplitJoinMethod.AppendStream;
        }

        public override bool CanAddChild
        {
            get
            {
                return base.CanAddChild;
            }
        }        
        public bool SaveProject(string directory)
        {
            if (PathUtils.CreateDir(directory))
            {
                File.WriteAllText(Path.Combine(directory, this.Name + AVISequenceConstant.PROJECTEXTENSION),
                    this.RenderXML(new CoreXmlSettingOptions()
                    {
                         Indent = true 
                    }));
            }
            return false;
        }

        public static AVISequenceProject CreateFromFile(string filename)
        {
            if (!File.Exists(filename))
                return null;

            CoreXmlElement c = CoreXmlElement.LoadFile(filename);
            if (c != null)
            { 
              if (c.TagName ==  AVISequenceConstant.PROJECT_TAG)
              {
                  AVISequenceProject p = new AVISequenceProject ();
                  p.CopyAttributes(c);
                  p.m_Name = Path.GetFileNameWithoutExtension(filename);
                  p.m_OutDir = Path.GetDirectoryName(filename);
                  p.LoadString (c.RenderInnerHTML(null));
                  return p;
              }
            }
            return null;
        }

        /// <summary>
        /// save the current project
        /// </summary>
        public void Save()
        {
            File.WriteAllText(Path.Combine(this.OutDir, 
                this.Name + AVISequenceConstant.PROJECTEXTENSION),
                 this.RenderXML(new CoreXmlSettingOptions()
                 {
                     Indent = true
                 }));
        }


      


        public int FramePerSec
        {
            get
            {
                return this.m_FramePerSec;
            }
            set
            {
                this.m_FramePerSec = value;
            }
        }

        public double GetTotalSeconds()
        {
            TimeSpan t = TimeSpan.Zero;

            foreach (IAVISequence item in this.Sequences)
            {
                t = t.Add(item.Duration);
            }
            return t.TotalSeconds;
        }


        public IAVISequenceCollections Sequences
        {
            get { return this.m_sequences; }
        }


        public event EventHandler<CoreItemEventArgs<IAVISequence>> SequenceAdded;

        public event EventHandler<CoreItemEventArgs<IAVISequence>> SequenceRemoved;

        internal void OnSequenceAdded(CoreItemEventArgs<IAVISequence> e)
        {
            if (SequenceAdded!=null)
            {
                this.SequenceAdded(this, e);
            }
        }

        public virtual  void OnSequenceRemoved(CoreItemEventArgs<IAVISequence> e)
        {
            if (SequenceRemoved != null)
            {
                this.SequenceRemoved(this, e);
            }
        }

        public string OutFolder
        {
            get
            {
                return Path.Combine(this.m_OutDir, "out");
            }
        }
        public string RawFolder
        {
            get
            {
                return Path.Combine(this.m_OutDir, "Raw");
            }
        }

        public string ResFolder
        {
            get
            {
                return Path.Combine(this.m_OutDir, "Res");
            }
        }

        public string TextFolder
        {
            get
            {
                return Path.Combine(this.m_OutDir, "Text");
            }
        }
    }
}
