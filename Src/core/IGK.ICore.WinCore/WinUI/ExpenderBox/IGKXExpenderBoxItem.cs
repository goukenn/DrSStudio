

using IGK.ICore.WinCore.WinUI.Controls;
/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKXExpenderBoxItem.cs
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
using System.Windows.Forms;

namespace IGK.ICore.WinCore.WinUI
{
    public class IGKXExpenderBoxItem : IGKXControl 
    {
        private IGKXExpenderBoxGroup m_ParentGroup;
        private int m_Index;
        private string m_ImageKey;
        public override string ToString()
        {
            return "ExpenderBoxItem : " + Name;
        }
        public IGKXExpenderBoxItem()
        {
            this.m_Index = 0;
            this.Margin = new Padding(0);
            this.Padding = new Padding(0);
        }
        /// <summary>
        /// get or set the image key
        /// </summary>
        public string ImageKey
        {
            get { return m_ImageKey; }
            set
            {
                if (m_ImageKey != value)
                {
                    m_ImageKey = value;
                }
            }
        }
        protected override System.Drawing.Size DefaultSize
        {
            get
            {
                return new System.Drawing.Size(24, 100);
            }
        }
        /// <summary>
        /// get or set the required index
        /// </summary>
        public int Index
        {
            get { return m_Index; }
            set
            {
                if (m_Index != value)
                {
                    m_Index = value;
                }
            }
        }
        public IGKXExpenderBoxGroup ParentGroup
        {
            get { return m_ParentGroup; }
            internal set
            {
                if (m_ParentGroup != value)
                {
                    m_ParentGroup = value;
                }
            }
        }
    }
}
