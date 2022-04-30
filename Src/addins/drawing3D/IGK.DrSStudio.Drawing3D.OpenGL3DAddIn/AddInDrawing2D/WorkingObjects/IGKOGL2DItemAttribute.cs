

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKOGL2DItemAttribute.cs
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.ICore.Resources;

namespace IGK.DrSStudio.Drawing2D.OpenGL
{
    [AttributeUsage(AttributeTargets.Class , Inherited =false , AllowMultiple = false )]
    public class IGKOGL2DItemAttribute : Core2DDrawingObjectAttribute, 
        ICoreWorkingGroupObjectAttribute, ICoreWorkingObjectAttribute
    {
        public virtual string GroupName
        {
            get
            {
                return "OpenGLDrawing2DGroup";
            }
        }
        public virtual string Environment
        {
            get
            {
                return CoreConstant.DRAWING2D_ENVIRONMENT;
            }
        }
        private Type m_MecanismType;

        public Type MecanismType
        {
            get { return m_MecanismType; }
         
        }
        public IGKOGL2DItemAttribute(string name, Type mecanism):base(name)
        {
            this.m_MecanismType = mecanism;
            this.m_IsVisible = true;
        }
        public override string ImageKey
        {
            get
            {
                return base.ImageKey;
            }

            set
            {
                if (!string.IsNullOrEmpty (value )){
                    value = CoreResources.GetResourceId(this.GetType().Assembly, value );
                }
                base.ImageKey = value;
            }
        }

        public virtual string GroupImageKey
        {
            get { return ""; }
        }
        private bool m_IsVisible;

        public bool IsVisible
        {
            get { return m_IsVisible; }
            set
            {
                if (m_IsVisible != value)
                {
                    m_IsVisible = value;
                }
            }
        }
        private enuKeys m_Keys;

        public enuKeys Keys
        {
            get { return m_Keys; }
            set
            {
                if (m_Keys != value)
                {
                    m_Keys = value;
                }
            }
        }

       
    }
}
