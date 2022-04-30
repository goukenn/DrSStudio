

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GLProperty.cs
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
file:GLProperty.cs
*/
using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.OGLGame
{
    /// <summary>
    /// represent a gl property
    /// </summary>
    public class GLProperty
    {
        private string m_Name;
        private bool m_IsDeviceProperty;
        private IGK.OGLGame.Graphics.OGLGraphicsDevice m_Device;
        private Dictionary<GLProperty, object > m_properties = new Dictionary<GLProperty,object> ();
        private object m_DefaultValue;
        public object DefaultValue
        {
            get { return m_DefaultValue; }
            internal   set
            {
                    m_DefaultValue = value;               
            }
        }
        public event GLPropertyChangedHandler PropertyChanged;
        public IGK.OGLGame.Graphics.OGLGraphicsDevice Device {
            get {
                return m_Device;
            }
        }
        public bool IsDeviceProperty
        {
            get { return m_IsDeviceProperty; }
        }
        public string Name
        {
            get { return m_Name; }
        }
        /// <summary>
        /// .ctr graphics device
        /// </summary>
        /// <param name="device"></param>
        public GLProperty(IGK.OGLGame.Graphics.OGLGraphicsDevice device)
        {
            this.m_Device = device;
            this.m_IsDeviceProperty = true;
        }
        /// <summary>
        /// .ctr default constructor
        /// </summary>
        public GLProperty()
        {
            this.m_Name = string.Empty;
            this.m_Device = null;
            this.m_IsDeviceProperty = false;
            this.RegisterProp();
        }
        protected virtual void OnPropertyChanged(GLPropertyChangedEventArgs  e)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, e);
        }
        private void RegisterProp()
        {
            System.Collections.IEnumerator e = GLPropertyRegister.GetProperties(GetType());
            if (e != null)
            {
                while (e.MoveNext())
                {
                    GLProperty c = e.Current as GLProperty;
                    this.m_properties.Add(c,c.DefaultValue);
                }
            }
        }
        public void SetValue(GLProperty gLProperty, object  value)
        {
            if (m_properties.ContainsKey(gLProperty))
            {
                if (m_properties[gLProperty] == value)
                    return;
                m_properties[gLProperty] = value;
            }
            else
                m_properties.Add(gLProperty, value);
            OnPropertyChanged(new GLPropertyChangedEventArgs(gLProperty));
        }
        public object GetValue(GLProperty gLProperty)
        {
            if (!this.IsDeviceProperty)
            {
                return m_properties[gLProperty];
            }
            return null;
        }
        public virtual void Bind(IGK.OGLGame.Graphics.OGLGraphicsDevice device) { 
        }
    }
}

