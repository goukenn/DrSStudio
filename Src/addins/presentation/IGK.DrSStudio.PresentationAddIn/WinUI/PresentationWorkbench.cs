using IGK.ICore;
using IGK.ICore.WinCore;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Presentation.WinUI
{
    /// <summary>
    /// represent a presentation workbench
    /// </summary>
    class PresentationWorkbench : CoreSystemWorbenchBase, ICoreSystemWorkbench
    {
        private PresentationForm presentationForm;

        /// <summary>
        /// .ctr
        /// </summary>
        internal PresentationWorkbench(){
        }

        public PresentationWorkbench(PresentationForm presentationForm)
        {
            this.presentationForm = presentationForm;
        }

        public override void Init(CoreSystem appInstance)
        {
            base.Init(appInstance);
        }

        Dictionary<ICoreMessageFilter, WinCoreMessageFilter> m_hostFilterMessage;
        public override void RegisterMessageFilter(ICoreMessageFilter messageFilter)
        {
            if (messageFilter == null) return;
            if (m_hostFilterMessage == null)
                m_hostFilterMessage = new Dictionary<ICoreMessageFilter, WinCoreMessageFilter>();
            if (!this.m_hostFilterMessage.ContainsKey(messageFilter))
            {
                WinCoreMessageFilter c = new WinCoreMessageFilter(messageFilter);
                m_hostFilterMessage.Add(messageFilter, c);
                //add message filter to application
                if (CoreApplicationManager.Application is ICoreMessageFilterApplication filter)
                    filter.AddMessageFilter(c);
            }
        }
        public override void UnregisterMessageFilter(ICoreMessageFilter messageFilter)
        {
            if (this.m_hostFilterMessage.ContainsKey(messageFilter))
            {
                WinCoreMessageFilter c = this.m_hostFilterMessage[messageFilter];

                if (CoreApplicationManager.Application is ICoreMessageFilterApplication filter)
                    filter.RemoveMessageFilter(c);

                //Application.RemoveMessageFilter(c);
            }
        }
    }
}
