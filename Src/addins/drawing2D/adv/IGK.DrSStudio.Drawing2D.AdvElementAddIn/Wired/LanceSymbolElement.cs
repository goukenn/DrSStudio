

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: LanceSymbolElement.cs
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
file:LanceSymbolElement.cs
*/
using System; 
using IGK.ICore; using IGK.ICore.Drawing2D;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.Drawing2D.Wired
{
    using IGK.ICore.Codec;
    using IGK.ICore.Drawing2D.WinUI;
    using IGK.ICore.WinUI;
    using IGK.ICore.Actions;
    using IGK.ICore.WinUI.Configuration;
    using System.Drawing;
    [LineCorner("LanceSymbol", typeof(Mecanism))]
    class LanceSymbolElement : RectangleElement  
    {
        private float m_Size;
        private float m_TopX;
        private float m_BottomX;
        //private float m_Angle;
        //[CoreXMLAttribute ()]
        //[CoreXMLDefaultAttributeValue (0)]
        //public float Angle
        //{
        //    get { return m_Angle; }
        //    set
        //    {
        //        if (m_Angle != value)
        //        {
        //            m_Angle = value;
        //            OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
        //        }
        //    }
        //}
        [CoreXMLAttribute()]
        [CoreXMLDefaultAttributeValue(0)]
        public float BottomX
        {
            get { return m_BottomX; }
            set
            {
                if (m_BottomX != value)
                {
                    m_BottomX = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        [CoreXMLAttribute()]
        [CoreXMLDefaultAttributeValue(0)]
        public float TopX
        {
            get { return m_TopX; }
            set
            {
                if (m_TopX != value)
                {
                    m_TopX = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        [CoreXMLAttribute()]
        [CoreXMLDefaultAttributeValue(0)]
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
        public LanceSymbolElement()
        {
            this.Size = 2.0f;
            this.m_TopX = 0;
            this.m_BottomX = 0;
        }
        protected override void InitGraphicPath(CoreGraphicsPath path)
        {

            path.Reset();
            Rectanglef rc = this.Bounds;
            if ((rc.Width < this.Size) || (rc.Height < this.Size))
            { 
                return;
            }
            Vector2f[] tpts = new Vector2f[9];
            Vector2f mid = rc.MiddleLeft;
            float _2Size = Size * 2;
            float v_H = rc.Height;
            //float au = (float)( (TopX * v_H )/ (2* Math.Sqrt((TopX * TopX)+(v_H *v_H /4.0f)) ));
            float aru = (2 * Size * TopX / v_H); 
            //float ab = (float)((BottomX * v_H) / (2 * Math.Sqrt((BottomX * BottomX) + (v_H * v_H / 4.0f))));
            float abu = (2 * Size * BottomX / v_H);
            tpts[0] = new Vector2f(mid.X, mid.Y - Size);
            tpts[1] = new Vector2f(mid.X, mid.Y + Size);
            tpts[2] = new Vector2f(mid.X + rc.Width - _2Size -abu  , mid.Y + Size);
            tpts[3] = new Vector2f(rc.Right - _2Size - BottomX, rc.Bottom);
            tpts[4] = new Vector2f(rc.Right - BottomX, rc.Bottom);
            tpts[5] = new Vector2f(mid.X + rc.Width, mid.Y);
            tpts[6] = new Vector2f(rc.Right - TopX , rc.Top );
            tpts[7] = new Vector2f(rc.Right - _2Size - TopX, rc.Top);
            tpts[8] = new Vector2f(mid.X + rc.Width - _2Size-aru , mid.Y - Size);
            path.AddPolygon(tpts);

            //Matrix m = new Matrix();
            //m.RotateAt(this.Angle,rc.Center );
            //path.Transform(m);
            //m.Dispose();
        }
        public override ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
        {
            parameters = base.GetParameters(parameters);
            Type t = this.GetType();
            var g = parameters.AddGroup("LanceSysmbolDefinition");
            g.AddItem(t.GetProperty("Size"));
            g.AddItem(t.GetProperty("TopX"));
            g.AddItem(t.GetProperty("BottomX"));
            //g.AddItem(t.GetProperty("Angle"));
            return parameters;
        }
        new class Mecanism : RectangleElement.Mecanism
        {
            protected override void GenerateSnippets()
            {
                base.GenerateSnippets();
            }
            protected override void InitSnippetsLocation()
            {
                base.InitSnippetsLocation();
            }
            protected override void UpdateSnippetEdit(CoreMouseEventArgs e)
            {
                base.UpdateSnippetEdit(e);
            }
        }
    }
}

