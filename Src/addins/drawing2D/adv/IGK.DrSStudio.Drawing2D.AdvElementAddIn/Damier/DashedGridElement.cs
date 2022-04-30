

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: DashedGridElement.cs
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
file:DashedGridElement.cs
*/

using IGK.ICore.WinUI;
using IGK.ICore.WinUI.Configuration;
using System; using IGK.ICore; using IGK.ICore.Drawing2D;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IGK.ICore.Codec;
namespace IGK.DrSStudio.Drawing2D.Damier
{
    /// <summary>
    /// represent a dashed grid element
    /// </summary>
    [LineCorner("DashedGrid", typeof(Mecanism))]
    public sealed class DashedGridElement :
            Core2DDrawingDualBrushElement       ,
        ICore2DFillModeElement 
    {
        private int m_Col;
        private int m_Row;
        private int m_OffsetX;
        private int m_OffsetY;
        private enuFillMode m_FillMode;
        private int m_Size;
        private enuDashedElementType  m_DashType;
        public override bool CanReSize=>false;
        public override bool CanRotate=>false;
        public override bool CanTranslate=>false;


        [CoreXMLAttribute()]
        [CoreXMLDefaultAttributeValue(enuDashedElementType.Circle )]
        public enuDashedElementType  DashType
        {
            get { return m_DashType; }
            set
            {
                if (m_DashType != value)
                {
                    m_DashType = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
            [CoreXMLAttribute ()]
            [CoreXMLDefaultAttributeValue(1)]
            public int Size
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
            [CoreXMLDefaultAttributeValue(0)]
        public int OffsetY
        {
            get { return m_OffsetY; }
            set
            {
                if (m_OffsetY != value)
                {
                    m_OffsetY = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
            [CoreXMLAttribute()]
            [CoreXMLDefaultAttributeValue(0)]
        public int OffsetX
        {
            get { return m_OffsetX; }
            set
            {
                if (m_OffsetX != value)
                {
                    m_OffsetX = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
            [CoreXMLAttribute()]
            [CoreXMLDefaultAttributeValue(10)]
        public int Row
        {
            get { return m_Row; }
            set
            {
                if (m_Row != value)
                {
                    m_Row = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        [CoreXMLAttribute()]
        [CoreXMLDefaultAttributeValue(0)]
        public int Col
        {
            get { return m_Col; }
            set
            {
                if (m_Col != value)
                {
                    m_Col = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }

        protected override void InitializeElement()
        {
            base.InitializeElement();
            this.m_Col = 10;
            this.m_Row = 10;
            this.m_Size = 1;
            this.m_DashType = enuDashedElementType.Circle;
        }
        public DashedGridElement()
        {
        }
           protected override void InitGraphicPath(CoreGraphicsPath v_path)
           {

               v_path.Reset();
               if (this.ParentDocument == null)
                {
                    return;
                }
                int w = this.ParentDocument.Width;
                int h = this.ParentDocument.Height;
                int stepx = w / this.m_Row ;
                int stepy = h / this.m_Col ;
                Vector2f v_ps = Vector2f .Zero;
                Rectanglef v_e = Rectanglei.Empty ;
                switch (this.DashType)
                {
                    case enuDashedElementType.Circle:
                        for (int i = 0; i <= this.m_Row; i++)
                        {
                            for (int j = 0; j <= this.m_Col; j++)
                            {
                                v_ps = new Vector2f(this.m_OffsetX + i * stepx,
                                    this.m_OffsetY + j * stepy);
                                v_e = new Rectanglef(v_ps, Size2f.Empty);
                                v_e.Inflate(this.m_Size, this.m_Size);
                                v_path.AddEllipse(v_e);
                            }
                        }
                        break;
                    case enuDashedElementType.Square :
                        for (int i = 0; i <= this.m_Row; i++)
                        {
                            for (int j = 0; j <= this.m_Col; j++)
                            {
                                v_ps = new Vector2f(this.m_OffsetX + i * stepx,
                                    this.m_OffsetY + j * stepy);
                                v_e = new Rectanglef(v_ps, Size2f.Empty);
                                v_e.Inflate(this.m_Size, this.m_Size);
                                v_path.AddRectangle(v_e);
                            }
                        }
                        break;
                    case enuDashedElementType.Diamond :
                        for (int i = 0; i <= this.m_Row; i++)
                        {
                            for (int j = 0; j <= this.m_Col; j++)
                            {
                                v_ps = new Vector2f(this.m_OffsetX + i * stepx,
                                    this.m_OffsetY + j * stepy);
                                v_e = new Rectanglef(v_ps, Size2f.Empty);
                                v_e.Inflate(this.m_Size, this.m_Size);
                                v_path.AddPolygon(CoreMathOperation.GetPolygons (v_ps, this.m_Size, this.m_Size , 4, 0));
                            }
                        }
                        break;
                    case enuDashedElementType.Custom :
                        //not yet implemented
                        break;
                }
                v_path.FillMode = this.FillMode;
            }
            //public override Rectanglef GetSelectionBound()
            //{
            //    if (this.ParentDocument == null)
            //    {
            //        return Rectanglef.Empty;
            //    }
            //    int w = this.ParentDocument.Width;
            //    int h = this.ParentDocument.Height;
            //    Rectanglef v_rc = new Rectanglef(0, 0, w, h);
            //    Matrix mm = GetDocumentMatrix();
            //    v_rc = CoreMathOperation.ApplyMatrix(v_rc, mm);
            //    mm.Dispose();
            //    return v_rc;                
            //}
        /// <summary>
        /// represent a dashed grid mecanism
        /// </summary>
            new class Mecanism : Core2DDrawingDualBrushElement.Mecanism 
            {
            public new DashedGridElement Element => base.Element as DashedGridElement ;

            protected override void EndDrawing(CoreMouseEventArgs e)
            {
                base.EndDrawing(e);
                this.Element.InitElement ();
                this.Invalidate();
            }
            protected override void InitSnippetsLocation()
                {
                    //base.InitSnippetsLocation();
                }
                protected override void GenerateActions()
                {
                    //base.GenerateActions();
                }
                protected override void GenerateSnippets()
                {
                    this.DisposeSnippet();
                    //base.GenerateSnippets();
                }
            }
            public override ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
            {
                parameters = base.GetParameters(parameters);
                var group = parameters.AddGroup("DashedGridProperty");
                Type t = this.GetType();
                group.AddItem(t.GetProperty("DashType"));
                group.AddItem(t.GetProperty ("Row"));
                group.AddItem(t.GetProperty ("Col"));
                group.AddItem(t.GetProperty("Size"));
                group.AddItem(t.GetProperty ("OffsetX"));
                group.AddItem(t.GetProperty ("OffsetY"));
                return parameters;
            }
    }
}

