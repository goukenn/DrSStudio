
using IGK.ICore;using IGK.ICore.Xml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace IGK.DrSStudio.Android.Entities
{
    /// <summary>
    /// represent a android entities base class
    /// </summary>
    public class AndroidEntities
    {
        public virtual string EntityTypeName{
            get {
                return AndroidConstant.ANDROID_ENTITIES;
            }
        }
       

        public AndroidEntities()
        {

        }
    }
}
