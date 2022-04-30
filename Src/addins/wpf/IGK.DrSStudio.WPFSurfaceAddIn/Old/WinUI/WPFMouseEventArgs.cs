

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WPFMouseEventArgs.cs
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
file:WPFMouseEventArgs.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.WPFSurfaceAddIn.WinUI
{
    public delegate void WPFMouseButtonEventHandler (object sender, WPFMouseButtonEventArgs e);
    public delegate void WPFMouseEventHandler(object sender, WPFMouseEventArgs e);
    public class WPFMouseEventArgs : System.Windows.Input.MouseEventArgs
    {
        private Vector2d m_Location;
        /// <summary>
        /// get the location
        /// </summary>
        public Vector2d Location
        {
            get { return m_Location; }
        }
        public WPFMouseEventArgs(
            System.Windows.IInputElement target,
            System.Windows.Input.MouseEventArgs e)
            : base(
                e.MouseDevice, e.Timestamp, e.StylusDevice)
        {
            System.Windows.Point pt = e.GetPosition(target);
            m_Location = new Vector2d(
                pt.X,
                pt.Y);
        }
    }
    public class WPFMouseButtonEventArgs : System.Windows.Input.MouseButtonEventArgs
    {
        private Vector2d m_Location;
        /// <summary>
        /// get the location
        /// </summary>
public Vector2d Location{
get{return m_Location;}
}
int m_clickCount;
public new int ClickCount {
    get { return m_clickCount; }
}
        public WPFMouseButtonEventArgs(
            System.Windows.IInputElement target,
            System.Windows.Input.MouseButtonEventArgs e)        :base(
            e.MouseDevice, e.Timestamp, e.ChangedButton , e.StylusDevice )
        {
            int n= e.ClickCount;
            this.m_clickCount = n;
            System.Windows.Point pt = e.GetPosition(target);
            m_Location = new Vector2d(pt.X, pt.Y);
        }
    }
}

