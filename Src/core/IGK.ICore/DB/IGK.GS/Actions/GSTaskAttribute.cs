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

namespace IGK.GS.Actions
{

    /// <summary>
    /// task action group
    /// </summary>
    [AttributeUsage (AttributeTargets.Class , AllowMultiple = false , Inherited = false )]
    public class GSTaskAttribute : GSActionAttribute
    {
        private string m_TaskGroup;

        public  int GetTaskGroupIndex() {
            return GetTaskGroupIndex(this.TaskGroup);   
        }

        public static int GetTaskGroupIndex(string name)
        {
            IGSApplication app = CoreApplicationManager.Application  as IGSApplication;
            if (app !=null)
            return app.GetTaskGroupIndex(name);
            return -1;
        }
        /// <summary>
        /// task group name
        /// </summary>
        public string TaskGroup
        {
            get { return m_TaskGroup; }
            set
            {
                if (m_TaskGroup != value)
                {
                    m_TaskGroup = value;
                }
            }
        }
        /// <summary>
        /// .Ctr
        /// </summary>
        /// <param name="name"></param>
        public GSTaskAttribute(string name, int index):base(name)
        {
            this.ImageKey  = "task_" + name;
            this.Index = index;
        }

        /// <summary>
        /// override tostring
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
