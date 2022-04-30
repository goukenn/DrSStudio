

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XElementTransformToolStrip.cs
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
file:XElementTransformToolStrip.cs
*/
/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igkdev.be
Application : DrSStudio
powered by IGK - DEV &copy; 2008-2012
THIS FILE IS A PART OF THE DRSSTUDIO APPLICATION. SEE "License.txt"
FOR MORE INFORMATION ABOUT THE LICENSE
------------------------------------------------------------------- 
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace IGK.DrSStudio.ElementTransform.WinUI
{
    using IGK.ICore.Tools;
    using Tools;
    using IGK.DrSStudio.Drawing2D;
    using IGK.DrSStudio.ElementTransform;
    using IGK.ICore.Resources;
    using IGK.ICore.WinCore.WinUI.Controls;
    class XElementTransformToolStrip : IGKXToolStripCoreToolHost 
    {
        private float m_Angle;
        /// <summary>
        /// get or set the rotation angle
        /// </summary>
        public float Angle
        {
            get { return m_Angle; }
            set
            {
                if (m_Angle != value)
                {
                    m_Angle = value;
                }
            }
        }
        public new _ElementTransform Tool { get { return base.Tool as _ElementTransform;  } }
        public XElementTransformToolStrip(_ElementTransform owner):base(owner )
        {
            InitControl();
        }
        private void InitControl()
        {
            IGKXToolStripButton v_btn = null;
            EventHandler btnEvent = new EventHandler(btn_Click);
            foreach (enuCore2DAlignElement i in Enum.GetValues (typeof (enuCore2DAlignElement )))
            {
                v_btn = new IGKXToolStripButton ();
                v_btn.Text = CoreSystem.GetString(string.Format(CoreConstant.ENUMVALUE, i.ToString()));
                v_btn .Click += btnEvent;
                v_btn.DisplayStyle = ToolStripItemDisplayStyle.Image;
                v_btn .Action = CoreSystem .GetAction (string.Format ("Menu.Tools.Align.{0}",i.ToString()));
                v_btn.Tag = i;
                v_btn.ImageDocument = CoreResources.GetDocument(
                     $"btn_2DAlign_{i.ToString()}_gkds");
                this.Items.Add (v_btn );
            }            
            this.Items.Add (new ToolStripSeparator ());
            //special alignment 
            foreach (enuCore2DSpecialAlignElement i in Enum.GetValues(typeof(enuCore2DSpecialAlignElement)))
            {
                v_btn = new IGKXToolStripButton();
                v_btn.Text = CoreSystem.GetString(string.Format(CoreConstant.ENUMVALUE, i.ToString()));
                v_btn.Click += btnEvent;
                v_btn.DisplayStyle = ToolStripItemDisplayStyle.Image;
                v_btn.Action = CoreSystem.GetAction(string.Format("Menu.Tools.Align.{0}", i.ToString()));
                v_btn.ImageDocument = CoreResources.GetDocument(
                    string.Format("btn_2DAlign_{0}_gkds", i.ToString()));
                v_btn.Tag = i;
                this.Items.Add(v_btn);
            }
            //Size transform
            this.Items.Add(new ToolStripSeparator());
            foreach (enuSize2DTransform i in Enum.GetValues(typeof(enuSize2DTransform)))
            {
                v_btn = new IGKXToolStripButton();
                v_btn.Text = CoreSystem.GetString ( string.Format(CoreConstant.ENUMVALUE , i.ToString()));
                v_btn.Click += btnEvent;
                v_btn.DisplayStyle = ToolStripItemDisplayStyle.Image;
                v_btn.Action = CoreSystem.GetAction(string.Format("Menu.Tools.SizeTransform.{0}", i.ToString()));
                v_btn.ImageDocument = CoreResources.GetDocument(
                    string.Format("btn_2DSizeTransform_{0}_gkds", i.ToString()));
                v_btn.Tag = i;
                this.Items.Add(v_btn);
            }
            this.Items.Add(new ToolStripSeparator());
            v_btn = new IGKXToolStripButton ();
            v_btn .Action = CoreSystem .GetAction (IGKElementTransformConstant.MENU_INVERT_X);            
            v_btn .Click +=btnEvent;
            v_btn.ImageDocument = CoreResources.GetDocument(
              CoreImageKeys.BTN_2DALIGN_INVERTX_GKDS);
            this.Items.Add (v_btn );
            v_btn = new IGKXToolStripButton ();
            v_btn .Action = CoreSystem .GetAction (IGKElementTransformConstant.MENU_INVERT_Y);
            v_btn.ImageDocument = CoreResources.GetDocument(
              CoreImageKeys.BTN_2DALIGN_INVERTY_GKDS);
            v_btn .Click +=btnEvent;
            this.Items.Add (v_btn );
            this.Items.Add(new ToolStripSeparator());
            
            //move element up
            v_btn = new IGKXToolStripButton();
            v_btn.Action = CoreSystem.GetMenu(IGKElementTransformConstant.MENU_LAYER_MOVE_ELEMENT_UP);
            v_btn.Click += btnEvent;
            v_btn.Paint += new PaintEventHandler(v_btn_Paint);
            v_btn.ImageDocument = CoreResources.GetDocument(CoreImageKeys.IMG_MOVEUP_GKDS);
            this.Items.Add(v_btn);

            ///move element down
            v_btn = new IGKXToolStripButton();
            v_btn.Action = CoreSystem.GetMenu(IGKElementTransformConstant.MENU_LAYER_MOVE_ELEMENT_DOWN);
            v_btn.Click += btnEvent;
            v_btn.ImageDocument = CoreResources.GetDocument(CoreImageKeys.IMG_MOVEDOWN_GKDS);
            this.Items.Add(v_btn);



            v_btn = new IGKXToolStripButton();
            v_btn.Action = CoreSystem.GetAction(IGKElementTransformConstant.MENU_LAYER_MOVE_ELEMENT_TOSTART);
            v_btn.Click += btnEvent;
            v_btn.ImageDocument = CoreResources.GetDocument(CoreImageKeys.BACKMOST_GKDS);
            this.Items.Add(v_btn);
            
            v_btn = new IGKXToolStripButton();
            v_btn.Action = CoreSystem.GetAction(IGKElementTransformConstant.MENU_LAYER_MOVE_ELEMENT_TOEND);
            v_btn.Click += btnEvent;
            v_btn.ImageDocument = CoreResources.GetDocument(CoreImageKeys.TOPMOST_GKDS);
            this.Items.Add(v_btn);



            v_btn = new IGKXToolStripButton();
            v_btn.Action = null;
            v_btn.Click += new EventHandler(RotateLeft);
            v_btn.ImageDocument = CoreResources.GetDocument(
               CoreImageKeys.MENU_ROTATELEFT_GKDS);
            this.Items.Add(v_btn);
            v_btn = new IGKXToolStripButton();
            v_btn.Action = null;
            v_btn.Click += new EventHandler(RotateRight);
            v_btn.ImageDocument = CoreResources.GetDocument(
               CoreImageKeys.MENU_ROTATERIGHT_GKDS);
            this.Items.Add(v_btn);
            ToolStripTextBox v_textbox = new ToolStripTextBox();
            v_textbox.Text = "0";
            v_textbox.Validating += new System.ComponentModel.CancelEventHandler(v_textbox_Validating);
            v_textbox.Validated += new EventHandler(v_textbox_Validated);
            v_textbox.AutoSize = false;
            v_textbox.Width = 32;
            this.Items.Add(v_textbox);
            //end add separator
            this.AddRemoveButton (null);
        }
        void v_btn_Paint(object sender, PaintEventArgs e)
        {
        }
        void v_textbox_Validated(object sender, EventArgs e)
        {
            ToolStripTextBox v_text = sender as ToolStripTextBox;            
            this.Angle = float.Parse(v_text.Text);
        }
        void v_textbox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ToolStripTextBox v_text = sender as ToolStripTextBox;
            float f = 0.0f;
            if (float.TryParse(v_text.Text , out f))
            {
                e.Cancel = !(Math.Abs(f) <= 360);
            }
            else
                e.Cancel = true;
        }
        void RotateRight(object sender, EventArgs e)
        {            
            Tool.RotateRight(Angle);
        }
        void RotateLeft(object sender, EventArgs e)
        {
            Tool.RotateLeft(Angle);
        }
        void btn_Click(object sender, EventArgs e)
        {
            IGKXToolStripButton btn = sender as IGKXToolStripButton;
            if (btn.Action != null)
            {
                btn.Action.DoAction();
            }
        }
    }
}

