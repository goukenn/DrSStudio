

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WebHtmlRadioButton.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/

using IGK.ICore;
using IGK.ICore.Codec;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.DrSStudio;
using IGK.DrSStudio.Menu;
using IGK.DrSStudio.WinUI;
using IGK.DrSStudio.Drawing2D;
using IGK.DrSStudio.Drawing2D.WinUI;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:WebHtmlRadioButton.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D ;
namespace IGK.DrSStudio.WebProjectAddIn.WorkingObjects
{
    using IGK.ICore.Xml;
    [HtmlWebElement("RadioButton", typeof(Mecanism))]
    public class WebHtmlRadioButton : HtmlInput 
    {
        private bool m_Checked;
        public bool Checked
        {
            get { return m_Checked; 
            }
            set {
                m_Checked = value;
                this.Node ["checked"] = this.m_Checked.ToString().ToLower();
            }
        }
        new class Mecanism : HtmlInput.Mecanism
        {
        }
        //public override void Draw(Graphics g)
        //{
        //   //base.Draw(g);
        //    GraphicsState vstate = g.Save();
        //    this.SetGraphicsProperty(g);
        //    ButtonState v_btnstate = this.m_Checked ? ButtonState.Checked : ButtonState.Normal;
        //    if (!this.Bound.IsEmpty)
        //    {
        //        WebRenderingEventArgs e = new WebRenderingEventArgs(g, this.GetRealBound());
        //        //HtmlRenderer.RenderBackground(this, e);
        //        //HtmlRenderer.RenderBorder(this, e);        
        //        ControlPaint.DrawRadioButton(g, e.Rectangle , v_btnstate);
        //    }
        //    g.Restore(vstate);
        //}
        protected override CoreXmlElementBase CreateNode()
        {
            CoreXmlElementBase node = base.CreateNode();
            node["type"] = "radio";
            node["checked"] = this.m_Checked.ToString ().ToLower();
            return node;
        }
    }
}

