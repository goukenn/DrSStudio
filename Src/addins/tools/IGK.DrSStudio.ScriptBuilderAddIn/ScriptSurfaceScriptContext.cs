using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.DrSStudio.ScriptBuilderAddIn
{
    [Serializable()]
    public class ScriptSurfaceScriptContext : IScriptContext 
    {
        private string m_Response;
        /// <summary>
        /// get or set the string response
        /// </summary>
        public string Response
        {
            get { return m_Response; }
            set
            {
                if (m_Response != value)
                {
                    m_Response = value;
                    OnResponseChanged(EventArgs.Empty);
                }
            }
        }
        public event EventHandler ResponseChanged;

        protected virtual void OnResponseChanged(EventArgs e)
        {
            if (ResponseChanged != null)
            {
                ResponseChanged(this, e);
            }
        }


    }
}
