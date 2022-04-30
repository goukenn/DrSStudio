

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WebSolutionCategoryItem.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/

using IGK.ICore;
using IGK.ICore.Codec;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.DrSStudio;
using IGK.DrSStudio.Menu;
using IGK.DrSStudio.WinUI;
using IGK.DrSStudio.Drawing2D;
using IGK.DrSStudio.Drawing2D.WinUI;
﻿using IGK.ICore.WinUI.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Web
{
    /// <summary>
    /// represent a web solution category items
    /// </summary>
    public abstract class WebSolutionCategoryItem : ICoreWorkingConfigurableObject
    {
        private WebSolutionSolution m_WebSolution;
        private static List<Type> sm_categoryType;
        //private static object sm_CategoryType;
        /// <summary>
        /// get the current web solution
        /// </summary>
        public WebSolutionSolution WebSolution
        {
            get { return m_WebSolution; }
            internal set
            {
                if (m_WebSolution != value)
                {
                    m_WebSolution = value;
                }
            }
        }
        public string Name
        {
            get { 
                string s = GetType().Name ;
                int i = s.IndexOf("Category")-3;
                return s.Substring(3,
                    i);
                
            }         
        }

        public virtual int Index
        {
            get { return -1; }
         
        }
        /// <summary>
        /// get the list of registrated categories
        /// </summary>
        /// <param name="webSolutionSolution"></param>
        /// <returns></returns>
        public  static IEnumerable GetCategories(WebSolutionSolution webSolutionSolution)
        {

            if (sm_categoryType == null)
            {
                sm_categoryType = new List<Type>();
                //load categories
                foreach (Type t in Assembly.GetExecutingAssembly().GetTypes())
                { 
                    if (!t.IsAbstract  && t.IsSubclassOf (typeof (WebSolutionCategoryItem )))
                    {
                        sm_categoryType.Add (t);
                    }
                }

            }
            List<WebSolutionCategoryItem> e = new List<WebSolutionCategoryItem>();
            foreach (Type item in sm_categoryType)
            {
                WebSolutionCategoryItem cat =  item.Assembly.CreateInstance(item.FullName) as WebSolutionCategoryItem ;
                cat.m_WebSolution = webSolutionSolution;
                e.Add (cat);
            }
            e.Sort(new WebSolutionItemComparer());
            return e;
        }

        public virtual enuParamConfigType GetConfigType()
        {
            return enuParamConfigType.ParameterConfig;
        }

        public virtual ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
        {
            CoreParameterConfigUtility.LoadConfigurationUtility(this, parameters, this.GetType());
            return parameters;
        }

        public virtual ICoreControl GetConfigControl()
        {
            return null;
        }

        public string Id
        {
            get { throw new NotImplementedException(); }
        }
    }
}
