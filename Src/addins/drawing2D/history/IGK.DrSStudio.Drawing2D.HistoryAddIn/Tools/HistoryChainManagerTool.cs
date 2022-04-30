

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: HistoryChainManagerTool.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
using IGK.ICore;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.History;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.DrSStudio;
using IGK.DrSStudio.Menu;
using IGK.DrSStudio.WinUI;
using IGK.DrSStudio.Drawing2D;
using IGK.DrSStudio.Drawing2D.WinUI;
﻿using IGK.ICore.Drawing2D.Tools;
using IGK.ICore.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Drawing2D.Tools
{

    /// <summary>
    /// represent the tool that manage history chain
    /// </summary>
    [CoreTools("Tool.Drawing2D.HistoryChainManager")]
    public class HistoryChainManagerTool : Core2DDrawingToolBase, IHistoryChain
    {
        private static HistoryChainManagerTool sm_instance;
        private HistoryChainCollection m_chains;

        private HistoryChainManagerTool()
        {
            this.m_chains = new HistoryChainCollection(this);
        }

        public static HistoryChainManagerTool Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static HistoryChainManagerTool()
        {
            sm_instance = new HistoryChainManagerTool();
        }
        protected override void GenerateHostedControl()
        {
            this.HostedControl = null;
        }
        public override bool CanShow
        {
            get
            {
                return false;
            }
        }

        public IHistoryChainCollection Chains
        {
            get { return this.m_chains; }
        }

        /// <summary>
        /// represent a chain collection
        /// </summary>
        class HistoryChainCollection : IHistoryChainCollection 
        {
            private HistoryChainManagerTool m_owner;
            private Queue<IHistoryChainItem> m_items;



            public HistoryChainCollection(HistoryChainManagerTool historyChainManagerTool)
            {                
                this.m_owner = historyChainManagerTool;
                this.m_items = new Queue<IHistoryChainItem>();
            }


            public int Count
            {
                get { return this.m_items.Count; }
            }
            /// <summary>
            /// peek the last item in the selection history
            /// </summary>
            public IHistoryChainItem SelectedHistory => this.m_items.Peek();

            public void Enqueue(IHistoryChainItem item)
            {
                if (!this.m_items.Contains(item))
                {
                    this.m_items.Enqueue(item);
                }
            }
            /// <summary>
            /// get enumerator of the chain queue list
            /// </summary>
            /// <returns></returns>
            public System.Collections.IEnumerator GetEnumerator()
            {
                return this.m_items.GetEnumerator();
            }


            public bool Enabled(Type type)
            {
                foreach (IHistoryChainItem item in this.m_items)
                {
                    if (item.GetType() == type)
                        return item.Enabled;
                }
                return false;
            }
        }
    }
}
