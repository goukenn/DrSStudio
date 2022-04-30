

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKXWinCoreWorkingSurface.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
using IGK.ICore;
using IGK.ICore.WinUI;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:XWinCoreWorkingSurface.cs
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.ICore.WinCore.WinUI.Controls;
namespace IGK.ICore.WinCore.WinUI.Controls
{
    /// <summary>
    /// represent the default working surface
    /// </summary>
    public class IGKXWinCoreWorkingSurface : 
        IGKXUserControl ,
        ICoreWorkingSurface , 
        ICoreIdentifier ,
        ICoreWorkingObjectPropertyEvent 
    {

        
        private string m_Title;//title to show
        private ICoreWorkingSurface m_CurrentChild;
        private ICoreWorkingSurface m_parentSurface;
        private ParameterCollection m_params;


        protected ParameterCollection Param {
            get {
                if (m_params == null)
                    m_params = CreateParameterCollection();
                return m_params;
            }
            }

        protected ParameterCollection CreateParameterCollection()
        {
            return new ParameterCollection(this);
        }

        
        protected  class ParameterCollection
        {
            Dictionary<string,string> m_params;
            private IGKXWinCoreWorkingSurface m_owner;
            public ParameterCollection(IGKXWinCoreWorkingSurface owner) {
                this.m_params = new Dictionary<string, string>();
                this.m_owner = owner;
            }

            public virtual  bool Contains(string name)
            {
                return this.m_params.ContainsKey(name);
            }
            public virtual string this[string key] {
                get {
                    return this.m_params[key];
                }
                set {
                    if (this.m_params.ContainsKey(key))
                        this.m_params[key] = value;
                    else
                        this.m_params.Add(key, value);
                }
            } 
        }

        public void SetParam(string name, string value) {
            this.Param[name] = value;
        }
        public string GetParam(string name)
        {
            if (this.Param.Contains(name))
                return this.Param[name ];
            return null;
        }
        /// <summary>
        /// get the project element
        /// </summary>
        /// <returns></returns>
        public  virtual ProjectElement GetProjectElement()
        {
            var s = (this as ICoreWorkingProjectManagerSurface);
            if ((s == null) || (s.GkdsElement == null))
                return null;
            return s.GkdsElement.GetProject();
        }
        /// <summary>
        /// get or set the current child
        /// </summary>
        public virtual ICoreWorkingSurface CurrentChild
        {
            get { return m_CurrentChild; }
            set
            {
                if (m_CurrentChild != value)
                {
                    m_CurrentChild = value;
                }
            }
        }

        public IGKXWinCoreWorkingSurface()
        {
        }

        /// <summary>
        /// get the surface that is the parent of this working surface
        /// </summary>
        public virtual ICoreWorkingSurface ParentSurface {
            get { 
                return this.m_parentSurface; 
            }
            set {
                this.m_parentSurface = value;
            }
        }
        [Browsable (false)]
        public string SurfaceEnvironment
        {
            get {
                CoreSurfaceAttribute attr = Attribute.GetCustomAttribute(GetType(), typeof(CoreSurfaceAttribute)) as CoreSurfaceAttribute;
                if (attr !=null)
                {
                    return attr.EnvironmentName;
                }
                return null;
            }
        }
        [Browsable(false)]
        /// <summary>
        /// get or set the title of this drawing surface
        /// </summary>
        public virtual string Title
        {
            get { return this.m_Title; }
            protected set {
                if (this.m_Title != value)
                {
                    this.m_Title = value;
                    OnTitleChanged(EventArgs.Empty);
                }
            }
        }

        public event EventHandler TitleChanged;

        protected virtual void OnTitleChanged(EventArgs eventArgs)
        {
            if (this.TitleChanged != null)
                this.TitleChanged(this, eventArgs);
        }


        [Browsable(false )]
        [DesignerSerializationVisibility (DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable (  EditorBrowsableState.Never )]
        public string Id
        {
            get { return this.Name; }
        }
        public event CoreWorkingObjectPropertyChangedEventHandler PropertyChanged;
   
        protected virtual void OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
            {
                this.PropertyChanged(this, e);
            }
        }
        public virtual  void SetParam(ICoreInitializatorParam p)
        {
        }


        public virtual  bool CanProcess(ICoreMessage msg)
        {
            return msg.HWnd == this.Handle;
        }
    }
}

