

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ColorfTypeEditor.cs
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
file:ColorfTypeEditor.cs
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;
using IGK.ICore.WinCore;
using IGK.ICore;
namespace IGK.ICore.WinCore.ComponentModel
{
    /// <summary>
    /// represent the colorf editor class
    /// </summary>
    internal class ColorfTypeEditor : System.Drawing.Design.UITypeEditor 
    {
        Dictionary<Colorf, Brush> sm_brushes = new Dictionary<Colorf, Brush>();
        public override bool IsDropDownResizable
        {
            get
            {
                return true;
            }
        }
        public override bool GetPaintValueSupported(ITypeDescriptorContext context)
        {
            return true;
        }
        public override void PaintValue(System.Drawing.Design.PaintValueEventArgs e)
        {
           try
            {
                Colorf cl = (Colorf)e.Value;
                System.Drawing.Brush br = this.GetBrush(cl);
                if (br != null)
                {
                    lock (br)
                    {
                        e.Graphics.FillRectangle(br, e.Bounds);
                    }
                }
                ControlPaint.DrawBorder(e.Graphics, e.Bounds, Color.Black, 
                    ButtonBorderStyle.Solid);
                
            }
            catch(Exception ex) { 
                CoreLog.WriteLine ("Exception produite "+ex.Message );
            }
        }

        private Brush GetBrush(Colorf cl)
        {
            if (this.sm_brushes.ContainsKey(cl))
            {
                return this.sm_brushes[cl];
            }
            Brush br = new SolidBrush(cl.CoreConvertTo<Color>());
            this.sm_brushes.Add(cl, br);
            return br;
        }
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {            
            return base.EditValue(context, provider, value);
        }
    }
}

