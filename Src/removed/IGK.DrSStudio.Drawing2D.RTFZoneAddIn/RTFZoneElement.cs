

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: RTFZoneElement.cs
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
file:RTFZoneElement.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D ;
using System.Windows.Forms ;
namespace IGK.DrSStudio.Drawing2D
{
    using IGK.ICore;using IGK.DrSStudio.WinUI;
    using IGK.DrSStudio.Drawing2D.WinUI;
    using IGK.DrSStudio.Drawing2D;
    using IGK.DrSStudio.Drawing2D.MecanismActions;
    [Core2DDrawingStandardElement("RTFZone", typeof(Mecanism))]
    public sealed class RTFZoneElement : RectangleElement  
    {
        XDummyRTF c_rtf;
        private Bitmap m_offbitmap;
        [IGK.DrSStudio.Codec .CoreXMLAttribute ()]
        public string RTF
        {
            get { return this.c_rtf .Rtf; }
            set
            {
                if (RTF != value)
                {
                    this.c_rtf.Rtf = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        public RTFZoneElement()
        {
            this.c_rtf = new XDummyRTF();
            this.c_rtf.Text = "RTF Text Zone";
        }
        public override void Dispose()
        {
            c_rtf.Dispose();
            this.DisposeBitmap();
            base.Dispose();
        }
        protected override void InitGraphicPath(CoreGraphicsPath p)
        {
            
            //GraphicsPath path = new GraphicsPath();
            p.Reset();
            Rectanglef rc = Rectanglef.Round (this.Bounds);
            p.AddRectangle(rc);
            ////free bitmap resources
            //this.DisposeBitmap();
            //c_rtf.Bounds = new Rectangle(0, 0, rc.Width, rc.Height);
            //this.m_offbitmap = XDummyRTF.RtbToBitmap(c_rtf);
            //this.SetPath(path);
        }
        private void DisposeBitmap()
        {
            if (m_offbitmap != null)
            {
                m_offbitmap.Dispose();
                m_offbitmap = null;
            }
        }
        //public override void Draw(Graphics g)
        //{
        //    base.Draw(g);
        //    if (m_offbitmap != null)
        //    {
        //        GraphicsState s = g.Save();
        //        g.MultiplyTransform(this.GetMatrix(), enuMatrixOrder.Prepend);
        //        //XDummyRTF.RtbDrawToGraphics(
        //        //    c_rtf ,
        //        //    g, Rectanglef .Round (this.Bound));
        //        g.DrawImage(m_offbitmap, this.Bound);
        //        g.Restore(s);
        //    }
        //}
        internal new class Mecanism : RectangleElement.Mecanism 
        {
            new RTFZoneElement Element {
                get {
                    return base.Element as RTFZoneElement;
                }
            }
            xDummyRTFEditForm  c_frm = null;
            private bool m_editText;
            void BeginEditText()
            {
                //RTFZoneElement l = this.Element ;
                //if (l==null || m_editText )
                //    return;
                //c_frm = new xDummyRTFEditForm(l.c_rtf );
                //if (this.CurrentSurface is Control)
                //c_frm.Owner = (this.CurrentSurface as Control).FindForm();                
                //c_frm.Size = l.c_rtf.Size;
                //Point v_ptr = Vector2f .Round ( this.CurrentSurface.GetScreenLocation(l.Bound.Location));
                //c_frm.Location = (this.CurrentSurface as Control).PointToScreen(v_ptr);
                ////l.c_rtf.KeyPress += new KeyPressEventHandler(c_rtf_KeyPress);
                //m_editText = true;
                //c_frm.ShowDialog ();
            }
            //void c_rtf_KeyPress(object sender, KeyPressEventArgs e)
            //{
            //    switch ((Keys)e.KeyChar)
            //    {
            //        case Keys.Escape :
            //            this.Actions[Keys.Escape].DoAction();
            //            e.Handled = true;
            //            break;
            //    }
            //}
            void EndEditText()
            { 
                RTFZoneElement l = this.Element ;
                if (l != null)
                {
                    c_frm.Controls.Remove(l.c_rtf);
                    l.InitElement();
                }
                c_frm.Close();
                this.m_editText = false;
            }
            protected override void GenerateActions()
            {
                base.GenerateActions();
                //replace the edit item
                this.Actions[enuKeys.Control | enuKeys.E] = new EditItemAction(this);
                this.Actions[enuKeys.Escape ] = new EndEditItemAction(this);
            }
            sealed class EditItemAction : Core2DDrawingMecanismActionBase
            {
                Mecanism mecanism;
                public EditItemAction(Mecanism mecanism):base()
                {
                    this.mecanism = mecanism;
                }
                protected override bool PerformAction()
                {
                    mecanism.BeginEditText();
                    return false;
                }
            }
            sealed class EndEditItemAction : Core2DDrawingMecanismActionBase
            {
                Mecanism mecanism;
                public EndEditItemAction(Mecanism mecanism)
                    : base()
                {
                    this.mecanism = mecanism;
                }
                protected override bool PerformAction()
                {
                    if (mecanism.m_editText)
                        this.mecanism.EndEditText();
                    else
                        this.mecanism.EndEdition();
                    return false;
                }
            }
        }
    }
}

