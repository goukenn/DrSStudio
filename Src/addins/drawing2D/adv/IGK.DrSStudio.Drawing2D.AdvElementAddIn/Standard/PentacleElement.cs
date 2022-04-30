

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: PentacleElement.cs
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
file:PentacleElement.cs
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
    using IGK.ICore.WinUI;
    using IGK.ICore.Drawing2D.Mecanism;
    using IGK.ICore.WinUI.Configuration;
    using IGK.ICore.Codec;
    using IGK.ICore.Drawing2D;
    using IGK.ICore;
    [Core2DDrawingStandardElement ("Pentacle", typeof (Mecanism))]
    public class PentacleElement : 
        //Core2DDrawingDualBrushElement ,
        CircleElement ,
        ICore2DTensionElement 
    {
        //private Vector2f m_Center;
        //private float m_Radius;
        private int m_Count;
        private float m_Angle;
      //  private enuFillMode m_FillMode;
        private float m_Tension;
        private bool m_EnableTension;
        public PentacleElement()
        {
            
        }
        protected override void InitializeElement()
        {
            base.InitializeElement();
            this.m_Count = 5;
            this.m_EnableTension = false;
        }
        [CoreXMLAttribute()]
        [CoreXMLDefaultAttributeValue (false )]
        public bool EnableTension
        {
            get { return m_EnableTension; }
            set
            {
                if (m_EnableTension != value)
                {
                    m_EnableTension = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        [CoreXMLAttribute()]
        public float Tension
        {
            get { return m_Tension; }
            set
            {
                if (m_Tension != value)
                {
                    m_Tension = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        //[CoreXMLAttribute()]
        //public enuFillMode FillMode
        //{
        //    get { return m_FillMode; }
        //    set
        //    {
        //        if (m_FillMode != value)
        //        {
        //            m_FillMode = value;
        //            OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
        //        }
        //    }
        //}
        [CoreXMLAttribute()]
        public float Angle
        {
            get { return m_Angle; }
            set
            {
                if (m_Angle != value)
                {
                    m_Angle = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        [CoreXMLAttribute()]
        [CoreXMLDefaultAttributeValue (5)]
        public int Count
        {
            get { return m_Count; }
            set
            {
                if ((m_Count != value)&& (value > 1))
                {
                    m_Count = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        //[CoreXMLAttribute()]
        //public float Radius
        //{
        //    get { return m_Radius; }
        //    set
        //    {
        //        if (m_Radius != value)
        //        {
        //            m_Radius = value;
        //            OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
        //        }
        //    }
        //}
        //[CoreXMLAttribute()]
        //public Vector2f Center
        //{
        //    get { return m_Center; }
        //    set
        //    {
        //        if (!m_Center.Equals (value))
        //        {
        //            m_Center = value;
        //            OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
        //        }
        //    }
        //}
        protected override void InitGraphicPath(CoreGraphicsPath path)
        {
            path.Reset();
            if (this.m_Count == 0)
                return;
            Vector2f[] vtab1 = new Vector2f[this.m_Count];
            float step = (float)((360 / (float)(m_Count)) * (Math.PI / 180.0f));
            int decal = (this.m_Count / 3) + 1;
            float vangle = (float)(m_Angle * (Math.PI / 180.0f));
            CoreGraphicsPath c = null;

            for (int r = 0; r < this.Radius.Length; r++)
            {
                for (int i = 0; i < this.m_Count; i++)
                {
                    vtab1[i] = new Vector2f(
                        (float)(this.Center.X + this.Radius[r] * Math.Cos(i * decal * step + vangle)),
                        (float)(this.Center.Y + this.Radius[r] * Math.Sin(i * decal * step + vangle)));
                }
                if (vtab1.Length > 0)
                {
                    c = new CoreGraphicsPath();
                    if (this.EnableTension)
                    {
                        c.AddClosedCurve(vtab1, this.Tension);
                    }
                    else
                    {
                        c.AddPolygon(vtab1);
                    }
                    path.Add(c);
                }
            }
            path.FillMode = this.FillMode;
            //if (this.EnableTension)
            //{
            //    path.AddClosedCurve(vtab1, this.Tension);
            //}
            //else
            //{
            //    path.AddPolygon(vtab1);
            //}
        }
        public override ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
        {
            ICoreParameterConfigCollections p = base.GetParameters(parameters);
            ICoreParameterGroup group = p.AddGroup("Definition");
            group.AddItem(GetType().GetProperty("FillMode"));
            group.AddItem(GetType().GetProperty("Count"));            
            group.AddItem(GetType().GetProperty("EnableTension"));
            group.AddItem(GetType().GetProperty("Tension"));
            return p;
        }
        protected override void BuildBeforeResetTransform()
        {
            Matrix m = this.GetMatrix();
            if (m.IsIdentity) return;
            Vector2f[] t = new Vector2f[] { 
                this.m_Center 
            };
            t = CoreMathOperation.TransformVector2fPoint(m, t);
            this.m_Center = t[0];
        }
        /// <summary>
        /// mecanism for simple pentacle element
        /// </summary>
        protected new class Mecanism : CircleElement.Mecanism // Core2DDrawingSurfaceMecanismBase<PentacleElement>
        {

            protected override void InitSnippetsLocation()
            {
                base.InitSnippetsLocation();
                //if (this.Element == null)
                //    return;
                //PentacleElement v_l = this.Element;
                //this.RegSnippets[0].Location = CurrentSurface.GetScreenLocation(v_l.m_Center);
                //this.RegSnippets[1].Location = CurrentSurface.GetScreenLocation(CoreMathOperation.GetPoint (
                //    v_l.m_Center,
                //    new Vector2f[]{ v_l.m_Radius },
                //    v_l.Angle ));
            }
            protected override void GenerateSnippets()
            {
                base.GenerateSnippets();
                //this.AddSnippet(CurrentSurface.CreateSnippet(this, 0, 0));
                //this.AddSnippet(CurrentSurface.CreateSnippet(this, 1, 1));
            }
            protected override void BeginDrawing(CoreMouseEventArgs e)
            {                
                base.BeginDrawing(e);
                PentacleElement v_l = this.Element as PentacleElement  ;
                v_l.m_Center = e.FactorPoint;
                v_l.m_Radius = new float []{1.0f};
                v_l.InitElement();
                this.DisableSnippet();
                this.Invalidate();
                this.State = ST_CREATING;
            }
            protected override void UpdateDrawing(CoreMouseEventArgs e)
            {
             
                PentacleElement v_l = this.Element as PentacleElement;
                switch (e.Button)
                {
                    case enuMouseButtons .Left :
                        //v_l.m_Radius[0] = CoreMathOperation.GetDistance(e.FactorPoint, v_l.Center);
                        v_l.m_Angle = CoreMathOperation.GetAngle(e.FactorPoint, v_l.Center) * CoreMathOperation.ConvRdToDEGREE;
                        break;
                }
                v_l.InitElement();
                base.UpdateDrawing(e);
            }
            protected override void EndDrawing(CoreMouseEventArgs e)
            {
                UpdateDrawing(e);
                this.InitSnippetsLocation();
                this.EnabledSnippet();
                this.State = ST_EDITING;
            }
            protected override void UpdateSnippetEdit(CoreMouseEventArgs e)
            {
                //switch (e.Button)
                //{
                //    case enuMouseButtons.Left :

                //        PentacleElement v_cl = this.Element as PentacleElement;

                //        switch (this.Snippet.Demand)
                //        {
                //            case 0:
                //                this.Element.Center = e.FactorPoint;
                //                break;
                //            case 1:
                //                v_cl.m_Radius[0] = CoreMathOperation.GetDistance(e.FactorPoint, this.Element.Center);
                //                v_cl.m_Angle = CoreMathOperation.GetAngle(e.FactorPoint, v_cl.Center) * CoreMathOperation.ConvRdToDEGREE;
                //                this.RegSnippets[1].Location = e.Location;                                
                //                break;
                //        }
                //        v_cl.InitElement();
                //        this.Invalidate();
                //        break;
                //}
                base.UpdateSnippetEdit(e);
            }
        }
    }
}

