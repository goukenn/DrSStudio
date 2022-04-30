

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _ConvertToMergedPathContextMenu.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.WinCore;
using IGK.ICore;
using IGK.ICore.Drawing2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.ICore.Drawing2D.WorkingObjects.Standard;

namespace IGK.DrSStudio.Drawing2D.ContextMenu
{
    [IGKD2DConvertToContextMenuAttribute("QuadraticPath", 0)]
    class _ConvertToQuadraticPathContextMenu : IGKD2DDrawingContextMenuBase
    {
        protected override bool IsVisible()
        {
            return (this.CurrentSurface !=null);
        }
        protected override bool IsEnabled()
        {
            return this.IsVisible () &&  (this.GetValidElement().Length > 0);
        }
        public _ConvertToQuadraticPathContextMenu()
        {
            this.IsRootMenu = false;
        }
        protected override bool PerformAction()
        {
            ICore2DDrawingLayeredElement[] t = this.GetValidElement();
            if (t.Length > 0)
            {
                if (t.Length == 1)
                {
                    if (t[0] is QuadraticElement)
                        return false;
                }


                ICoreBrushOwner brOwner  = t[0] as ICoreBrushOwner ;

                ICoreBrush v_fillBrush =null;
                ICorePen v_strokeBrush=null;

                if (brOwner != null)
                {
                    v_fillBrush = brOwner.GetBrush(enuBrushMode.Fill);
                    v_strokeBrush = brOwner.GetBrush(enuBrushMode.Stroke) as ICorePen ;
                }


                List<Vector2f> v_points = new List<Vector2f>();
                List<Byte> v_bytes = new List<byte>();
                Vector2f[] v_p = null;
                byte[] v_b = null;
                for (int i = 0; i < t.Length; i++)
                {
                    if (t[i].GetPath().GetAllDefinition(out v_p, out v_b))
                    {
                        v_points.AddRange(v_p);
                        v_bytes.AddRange(v_b);
                    }
                    else
                        return false ;
                }

                QuadraticElement p = QuadraticElement.CreateFromGdiDefinition(v_points.ToArray(), v_bytes.ToArray());
                //create a merged graphics path
                //PathElement p = PathElement.CreateElement(v_points.ToArray (), v_bytes.ToArray());
                if (p != null)
                {
                    this.CurrentSurface.CurrentLayer.Elements.RemoveAll(t);
                    //for (int i = 0; i < t.Length; i++)
                    //{
                    //    this.CurrentSurface.CurrentLayer.Elements.Remove(t[i]);
                    //}
                    if (v_fillBrush !=null)
                    p.FillBrush.Copy(v_fillBrush);
                    if (v_strokeBrush!=null)
                    p.StrokeBrush.Copy(v_strokeBrush);
                    this.CurrentSurface.CurrentLayer.Elements.Add(p);
                    this.CurrentSurface.CurrentLayer.Select(p);
                    return true;
                }
            }
            return false;
        }

        private ICore2DDrawingLayeredElement[] GetValidElement()
        {
            List<ICore2DDrawingLayeredElement> v = new List<ICore2DDrawingLayeredElement>();
            foreach (ICore2DDrawingLayeredElement  item in this.CurrentSurface.CurrentLayer.SelectedElements)
            {
                if (item == null) 
                    continue;
                v.Add(item);
            }
            return v.ToArray ();
        }
    }
}
