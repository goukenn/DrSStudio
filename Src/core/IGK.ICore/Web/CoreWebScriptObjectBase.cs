
using IGK.ICore;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using IGK.ICore.Web;
using IGK.ICore.Xml;

namespace IGK.ICore.Web
{
    [ComVisible(true)]
    public class CoreWebScriptObjectBase : ICoreWebScriptObject
    {
        private ICoreDialogForm m_dialog;
        private ICoreWebReloadDocumentListener m_ReloadListener;
        private ICoreWebScriptListener m_ScriptListener;
        private CoreXmlWebDocument m_document;

        protected ICoreWebReloadDocumentListener ReloadListener {
            get { return this.m_ReloadListener; }
        }

        public virtual bool UpdateData(string data) {
            return false;
        }
        public enuDialogResult DialogResult {
            get {
                if (this.m_dialog !=null )
                return this.m_dialog.DialogResult;
                return enuDialogResult.None;
            }
            set {
                if (this.m_dialog !=null)
                this.m_dialog.DialogResult  = value;
            }
        }
      
        /// <summary>
        /// set object used for reloading document content
        /// </summary>
        /// <param name="ReloadListener"></param>
        public void SetReloadListener(ICoreWebReloadDocumentListener ReloadListener) {
            m_ReloadListener = ReloadListener;
        }
        /// <summary>
        /// set document that will be used for invoking script method
        /// </summary>
        /// <param name="scriptListener"></param>
        public void SetScriptListener(ICoreWebScriptListener scriptListener) {
            this.m_ScriptListener = scriptListener;
        }
        /// <summary>
        /// get or set the dialog
        /// </summary>
        public ICoreDialogForm Dialog
        {
            get
            {
                return this.m_dialog;
            }
            set
            {
                this.m_dialog = value;
            }
        }

        public CoreXmlWebDocument Document
        {
            get
            {
                return this.m_document;
            }
            set
            {
                if (this.m_document != value)
                {
                    this.m_document = value;
                    OnDocumentChanged(EventArgs.Empty);
                }
            }
        }
        public event EventHandler DocumentChanged;
        protected virtual void OnDocumentChanged(EventArgs eventArgs)
        {
            if (this.DocumentChanged != null)
                this.DocumentChanged(this, eventArgs);
        }
        public virtual void Reload() {
            if ((this.m_ReloadListener != null)&&(this.Document!=null)) {
                this.m_ReloadListener.ReloadDocument(this.Document, true );
            }
        }
        public void InvokeScript(string p)
        {
            if ((this.m_ScriptListener != null)){
                var k = this.m_ScriptListener.InvokeScript(p);
            }            
        }


        //script utility fonction
        protected void JSInvokeMsBox(string title, string message)
        {
            this.InvokeScript(string.Format("ns_igk.winui.notify.showMsBox('{0}', '{1}')",
                title,
                message));
        }
        public virtual void Submit(object data)
        {
        }
        public string getLang(string k) {
            if (k == null)
                return null;
            return k.R();
        }
        /// <summary>
        /// call script function 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="arg"></param>
        public virtual object  CallFunc(string name, string arg)
        {
            var m = this.GetType().GetMethod(name, System.Reflection.BindingFlags.Public |
                System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Instance );
            if (m != null) {
                 //var targs = new IGK.ICore.JSon.CoreJSon().ToDictionary(arg, "cl");
                 if (arg == null) {
                     return m.Invoke(this, new object[0]);
                 }
                 else
                    return m.Invoke(this, new string[]{arg});
            }
            return null;
        }

        /// <summary>
        /// public void notification
        /// </summary>
        /// <param name="args"></param>
        public virtual object Notify(string args)
        {
            var d = IGK.ICore.JSon.CoreJSonReader.Load(args);
            var m = d.ContainsKey("method") ? d["method"] : null;
            var p = d.ContainsKey("param") ? d["param"] : null;
            object output = null;
            if (m != null)
            {
                var meth = this.GetType().GetMethod(m.ToString(), System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.IgnoreCase);

                if (meth != null)
                {
                    try
                    {
                        output =  meth.Invoke(this, p != null ? new object[] { p } :
                            (meth.GetParameters()?.Length > 0 ? new object[] { null } : new object[0]));
                    }
                    catch (Exception ex) {
                        System.Diagnostics.Debug.WriteLine(ex.Message);
                    }
                }
            }
            return output;
        }


    }
}
