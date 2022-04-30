using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IGK.DrSStudio.WinUI;
using IGK.ICore.WinUI.Theme;
using IGK.ICore.WinCore.WinUI.Controls;
using IGK.ICore;

namespace IGK.DrSStudio.SkinEditorAddin.WinUI
{
    public partial class ApplicationThemeEditor : IGKXUserControl
    {
        public class ThemeHostItem
        {
            private ApplicationThemeEditor m_applicationThemeEditor;
            private CoreTheme m_th;

            public CoreTheme Theme { get { return m_th;  } }
            internal ThemeHostItem(ApplicationThemeEditor applicationThemeEditor, CoreTheme theme)
            {
                this.m_applicationThemeEditor = applicationThemeEditor;
                this.m_th = theme;
            }
            public override string ToString()
            {
                return this.m_th.Name;
            }
 
        }
        /// <summary>
        /// .ctr
        /// </summary>
        public ApplicationThemeEditor()
        {
            InitializeComponent();
            this.Load += _Load;
        }

        void _Load(object sender, EventArgs e)
        {
            //load system theme
            try
            {
                var tab = CoreTheme.GetThemes();
           
            this.c_lsb_items.SuspendLayout();
            this.c_lsb_items.Items.Clear();
            foreach (var th in tab)
            {
                var p = new ThemeHostItem(this, th);
                 
                this.c_lsb_items.Items.Add(p);
            }
            this.c_lsb_items.IntegralHeight = false;
            this.c_lsb_items.ResumeLayout();
            this.c_lsb_items.SelectedIndexChanged += listBox1_SelectedIndexChanged;
            if (this.c_lsb_items.Items.Count > 0)
            this.c_lsb_items.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                CoreLog.WriteLine(ex.Message);
            }
        }

        void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SelectedItem = this.c_lsb_items.SelectedItem as ThemeHostItem;
        }

        public ThemeHostItem SelectedItem { get; set; }

        private void c_btn_save_Click(object sender, EventArgs e)
        {
            var th = this.SelectedItem;
            if (th == null)
                return;
            //save theme as new files
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Theme files |*.theme";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    th.Theme.save(sfd.FileName);
                }
            }
        }

        private void c_btn_apply_Click(object sender, EventArgs e)
        {
            var th = this.SelectedItem;
            if (th == null)
                return;
            th.Theme.LoadTheme();
        }
    }
}
