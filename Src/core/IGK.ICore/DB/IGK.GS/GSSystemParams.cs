using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.GS
{
    /// <summary>
    /// represent default gs parameter
    /// </summary>
    public static class GSSystemParams
    {
        private static string m_Prompt;
        private static string m_WelcomeMessage;

        public static string WelcomeMessage
        {
            get { return m_WelcomeMessage; }
            set
            {
                if (m_WelcomeMessage != value)
                {
                    m_WelcomeMessage = value;
                }
            }
        }
        public static string Prompt
        {
            get { return m_Prompt; }
            set
            {
                if (m_Prompt != value)
                {
                    m_Prompt = value;
                }
            }
        }

        public static DateTime LastLogin { get; set; }
    }
}
