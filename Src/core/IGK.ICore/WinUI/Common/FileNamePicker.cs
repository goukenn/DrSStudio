

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: FileNamePicker.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.WinUI.Common
{

    /// <summary>
    /// represent a file name picker dialog
    /// </summary>
    public abstract class FileNamePicker : IXCommonDialog
    {
        private string m_FileName;
        public string FileName
        {
            get { return m_FileName; }
            set
            {
                if (m_FileName != value)
                {
                    m_FileName = value;
                }
            }
        }
        /// <summary>
        /// get or set the title of this file name picker
        /// </summary>
        public abstract string Title
        {
            get;
            set;
        }
        /// <summary>
        /// get or set the filter to used
        /// </summary>
        public abstract string Filter { get; set; }

        public abstract enuDialogResult ShowDialog();

        public virtual void Dispose() { }
    }
}
