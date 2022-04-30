

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _PresentationMenu.cs
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
file:_PresentationMenu.cs
*/
/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igkdev.be
Application : DrSStudio
powered by IGK - DEV &copy; 2008-2012
THIS FILE IS A PART OF THE DRSSTUDIO APPLICATION. SEE "License.txt"
FOR MORE INFORMATION ABOUT THE LICENSE
------------------------------------------------------------------- 
*/
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Reflection;
using System.IO;
namespace IGK.DrSStudio.Presentation.Menu
{
    using IGK.ICore.WinCore;
    using IGK.ICore;
    using IGK.DrSStudio.WinUI;
    using IGK.DrSStudio.Drawing2D.WinUI;
    using IGK.DrSStudio.Drawing2D;
    using System.Threading;
    using IGK.ICore.Menu;
    using IGK.ICore.WinUI;
    using IGK.ICore.Drawing2D;
    using IGK.ICore.Drawing2D.WinUI;
    using IGK.ICore.Codec;
    [Serializable ()]
    [CoreMenu ("Tools.Presentation", 40,
        Shortcut=enuKeys.Control | enuKeys.F5 ,
        ImageKey=CoreImageKeys.MENU_PRESENTATION_GKDS)]
    class _PresentationMenu : CoreApplicationMenu 
    {
        DomLauncher m_currentDomLauncher;
        /// <summary>
        /// perform presentation call back
        /// </summary>
        /// <returns></returns>
        protected override bool PerformAction()
        {
            if (m_currentDomLauncher != null)
            {
               // m_currentDomLauncher.dom.DoCallBack(PresentationProgram.CloseApplication);               
                m_currentDomLauncher = null;               
            }
            
            ICore2DDrawingDocument[] doc = null;
            string args = string.Empty;
            //serialize document
            if (this.Workbench.CurrentSurface is ICore2DDrawingSurface)
            {
                doc = (this.Workbench.CurrentSurface as ICore2DDrawingSurface).Documents.ToArray().ConvertTo<ICore2DDrawingDocument>();
                MemoryStream mem = null;
                try
                {
                    mem = new MemoryStream();
                    if (CoreEncoder.Instance.Save(                        
                        mem,
                        null,
                        doc))
                    {
                        mem.Seek(0, SeekOrigin.Begin);
                        StreamReader sr = new StreamReader(mem);
                        args = sr.ReadToEnd();
                        sr.Close();
                    }
                }
                catch
                {
                    //exception not treated
                }
                finally {
                    if (mem != null)
                    {
                        mem.Dispose();
                    }
                }
            }
            //run 
            DomLauncher c = new DomLauncher();
            c.args = args;
            c.owner = this;                
            m_currentDomLauncher = c;
            Thread th = new Thread(c.RunDomain);
            th.SetApartmentState(ApartmentState.STA);
            th.IsBackground = false;
            c.m_thread = th;
            th.Start();            
            return false;
        }
        /// <summary>
        /// represent the domain launcher
        /// </summary>
        sealed class DomLauncher
        {
            internal AppDomain dom;
            internal string args;
            internal _PresentationMenu owner;
            internal Thread m_thread;
            internal void RunDomain()
            {
                AppDomain dom = owner.InitDomain();
                this.dom = dom;
                //execute assembly
                //----------------
                try
                {                   
                    string location = Assembly.GetExecutingAssembly().FullName;
                    dom.ExecuteAssemblyByName(location, args);
                    AppDomain.Unload(dom);
                }
                catch (Exception ex)
                {
                    CoreLog.WriteDebug("Exception on : " + ex.Message);
                }
                this.owner.m_currentDomLauncher = null;
            }
            internal void Abort()
            {
                this.m_thread.Abort();                
                this.m_thread.Join();
            }
        }
        private AppDomain InitDomain()
        {
            AppDomainSetup setup = new AppDomainSetup();
            setup.ApplicationName = PresentationConstant.DOMAIN_NAME;
            string v_location = Assembly.GetExecutingAssembly().Location;
            if (File.Exists(v_location ))
            {
            setup.ApplicationBase = System.IO.Path.GetDirectoryName(v_location);
            }
            setup.PrivateBinPath = "AddIn;Lib";
            AppDomain dom = AppDomain.CreateDomain(PresentationConstant.DOMAIN_NAME, null, setup);
            dom.AssemblyResolve += new ResolveEventHandler(dom_AssemblyResolve);
            dom.ResourceResolve += new ResolveEventHandler(dom_ResourceResolve);
            dom.TypeResolve += new ResolveEventHandler(dom_TypeResolve);
            return dom;
        }        
        static Assembly dom_TypeResolve(object sender, ResolveEventArgs args)
        {
            return null;
        }
        static Assembly dom_ResourceResolve(object sender, ResolveEventArgs args)
        {
            return null;
        }
        static Assembly dom_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            return null;
        }
    }
}

