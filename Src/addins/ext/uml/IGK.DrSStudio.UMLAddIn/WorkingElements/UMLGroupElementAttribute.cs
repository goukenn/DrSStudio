

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: UMLGroupElementAttribute.cs
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
file:UMLGroupElementAttribute.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.UMLAddIn.WorkingElements
{
    
using IGK.ICore;
    using IGK.ICore.Drawing2D;
    [AttributeUsage (AttributeTargets.Class, AllowMultiple=false, Inherited=false)]
    /// <summary>
    /// represent a base UML group
    /// </summary>
    public class UMLGroupElementAttribute : Core2DDrawingGroupAttribute 
    {
        public UMLGroupElementAttribute(string name, Type mecanism)
            : base("UML"+name, mecanism)
        {
        }
        public override string GroupName
        {
            get { return "UML"; }
        }
        public override string Environment
        {
            get { return CoreConstant.DRAWING2D_ENVIRONMENT; }
        }
        public override string GroupImageKey
        {
            get { return "DE_UMLGroup"; }
        }
    }
}

