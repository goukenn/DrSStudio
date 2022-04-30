using IGK.ICore;
using IGK.ICore.Codec;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.ICore.WinUI.Configuration;
using IGK.ICore.Settings;







using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.ICore.Actions;
using IGK.GS.WinUI;

namespace IGK.GS.Actions
{
    /// <summary>
    /// represent the base GS Actions
    /// </summary>
    public abstract class GSActionBase : CoreActionBase
    {
        private object  m_Param;
        private string m_imageKey; //image key info
        private object m_Response;
        /// <summary>
        /// get of this action after exécution
        /// </summary>
        public object Response
        {
            get { return m_Response; }
            internal protected set
            {
                if (m_Response != value)
                {
                    m_Response = value;
                }
            }
        }
        /// <summary>
        /// get or set the parameter for the perform action task
        /// </summary>
        public object  Param
        {
            get { return m_Param; }
            set
            {
                if (m_Param != value)
                {
                    m_Param = value;
                }
            }
        }

        /// <summary>
        /// set the refreshing listerner of this object
        /// </summary>
        /// <param name="listener"></param>
        public IGSRefreshViewListener RefreshListener { get; set; }

        protected int GetParamInt(int p)
        {
            if (Param is object[])
            {
                var t = (object[])Param;
                return Convert.ToInt32(t[0]);
            }
            else if (Param is string)
            {
                try
                {
                    return Convert.ToInt32(Param.ToString().Split(' ')[0]);
                }
                catch { 

                }
            }

            return 0;
        }
        protected T GetParam<T>(int p)
        {
            if (this.Param is T)
                return (T)this.Param;
            return default(T);
        }

        public GSActionBase()
        {

        }
        /// <summary>
        /// retreive the application GS System
        /// </summary>
        internal protected GSSystem GSSystem
        {
            get
            {
                return GSSystem.Instance;
            }
        }
        /// <summary>
        /// get the main form
        /// </summary>
        public IGSMainForm MainForm
        {
            get
            {
                return this.GSSystem.MainForm;
            }
        }       
        public virtual bool IsAvailable
        {
            get
            {
                return true;
            }
        }
        /// <summary>
        /// get the image key of the action
        /// </summary>
        public override  string ImageKey
        {
            get
            {
                if (string.IsNullOrEmpty(this.m_imageKey))
                {
                    GSActionAttribute c = GSTaskAttribute.GetCustomAttribute(this.GetType(), typeof(GSActionAttribute)) as GSActionAttribute;
                    if (c != null)
                        return c.ImageKey;
                }
                return this.m_imageKey;
            }
            set { 
                //do nothing
                this.m_imageKey = value;
            }
        }
        public virtual int Index
        {
            get
            {
                GSActionAttribute c = GSTaskAttribute.GetCustomAttribute(this.GetType(), typeof(GSActionAttribute)) as GSActionAttribute;
                if (c != null)
                    return c.Index;
                return -1;
            }
        }
     
        protected override bool PerformAction()
        {
            if (CoreSystemEnvironment.IsInConsoleMode )
            {
                return  NonInterfaceAction();
            }
            return false;
        }
        /// <summary>
        /// call non interactive method
        /// </summary>
        protected virtual bool NonInterfaceAction()
        {
            return false; 
        }
        /// <summary>
        /// determine if this action have the authorization access.
        /// call this method externally to check if you will have the authorization.
        /// Not that perform action must call this method first to garantees the correspondance with
        /// the actual grand access.
        /// </summary>
        /// <returns></returns>
        public virtual bool Grant() {
            return true;
        }

        public override string Id
        {
            get { return GSActionAttribute.GetName(this.GetType()); }
        }

        internal void InitAttribute(GSActionAttribute attr)
        {
            
        }
        
    }
}
