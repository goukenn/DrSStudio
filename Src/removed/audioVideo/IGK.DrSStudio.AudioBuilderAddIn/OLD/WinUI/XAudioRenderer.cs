

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XAudioRenderer.cs
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
file:XAudioRenderer.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
namespace IGK.DrSStudio.AudioBuilder.WinUI
{
    using IGK.ICore;using IGK.DrSStudio.WinUI;
    public static class XAudioRenderer
    {
        public static Colorf AudioTimeLineBackgroundColor { get { return CoreRenderer.GetColor("AudioTimeLineBackgroundColor", Colorf.CornflowerBlue); } }
        public static Colorf AudioExplorerStartColor { get { return CoreRenderer.GetColor("AudioExplorerStartColor", Colorf.FromFloat (0.8f,0.95f,1.0f)); } }
        public static Colorf AudioExplorerEndColor { get { return CoreRenderer.GetColor("AudioExplorerEndColor", Colorf.FromFloat (0.4f,0.7f,1.0f)); } }
        public static Colorf AudioItemStartColor { get { return CoreRenderer.GetColor("AudioItemStartColor", Colorf.FromFloat (0.7f)); } }
        public static Colorf AudioItemEndColor { get { return CoreRenderer.GetColor("AudioItemEndColor", Colorf.FromFloat (0.6f)); } }
        internal static void FillRectangle(Graphics graphics, Rectangle rectangle, Colorf startC, Colorf endC, float angle)
        {
            using (LinearGradientBrush ln = new LinearGradientBrush(
                rectangle, startC, endC, angle))
            {
                graphics.FillRectangle(ln, rectangle);
            }
        }
    }
}

