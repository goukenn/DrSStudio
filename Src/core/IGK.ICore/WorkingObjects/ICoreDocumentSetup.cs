using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore
{
    /// <summary>
    /// used to setup a document with a working object
    /// </summary>
    public interface ICoreDocumentSetup : ICoreWorkingDocument 
    {
        void SetupDocument(ICoreWorkingObject obj);
    }
}
