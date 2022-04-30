namespace IGK.ICore.WinUI
{
    /// <summary>
    /// represent the base system workbench
    /// </summary>
    public interface ICoreSystemWorkbench: ICoreWorkingSurfaceHost
    {        
        /// <summary>
        /// initialize the app instance
        /// </summary>
        /// <param name="appInstance"></param>
        void Init(CoreSystem appInstance);
        /// <summary>
        /// call action
        /// </summary>
        /// <param name="cmd"></param>
        void CallAction(string cmd);
    }
}