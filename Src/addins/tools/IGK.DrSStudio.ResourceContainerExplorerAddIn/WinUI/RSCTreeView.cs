using IGK.ICore.WinCore;
using IGK.ICore;using IGK.ICore.WorkingObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGK.DrSStudio.WinUI
{
    class RSCTreeView : TreeView
    {
        private ResourceElement m_ResouceElement;

        public ResourceElement ResouceElement
        {
            get { return m_ResouceElement; }
            set
            {
                if (m_ResouceElement != value)
                {
                    m_ResouceElement = value;
                    OnResourceElementChanged(EventArgs.Empty);
                }
            }
        }
        public event EventHandler ResourceElementChanged;
        /// <summary>
        /// resource element changed
        /// </summary>
        /// <param name="eventArgs"></param>
        private void OnResourceElementChanged(EventArgs eventArgs)
        {
            if (this.ResourceElementChanged != null)
                this.ResourceElementChanged(this, eventArgs);
        }
        public RSCTreeView()
        {
        }
    }
}
