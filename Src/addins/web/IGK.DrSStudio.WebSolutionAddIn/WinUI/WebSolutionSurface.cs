

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WebSolutionSurface.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/

using IGK.ICore;
using IGK.ICore.Codec;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.DrSStudio;
using IGK.DrSStudio.Menu;
using IGK.DrSStudio.WinUI;
using IGK.DrSStudio.Drawing2D;
using IGK.DrSStudio.Drawing2D.WinUI;
﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Web.WinUI
{
    using IGK.ICore.WinUI.Configuration;
    using System.IO;
    using System.Windows.Forms;
    using IGK.ICore.WinCore.WinUI.Controls;
    using IGK.ICore.WinCore;
    using IGK.ICore.WinCore.WinUI;

    [CoreSurface("WEBSOLUTION", EnvironmentName="WEBSOLUTION")]
    /// <summary>
    /// represent a default web solution surface
    /// </summary>
    public class WebSolutionSurface :
        IGKXWinCoreWorkingSurface,         
        ICoreWorkingProjectSolutionSurface ,
        ICoreWorkingFilemanagerSurface 
    {
        private WebSolutionSolution m_Solution;
        private bool m_Saving;
        private IGKXPanel c_panel;
        private IGKXExpenderBox c_exbox_categories;
        private IGKXLabel c_lb_choosecat;
        private Splitter c_splitter;
        private string m_FileName;


        /// <summary>
        /// get if this surface is in saving mode
        /// </summary>
        public bool Saving
        {
            get { return m_Saving; }
            protected set
            {
                if (m_Saving != value)
                {
                    m_Saving = value;
                }
            }
        }

        public event EventHandler Saved;

        /// <summary>
        /// raise the saved eventhandler
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnSaved(EventArgs e)
        {
            this.NeedToSave = false;
            if (this.Saved != null)
                this.Saved(this, e);
        }
        public WebSolutionSolution Solution
        {
            get { return m_Solution; }
           
                 set {
                if ((this.m_Solution != value)&&(value !=null))
                {
                    this.m_Solution = value;
                    OnWebSolutionChanged(EventArgs.Empty);
                }
            }
            
        }
        public void CreateFile(){
        }

        ICoreWorkingProjectSolution ICoreWorkingProjectSolutionSurface.Solution
        {
            get {
                return this.m_Solution;
            }
         
        }
        public event EventHandler WebSolutionChanged;
        ///<summary>
        ///raise the WebSolutionChanged 
        ///</summary>
        protected virtual void OnWebSolutionChanged(EventArgs e)
        {
            this.Title = this.Solution.Name;
            if (WebSolutionChanged != null)
                WebSolutionChanged(this, e);
        }

        public WebSolutionSurface()
        {
            this.InitializeComponent();
            this.Paint += _Paint;
            this.Load += _Load;
        }

     
        public override void LoadDisplayText()
        {
            base.LoadDisplayText();
            this.c_lb_choosecat.Text = "lb.pleasechoose.cat".R();
        }
        void _Load(object sender, EventArgs e)
        {
            this.LoadDisplayText();
            this.LoadSolutionCategories();
            this.c_exbox_categories.SelectedGroupChanged +=c_exbox_categories_SelectedGroupChanged;
            this.Title = this.Solution.Name;
            this.Solution.NameChanged += _SolutionNameChanged;
        }

        private void _SolutionNameChanged(object sender, EventArgs e)
        {
            this.Title = this.Solution.Name;
        }

        private void c_exbox_categories_SelectedGroupChanged(object sender, EventArgs e)
        {
            this.c_panel.SuspendLayout();
            this.c_panel.Controls.Clear();
            this.CurrentChild = null;
            if (this.c_exbox_categories.SelectedGroup == null)
            {                
                this.c_panel.Controls.Add(this.c_lb_choosecat);
            }
            else { 
                //build configuration
                WebSolutionCategoryItem r =
                    (this.c_exbox_categories.SelectedGroup as System.Windows.Forms.Control).Tag  as WebSolutionCategoryItem;
                if (r != null)
                {
                    CoreSystem.GetWorkbench().BuildWorkingProperty(
                        this.c_panel,
                        r);
                    //bind curface
                    ICoreWorkingSurface s = this;
                    if (r.GetConfigType() == enuParamConfigType.CustomControl)
                    {
                        var ps = this.c_panel.Controls[0] as ICoreWorkingSurface;
                        if (ps != null)
                        {
                            s = ps;
                            s.ParentSurface = this;
                            this.CurrentChild = s;
                        }
                    }                    
                    this.Workbench.CurrentSurface = s;
                }
            }
            this.c_panel.ResumeLayout ();
        }

        private void LoadSolutionCategories()
        {
            this.c_exbox_categories.Clear();

            WebSolutionCategoryItem.GetCategories(this.Solution);

            foreach(WebSolutionCategoryItem item in WebSolutionCategoryItem.GetCategories(this.Solution))
            {
                var g = this.c_exbox_categories.AddGroup(item.Name);
                g.Name = item.Name ;
                g.Tag = item;
            }

        }

        private void _Paint(object sender, CorePaintEventArgs e)
        {
            
        }
     

        public string FileName
        {
            get { return m_FileName; }
            set
            {
                if (m_FileName != value)
                {
                    m_FileName = value;
                    this.OnFileNameChanged(EventArgs.Empty);
                }
            }
        }
        public event EventHandler FileNameChanged;
        ///<summary>
        ///raise the FileNameChanged 
        ///</summary>
        protected virtual void OnFileNameChanged(EventArgs e)
        {
            if (FileNameChanged != null)
                FileNameChanged(this, e);
        }

        public void RenameTo(string newName)
        {
            this.m_Solution.Name = newName;
        }
        private bool m_NeedToSave;

        public bool NeedToSave
        {
            get { return m_NeedToSave; }
            set
            {
                if (m_NeedToSave != value)
                {
                    m_NeedToSave = value;
                    OnNeedToSaveChanged(EventArgs.Empty);
                }
            }
        }

        public event EventHandler NeedToSaveChanged;
        ///<summary>
        ///raise the NeedToSaveChanged 
        ///</summary>
        protected virtual void OnNeedToSaveChanged(EventArgs e)
        {
            if (NeedToSaveChanged != null)
                NeedToSaveChanged(this, e);
        }



        public void Save()
        {
            if (File.Exists(FileName))
            {
                this.Saving = true;
                this.m_Solution.Save(this.FileName);
                this.Saving = false;
                OnSaved(EventArgs.Empty);
            }
            else{
                Workbench.CallAction("File.SaveAs");
            }
        }

        public ICoreSaveAsInfo GetSaveAsInfo()
        {
            return new CoreSaveAsInfo(
                "title.SaveWebSolutionAs".R(),
                "websolution".R() +"| *.gkwebsln", 
                this.FileName 
                );
        }

        public void SaveAs(string filename)
        {
            Saving = true;
            this.Solution.Save(filename);
            Saving = false;
            OnSaved(EventArgs.Empty);
        }

        private void InitializeComponent()
        {
            this.c_panel = new IGKXPanel();
            this.c_lb_choosecat = new IGKXLabel();
            this.c_exbox_categories = new IGKXExpenderBox();
            this.c_splitter = new System.Windows.Forms.Splitter();
            this.c_panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // c_panel
            // 
            this.c_panel.CaptionKey = null;
            this.c_panel.Controls.Add(this.c_lb_choosecat);
            this.c_panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c_panel.Location = new System.Drawing.Point(228, 0);
            this.c_panel.Name = "c_panel";
            this.c_panel.Size = new System.Drawing.Size(393, 328);
            this.c_panel.TabIndex = 1;
            // 
            // lb_choosecat
            // 
            this.c_lb_choosecat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c_lb_choosecat.Font = new System.Drawing.Font("Arial Black", 26F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c_lb_choosecat.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.c_lb_choosecat.Location = new System.Drawing.Point(0, 0);
            this.c_lb_choosecat.Name = "lb_choosecat";
            this.c_lb_choosecat.Size = new System.Drawing.Size(393, 328);
            this.c_lb_choosecat.TabIndex = 0;
            this.c_lb_choosecat.Text = "Please Choose a Category";
            this.c_lb_choosecat.HorizontalAlignment = enuStringAlignment.Center;
            this.c_lb_choosecat.VerticalAlignment = enuStringAlignment.Center;
            this.c_lb_choosecat.AutoSize = false;

            // 
            // c_exbox_categories
            // 
            this.c_exbox_categories.CaptionKey = null;
            this.c_exbox_categories.Dock = System.Windows.Forms.DockStyle.Left;
            this.c_exbox_categories.Location = new System.Drawing.Point(0, 0);
            this.c_exbox_categories.Margin = new System.Windows.Forms.Padding(0);
            this.c_exbox_categories.Name = "c_exbox_categories";
            this.c_exbox_categories.SelectedGroup = null;
            this.c_exbox_categories.Size = new System.Drawing.Size(228, 328);
            this.c_exbox_categories.TabIndex = 0;
            // 
            // splitter1
            // 
            this.c_splitter.Location = new System.Drawing.Point(228, 0);
            this.c_splitter.Name = "splitter1";
            this.c_splitter.Size = new System.Drawing.Size(3, 328);
            this.c_splitter.TabIndex = 1;
            this.c_splitter.TabStop = false;
            // 
            // WebSolutionSurface
            // 
            this.Controls.Add(this.c_splitter);
            this.Controls.Add(this.c_panel);
            this.Controls.Add(this.c_exbox_categories);
            this.Name = "WebSolutionSurface";
            this.Size = new System.Drawing.Size(621, 328);
            this.c_panel.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        /// <summary>
        /// create the surface from the solution
        /// </summary>
        /// <param name="sln">solution</param>
        /// <returns></returns>
        internal static WebSolutionSurface Create(WebSolutionSolution sln)
        {
            if (sln == null)
                return null;
            WebSolutionSurface v_s = new WebSolutionSurface();
            v_s.m_Solution = sln;
            v_s.Title = sln.Name;
            v_s.FileName = sln.FileName;
            return v_s;
        }


        public void ReloadFileFromDisk()
        {
            //WebSolutionSolution sln = WebSolutionSolution.Open(this.FileName);
            //if (sln != null)
            //{ 
            //    //replace solution
            //    this.m_Solution = sln;
            //    this.Title = sln.Name;
            //    this.FileName = sln.FileName;
            //}
        }
    }
}
