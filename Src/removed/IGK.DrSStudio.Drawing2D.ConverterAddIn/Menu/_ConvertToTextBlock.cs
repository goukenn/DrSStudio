

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _ConvertToTextBlock.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV - [2008 - 2014]
author: C.A.D . BONDJE DOUE
file:_ConvertToTextBlock.cs
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
using System.Text.RegularExpressions ;
using System.Drawing;
using System.Drawing.Drawing2D;
namespace IGK.DrSStudio.Drawing2D.Menu
{
    using IGK.ICore;using IGK.DrSStudio.Menu;
    public enum enuPathConvertType
    { 
        Char ,
        Word,
        Line
    }
    [CoreMenu(ConverterConstant.MENU_CONVERTTO_TEXTBLOCK, 1)]
    class _ConvertToTextBlock :  Core2DMenuBase , IGK.DrSStudio.WinUI.Configuration .ICoreWorkingConfigurableObject 
    {
        private enuPathConvertType m_ConvertType;
        /// <summary>
        /// get or set the path convert type
        /// </summary>
        public enuPathConvertType ConvertType
        {
            get { return m_ConvertType; }
            set
            {
                if (m_ConvertType != value)
                {
                    m_ConvertType = value;
                }
            }
        }
        protected override void InitMenu()
        {
            base.InitMenu();
            this.m_ConvertType = enuPathConvertType.Char;
        }
        protected override bool PerformAction()
        {
            ICore2DDrawingLayeredElement[] l = this.CurrentSurface.CurrentLayer.SelectedElements.ToArray();
            ICore2DDrawingLayeredElement[] tab = null;
            if ((l.Length == 1) && (l[0] is TextElement))
            {
                TextElement c = l[0] as TextElement;
                if (this.Workbench.ConfigureWorkingObject(this) == System.Windows.Forms.DialogResult.OK)
                {
                    switch (this.m_ConvertType)
                    {
                        case enuPathConvertType.Char:
                            {
                                tab = CreateElement(c.Content,
                                      c.Font.FontName, c.Font.FontSize, c.Font.FontStyle, c.GetBound().Location);
                            }
                            break;
                        case enuPathConvertType.Word:
                            {
                                tab = CreateWordElement(c.Content,
                                  c.Font.FontName, c.Font.FontSize, c.Font.FontStyle, c.GetBound().Location);
                            }
                            break;
                        case enuPathConvertType.Line :
                             tab = CreateLineElement(c.Content,
                                  c.Font.FontName, c.Font.FontSize, c.Font.FontStyle, c.GetBound().Location);
                            break ;
                    }
                    if ((tab != null) && (tab.Length > 0))
                    {
                        this.CurrentSurface.CurrentLayer.Select(null);
                        this.CurrentSurface.CurrentLayer.Elements.Remove(l);
                        this.CurrentSurface.CurrentLayer.Elements.AddRange(tab);
                        this.CurrentSurface.Invalidate();
                    }
                }
            }
            return false;
        }
        private ICore2DDrawingLayeredElement[] CreateLineElement(string text, string fontName, float fontSize,
           FontStyle fontStyle,
           Vector2f Location)
        {
         if (string.IsNullOrEmpty (text ))
             return null;
            ICore2DDrawingLayeredElement[] tb =null;
            float x = Location.X;
            float y = Location.Y;
            string[] line = text.Split('\n');
            int c = 0;
            float h = 0.0f;
            GraphicsPath v_p = null;
            PathElement v_lc = null;
            FontFamily fm = new FontFamily(fontName);
            StringFormat sf = new StringFormat();
             tb = new ICore2DDrawingLayeredElement[line.Length];
            //empty width
            v_p = new GraphicsPath();
            v_p.AddString("_",
                fm, (int)fontSize, fontSize, new Vector2f(x, y), sf);
            float v_emptyWidth = v_p.GetBounds().Width;
            v_p.Dispose();
            int i = 0;
            foreach (string st in line)
            {
                    v_p = new GraphicsPath();
                    v_p.AddString(st,
                        fm, (int)fontStyle, fontSize, new Vector2f(x, y), sf);
                    v_lc = PathElement.Create(v_p);
                    tb[i + c] = v_lc;                    
                    if (i == 0)
                    {
                        h = Math.Max(h, tb[0].GetBound().Height);
                    }
                    i++;
                x = Location.X;
                y += h;
            }
            fm.Dispose();
            sf.Dispose();
            return tb;
        }
        private ICore2DDrawingLayeredElement[] CreateWordElement(string text, string fontName, float fontSize,
            FontStyle fontStyle,
            Vector2f Location)
        {
            Regex v_rg = new Regex(@"\w+");
            MatchCollection v_col =  v_rg.Matches(text);
            if (v_col.Count == 0)
                return null;
            ICore2DDrawingLayeredElement[] tb = new ICore2DDrawingLayeredElement[v_col.Count];
            float x = Location.X;
            float y = Location.Y;
            string[] line = text.Split('\n');
            int c = 0;
            float h = 0.0f;
            GraphicsPath v_p = null;
            PathElement v_lc = null;
            FontFamily fm = new FontFamily(fontName);
            StringFormat sf = new StringFormat();
            //empty width
            v_p = new GraphicsPath();
            v_p.AddString("_",
                fm, (int)fontSize, fontSize, new Vector2f(x, y), sf);
            float v_emptyWidth = v_p.GetBounds().Width;
            v_p.Dispose();
            int i = 0;
            foreach (string st in line)
            {
                v_col = v_rg.Matches(st);
                foreach (Match  item in v_col)
                {
                           v_p = new GraphicsPath();
                      v_p.AddString(item.Value,
                          fm, (int)fontStyle , fontSize, new Vector2f(x, y), sf);
                    v_lc = PathElement.Create(v_p);
                    tb[i + c] = v_lc;
                    x += v_lc.GetBound().Width +v_emptyWidth;
                    if (i == 0)
                    {
                        h = Math.Max(h, tb[0].GetBound().Height);
                    }
                    i++;
                }
                x = Location.X;
                //for (int i = 0; i < st.Length; i++)
                //{
                //    if (st[i] != ' ')
                //    {
                //        v_p = new GraphicsPath();
                //        v_p.AddString(st[i].ToString(),
                //            fm, (int)fstyle, fontSize, new Vector2f(x, y), sf);
                //        v_lc = PathElement.Create(v_p);
                //        tb[i + c] = v_lc;
                //        x += tb[i + c].GetBound().Width;
                //        h = Math.Max(h, tb[i + c].GetBound().Height);
                //    }
                //    else
                //    {
                //        x += v_emptyWidth;
                //    }
                //}
                x = Location.X;
                y += h;
            }
            fm.Dispose();
            sf.Dispose();
            return tb;
        }
        public static ICore2DDrawingLayeredElement[] CreateElement(string Text,
            string fontName, 
            float fontSize, FontStyle fstyle, Vector2f Location)
        {
            if (Text.Length <= 0) return null;
            ICore2DDrawingLayeredElement[] tb = new ICore2DDrawingLayeredElement[Text.Length];
            float x = Location.X;
            float y = Location.Y;
            string[] line = Text.Split('\n');
            int c = 0;
            float h = 0.0f;
            GraphicsPath v_p = null;
            PathElement v_lc = null;
            FontFamily fm = new FontFamily (fontName );
            StringFormat sf = new StringFormat();
            //empty width
            v_p = new GraphicsPath();
            v_p.AddString("_",
                fm, (int)fstyle, fontSize, new Vector2f(x, y), sf);
            float v_emptyWidth = v_p.GetBounds().Width;
            v_p.Dispose();
            foreach (string st in line)
            {
                for (int i = 0; i < st.Length; i++)
                {
                    if (st[i] != ' ')
                    {
                        v_p = new GraphicsPath();
                        v_p.AddString (st[i].ToString(),
                            fm, (int)fstyle ,fontSize  ,new Vector2f (x,y), sf );
                        v_lc  = PathElement .Create (v_p );
                        tb[i + c] = v_lc ;
                        x += tb[i + c].GetBound ().Width;
                        h = Math.Max(h, tb[i + c].GetBound().Height);
                    }
                    else
                    {
                        x += v_emptyWidth;
                    }
                }
                c += st.Length;
                x = Location.X;
                y += h;
            }
            fm.Dispose();
            sf.Dispose();
            return tb;
        }
        public DrSStudio.WinUI.Configuration.enuParamConfigType GetConfigType()
        {
            return DrSStudio.WinUI.Configuration.enuParamConfigType.ParameterConfig;
        }
        public DrSStudio.WinUI.ICoreParameterConfigCollections GetParameters(DrSStudio.WinUI.ICoreParameterConfigCollections parameters)
        {
            parameters.AddGroup(CoreConstant.PARAM_DEFINITION).AddItem(GetType().GetProperty("ConvertType"));
            return parameters;
        }
        public System.Windows.Forms.Control GetConfigControl()
        {
            throw new NotImplementedException();
        }
    }
}

