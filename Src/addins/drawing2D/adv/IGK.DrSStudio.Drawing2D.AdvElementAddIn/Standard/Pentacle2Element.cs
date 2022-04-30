

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Pentacle2Element.cs
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
file:Pentacle2Element.cs
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
using System; 
using IGK.ICore;  using IGK.ICore.Drawing2D;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
namespace IGK.DrSStudio.Drawing2D.Standard
{
    using IGK ;
    using IGK.ICore.Codec;
    using IGK.ICore.WinUI.Configuration;
    using IGK.ICore;
    using IGK.ICore.WinUI;
    [Core2DDrawingStandardElement ("Pentacle2", typeof (Mecanism ))]
    class Pentacle2Element : PentacleElement 
    {
        private int m_RoundItem;
        [CoreXMLAttribute ()]
public int RoundItem{
get{ return m_RoundItem;}
set{ 
if ((m_RoundItem !=value)&&(value > 0))
{
m_RoundItem =value;
OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
}
}
}
        public override ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
        {
            ICoreParameterConfigCollections p = base.GetParameters(parameters);
            ICoreParameterGroup group = p.AddGroup("Definition");
            group.AddItem(GetType().GetProperty("RoundItem"));
            return p;
        }
        public Pentacle2Element()
        {
            this.m_RoundItem = 4;
        }
        protected override void InitGraphicPath(CoreGraphicsPath path)
        {

            path.Reset();
            float v_step = (float)((360 / (float)(Count)) * (Math.PI / 180.0f));
            float v_angle = (float)(this.Angle * Math.PI / 180.0F);
            System.Collections.Generic.List<Vector2f> v_pts =
                new System.Collections.Generic.List<Vector2f>();
            float v_offset = 0.0f;
            CoreGraphicsPath c = null;
            for (int r = 0; r < this.Radius.Length; r++)
            {


                c = new CoreGraphicsPath();
                for (int j = 0; j < this.m_RoundItem; ++j)
                {
                    v_pts.Clear();
                    v_offset = (j * ((360.0F / (float)this.m_RoundItem) / this.Count) * CoreMathOperation.ConvDgToRadian) + v_angle;
                    for (int i = 0; i < this.Count; i++)
                    {
                        v_pts.Add(new Vector2f(
                            (float)(this.Center.X + this.Radius[r] * Math.Cos(i * v_step + v_offset)),
                            (float)(this.Center.Y + this.Radius[r] * Math.Sin(i * v_step + v_offset))));
                    }

                    if (this.EnableTension)
                    {
                        c.AddClosedCurve(v_pts.ToArray(), Tension);
                    }
                    else
                        c.AddPolygon(v_pts.ToArray());

                }
                path.Add(c);
        }
            path.FillMode = this.FillMode;
    }
        new sealed class Mecanism : PentacleElement.Mecanism
        { 
        }
}
}

