

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: DeformPathElement.cs
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
file:DeformPathElement.cs
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
namespace IGK.DrSStudio.Drawing2D.Selection
{
    using IGK.ICore;
    using IGK.ICore.WinUI;
    using IGK.ICore.GraphicModels;
    using IGK.ICore.MecanismActions;

    using IGK.ICore.WinUI.Configuration;
    /// <summary>
    /// mecanism that works only on path element
    /// </summary>
    [Core2DDrawingSelection ("DeformPath", typeof(Mecanism), IsVisible = true )]
    class DeformPathElement : Core2DDrawingDualBrushElement
    {
        private CoreGraphicsPath m_defaultPath;
        private Mecanism m_targetMecanism;
        public enuWarpMode WarpMode
        {
            get { 
                if (m_targetMecanism !=null)
                 return m_targetMecanism.WarpMode;
                return enuWarpMode.Perspective;
            }
            set
            {
                if (m_targetMecanism != null)
                m_targetMecanism.WarpMode = value;
            }
        }
        public float Flatness
        {
            get
            {
                if (m_targetMecanism != null)
                    return m_targetMecanism.Flatness;
                return 1.0f;
            }
            set
            {
                if ((m_targetMecanism !=null)&&(value >=0.0f) && (value <=1.0f))
                    m_targetMecanism.Flatness  = value;
            }
        }
        public override void Dispose()
        {
            if (this.m_defaultPath != null)
                this.m_defaultPath.Dispose();
            base.Dispose();
        }
        public override ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
        {
           ICoreParameterConfigCollections c =  base.GetParameters(parameters);
           var group = c.AddGroup(CoreConstant.PARAM_DEFINITION);
           group.AddItem(GetType().GetProperty("Flatness"));
           group.AddItem(GetType().GetProperty("WarpMode"));
           c.PropertyChanged += new CoreParameterChangedEventHandler(c_PropertyChanged);
           return c;
        }
        void c_PropertyChanged(object sender, CoreParameterChangedEventArgs e)
        {
            if (this.m_targetMecanism != null)
            {
                this.m_targetMecanism.Invalidate();
            }
        }
        internal DeformPathElement(Mecanism target)
        {
            this.m_targetMecanism = target ;
            this.FillBrush.SetSolidColor(new Colorf(0.5f, 0.2f, 0.4f, 1.0f));
            this.StrokeBrush.SetSolidColor(new Colorf(0.5f, 0.2f, 0.4f, 0.1f));
        }
       protected override void InitGraphicPath(CoreGraphicsPath path)
        {
            path.Reset();
            if (this.m_defaultPath != null)
                path.Add(this.m_defaultPath);
            //this.SetPath(this.m_defaultPath.Clone() as CoreGraphicsPath);
        }
        internal new class Mecanism : PathElementMecanism
        {
            Vector2f[] m_deformPathPoints;
            const int DEFORM_PATH_POINT = 4;
            DeformPathElement m_bckPath;
            CoreGraphicsPath m_bckgPath;
            private float m_Flatness;
            private enuWarpMode m_WarpMode;
            public enuWarpMode WarpMode
            {
                get { return m_WarpMode; }
                set
                {
                    if (m_WarpMode != value)
                    {
                        
                        m_WarpMode = value;
                        this.BuildResult();
                    }
                }
            }
            public float Flatness
            {
                get { return m_Flatness; }
                set
                {
                    if ((m_Flatness != value)&&(value >=0) && (value <=1.0f))
                    {
                        m_Flatness = value;
                        this.BuildResult();
                    }
                }
            }
            /// <summary>
            /// dispose bck path
            /// </summary>
            void DisposeBCKPath()
            {
                if (this.m_bckgPath != null)
                    this.m_bckgPath.Dispose();
                if (m_bckPath != null)
                    this.m_bckPath.Dispose();
                this.m_bckPath = null;
                this.m_bckgPath = null;
            }
            public Mecanism()
            {
                this.m_deformPathPoints = new Vector2f[DEFORM_PATH_POINT];
            }
            protected override void OnElementChanged(CoreWorkingElementChangedEventArgs<PathElement> e)
            {
             
                base.OnElementChanged(e);
                if (this.Element != null)
                {                    
                    this.BuildBCK();                   
                }
            }
            private void BuildBCK()
            {
                this.DisposeBCKPath();
                Vector2f [] t = CoreMathOperation.GetResizePoints(this.Element.GetBound());
                this.m_deformPathPoints[0] = t[0];
                this.m_deformPathPoints[1] = t[2];
                this.m_deformPathPoints[2] = t[6];
                this.m_deformPathPoints[3] = t[4];
                DeformPathElement v = new DeformPathElement(this);
                this.m_bckgPath = this.Element.GetPath().Clone() as CoreGraphicsPath;
                v.m_defaultPath = this.m_bckgPath.Clone() as CoreGraphicsPath;
                this.m_bckPath = v;
            }
            void BuildResult()
            {
                if (this.Element == null)
                    return;
                Vector2f[] v_t = new Vector2f[DEFORM_PATH_POINT];
                v_t[0] = this.m_deformPathPoints[0];
                v_t[1] = this.m_deformPathPoints[1];
                v_t[2] = this.m_deformPathPoints[2];
                v_t[3] = this.m_deformPathPoints[3];
                if (this.m_bckPath.m_defaultPath != null)
                    this.m_bckPath.m_defaultPath.Dispose();
                this.m_bckPath.m_defaultPath = this.m_bckgPath.Clone() as CoreGraphicsPath;
                Matrix m = new Matrix();
                this.m_bckPath.m_defaultPath.Warp(
                    v_t,
                    this.Element.GetBound(),
                    m,
                    this.WarpMode,
                    this.Flatness);
                this.m_bckPath.InitElement();
                this.Invalidate();
            }
            protected override void GenerateSnippets()
            {
                base.GenerateSnippets();
                for (int i = 0; i < DEFORM_PATH_POINT; i++)
                {
                    this.AddSnippet (this.CurrentSurface.CreateSnippet (this, i,i));
                }
            }
            protected override void InitSnippetsLocation()
            {
                base.InitSnippetsLocation();
                if (this.RegSnippets.Count >= DEFORM_PATH_POINT)
                {
                    for (int i = 0; i < DEFORM_PATH_POINT; i++)
                    {
                        this.RegSnippets[i].Location = CurrentSurface.GetScreenLocation(m_deformPathPoints[i]);
                    }
                }
            }
            void UpdateElementPath()
            {
                if (this.Element == null) return;
                CoreGraphicsPath p = this.Element.GetPath();
                //p.Warp (this.m_deformPathPoints.ToArray<Vector2f>(), 
            }
            public override void Render(ICoreGraphics e)
            {
                base.Render(e);
          
                if (this.m_bckPath != null)
                {
                    object  s = e.Save();
                    this.ApplyCurrentSurfaceTransform(e);
                    this.m_bckPath.Draw(e);
                    e.Restore(s);
                }
            }
            protected override void OnMouseDown(CoreMouseEventArgs e)
            {
                switch (e.Button)
                {
                    case enuMouseButtons.Left :
                        if ((this.Element != null) && (Snippet != null))
                        {
                            //begin edit point
                        }
                        else {
                            if (SelectElement(e.FactorPoint))
                            {
                                this.GenerateSnippets();
                                this.InitSnippetsLocation();
                            }
                        }
                        break;
                }
            }
            protected override void OnMouseMove(CoreMouseEventArgs e)
            {
                switch (e.Button)
                {
                    case enuMouseButtons.Left:
                        if ((this.Element != null) && (Snippet != null))
                        {
                            //update edit point
                            UpdateSnippetEdit(e);
                        }
                        break;
                }
            }
            protected override void OnMouseUp(CoreMouseEventArgs e)
            {
                switch (e.Button)
                {
                    case enuMouseButtons.Left:
                        if ((this.Element != null) && (Snippet != null))
                        {
                            //begin edit point
                            UpdateSnippetEdit(e);
                        }                       
                        break;
                    case enuMouseButtons.Right :
                        break;
                }
            }
            public override void Dispose()
            {
                base.Dispose();
                DisposeBCKPath();
            }
            public  override void EndEdition()
            {
                //dispose path
                this.DisposeBCKPath();
                base.EndEdition();
            }
           
            protected override void UpdateSnippetEdit(CoreMouseEventArgs e)
            {
                this.Snippet.Location = e.Location;
                this.m_deformPathPoints[this.Snippet.Index] =
                    e.FactorPoint;
                this.BuildResult();
            }
            protected override ICoreWorkingConfigurableObject GetEditElement()
            {         
                if (this.m_bckPath == null)
                return this.Element;
                return this.m_bckPath;
            }
            //public override ICoreWorkingConfigurableObject GetEditElement()
            //{
            //    if (this.m_bckPath == null)
            //        return this.Element;
            //    return this.m_bckPath;
            //}
            protected override void GenerateActions()
            {
                base.GenerateActions();
                this.Actions[enuKeys.Enter] = new DeformPathElementValidate();
                this.Actions[enuKeys.Escape] = new DeformPathElementEscape();
            }
         
            class DeformPathElementEscape : CoreMecanismActionBase
            {
                protected override bool PerformAction()
                {
                    Mecanism c = this.Mecanism as Mecanism;

                    if ((c.Element != null) && (c.m_bckPath != null))
                    {
                        c.CurrentLayer.Select(null);
                        c.m_bckPath = null;
                        c.Invalidate();
                    }
                    return false;
                }
            }
            class DeformPathElementValidate : CoreMecanismActionBase
            {
                protected override bool PerformAction()
                {
                    Mecanism c = this.Mecanism as Mecanism;

                    c.Element.ClearTransform();
                    c.Element.SetDefinition (c.m_bckPath.m_defaultPath);
                    c.BuildBCK();
                    c.InitSnippetsLocation();
                    c.Invalidate();
                    return false;
                }
            }
        }
    }
}

