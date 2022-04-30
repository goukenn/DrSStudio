

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Vector4f.cs
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
file:Vector4f.cs
*/

/* 
-------------------------------------------------------------
This file is part of iGK-DEV-DrawingStudio
-------------------------------------------------------------
-------------------------------------------------------------
-------------------------------------------------------------
view license file in Documentation folder to get more info
Copyright (c) 2008-2010 
Author  : Charles A.D. BONDJE DOUE 
mail : bondje.doue@hotmail.com
-------------------------------------------------------------
-------------------------------------------------------------
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using IGK.ICore;using IGK.ICore.ComponentModel;
namespace IGK.ICore
{
    /// <summary>
    /// represent the vector4 structure in only 4 float component
    /// </summary>
    [StructLayout(LayoutKind.Sequential )]
    [System.ComponentModel.TypeConverter(typeof(VectorfConverter))]
    public struct Vector4f : IVector4f
    {
        private float m_x;
        private float m_y;
        private float m_z;
        private float m_w;
        public float X { get { return this.m_x; } set { m_x = value; } }
        public float Y { get { return this.m_y; } set { m_y = value; } }
        public float Z { get { return this.m_z; } set { m_z = value; } }
        public float W { get { return this.m_w; } set { m_w = value; } }

        public static readonly Vector4f Zero;

        static Vector4f() {
            Zero = new Vector4f(0.0f);
        }
        public override string  ToString()
        {
            return string.Format("[{0};{1};{2};{3}]", X, Y, Z, W);
        }
        public Vector4f(float value)
        {
            m_x = value;
            m_y = value;
            m_z = value;
            m_w = value;
        }
        public Vector4f(float x, float y, float z, float w)
        {
            m_x = x;
            m_y = y;
            m_z = z;
            m_w = w;
        }
        
        public static implicit operator Vector4f(Vector3f v)
        {
            return new Vector4f(v.X,
                v.Y,
                v.Z,
                0.0f);
        }


        public static Vector4f operator * (Vector4f c, Matrix mat) {
            if (mat==null)
            return Vector4f.Zero;

            var t = mat.Elements;
            return new Vector4f(
                c.X * t[0] + c.Y * t[4] + c.Z * t[8] + c.W * t[12],
                c.X * t[1] + c.Y * t[5] + c.Z * t[9] + c.W * t[13],
                c.X * t[2] + c.Y * t[6] + c.Z * t[10] + c.W * t[14],
                c.X * t[3] + c.Y * t[7] + c.Z * t[11] + c.W * t[15]
                );
        }
    }
}

