

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WiXYesOrNoValue.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
using IGK.ICore;
using IGK.ICore.Codec;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.DrSStudio;
using IGK.DrSStudio.Menu;
using IGK.DrSStudio.WinUI;
using IGK.DrSStudio.Drawing2D;
using IGK.DrSStudio.Drawing2D.WinUI;
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.WiXAddIn
{
    public struct WiXYesOrNoValue
    {
        public static readonly WiXYesOrNoValue Yes;
        public static readonly WiXYesOrNoValue No;
        private string name;

        static WiXYesOrNoValue() {
            Yes = new WiXYesOrNoValue("yes");
            No = new WiXYesOrNoValue("no");
        }

        public WiXYesOrNoValue(string name)
        {            
            this.name = name;
        }
        public override string ToString()
        {
            return this.name;
        }
        public static bool operator == (WiXYesOrNoValue op, WiXYesOrNoValue op2)
        {
            return op.name == op2.name;
        }
        public static bool operator !=(WiXYesOrNoValue op, WiXYesOrNoValue op2)
        {
            return op.name != op2.name;
        }
        public override int GetHashCode()
        {
 	         return base.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            if (obj is WiXYesOrNoValue)
            {
                this.name.Equals(((WiXYesOrNoValue)obj).name);
            }
 	         return this.name.Equals (obj );
        }

    }
}
