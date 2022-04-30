using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore
{
    /// <summary>
    /// represent an object that must be initialize by the CoreWorkingObjectCollections.CreateObject method
    /// </summary>
    public interface ICoreWorkingInitializableObject : ICoreWorkingObject 
    {
        /// <summary>
        /// initialize the working object
        /// </summary>
        void Initialize();
    }
}
