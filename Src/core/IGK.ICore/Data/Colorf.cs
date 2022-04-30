

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Colorf.cs
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
file:Colorf.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;

namespace IGK.ICore
{
    using IGK.ICore.Drawing2D;
    using IGK.ICore;
    using IGK.ICore.ComponentModel;
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    /// <summary>
    /// represent the default color manager
    /// </summary>
    public struct Colorf : IColorf
    {
        private float m_R;
        private float m_G;
        private float m_B;
        private float m_A;

        public int IntValue() {
            uint v = (uint)(m_A * 255) << 24;
            v += (uint)(m_R * 255) << 16;
            v += (uint)(m_G * 255) << 8;
            v += (uint)(m_B * 255) ;
            return (int)v;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public float A
        {
            get { return m_A; }
            set { m_A = value; }
        }
        public float B
        {
            get { return m_B; }
            set { m_B = value; }
        }
        public float G
        {
            get { return m_G; }
            set { m_G = value; }
        }
        public float R
        {
            get { return m_R; }
            set { m_R = value; }
        }
        public override string ToString()
        {
            return string.Format("{0};{1};{2};{3}",
                this.A,
                this.R,
                this.G,
                this.B);
        }
        public string ToString(bool webColor)
        {
            return ConvertToString(this, webColor);
        }
        public override bool  Equals(object obj)
        {
            if ((null == obj) || ! (obj is Colorf )) 
                return false; 
             Colorf f1 = (Colorf)obj;
            return ((this.m_A == f1.m_A) &&
                (this.m_R == f1.m_R) &&
                (this.m_G == f1.m_G) &&
                (this.m_B == f1.m_B));
        }
        public static implicit operator Colorf(string d)        
        {
            return Colorf.FromString (d);
        }
        public static  bool operator  ==(Colorf t1, Colorf f1)
        {
            return ((t1.m_A == f1.m_A) &&
                (t1.m_R == f1.m_R) &&
                (t1.m_G == f1.m_G) &&
                (t1.m_B == f1.m_B));
        }
        public static bool operator !=(Colorf t1, Colorf f1)
        {
            return !(t1 == f1.m_A);
        }
        static Colorf (){
            Colorf v_cl = Colorf.Empty;
            Type t = typeof(Colorf);
            sm_colors = new Dictionary<Colorf, string>();
            foreach (System.Reflection.PropertyInfo pr in
               t.GetProperties(System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public))
            {
                if (pr.PropertyType == t)
                {
                    v_cl = (Colorf)pr.GetValue(null, null);
                    if (!sm_colors.ContainsKey(v_cl) && !sm_colors.ContainsValue(pr.Name))
                    {
                        sm_colors.Add(v_cl, pr.Name);
                    }
                }
            }
            Empty = new Colorf(0, 0, 0, 0);
        }
        public Colorf(float r, float g, float b):this(1.0f,r,g,b)
        { 
        }
        public Colorf(float a, float r, float g, float b)
        {
            this.m_R = Trim(r);
            this.m_G = Trim(g);
            this.m_B = Trim(b);
            this.m_A = Trim(a);            
        }
        public static float Trim(float value)
        {
            if (value < 0.0f) return 0.0f;
            if (value > 1.0f) return 1;
            return value;
        }
        /// <summary>
        /// represent a complent color method
        /// </summary>
        /// <param name="cl"></param>
        /// <returns></returns>
        public static Colorf Complement(Colorf cl)
        {
            Colorf v_cl = new Colorf();
            v_cl.m_A = cl.A;
            v_cl.R = 1.0f - cl.R;
            v_cl.G = 1.0f - cl.G;
            v_cl.B = 1.0f - cl.B;
            return v_cl ; 
        }
        public string GetName()
        {
            if (sm_colors.ContainsKey(this))
            {
                return sm_colors[this];
            }            
            return string.Empty;
        }
        static Dictionary<Colorf, string> sm_colors;
        public static readonly Colorf Empty;
        public static Colorf Transparent { get { return new Colorf(0.00f, 0.00f, 0.00f, 0.00f); } }
        public static Colorf Black { get { return new Colorf(1.00f, 0.00f, 0.00f, 0.00f); } }
        public static Colorf Navy { get { return new Colorf(1.00f, 0.00f, 0.00f, 0.50f); } }
        public static Colorf DarkBlue { get { return new Colorf(1.00f, 0.00f, 0.00f, 0.55f); } }
        public static Colorf MediumBlue { get { return new Colorf(1.00f, 0.00f, 0.00f, 0.80f); } }
        public static Colorf Blue { get { return new Colorf(1.00f, 0.00f, 0.00f, 1.00f); } }
        public static Colorf DarkGreen { get { return new Colorf(1.00f, 0.00f, 0.39f, 0.00f); } }
        public static Colorf Green { get { return new Colorf(1.00f, 0.00f, 0.50f, 0.00f); } }
        public static Colorf Teal { get { return new Colorf(1.00f, 0.00f, 0.50f, 0.50f); } }
        public static Colorf DarkCyan { get { return new Colorf(1.00f, 0.00f, 0.55f, 0.55f); } }
        public static Colorf DeepSkyBlue { get { return new Colorf(1.00f, 0.00f, 0.75f, 1.00f); } }
        public static Colorf DarkTurquoise { get { return new Colorf(1.00f, 0.00f, 0.81f, 0.82f); } }
        public static Colorf MediumSpringGreen { get { return new Colorf(1.00f, 0.00f, 0.98f, 0.60f); } }
        public static Colorf Lime { get { return new Colorf(1.00f, 0.00f, 1.00f, 0.00f); } }
        public static Colorf SpringGreen { get { return new Colorf(1.00f, 0.00f, 1.00f, 0.50f); } }
        public static Colorf Aqua { get { return new Colorf(1.00f, 0.00f, 1.00f, 1.00f); } }
        public static Colorf Cyan { get { return new Colorf(1.00f, 0.00f, 1.00f, 1.00f); } }
        public static Colorf MidnightBlue { get { return new Colorf(1.00f, 0.10f, 0.10f, 0.44f); } }
        public static Colorf DodgerBlue { get { return new Colorf(1.00f, 0.12f, 0.56f, 1.00f); } }
        public static Colorf LightSeaGreen { get { return new Colorf(1.00f, 0.13f, 0.70f, 0.67f); } }
        public static Colorf ForestGreen { get { return new Colorf(1.00f, 0.13f, 0.55f, 0.13f); } }
        public static Colorf SeaGreen { get { return new Colorf(1.00f, 0.18f, 0.55f, 0.34f); } }
        public static Colorf DarkSlateGrey { get { return new Colorf(1.00f, 0.18f, 0.31f, 0.31f); } }
        public static Colorf DarkSlateGray { get { return new Colorf(1.00f, 0.18f, 0.31f, 0.31f); } }
        public static Colorf LimeGreen { get { return new Colorf(1.00f, 0.20f, 0.80f, 0.20f); } }
        public static Colorf MediumSeaGreen { get { return new Colorf(1.00f, 0.24f, 0.70f, 0.44f); } }
        public static Colorf Turquoise { get { return new Colorf(1.00f, 0.25f, 0.88f, 0.82f); } }
        public static Colorf RoyalBlue { get { return new Colorf(1.00f, 0.25f, 0.41f, 0.88f); } }
        public static Colorf SteelBlue { get { return new Colorf(1.00f, 0.27f, 0.51f, 0.71f); } }
        public static Colorf DarkSlateBlue { get { return new Colorf(1.00f, 0.28f, 0.24f, 0.55f); } }
        public static Colorf MediumTurquoise { get { return new Colorf(1.00f, 0.28f, 0.82f, 0.80f); } }
        public static Colorf Indigo { get { return new Colorf(1.00f, 0.29f, 0.00f, 0.51f); } }
        public static Colorf DarkOliveGreen { get { return new Colorf(1.00f, 0.33f, 0.42f, 0.18f); } }
        public static Colorf CadetBlue { get { return new Colorf(1.00f, 0.37f, 0.62f, 0.63f); } }
        public static Colorf CornflowerBlue { get { return new Colorf(1.00f, 0.39f, 0.58f, 0.93f); } }
        public static Colorf MediumAquaMarine { get { return new Colorf(1.00f, 0.40f, 0.80f, 0.67f); } }
        public static Colorf DimGray { get { return new Colorf(1.00f, 0.41f, 0.41f, 0.41f); } }
        public static Colorf DimGrey { get { return new Colorf(1.00f, 0.41f, 0.41f, 0.41f); } }
        public static Colorf SlateBlue { get { return new Colorf(1.00f, 0.42f, 0.35f, 0.80f); } }
        public static Colorf OliveDrab { get { return new Colorf(1.00f, 0.42f, 0.56f, 0.14f); } }
        public static Colorf SlateGrey { get { return new Colorf(1.00f, 0.44f, 0.50f, 0.56f); } }
        public static Colorf SlateGray { get { return new Colorf(1.00f, 0.44f, 0.50f, 0.56f); } }
        public static Colorf LightSlateGray { get { return new Colorf(1.00f, 0.47f, 0.53f, 0.60f); } }
        public static Colorf LightSlateGrey { get { return new Colorf(1.00f, 0.47f, 0.53f, 0.60f); } }
        public static Colorf MediumSlateBlue { get { return new Colorf(1.00f, 0.48f, 0.41f, 0.93f); } }
        public static Colorf LawnGreen { get { return new Colorf(1.00f, 0.49f, 0.99f, 0.00f); } }
        public static Colorf Chartreuse { get { return new Colorf(1.00f, 0.50f, 1.00f, 0.00f); } }
        public static Colorf Aquamarine { get { return new Colorf(1.00f, 0.50f, 1.00f, 0.83f); } }
        public static Colorf Maroon { get { return new Colorf(1.00f, 0.50f, 0.00f, 0.00f); } }
        public static Colorf Purple { get { return new Colorf(1.00f, 0.50f, 0.00f, 0.50f); } }
        public static Colorf Olive { get { return new Colorf(1.00f, 0.50f, 0.50f, 0.00f); } }
        public static Colorf Grey { get { return new Colorf(1.00f, 0.50f, 0.50f, 0.50f); } }
        public static Colorf Gray { get { return new Colorf(1.00f, 0.50f, 0.50f, 0.50f); } }
        public static Colorf SkyBlue { get { return new Colorf(1.00f, 0.53f, 0.81f, 0.92f); } }
        public static Colorf LightSkyBlue { get { return new Colorf(1.00f, 0.53f, 0.81f, 0.98f); } }
        public static Colorf BlueViolet { get { return new Colorf(1.00f, 0.54f, 0.17f, 0.89f); } }
        public static Colorf DarkRed { get { return new Colorf(1.00f, 0.55f, 0.00f, 0.00f); } }
        public static Colorf DarkMagenta { get { return new Colorf(1.00f, 0.55f, 0.00f, 0.55f); } }
        public static Colorf SaddleBrown { get { return new Colorf(1.00f, 0.55f, 0.27f, 0.07f); } }
        public static Colorf DarkSeaGreen { get { return new Colorf(1.00f, 0.56f, 0.74f, 0.56f); } }
        public static Colorf LightGreen { get { return new Colorf(1.00f, 0.56f, 0.93f, 0.56f); } }
        public static Colorf MediumPurple { get { return new Colorf(1.00f, 0.58f, 0.44f, 0.85f); } }
        public static Colorf DarkViolet { get { return new Colorf(1.00f, 0.58f, 0.00f, 0.83f); } }
        public static Colorf PaleGreen { get { return new Colorf(1.00f, 0.60f, 0.98f, 0.60f); } }
        public static Colorf DarkOrchid { get { return new Colorf(1.00f, 0.60f, 0.20f, 0.80f); } }
        public static Colorf YellowGreen { get { return new Colorf(1.00f, 0.60f, 0.80f, 0.20f); } }
        public static Colorf Sienna { get { return new Colorf(1.00f, 0.63f, 0.32f, 0.18f); } }
        public static Colorf Brown { get { return new Colorf(1.00f, 0.65f, 0.16f, 0.16f); } }
        public static Colorf DarkGrey { get { return new Colorf(1.00f, 0.66f, 0.66f, 0.66f); } }
        public static Colorf DarkGray { get { return new Colorf(1.00f, 0.66f, 0.66f, 0.66f); } }
        public static Colorf LightBlue { get { return new Colorf(1.00f, 0.68f, 0.85f, 0.90f); } }
        public static Colorf GreenYellow { get { return new Colorf(1.00f, 0.68f, 1.00f, 0.18f); } }
        public static Colorf PaleTurquoise { get { return new Colorf(1.00f, 0.69f, 0.93f, 0.93f); } }
        public static Colorf LightSteelBlue { get { return new Colorf(1.00f, 0.69f, 0.77f, 0.87f); } }
        public static Colorf PowderBlue { get { return new Colorf(1.00f, 0.69f, 0.88f, 0.90f); } }
        public static Colorf FireBrick { get { return new Colorf(1.00f, 0.70f, 0.13f, 0.13f); } }
        public static Colorf DarkGoldenRod { get { return new Colorf(1.00f, 0.72f, 0.53f, 0.04f); } }
        public static Colorf MediumOrchid { get { return new Colorf(1.00f, 0.73f, 0.33f, 0.83f); } }
        public static Colorf RosyBrown { get { return new Colorf(1.00f, 0.74f, 0.56f, 0.56f); } }
        public static Colorf DarkKhaki { get { return new Colorf(1.00f, 0.74f, 0.72f, 0.42f); } }
        public static Colorf Silver { get { return new Colorf(1.00f, 0.75f, 0.75f, 0.75f); } }
        public static Colorf MediumVioletRed { get { return new Colorf(1.00f, 0.78f, 0.08f, 0.52f); } }
        public static Colorf IndianRed { get { return new Colorf(1.00f, 0.80f, 0.36f, 0.36f); } }
        public static Colorf Peru { get { return new Colorf(1.00f, 0.80f, 0.52f, 0.25f); } }
        public static Colorf Chocolate { get { return new Colorf(1.00f, 0.82f, 0.41f, 0.12f); } }
        public static Colorf Tan { get { return new Colorf(1.00f, 0.82f, 0.71f, 0.55f); } }
        public static Colorf LightGrey { get { return new Colorf(1.00f, 0.83f, 0.83f, 0.83f); } }
        public static Colorf LightGray { get { return new Colorf(1.00f, 0.83f, 0.83f, 0.83f); } }
        public static Colorf PaleVioletRed { get { return new Colorf(1.00f, 0.85f, 0.44f, 0.58f); } }
        public static Colorf Thistle { get { return new Colorf(1.00f, 0.85f, 0.75f, 0.85f); } }
        public static Colorf Orchid { get { return new Colorf(1.00f, 0.85f, 0.44f, 0.84f); } }
        public static Colorf GoldenRod { get { return new Colorf(1.00f, 0.85f, 0.65f, 0.13f); } }
        public static Colorf Crimson { get { return new Colorf(1.00f, 0.86f, 0.08f, 0.24f); } }
        public static Colorf Gainsboro { get { return new Colorf(1.00f, 0.86f, 0.86f, 0.86f); } }
        public static Colorf Plum { get { return new Colorf(1.00f, 0.87f, 0.63f, 0.87f); } }
        public static Colorf BurlyWood { get { return new Colorf(1.00f, 0.87f, 0.72f, 0.53f); } }
        public static Colorf LightCyan { get { return new Colorf(1.00f, 0.88f, 1.00f, 1.00f); } }
        public static Colorf Lavender { get { return new Colorf(1.00f, 0.90f, 0.90f, 0.98f); } }
        public static Colorf DarkSalmon { get { return new Colorf(1.00f, 0.91f, 0.59f, 0.48f); } }
        public static Colorf Violet { get { return new Colorf(1.00f, 0.93f, 0.51f, 0.93f); } }
        public static Colorf PaleGoldenRod { get { return new Colorf(1.00f, 0.93f, 0.91f, 0.67f); } }
        public static Colorf LightCoral { get { return new Colorf(1.00f, 0.94f, 0.50f, 0.50f); } }
        public static Colorf Khaki { get { return new Colorf(1.00f, 0.94f, 0.90f, 0.55f); } }
        public static Colorf AliceBlue { get { return new Colorf(1.00f, 0.94f, 0.97f, 1.00f); } }
        public static Colorf HoneyDew { get { return new Colorf(1.00f, 0.94f, 1.00f, 0.94f); } }
        public static Colorf Azure { get { return new Colorf(1.00f, 0.94f, 1.00f, 1.00f); } }
        public static Colorf SandyBrown { get { return new Colorf(1.00f, 0.96f, 0.64f, 0.38f); } }
        public static Colorf Wheat { get { return new Colorf(1.00f, 0.96f, 0.87f, 0.70f); } }
        public static Colorf Beige { get { return new Colorf(1.00f, 0.96f, 0.96f, 0.86f); } }
        public static Colorf WhiteSmoke { get { return new Colorf(1.00f, 0.96f, 0.96f, 0.96f); } }
        public static Colorf MintCream { get { return new Colorf(1.00f, 0.96f, 1.00f, 0.98f); } }
        public static Colorf GhostWhite { get { return new Colorf(1.00f, 0.97f, 0.97f, 1.00f); } }
        public static Colorf Salmon { get { return new Colorf(1.00f, 0.98f, 0.50f, 0.45f); } }
        public static Colorf AntiqueWhite { get { return new Colorf(1.00f, 0.98f, 0.92f, 0.84f); } }
        public static Colorf Linen { get { return new Colorf(1.00f, 0.98f, 0.94f, 0.90f); } }
        public static Colorf LightGoldenRodYellow { get { return new Colorf(1.00f, 0.98f, 0.98f, 0.82f); } }
        public static Colorf OldLace { get { return new Colorf(1.00f, 0.99f, 0.96f, 0.90f); } }
        public static Colorf Red { get { return new Colorf(1.00f, 1.00f, 0.00f, 0.00f); } }
        public static Colorf Magenta { get { return new Colorf(1.00f, 1.00f, 0.00f, 1.00f); } }
        public static Colorf Fuchsia { get { return new Colorf(1.00f, 1.00f, 0.00f, 1.00f); } }
        public static Colorf DeepPink { get { return new Colorf(1.00f, 1.00f, 0.08f, 0.58f); } }
        public static Colorf OrangeRed { get { return new Colorf(1.00f, 1.00f, 0.27f, 0.00f); } }
        public static Colorf Tomato { get { return new Colorf(1.00f, 1.00f, 0.39f, 0.28f); } }
        public static Colorf HotPink { get { return new Colorf(1.00f, 1.00f, 0.41f, 0.71f); } }
        public static Colorf Coral { get { return new Colorf(1.00f, 1.00f, 0.50f, 0.31f); } }
        public static Colorf Darkorange { get { return new Colorf(1.00f, 1.00f, 0.55f, 0.00f); } }
        public static Colorf LightSalmon { get { return new Colorf(1.00f, 1.00f, 0.63f, 0.48f); } }
        public static Colorf Orange { get { return new Colorf(1.00f, 1.00f, 0.65f, 0.00f); } }
        public static Colorf LightPink { get { return new Colorf(1.00f, 1.00f, 0.71f, 0.76f); } }
        public static Colorf Pink { get { return new Colorf(1.00f, 1.00f, 0.75f, 0.80f); } }
        public static Colorf Gold { get { return new Colorf(1.00f, 1.00f, 0.84f, 0.00f); } }
        public static Colorf PeachPuff { get { return new Colorf(1.00f, 1.00f, 0.85f, 0.73f); } }
        public static Colorf NavajoWhite { get { return new Colorf(1.00f, 1.00f, 0.87f, 0.68f); } }
        public static Colorf Moccasin { get { return new Colorf(1.00f, 1.00f, 0.89f, 0.71f); } }
        public static Colorf Bisque { get { return new Colorf(1.00f, 1.00f, 0.89f, 0.77f); } }
        public static Colorf MistyRose { get { return new Colorf(1.00f, 1.00f, 0.89f, 0.88f); } }
        public static Colorf BlanchedAlmond { get { return new Colorf(1.00f, 1.00f, 0.92f, 0.80f); } }
        public static Colorf PapayaWhip { get { return new Colorf(1.00f, 1.00f, 0.94f, 0.84f); } }
        public static Colorf LavenderBlush { get { return new Colorf(1.00f, 1.00f, 0.94f, 0.96f); } }
        public static Colorf SeaShell { get { return new Colorf(1.00f, 1.00f, 0.96f, 0.93f); } }
        public static Colorf Cornsilk { get { return new Colorf(1.00f, 1.00f, 0.97f, 0.86f); } }
        public static Colorf LemonChiffon { get { return new Colorf(1.00f, 1.00f, 0.98f, 0.80f); } }
        public static Colorf FloralWhite { get { return new Colorf(1.00f, 1.00f, 0.98f, 0.94f); } }
        public static Colorf Snow { get { return new Colorf(1.00f, 1.00f, 0.98f, 0.98f); } }
        public static Colorf Yellow { get { return new Colorf(1.00f, 1.00f, 1.00f, 0.00f); } }
        public static Colorf LightYellow { get { return new Colorf(1.00f, 1.00f, 1.00f, 0.88f); } }
        public static Colorf Ivory { get { return new Colorf(1.00f, 1.00f, 1.00f, 0.94f); } }
        public static Colorf White { get { return new Colorf(1.00f, 1.00f, 1.00f, 1.00f); } }
        /// <summary>
        /// invert the currrent color
        /// </summary>
        public void Invert() {
            this.m_B = 1 - this.m_B;
            this.m_G = 1 - this.m_G;
            this.m_R = 1 - this.m_R;
        }
        public static implicit operator Colorf(int all)
        {
            return new Colorf(all / 255.0f,
                all / 255.0f,
                all / 255.0f,
                all / 255.0f);
        }
        public static implicit operator Colorf(float  all)
        {
            return new Colorf(all ,
                all ,
                all ,
                all );
        }
        public static Colorf Convert(string format)
        {
            Type t = typeof(Colorf);
            System.Reflection.PropertyInfo v_pr = t.GetProperty(format, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Static );
            if ((v_pr != null) && (v_pr.PropertyType == t))
            {
                return (Colorf)v_pr.GetValue(null, null);
            }
            return GetColorFromWebHex(format);
        }
        public static string ConvertToString(Colorf colorf)
        {
           Colorf cl = colorf;
           string v_result = string.Empty;
           v_result = GetWebColorHex(
               (cl.A).TrimByte(),
               (cl.R).TrimByte(),
               (cl.G).TrimByte(),
               (cl.B).TrimByte());
           return v_result;
        }
        public static string ConvertToString(Colorf colorf, bool ignoreAlpha)
        {
            byte r = (byte)(colorf.R * 255);
            byte g = (Byte)(colorf.G* 255);
            byte b = (byte)(colorf.B * 255);
            byte a = (byte)(colorf.A * 255);
            string v_result = string.Empty;
            if (ignoreAlpha)
            {
                if (a == 0)
                {
                    return "Transparent";
                }
                else 
                    v_result = GetWebColorHex(r,g,b);
            }
            else
            {
                v_result = GetWebColorHex(a, r, g, b);
            }
            return v_result;
        }
        static string ToHex(byte t, ref bool areSame)
        {
            int h = ((h = (t / 16)) < 10) ? h + '0' : (h - 10) + 'A';
            int v = ((v = t & 0x0f) < 10) ? v + '0' : (v - 10) + 'A';
            string stv = (char)h + "" + (char)v;
            areSame = (h == v);
            return stv;
        }
        private static string GetWebColorHex(byte A, byte R, byte G, byte B)
        {
            bool vare = false;
            int count = 0;
            string a = ToHex(A, ref vare);
            if (vare) count++;
            string r = ToHex(R, ref vare);
            if (vare) count++;
            string g = ToHex(G, ref vare);
            if (vare) count++;
            string b = ToHex(B, ref vare);
            if (vare) count++;
            string vout = "";
            if (count == 4)
            {
                //are the same
                vout = string.Format("#{0}{1}{2}{3}", a[0], r[0], g[0], b[0]);
            }
            else
            {
                vout = string.Format("#{0}{1}{2}{3}", a, r, g, b);
            }
            return vout;
        }
        private static string GetWebColorHex(byte R, byte G, byte B)
        {
            bool vare = false;
            int count = 0;
            string r = ToHex(R, ref vare);
            if (vare) count++;
            string g = ToHex(G, ref vare);
            if (vare) count++;
            string b = ToHex(B, ref vare);
            if (vare) count++;
            string vout = "";
            if (count == 3)
            {
                //are the same
                vout = string.Format("#{0}{1}{2}",  r[0], g[0], b[0]);
            }
            else
            {
                vout = string.Format("#{0}{1}{2}", r, g, b);
            }
            return vout;
        }
        private static Colorf GetColorFromWebHex(string txt)
        {
            txt = txt.Trim ();
            if (txt.StartsWith("#") || txt.StartsWith("0x"))
            {
                txt = txt.Replace("#", "").Replace("0x", "");
                uint i = 0;
                switch (txt.Length)
                {
                    case 8:
                        break;
                    case 4:
                        txt = "" + txt[0] + txt[0] +
                            txt[1] + txt[1] +
                            txt[2] + txt[2] +
                            txt[3] + txt[3];
                        break;
                    case 6:
                        txt = "FF" + txt;
                        break;
                    case 3:
                        txt = "FF" + txt[0] + txt[0] +
                            txt[1] + txt[1] +
                            txt[2] + txt[2];
                        break;
                    default :
                        return Colorf.Black;
                }
                try
                {
                    i = (uint)System.Convert.ToInt32(txt, 16);
                    Colorf c = new Colorf();
                    int a = (int)((i >> 24) & 0x00FF);
                    int r = (int)((i >> 16) & 0x00FF);
                    int g = (int)((i >> 8) & 0x00FF);
                    int b = (int)((i) & 0x00FF);
                    c.m_R = r / 255.0f;
                    c.m_G = g / 255.0f;
                    c.m_B = b / 255.0f;
                    c.m_A = a / 255.0f;
                    return c;
                }
                catch {
                    return Colorf.Empty;
                }
            }
            else if (Enum.IsDefined(typeof(enuCoreWebColors), txt))
            {
                //get if is web color
                object v_obj = Enum.Parse(typeof(
                    enuCoreWebColors),
                    txt, true);
                if (v_obj != null)
                {
                    uint i = (uint)v_obj;
                    Colorf c = new Colorf();
                    int a = (int)((i >> 24) & 0x00FF);
                    int r = (int)((i >> 16) & 0x00FF);
                    int g = (int)((i >> 8) & 0x00FF);
                    int b = (int)((i) & 0x00FF);
                    c.m_R = r / 255.0f;
                    c.m_G = g / 255.0f;
                    c.m_B = b / 255.0f;
                    c.m_A = a / 255.0f;
                    return c;
                }
            }
            else {
                string[] v_t = txt.Split(';');
                if (v_t.Length == 4)
                {
                    Colorf v = new Colorf ();
                    v.m_A = Trim(float.Parse(v_t[0]));
                    v.m_R = Trim(float.Parse(v_t[1]));
                    v.m_G = Trim(float.Parse(v_t[2]));
                    v.m_B = Trim(float.Parse(v_t[3]));
                    return v;
                }
            }
            return Colorf.Empty;
        }
        public static float GetLuminosity(Colorf cl)
        {
            float r = cl.R;
            float g = cl.G;
            float b = cl.B;
            //from internet resources documentation 
            float v_r =  ((r * 299) + (g * 587) + (b * 114)) / 10.0f;
            return v_r;              
        }
        //{
        //    return null;
        //}
        public static Colorf FromFloat(float r, float g, float b)
        {
            return new Colorf(1.0f, Trim(r),Trim ( g),Trim ( b));
        }
        public static Colorf FromFloat(float rgb)
        {
            rgb = Trim(rgb);
            return new Colorf(1.0f, rgb, rgb, rgb);
        }
        public static Colorf FromFloat(float a, float rgb)
        {
            rgb = Trim(rgb);
            return new Colorf(Trim(a), rgb, rgb, rgb);
        }
        /// <summary>
        /// from float
        /// </summary>
        /// <param name="a"></param>
        /// <param name="rgb"></param>
        /// <returns></returns>
        public static Colorf FromFloat(float a, Colorf rgb)
        {
            a = Trim(a);
            return new Colorf(a, rgb.R , rgb.G , rgb.B );
        }
        public static Colorf FromFloat(float a, float r, float g, float b )
        {
            return new Colorf(Trim(a), Trim(r), Trim(g),Trim (b));
        }
        /// <summary>
        //class MySerializer : CodeDomSerializer
        //{ 
        //}
        public  static Colorf FromArgb(byte A, byte R, byte G, byte B)
        {
            return new Colorf(A / 255,
                R / 255,
                G / 255,
                B / 255);
        }
        public static Colorf FromString(string stringValue)
        {
            TypeConverter conv=  CoreTypeDescriptor.GetConverter(typeof(Colorf));
            if (conv.CanConvertFrom(typeof(string)))
            {
                return (Colorf)conv.ConvertFromString(stringValue);
            }
            return Colorf.Empty;
        }
        public static Colorf FromIntArgb(int argb)
        {
            return FromIntArgb(
                (argb >>24) & 0x000000FF,
                (argb & 0x00FF0000)>>16,
                (argb & 0x0000FF00)>>8,
                (argb & 0x000000FF)
                );
        }
        public static Colorf FromIntArgb(int a, int r, int g, int b)
        {
            return new Colorf(a / 255.0f,
               r / 255.0f,
               g / 255.0f,
               b / 255.0f);
        }
        /// <summary>
        /// get color from byte argb
        /// </summary>
        /// <param name="a">alpha</param>
        /// <param name="r">red</param>
        /// <param name="g">green </param>
        /// <param name="b">blue</param>
        /// <returns></returns>
        public static Colorf FromByteArgb(byte a, byte r, byte g, byte b)
        {
            return new Colorf(a / 255.0f,
               r / 255.0f,
               g / 255.0f,
               b / 255.0f);
        }
        /// <summary>
        /// get a color from byte rgb
        /// </summary>
        /// <param name="r">red</param>
        /// <param name="g">green</param>
        /// <param name="b">blue </param>
        /// <returns></returns>
        public static Colorf FromByteRgb( byte r, byte g, byte b)
        {
            return FromByteArgb(255, r, g, b);
        }
        //public static Colorf FromFloat(float A, Colorf cl)
        //{
        //    return new Colorf(A,
        //      cl.R ,
        //      cl.G ,
        //      cl.B);
        //}
        public static Colorf FromArgb(int a, int  r, int  g, int  b)
        {
            return FromIntArgb(a, r, g, b);// Argb((byte)a, (byte)r, (byte)g, (byte)b);
        }
    }
}

