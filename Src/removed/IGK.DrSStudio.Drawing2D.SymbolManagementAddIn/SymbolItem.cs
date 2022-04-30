

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: SymbolItem.cs
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
file:SymbolItem.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.Drawing2D.SymbolManagementAddIn
{
    class SymbolItem
    {
        //ICore2DDrawingLayeredElement m_element;
        private string m_Name;
        private string m_Location;
        /// <summary>
        /// get the location of the symbol
        /// </summary>
        public string Location
        {
            get { return m_Location; }
            set
            {
                if (m_Location != value)
                {
                    m_Location = value;
                }
            }
        }
        /// <summary>
        /// get the name
        /// </summary>
        public string Name
        {
            get { return m_Name; }
            internal set
            {
                if (m_Name != value)
                {
                    m_Name = value;
                }
            }
        }
    }
}

