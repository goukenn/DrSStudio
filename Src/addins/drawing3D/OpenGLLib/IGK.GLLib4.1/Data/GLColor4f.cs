

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GLColor4f.cs
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
file:GLColor4f.cs
*/
/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igkdev.be
Application : DrSStudio
powered by IGK - DEV &copy; 2008-2011
THIS FILE IS A PART OF THE DRSSTUDIO APPLICATION. SEE "License.txt"
FOR MORE INFORMATION ABOUT THE LICENSE
------------------------------------------------------------------- 
*/
/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igk-dev.be
App : DrSStudio
powered by IGK - DEV 2008-2011
------------------------------------------------------------------- 
*/
/* This file is part of iGK-DrawingSoft.
*    iGK-DrawingSoft is free software: you can redistribute it and/or modify
*    it under the terms of the GNU Lesser General Public License as published by
*    the Free Software Foundation, either version 3 of the License, or
*    (at your option) any later version.
*    iGK-DrawingSoft is distributed in the hope that it will be useful,
*    but WITHOUT ANY WARRANTY; without even the implied warranty of
*    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
*    GNU General Public License for more details.
*    You should have received a copy of the GNU Lesser General Public License
*    along with IGK-DRAWING SOFT.  If not, see <http://www.gnu.org/licenses/>.
*
*    Copyright (c) 2008-2009 
*    Author : C.A.D. BONDJE DOUE
*    mail : bondje.doue@hotmail.com
*/
/*
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * */
using System;
using System.Drawing;
using System.ComponentModel ;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices ;
namespace IGK.GLLib
{
    //couleur � virgule floatante
    [TypeConverter(typeof(GLColor4f.GLTypeConverter ))]
    [StructLayout(LayoutKind.Sequential )]
    public struct GLColor4f
    {
        float m_r, m_g, m_b, m_a;
        string m_colorName;
        public float R{get{return m_r;}set{m_r = value;}}
        public float G{get{return m_g;}set{m_g = value;}}
        public float B{get{return m_b;}set{m_b = value;}}
        public float A{get{return m_a;}set{m_a = value;}}
        public static implicit operator float[](GLColor4f color){
            return new float[] { 
                color.R,
                color.G ,
                color.B ,
                color.A 
            };
        }
        public static implicit operator vect4f(GLColor4f color)
        {
            vect4f c = new vect4f();
            c.X = color.R;
            c.Y = color.G;
            c.Z = color.B;
            c.Q = color.A;
            return c;
        }
        public GLColor4f(float r, float g, float b, float a)
        {
            m_r = r;
            m_g = g;
            m_b = b;
            m_a = a;
            m_colorName = string.Empty;
        }
        private GLColor4f(float r, float g, float b)
        {
            m_r = r;
            m_g = g;
            m_b = b;
            m_a = 1.0f;
            m_colorName = string.Empty;
        }
        public static implicit operator GLColor4f(Color c)
        {
            return new GLColor4f(c.R / 255.0f,
                c.G / 255.0f,
                c.B / 255.0f,
                c.A / 255.0f);
        }
        public static implicit operator Color(GLColor4f cf)
        {
            return Color.FromArgb(
                (int)(cf.A * 255.0f),
                (int)(cf.R * 255.0f),
                (int)(cf.G * 255.0f),
                (int)(cf.B * 255.0f)
                );
        }
        public static implicit operator GLColor4f (string valuename)
        {
            System.Reflection .PropertyInfo v_pr = typeof(GLColor4f).GetProperty (valuename,
                 System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags .Public)
                ;
            GLColor4f v = new GLColor4f();
            if (v_pr!=null)
            {
                v = (GLColor4f)v_pr.GetValue(null, null);
                v.m_colorName = valuename;
                return v;
            }
            Color cl = Color.FromName (valuename);
            v = GetColor(cl.R, cl.G, cl.B);
            v.m_colorName = valuename;            
            return v;
        }
        public override string ToString()
        {
            if (!string.IsNullOrEmpty(this.m_colorName))
            {
                return this.m_colorName;
            }
            return string.Format ("{0}:{1}:{2}:{3}",R,G,B,A);
        }
        /// <summary>
        /// get the color name
        /// </summary>
        public string ColorName { 
            get{
                return this.m_colorName;
            }
        }
        private static GLColor4f GetColor(int r, int g, int b) { 
            return new GLColor4f(r / 255.0f, g / 255.0f, b / 255.0f);
        }
        private static readonly GLColor4f transparent = GetColor(255, 255, 255);
        private static readonly GLColor4f aliceblue = GetColor(240, 248, 255);
        private static readonly GLColor4f antiquewhite = GetColor(250, 235, 215);
        private static readonly GLColor4f aqua = GetColor(0, 255, 255);
        private static readonly GLColor4f aquamarine = GetColor(127, 255, 212);
        private static readonly GLColor4f azure = GetColor(240, 255, 255);
        private static readonly GLColor4f beige = GetColor(245, 245, 220);
        private static readonly GLColor4f bisque = GetColor(255, 228, 196);
        private static readonly GLColor4f black = GetColor(0, 0, 0);
        private static readonly GLColor4f blanchedalmond = GetColor(255, 235, 205);
        private static readonly GLColor4f blue = GetColor(0, 0, 255);
        private static readonly GLColor4f blueviolet = GetColor(138, 43, 226);
        private static readonly GLColor4f brown = GetColor(165, 42, 42);
        private static readonly GLColor4f burlywood = GetColor(222, 184, 135);
        private static readonly GLColor4f cadetblue = GetColor(95, 158, 160);
        private static readonly GLColor4f chartreuse = GetColor(127, 255, 0);
        private static readonly GLColor4f chocolate = GetColor(210, 105, 30);
        private static readonly GLColor4f coral = GetColor(255, 127, 80);
        private static readonly GLColor4f cornflowerblue = GetColor(100, 149, 237);
        private static readonly GLColor4f cornsilk = GetColor(255, 248, 220);
        private static readonly GLColor4f crimson = GetColor(220, 20, 60);
        private static readonly GLColor4f cyan = GetColor(0, 255, 255);
        private static readonly GLColor4f darkblue = GetColor(0, 0, 139);
        private static readonly GLColor4f darkcyan = GetColor(0, 139, 139);
        private static readonly GLColor4f darkgoldenrod = GetColor(184, 134, 11);
        private static readonly GLColor4f darkgray = GetColor(169, 169, 169);
        private static readonly GLColor4f darkgreen = GetColor(0, 100, 0);
        private static readonly GLColor4f darkkhaki = GetColor(189, 183, 107);
        private static readonly GLColor4f darkmagenta = GetColor(139, 0, 139);
        private static readonly GLColor4f darkolivegreen = GetColor(85, 107, 47);
        private static readonly GLColor4f darkorange = GetColor(255, 140, 0);
        private static readonly GLColor4f darkorchid = GetColor(153, 50, 204);
        private static readonly GLColor4f darkred = GetColor(139, 0, 0);
        private static readonly GLColor4f darksalmon = GetColor(233, 150, 122);
        private static readonly GLColor4f darkseagreen = GetColor(143, 188, 139);
        private static readonly GLColor4f darkslateblue = GetColor(72, 61, 139);
        private static readonly GLColor4f darkslategray = GetColor(47, 79, 79);
        private static readonly GLColor4f darkturquoise = GetColor(0, 206, 209);
        private static readonly GLColor4f darkviolet = GetColor(148, 0, 211);
        private static readonly GLColor4f deeppink = GetColor(255, 20, 147);
        private static readonly GLColor4f deepskyblue = GetColor(0, 191, 255);
        private static readonly GLColor4f dimgray = GetColor(105, 105, 105);
        private static readonly GLColor4f dodgerblue = GetColor(30, 144, 255);
        private static readonly GLColor4f firebrick = GetColor(178, 34, 34);
        private static readonly GLColor4f floralwhite = GetColor(255, 250, 240);
        private static readonly GLColor4f forestgreen = GetColor(34, 139, 34);
        private static readonly GLColor4f fuchsia = GetColor(255, 0, 255);
        private static readonly GLColor4f gainsboro = GetColor(220, 220, 220);
        private static readonly GLColor4f ghostwhite = GetColor(248, 248, 255);
        private static readonly GLColor4f gold = GetColor(255, 215, 0);
        private static readonly GLColor4f goldenrod = GetColor(218, 165, 32);
        private static readonly GLColor4f gray = GetColor(128, 128, 128);
        private static readonly GLColor4f green = GetColor(0, 128, 0);
        private static readonly GLColor4f greenyellow = GetColor(173, 255, 47);
        private static readonly GLColor4f honeydew = GetColor(240, 255, 240);
        private static readonly GLColor4f hotpink = GetColor(255, 105, 180);
        private static readonly GLColor4f indianred = GetColor(205, 92, 92);
        private static readonly GLColor4f indigo = GetColor(75, 0, 130);
        private static readonly GLColor4f ivory = GetColor(255, 255, 240);
        private static readonly GLColor4f khaki = GetColor(240, 230, 140);
        private static readonly GLColor4f lavender = GetColor(230, 230, 250);
        private static readonly GLColor4f lavenderblush = GetColor(255, 240, 245);
        private static readonly GLColor4f lawngreen = GetColor(124, 252, 0);
        private static readonly GLColor4f lemonchiffon = GetColor(255, 250, 205);
        private static readonly GLColor4f lightblue = GetColor(173, 216, 230);
        private static readonly GLColor4f lightcoral = GetColor(240, 128, 128);
        private static readonly GLColor4f lightcyan = GetColor(224, 255, 255);
        private static readonly GLColor4f lightgoldenrodyellow = GetColor(250, 250, 210);
        private static readonly GLColor4f lightgray = GetColor(211, 211, 211);
        private static readonly GLColor4f lightgreen = GetColor(144, 238, 144);
        private static readonly GLColor4f lightpink = GetColor(255, 182, 193);
        private static readonly GLColor4f lightsalmon = GetColor(255, 160, 122);
        private static readonly GLColor4f lightseagreen = GetColor(32, 178, 170);
        private static readonly GLColor4f lightskyblue = GetColor(135, 206, 250);
        private static readonly GLColor4f lightslategray = GetColor(119, 136, 153);
        private static readonly GLColor4f lightsteelblue = GetColor(176, 196, 222);
        private static readonly GLColor4f lightyellow = GetColor(255, 255, 224);
        private static readonly GLColor4f lime = GetColor(0, 255, 0);
        private static readonly GLColor4f limegreen = GetColor(50, 205, 50);
        private static readonly GLColor4f linen = GetColor(250, 240, 230);
        private static readonly GLColor4f magenta = GetColor(255, 0, 255);
        private static readonly GLColor4f maroon = GetColor(128, 0, 0);
        private static readonly GLColor4f mediumaquamarine = GetColor(102, 205, 170);
        private static readonly GLColor4f mediumblue = GetColor(0, 0, 205);
        private static readonly GLColor4f mediumorchid = GetColor(186, 85, 211);
        private static readonly GLColor4f mediumpurple = GetColor(147, 112, 219);
        private static readonly GLColor4f mediumseagreen = GetColor(60, 179, 113);
        private static readonly GLColor4f mediumslateblue = GetColor(123, 104, 238);
        private static readonly GLColor4f mediumspringgreen = GetColor(0, 250, 154);
        private static readonly GLColor4f mediumturquoise = GetColor(72, 209, 204);
        private static readonly GLColor4f mediumvioletred = GetColor(199, 21, 133);
        private static readonly GLColor4f midnightblue = GetColor(25, 25, 112);
        private static readonly GLColor4f mintcream = GetColor(245, 255, 250);
        private static readonly GLColor4f mistyrose = GetColor(255, 228, 225);
        private static readonly GLColor4f moccasin = GetColor(255, 228, 181);
        private static readonly GLColor4f navajowhite = GetColor(255, 222, 173);
        private static readonly GLColor4f navy = GetColor(0, 0, 128);
        private static readonly GLColor4f oldlace = GetColor(253, 245, 230);
        private static readonly GLColor4f olive = GetColor(128, 128, 0);
        private static readonly GLColor4f olivedrab = GetColor(107, 142, 35);
        private static readonly GLColor4f orange = GetColor(255, 165, 0);
        private static readonly GLColor4f orangered = GetColor(255, 69, 0);
        private static readonly GLColor4f orchid = GetColor(218, 112, 214);
        private static readonly GLColor4f palegoldenrod = GetColor(238, 232, 170);
        private static readonly GLColor4f palegreen = GetColor(152, 251, 152);
        private static readonly GLColor4f paleturquoise = GetColor(175, 238, 238);
        private static readonly GLColor4f palevioletred = GetColor(219, 112, 147);
        private static readonly GLColor4f papayawhip = GetColor(255, 239, 213);
        private static readonly GLColor4f peachpuff = GetColor(255, 218, 185);
        private static readonly GLColor4f peru = GetColor(205, 133, 63);
        private static readonly GLColor4f pink = GetColor(255, 192, 203);
        private static readonly GLColor4f plum = GetColor(221, 160, 221);
        private static readonly GLColor4f powderblue = GetColor(176, 224, 230);
        private static readonly GLColor4f purple = GetColor(128, 0, 128);
        private static readonly GLColor4f red = GetColor(255, 0, 0);
        private static readonly GLColor4f rosybrown = GetColor(188, 143, 143);
        private static readonly GLColor4f royalblue = GetColor(65, 105, 225);
        private static readonly GLColor4f saddlebrown = GetColor(139, 69, 19);
        private static readonly GLColor4f salmon = GetColor(250, 128, 114);
        private static readonly GLColor4f sandybrown = GetColor(244, 164, 96);
        private static readonly GLColor4f seagreen = GetColor(46, 139, 87);
        private static readonly GLColor4f seashell = GetColor(255, 245, 238);
        private static readonly GLColor4f sienna = GetColor(160, 82, 45);
        private static readonly GLColor4f silver = GetColor(192, 192, 192);
        private static readonly GLColor4f skyblue = GetColor(135, 206, 235);
        private static readonly GLColor4f slateblue = GetColor(106, 90, 205);
        private static readonly GLColor4f slategray = GetColor(112, 128, 144);
        private static readonly GLColor4f snow = GetColor(255, 250, 250);
        private static readonly GLColor4f springgreen = GetColor(0, 255, 127);
        private static readonly GLColor4f steelblue = GetColor(70, 130, 180);
        private static readonly GLColor4f tan = GetColor(210, 180, 140);
        private static readonly GLColor4f teal = GetColor(0, 128, 128);
        private static readonly GLColor4f thistle = GetColor(216, 191, 216);
        private static readonly GLColor4f tomato = GetColor(255, 99, 71);
        private static readonly GLColor4f turquoise = GetColor(64, 224, 208);
        private static readonly GLColor4f violet = GetColor(238, 130, 238);
        private static readonly GLColor4f wheat = GetColor(245, 222, 179);
        private static readonly GLColor4f white = GetColor(255, 255, 255);
        private static readonly GLColor4f whitesmoke = GetColor(245, 245, 245);
        private static readonly GLColor4f yellow = GetColor(255, 255, 0);
        private static readonly GLColor4f yellowgreen = GetColor(154, 205, 50);
        /*Properties*/
        public static GLColor4f Transparent { get { return transparent; } }
        public static GLColor4f AliceBlue { get { return aliceblue; } }
        public static GLColor4f AntiqueWhite { get { return antiquewhite; } }
        public static GLColor4f Aqua { get { return aqua; } }
        public static GLColor4f Aquamarine { get { return aquamarine; } }
        public static GLColor4f Azure { get { return azure; } }
        public static GLColor4f Beige { get { return beige; } }
        public static GLColor4f Bisque { get { return bisque; } }
        public static GLColor4f Black { get { return black; } }
        public static GLColor4f BlanchedAlmond { get { return blanchedalmond; } }
        public static GLColor4f Blue { get { return blue; } }
        public static GLColor4f BlueViolet { get { return blueviolet; } }
        public static GLColor4f Brown { get { return brown; } }
        public static GLColor4f BurlyWood { get { return burlywood; } }
        public static GLColor4f CadetBlue { get { return cadetblue; } }
        public static GLColor4f Chartreuse { get { return chartreuse; } }
        public static GLColor4f Chocolate { get { return chocolate; } }
        public static GLColor4f Coral { get { return coral; } }
        public static GLColor4f CornflowerBlue { get { return cornflowerblue; } }
        public static GLColor4f Cornsilk { get { return cornsilk; } }
        public static GLColor4f Crimson { get { return crimson; } }
        public static GLColor4f Cyan { get { return cyan; } }
        public static GLColor4f DarkBlue { get { return darkblue; } }
        public static GLColor4f DarkCyan { get { return darkcyan; } }
        public static GLColor4f DarkGoldenrod { get { return darkgoldenrod; } }
        public static GLColor4f DarkGray { get { return darkgray; } }
        public static GLColor4f DarkGreen { get { return darkgreen; } }
        public static GLColor4f DarkKhaki { get { return darkkhaki; } }
        public static GLColor4f DarkMagenta { get { return darkmagenta; } }
        public static GLColor4f DarkOliveGreen { get { return darkolivegreen; } }
        public static GLColor4f DarkOrange { get { return darkorange; } }
        public static GLColor4f DarkOrchid { get { return darkorchid; } }
        public static GLColor4f DarkRed { get { return darkred; } }
        public static GLColor4f DarkSalmon { get { return darksalmon; } }
        public static GLColor4f DarkSeaGreen { get { return darkseagreen; } }
        public static GLColor4f DarkSlateBlue { get { return darkslateblue; } }
        public static GLColor4f DarkSlateGray { get { return darkslategray; } }
        public static GLColor4f DarkTurquoise { get { return darkturquoise; } }
        public static GLColor4f DarkViolet { get { return darkviolet; } }
        public static GLColor4f DeepPink { get { return deeppink; } }
        public static GLColor4f DeepSkyBlue { get { return deepskyblue; } }
        public static GLColor4f DimGray { get { return dimgray; } }
        public static GLColor4f DodgerBlue { get { return dodgerblue; } }
        public static GLColor4f Firebrick { get { return firebrick; } }
        public static GLColor4f FloralWhite { get { return floralwhite; } }
        public static GLColor4f ForestGreen { get { return forestgreen; } }
        public static GLColor4f Fuchsia { get { return fuchsia; } }
        public static GLColor4f Gainsboro { get { return gainsboro; } }
        public static GLColor4f GhostWhite { get { return ghostwhite; } }
        public static GLColor4f Gold { get { return gold; } }
        public static GLColor4f Goldenrod { get { return goldenrod; } }
        public static GLColor4f Gray { get { return gray; } }
        public static GLColor4f Green { get { return green; } }
        public static GLColor4f GreenYellow { get { return greenyellow; } }
        public static GLColor4f Honeydew { get { return honeydew; } }
        public static GLColor4f HotPink { get { return hotpink; } }
        public static GLColor4f IndianRed { get { return indianred; } }
        public static GLColor4f Indigo { get { return indigo; } }
        public static GLColor4f Ivory { get { return ivory; } }
        public static GLColor4f Khaki { get { return khaki; } }
        public static GLColor4f Lavender { get { return lavender; } }
        public static GLColor4f LavenderBlush { get { return lavenderblush; } }
        public static GLColor4f LawnGreen { get { return lawngreen; } }
        public static GLColor4f LemonChiffon { get { return lemonchiffon; } }
        public static GLColor4f LightBlue { get { return lightblue; } }
        public static GLColor4f LightCoral { get { return lightcoral; } }
        public static GLColor4f LightCyan { get { return lightcyan; } }
        public static GLColor4f LightGoldenrodYellow { get { return lightgoldenrodyellow; } }
        public static GLColor4f LightGray { get { return lightgray; } }
        public static GLColor4f LightGreen { get { return lightgreen; } }
        public static GLColor4f LightPink { get { return lightpink; } }
        public static GLColor4f LightSalmon { get { return lightsalmon; } }
        public static GLColor4f LightSeaGreen { get { return lightseagreen; } }
        public static GLColor4f LightSkyBlue { get { return lightskyblue; } }
        public static GLColor4f LightSlateGray { get { return lightslategray; } }
        public static GLColor4f LightSteelBlue { get { return lightsteelblue; } }
        public static GLColor4f LightYellow { get { return lightyellow; } }
        public static GLColor4f Lime { get { return lime; } }
        public static GLColor4f LimeGreen { get { return limegreen; } }
        public static GLColor4f Linen { get { return linen; } }
        public static GLColor4f Magenta { get { return magenta; } }
        public static GLColor4f Maroon { get { return maroon; } }
        public static GLColor4f MediumAquamarine { get { return mediumaquamarine; } }
        public static GLColor4f MediumBlue { get { return mediumblue; } }
        public static GLColor4f MediumOrchid { get { return mediumorchid; } }
        public static GLColor4f MediumPurple { get { return mediumpurple; } }
        public static GLColor4f MediumSeaGreen { get { return mediumseagreen; } }
        public static GLColor4f MediumSlateBlue { get { return mediumslateblue; } }
        public static GLColor4f MediumSpringGreen { get { return mediumspringgreen; } }
        public static GLColor4f MediumTurquoise { get { return mediumturquoise; } }
        public static GLColor4f MediumVioletRed { get { return mediumvioletred; } }
        public static GLColor4f MidnightBlue { get { return midnightblue; } }
        public static GLColor4f MintCream { get { return mintcream; } }
        public static GLColor4f MistyRose { get { return mistyrose; } }
        public static GLColor4f Moccasin { get { return moccasin; } }
        public static GLColor4f NavajoWhite { get { return navajowhite; } }
        public static GLColor4f Navy { get { return navy; } }
        public static GLColor4f OldLace { get { return oldlace; } }
        public static GLColor4f Olive { get { return olive; } }
        public static GLColor4f OliveDrab { get { return olivedrab; } }
        public static GLColor4f Orange { get { return orange; } }
        public static GLColor4f OrangeRed { get { return orangered; } }
        public static GLColor4f Orchid { get { return orchid; } }
        public static GLColor4f PaleGoldenrod { get { return palegoldenrod; } }
        public static GLColor4f PaleGreen { get { return palegreen; } }
        public static GLColor4f PaleTurquoise { get { return paleturquoise; } }
        public static GLColor4f PaleVioletRed { get { return palevioletred; } }
        public static GLColor4f PapayaWhip { get { return papayawhip; } }
        public static GLColor4f PeachPuff { get { return peachpuff; } }
        public static GLColor4f Peru { get { return peru; } }
        public static GLColor4f Pink { get { return pink; } }
        public static GLColor4f Plum { get { return plum; } }
        public static GLColor4f PowderBlue { get { return powderblue; } }
        public static GLColor4f Purple { get { return purple; } }
        public static GLColor4f Red { get { return red; } }
        public static GLColor4f RosyBrown { get { return rosybrown; } }
        public static GLColor4f RoyalBlue { get { return royalblue; } }
        public static GLColor4f SaddleBrown { get { return saddlebrown; } }
        public static GLColor4f Salmon { get { return salmon; } }
        public static GLColor4f SandyBrown { get { return sandybrown; } }
        public static GLColor4f SeaGreen { get { return seagreen; } }
        public static GLColor4f SeaShell { get { return seashell; } }
        public static GLColor4f Sienna { get { return sienna; } }
        public static GLColor4f Silver { get { return silver; } }
        public static GLColor4f SkyBlue { get { return skyblue; } }
        public static GLColor4f SlateBlue { get { return slateblue; } }
        public static GLColor4f SlateGray { get { return slategray; } }
        public static GLColor4f Snow { get { return snow; } }
        public static GLColor4f SpringGreen { get { return springgreen; } }
        public static GLColor4f SteelBlue { get { return steelblue; } }
        public static GLColor4f Tan { get { return tan; } }
        public static GLColor4f Teal { get { return teal; } }
        public static GLColor4f Thistle { get { return thistle; } }
        public static GLColor4f Tomato { get { return tomato; } }
        public static GLColor4f Turquoise { get { return turquoise; } }
        public static GLColor4f Violet { get { return violet; } }
        public static GLColor4f Wheat { get { return wheat; } }
        public static GLColor4f White { get { return white; } }
        public static GLColor4f WhiteSmoke { get { return whitesmoke; } }
        public static GLColor4f Yellow { get { return yellow; } }
        public static GLColor4f YellowGreen { get { return yellowgreen; } }
        class GLTypeConverter : System.ComponentModel.TypeConverter
        {
            public override bool GetPropertiesSupported(ITypeDescriptorContext context)
            {
                return true;
            }
            public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
            {
                return TypeDescriptor.GetProperties (value);
            }
            public override object CreateInstance(ITypeDescriptorContext context, System.Collections.IDictionary propertyValues)
            {
                GLColor4f v_obj = new GLColor4f();
                v_obj.m_r =(float)propertyValues["R"];
                v_obj.m_g =(float)propertyValues["G"];
                v_obj.m_b =(float)propertyValues["B"];
                v_obj.m_a =(float)propertyValues["A"];
                return v_obj;
            }
            public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
            {
                return true;
            }
        }
        /// <summary>
        /// get the color
        /// </summary>
        /// <returns></returns>
        public System.Drawing.Color  GetColor()
        {
            return Color.FromArgb((int)(this.m_a * 255.0f),
                (int)(this.m_r * 255.0f),
                (int)(this.m_g * 255.0f),
                (int)(this.m_b * 255.0f));
        }
    }
    //Couleur bool�enne
    [TypeConverter(typeof(GLColor4b.GLTypeConverter))]
    [StructLayout(LayoutKind.Sequential)]
    public struct GLColor4b
    {
        bool m_r, m_g, m_b, m_a;
        public bool R { get { return m_r; } set { m_r = value; } }
        public bool G { get { return m_g; } set { m_g = value; } }
        public bool B { get { return m_b; } set { m_b = value; } }
        public bool A { get { return m_a; } set { m_a = value; } }
        public GLColor4b(bool r, bool g, bool b, bool a)
        {
            m_r = r;
            m_g = g;
            m_b = b;
            m_a = a;
        }
        public override string ToString()
        {
            return string.Format("r:{0}; g:{1}; b:{2}; a:{3}", R, G, B, A);
        }
        class GLTypeConverter : System.ComponentModel.TypeConverter
        {
            public override bool GetPropertiesSupported(ITypeDescriptorContext context)
            {
                return true;
            }
            public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
            {
                return TypeDescriptor.GetProperties(value);
            }
            public override object CreateInstance(ITypeDescriptorContext context, System.Collections.IDictionary propertyValues)
            {
                GLColor4b v_obj = new GLColor4b();
                v_obj.m_r = (bool)propertyValues["R"];
                v_obj.m_g = (bool)propertyValues["G"];
                v_obj.m_b = (bool)propertyValues["B"];
                v_obj.m_a = (bool)propertyValues["A"];
                return v_obj;
            }
            public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
            {
                return true;
            }
        }
    }
    //Couleur double
    [TypeConverter(typeof(GLColor3d.GLTypeConverter))]
    [StructLayout(LayoutKind.Sequential)]
    public struct GLColor3d
    {
        double m_r, m_g, m_b;
        public double R { get { return m_r; } set { m_r = value; } }
        public double G { get { return m_g; } set { m_g = value; } }
        public double B { get { return m_b; } set { m_b = value; } }
        public GLColor3d(double r, double g, double b)
        {
            m_r = r;
            m_g = g;
            m_b = b;
        }
        public override string ToString()
        {
            return string.Format("r:{0}; g:{1}; b:{2};", R, G, B);
        }
        public static implicit operator GLColor3d(GLColor3f c)
        {
            return new GLColor3d (c.R  , c.G , c.B  );
        }
        class GLTypeConverter : System.ComponentModel.TypeConverter
        {
            public override bool GetPropertiesSupported(ITypeDescriptorContext context)
            {
                return true;
            }
            public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
            {
                return TypeDescriptor.GetProperties(value);
            }
            public override object CreateInstance(ITypeDescriptorContext context, System.Collections.IDictionary propertyValues)
            {
                GLColor3d v_obj = new GLColor3d();
                v_obj.m_r = (double)propertyValues["R"];
                v_obj.m_g = (double)propertyValues["G"];
                v_obj.m_b = (double)propertyValues["B"];
                return v_obj;
            }
            public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
            {
                return true;
            }
        }
    }
    [TypeConverter(typeof(GLColor4d.GLTypeConverter))]
    [StructLayout(LayoutKind.Sequential)]
    public struct GLColor4d
    {
        double m_r, m_g, m_b, m_a;
        public double R { get { return m_r; } set { m_r = value; } }
        public double G { get { return m_g; } set { m_g = value; } }
        public double B { get { return m_b; } set { m_b = value; } }
        public double A { get { return m_a; } set { m_a = value; } }
        public GLColor4d(double r, double g, double b, double a)
        {
            m_r = r;
            m_g = g;
            m_b = b;
            m_a = a;
        }
        private GLColor4d(double r, double g, double b)
        {
            m_r = r;
            m_g = g;
            m_b = b;
            m_a = 1.0f;
        }
        public override string ToString()
        {
            return string.Format("r:{0}; g:{1}; b:{2}; a:{3}", R, G, B, A);
        }
        public static implicit operator GLColor4d(GLColor4f c)
        {
            return new GLColor4d (c.R , c.G , c.B , c.A );
        }
        /*Properties*/
        class GLTypeConverter : System.ComponentModel.TypeConverter
        {
            public override bool GetPropertiesSupported(ITypeDescriptorContext context)
            {
                return true;
            }
            public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
            {
                return TypeDescriptor.GetProperties(value);
            }
            public override object CreateInstance(ITypeDescriptorContext context, System.Collections.IDictionary propertyValues)
            {
                GLColor4d v_obj = new GLColor4d();
                v_obj.m_r = (double)propertyValues["R"];
                v_obj.m_g = (double)propertyValues["G"];
                v_obj.m_b = (double)propertyValues["B"];
                v_obj.m_a = (double)propertyValues["A"];
                return v_obj;
            }
            public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
            {
                return true;
            }
        }
    }
}

