

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreSaveAsInfo.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:CoreSaveAsInfo.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.ICore.WinUI
{
    public class CoreSaveAsInfo : ICoreSaveAsInfo 
    {
        private string m_Title;
        private string m_Filter;
        private string m_FileName;
        public string FileName
        {
            get { return m_FileName; }
        }
        public string Filter
        {
            get { return m_Filter; }
        }
        public string Title
        {
            get { return m_Title; }
        }
        public CoreSaveAsInfo(string title, string filter, string filename)
        {
            this.m_Title = title;
            this.m_Filter = filter;
            this.m_FileName = filename;
        }
    }
}

