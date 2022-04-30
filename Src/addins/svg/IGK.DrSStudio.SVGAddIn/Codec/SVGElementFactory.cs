
using IGK.ICore;
using IGK.ICore.Drawing2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace IGK.DrSStudio.SVGAddIn
{
    /// <summary>
    /// represent class used to create object
    /// </summary>
    public class SVGElementFactory
    {
        /// <summary>
        /// object creator factory
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public ICoreWorkingObject Create(string name)
        {
            CoreLog.WriteDebug("SVG", $"Create {name}");
            var v_method = MethodInfo.GetCurrentMethod().DeclaringType.GetMethod("Create" + name,
                BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic| BindingFlags.Instance | BindingFlags.IgnoreCase);
            if (v_method != null)
                return v_method.Invoke(this,null) as ICoreWorkingObject;
            return null;
        }

        public ICoreWorkingObject CreateDefs() {
            return new SVGDefElement();
        }
        public ICoreWorkingObject CreateRect()
        {
            return new RectangleElement();
        }
        public ICoreWorkingObject CreateG()
        {
            // return new Core2DDrawingLayer();
            return new SVGGroupElement();// new SVGGElement();
        }
        public ICoreWorkingObject CreatePath() {
            return new PathElement();
        }
        public ICoreWorkingObject CreatePolygon() {
            return new CustomPolygonElement();
            //return new SVGPolygonElement();
        }
        public ICoreWorkingObject CreateCircle() {
            return new CircleElement();
        }
        public ICoreWorkingObject CreateClipPath() {
            return new SvgClipPathElement();
        }

    }
}
