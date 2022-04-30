

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XFormDialog.cs
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
file:XFormDialog.cs
*/
using IGK.ICore.WinCore;
using IGK.ICore;
using IGK.ICore.Windows.Native;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;
namespace IGK.ICore.WinUI
{
    /// <summary>
    /// get the form dialog
    /// </summary>
    public class XFormDialog : XForm, ICoreDialogForm
    {
        [DesignerSerializationVisibility (DesignerSerializationVisibility.Hidden)]
        public enuDialogStyle DialogStyle { get {
            
            switch (this.FormBorderStyle)
            {
                case System.Windows.Forms.FormBorderStyle.Fixed3D:
                    break;
                case System.Windows.Forms.FormBorderStyle.FixedDialog:
                    break;
                case System.Windows.Forms.FormBorderStyle.FixedSingle:
                    break;
                case System.Windows.Forms.FormBorderStyle.FixedToolWindow:
                    return enuDialogStyle.ToolWindow;                    
                case System.Windows.Forms.FormBorderStyle.None:
                    return enuDialogStyle.NoBorder;      
                case System.Windows.Forms.FormBorderStyle.Sizable:
                    break;
                case System.Windows.Forms.FormBorderStyle.SizableToolWindow:
                    return enuDialogStyle.SizeableTool;                   
                default:
                    break;
            }
            return enuDialogStyle.Sizeable;
        }
            set {
                switch (value)
                {
                    case enuDialogStyle.NoBorder:
                        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                        break;
                    case enuDialogStyle.Sizeable:
                        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
                        break;
                    case enuDialogStyle.ToolWindow :
                        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
                        break;
                    case enuDialogStyle.SizeableTool :
                        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
                        break;
                    default:
                        break;
                }
            }
        }

        protected override System.Drawing.Size DefaultSize
        {
            get
            {
                return base.DefaultSize;
            }
        }
        public override System.Drawing.Size MinimumSize
        {
            get
            {
                return base.MinimumSize;
            }
            set
            {
                base.MinimumSize = value;
            }
        }

        protected override System.Windows.Forms.CreateParams CreateParams
        {
            get
            {
                var p = base.CreateParams;
                p.ExStyle &= ~User32.WS_EX_TOOLWINDOW;//deactivate ex tool windows. cause the item to show in alt-tab and icon displayed
                return p;
            }
        }
        public XFormDialog():base()
        {
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.DialogStyle = enuDialogStyle.SizeableTool;
            this.MinimumSize = new System.Drawing.Size(WinCoreConstant.DEFAULT_DIALOG_WIDTH, WinCoreConstant.DEFAULT_DIALOG_HEIGHT );
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowOnly;
            this.AutoSize = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowInTaskbar = false;
            this.ShowIcon = true;
            this.Padding = global::System.Windows.Forms.Padding.Empty;
            
        }
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            //base.OnPaintBackground(e);
        }
        public bool CanReduce
        {
            get { return this.MinimizeBox ; }
            set
            {
                this.MinimizeBox = value;
            }
        }
        public bool CanMaximize
        {
            get { return this.MaximizeBox; }
            set
            {
                this.MaximizeBox = value;
            }
        }       
    }
}

