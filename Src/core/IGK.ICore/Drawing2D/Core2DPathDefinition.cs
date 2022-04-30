

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Core2DPathDefinition.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.Drawing2D
{
    public struct  Core2DPathDefinition : ICore2DPathDefinition
    {
        private Vector2f[] m_points;
        private byte[] m_types;

        public Vector2f[] Points
        {
            get { return m_points; }
            set {
                m_points = value;
            }
        }

        public byte[] Types
        {
            get { return m_types; }
            set { this.m_types = value; }
        }
        public Core2DPathDefinition(Vector2f[] defPoints, byte[] defTypes)
        {
            this.m_points = defPoints;
            this.m_types = defTypes;
        }
    }
}
