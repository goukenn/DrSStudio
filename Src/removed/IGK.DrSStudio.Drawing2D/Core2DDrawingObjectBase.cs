

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Core2DDrawingObjectBase.cs
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
file:Core2DDrawingObjectBase.cs
*/
/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igkdev.be
Application : DrSStudio
powered by IGK - DEV &copy; 2008-2012
THIS FILE IS A PART OF THE DRSSTUDIO APPLICATION. SEE "License.txt"
FOR MORE INFORMATION ABOUT THE LICENSE
------------------------------------------------------------------- 
*/
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary ;
namespace IGK.DrSStudio.Drawing2D
{
    using IGK.ICore;using IGK.DrSStudio.Drawing2D;
    using IGK.DrSStudio.Codec ;
    using IGK.DrSStudio.WinUI ;
    using IGK.DrSStudio.Drawing2D.WinUI;
    using System.ComponentModel;
    using IGK.DrSStudio.WinUI.Configuration;
    [Serializable ()]
    public abstract class Core2DDrawingObjectBase:
        Core2DDrawingComponentObjectBase,
        ICore2DDrawingObject,
        ICoreSerializerService ,
        ICoreWorkingObjectPropertyEvent,
        ICoreDisposableObject ,
        ICore2DDrawingEditableElement ,
        ICloneable ,
        ICoreLoadableComponent,
        ICoreSerializerAdditionalPropertyService
    {
       public virtual void Edit(){
           try
           {
               CoreSystem.Instance.Workbench.ConfigureWorkingObject(this as ICoreWorkingConfigurableObject);
           }
           catch { 
           }
       }
    }
}

