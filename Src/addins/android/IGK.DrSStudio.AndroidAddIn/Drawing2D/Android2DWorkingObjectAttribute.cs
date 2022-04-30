using IGK.ICore;
using IGK.ICore.Drawing2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.DrSStudio.Android.Drawing2D
{
    [AttributeUsage (AttributeTargets.Class , Inherited = false , AllowMultiple =false )]
    class Android2DWorkingObjectAttribute : Core2DDrawingStandardElementAttribute
    {
        public override string GroupName
        {
            get
            {
                return AndroidConstant.ANDROID_DRAWING2D_GROUP_NAME;
            }
        }

        public override string NameSpace
        {
            get
            {
                return AndroidConstant.ANDROID_DRAWING2D_NAMESPACE;
            }
        }
        public Android2DWorkingObjectAttribute(string name, Type mecanism):base(name, mecanism )
        {

        }
    }
}
