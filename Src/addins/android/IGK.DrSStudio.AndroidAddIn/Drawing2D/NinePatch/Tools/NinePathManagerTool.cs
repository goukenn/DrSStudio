using IGK.ICore;
using IGK.ICore.Drawing2D;
using IGK.ICore.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Android.Drawing2D.Tools
{
    [CoreTools("Android.NinePathManager")]
    class NinePathManagerTool
    {
        private static NinePathManagerTool sm_instance;
        private NinePathManagerTool()
        {
        }

        public static NinePathManagerTool Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static NinePathManagerTool()
        {
            sm_instance = new NinePathManagerTool();

        }
        public event EventHandler MustUpdate;
        ///<summary>
        ///raise the MustUpdate 
        ///</summary>
        protected virtual void OnMustUpdate(EventArgs e)
        {
            if (MustUpdate != null)
                MustUpdate(this, e);
        }

        internal void OnMustUpdate() {
            this.OnMustUpdate(EventArgs.Empty);
        }

       

        internal void Manage(ICore.ProjectElement v_project, AndroidNinePatchElement element, ICore2DDrawingLayer layer)
        {
            new NinePatchManagerItem(v_project, element, layer);
        }

        class NinePatchManagerItem {
            private ProjectElement v_project;
            private AndroidNinePatchElement element;
            private ICore2DDrawingLayer layer;

            public NinePatchManagerItem(ICore.ProjectElement v_project, AndroidNinePatchElement element, ICore2DDrawingLayer layer)
            {
                this.v_project = v_project;
                this.element = element;
                this.layer = layer;
                this.layer.ElementRemoved += layer_ElementRemoved;
                this.element.Disposed += element_Disposed;
            }

            void element_Disposed(object sender, EventArgs e)
            {
                //remove from project
                if (element.Parent == null)
                {
                    this.v_project.SetAttribute(AndroidConstant.ANDROID_DRAWING2D_NINEPATCHATTRIBUTE, null);
                }
            }

            void layer_ElementRemoved(object sender, ICore.CoreItemEventArgs<ICore2DDrawingLayeredElement> e)
            {
                if (e.Item == this.element)
                {
                    this.v_project.SetAttribute(AndroidConstant.ANDROID_DRAWING2D_NINEPATCHATTRIBUTE, null);
                }
            } 
        }
    }
}
