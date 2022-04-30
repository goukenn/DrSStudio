

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CodeBarENA13Element.cs
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
file:CodeBarENA13Element.cs
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
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using System.Text.RegularExpressions;
namespace IGK.DrSStudio.Drawing2D
{
    
using IGK.ICore; using IGK.ICore.Drawing2D; using IGK.DrSStudio.Drawing2D;
    using IGK.ICore.Drawing2D.WinUI ;
    using IGK.ICore.WinUI.Configuration;
    using IGK.ICore.Codec;
    using IGK.ICore.WinUI;
    
    [CodeBarCategory ("CodeENA13", typeof (Mecanism), ImageKey = CodeBarConstant.DE_CODEBAR)]
    public class CodeBarENA13Element : 
        CodeBarElementBase,//RectangleElement ,
        ICoreCodeBarElement ,
        ICodeBar,
        ICoreTextElement ,
        ICore2DDrawingVisitable 
    {
        private bool m_DrawBorder;

        [CoreXMLAttribute()]
        [CoreXMLDefaultAttributeValue(false)]
        public bool DrawBorder
        {
            get { return m_DrawBorder; }
            set
            {
                if (m_DrawBorder != value)
                {
                    m_DrawBorder = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
     


       
        public CodeBarENA13Element():base()
        {
        }
        
        protected override void InitializeElement()
        {
            base.InitializeElement();
            this.Text = "0000000000000";
        }
      
        [CoreXMLAttribute()]
        [CoreXMLDefaultAttributeValue ( enuCodeBarModel.EAN_8 )]
        /// <summary>
        /// get the code bar model
        /// </summary>
        public enuCodeBarModel Model
        {
            get { return enuCodeBarModel.EAN_13 ; }
        }
   
        private string TreatValue(string value)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;
            Regex rg = new Regex("[^0-9]");
            if (rg.IsMatch(value))
                return string.Empty;
            string V = value ;
            if (V.Length < 12)
            {
                V = V.PadRight(12, '0');
            }
            else if (V.Length > 12)
            {
                V = V.Substring(0, 12);
            }
            return V ;
        }
        /// <summary>
        /// get maximum segment size
        /// </summary>
        /// <returns></returns>
        protected int GetMaxSegment()
        {
            //for ENA 13
            return 12 * 7 + 3 * 2 + 5;
        }
        public override ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
        {
            ICoreParameterConfigCollections p = base.GetParameters(parameters);
            ICoreParameterGroup group = p.AddGroup("Definition");
            group.AddItem(GetType().GetProperty("ShowValue"));
            group.AddItem(GetType().GetProperty("DrawBorder"));      
            return p;
        }

        public override Rectanglef GetBound()
        {
            Rectanglef rc =  base.GetBound();
            if (this.ShowValue)
            {
               // rc.Width += 2 * this.getOffsetx();
            }
            return rc;
        }
        private float getOffsetx()
        {
            int v_x_offset = ShowValue ? (int)Math.Ceiling(
    CoreGraphicsPath.MeasureString("_", this.Font).Width) : 0;
            return v_x_offset; 
        }
        protected override void InitGraphicPath(CoreGraphicsPath v_path)
        {
            v_path.Reset();
            string V = this.Text;
            if (string.IsNullOrEmpty(V))
                return;
            int v_x_offset = ShowValue ?(int) getOffsetx() : 0;

            if (V.Length < 12)
            {
                V  = V.PadRight(12, '0');
            }
            else if(V.Length > 12)
            {
                V = V.Substring (0,12);
            }
            V = CodeBarENA13Manager.GetCheckCode(V);
            if (V == null) {
                return;
            }
            var v_rbound = this.Bounds;
            float x = v_rbound.X;
            float w = v_rbound.Width-v_x_offset;
            float y = v_rbound.Y;
            float h = v_rbound.Height;
            float n = GetMaxSegment();
            if (n == 0)
                return;
            float v_segmentSize = w / n;
            //v_path.SetMarkers();
        
            //draw begin
            AddInsert(v_path , "101", v_segmentSize , 0, x+v_x_offset   ,y, h);
            float v_lineH = ShowValue? this.Font.GetLineHeight() : 0;
            string[] v_tab = new string[3];
            v_tab[0] = V[0].ToString();
            v_tab[1] = V.Substring(1, 6);
            v_tab[2] = V.Substring(7);
            string v_sequence = CodeBarENA13Manager.GetSequence(Int32.Parse(v_tab[0]));
            string v_stype = "A";
            float v_intBar = ShowValue ? v_lineH : 0;
            //first part
                for (int s = 0; s < v_tab[1].Length; s++)
                {
                    v_stype = v_sequence[s].ToString().ToUpper ();
                    AddInsert(v_path,
                        CodeBarENA13Manager.GetElement (v_stype ,Int32. Parse(v_tab[1][s].ToString()))
                        ,
                        v_segmentSize, 3 + 7 * s, x+v_x_offset  , y, h - v_intBar);
                }
            //insert middle
            AddInsert(v_path, "01010", v_segmentSize, 3 + 7 * 6, x+v_x_offset  , y, h);
            //second part
            for (int s = 0; s < v_tab[2].Length; s++)
            {                
                AddInsert(v_path,
                    CodeBarENA13Manager.GetElement("C", Int32.Parse(v_tab[2][s].ToString()))
                    ,
                    v_segmentSize, 3 + 7 * 6 + 5 + 7 * s, x + v_x_offset, y, h - v_intBar);
            }
            //build end
           // v_path.SetMarkers();
            //v_sbound = new Rectanglef(x+w - 3 * v_segmentSize , y,
            //            3 * v_segmentSize ,
            //            h);
            AddInsert(v_path, "101", v_segmentSize, (int)n - 3, x + v_x_offset, y, h);
            //Font ft = this.Font.GetFont();
            //StringFormat sf = new StringFormat  ();
            //sf.Alignment = StringAlignment.Center;
            //sf.LineAlignment = StringAlignment.Center;
            //add string value
            if (this.ShowValue)
            {
                string v_str = v_tab[0];
                v_path.AddText(
        v_str,
        new Rectanglef(x, Bounds.Bottom - v_lineH, v_x_offset , v_lineH),
        this.Font);
                v_str = v_tab[1];
                v_path.AddText(
            v_str,
           GetFirstBound(v_segmentSize),
            this.Font);

                v_str = v_tab[2];
                v_path.AddText(
             v_str,
             GetSecondBound(v_segmentSize),
             this.Font);
            }
        }
        private void AddInsert(CoreGraphicsPath path, string pattern, float v_segmentSize ,
            int startIndex, float xoffset, 
            float yoffset,
            float height)
        {
            for (int i = 0; i < pattern.Length; i++)
            {
                if (pattern[i] == '0')
                    continue;
                path.AddRectangle(
                    new Rectanglef(
                        xoffset  +  (startIndex + i) * v_segmentSize,
                        yoffset,
                        v_segmentSize,
                       height ));
            }
        }
        private Rectanglef GetSecondBound(float segmentSize)
        {
            Rectanglef rc = new Rectanglef();
            float v_lineh = Font.GetLineHeight();
            rc.Y = this.Bounds.Bottom - v_lineh;
            rc.X = this.Bounds.Right - segmentSize*(3 + 7 * 6);
            rc.Height = v_lineh;
            rc.Width = 7 * segmentSize * 6; ;
            return rc;
        }
        private Rectanglef GetFirstBound(float segmentSize)
        {
            Rectanglef rc = new Rectanglef();
            float v_lineh = Font.GetLineHeight();
            rc.Y = this.Bounds.Bottom - v_lineh;
            rc.X = this.Bounds.Right - 2 * segmentSize * (3 + 7 * 6);
            rc.Height = v_lineh;
            rc.Width = 7 * segmentSize * 6;
            return rc;
        }
       
        new class Mecanism : RectangleElement.Mecanism 
        {
            protected override void InitNewCreatedElement(RectangleElement element, Vector2f defPoint)
            {
                base.InitNewCreatedElement(element, defPoint );            
                CodeBarENA13Element m = element as CodeBarENA13Element;
                m.FillBrush.SetSolidColor(Colorf.Black);
            }
        }

        public bool Accept(ICore2DDrawingVisitor visitor)
        {
            return visitor != null;
        }

        public void Visit(ICore2DDrawingVisitor visitor)
        {
            if (visitor == null)
                return;
            if (this.m_DrawBorder)
            {
                var  b = this.GetBound();
                visitor.DrawRectangle(
                    this.StrokeBrush, b.X, b.Y, b.Width, b.Height);
            }
            visitor.Visit(this, typeof(RectangleElement));
        }
    }
}

