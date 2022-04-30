

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CodeBar39Element.cs
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
file:CodeBar39Element.cs
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
    
using IGK.ICore.Drawing2D;
    using IGK.ICore.Drawing2D.WinUI;
    using IGK.ICore.WinUI.Configuration;
    using IGK.ICore.Codec;
    using IGK.ICore;
    using IGK.ICore.WinUI;
    
    /// <summary>
    /// Code39 Element
    /// </summary>
    [CodeBarCategory("Code39Bar", typeof(Mecanism), ImageKey = CodeBarConstant.DE_CODEBAR)]
    public sealed class CodeBar39Element :
        CodeBarElementBase,
        ICoreCodeBarElement ,
        ICodeBar,
        ICoreTextElement
    {
     
        
        public CodeBar39Element():base()
        {

        }

        protected override void InitializeElement()
        {
            base.InitializeElement();
        }
     
      
        [CoreXMLAttribute()]
        [CoreXMLDefaultAttributeValue(enuCodeBarModel.EAN_8)]
        /// <summary>
        /// get the code bar model
        /// </summary>
        public enuCodeBarModel Model
        {
            get { return enuCodeBarModel.Code39; }
        }
      
        private int GetMaxSegment()
        {
            //for code 39
            if (!string.IsNullOrEmpty (this.Text ))
                return  (this.Text.Length + 2);
            return 0;
        }
        public override ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
        {
            ICoreParameterConfigCollections p = base.GetParameters(parameters);
            ICoreParameterGroup group = p.AddGroup("Definition");
            group.AddItem(GetType().GetProperty("Value"));
            group.AddItem(GetType().GetProperty("ShowValue"));
           
            return p;
        }
        protected override void InitGraphicPath(CoreGraphicsPath p)
        {
            p.Reset();
            
            if (string.IsNullOrEmpty(this.Text))            
            {                
                p.AddRectangle(this.Bounds);
                return;
            }
            CoreGraphicsPath v_path = new CoreGraphicsPath();
            string V = this.Text;
            float x = this.Bounds.X;
            float w = this.Bounds.Width;
            float y = this.Bounds.Y;
            float h = this.Bounds.Height;
            float n = GetMaxSegment();
            float v_lineH = this.Font.GetLineHeight();
            float v_intBar = ShowValue ? v_lineH : 0;
            float nbrl = (n- 1) + 6 * (n);
            float nbrL = 3 * ( n);
            float maxw = w / (float)(nbrl * 0.5f + nbrL);
            float minw = maxw * 0.5f;
            v_path.SetMarkers();
            //draw begin
            AddInsert(v_path,Core39Manager.GetValue('*'), minw ,maxw , 0, x, y, h);
            for (int s = 0; s < V.Length; s++)
            {
                AddInsert(v_path,
                    Core39Manager.GetValue(V[s]),
                    minw, maxw,
                    1 + s,
                    x,
                    y,
                    h - v_intBar);
            }
            //draw end
            AddInsert(v_path, Core39Manager.GetValue('*'), minw, maxw, (int)n - 1, x , y, h);
            //first part
            //add string value

            p.Add(v_path);

            if (this.ShowValue)
            {
                p.AddText(
               this.Text,
               new Rectanglef(x, Bounds.Bottom - v_lineH, w, v_lineH),
               this.Font );

                //Font ft = this.Font.GetFont();
                //StringFormat sf = new StringFormat();
                //sf.Alignment = StringAlignment.Center;
                //sf.LineAlignment = StringAlignment.Center;
               // v_path.SetMarkers();
                //v_path.AddString(V, ft.FontFamily,
                //    (int)Font.FontStyle,
                //    Font.FontSize,
                //    new Rectanglef(x, Bounds.Bottom - v_lineH, w, v_lineH),
                //    sf);               
            }
        }
        private void AddInsert(
            CoreGraphicsPath path, 
            string pattern, 
            float minw , float maxw ,
            int startIndex, float xoffset,
            float yoffset,
            float height)
        {
            if (string.IsNullOrEmpty(pattern))
                return;
            float h = xoffset + startIndex * ( 7 * minw + 3 * maxw);
            for (int i = 0; i < pattern.Length; i++)
            {
                if (pattern[i] == '0')
                {
                    if ((i % 2 == 0))
                        path.AddRectangle(
                            new Rectanglef(
                                h,
                                yoffset,
                                minw,
                               height));
                    h += minw;
                }
                else
                { // case 1
                    if ((i%2==0))
                    path.AddRectangle(
                        new Rectanglef(
                            h,
                            yoffset,
                            maxw ,
                           height));
                    h += maxw;
                }
            }
        }
        private Rectanglef GetSecondBound(float segmentSize)
        {
            Rectanglef rc = new Rectanglef();
            float v_lineh = Font.GetLineHeight();
            rc.Y = this.Bounds.Bottom - v_lineh;
            rc.X = this.Bounds.Right - segmentSize * (3 + 7 * 6);
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
        }
    }
}

