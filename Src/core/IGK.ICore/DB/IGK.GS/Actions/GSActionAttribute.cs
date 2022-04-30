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
using System.Text;
using System.Threading.Tasks;
using IGK.ICore.Actions;

namespace IGK.GS.Actions
{
    /// <summary>
    /// attribute used to register actions.
    /// </summary>
    [AttributeUsage (AttributeTargets.Class , 
        Inherited =false , AllowMultiple = true)]
    public class GSActionAttribute : CoreActionAttribute 
    {
        private string m_ImageKey;
        private int m_Index;
        private string m_Autorisation;

        /// <summary>
        /// get or set the autorisation for this action. it's a semi-column separated autorisation model.
        /// </summary>
        public string Autorisation
        {
            get { return m_Autorisation; }
            set
            {
                if (m_Autorisation != value)
                {
                    m_Autorisation = value;
                }
            }
        }

       
        public int Index
        {
            get { return m_Index; }
            set
            {
                if (m_Index != value)
                {
                    m_Index = value;
                }
            }
        }

      

        public GSActionAttribute(string name):base(name)
        {            
            this.m_ImageKey = "GSCompanyName".Configs()+"_" + name;
        }

        internal static string GetName(Type type)
        {
            GSActionAttribute c = Attribute.GetCustomAttribute(type, typeof(GSActionAttribute)) as GSActionAttribute;
            if (c == null)
                return null;
            return c.Name;
        }
    }
}
