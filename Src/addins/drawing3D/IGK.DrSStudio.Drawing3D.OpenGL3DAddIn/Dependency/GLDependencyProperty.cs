

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GLDependencyProperty.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/

using IGK.ICore;
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
/*
IGKDEV - [2008 - 2014]
author: C.A.D . BONDJE DOUE
file:GLDependencyProperty.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.Drawing3D.OpenGL
{
    public class GLDependencyProperty
    {
        private System.Type m_PropertyType;
        private System.Type m_DeclaringType;
        private object m_DefaultValue;
        public object DefaultValue
        {
            get { return m_DefaultValue; }           
        }
        public System.Type DeclaringType
        {
            get { return m_DeclaringType; }
        }
        public System.Type PropertyType
        {
            get { return m_PropertyType; }
        }
        private string m_Name;
        public string Name
        {
            get { return m_Name; }
        }
        private GLDependencyProperty()
        {
        }
        //public override string ToString()
        //{
        //    return this.Name;
        //}
        internal static GLDependencyProperty Register(string name, Type propertyType, Type declaringType)
        {
            if (propertyType == null)
                throw new ArgumentException("propertyType");
            GLDependencyProperty v_dep = new GLDependencyProperty();
            v_dep.m_Name = name;
            v_dep.m_DeclaringType = declaringType;
            v_dep.m_PropertyType = propertyType;
            v_dep.m_DefaultValue = propertyType.Assembly.CreateInstance(propertyType.FullName);
            return v_dep;
        }
    }
}

