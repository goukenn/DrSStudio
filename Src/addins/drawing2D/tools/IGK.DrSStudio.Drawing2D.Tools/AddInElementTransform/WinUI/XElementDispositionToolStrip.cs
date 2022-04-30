

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XElementDispositionToolStrip.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/

using IGK.ICore.WinCore;

using IGK.ICore;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.DrSStudio;
using IGK.DrSStudio.Menu;
using IGK.DrSStudio.WinUI;
using IGK.DrSStudio.Drawing2D.Menu;
using IGK.DrSStudio.Drawing2D.WinUI;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:XElementDispositionToolStrip.cs
*/
using System; using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
namespace IGK.DrSStudio.ElementTransform.WinUI
{
    using IGK.DrSStudio.ElementTransform.Tools ;
    using IGK.DrSStudio.Drawing2D;
    using IGK.ICore.WinUI.Configuration;
    using IGK.ICore.Resources;    
    using IGK.ICore.Actions;
    using IGK.ICore.WinCore.WinUI.Controls;
    sealed class XElementDispositionToolStrip
          : IGKXToolStripCoreToolHost 
    {
        public new _ElementDisposition Tool {
            get {
                return base.Tool as _ElementDisposition;
            }
        }
        public XElementDispositionToolStrip(_ElementDisposition tool ):base(tool )
        {
            InitControl();
        }
        private void InitControl()
        {
            IGKXToolStripButton v_btn = null;
            EventHandler btnEvent = new EventHandler(BtnClick);
            foreach (Tools.enuElementDisposition i in Enum.GetValues(typeof(enuElementDisposition)))
            {
                v_btn = new IGKXToolStripButton();
                v_btn.Text = CoreSystem.GetString (
                    string.Format(CoreConstant.ENUMVALUE, i.ToString()));
                v_btn.Click += btnEvent;
                v_btn.DisplayStyle = ToolStripItemDisplayStyle.Image;
                v_btn.Action = new DispositionAction(this, i);
                v_btn.ImageDocument = CoreResources.GetDocument(
                    string.Format("Disp_{0}_gkds", i.ToString()));
                this.Items.Add(v_btn);
            }
            this.AddRemoveButton(null);
        }
        void BtnClick(Object s , EventArgs e)
        {
            IGKXToolStripButton v_btn = s as IGKXToolStripButton ;
            v_btn.Action.DoAction();
        }
        void LoadDisplayString()
        { 
        }
        internal sealed class DispositionAction : CoreActionBase
        {
            Tools.enuElementDisposition m_disp;
            XElementDispositionToolStrip m_tool;
            public DispositionAction(XElementDispositionToolStrip tool , Tools.enuElementDisposition disp)
            {
                this.m_tool = tool;
                m_disp = disp;
            }
            ICore2DDrawingSurface CurrentSurface {
                get {
                    if (this.m_tool.Tool.CurrentSurface !=null)
                        return this.m_tool.Tool.CurrentSurface;
                    return null;
                }
            }
            protected override bool PerformAction()
            {
                ICore2DDrawingSurface s = this.CurrentSurface;
              ICore2DDrawingLayeredElement[] l =   
                  s!=null ? s.CurrentLayer.SelectedElements.ToArray(): null;
              if ((l == null) || (l.Length < 2))
                  return false ;
              Rectanglef rc1 = Rectanglef.Empty;
              Rectanglef rc2 = Rectanglef.Empty;
                switch (this.m_disp)
                {
                    case IGK.DrSStudio.ElementTransform.Tools.enuElementDisposition.SameVerticalSpacing:
                        if (l.Length > 2)
                        {
                            rc1 = l[0].GetBound();
                            rc2 = l[1].GetBound();
                            float v_h = GetVerticalSpacing(rc1,rc2 );
                            this.SetVerticalSpace(v_h, rc1, rc2, l);
                            s.Invalidate();
                        }
                        break;
                    case IGK.DrSStudio.ElementTransform.Tools.enuElementDisposition.SameHorizontalSpacing:
                        if (l.Length > 2)
                        {
                            rc1 = l[0].GetBound();
                            rc2 = l[1].GetBound();
                            float v_w = GetHorizontalSpacing(rc1, rc2);                            
                            this.SetHorizontalSpace(v_w, rc1, rc2, l);
                            s.Invalidate();
                        }
                        break;
                    case IGK.DrSStudio.ElementTransform.Tools.enuElementDisposition.CustomVerticalSpacing:
                        {
                            if (l.Length > 1)
                            {
                                rc1 = l[0].GetBound();
                                rc2 = l[1].GetBound();
                                SpacingConfiguration c = new SpacingConfiguration("VerticalSpacing");
                                if (m_tool.WorkBench.ConfigureWorkingObject(c,"title.editverticalspacing".R(),
                                    true, Size2i.Empty ).Equals (enuDialogResult.OK))
                                {
                                    this.SetVerticalSpace(-c.Value , rc1, rc2, l);
                                    s.Invalidate();
                                }
                            }
                        }
                        break;
                    case IGK.DrSStudio.ElementTransform.Tools.enuElementDisposition.CustomHorizontalSpacing:
                        {
                            if (l.Length > 1)
                            {
                                rc1 = l[0].GetBound();
                                rc2 = l[1].GetBound();
                                SpacingConfiguration c = new SpacingConfiguration("HorizontalSpacing");
                                if (m_tool.WorkBench.ConfigureWorkingObject(c, "title.edithorizontalspacing".R(), true, Size2i.Empty).Equals(enuDialogResult.OK))
                                {
                                    this.SetHorizontalSpace(-c.Value, rc1, rc2, l);
                                    s.Invalidate();
                                }
                            }
                        }
                        break;
                    case IGK.DrSStudio.ElementTransform.Tools.enuElementDisposition.IncreaseVerticalSpacing:
                        if (l.Length > 2)
                        {
                            rc1 = l[0].GetBound();
                            rc2 = l[1].GetBound();
                            float v_h = GetVerticalSpacing(rc1, rc2);
                            v_h *= 2;
                            if (v_h > 0) v_h *= -1;
                            this.SetVerticalSpace(v_h, rc1, rc2, l);
                            s.Invalidate();
                        }
                        //x 2
                        break;
                    case IGK.DrSStudio.ElementTransform.Tools.enuElementDisposition.DecreaseVerticalSpacing:
                        // /2
                        if (l.Length > 2)
                        {
                            rc1 = l[0].GetBound();
                            rc2 = l[1].GetBound();
                            float v_h = GetVerticalSpacing(rc1, rc2);
                            v_h /= 2;
                            if (v_h > 0) v_h *= -1;
                            this.SetVerticalSpace(v_h, rc1, rc2, l);
                            s.Invalidate();
                        }
                        break;
                    case enuElementDisposition .DecreaseHSpacing :
                        //x2 the base sapcing
                        if (l.Length > 2)
                        {
                            rc1 = l[0].GetBound();
                            rc2 = l[1].GetBound();
                            float v_w = GetHorizontalSpacing(rc1, rc2);
                            v_w /= 2;
                            if (v_w > 0) v_w *= -1;
                            this.SetHorizontalSpace(v_w, rc1, rc2, l);
                            s.Invalidate();
                        }
                        break;
                    case enuElementDisposition .IncreaseHSpacing :
                        // *2
                        if (l.Length > 2)
                        {
                            rc1 = l[0].GetBound();
                            rc2 = l[1].GetBound();
                            float v_w = GetHorizontalSpacing(rc1, rc2);
                            v_w *= 2;
                            if (v_w > 0) v_w *= -1;
                            this.SetHorizontalSpace(v_w, rc1, rc2, l);
                            s.Invalidate();
                        }
                        break;
                    default:
                        break;
                }
                return false; 
            }
            private void SetVerticalSpace(float v_h, Rectanglef rc1, Rectanglef rc2, ICore2DDrawingLayeredElement[] l)
            {
                Vector2f c = Vector2f.Zero;
                int i = 2;
                if (v_h < 0)
                {
                    i = 1;
                    v_h *= -1;
                    rc2 = rc1;
                }
                for (; i < l.Length; i++)
                {
                    rc1 = l[i].GetBound();
                    c = new Vector2f(rc1.X, rc2.Bottom + v_h);
                    c = CoreMathOperation.GetDistanceP(c, rc1.Location);
                    l[i].Translate(c.X, c.Y, enuMatrixOrder.Append);
                    rc2 = l[i].GetBound();
                }
            }
            private void SetHorizontalSpace(float v_w, Rectanglef rc1, Rectanglef rc2, ICore2DDrawingLayeredElement[] l)
            {
                Vector2f c = Vector2f.Zero;
                int i = 2;
                if (v_w < 0)
                {
                    i = 1;
                    v_w *= -1;
                    rc2 = rc1;
                }
                for (; i < l.Length; i++)
                {
                    rc1 = l[i].GetBound();
                    c = new Vector2f(rc2.Right   + v_w, rc1.Top);
                    c = CoreMathOperation.GetDistanceP(c, rc1.Location);
                    l[i].Translate(c.X, c.Y, enuMatrixOrder.Append);
                    rc2 = l[i].GetBound();
                }
            }
            private float GetVerticalSpacing(Rectanglef rec1, Rectanglef rec2)
            {
                if (rec1.Top == rec2.Top)
                {
                    return Math.Abs(rec1.Height - rec2.Height);
                }
                if (rec1.Top < rec2.Top)
                {
                    return -rec1.Bottom + rec2.Top;
                }
                else {
                    return -rec2.Bottom + rec1.Top;
                }
            }
            private float GetHorizontalSpacing(Rectanglef rec1, Rectanglef rec2)
            {
                if (rec1.Left == rec2.Left)
                {
                    return Math.Abs(rec1.Width  - rec2.Width );
                }
                if (rec1.Left  < rec2.Left )
                {
                    return -rec1.Right  + rec2.Left ;
                }
                else
                {
                    return -rec2.Right+ rec1.Left;
                }
            }
            public override string Id
            {
                get { return string.Format("Drawing2DDispositioin.{0}", this.GetType().Name); }
            }
        }
        /// <summary>
        /// represent a spacing configurable object
        /// </summary>
        sealed class SpacingConfiguration : ICoreWorkingConfigurableObject
        {
            string m_id;
            private int m_Value;
            public int Value
            {
                get { return m_Value; }
                set
                {
                    if (m_Value != value)
                    {
                        m_Value = value;
                    }
                }
            }
            public SpacingConfiguration(string id)
            {
                this.m_id = id;
            }
            #region ICoreWorkingConfigurableObject Members
            public enuParamConfigType GetConfigType()
            {
                return enuParamConfigType.ParameterConfig;
            }
            public ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
            {
                parameters.AddGroup ("Definition").AddItem("Spacing", "lb.Spacing.caption", enuParameterType.Text, SpaceChanged);
                return parameters;
            }
            void SpaceChanged(object sender,CoreParameterChangedEventArgs   e)
            {
                if ((e.Value ==null) || (string.IsNullOrEmpty (e.Value.ToString ())))
                    return ;
                CoreUnit c = (CoreUnit)e.Value.ToString();
                this.Value =Convert.ToInt32 ( Math.Ceiling( ((ICoreUnitPixel)c).Value));
            }
            public ICoreControl GetConfigControl()
            {
                throw new NotImplementedException();
            }
            #endregion
            #region ICoreIdentifier Members
            public string Id
            {
                get { return this.m_id; }
            }
            #endregion
        }
    }
}

