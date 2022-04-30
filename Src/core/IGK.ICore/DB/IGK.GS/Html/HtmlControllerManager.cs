using IGK.ICore;
using IGK.ICore.Codec;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.ICore.WinUI.Configuration;
using IGK.ICore.Settings;






﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace IGK.GS.Html
{
    public class HtmlControllerManager
    {
        private GSDocument gSDocument;
        Dictionary<string, HtmlController> m_controllers;
        static List<Type> sm_regcontrollers;
        public HtmlControllerManager(GSDocument gSDocument)
        {
            this.gSDocument = gSDocument;
            this.m_controllers = new Dictionary<string, HtmlController>();
            this.initController();
        }
        static HtmlControllerManager() { 

               //load assembly
            sm_regcontrollers = new List<Type>();
            foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                LoadAssembly(asm);
             
            }
            AppDomain.CurrentDomain.AssemblyLoad += CurrentDomain_AssemblyLoad;
        }

        static void CurrentDomain_AssemblyLoad(object sender, AssemblyLoadEventArgs args)
        {
            LoadAssembly(args.LoadedAssembly);
        }

        private static void LoadAssembly(Assembly asm)
        {
            foreach (Type t in asm.GetTypes())
            {
                if (t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(HtmlController)))
                {
                    HtmlControllerAttribute r = Attribute.GetCustomAttribute(t, typeof(HtmlControllerAttribute)) as
                        HtmlControllerAttribute;
                    if ((r != null) && (r.IsController))
                    {
                        sm_regcontrollers.Add(t);
                    }
                }
            }
        }

        private void initController()
        {
            foreach ( Type item in sm_regcontrollers)
            {
                HtmlController c = item.Assembly.CreateInstance(item.FullName) 
                    as HtmlController;
                if (c != null) {
                    this.m_controllers.Add(c.Name, c);
                }
            }
        }
        public void ViewController()
        {
            foreach (HtmlController item in this.m_controllers.Values)
            {
                if ((item.Parent == null) && (item.Visible))
                {
                    this.gSDocument.Body.add(item.TargetNode);
                    item.View();
                }
            }
        }
    }
}
