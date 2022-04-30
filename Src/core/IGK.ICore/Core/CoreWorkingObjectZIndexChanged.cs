

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreWorkingObjectZIndexChanged.cs
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
file:CoreWorkingObjectZIndexChanged.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore
{
    public delegate void CoreWorkingObjectZIndexChangedHandler (object o, CoreWorkingObjectZIndexChangedEventArgs e);
    
    
    /// <summary>
    /// represent event args for zindex modification
    /// </summary>
    public class CoreWorkingObjectZIndexChangedEventArgs : EventArgs 
    {
        
        private int m_PreviousIndex;
        private int m_CurrentIndex;
        private ICoreWorkingPositionableObject  m_Item;
        public ICoreWorkingPositionableObject  Item{
        get{return m_Item;}
        }
        public int CurrentIndex{
        get{return m_CurrentIndex;}
        }
        public int PreviousIndex{
        get{return m_PreviousIndex;}
        }
        public CoreWorkingObjectZIndexChangedEventArgs(ICoreWorkingPositionableObject item, int oldindex, int currentIndex){ 
            this.m_Item = item;
            this.m_PreviousIndex = oldindex ;
            this.m_CurrentIndex = currentIndex ;
        }
    }
}

