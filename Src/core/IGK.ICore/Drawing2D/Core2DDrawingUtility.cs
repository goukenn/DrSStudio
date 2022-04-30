

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Core2DDrawingUtility.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.ICore.Drawing2D.WorkingObjects.Standard;

namespace IGK.ICore.Drawing2D
{
    class Core2DDrawingUtility
    {
        public static ICore2DDrawingDocument GetDocument(ICore2DDrawingLayeredElement lElement)
        {
            var v_rc = lElement.GetBound();
            Core2DDrawingLayerDocument v_doc = new Core2DDrawingLayerDocument((CoreUnit)v_rc.Width, (CoreUnit)v_rc.Height);
            v_doc.CurrentLayer.Elements.Add(lElement);
            return v_doc;
        }

        public static bool ReverseSelect(Vector2f point, ICore2DDrawingLayeredElement[] items)
        {
            for (int i = items.Length - 1; i >= 0; i--)
            {
                if (items[i].Contains(point)) { 
                }
            }
            return false;
        }

        internal static bool ReverseSelect(Vector2f point, 
            Core2DDrawingLayer layer, 
            ICore2DDrawingLayeredElement[] elements, 
            List<ICore2DDrawingLayeredElement> outlist, bool deepContainer)
        {
            var h = elements;
            Array.Reverse(h);
            bool ch = false;
            if (deepContainer)
            {
             
                Queue<ICore2DDrawingLayeredElement> c = new Queue<ICore2DDrawingLayeredElement>(h);
                while (c.Count > 0)
                {
                    var bb = c.Dequeue();
                    if (bb is ICore2DDrawingSelectionContainer)
                    {
                        foreach (ICore2DDrawingLayeredElement m in (bb as ICore2DDrawingSelectionContainer).Elements)
                        {
                            if (m != null)
                                c.Enqueue(m);
                        }
                    }
                    else
                    {
                        if (bb.Contains(point))
                        {
                            outlist.Add(bb);
                        }
                    }
                }

            }
            else
            {
                for (int i = elements.Length  - 1; i >= 0; i--)
                {
                    var f = elements[i];
                    if (f.Contains(point))
                        outlist.Add(f);
                }
            }
            if (outlist.Count > 0)
            {
                layer.Select(outlist.ToArray());
                ch = true;
            }
            return ch;
        }

      
        public  static void AlignElementWithCenter(Core2DDrawingLayeredElement e,
            enuCore2DAlignElement alignment, Rectanglef bounds, Vector2f center,
            Rectanglef alignmentBound)
        {
            Matrix m = e.GetMatrix();
            Rectanglef b = alignmentBound;
            Vector2f c = center; //CoreMathOperation.GetCenter(b);
            Vector2f bc = CoreMathOperation.GetCenter(bounds);
            Vector2f p = CoreMathOperation.GetDistanceP(bc, c);
            Rectanglef v_rc = bounds;
            float dx = 0.0f;
            float dy = 0.0f;
            float rx = b.X;
            float ry = b.Y;
            switch (alignment)
            {
                case enuCore2DAlignElement.TopLeft:
                    dx = 0;
                    dy = 0;
                    break;
                case enuCore2DAlignElement.CenterHorizontal:
                    ry = 0;
                    dx = p.X;// (v_rc.Width - b.Width) / 2.0f;
                    e.Translate(dx, dy, enuMatrixOrder.Append, false);
                    return;
                case enuCore2DAlignElement.CenterVertical:
                    rx = 0;
                    dy = p.Y;// (v_rc.Height - b.Height) / 2.0f;
                    e.Translate(dx, dy, enuMatrixOrder.Append, false);
                    return;
                case enuCore2DAlignElement.TopMiddle:
                    dx = (v_rc.Width - b.Width) / 2.0f;
                    break;
                case enuCore2DAlignElement.TopRight:
                    dx = v_rc.Width - b.Width;
                    break;
                case enuCore2DAlignElement.Center:
                    dx = p.X;// (v_rc.Width - b.Width) / 2.0f;
                    dy = p.Y;// (v_rc.Height - b.Height) / 2.0f;
                    e.Translate(dx, dy, enuMatrixOrder.Append, false);
                    return;
                case enuCore2DAlignElement.MiddleLeft:
                    dy = (v_rc.Height - b.Height) / 2.0f;
                    break;
                case enuCore2DAlignElement.MiddleRight:
                    dx = v_rc.Width - b.Width;
                    dy = (v_rc.Height - b.Height) / 2.0f;
                    break;
                case enuCore2DAlignElement.BottomLeft:
                    dy = v_rc.Height - b.Height;
                    break;
                case enuCore2DAlignElement.BottomRight:
                    dy = v_rc.Height - b.Height;
                    dx = v_rc.Width - b.Width;
                    break;
                case enuCore2DAlignElement.BottomMiddle:
                    dx = (v_rc.Width - b.Width) / 2.0f;
                    dy = v_rc.Height - b.Height;
                    break;
            }
#pragma warning disable IDE0054 // Use compound assignment
            dx = dx - rx;
#pragma warning restore IDE0054 // Use compound assignment
#pragma warning disable IDE0054 // Use compound assignment
            dy = dy - ry;
#pragma warning restore IDE0054 // Use compound assignment
            e.Translate(dx, dy, enuMatrixOrder.Append, false);
        }
    }
}
