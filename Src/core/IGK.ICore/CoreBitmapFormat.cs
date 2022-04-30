

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreBitmapFormat.cs
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

namespace IGK.ICore.Imaging
{
    /// <summary>
    /// represent the bitmap format
    /// </summary>
    public sealed class CoreBitmapFormat
    {
        public static readonly CoreBitmapFormat Png;
        public static readonly CoreBitmapFormat JPEG;
    
        private string m_Name;

        public string Name
        {
            get { return m_Name; }
        }
        static CoreBitmapFormat() {
            Png = new CoreBitmapFormat("Png");
            JPEG = new CoreBitmapFormat("JPEG");
        }

        private CoreBitmapFormat(string name)
        {
            this.m_Name = name;
        }
    }
}