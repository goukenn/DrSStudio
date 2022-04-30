using IGK.GS.WinUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.GS
{
    /// <summary>
    /// represent the log class
    /// </summary>
    public class GSLog : IDisposable
    {
        private StreamWriter sm_sw;
        private static GSLog sm_instance;
        private string m_LogFile;
        private IGSLogMessageReciever m_receiver;

        public static void SetReceiver(IGSLogMessageReciever receiver)
        {
            sm_instance.m_receiver = receiver;
        }
        /// <summary>
        /// get or set the log file
        /// </summary>
        public string LogFile
        {
            get { return m_LogFile; }
            set
            {
                if (m_LogFile != value)
                {
                    m_LogFile = value;
                }
            }
        }

        private GSLog()
        {

            sm_sw = null;// new StreamWriter(new MemoryStream());
        }

        public static GSLog Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static GSLog()
        {
            sm_instance = new GSLog();
        }
        public static void WriteLine(string msg)
        {
            Debug.WriteLine(msg);
            WriteReceiver(msg);
        }
        static void WriteReceiver(string msg)
        {
            if (sm_instance.m_receiver != null)
            {
                sm_instance.m_receiver.SendMessage(msg);
            }
        }
        public static void WriteDebug(string msg)
        {
#if DEBUG
            Debug.WriteLine(msg);
            WriteReceiver(msg);
#endif
        }


        static void Application_ApplicationExit(object sender, EventArgs e)
        {
            sm_instance.Dispose();
        }

        public void Dispose()
        {
            if (sm_sw != null)
            {
                sm_sw.Flush();
                sm_sw.Close();
                sm_sw = null;
            }
        }
        public static void WriteLog(string text)
        {
            sm_instance.__writeLog(text);
        }

        private void __writeLog(string text)
        {
            if (sm_sw == null)
                return;
            sm_sw.WriteLine(DateTime.Now.ToString() + ":->" + text);
            sm_sw.Flush();
        }

        /// <summary>
        /// console write line
        /// </summary>
        /// <param name="p"></param>
        public static void CWriteLine(string p, bool writelog)
        {
            Console.WriteLine(p);
            if (writelog)
                WriteLog(p);
            WriteReceiver(p);
        }
        /// <summary>
        /// console write line
        /// </summary>
        /// <param name="p"></param>
        public static void CWriteLine(string p)
        {
            CWriteLine(p, true);
        }
        /// <summary>
        /// console write line
        /// </summary>
        /// <param name="p"></param>
        public static void CWrite(string p)
        {
            Console.Write(p);
            WriteLog(p);
            WriteReceiver(p);
        }
        public static void EWriteLine(string p)
        {//write error message
            EWriteLine(p, true);
        }
        public static void EWriteLine(string p, bool writelog)
        {//write error message

#if !__ANDROID__
            ConsoleColor cl = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
#endif
            Console.WriteLine(p);
#if !__ANDROID__
            Console.ForegroundColor = cl;
#endif
            if (writelog)
                WriteLog(p);
        }
    }
}
