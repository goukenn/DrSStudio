

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AcceleratorPropertyTool.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/

using IGK.ICore.WinCore;

using IGK.ICore;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.DrSStudio;
using IGK.DrSStudio.Menu;
using IGK.DrSStudio.WinUI;
using IGK.DrSStudio.Drawing2D.Menu;
using IGK.DrSStudio.Drawing2D.WinUI;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:AcceleratorPropertyTool.cs
*/
using System; using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms ;
using System.Drawing.Drawing2D;
namespace IGK.DrSStudio.Drawing2D.PropertyToolAcceleratorAddIn.Tools
{
    
    using IGK.ICore.Resources;
    using System.Drawing;
    using IGK.ICore.Tools;
    using IGK.ICore.Drawing2D.Tools;
    using IGK.DrSStudio.Drawing2D.PropertyToolAcceleratorAddIn.WinUI;
    [CoreTools("Tool.AcceleratorTool")]
    class AcceleratorPropertyTool : Core2DDrawingToolBase 
    {
        private static AcceleratorPropertyTool sm_instance;
        private AcceleratorPropertyTool()
        {
        }
        public static AcceleratorPropertyTool Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static AcceleratorPropertyTool()
        {
            sm_instance = new AcceleratorPropertyTool();
        }
        public new XAcceleratorToolStrip HostedControl {
            get {
                return base.HostedControl as XAcceleratorToolStrip;
            }
            set {
                base.HostedControl = value;
            }
        }
        protected override void GenerateHostedControl()
        {
            var ctr = new XAcceleratorToolStrip(this);
            this.HostedControl = ctr;
            new SmothingModeManager(this);
            new FillModeManager(this);
            new CompositingManager(this);
            ctr.AddRemoveButton(null);
        }
        abstract class AcceleratorBaseManager : Core2DDrawingToolBase
        {
            AcceleratorPropertyTool owner;
            protected XAcceleratorToolStrip OwnerHostedControl {
                get { return owner.HostedControl; }
            }
            public AcceleratorBaseManager(AcceleratorPropertyTool owner)
            {
                this.owner = owner;
                this.Workbench = owner.Workbench;
                this.owner.WorkBenchChanged +=  owner_WorkbenchChanged;
            }
            void owner_WorkbenchChanged(object sender, EventArgs e)
            {
                this.Workbench = owner.Workbench;
            }
        }
        class FillModeManager : AcceleratorBaseManager
        {
            ToolStripSplitButton c_btn_fillMode;
            public FillModeManager(AcceleratorPropertyTool owner):base(owner)
            {
                c_btn_fillMode = new ToolStripSplitButton();                
                c_btn_fillMode.DropDownItemClicked += new ToolStripItemClickedEventHandler(c_btn_fillMode_DropDownItemClicked);
                c_btn_fillMode.Tag = enuFillMode.Winding;

                c_btn_fillMode.ButtonClick += new EventHandler(c_btn_fillMode_ButtonClick);
                c_btn_fillMode.DropDownItems.Add("Alternate", WinCoreExtensions.GetDocumentImage(
                   CoreImageKeys.IMG_FILLMODE_ALTERNATE_GKDS) , null).Tag = System.Drawing.Drawing2D.FillMode.Alternate;
                c_btn_fillMode.DropDownItems.Add("Winding", WinCoreExtensions.GetDocumentImage(
                    CoreImageKeys.IMG_FILLMODE_ALTERNATE_GKDS) , null).Tag = System.Drawing.Drawing2D.FillMode.Winding;
                c_btn_fillMode.Image = c_btn_fillMode.DropDownItems[1].Image;
                this.OwnerHostedControl.Items.Add(c_btn_fillMode);
            }
            void c_btn_fillMode_ButtonClick(object sender, EventArgs e)
            {
                enuFillMode mode = (enuFillMode)(this.c_btn_fillMode.Tag );
                this.UpdateFillMode(mode);
            }
            void c_btn_fillMode_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
            {
                enuFillMode mode = (enuFillMode )(e.ClickedItem.Tag );
                UpdateFillMode(mode);
                this.c_btn_fillMode.Image = e.ClickedItem.Image;
            }
         
