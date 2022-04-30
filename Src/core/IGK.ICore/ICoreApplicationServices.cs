using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore
{
    public interface ICoreApplicationServices
    {
        /// <summary>
        /// get an application service
        /// </summary>
        /// <param name="name">name of the requested application</param>
        /// <returns></returns>
        ICoreApplicationService GetService(string name);
        /// <summary>
        /// get the application service
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T GetService<T>() where T : ICoreApplicationService ;
    }
}
