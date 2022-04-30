
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.DrSStudio.AudioVideo.Xml
{
    using IGK.ICore;
    using IGK.ICore.Xml;
    /// <summary>
    /// represent a default xml element base
    /// </summary>
    public abstract class AVISequenceItemBase : CoreXmlElement
    {
        protected  AVISequenceItemBase(string p):base(p)
        {
        }
        public AVISequenceItemBase()
        {

        }
        protected override string getLoadStringNamespace()
        {
            return string.Format ("{0} {1}",
                string.Format("xmlns:igkdevvids=\"{0}\"", AVISequenceConstant.NAMESPACE),
                base.getLoadStringNamespace());
        }
    }
}
