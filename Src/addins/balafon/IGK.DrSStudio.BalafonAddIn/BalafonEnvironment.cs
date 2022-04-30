using System;
using IGK.DrSStudio.Balafon.Xml;
using IGK.ICore.WinUI;

namespace IGK.DrSStudio.Balafon
{
    public class BalafonEnvironment :       
        ICoreWorkbenchEnvironment
    {
        private BalafonProject m_Project;
        private ICoreSystemWorkbench m_workbench;

        public BalafonProject Project { get { return m_Project; } }
        /// <summary>
        /// .ctr
        /// </summary>
        private BalafonEnvironment() {
        }

        public ICoreSystemWorkbench Workbench => m_workbench;

        internal static ICoreWorkbenchEnvironment Create(BalafonProject project,
            ICoreSystemWorkbench bench)
        {
            var g = new BalafonEnvironment
            {
                m_Project = project,
                m_workbench = bench
            };
            return g;
        }

        public bool OpenFile(string filename)
        {
            return false;
        }
    }
}