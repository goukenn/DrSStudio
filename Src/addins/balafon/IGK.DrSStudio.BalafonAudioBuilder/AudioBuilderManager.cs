using IGK.ICore.WinCore.WinUI.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGK.DRSStudio.BalafonAudioBuilder
{
    public class AudioBuilderManager
    {
        static string[] m_components;


        public static string[] Components() {

            bool complete = false;
            //while (!complete)
            //{
            //    Application.DoEvents();
            //    Thread.Sleep(1000);
            //}


            var h =  new IGKXWebBrowserControl();
            h.ObjectForScripting = new AudioBuilderScriptData();

            var doc = IGK.ICore.Xml.CoreXmlWebDocument.CreateICoreDocument();

            doc.Body .Add ("div").Content = "OK";
            doc.Head.Add("script").SetAttribute ("type", "text/javascript").Content = @"";
            doc.Body["onload"]= @"javascript: window.external.loadComponent(navigator.userAgent+ ' '  +' ' + igk.html5.audioBuilder.getComponents());";


            h.AttachDocument (doc);
            h.Load += (o,e)=>{
                //Console.WriteLine ("document loaded");
                //Console.WriteLine("data 2 = " + ((AudioBuilderScriptData)h.ObjectForScripting).Data);
            }; 
           
            h.DocumentComplete+=(o,e)=> {
                //Console.WriteLine("loading complete ");
                //Console.WriteLine("data 3 = " + ((AudioBuilderScriptData)h.ObjectForScripting).Data);
                complete = true;

            };
            h.CreateControl ();
            while (!complete) {
                Application.DoEvents ();
                Thread.Sleep (1000);
            }
            string _data = ((AudioBuilderScriptData)h.ObjectForScripting).Data;

          Console.WriteLine ("data = "+ _data);
            //using (var frm = new Form())
            //{

            //    h.Dock = DockStyle.Fill;
            //    frm.Controls.Add(h);
            //    frm.ShowDialog();
            //    Console.WriteLine("data = " + ((AudioBuilderScriptData)h.ObjectForScripting).Data);
            //}
            m_components = _data?.Split (',');
            return null;
        }

        private static void H_Load(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
