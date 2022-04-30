using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.WinUI.Dispatch
{
    /// <summary>
    /// represent an environment dispatcher event mecanism
    /// </summary>
    public interface ICoreDispatcher
    {
        /// <summary>
        /// get the number or registrated event
        /// </summary>
        int Count { get; }
        /// <summary>
        /// clear the registrated list
        /// </summary>
        void Clear();
        string[] GetRegistrableEventNames();
        ICoreDispatcherInfo GetDispatcherInfo(string name);
        /// <summary>
        /// register a dispatcheable event. 
        /// </summary>
        /// <param name="wObject">cibling working object</param>
        /// <param name="eventName">event name to register. must be a name from RegistrableEventNames List</param>
        /// <param name="delegate">delage that will be call if event raised</param>
        /// <returns>true if registrated</returns>
        bool Register(ICoreWorkingObject wObject, string eventName, MulticastDelegate @delegate);
        /// <summary>
        /// unregister the a dispacheable event
        /// </summary>
        /// <param name="wObject">cibling working object</param>
        /// <param name="eventName">event name</param>
        /// <param name="delegate">remove the delegate. this can be null to remove all registrated event click</param>
        void UnRegister(ICoreWorkingObject wObject, string eventName, MulticastDelegate @delegate);

        /// <summary>
        /// unregister all event that concern this working object
        /// </summary>
        /// <param name="wObject">working object that attached to event</param>
        void UnRegister(ICoreWorkingObject wObject);

        void Invoke(string eventName, EventArgs e);
    }
}
