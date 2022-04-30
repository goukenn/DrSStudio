

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: MergerContextMenuItem.cs
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
file:MergerContextMenuItem.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace IGK.DrSStudio
{
    class MergerContextMenuItem
    {
        private static MergerContextMenuItem sm_instance;
        private MergerContextMenuItem()
        {
            this.m_contextMenuStrip = new ContextMenuStrip();
            this.Generate();
        }
        public static MergerContextMenuItem Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static MergerContextMenuItem()
        {
            sm_instance = new MergerContextMenuItem();
        }
        private ContextMenuStrip m_contextMenuStrip;
        public ContextMenuStrip ContextMenuStrip
        {
            get { return m_contextMenuStrip; }
        }
        void Generate()
        {
            this.Add(new Actions.ExportAudioAction());
            this.Add(new Actions.ExtractAudioAsDataAction());
        }
        private void Add(IGK.DrSStudio.Actions.MergeActionBase action)
        {
            this.ContextMenuStrip.Items.Add(action.ContextMenuItem);
        }
    }
}

