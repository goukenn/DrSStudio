

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WinCoreApplication.cs
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
file:WinCoreApplication.cs
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace IGK.DrSStudio
{
    using IGK.ICore.WinCore;
    using IGK.DrSStudio.Drawing2D;
    using IGK.DrSStudio.Tools;
    using IGK.DrSStudio.WinUI;
    using IGK.ICore;
    using IGK.ICore.ComponentModel;
    using IGK.ICore.Drawing2D;
    using IGK.ICore.IO;
    using IGK.ICore.WinUI;
    using IGK.ICore.WinCore.Tools;
    /// <summary>
    /// Basic windows Application for Windows Application 
    /// </summary>
    public abstract class DrSStudioWinCoreApp : WinCoreApplication, ICoreApplication 
    {
        private ObjRef m_objRef;
        private TcpServerChannel m_tcpChannel;

        public DrSStudioWinCoreApp()
        {        
        }
        /// <summary>
        /// override this method to initialize custum dictionnary collection type for exemple
        /// </summary>
        /// <param name="instance"></param>
        public override void Register(CoreSystem instance)
        {
        }
        public override  void Initialize()
        {
            base.Initialize();          
            //register for windows ie 11 service
            WinCoreService.RegisterIE11WebService();
        }
        public override void Close()
        {
            Application.Exit();
        }
        public override  event EventHandler ApplicationExit
        {
            add
            {
                Application.ApplicationExit += value;
            }
            remove
            {
                Application.ApplicationExit -= value;
            }
        }
        public override  string CurrentWorkingPath
        {
            get { return Environment.CurrentDirectory; }
        }
        public override string StartupPath
        {
            get { return Application.StartupPath; }
        }
        protected override   ICoreControlManager CreateControlManager()
        {
            return new WinCoreControlManager();
        }
        protected override ICoreBrushRegister CreateBrushRegister()
        {
            return new WinCoreBrushRegister();
        }
        protected override ICoreResourceManager CreateResourceManager()
        {
            return new WinCoreResourceManager();
        }
        Dictionary<ICoreMessageFilter, WinCoreMessageFilter> m_filters;

        public override void AddMessageFilter(ICoreMessageFilter messageFilter)
        {
            if (m_filters == null)
            {
                m_filters = new Dictionary<ICoreMessageFilter, WinCoreMessageFilter>();
            }
            if (!m_filters.ContainsKey(messageFilter))
            {
                m_filters.Add(messageFilter, new WinCoreMessageFilter(messageFilter));
                Application.AddMessageFilter(m_filters[messageFilter]);
            }

        }
        
        public override void RemoveMessageFilter(ICoreMessageFilter messageFilter)
        {
            if (m_filters.ContainsKey(messageFilter))
            {
                Application.RemoveMessageFilter(m_filters[messageFilter]);
                m_filters.Remove(messageFilter);
            }
        }
   
        public override string UserAppDataPath
        {
            get {
                return System.Windows.Forms.Application.UserAppDataPath;
            }
        }
     
        public override bool RegisterClientSystem(Func<CoreSystem> __initInstance)
        {
            try
            {
                TcpClientChannel channel = new TcpClientChannel();
                ChannelServices.RegisterChannel(channel, false);
                // sm_instance = (CoreSystem)Activator.GetObject(typeof(CoreSystem), "tcp://localhost:3088/DDD");
                WellKnownClientTypeEntry v_entry = new WellKnownClientTypeEntry(typeof(CoreSystem),
       string.Format("tcp://localhost:{0}/{1}", CoreConstant.APP_CHANNEL_PORT, CoreConstant.CHANNEL_NAME));
                RemotingConfiguration.RegisterWellKnownClientType(v_entry);
                //retrieve a new element
                __initInstance();                
                return true;
            }
            catch
            {
            }
            return false;
        }
        public override bool RegisterServerSystem(Func<CoreSystem> __initInstance)
        {
            try
            {
                //TcpChannel channel = new TcpChannel(3088);
                //ChannelServices.RegisterChannel(channel, false);
                //RemotingConfiguration.RegisterWellKnownServiceType(typeof(CoreSystem), "DDD", WellKnownObjectMode.Singleton);
                //sm_instance = new CoreSystem();
                //RemotingServices.Marshal(sm_instance);
                //return true;
                m_tcpChannel = new TcpServerChannel(CoreConstant.APP_CHANNEL_PORT);
                ChannelServices.RegisterChannel(m_tcpChannel, false);
                CoreSystem c = __initInstance();
                //register well know type
                this.m_objRef =  RemotingServices.Marshal(
                    c,
                    CoreConstant.CHANNEL_NAME);
                Application.ApplicationExit += _ApplicationExit;
                return true;
            }
            catch(Exception ex)
            {
                CoreLog.WriteLine(ex.Message);
            }
            return false;
        }

        public override ICoreSystem GetSystem()
        {
            var s = (ICoreSystem)RemotingServices.Unmarshal(this.m_objRef);
            return s;
        }

        void _ApplicationExit(object sender, EventArgs e)
        {
            object obj = RemotingServices.Unmarshal(this.m_objRef);
            if (m_tcpChannel != null)
            {
                ChannelServices.UnregisterChannel(m_tcpChannel);
                m_tcpChannel = null;
            }

        }
      
    }
}

