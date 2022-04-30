using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.Resources
{
    class SoundFileResource : FileResourceItem
    {
        public override enuCoreResourceType ResourceType
        {
            get { return enuCoreResourceType.SoundFile; }
        }
    }
}
