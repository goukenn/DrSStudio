using IGK.DRSStudio.BalafonDesigner.WinUI;
using IGK.ICore;
using System;

namespace IGK.DRSStudio.BalafonDesigner.Codec
{
    /// <summary>
    /// the document for view designer document
    /// </summary>
    public class BalafonViewDesignerDocument : BalafonViewDesignerObjectBase, ICoreWorkingDocument
    {
        public string SolutionDir { get; internal set; }

        private string m_name;

        public string Name
        {
            get { return m_name; }
            set
            {
                if (m_name != value)
                {
                    m_name = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }

        ///<summary>
        ///public .ctr
        ///</summary>
        public BalafonViewDesignerDocument()
        {

        }

        public virtual Type DefaultSurfaceType
        {
            get
            {
                return typeof(BalafonViewDesignerSurface);
            }
        }
    }
}