using IGK.GS.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.GS
{
    public interface  IGSActionRequestListener
    {
        void GetActions(IGSActionCollections actionCollections);
    }
}
