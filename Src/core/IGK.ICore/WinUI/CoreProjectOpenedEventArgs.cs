

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreProjectOpenedEventArgs.cs
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
file:CoreProjectOpenedEventArgs.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore.WinUI
{
    /// <summary>
    /// event handler
    /// </summary>
    /// <param name="o"></param>
    /// <param name="e"></param>
    public delegate void CoreProjectOpenedEventHandler(object o, CoreProjectOpenedEventArgs e);
    /// <summary>
    /// event args
    /// </summary>
    public class CoreProjectOpenedEventArgs : EventArgs 
    {
        private string m_Name;
        private string m_Path;
        public string Path
        {
            get { return m_Path; }        
        }
        public string Name
        {
            get { return m_Name; }            
        }
        public CoreProjectOpenedEventArgs(string name, string Path)
        {
            this.m_Name = name;
            this.m_Path = Path;
        }
    }
}

