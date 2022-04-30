

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: MangaRectangleSCI.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿
using IGK.ICore;
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
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace IGK.DrSStudio.MangaStuffAddIn.WorkingElements
{
    using IGK.ICore.WinUI.Configuration;
    using IGK.ICore.Codec;
    [MangaStuffElement ("MangaSCI", typeof (Mecanism ))]
    public class MangaRectangleSCI : 
        RectangleElement ,
        ICore2DTensionElement 

    {
        private int m_Count;
        private float m_Size;
        private float m_StepAngle;
        
        private bool m_EnableTension;
        private float m_Tension;
        private enuFillMode m_FillMode;
        [CoreXMLAttribute()]
        [CoreXMLDefaultAttributeValue(enuFillMode.Alternate )]     
        public enuFillMode FillMode
        {
            get { return m_FillMode; }
            set
            {
                if (m_FillMode != value)
                {
                    m_FillMode = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
 
                }
            }
        }

        [CoreXMLAttribute()]
        [CoreXMLDefaultAttributeValue(0.0f)]
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
        [CoreXMLAttribute()]
        [CoreXMLDefaultAttributeValue(false)]
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
        [CoreXMLDefaultAttributeValue(1.0f)]
        public float StepAngle
        {
            get { return m_StepAngle; }
            set
            {
                if ((m_StepAngle != value) && (value > 0))
                {
                    m_StepAngle = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }


        [CoreXMLAttribute ()]
        public float Size
        {
            get { return m_Size; }
            set
            {
                if (m_Size != value)
                {
                    m_Size = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                
                }
            }
        }
        [CoreXMLAttribute()]
        public int Count
        {
            get { return m_Count; }
            set
            {
                if ((m_Count != value) && (value > 0))
                {
                    m_Count = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                
                }
            }
        }
        public MangaRectangleSCI()
        {
            this.m_Count = 10;
            this.m_Size = 5;
            this.m_EnableTension = false;
            this.m_Tension = 0.0f;
            this.m_StepAngle = 1.0f;
            
        }

        public override ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
        {
            parameters = base.GetParameters(parameters);
            var g = parameters.AddGroup(CoreConstant.PARAM_DEFINITION);
            g.AddItem(GetType().GetProperty("StepAngle"));
            g.AddItem(GetType().GetProperty("Count"));
            g.AddItem(GetType().GetProperty("Size"));
            g.AddItem(GetType().GetProperty("EnableTension"));
            g.AddItem(GetType().GetProperty("Tension"));
            g.AddItem(GetType().GetProperty("enuFillMode"));


            return parameters;
        }
        protected override void InitGraphicPath(CoreGraphicsPath path)
        {
            path.Reset();
            
            Rectanglef rc = this.Bounds;
            Vector2f [] t = CoreMathOperation.GetPoints(rc);
            m_StepAngle = Math.Max(1.0f, this.m_StepAngle);
            path.AddLine(t[1], t[0]);
            path.AddLine(t[0], t[3]);
            path.AddLine(t[3], t[2]);

            float H = rc.Height ;
            float h = rc.Height * (this.Size / 100f);
            
            if ((h > 0)&&(Count > 0))
            {
                float tp = (m_StepAngle * H)/ (float)(360.0f * Count);
                if (tp > 0.0f)
                {

                    float v_t = 0.0f;
                    List<Vector2f> v_pt = new List<Vector2f>();
                    for (float x = 0; x < H; x += tp)
                    {
                        v_t = (float)(x * Count * 2 * Math.PI / H);
                        v_pt.Add(new Vector2f((float)
                            ((t[2].X )+ h * Math.Sin(v_t)),
                            t[2].Y - x
                            ));
                    }
                    if (this.EnableTension)
                    {
                        path.AddCurve(v_pt.ToArray(), this.Tension, false  );    
                    }
                    else 
                    path.AddCurve(v_pt.ToArray());
                }
            }
            //render sin beetwed 1 and 2

            path.FillMode = this.FillMode;
            path.CloseFigure();
        }

        new class Mecanism : RectangleElement.Mecanism
        { 

        }

    }
}
