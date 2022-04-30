
using IGK.ICore;
using IGK.ICore.IO;
using IGK.ICore.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IGK.DrSStudio.HelpManagerAddIn.WinUI
{
    static class HelpWebExtensions
    {
        public static void WaitToComplete(this WebBrowser c)
        {
            var v_c = new Waiter(c);
            v_c.Wait();
        }

        class Waiter
        {
            private WebBrowser c;
            private bool m_mustwait;

            public Waiter(WebBrowser c)
            {
                this.c = c;
                this.c.DocumentCompleted += c_DocumentCompleted;
            }

            void c_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
            {
                lock (this)
                {
                    this.m_mustwait = false;
                }
                this.c.DocumentCompleted -= c_DocumentCompleted;
            }

            internal void Wait()
            {
                this.m_mustwait = true;
                while (this.m_mustwait)
                {

                    Application.DoEvents();
                    System.Threading.Thread.Sleep(200);
                    if (!this.c.IsBusy)
                    {
                        break;
                    }
                }
              // this.c.DocumentCompleted -= c_DocumentCompleted;
            }
        }
    }
}
