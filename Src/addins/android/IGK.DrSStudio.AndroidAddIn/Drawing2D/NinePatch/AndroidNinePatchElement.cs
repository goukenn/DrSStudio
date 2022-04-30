using IGK.DrSStudio.Android.Drawing2D.Tools;
using IGK.ICore;
using IGK.ICore.Codec;
using IGK.ICore.Drawing2D;
using IGK.ICore.WinUI;
using IGK.ICore.WinUI.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Android.Drawing2D
{
    [Android2DWorkingObject("NinePatch", typeof (Mecanism ))]
    sealed class AndroidNinePatchElement : RectangleElement
    {

        private Vector2i m_Vector1;
        private Vector2i m_Vector2;
        private Vector2i m_Vector3;
        private Vector2i  m_Vector4;
        [CoreXMLAttribute()]
        [CoreConfigurableProperty()]
        public Vector2i  Vector4
        {
            get { return m_Vector4; }
            set
            {
                if (m_Vector4 != value)
                {
                    m_Vector4 = value;
                    OnPropertyChanged(Core2DDrawingChangement.Definition);
                }
            }
        }
        [CoreXMLAttribute()]
        [CoreConfigurableProperty()]
        public Vector2i Vector3
        {
            get { return m_Vector3; }
            set
            {
                if (m_Vector3 != value)
                {
                    m_Vector3 = value;
                    OnPropertyChanged(Core2DDrawingChangement.Definition);
                }
            }
        }
     
        [CoreXMLAttribute()]
        [CoreConfigurableProperty()]
        public Vector2i Vector2
        {
            get { return m_Vector2; }
            set
            {
                if (m_Vector2 != value)
                {
                    m_Vector2 = value;
                    OnPropertyChanged(Core2DDrawingChangement.Definition);
                }
            }
        }
        [CoreXMLAttribute()]
        [CoreConfigurableProperty()]
        public Vector2i Vector1
        {
            get { return m_Vector1; }
            set
            {
                if (m_Vector1 != value)
                {
                    m_Vector1 = value;
                    OnPropertyChanged(Core2DDrawingChangement.Definition);
                }
            }
        }

        protected override void InitGraphicPath(CoreGraphicsPath p)
        {
            p.Reset();
            p.AddRectangle(Vector1.X, 0.0f, Vector1.Y - Vector1 .X , 1.0f);
            p.AddRectangle(0, Vector2.X, 1.0f, Vector2.Y - Vector2.X);


      
            //p.AddRectangle(Vector1.X, 0.0f, Vector1.Y, 0.0f);
           
           // p.AddLine(0.0f, Vector2.X, 0.0f, Vector2.Y);

            var doc = this.ParentDocument;
            if (doc != null) {

                int w = doc.Width;
                int h = doc.Height;
                p.AddRectangle(w-1, Vector3.X, 1.0f, Vector3.Y - Vector3.X);
                p.AddRectangle(Vector4.X, h - 1.0f, Vector4.Y - Vector4.X, 1.0f);
                //p.AddLine(w, Vector3.X, w, Vector3.Y);
                //p.AddLine(Vector4.X, h, Vector4.Y, h);
            }


        }
        /// <summary>
        /// .ctr
        /// </summary>
        public AndroidNinePatchElement()
        {

        }
        protected override void InitializeElement()
        {
            base.InitializeElement();
            this.FillBrush.SetSolidColor(Colorf.Black);
            this.StrokeBrush.SetSolidColor(Colorf.Transparent );
            this.StrokeBrush.Width = 1.0f;
            this.SmoothingMode = enuSmoothingMode.None;



        }
        new class Mecanism : RectangleElement.Mecanism
        {
            private int DM_BOTTOM_SN = 3;
            private int DM_RIGHT_SN = 2;
            private int DM_LEFT_SN = 1;
            private int DM_TOP_SN = 0;
            public new AndroidNinePatchElement Element {
                get {
                    return base.Element as AndroidNinePatchElement;
                }
            }
            protected override void BeginDrawing(CoreMouseEventArgs e)
            {
                base.BeginDrawing(e);
            }

            protected override void InitNewCreatedElement(RectangleElement element, Vector2f defPoint)
            {
                base.InitNewCreatedElement(element, defPoint);
            }

            protected override RectangleElement CreateNewElement()
            {
                var s = this.CurrentSurface.GetProjectElement().GetAttribute < AndroidNinePatchElement>(AndroidConstant.ANDROID_DRAWING2D_NINEPATCHATTRIBUTE);
                if (s == null)
                {
                    return base.CreateNewElement();
                }
                return s;
            }

            protected override void OnMouseDown(CoreMouseEventArgs e)
            {
                switch (e.Button)
                {
                    case enuMouseButtons.Left:
                        this.EndPoint = e.FactorPoint;
                        this.StartPoint = e.FactorPoint;
                        var s = this.CurrentSurface.GetProjectElement().GetAttribute<AndroidNinePatchElement>(AndroidConstant.ANDROID_DRAWING2D_NINEPATCHATTRIBUTE);
            
                        if ((this.Element == null)&&(s==null))
                        {
                            base.Element = CreateNewElement() as AndroidNinePatchElement;
                            if (this.Element != null)
                            {
                                this.InitNewCreatedElement(this.Element, e.FactorPoint);
                                this.CurrentLayer.Elements.Add(this.Element);

                                var v_project =  this.CurrentSurface.GetProjectElement();
                                v_project.SetAttribute(AndroidConstant.ANDROID_DRAWING2D_NINEPATCHATTRIBUTE,
                                    this.Element);

                                this.CurrentLayer.Select(this.Element);
                                this.Element.InitElement();
                                this.BeginDrawing(e);
                                this.State = ST_CREATING;
                                this.GenerateSnippets();
                                this.InitSnippetsLocation();

                                NinePathManagerTool.Instance.Manage(v_project , this.Element, this.CurrentLayer);
                                NinePathManagerTool.Instance.OnMustUpdate();
                                return;
                            }
                        }
                        break;                  
                }
                base.OnMouseDown(e);
            }

            protected override void UpdateDrawing(CoreMouseEventArgs e)
            {
                var v_element = this.Element;

                if (v_element == null)
                {
                    this.State = ST_NONE; 
                    return;
                }
                this.EndPoint = e.FactorPoint;
     
                switch (e.Button)
                {
                    case enuMouseButtons.Left:
                        v_element.SuspendLayout();
                        var v_rc = CoreMathOperation.GetBounds(this.StartPoint, this.EndPoint);
                        v_element.Bounds = v_rc ;
                        v_element.Vector1 = new Vector2i((int)v_rc.X, (int)v_rc.Right);
                        v_element.Vector2 = new Vector2i((int)v_rc.Y  , (int)v_rc.Bottom  );
                        v_element.Vector3 = new Vector2i((int)v_rc.Y, (int)v_rc.Bottom);
                        v_element.Vector4 = new Vector2i((int)v_rc.X, (int)v_rc.Right);
                        v_element.InitElement();
                        v_element.ResumeLayout();
                        this.Invalidate();
                        break;                
                }
            }
            protected override void GenerateSnippets()
            {
                this.DisposeSnippet();
                this.RegSnippets.Add(CurrentSurface.CreateSnippet(this, DM_TOP_SN, 0));
                this.RegSnippets.Add(CurrentSurface.CreateSnippet(this, DM_TOP_SN, 1));
                this.RegSnippets.Add(CurrentSurface.CreateSnippet(this, DM_LEFT_SN, 2));
                this.RegSnippets.Add(CurrentSurface.CreateSnippet(this, DM_LEFT_SN, 3));

                this.RegSnippets.Add(CurrentSurface.CreateSnippet(this, DM_RIGHT_SN, 4));
                this.RegSnippets.Add(CurrentSurface.CreateSnippet(this, DM_RIGHT_SN, 5));
                this.RegSnippets.Add(CurrentSurface.CreateSnippet(this, DM_BOTTOM_SN, 6));
                this.RegSnippets.Add(CurrentSurface.CreateSnippet(this, DM_BOTTOM_SN, 7));
            }
            protected override void InitSnippetsLocation()
            {
                var v_element = this.Element;
                if (v_element == null)
                {
                

                    this.State = ST_NONE;
                    return;
                }
                if (this.RegSnippets.Count >= 8)
                {
                    this.RegSnippets[0].Location = CurrentSurface.GetScreenLocation(new Vector2f(v_element.Vector1.X, 0));
                    this.RegSnippets[1].Location = CurrentSurface.GetScreenLocation(new Vector2f(v_element.Vector1.Y, 0));
                    this.RegSnippets[2].Location = CurrentSurface.GetScreenLocation(new Vector2f(0,v_element.Vector2.X));
                    this.RegSnippets[3].Location = CurrentSurface.GetScreenLocation(new Vector2f(0, v_element.Vector2.Y));

                    int w = this.CurrentDocument.Width;
                    int h = this.CurrentDocument.Height;

                    this.RegSnippets[4].Location = CurrentSurface.GetScreenLocation(new Vector2f(w-1, v_element.Vector3.X));
                    this.RegSnippets[5].Location = CurrentSurface.GetScreenLocation(new Vector2f(w-1, v_element.Vector3.Y));

                    this.RegSnippets[6].Location = CurrentSurface.GetScreenLocation(new Vector2f(v_element.Vector4.X, h-1));
                    this.RegSnippets[7].Location = CurrentSurface.GetScreenLocation(new Vector2f(v_element.Vector4.Y, h-1));

                }

            }
            protected override void UpdateSnippetEdit(CoreMouseEventArgs e)
            {
                var v_element = this.Element;
                if (v_element == null)
                {
                
                    this.State = ST_NONE;
                    return;
                }
                switch (Snippet .Index)
                {
                    case 0:
                        v_element.Vector1 = new Vector2i((int)Math.Ceiling(e.FactorPoint.X), v_element.Vector1.Y);
                        break;
                    case 1:
                        v_element.Vector1 = new Vector2i(v_element.Vector1.X, (int)Math.Ceiling(e.FactorPoint.X));
                        break;
                    case 2:
                        v_element.Vector2 = new Vector2i((int)Math.Ceiling(e.FactorPoint.Y), v_element.Vector2.Y);
                        break;
                    case 3:
                        v_element.Vector2 = new Vector2i(v_element.Vector2.X, (int)Math.Ceiling(e.FactorPoint.Y));
                        break;
                    case 4:
                        v_element.Vector3 = new Vector2i((int)Math.Ceiling(e.FactorPoint.Y), v_element.Vector3.Y);
                        break;
                    case 5:
                        v_element.Vector3 = new Vector2i(v_element.Vector3.X, (int)Math.Ceiling(e.FactorPoint.Y));
                        break;
                    case 6:
                        v_element.Vector4 = new Vector2i((int)Math.Ceiling(e.FactorPoint.X), v_element.Vector4.Y);
                        break;
                    case 7:
                        v_element.Vector4 = new Vector2i(v_element.Vector4.X, (int)Math.Ceiling(e.FactorPoint.X));
                        break;
                    default:
                        break;
                }
                Snippet.Location = e.Location;
                this.Invalidate();
            }
        }
    }
}
