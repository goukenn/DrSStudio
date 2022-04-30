

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ProjectSelectorSurface.cs
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
file:ProjectSelectorSurface.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.ProjectSelectorAddIn.WinUI
{
    using IGK.ICore;using IGK.DrSStudio.WinUI;
    using IGK.DrSStudio.Drawing2D;
    using IGK.DrSStudio.Drawing3D;
    using IGK.OGLGame.Graphics;
    using IGK.OGLGame.WinUI.GLControls;
    [CoreWorkingObject (CoreConstant.DRS_PROJECT_SELECTOR )]
    public sealed class ProjectSelectorSurface : GLPanel, ICoreProjectSelector, ICoreWorkingObject
    {
        List<SelectProjectItem> m_projects;
        Texture2D m_texture;
        private SelectProjectItem m_SelectedItem;
        SelectProjectItem SelectedItem
        {
            get { return m_SelectedItem; }
            set
            {
                if (m_SelectedItem != value)
                {
                    m_SelectedItem = value;
                    OnSelectedItemChanged(EventArgs.Empty);
                }
            }
        }
        public event EventHandler SelectedItemChanged;
        private void OnSelectedItemChanged(EventArgs eventArgs)
        {
            if (this.SelectedItemChanged != null)
                this.SelectedItemChanged(this, eventArgs);
        }
        protected override void Initialize()
        {
            base.Initialize();
        }
        protected override void LoadContent()
        {
            base.LoadContent();
            m_texture = Texture2D.Load(this.GraphicsDevice, 64, 64);
        }
        protected override void UnloadContent()
        {
            base.UnloadContent();
            m_texture.Dispose();
        }
        public ProjectSelectorSurface()
        {
            m_projects = new List<SelectProjectItem>();
            this.MinimumSize = new System.Drawing.Size(448, 400);
            for (int i = 0; i < 100; i++)
            {
                AddProject(new DummyProjectInfo() {
                ProjectName ="DrsStudio 2D",
                ImageKey= "Default_projectKey"
                });
            }
        }
        public int ProjectCount
        {
            get { return this.m_projects.Count; }
        }
        public void AddProject(ICoreProjectInfo project)
        {
            if ((project == null) || Contains(project))
                return;
            SelectProjectItem pjr = new SelectProjectItem(this,project);
            this.m_projects.Add(pjr);
        }
        bool Contains(ICoreProjectInfo project)
        {
            for (int i = 0; i < this.m_projects .Count; i++)
            {
                if (this.m_projects[i].ProjectInfo == project)
                    return true;
            }
            return false;
        }
        SelectProjectItem GetProjectItem(ICoreProjectInfo project)
        {
            for (int i = 0; i < this.m_projects.Count; i++)
            {
                if (this.m_projects[i].ProjectInfo == project)
                    return this.m_projects[i];
            }
            return null;
        }
        public void RemoveProject(ICoreProjectInfo project)
        {
            SelectProjectItem  i = GetProjectItem(project);
            if (i != null)
            {
                this.m_projects.Remove(i);
            }
        }
        protected override void OnSizeChanged(EventArgs e)
        {
            UpdateItemBounds();
            base.OnSizeChanged(e);
        }
        protected override void OnMouseMove(System.Windows.Forms.MouseEventArgs e)
        {
            base.OnMouseMove(e);
            this.Invalidate();
        }
        void UpdateItemBounds()
        {
            float x = 4.0f;
            float y = 4.0f;
            float w = 64;
            float h = 64;
            int v_i = (int)Math.Ceiling((this.Width - 8) / w);
            int v_j = (int)Math.Ceiling((this.Height - 8) / h);
            int i = 0;
            int j = 0;
            for (int k = 0; k < this.m_projects.Count; k++)
            {
                this.m_projects[k].Bound = new Rectanglef(
                    x + (w + 4) * i, y + j * (h + 4), w, h
                    );
                i++;
                if (i >= v_i)
                {
                    i = 0;
                    j++;
                }
            }
        }
        protected override void Render(OGLGame.Graphics.GraphicsDevice device, IGLControlTime Time)
        {
            base.Render(device, Time);
            this.SpriteBatch.Begin();
            foreach (SelectProjectItem item in this.m_projects)
            {
                item.Render(device);
            }
            this.SpriteBatch.End();
        }
        public string Id
        {
            get { return "ProjectSelector"; }
        }
        class SelectProjectItem
        {
            private int m_State;           
            const int ST_DOWN = -1;
            const int ST_NONE = 0;
            const int ST_HOVER = 1;
            private Rectanglef  m_Bound;
            private ICoreProjectInfo m_projectInfo;
            public ICoreProjectInfo ProjectInfo
            {
                get { return m_projectInfo; }
            }
            /// <summary>
            /// get or set the bound of this element
            /// </summary>
public Rectanglef  Bound{
get{ return m_Bound;}
set{ 
        if (!m_Bound.Equals (value))
        {
        m_Bound =value;
        }
    }
    }
            public bool IsHover
            {
                get { return this.m_State == ST_HOVER; }            
            }
            public bool IsDown {
                get { return this.m_State == ST_DOWN; }
            }
            public int State
            {
                get { return m_State; }
                set
                {
                    if (m_State != value)
                    {
                        m_State = value;
                        m_surface.Invalidate();
                    }
                }
            }
            ProjectSelectorSurface m_surface;
            public SelectProjectItem(ProjectSelectorSurface item ,ICoreProjectInfo project)
            {
                this.m_projectInfo = project;
                this.m_surface = item;
                Regisger();
            }
            private void Regisger()
            {
                this.m_surface.MouseMove += new System.Windows.Forms.MouseEventHandler(m_surface_MouseMove);
                this.m_surface.MouseLeave += _surfaceMouseLeave;
                this.m_surface.MouseClick += new System.Windows.Forms.MouseEventHandler(m_surface_MouseClick);
            }
            void m_surface_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
            {
                if (this.IsHover )
                this.m_surface.SelectedItem = this;
            }
            void _surfaceMouseLeave(object sender, System.EventArgs e)
            {
                if (this.IsHover)
                    this.State = ST_NONE;
            }
            void m_surface_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
            {
                if (this.Bound.Contains(e.Location))
                {
                    this.State = ST_HOVER;
                }
                else {
                    this.State = ST_NONE;
                }
            }
            public void Render(GraphicsDevice device)
            {
                Colorf clf = Colorf.FromFloat(0.7f, 0.4f);
                Rectanglef v_rc = this.Bound;
                if (IsHover)
                {
                    clf = Colorf.FromFloat(0.7f, Colorf.Yellow);
                    v_rc.Inflate(3, 3);
                }
                else if (IsDown)
                {
                    clf = Colorf.FromFloat(0.7f, Colorf.DarkBlue);
                }               
                GLDrawUtils.FillRoundRect(device, clf, v_rc, 4);
                if (this.m_surface.m_texture != null)
                {
                    v_rc.Inflate(-2, -2);
                    using (System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(
                        this.m_surface.m_texture.Width,
                        this.m_surface.m_texture.Height))
                    {
                        System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bmp);
                        //g.Clear(Colorf.Black);
                        ICore2DDrawingDocument  doc = CoreResources.GetDocument(this.ProjectInfo.ImageKey);
                        if (doc != null)
                            doc.Draw(g, new Rectanglei(0, 0, bmp.Width, bmp.Height));
                        this.m_surface.m_texture.ReplaceTexture(bmp);
                    }
                    this.m_surface.SpriteBatch.Draw(this.m_surface.m_texture, Rectanglei.Round (v_rc), Colorf.White );
                }
            }
        }
        class DummyProjectInfo : ICoreProjectInfo
        {
            private string m_ProjectName;
            public string ProjectName
            {
                get { return m_ProjectName; }
                set
                {
                    if (m_ProjectName != value)
                    {
                        m_ProjectName = value;
                    }
                }
            }
            private string m_ImageKey;
            public string ImageKey
            {
                get { return m_ImageKey; }
                set
                {
                    if (m_ImageKey != value)
                    {
                        m_ImageKey = value;
                    }
                }
            }
            public virtual Type SurfaceType
            {
                get { return typeof (IGK.DrSStudio.WinUI.XDrawing2DSurface ); }
            }
        }
        ICoreProjectInfo ICoreProjectSelector.SelectedItem
        {
            get { return this.SelectedItem.ProjectInfo; }
        }
    }
}

