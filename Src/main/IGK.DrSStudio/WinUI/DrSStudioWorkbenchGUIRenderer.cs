

using IGK.ICore.WinCore;
using IGK.ICore;using IGK.ICore.ComponentModel;
/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: DrSStudioWorkbenchGUIRenderer.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinUI;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:DrSStudioWorkbenchGUIRenderer.cs
*/
using IGK.ICore.WinUI.Configuration;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IGK.ICore.WinCore.WinUI.Controls;
namespace IGK.DrSStudio.WinUI
{
    /// <summary>
    /// Represent the default GUIManager
    /// </summary>
    public class DrSStudioWorkbenchGUIRenderer
    {
        internal const int SPACE = 3;
        internal const int YOFFSET = 4;
        internal const int LABEL_HEIGHT = 24;
        internal const int LABEL_WIDTH = 125;

        internal ICoreParameterConfigCollections sm_ConfigCollections;
        private ICoreDialogToolRenderer winLayoutWorkbenchToolRenderer;
        
        internal DrSStudioWorkbenchGUIRenderer(ICoreDialogToolRenderer winLayoutWorkbenchToolRenderer)
        {
            this.winLayoutWorkbenchToolRenderer = winLayoutWorkbenchToolRenderer;
        }
        public bool BuildConfigurableItem(ICoreControl ctr, ICoreWorkingConfigurableObject obj, ref Size2i preferredSize)
        {
            int x = 10;
            int y = 10;
            bool r = __BuildConfigurableItem(ctr, obj, ref x, ref y);
            preferredSize = new Size2i(x, y + XDrSStudioConfigureItemPropertyControl.DOWN_HEIGHT);
            //ctr.Size = preferredSize;
            return r;
        }
        private bool __BuildConfigurableItem(
            ICoreControl control,
            ICoreWorkingConfigurableObject item, ref int x, ref int y)
        {
            if (control == null)
                return false;
            control.SuspendLayout();
            int SUBPOS = x + 20;
            
            switch (item.GetConfigType())
            {
                case enuParamConfigType.CustomControl:
                    Control c = item.GetConfigControl() as Control;
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
                            CaptionKey = group.CaptionKey,
                            AutoSize = false 
                        };
                        bl.Location = new Point(x, y+4);//with top margin
                        bl.Width = control.Width - x - 10;
                        bl.Height = 24;
                        bl.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
                        bl.MinimumSize = new Size(130, 0);
                        y += bl.Height+4;//with bottom margin
                        control.Controls.Add(bl);
                        //populate items 
                        foreach (ICoreParameterEntry h in group)
                        {
                            x = SUBPOS;
                            if (h is ICoreParameterControl)
                            {
                                Control v_ctr = (h as ICoreParameterControl).Control as Control;
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
                                }
                                else if (h is ICoreParameterAction)
                                {
                                    BuildButtonItem(
                                    (h as ICoreParameterAction),
                                        control,
                                        ref x,
                                        ref y);
                                    y += 10;
                                }
                            }
                        }
                        x -= 20;
                    }
                    break;
            }
            var s = (control as System.Windows.Forms.Control).PreferredSize;
            control.Size = new Size2i(s.Width, s.Height);
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
        private bool BuildConfigurableObjectParameterItem(ICoreControl control, ICoreConfigurableObjectParameterItem v_h, ref int x, ref int y)
        {
            int bx = 10;
            return __BuildConfigurableItem(control, v_h.Target, ref bx, ref y);
        }
        private void BuildButtonItem(
            ICoreParameterAction action,
            ICoreControl control,
            ref int x,
            ref int y)
        {
            ButtonElementItem btnAction = new ButtonElementItem(action);
            control.Controls.Add(btnAction.Button);
            btnAction.Button.Location = new Point(x, y);
            x += btnAction.Button.Width;
            y += btnAction.Button.Height;
        }
        private static void BuildGroupItem(
            ICoreParameterGroupItem groupItem,
            ICoreControl targetControl,
            ICoreWorkingConfigurableObject configurableObject,
            ref int x,
            ref int y)
        {
            Control v_ctr = null;
            int v_h = 0;
            #region "params"
            switch (groupItem.ParamType)
            {
                case enuParameterType.Bool:
                    var b  = new IGKXCheckBox()
                    {
                        CaptionKey = groupItem.CaptionKey,
                        Location = new Point(x, y)
                    };
                    v_ctr = b;
                    targetControl.Controls.Add(v_ctr);
                    new CheckElementItem(configurableObject, groupItem, b);
                  
                    v_h = b.Height;
                    break;
                case enuParameterType.IntNumber:
                    {
                        CreateLabel(groupItem, targetControl, ref x, ref y);
                        IGKXNumericTextBox v_ntxb = new IGKXNumericTextBox();
                        __initConfigControl(v_ntxb, targetControl.Width, ref x, ref  y);
                        v_ntxb.AllowDecimalValue = false;
                        v_ntxb.AllowNegativeValue = true;
                        new NumTextElementItem(configurableObject, groupItem, v_ntxb);
                        targetControl.Controls.Add(v_ntxb);
                        v_ctr = v_ntxb;
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
                        v_ctr = box;
                    }
                    else
                    {
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
                        v_ctr = v_ntxb;
                    }
                    break;
                case enuParameterType.MultiTextLine :
                    {
                        CreateLabel(groupItem, targetControl, ref x, ref y);
                        IGKXTextBox v_ntxb = new IGKXTextBox();
                        v_ntxb.Multiline = true;

                        v_ntxb.Location = new Point(x, y);
                        v_ntxb.Size = new Size(128, 64);
                        __initConfigControl(v_ntxb, targetControl.Width, ref x, ref y);
                       
                        targetControl.Controls.Add(v_ntxb);
                        new TextElementItem(configurableObject, groupItem, v_ntxb);
                        v_ctr = v_ntxb;
                    }
                    break;
                
                case enuParameterType.Interval:
                    CreateLabel(groupItem, targetControl, ref x, ref y);
                    IGKXTrackbar c = new IGKXTrackbar();
                    __initConfigControl(c, targetControl.Width, ref x, ref y);
                    targetControl.Controls.Add(c);
                    new TrackbarElementItem(
                        groupItem as ICoreParameterTrackbarItem,
                        configurableObject,
                        c);
                    v_ctr = c;
                    break;
                case enuParameterType.FileName:
                    CreateLabel(groupItem, targetControl, ref x, ref y);
                    v_ctr = new IGKXTextBox();
                    __initConfigControl(v_ctr, targetControl.Width - 32, ref x, ref  y);
                    targetControl.Controls.Add(v_ctr);
                    new TextElementItem(configurableObject, groupItem, v_ctr as IGKXTextBox);
                    v_ctr = new IGKXButton();
                    v_ctr.Location = new Point(x, y);
                    targetControl.Controls.Add(v_ctr);
                    new FileNameSelectorItem(configurableObject, groupItem, v_ctr);
                    break;
                case enuParameterType.Folder:
                    CreateLabel(groupItem, targetControl, ref x, ref y);
                    v_ctr = new IGKXTextBoxHost();
                    __initConfigControl(v_ctr, targetControl.Width - 32, ref x, ref y);
                    targetControl.Controls.Add(v_ctr);
                    new TextElementItem(configurableObject, groupItem, v_ctr as IGKXTextBox);
                    v_ctr = new IGKXButton();
                    targetControl.Controls.Add(v_ctr);
                    new FolderSelectorItem(configurableObject, groupItem, v_ctr);
                    break;
                case enuParameterType.TextArea:
                    {
                        CreateLabel(groupItem, targetControl, ref x, ref y);
                        RichTextBox v_txb = new RichTextBox();
                        v_txb.Height = 120;
                        __initConfigControl(v_txb, targetControl.Width, ref x, ref y);
                        targetControl.Controls.Add(v_txb);
                        new TextAreaElementItem(configurableObject, groupItem, v_txb);
                        v_ctr = v_txb;
                    }
                    break;
                case enuParameterType.Label:
                    {
                        var lb = CreateLabel(groupItem, targetControl, ref x, ref y);
                        lb.AutoSize = true;
                        new LabelInfoItem(configurableObject, groupItem , lb);
                        v_ctr = lb;
                    }
                    break;
                case enuParameterType.Text:
                case enuParameterType.Password:
                default:
                    {
                        CreateLabel(groupItem, targetControl, ref x, ref y);
                        IGKXTextBox v_txb = new IGKXTextBox();
                        __initConfigControl(v_txb, targetControl.Width, ref x, ref y);
                        targetControl.Controls.Add(v_txb);
                        new TextElementItem(configurableObject, groupItem, v_txb);

                        if (groupItem.ParamType == enuParameterType.Password)
                        {
                            v_txb.PasswordChar = '*';
                            v_txb.UseSystemPasswordChar = true;
                        }
                        v_ctr = v_txb;
                    }
                    break;
            }
            #endregion
            y += YOFFSET + Math.Max(LABEL_HEIGHT, v_ctr !=null? v_ctr.Height : 0);
        }
        private static void __initConfigControl(Control c, int width, ref int x, ref int y)
        {
            c.Location = new Point(x, y);
            c.Width = width - x - 40;
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
        private static IGKXLabel CreateLabel(ICoreParameterItem i, ICoreControl control, ref int x, ref  int y)
        {
            IGKXLabel bl = new IGKXLabel()
            {
                CaptionKey = i.CaptionKey,
                AutoSize = false,
                Size = new Size(LABEL_WIDTH, LABEL_HEIGHT),
                Location = new Point(x, y),
            };
            control.Controls.Add(bl);
            x += bl.Width;
            return bl;
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
        sealed class TextElementItem : ControlItemConfig
        {
            TextBox v_textbox;
            public TextElementItem(ICoreWorkingConfigurableObject v_object, 
                ICoreParameterGroupItem item, IGKXTextBox textbox) :
                base(item, v_object)
            {
                v_textbox = textbox;
                if (item.DefaultValue != null)
                    v_textbox.Text = item.DefaultValue.ToString();
                else
                {
                    var prop = Target.GetType().GetProperty(Item.Name);
                    if (prop != null)
                    {
                        System.ComponentModel.TypeConverter conf = null;
                        var mm = prop.GetCustomAttributes(typeof(System.ComponentModel.TypeConverterAttribute), false);
                        var s =  mm!=null && mm.Length >0? mm[0] as System.ComponentModel.TypeConverterAttribute : null;
                        if (s != null){
                            var v_st  = System.Type.GetType (s.ConverterTypeName ) ;
                            conf = v_st.Assembly.CreateInstance(v_st.FullName ) as System.ComponentModel.TypeConverter ;
                        }
                        else
                           conf = CoreTypeDescriptor.GetConverter(prop.PropertyType);
                        if (conf.CanConvertTo (typeof (string)))
                        {
                            v_textbox.Text = conf.ConvertToString (prop.GetValue(Target, null));
                        }
                        else
                        v_textbox.Text = Convert.ToString(Target.GetType().GetProperty(Item.Name).GetValue(Target, null));
                    }
                }
                textbox.TextChanged += new EventHandler(textbox_TextChanged);
            }
            void textbox_TextChanged(object sender, EventArgs e)
            {
               Item.Invoke(Target, v_textbox.Text);
            }
        }
        sealed class TextAreaElementItem : ControlItemConfig
        {
            RichTextBox v_textbox;
            public TextAreaElementItem(ICoreWorkingConfigurableObject v_object, ICoreParameterGroupItem item, RichTextBox textbox) :
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
        sealed class LabelInfoItem : ControlItemConfig
        {
            private ICoreWorkingConfigurableObject configurableObject;
            private ICoreParameterGroupItem groupItem;
            private IGKXLabel lb;

            public LabelInfoItem(ICoreWorkingConfigurableObject configurableObject, ICoreParameterGroupItem groupItem, IGKXLabel lb)
                :base(groupItem, configurableObject )
            {
             
                this.configurableObject = configurableObject;
                this.groupItem = groupItem;
                this.lb = lb;
                groupItem.ValueChanged += groupItem_ValueChanged;
            }

            void groupItem_ValueChanged(object sender, EventArgs e)
            {
                this.lb.Text = groupItem.Value.ToStringCore();
            } 
            
        }
        
        class CheckElementItem : ControlItemConfig
        {
            IGKXCheckBox m_checkBox;
            public CheckElementItem(
                ICoreWorkingConfigurableObject v_object,
                ICoreParameterGroupItem item,
                IGKXCheckBox chb) :
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
                m_cmbox.Anchor = AnchorStyles.Left | AnchorStyles.Top;
                m_cmbox.SelectedIndexChanged += new EventHandler(textbox_SelectedIndexChanged);
            }
            void textbox_SelectedIndexChanged(object sender, EventArgs e)
            {
                Item.Invoke(Target, m_cmbox.SelectedItem);
            }
        }
        class TrackbarElementItem : ControlItemConfig
        {
            IGKXTrackbar c_mtrackbar;
            /// <summary>
            /// 
            /// </summary>
            /// <param name="item">parameteritem</param>
            /// <param name="v_object">target</param>
            /// <param name="c_mtrackbar">trackbar</param>
            public TrackbarElementItem(
                ICoreParameterTrackbarItem item,
                ICoreWorkingConfigurableObject v_object,
                IGKXTrackbar c_mtrackbar)
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
            public ButtonElementItem(ICoreParameterAction action)
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
                    CoreParameterActionBase c = c_action as CoreParameterActionBase;
                    var d = c.Host;
                    if (d != null)
                        d.Reload();
                    //                    this.c_action.Host.Reload();
                }
            }
        }
        class FileNameSelectorItem : ControlItemConfig
        {
            Control c_btn;
            public string FileName
            {
                get
                {
                    return "";
                }
            }
            public FileNameSelectorItem(ICoreWorkingConfigurableObject obj, ICoreParameterGroupItem item,
                Control btn)
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