            public void UpdateFillMode(enuFillMode fillMode)            
            {
                ICore2DDrawingLayer l = this.CurrentSurface.CurrentLayer;
                foreach (ICore2DDrawingLayeredElement v in l.SelectedElements)
                {
                    if ((v is ICore2DFillModeElement))
                    {
                        (v as ICore2DFillModeElement).FillMode = fillMode;
                    }
                }
                this.c_btn_fillMode.Tag = fillMode;
            }
        }
        /// <summary>
        /// represent a compositing mode event args
        /// </summary>
        sealed class CompositingManager : AcceleratorBaseManager
        {
            ToolStripSplitButton c_btn_compositingMode;
            public CompositingManager(AcceleratorPropertyTool owner)
                : base(owner)
               {
                   c_btn_compositingMode = new ToolStripSplitButton();
                   c_btn_compositingMode.DropDownItemClicked += new ToolStripItemClickedEventHandler((object o, ToolStripItemClickedEventArgs  te) =>
                   {
                       enuCompositingMode mode = (enuCompositingMode)(te.ClickedItem.Tag);
                       UpdateComposition(mode);
                       this.c_btn_compositingMode.Image = te.ClickedItem.Image;
                   });
                   c_btn_compositingMode.ButtonClick += new EventHandler((object o, EventArgs te) =>
                   {
                       enuCompositingMode mode = (enuCompositingMode)(c_btn_compositingMode.Tag);
                       UpdateComposition(mode);
                   });
                   c_btn_compositingMode.Tag = CompositingMode.SourceOver;
                   c_btn_compositingMode.DropDownItems.Add("SourceOver", 
                       WinCoreExtensions.GetDocumentImage(CoreImageKeys.COMPOSITINGSOURCEOVER_GKDS), null).Tag = enuCompositingMode.Over ;
                   c_btn_compositingMode.DropDownItems.Add("SourceCopy",
                       WinCoreExtensions.GetDocumentImage(CoreImageKeys.COMPOSITINGSOURCECOPY_GKDS) , null).Tag = enuCompositingMode.Copy;
                   c_btn_compositingMode.Image = c_btn_compositingMode.DropDownItems[1].Image;
                   this.OwnerHostedControl.Items.Add(c_btn_compositingMode);
               }
            void UpdateComposition(enuCompositingMode mode)
            {
                ICore2DDrawingLayer l = this.CurrentSurface.CurrentLayer;
                Core2DDrawingLayeredElement v_l=null;
                foreach (ICore2DDrawingLayeredElement v in l.SelectedElements)
                {
                    v_l  = v as Core2DDrawingLayeredElement ;
                    if (v_l == null)
                    {
                        continue;
                    }
                    v_l.CompositingMode = mode;
                }
                this.c_btn_compositingMode.Tag = mode;
            }
        }
        sealed class SmothingModeManager : AcceleratorBaseManager
        {
            ToolStripSplitButton c_btn_smothingMode;
            //enuSmoothingMode m_SmothingMode;
            public SmothingModeManager(AcceleratorPropertyTool owner):base(owner )
            {
                c_btn_smothingMode = new ToolStripSplitButton ();
                //c_btn_smothingMode.Click += new EventHandler(c_btn_smothingMode_Click);
                c_btn_smothingMode.DropDownItemClicked += new ToolStripItemClickedEventHandler(c_btn_smothingMode_DropDownItemClicked);
                c_btn_smothingMode.ButtonClick += new EventHandler(c_btn_smothingMode_ButtonClick);
                c_btn_smothingMode.Tag = enuSmoothingMode.AntiAliazed;
                c_btn_smothingMode.DropDownItems.Add("None", 
                   WinCoreExtensions .GetDocumentImage(CoreImageKeys.IMG_SMOTHING_NONE_GKDS), null).Tag = enuSmoothingMode.None ;
                c_btn_smothingMode.DropDownItems.Add("AntiAliased",
                    WinCoreExtensions.GetDocumentImage(CoreImageKeys.IMG_SMOTHING_ANTIALIZED_GKDS), null).Tag = enuSmoothingMode.AntiAliazed;
                c_btn_smothingMode.Image = c_btn_smothingMode.DropDownItems[1].Image;
                this.OwnerHostedControl.Items.Add(c_btn_smothingMode);
            }
            void c_btn_smothingMode_ButtonClick(object sender, EventArgs e)
            {
                enuSmoothingMode mode = (enuSmoothingMode)(this.c_btn_smothingMode.Tag );
                UpdateSmothing(mode);   
            }
            void c_btn_smothingMode_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
            {
                enuSmoothingMode mode = (enuSmoothingMode)(e.ClickedItem.Tag);
                UpdateSmothing(mode);
                this.c_btn_smothingMode.Image = e.ClickedItem.Image;
            }
            void c_btn_smothingMode_Click(object sender, EventArgs e)
            {
                enuSmoothingMode mode = (enuSmoothingMode)(sender as ToolStripButton).Tag;
                UpdateSmothing(mode);
            }
            public void UpdateSmothing(enuSmoothingMode mode)
            {
                ICore2DDrawingLayer l = this.CurrentSurface.CurrentLayer;
                foreach (ICore2DDrawingLayeredElement v in l.SelectedElements)
                {
                    if (v is ICore2DSmoothObject)
                    {
                        (v as ICore2DSmoothObject).SmoothingMode = mode;
                    }
                    //v_prInfo  = v.GetType().GetProperty("SmoothingMode");
                    //if (v_prInfo !=null)
                    //{
                    //    v_prInfo.SetValue(v, mode, null);                        
                    //}
                }
                this.c_btn_smothingMode.Tag = mode;
            }
        }
    }
}

