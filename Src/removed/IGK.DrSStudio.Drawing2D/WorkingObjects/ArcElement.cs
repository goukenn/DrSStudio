

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ArcElement.cs
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
file:ArcElement.cs
*/
/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igkdev.be
Application : DrSStudio
powered by IGK - DEV &copy; 2008-2012
THIS FILE IS A PART OF THE DRSSTUDIO APPLICATION. SEE "License.txt"
FOR MORE INFORMATION ABOUT THE LICENSE
------------------------------------------------------------------- 
*/
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
namespace IGK.DrSStudio.Drawing2D
{
    [Core2DDrawingStandardItem("Arc",
    typeof(Mecanism),
    Keys = Keys.A)]
    class ArcElement :        
        PieElement,
        ICore2DArcElement
    {
       private bool m_Closed;
       [IGK.DrSStudio.Codec.CoreXMLAttribute()]
       [IGK.DrSStudio.Codec.CoreXMLDefaultAttributeValue(false)]
        public bool Closed
        {
            get { return m_Closed; }
            set
            {
                if (m_Closed != value)
                {
                    m_Closed = value;
                    OnPropertyChanged(new CoreWorkingObjectPropertyChangedEventArgs(enuPropertyChanged.Definition));
                }
            }
        }    
        protected override void GeneratePath()
        {
            if ((this.Radius.X == 0) || (this.Radius.Y == 0) || this.Radius.Equals (Vector2f.Zero ))
            {
                this.SetPath(null);
                return;
            }
            CoreGraphicsPath v_path = new CoreGraphicsPath();
            Rectanglef v_rc =
                CoreMathOperation.GetBounds(
                this.Center,
                this.Radius.X,
                this.Radius.Y);
            v_path.AddArc(v_rc ,
                this.StartAngle,
                this.SweepAngle);
            if (this.Closed)
                v_path.CloseFigure();
            this.SetPath(v_path);
        }
        public override IGK.DrSStudio.WinUI.ICoreParameterConfigCollections GetParameters(IGK.DrSStudio.WinUI.ICoreParameterConfigCollections parameters)
        {
            IGK.DrSStudio.WinUI.ICoreParameterConfigCollections p = base.GetParameters(parameters);
            parameters.AddGroup(CoreConstant.PARAM_DEFINITION).AddItem(GetType().GetProperty("Closed"));
            return p;
        }
        protected override void BuildBeforeResetTransform()
        {
            Matrix m = this.GetMatrix();
            if (m.IsIdentity) return;
            this.Center =  CoreMathOperation.TransformVector2fPoint(m, this.Center)[0];
        }
        new class Mecanism : PieElement.Mecanism
        {
            protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);
                if (this.Element == null)
                    return;
                Vector2f v_c = CurrentSurface.GetScreenLocation(this.Element.Center);
                Vector2f v_t = CurrentSurface.GetScreenLocation ( CoreMathOperation.GetPoint(this.Element.Center, this.Element.Radius, this.Element.StartAngle));
                Vector2f v_s = CurrentSurface.GetScreenLocation(CoreMathOperation.GetPoint(this.Element.Center, this.Element.Radius, this.Element.StartAngle + this.Element.SweepAngle ));
                e.Graphics.DrawLine(Pens.Yellow,
                    v_c,
                    v_t);
                e.Graphics.DrawLine(Pens.Yellow,
                    v_c,
                    v_s);
            }
        }
    }
}

