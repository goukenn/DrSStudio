

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AddFileResources.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV - [2008 - 2014]
author: C.A.D . BONDJE DOUE
file:AddFileResources.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.Actions
{
    public abstract class ProjectInfoActionBase : CoreActionBase, ICoreProjectAction
    {
        private bool m_visible;
        private bool m_enabled;
        #region ICoreProjectAction Members
        public bool Visible
        {
            get
            {
                return m_visible;
            }
            set
            {
                m_visible = value;
            }
        }
        public bool Enabled
        {
            get
            {
                return m_enabled;
            }
            set
            {
                this.m_enabled = value;
            }
        }
        #endregion
        #region ICoreProjectAction Members
        IGK.DrSStudio.Codec.ICoreProject m_ProjectInfo;
        public IGK.DrSStudio.Codec.ICoreProject ProjectInfo
        {
            get
            {
                return m_ProjectInfo;
            }
            set
            {
                m_ProjectInfo = value;
            }
        }
        #endregion
        public override string Id
        {
            get { return string.Format ("SolutionExplorerAction.{0}", this.GetType ().Name ); }
        }
    }
    class AddFileResources : ProjectInfoActionBase
    {
        protected override bool PerformAction()
        {
            System.Windows.Forms.MessageBox.Show("File resources removed");
            throw new NotImplementedException();
        }
    }
}

