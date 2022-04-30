using IGK.ICore;using IGK.ICore.Codec;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IGK.ICore.WorkingObjects;

namespace IGK.ICore
{
    public abstract class GKDSElementItemBase : CoreWorkingObjectBase, ICoreSerializerService, ICoreGKDSElementItem
    {
        private GKDSElement m_Parent;
        /// <summary>
        /// get the parent of his 
        /// </summary>
        public GKDSElement Parent
        {
            get { return m_Parent; }
            internal set
            {
                if (m_Parent != value)
                {
                    m_Parent = value;
                }
            }
        }

        /// <summary>
        /// get the surface
        /// </summary>
        public ICoreWorkingSurface Surface {
            get {
                return this.m_Parent != null ? this.m_Parent.Surface : null;
            }
        }
    }
}
