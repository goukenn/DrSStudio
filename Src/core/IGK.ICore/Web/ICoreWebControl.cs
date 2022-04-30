using IGK.ICore.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.Web
{
    /// <summary>
    /// represent a core web control
    /// </summary>
    public interface ICoreWebControl
    {
        void SetReloadDocumentListener(ICoreWebReloadDocumentListener listener);
        /// <summary>
        /// get or set object for scripting
        /// </summary>
        object ObjectForScripting { get; set; }
        /// <summary>
        /// attach a document
        /// </summary>
        /// <param name="document"></param>
        void AttachDocument(CoreXmlWebDocument document);
        void LoadHtmlString(string htmltext);
        void LoadFromUriStream(string uri);
    }
}
