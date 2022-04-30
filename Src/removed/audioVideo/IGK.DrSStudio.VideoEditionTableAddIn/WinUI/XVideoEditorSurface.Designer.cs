

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XVideoEditorSurface.Designer.cs
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
file:XVideoEditorSurface.Designer.cs
*/
namespace IGK.DrSStudio.VideoEditionTableAddIn.WinUI
{
    partial class XVideoEditorSurface
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        #region Component Designer generated code
        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.c_tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.c_listView1 = new System.Windows.Forms.ListView();
            this.c_imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.c_toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.c_videoPlayer = new IGK.DrSStudio.VideoEditionTableAddIn.WinUI.XVideoPlayer();
            this.c_timeLineManager = new IGK.DrSStudio.VideoEditionTableAddIn.WinUI.XVideoTimeLineManager();
            this.c_tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.c_tableLayoutPanel1.ColumnCount = 2;
            this.c_tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.c_tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.c_tableLayoutPanel1.Controls.Add(this.c_videoPlayer, 1, 0);
            this.c_tableLayoutPanel1.Controls.Add(this.c_timeLineManager, 0, 1);
            this.c_tableLayoutPanel1.Controls.Add(this.c_listView1, 0, 0);
            this.c_tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c_tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.c_tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.c_tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.c_tableLayoutPanel1.RowCount = 2;
            this.c_tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.c_tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 191F));
            this.c_tableLayoutPanel1.Size = new System.Drawing.Size(627, 348);
            this.c_tableLayoutPanel1.TabIndex = 0;
            // 
            // listView1
            // 
            this.c_listView1.AllowDrop = true;
            this.c_listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c_listView1.LargeImageList = this.c_imageList1;
            this.c_listView1.Location = new System.Drawing.Point(0, 0);
            this.c_listView1.Margin = new System.Windows.Forms.Padding(0);
            this.c_listView1.Name = "listView1";
            this.c_listView1.Size = new System.Drawing.Size(313, 157);
            this.c_listView1.TabIndex = 2;
            this.c_listView1.UseCompatibleStateImageBehavior = false;
            this.c_listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            this.c_listView1.DragEnter += new System.Windows.Forms.DragEventHandler(listView1_DragEnter);
            this.c_listView1.DragDrop += new System.Windows.Forms.DragEventHandler(listView1_DragDrop);
            // 
            // imageList1
            // 
            this.c_imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.c_imageList1.ImageSize = new System.Drawing.Size(64, 64);
            this.c_imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // toolTip1
            // 
            this.c_toolTip1.Draw += new System.Windows.Forms.DrawToolTipEventHandler(this.toolTip1_Draw);
            // 
            // xVideoPlayer1
            // 
            this.c_videoPlayer.CaptionKey = null;
            this.c_videoPlayer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c_videoPlayer.Location = new System.Drawing.Point(313, 0);
            this.c_videoPlayer.Margin = new System.Windows.Forms.Padding(0);
            this.c_videoPlayer.Name = "xVideoPlayer1";
            this.c_videoPlayer.Padding = new System.Windows.Forms.Padding(3);
            this.c_videoPlayer.Size = new System.Drawing.Size(314, 157);
            this.c_videoPlayer.Surface = null;
            this.c_videoPlayer.TabIndex = 0;
            this.c_videoPlayer.Paint += new System.Windows.Forms.PaintEventHandler(this.xVideoPlayer1_Paint);
            // 
            // xVideoTimeLineManager1
            // 
            this.c_timeLineManager.AutoScroll = true;
            this.c_timeLineManager.CaptionKey = null;
            this.c_tableLayoutPanel1.SetColumnSpan(this.c_timeLineManager, 2);
            this.c_timeLineManager.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c_timeLineManager.Location = new System.Drawing.Point(0, 157);
            this.c_timeLineManager.Margin = new System.Windows.Forms.Padding(0);
            this.c_timeLineManager.Name = "xVideoTimeLineManager1";
            this.c_timeLineManager.Padding = new System.Windows.Forms.Padding(3);
            this.c_timeLineManager.Size = new System.Drawing.Size(627, 191);
            this.c_timeLineManager.Surface = null;
            this.c_timeLineManager.TabIndex = 1;
            // 
            // XVideoEditorSurface
            // 
            this.Controls.Add(this.c_tableLayoutPanel1);
            this.Name = "XVideoEditorSurface";
            this.Size = new System.Drawing.Size(627, 348);
            this.c_tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
        }
        void listView1_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
        {
            const string FileName = "FileDrop";
            string v_filename = string.Empty;
            if (e.Effect == System.Windows.Forms.DragDropEffects.Copy)
            {
                object obj = e.Data.GetData(FileName);
                if (obj is string[])
                {
                    foreach (var item in (obj as string[]))
                    {
                    this.Import(item);    
                    }
                }
                else
                {
                    v_filename = (string)e.Data.GetData(FileName);
                    this.Import(v_filename);
                }
            }
        }
        void listView1_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
        {
            string[] format = e.Data.GetFormats ();
            const string FileName = "FileDrop";
            if (e.Data.GetDataPresent (FileName) )
            {
                    e.Effect = System.Windows.Forms.DragDropEffects.Copy;
            }
        }
        #endregion
        private System.Windows.Forms.TableLayoutPanel c_tableLayoutPanel1;
        private XVideoPlayer c_videoPlayer;
        private XVideoTimeLineManager c_timeLineManager;
        private System.Windows.Forms.ListView c_listView1;
        private System.Windows.Forms.ImageList c_imageList1;
        private System.Windows.Forms.ToolTip c_toolTip1;
    }
}

