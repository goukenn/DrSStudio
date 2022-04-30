

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AndroidGUIObjectAttribute.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿

using IGK.DrSStudio.Android.WinUI;
using IGK.ICore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.DrSStudio.Android
{
    [AttributeUsage (AttributeTargets.Class , Inherited =false , AllowMultiple = false )]
    public class AndroidGUIObjectAttribute : 
        AndroidGroupAttribute ,
        ICoreWorkingGroupObjectAttribute
    {
        public override string GroupName
        {
            get
            {
                return "androidGUI";
            }
        }
        public AndroidGUIObjectAttribute(string name, Type mecanism):base(name, mecanism)
        {

        }
    }
}
