using IGK.ICore.WinCore;
using IGK.ICore;using IGK.DrSStudio.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IGK.ICore.Settings;
using IGK.ICore.Resources;
using IGK.ICore.WinCore.WinUI.Controls;

namespace IGK.DrSStudio.SkinEditorAddin.WinUI
{
    class UIXSkinEditorListBox : IGKXListBox
    {
        StringElement m_string;
        protected override void Dispose(bool disposing)
        {
            if (m_string != null)
            {
                m_string.Dispose();
                m_string = null;
            }
            base.Dispose(disposing);
        }
        public UIXSkinEditorListBox()
        {
            this.m_string = new StringElement();
            this.SetStyle(System.Windows.Forms.ControlStyles.UserPaint, false);
            this.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.m_string.FillBrush.SetSolidColor(Colorf.Black);
        }
        protected override void OnDrawItem(System.Windows.Forms.DrawItemEventArgs e)
        {
            e.DrawBackground();
            if (e.Index < 0)
                return;

            ICoreRendererSetting s = (ICoreRendererSetting)this.Items[e.Index];
            this.m_string.SuspendLayout();
            this.m_string.Text = s.Name;
            this.m_string.Bounds = new Rectanglef(e.Bounds.X + 32, e.Bounds.Y, e.Bounds.Width - 32, e.Bounds.Height);
            this.m_string.Font.CopyDefinition(e.Font.ToCoreFont().GetDefinition());
            this.m_string.ResumeLayout();
            this.m_string.Draw(e.Graphics);

            //draw presentation
            var v_rc = new Rectangle(e.Bounds.X, e.Bounds.Y, 32, e.Bounds.Height);
            switch (s.Type)
            {
                case enuRendererSettingType.Color:
                    {
                        Colorf cl = (Colorf)s.Value;
                        e.Graphics.FillRectangle(
                            WinCoreBrushRegister.GetBrush(cl), v_rc.X, v_rc.Y, v_rc.Width, v_rc.Height);

                    }
                    break;
                case enuRendererSettingType.Font:
                    var p = CoreResources.GetDocument(CoreConstant.IMG_POLICE);
                    if (p != null)
                    {
                        p.Draw(e.Graphics, v_rc);
                    }
                    break;

            }
            ControlPaint.DrawBorder(e.Graphics,
                    v_rc, Colorf.Black.ToGdiColor(), ButtonBorderStyle.Solid);

        }
    }
}
