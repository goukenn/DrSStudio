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

namespace IGK.GS.Html
{
    /// <summary>
    /// represent the base controller class model
    /// </summary>
    public class HtmlController : IHtmlController
    {
        private HtmlItem m_TargetNode;        
        private HtmlControllerCollection m_Childs;
        public HtmlController m_parent;

        public HtmlControllerCollection Childs
        {
            get { return m_Childs; }
        }
        /// <summary>
        /// get the name of this controller
        /// </summary>
        public virtual string Name
        {
            get { return this.GetType().Name.ToLower(); }           
        }
        public HtmlItem TargetNode
        {
            get { return m_TargetNode; }
            set
            {
                if (m_TargetNode != value)
                {
                    m_TargetNode = value;
                }
            }
        }
        public HtmlController()
        {
            m_Childs = new HtmlControllerCollection(this);
            this.TargetNode = InitTargetNode();
            if (this.TargetNode != null)
            {
                this.TargetNode["class"] = this.Name.ToLower();
            }
        }
        protected virtual HtmlItem InitTargetNode()
        {
            HtmlItem c = HtmlItem.CreateWebNode("div");

            return c;

        }
        public virtual void View()
        {
            if (this.Visible)
            {
                this._showChild();
            }
            else {
                this.remove(this.TargetNode);
            }
        }

        private void remove(HtmlItem htmlItem)
        {
            if (htmlItem.Parent != null)
                htmlItem.Parent.Childs.Remove(htmlItem);
        }

        private void _showChild()
        {
            foreach (HtmlController item in this.m_Childs )
            {
                if (item.Visible)
                {
                    this.TargetNode.add(item.TargetNode);
                    item.View();
                }
            }
        }
        public virtual bool Visible
        {
            get { return true; }
        }

        public class HtmlControllerCollection : IHtmlControllerCollection
        {
            private List<HtmlController> m_controllers;
            private HtmlController m_owner;
            public HtmlControllerCollection(HtmlController owner)
            {
                this.m_owner = owner;
                this.m_controllers = new List<HtmlController>();
            }

            public void Add(HtmlController item)
            {
                if (this.m_controllers.Contains(item))
                    return;
                this.m_controllers.Add(item);
                item.m_parent = this.m_owner;
            }

            public void Remove(HtmlController item)
            {
                if (this.m_controllers.Contains(item))
                {
                    this.m_controllers.Remove(item);
                    item.m_parent = null;
                }
            }

            public System.Collections.IEnumerator GetEnumerator()
            {
                return this.m_controllers.GetEnumerator();
            }

            void IHtmlControllerCollection.Add(IHtmlController item)
            {
                this.Add(item as HtmlController);
            }

            void IHtmlControllerCollection.Remove(IHtmlController item)
            {
                this.Add(item as HtmlController);
            }
        }

        IHtmlControllerCollection IHtmlController.Childs
        {
            get { return this.Childs; }
        }
        /// <summary>
        /// get the current parent of this controller
        /// </summary>
        public IHtmlController Parent
        {
            get { return this.m_parent; }
        }
    }
}
