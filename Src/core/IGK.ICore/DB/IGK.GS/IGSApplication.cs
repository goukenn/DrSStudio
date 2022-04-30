using IGK.ICore.Web;
using IGK.ICore.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.GS
{
    /// <summary>
    /// represent a gs application
    /// </summary>
    public interface IGSApplication : ICoreWorkbenchDocumentInitializer
    {
        byte[] GetResources(string name);

        int GetTaskGroupIndex(string name);

        void OnGetActions(string p, Actions.IGSActionCollections actions);
        void OnGetPrimaryActions(string p, Actions.IGSActionCollections actions);

        void addActionRequestListener(IGSActionRequestListener actions);
        void removeActionRequestListener(IGSActionRequestListener actions);
        void addPrimaryActionRequestListener(IGSActionRequestListener actions);
        void removePrimaryActionRequestListener(IGSActionRequestListener actions);
    }
}
