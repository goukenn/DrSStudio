

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreScrollBar.cs
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
file:ICoreScrollBar.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.ICore.WinUI
{
    /// <summary>
    /// represent a scrollbar
    /// </summary>
    public interface ICoreScrollBar
    {
        /// <summary>
        /// get or set the bound of this scrollbar
        /// </summary>
        Rectanglei Bounds { get; set; }
        /// <summary>
        /// get or set the visibility of the scrollbar
        /// </summary>
        bool Visible { get; set; }
        /// <summary>
        /// get or set the value
        /// </summary>
        int Value { get; set; }
        /// <summary>
        /// get or set the minimum value
        /// </summary>
        int Minimum { get; set; }
        /// <summary>
        /// get or set the maximum value
        /// </summary>
        int Maximum { get; set; }
        /// <summary>
        /// setup the scroll visibility
        /// </summary>
        /// <param name="minimum"></param>
        /// <param name="maximum"></param>
        /// <param name="value"></param>
        void SetupScrollValue(int minimum, int maximum, int value);
        /// <summary>
        /// value changed
        /// </summary>
        event EventHandler<CoreScrollEventArgs> Scroll;
    }
}

