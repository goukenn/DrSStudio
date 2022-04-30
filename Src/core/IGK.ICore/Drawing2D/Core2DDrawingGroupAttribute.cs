

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Core2DDrawingGroupAttribute.cs
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
file:Core2DDrawingGroupAttribute.cs
*/

﻿using IGK.ICore;using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore.Drawing2D
{
    /// <summary>
    /// represent the abstract group element
    /// </summary>
    public abstract class Core2DDrawingGroupAttribute :
        Core2DDrawingObjectAttribute ,
        ICoreWorkingGroupObjectAttribute,
        ICoreWorkingDocumentContainerName,
        ICoreWorkingLayerContainerName,
        ICoreWorkingDesignerAttribute
    {
        public abstract string GroupName { get; }
        public abstract string Environment { get; }
        private Type m_MecanismType;
        private bool m_IsVisible;
        private enuKeys m_Keys;
        /// <summary>
        /// get or set the type that this element can force to Edit. by default element is edited by the is own mecanism
        /// </summary>
        public Type[] Edition { get; set; }

        public virtual string DefaultDocumentName { 
            get{
                return CoreConstant.LAYEREDDOCUEMENT;
            }
        }
        public virtual string DefaultLayerName {
            get {
                return CoreConstant.LAYER;
            }
        }
        /// <summary>
        /// get the group image key of this group
        /// </summary>
        public abstract string GroupImageKey
        {
            get;
        }
        /// <summary>
        /// get or set if the element is visible or not
        /// </summary>
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
        public Type MecanismType
        {
            get { return m_MecanismType; }
            set { m_MecanismType = value; }
        }
        public Core2DDrawingGroupAttribute(string name, Type type):base(name )
        {
            this.m_MecanismType = type;
            this.m_IsVisible = true;
        }
        #region ICoreWorkingGroupObjectAttribute Members
        public enuKeys Keys
        {
            get
            {
                return this.m_Keys;
            }
            set
            {
                this.m_Keys = value;
            }
        }
        #endregion
    }
}

