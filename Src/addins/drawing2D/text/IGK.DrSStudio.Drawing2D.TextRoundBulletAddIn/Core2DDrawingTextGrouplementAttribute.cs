
using IGK.ICore;
using IGK.ICore.Drawing2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.DrSStudio.Drawing2D
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false , AllowMultiple =false )]
    public class Core2DDrawingTextGrouplementAttribute : Core2DDrawingStandardElementAttribute  
    {
        public override string GroupName
        {
            get { return CoreConstant.GROUP_TEXT;  }
        }
        public Core2DDrawingTextGrouplementAttribute(string name, Type mecanism):base(name , mecanism )
        {

        }
    }
}
