

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Core2DStandarItemAttribute.cs
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
file:Core2DStandarItemAttribute.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.Drawing2D
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false , Inherited =false ) ]
    public class Core2DStandarItemAttribute : Core2DDrawingGroupAttribute
    {
        public override string Environment
        {
            get { return "Drawing2DEnvironment"; }
        }
        public override string GroupImageKey
        {
            get { return "Default"; }
        }
        public override string GroupName
        {
            get { return "Default"; }
        }
        public Core2DStandarItemAttribute(string name, Type type): base(name,type)
        {
        }
    }
}

