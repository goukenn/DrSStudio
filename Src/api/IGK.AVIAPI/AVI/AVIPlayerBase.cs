

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AVIPlayerBase.cs
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
file:AVIPlayerBase.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading ;
using System.Windows.Forms;
namespace IGK.AVIApi.AVI
{
    using IGK.ICore;using IGK.AVIApi.MMIO;
    using IGK.AVIApi.MCI;
    using IGK.AVIApi.AVI;
    /// <summary>
    /// represent the base abstract Player
    /// </summary>
    public abstract class AVIPlayerBase : 
        System.ComponentModel.ISynchronizeInvoke , 
        IAVIPlayer
        , IDisposable 
    {
        AVIStream m_BaseStream;
        private IntPtr m_mmioHandle;
        private Thread m_currentThread;
        private SynchronizationContext m_syncContext;
        private Control m_Parent;
        protected IntPtr MmioHandle {
            get { return this.m_mmioHandle; }
            set { this.m_mmioHandle = value; }
        }
        public Control Parent
        {
            get { return m_Parent; }
            set
            {
                if (m_Parent != value)
                {
                    m_Parent = value;
                }
            }
        }
        public AVIStream BaseStream {
            get {
                return m_BaseStream;
            }
            protected set {
                this.m_BaseStream = value;
            }
        }
        protected AVIPlayerBase()
        {
            this.m_currentThread = System.Threading.Thread.CurrentThread;
            this.m_syncContext = System.ComponentModel.AsyncOperationManager.SynchronizationContext;            
        }
        public IAsyncResult BeginInvoke(Delegate method, object[] args)
        {
            if (this.m_Parent != null)
            {
            return this.m_Parent.BeginInvoke(method, args);
            }
            return null;
        }
        public object EndInvoke(IAsyncResult result)
        {
            if (this.m_Parent != null)
                this.m_Parent.EndInvoke(result);
            return null;
        }
        public object Invoke(Delegate method, object[] args)
        {
            if (this.m_Parent != null)
                this.m_Parent.Invoke(method, args);
            return null;
        }
        public bool InvokeRequired
        {
            get {
                if (this.Parent != null)
                    return this.Parent.InvokeRequired;
                return (this.m_currentThread != Thread.CurrentThread);
            }
        }
        string m_Name;
        //System.Threading.Thread  thread;
        public string Name
        {
            get { return this.m_Name; }
            protected set { this.m_Name =value  ; }
        }
         public virtual void Play()
        {
            MCIManager.Play(this.Name);
        }
         //void mplay()
         //{
         //    MCIManager.Play(this.Name);
         //    while (true)
         //    {
         //        System.Threading.Thread.Sleep(20);
         //    }
         //}
        public virtual void Pause()
        {
            MCIManager.Pause(this.Name,false ,true );        
        }
        public virtual void Stop() {
            MCIManager.Stop(this.m_Name,false ,true );
        }
        public virtual  void Close()
        {
            this.Stop();
            try
            {
                if (this.MmioHandle != IntPtr.Zero)
                {
                    MMIO.MMIOManager.Close(this.MmioHandle, true);
                    this.MmioHandle = IntPtr.Zero;
                }
            }
            catch
            {
            }
            finally
            {
                this.m_mmioHandle = IntPtr.Zero;
            }
        }
        public virtual void Dispose()
        {
        }
    }
}

