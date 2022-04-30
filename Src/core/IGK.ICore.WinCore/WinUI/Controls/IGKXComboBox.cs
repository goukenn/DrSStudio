

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKXComboBox.cs
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
file:IGKXComboBox.cs
*/
/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igkdev.be
Application : DrSStudio
powered by IGK - DEV &copy; 2008-2012
THIS FILE IS A PART OF THE DRSSTUDIO APPLICATION. SEE "License.txt"
FOR MORE INFORMATION ABOUT THE LICENSE
------------------------------------------------------------------- 
*/
﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace IGK.ICore.WinCore.WinUI.Controls
{

    public class IGKXComboBox : ComboBox 
    { 
    }
    /// <summary>
    /// represent the combobox
    /// </summary>
    public class IGKXComboBox<T> : ComboBox where T: class  
    {
        private IXComboBoxMatchListener<T> m_MatchListener;        
        public IGKXComboBox()
        {
            
        }
        protected override void SetItemCore(int index, object value)
        {
            base.SetItemCore(index, value);
        }
        public void SetMatchListener(IXComboBoxMatchListener<T> c)
        {
            this.m_MatchListener = c;
        }

        public void setSelectedItem( object c)
        {
            if (this.m_MatchListener ==null)
                return ;
            if (c ==null){
                this.SelectedItem = null;
                return;
            }
            foreach (T item in this.Items)
            {
                if (this.m_MatchListener(item, c))
                {
                    this.SelectedItem = item;
                    return;
                }
            }
            this.SelectedItem = null;
        }
    }
}

