using IGK.ICore.WinCore.WinUI.Controls;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.DrSStudio.Android.WinUI
{
    /// <summary>
    /// define widget explorer
    /// </summary>
    public class AndroidWidgetExplorer : IGKXUserControl
    {
        private AndroidWidgetControl c_wcontrol;
        private IGKXPanel c_panel;
        private IGKXComboBox<IGKXComboBoxItem<AndroidTargetInfo>> c_Box;
        private AndroidTargetInfo[] m_targets;

        
        public AndroidTargetInfo TargetInfo 
        {
            get { return this.c_wcontrol.TargetInfo; }
            set
            {
                this.c_wcontrol.TargetInfo = value;
            }
        }
        /// <summary>
        /// represent target informationo changed
        /// </summary>
        public event EventHandler TargetInfoChanged {
            add{
                this.c_wcontrol.TargetInfoChanged += value;
            }
            remove{
                this.c_wcontrol.TargetInfoChanged -= value;
            }
        }
        public AndroidWidgetExplorer()
        {
             c_wcontrol = new AndroidWidgetControl();
             c_wcontrol.Dock = System.Windows.Forms.DockStyle.Fill;
             c_Box = new IGKXComboBox<IGKXComboBoxItem<AndroidTargetInfo>>();
             c_Box.MaximumSize = new System.Drawing.Size(300, 0);
             c_Box.Dock = System.Windows.Forms.DockStyle.Fill;
             c_panel = new IGKXPanel();
             c_panel.Dock = System.Windows.Forms.DockStyle.Top;
             c_panel.Height = 48;
              
             c_panel.Padding = new System.Windows.Forms.Padding(8);
             c_panel.Controls.Add(c_Box);

             this.Controls.Add(c_wcontrol);
             this.Controls.Add(c_panel);
             this.Load += _Load;
        }

        private void _Load(object sender, EventArgs e)
        {
            this.m_targets = AndroidSystemManager.GetAndroidTargets();
            var L = new IXComboBoxDisplayListener<AndroidTargetInfo>((o)=>{
                return o.Item.TargetName;
            });
            this.c_Box.SuspendLayout();
            this.c_Box.Items.Clear();
            for (int i = 0; i < m_targets.Length; i++)
            {
                this.c_Box.Items.Add(new IGKXComboBoxItem<AndroidTargetInfo>(m_targets[i])
                {
                    Listerner = L
                });
            }
            this.c_Box.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            var c = new IXComboBoxMatchListener<IGKXComboBoxItem<AndroidTargetInfo>>(( b, t)=>{
                    if (b.Item  == t)
                        return true;
                        return false;
            });
            this.c_Box.SetMatchListener(c);
            this.c_Box.setSelectedItem (this.TargetInfo);
            this.c_Box.ResumeLayout();
            
            this.c_Box.SelectedIndexChanged += _c_Box_itemChanged;
        }

        private void _c_Box_itemChanged(object sender, EventArgs e)
        {
            if (this.c_Box.SelectedItem != null)
                this.TargetInfo = (this.c_Box.SelectedItem as IGKXComboBoxItem<AndroidTargetInfo>).Item;
        }
    }
}
