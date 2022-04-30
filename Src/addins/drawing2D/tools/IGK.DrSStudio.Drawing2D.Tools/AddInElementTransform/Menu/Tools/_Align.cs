

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _Align.cs
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
file:_Align.cs
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
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.ElementTransform.Menu.Tools
{
    using IGK.DrSStudio.Drawing2D;
    [DrSStudioMenu("Tools.Align",
     20, SeparatorBefore=true )]
    class _Align : Core2DDrawingMenuBase 
    {
        public _Align()
        {
            //register alignment menu
            int v_count = 0;
            foreach (enuCore2DAlignElement item in Enum.GetValues (typeof (enuCore2DAlignElement )))
            {
                CoreMenuAttribute v_attr = new CoreMenuAttribute(string.Format (this.Id+".{0}", item.ToString()), v_count);
                v_attr.ImageKey = $"btn_2DAlign_{item.ToString()}_gkds"; 
                ChildAlingMenu ch = new ChildAlingMenu(item);
                ch.SetAttribute(v_attr);
                //System.ComponentModel.TypeDescriptionProvider  t = System.ComponentModel.TypeDescriptor.AddAttributes(typeof(ChildMenu), v_attr);
                //ch = t.CreateInstance(null, typeof(ChildMenu), new Type[] { typeof(enuCore2DAlignElement) },
                //    new object[] { item }) as ChildMenu;
                if (this.Register(v_attr, ch) == false)
                {
#if DEBUG
                    CoreMessageBox.Show(string.Format ("Element not  : [{0}]", v_attr.Name ));
#endif
                    continue;

                }
                v_count++;
                this.Childs.Add(ch);
            }
            bool v = true ;
            foreach (enuCore2DSpecialAlignElement item in Enum.GetValues(typeof(enuCore2DSpecialAlignElement)))
            {
                CoreMenuAttribute v_attr = new CoreMenuAttribute(string.Format(this.Id + ".{0}", item.ToString()), v_count );
                if (v)
                {
                    v_attr.SeparatorBefore = true;
                    v = false;
                }
                v_attr.ImageKey = $"btn_2DAlign_{item.ToString()}_gkds";
                ChildSAlingMenu ch = new ChildSAlingMenu(item);
                ch.SetAttribute(v_attr);
                //register menu
                if (this.Register(v_attr, ch) == false)
                {
#if DEBUG
                    CoreMessageBox.Show(string.Format("Element not  : [{0}]", v_attr.Name));
#endif
                    continue;

                }
                v_count++;
                this.Childs.Add(ch);
            }
            //
        }
        protected override void OnWorkbenchChanged(EventArgs eventArgs)
        {
            base.OnWorkbenchChanged(eventArgs);
        }
        sealed class ChildAlingMenu : Core2DDrawingMenuBase 
        {
            internal enuCore2DAlignElement alignment;
            internal ChildAlingMenu( enuCore2DAlignElement alignment)
            {
                this.alignment = alignment;
            }
            protected override bool PerformAction()
            {
                ICore2DDrawingLayeredElement[] elements =  CurrentSurface.CurrentLayer.SelectedElements.ToArray();
                Rectanglef docBound = this.CurrentSurface .CurrentDocument .Bounds ;
                for (int i = 0; i < elements.Length; i++)
                {
                    elements[i].Align(alignment);
                }
                return false;
            }
            public override string ToString()
            {
                return "Align : " + this.alignment;
            }
        }
        sealed class ChildSAlingMenu : Core2DDrawingMenuBase
        {
            internal enuCore2DSpecialAlignElement alignment;
            internal ChildSAlingMenu(enuCore2DSpecialAlignElement alignment)
            {
                this.alignment = alignment;
            }
            protected override bool PerformAction()
            {
                ICore2DDrawingLayeredElement[] elements = CurrentSurface.CurrentLayer.SelectedElements.ToArray();
                if (elements .Length == 0)return false;
                Rectanglef docBound = this.CurrentSurface.CurrentDocument.Bounds;
                Vector2f dx = Vector2f.Zero;
                ICore2DDrawingLayeredElement v_c = null;
                Vector2f v_ref = Vector2f.Zero;
                v_c = elements[0];
                switch (this.alignment)
                {
                    case enuCore2DSpecialAlignElement.Left:
                        if (elements.Length == 1)
                        {                            
                            dx = CoreMathOperation.GetDistanceP(docBound.Location, v_c.GetBound().Location);
                            if (dx.X != 0.0f)
                                v_c.Translate(dx.X, 0, enuMatrixOrder.Append);
                        }
                        else { 
                            //arrange to all element                         
                            v_ref = v_c.GetBound().Location;
                            for (int i = 1; i < elements .Length; i++)
                            {
                                v_c = elements[i];
                                dx = CoreMathOperation.GetDistanceP(v_ref, v_c.GetBound().Location);
                                if (dx.X != 0.0f)
                                    v_c.Translate(dx.X, 0, enuMatrixOrder.Append);
                            }
                        }
                        break;
                    case enuCore2DSpecialAlignElement.Top:
                        if (elements.Length == 1)
                        {
                            dx = CoreMathOperation.GetDistanceP(docBound.Location, v_c.GetBound().Location);
                            if (dx.Y != 0)
                                v_c.Translate(0, dx.Y, enuMatrixOrder.Append);
                        }
                        else {
                            v_ref = v_c.GetBound().Location;
                            for (int i = 1; i < elements.Length; i++)
                            {
                                v_c = elements[i];
                                dx = CoreMathOperation.GetDistanceP(v_ref, v_c.GetBound().Location);
                                if (dx.Y != 0.0f)
                                    v_c.Translate(0, dx.Y , enuMatrixOrder.Append);
                            }
                        }
                        break;
                    case enuCore2DSpecialAlignElement.Right:
                        if (elements.Length == 1)
                        {
                            dx = CoreMathOperation.GetDistanceP(new Vector2f(docBound.Right, 0),
                                new Vector2f(v_c.GetBound().Right, 0));
                            if (dx.X != 0.0f)
                                v_c.Translate(dx.X, 0, enuMatrixOrder.Append);
                        }
                        else {
                            v_ref = new Vector2f(v_c.GetBound().Right, 0);
                            for (int i = 1; i < elements.Length; i++)
                            {
                                v_c = elements[i];
                                dx = CoreMathOperation.GetDistanceP(v_ref, 
                                    new Vector2f( v_c.GetBound().Right ,0));
                                if (dx.X != 0.0f)
                                    v_c.Translate( dx.X,0, enuMatrixOrder.Append);
                            }
                        }
                        break;
                    case enuCore2DSpecialAlignElement.Bottom:
                        if (elements.Length == 1)
                        {
                            dx = CoreMathOperation.GetDistanceP(new Vector2f(0, docBound.Bottom ),
                                new Vector2f(0, v_c.GetBound().Bottom));
                            if (dx.Y != 0.0f)
                                v_c.Translate( 0, dx.Y , enuMatrixOrder.Append);
                        }
                        else
                        {
                            v_ref = new Vector2f(0, v_c.GetBound().Bottom);
                            for (int i = 1; i < elements.Length; i++)
                            {
                                v_c = elements[i];
                                dx = CoreMathOperation.GetDistanceP(v_ref, new Vector2f(0, v_c.GetBound().Bottom ));
                                if (dx.Y != 0.0f)
                                    v_c.Translate(0, dx.Y, enuMatrixOrder.Append);
                            }
                        }
                        break;
                    default:
                        break;
                }
                this.CurrentSurface.RefreshScene();
                return false;
            }
            public override string ToString()
            {
                return "SAlign : " + this.alignment;
            }
        }
    }
}

