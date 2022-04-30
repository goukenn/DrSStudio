

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Vector4d.cs
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
file:Vector4d.cs
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
using IGK.ICore;using IGK.ICore.ComponentModel;
namespace IGK.ICore
{
    [System.ComponentModel.TypeConverter(typeof(VectorfConverter))]
    public struct Vector4d
    {
        private double m_x;
        private double m_y;
        private double m_z;
        private double m_t;
        public double X { get { return this.m_x; } set { m_x = value; } }
        public double Y { get { return this.m_y; } set { m_y = value; } }
        public double Z { get { return this.m_z; } set { m_z = value; } }
        public double T { get { return this.m_t; } set { m_t = value; } }
        public override string ToString()
        {
            return string.Format("[{0};{1};{2};{3}]", X, Y, Z, T);
        }
        public Vector4d(double value)
        {
            m_x = value;
            m_y = value;
            m_z = value;
            m_t = value;
        }
        public Vector4d(double x, double y, double z, double t)
        {
            m_x = x;
            m_y = y;
            m_z = z;
            m_t = t;
        }
        public static Vector4d Zero { get { return new Vector4d(0); } }
    }
}

