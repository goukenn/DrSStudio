

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: LayerTransparencyKey.cs
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
file:LayerTransparencyKey.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Remoting;
using System.Runtime.InteropServices;
using System.ComponentModel;
namespace IGK.ICore.Drawing2D
{
    using IGK.ICore;using IGK.ICore.Codec;
    using IGK.ICore.ComponentModel;
    [StructLayout(LayoutKind.Sequential)]
    public struct LayerTransparencyKey : IXMLDefinition
    {
        private Colorf m_Color1;
        private Colorf m_Color2;
        public static LayerTransparencyKey Empty;
        public Colorf Color2
        {
            get { return m_Color2; }
            set { m_Color2 = value; }
        }
        public Colorf Color1
        {
            get { return m_Color1; }
            set { this.m_Color1 = value; }
        }
        static LayerTransparencyKey()
        {
            Empty = new LayerTransparencyKey();
            Empty.m_Color1 = Colorf.Empty;
            Empty.m_Color2 = Colorf.Empty;
        }
        public LayerTransparencyKey(Colorf cl1, Colorf cl2)
        {
            this.m_Color1 = cl1;
            this.m_Color2 = cl2;
        }
        public override string ToString()
        {
            return string.Format("{0} {1}", this.m_Color1.ToString(), this.m_Color2.ToString());
        }
        #region ICoreGKDSDefinition Members
        public string GetDefinition()
        {
            if (this.Color1.Equals(Colorf.Empty) && this.Color2.Equals(Colorf.Empty))
                return null;
            return string.Format("Start:{0};End:{1}",
                this.Color1.ToString(),
                this.Color2.ToString()
                );
        }
        public void CopyFromDefinition(string definition)
        {
            string[] v_tb = definition.Split(new char[] { ';', ':' });
            for (int i = 0; i < v_tb.Length; i += 2)
            {
                switch (v_tb[i].Trim())
                {
                    case "Start":
                        this.m_Color1 = Colorf.Convert(v_tb[i + 1]);
                        break;
                    case "End":
                        this.m_Color2 = Colorf.Convert(v_tb[i + 1]);
                        break;
                }
            }
        }
        #endregion
      }
}

