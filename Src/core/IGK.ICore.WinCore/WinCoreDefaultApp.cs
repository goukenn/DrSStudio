
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.WinCore
{
    using IGK.ICore.Drawing2D;
    using IGK.ICore.IO;
    using IGK.ICore.WinUI;
    using System.Windows.Forms;

    public class WinCoreDefaultApp : WinCoreApplication 
    {
        private Dictionary<Type , object> m_services;

        public WinCoreDefaultApp()
        {
            this.m_services = new Dictionary<Type, object>();

            this.m_services.Add(typeof(ICoreClipboard), WinCoreClipBoard.Instance);
        }
        protected override ICoreControlManager CreateControlManager()
        {
            return new WinCoreControlManager();
        }    
     

        public override void AddMessageFilter(ICoreMessageFilter messageFilter)
        {
            CoreLog.WriteLine("no message filtering implement");
        }

        public override void RemoveMessageFilter(ICoreMessageFilter messageFilter)
        {
            CoreLog.WriteLine("no message filtering implement");
        }

        public override bool RegisterServerSystem(Func<CoreSystem> __initInstance)
        {
            __initInstance();
            return true;
        }

        public override bool RegisterClientSystem(Func<CoreSystem> __initInstance)
        {
            return false;
        }

        public override bool IsTransparentProxy(object obj)
        {
            return System.Runtime.Remoting.RemotingServices.IsTransparentProxy(obj);
        }

        public override ICoreScreenInfo GetScreenInfo()
        {
            return WinCoreScreenInfo.Instance; 
        }

        public override string PrivateFontsDirectory
        {
            get {
                return PathUtils.GetPath ("%startup%/Fonts");
            }
        }

        public override string AppName
        {
            get
            {
                return WinCoreConstant.WIN_CORE_LIB_NAME;
            }
        }

        public override string Copyright
        {
            get
            {
                return CoreConstant.COPYRIGHT;
            }
        }

        public override string AppAuthor
        {
            get
            {
                return CoreConstant.AUTHOR;
            }
        }

        public override T GetService<T>()
        {
            if (m_services.ContainsKey (typeof(T)))
            {
                return (T)m_services[typeof (T)];
            }
            return base.GetService<T>();
        }
      
        sealed class WinClipBoard : ICoreClipboard  {

            public void CopyToClipboard(object obj)
            {
                if (obj is String)
                {
                    CopyToClipboard(DataFormats.Text, obj);
                }
            }
            public void CopyToClipboard(string dataType, object obj)
            {
                if ((obj ==null ) || string.IsNullOrEmpty (dataType))
                    return;
                WinCoreClipBoard.Copy(dataType, obj);
            }


            public string GetTextData()
            {
                if (Clipboard.ContainsText() == false)
                    return string.Empty;
                return Clipboard.GetText();
            }
        }
    }
}
