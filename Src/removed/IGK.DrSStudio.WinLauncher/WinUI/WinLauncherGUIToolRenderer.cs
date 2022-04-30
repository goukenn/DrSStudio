

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WinLauncherGUIToolRenderer.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV - [2008 - 2014]
author: C.A.D . BONDJE DOUE
file:WinLauncherGUIToolRenderer.cs
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
namespace IGK.DrSStudio.WinUI
{
    using IGK.ICore;using IGK.DrSStudio.WinUI.Configuration;
    /// <summary>
    /// represent the default GUIManager
    /// </summary>
    public class WinLauncherGUIToolRenderer 
    {
        internal const int SPACE = 3;
        internal ICoreParameterConfigCollections sm_ConfigCollections;
        private ICoreDialogToolRenderer winLayoutWorkbenchToolRenderer;
        internal WinLauncherGUIToolRenderer(ICoreDialogToolRenderer winLayoutWorkbenchToolRenderer)
            {
                    this.winLayoutWorkbenchToolRenderer = winLayoutWorkbenchToolRenderer;
            }
        public bool  BuildConfigurableItem(Control ctr, ICoreWorkingConfigurableObject obj)
        {
            int x = 10;
            int y = 10;
            bool r =  __BuildConfigurableItem(ctr, obj, ref x,  ref y);
            return r;
        }
        private  bool __BuildConfigurableItem(
            System.Windows.Forms.Control control,
            ICoreWorkingConfigurableObject item, ref int x, ref int y)
        {
            control.SuspendLayout();
            int SUBPOS = x + 20;
            int YOFFSET = 24;
            switch (item.GetConfigType())
            {
                case enuParamConfigType.CustomControl:
                    Control c = item.GetConfigControl() as Control ;
                    if (c != null)
                    {
                        control.Controls.Add(c);
                    }
                    break;
                case enuParamConfigType.ParameterConfig:
                    //-------------------------------------------------------
                    //paramter config case
                    //-------------------------------------------------------
                    ICoreParameterConfigCollections v_p = item.GetParameters(new CoreParameterConfigCollections(item));
                    v_p.Host = winLayoutWorkbenchToolRenderer;
                    sm_ConfigCollections = v_p;
                    Control bl = null;
                    foreach (ICoreParameterGroup group in v_p)
                    {
                        x = 10; //CONFIGURATION_OFFSET_X
                        //add label
                        bl = new IGKXRuleLabel()
                        {
                            CaptionKey = group.CaptionKey
                        };
                        bl.Location = new Point(x, y);
                        bl.Width = control.Width - x - 10;
                        bl.Anchor = AnchorStyles.Left | AnchorStyles.Top;
                        bl.MinimumSize = new Size(130, 0);
                        y += bl.Height;
                        control.Controls.Add(bl);
                        //populate items 
                        foreach (ICoreParameterEntry h in group)
                        {
                            x = SUBPOS;
                            if (h is ICoreParameterControl)
                            {
                                Control v_ctr = (h as ICoreParameterControl).Control as Control ;
                                if (v_ctr != null)
                                {
                                    //no label
                                    //CreateLabel(h, control, ref x, ref y);
                                    control.Controls.Add(v_ctr);
                                    v_ctr.Location = new Point(x, y);
                                    y += v_ctr.Height + SPACE;
                                }
                            }
                            else
                            {
                                if (h is ICoreConfigurableObjectParameterItem)
                                {
                                    ICoreConfigurableObjectParameterItem v_h = h as ICoreConfigurableObjectParameterItem;
                                    BuildConfigurableObjectParameterItem(control, v_h, ref x, ref y);
                                }
                                else if (h is ICoreParameterGroupItem)
                                {
                                    ICoreParameterGroupItem v_groupitem = h as ICoreParameterGroupItem;
                                    BuildGroupItem(v_groupitem, control, item, ref x, ref y);
                                    y += YOFFSET;
                                }
                                else if (h is ICoreParameterAction)
                                {
                                    BuildButtonItem(
                                    (h as ICoreParameterAction),
                                        control,
                                        ref x,
                                        ref y);
                                }
                                else
                                { 
                                }
                            }
                        }
                        x -= 20;
                    }
                    break;
            }
            control.ResumeLayout();
            return true;
        }
        /// <summary>
        /// build configuratble items
        /// </summary>
        /// <param name="control"></param>
        /// <param name="v_h"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private  bool BuildConfigurableObjectParameterItem(Control control, ICoreConfigurableObjectParameterItem v_h, ref int x, ref int y)
        {
            int bx = 10;
           return  __BuildConfigurableItem(control, v_h.Target, ref bx, ref y);
        }
        private void BuildButtonItem(
            ICoreParameterAction action,
            Control control,
            ref int x,
            ref int y)
        {
            ButtonElementItem btnAction =  new ButtonElementItem(action);
            control.Controls.Add(btnAction.Button);
            btnAction.Button.Location = new Point(x, y);
            x += btnAction.Button.Width;
            y += btnAction.Button.Height;
        }
        private static void BuildGroupItem(
            ICoreParameterGroupItem groupItem,
            Control targetControl,
            ICoreWorkingConfigurableObject configurableObject,
            ref int x,
            ref int y)
        {
            Control v_ctr = null;
            switch (groupItem.ParamType)
            {
                case enuParameterType.Bool:
                    XCheckBox cbl = new XCheckBox()
                    {
                        CaptionKey = groupItem.CaptionKey,
                        Location = new Point(x, y)
                    };
                    targetControl.Controls.Add(cbl);
                    new CheckElementItem(configurableObject, groupItem, cbl);
                    break;
                case enuParameterType.IntNumber:
                    {
                        CreateLabel(groupItem, targetControl, ref x, ref y);
                        IGKXNumericTextBox v_ntxb = new IGKXNumericTextBox();
                        __initConfigControl(v_ntxb, targetControl.Width, ref x,ref  y);
                        v_ntxb.AllowDecimalValue = false;
                        v_ntxb.AllowNegativeValue = true;
                        new NumTextElementItem(configurableObject, groupItem, v_ntxb);
                        targetControl.Controls.Add(v_ntxb);
                    }
                    break;
                case enuParameterType.EnumType:
                    if (groupItem is ICoreParameterGroupEnumItem)
                    {
                        ICoreParameterGroupEnumItem h = groupItem as ICoreParameterGroupEnumItem;
                        CreateLabel(groupItem, targetControl, ref x, ref y);
                        IGKXComboBox box = new IGKXComboBox();
                        box.DropDownStyle = ComboBoxStyle.DropDownList;
                        __initConfigControl(box, targetControl.Width, ref x, ref y);
                        targetControl.Controls.Add(box);
                        box.DataSource = h.GetDefaultValues();
                        box.DisplayMember = "CaptionKey";
                        box.ValueMember = null;
                        box.SelectedItem = h.GetSelectedItem();
                        box.PerformLayout();
                        box.Refresh();
                        new BoxElementItem(configurableObject, groupItem as ICoreParameterGroupEnumItem, box);
                    }
                    else {
                        //item is not a valid item
                    }
                    break;
                case enuParameterType.SingleNumber:
                    {
                        CreateLabel(groupItem, targetControl, ref x, ref y);
                        IGKXNumericTextBox v_ntxb = new IGKXNumericTextBox();
                        v_ntxb.AllowDecimalValue = true;
                        v_ntxb.AllowNegativeValue = true;
                        v_ntxb.Location = new Point(x, y);
                        __initConfigControl(v_ntxb, targetControl.Width, ref x, ref y);
                        targetControl.Controls.Add(v_ntxb);
                        new NumTextElementItem(configurableObject, groupItem, v_ntxb);
                    }
                    break;
                case enuParameterType.Interval:
                    CreateLabel(groupItem, targetControl, ref x, ref y);
                    XTrackbar c = new XTrackbar();
                    __initConfigControl(c, targetControl.Width,ref x, ref y);
                    targetControl.Controls.Add(c);
                    new TrackbarElementItem(
                        groupItem as ICoreParameterTrackbarItem,
                        configurableObject,
                        c);
                    break;
                case  enuParameterType.FileName :
                    CreateLabel(groupItem, targetControl, ref x, ref y);
                    v_ctr = new XTextBox();
                    __initConfigControl(v_ctr, targetControl.Width - 32 ,  ref x,ref  y);
                    targetControl.Controls.Add(v_ctr);
                    new TextElementItem(configurableObject, groupItem, v_ctr as XTextBox );
                    v_ctr = new IGKXButton();
                    v_ctr.Location = new Point(x, y);
                    targetControl.Controls.Add(v_ctr);
                    new FileNameSelectorItem(configurableObject, groupItem, v_ctr);
                    break;
                case  enuParameterType.Folder :
                    CreateLabel(groupItem, targetControl, ref x, ref y);
                    v_ctr = new XTextBox();
                    __initConfigControl(v_ctr, targetControl.Width - 32 ,  ref x,ref y);
                    targetControl.Controls.Add(v_ctr);
                    new TextElementItem(configurableObject, groupItem, v_ctr as XTextBox);
                    v_ctr = new IGKXButton();
                    targetControl.Controls.Add(v_ctr);
                    new FolderSelectorItem(configurableObject, groupItem, v_ctr );
                    break;
                case enuParameterType.Text:                    
                default:
                    CreateLabel(groupItem, targetControl, ref x, ref y);
                    XTextBox v_txb = new XTextBox();
                    __initConfigControl(v_txb, targetControl.Width , ref x, ref y);
                    targetControl.Controls.Add(v_txb);
                    new TextElementItem(configurableObject, groupItem, v_txb);
                    break;
            }
        }
        private static void __initConfigControl(Control c,int width,  ref int x,ref int y)
        {
            c.Location = new Point(x, y);
            c.Width = width  - x - 40;
            c.Anchor = AnchorStyles.Left | AnchorStyles.Top;
            c.MinimumSize = new Size(150, 0);
            x += c.Width;
        }
        internal void Restore(ICoreWorkingConfigurableObject item)
        {
            if (sm_ConfigCollections != null)
            {
                sm_ConfigCollections.RestoreDefault();
            }
        }
        private static void CreateLabel(ICoreParameterItem i, Control control, ref int x, ref  int y)
        {
            IGKXLabel bl = new IGKXLabel()
            {
                CaptionKey = i.CaptionKey,
                AutoSize = false,
                Size = new Size(125, 24),
                Location = new Point(x, y),
            };
            control.Controls.Add(bl);
            x += bl.Width;
        }
        /// <summary>
        /// represent a base configurable item
        /// </summary>
        class ControlItemConfig
        {
            ICoreParameterGroupItem v_paramItem;
            ICoreWorkingConfigurableObject v_object;
            public ICoreWorkingConfigurableObject Target
            {
                get { return v_object; }
            }
            public ICoreParameterGroupItem Item
            {
                get { return v_paramItem; }
            }
            protected ControlItemConfig(ICoreParameterGroupItem v_paramItem,
            ICoreWorkingConfigurableObject v_object)
            {
                this.v_paramItem = v_paramItem;
                this.v_object = v_object;
            }
        }
        class TextElementItem : ControlItemConfig
        {
            TextBox v_textbox;
            public TextElementItem(ICoreWorkingConfigurableObject v_object, ICoreParameterGroupItem item, XTextBox textbox) :
                base(item, v_object)
            {
                v_textbox = textbox;
                if (item.DefaultValue != null)
                    v_textbox.Text = item.DefaultValue.ToString();
                else
                    if (Target.GetType().GetProperty(Item.Name) != null)
                        v_textbox.Text = Convert.ToString(Target.GetType().GetProperty(Item.Name).GetValue(Target, null));
                textbox.TextChanged += new EventHandler(textbox_TextChanged);
            }
            void textbox_TextChanged(object sender, EventArgs e)
            {
                Item.Invoke(Target, v_textbox.Text);
            }
        }
        class CheckElementItem : ControlItemConfig
        {
            XCheckBox m_checkBox;
            public CheckElementItem(
                ICoreWorkingConfigurableObject v_object,
                ICoreParameterGroupItem item,
                XCheckBox chb) :
                base(item, v_object)
            {
                chb.Checked = Convert.ToBoolean(item.DefaultValue);
                chb.CheckedChanged += new EventHandler(chb_CheckedChanged);
                chb.AutoSize = true;
                m_checkBox = chb;
            }
            void chb_CheckedChanged(object sender, EventArgs e)
            {
                Item.Invoke(Target, m_checkBox.Checked);
            }
        }
        class NumTextElementItem : ControlItemConfig
        {
            IGKXNumericTextBox v_textbox;
            public NumTextElementItem(
                ICoreWorkingConfigurableObject v_object,
                ICoreParameterGroupItem item,
                IGKXNumericTextBox textbox) :
                base(item, v_object)
            {
                v_textbox = textbox;
                if (item.DefaultValue != null)
                    v_textbox.Value = Convert.ToDecimal(item.DefaultValue.ToString());
                else if (Target.GetType().GetProperty(Item.Name) != null)
                {
                    v_textbox.Value = Convert.ToDecimal(Target.GetType().GetProperty(Item.Name).GetValue(Target, null));
                }
                textbox.ValueChanged += new EventHandler(textbox_ValueChanged);
            }
            public NumTextElementItem(
              ICoreWorkingConfigurableObject v_object,
              ICoreParameterGroupItem item,
              int defaultValue,
              IGKXNumericTextBox textbox) :
                base(item, v_object)
            {
                v_textbox = textbox;
                v_textbox.Value = defaultValue;
                textbox.ValueChanged += new EventHandler(textbox_ValueChanged);
            }
            void textbox_ValueChanged(object sender, EventArgs e)
            {
                Item.Invoke(Target, v_textbox.Value);
            }
        }
        class BoxElementItem : ControlItemConfig
        {
            IGKXComboBox m_cmbox;
            public BoxElementItem
                (
                ICoreWorkingConfigurableObject v_object,
                ICoreParameterGroupEnumItem item,
                IGKXComboBox textbox) :
                base(item, v_object)
            {
                m_cmbox = textbox;
                m_cmbox.Anchor = AnchorStyles.Left |  AnchorStyles.Top;
                m_cmbox.SelectedIndexChanged += new EventHandler(textbox_SelectedIndexChanged);
            }
            void textbox_SelectedIndexChanged(object sender, EventArgs e)
            {
                Item.Invoke(Target, m_cmbox.SelectedItem);
            }
        }
        class TrackbarElementItem : ControlItemConfig
        {
            XTrackbar c_mtrackbar;
            /// <summary>
            /// 
            /// </summary>
            /// <param name="item">parameteritem</param>
            /// <param name="v_object">target</param>
            /// <param name="c_mtrackbar">trackbar</param>
            public TrackbarElementItem(
                ICoreParameterTrackbarItem item,
                ICoreWorkingConfigurableObject v_object,
                XTrackbar c_mtrackbar)
                : base(item, v_object)
            {
                this.c_mtrackbar = c_mtrackbar;
                this.c_mtrackbar.Min = item.min;
                this.c_mtrackbar.Max = item.max;
                this.c_mtrackbar.Value = item.DefaultValue;
                this.c_mtrackbar.ValueChanged += new EventHandler(c_mtrackbar_ValueChanged);
            }
            void c_mtrackbar_ValueChanged(object sender, EventArgs e)
            {
                Item.Invoke(Target, c_mtrackbar.Value);
            }
        }
        class ButtonElementItem
        {
            IGKXButton c_btn;
            ICoreParameterAction c_action;
            public IGKXButton Button { get { return c_btn; } }
            public ButtonElementItem( ICoreParameterAction action)
            {
                c_action = action;
                c_btn = new IGKXButton();
                c_btn.Width = 100; // BTN_DEFAULT_WIDTH;
                c_btn.Height = 28;
                c_btn.CaptionKey = action.CaptionKey;
                c_btn.Click += new EventHandler(c_btn_Click);
            }
            void c_btn_Click(object sender, EventArgs e)
            {
                c_action.Action.DoAction();
                if (c_action.Reload)
                {
                    CoreParameterAction c = c_action as CoreParameterAction;
                   var  d = c.Host;
                   if (d != null)
                       d.Reload();
//                    this.c_action.Host.Reload();
                }
            }
        }
        class FileNameSelectorItem : ControlItemConfig
        {
            Control  c_btn;
            public string FileName {
                get {
                    return "";
                }
            }
            public FileNameSelectorItem(ICoreWorkingConfigurableObject obj, ICoreParameterGroupItem item, 
                Control  btn)
                : base(item, obj)
            {
                this.c_btn = btn;
                this.c_btn.Width = 32; // BTN_DEFAULT_WIDTH;
                this.c_btn.Height = 28;
                this.c_btn.Text = "...";
                this.c_btn.Click += c_btn_Click;
            }
            void c_btn_Click(object sender, EventArgs e)
            {
                using (SaveFileDialog fd = new SaveFileDialog())
                {
                    fd.Filter = "*.*|*.*";
                    fd.FileName = this.FileName;
                    if (fd.ShowDialog() == DialogResult.OK)
                    {
                        this.Item.Invoke(this.Target, fd.FileName);
                    }
                }
            }
        }
        class FolderSelectorItem : ControlItemConfig
        {
            Control c_btn;
            public FolderSelectorItem(ICoreWorkingConfigurableObject obj,
                ICoreParameterGroupItem item, Control btn)
                : base(item, obj)
            {
                this.c_btn = btn;
                this.c_btn.Width = 32; // BTN_DEFAULT_WIDTH;
                this.c_btn.Height = 28;
                this.c_btn.Text = "...";
                this.c_btn.Click += c_btn_Click;
            }
            void c_btn_Click(object sender, EventArgs e)
            {
                using (FolderBrowserDialog fbd = new FolderBrowserDialog())
                {
                    if (fbd.ShowDialog() == DialogResult.OK)
                    {
                        this.Item.Invoke(this.Target, fbd.SelectedPath);
                    }
                }
            }
        }
    }
}

