

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreWorkingElementChangedEventArgs.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:CoreWorkingElementChangedEventArgs.cs
*/
using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore
{
    public delegate void CoreWorkingElementChangedEventHandler<T>(object o, CoreWorkingElementChangedEventArgs<T>  e) where T :class  ;
    public delegate void CoreWorkingElementChangedEventHandler(object o, CoreWorkingElementChangedEventArgs e);
    /// <summary>
    /// used to register element changed
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CoreWorkingElementChangedEventArgs<T> : EventArgs
    {
        private T m_OldElement;
        private T m_NewElement;
        public T NewElement
        {
            get { return m_NewElement; }
        }
        public T OldElement
        {
            get { return m_OldElement; }
        }
        public CoreWorkingElementChangedEventArgs(T oldElement, T newElement)
        {
            this.m_OldElement = oldElement;
            this.m_NewElement = newElement;
        }
    }
    public class CoreWorkingElementChangedEventArgs : CoreWorkingElementChangedEventArgs<ICoreWorkingObject>
    {
        public CoreWorkingElementChangedEventArgs(ICoreWorkingObject old, ICoreWorkingObject newObject):base(old, newObject)
        {
        }
    }
}

