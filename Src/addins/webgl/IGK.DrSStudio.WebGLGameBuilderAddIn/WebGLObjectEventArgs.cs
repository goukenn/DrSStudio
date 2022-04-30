namespace IGK.DrSStudio.WebGLEngine
{
    /// <summary>
    /// event class object
    /// </summary>
    public class WebGLObjectEventArgs<T> where T : class
    {
        private T m_object;
        private WebGLGameDesignScene scene;

        public T Object { get => m_object; }
        ///<summary>
        ///public .ctr
        ///</summary>
        public WebGLObjectEventArgs(T o)
        {
            this.m_object = o;

        }

        public WebGLObjectEventArgs(WebGLGameDesignScene scene)
        {
            this.scene = scene;
        }
    }
}