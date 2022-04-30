using IGK.ICore.Web;
using IGK.ICore.Xml;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.WebGLEngine
{
    /// <summary>
    /// represent a game design project
    /// </summary>
    public class WebGLGameDesingProject
    {
        private WebGLGameDesingSceneCollections m_scenes;
        public event WebGLObjectEventHandler<WebGLGameDesignScene> SceneAdded;
        public event WebGLObjectEventHandler<WebGLGameDesignScene> SceneRemoved;

        ///<summary>
        ///public .ctr
        ///</summary>
        public WebGLGameDesingProject()
        {
            m_scenes = new WebGLGameDesingSceneCollections(this);
        }

        public WebGLGameDesingSceneCollections Scenes { get => m_scenes; }

        public static WebGLGameDesingProject LoadFromFile(string webprojfile) { 
            throw new NotImplementedException(); 
        }
        public void Save(string outfile) {

            var p = CoreXmlElement.CreateXmlNode("Project");
            p["xmlns"] = WebGLGameConstant.XML_NAMESPACE; 
            File.WriteAllText(outfile, p.RenderToXml());

        }

        /// <summary>
        /// represent a scene collections
        /// </summary>
        public class WebGLGameDesingSceneCollections : IEnumerable
        {
            private WebGLGameDesingProject m_project;
            private List<WebGLGameDesignScene> m_list;

            public int Count => m_list.Count;

            public WebGLGameDesingSceneCollections(WebGLGameDesingProject webGLGameDesingProject)
            {
                this.m_project = webGLGameDesingProject;
                this.m_list = new List<WebGLGameDesignScene>();
            }

            public IEnumerator GetEnumerator()
            {
                return this.m_list.GetEnumerator();
            }

            internal void Add(WebGLGameDesignScene Scene)
            {
                if (!this.m_list.Contains(Scene))
                {
                    this.m_list.Add(Scene);
                    Scene.Index = this.m_list.Count - 1;
                    this.m_project.OnSceneAdded(new WebGLObjectEventArgs<WebGLGameDesignScene>(Scene));
                }
            }
            internal void Remove(WebGLGameDesignScene Scene) {
                if (this.m_list.Contains(Scene)) {
                    this.m_list.Remove(Scene);
                    this.m_project.OnSceneRemoved(new WebGLObjectEventArgs<WebGLGameDesignScene>(Scene));

                }
            }
        }

        protected virtual void OnSceneAdded(WebGLObjectEventArgs<WebGLGameDesignScene> e)
        {
            this.SceneAdded?.Invoke(this, e);
        }

        protected virtual void OnSceneRemoved(WebGLObjectEventArgs<WebGLGameDesignScene> e)
        {
            this.SceneRemoved?.Invoke(this, e);
        }
    }
}
