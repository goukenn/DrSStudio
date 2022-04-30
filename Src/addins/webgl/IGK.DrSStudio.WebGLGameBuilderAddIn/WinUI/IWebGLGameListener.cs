using IGK.ICore.Xml;

namespace IGK.DrSStudio.WebGLEngine.WinUI
{
    /// <summary>
    /// represent a game listener object
    /// </summary>
    public interface IWebGLGameListener
    {
        void InitGame(CoreXmlWebDocument document, CoreXmlElement webGLGameTag);
    }
}