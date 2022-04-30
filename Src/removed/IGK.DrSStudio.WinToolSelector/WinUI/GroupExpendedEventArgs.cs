

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GroupExpendedEventArgs.cs
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
file:GroupExpendedEventArgs.cs
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
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.WinUI
{
    public delegate void GroupExpandedChangedEventHandler(object sender, GroupExpandedChangedEventArgs e);
    /// <summary>
    /// represent group expanded event args
    /// </summary>
    public class GroupExpandedChangedEventArgs : EventArgs
    {
        IWorkingItemGroup m_group;
        public IWorkingItemGroup Group
        {
            get { return m_group; }
        }
        public int Index
        {
            get
            {
                return m_group.Owner.Groups.IndexOf(m_group);
            }
        }
        public GroupExpandedChangedEventArgs(IWorkingItemGroup group)
        {
            if (group == null)
                throw new CoreException(enuExceptionType.ArgumentIsNull, "group");
            this.m_group = group;
        }
    }
}

