

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: TextSegment.cs
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
file:TextSegment.cs
*/
using IGK.ICore;using IGK.ICore.GraphicModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.ICore.Drawing2D.Segments
{
    public class TextSegment : CorePathSegmentBase
    {
        private Vector2f[] m_points;
        private Byte[] m_types;
        private string text;
        private Rectanglef bounds;
        private CoreFont coreFont;
        private enuStringAlignment halignment;
        private enuStringAlignment valignment;
        private bool m_closed;
        private bool m_outLineDefined;
        public string Text { get { return this.text; } }
        public Rectanglef Bounds { get { return this.bounds; } }
        public CoreFont CoreFont { get { return this.coreFont; } }
        public enuStringAlignment HAlignment{get{return this.halignment ;}}
        public enuStringAlignment VAlignment{get{return this.valignment ;}}
        public override void SetPoint(int index, int def)
        {
            this.m_types[index] = (byte)def;
        }
        /// <summary>
        /// get if this text segment contains outline definition
        /// </summary>
        public bool OutlineDefined { get { return this.m_outLineDefined; } }
        public override bool IsEmpty
        {
            get
            {
                return ((this.m_types == null) || (this.m_types.Length == 0));
            }
        }
        private TextSegment() { 
        }

        /// <summary>
        /// create a text segment
        /// </summary>
        /// <param name="text"></param>
        /// <param name="bounds"></param>
        /// <param name="coreFont"></param>
        /// <param name="halignment"></param>
        /// <param name="valignment"></param>
        /// <returns></returns>
        public static TextSegment CreateSegment(string text, Rectanglef bounds, CoreFont coreFont,
            enuStringAlignment halignment, enuStringAlignment valignment)
        {
            ICorePathStringDefinition v_def
                = BuildPath(text, bounds, coreFont, halignment, valignment);
         
                TextSegment o = new TextSegment();
                o.text = text;
                o.bounds = bounds;
                o.coreFont = coreFont;
                o.halignment = halignment;
                o.valignment = valignment;
                if ((v_def != null) && (v_def.Points !=null)&& (v_def.Points.Length > 0))
                {
                    o.m_points = v_def.Points;
                    o.m_types = v_def.PointTypes;
                    o.m_outLineDefined = true;
                }
                return o;
            
            
        }
        private static ICorePathStringDefinition BuildPath(string Text, Rectanglef Bounds, 
            CoreFont coreFont,
            enuStringAlignment halignment, enuStringAlignment valignment)
        {
            var m = CoreApplicationManager.Application.ResourcesManager;
            Debug.Assert(m != null, "ResourcesManager not define");
            ICorePathStringDefinition def = m.CreatePathStringDefinition(
                Text,
                Bounds,
                coreFont,
                halignment,
                valignment);
          
            return def;
        }
        public override enuSegmentType SegmentType
        {
            get { return enuSegmentType.TextPath; }
        }
      
      
        public override Vector2f[] GetPathPoints()
        {
            return this.m_points;
        }
        public override byte[] GetPathTypes()
        {
            return this.m_types;
        }
       
        public override void Invert()
        {
            List<Vector2f> pts = new List<Vector2f> ();
            pts.AddRange (this.GetPathPoints ());
            pts.Reverse ();
            this.m_points = pts.ToArray();
        }

        public override void Transform(Matrix matrix)
        {
             this.m_points = CoreMathOperation.TransformVector2fPoint(matrix, this.GetPathPoints());
        }

        public override void CloseFigure()
        {
            this.m_closed = true;
        }

        public override bool IsClosed
        {
            get { return this.m_closed; }
        }
    }
}

