

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _ViewCurrentDocumentAsBobyBG.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/

using IGK.ICore;
using IGK.ICore.Codec;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.DrSStudio;
using IGK.DrSStudio.Menu;
using IGK.DrSStudio.WinUI;
using IGK.DrSStudio.Drawing2D;
using IGK.DrSStudio.Drawing2D.WinUI;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:_ViewCurrentDocumentAsBobyBG.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Windows.Forms;
namespace IGK.DrSStudio.WebAddIn.Menu.Web.Tools
{
    using IGK.ICore.Imaging;
    using IGK.DrSStudio.WebAddIn.Tools;
    using IGK.DrSStudio.Drawing2D.Menu;
    [DrSStudioMenu("Tools.Web.ViewCurrentDocAsBodyBG",
        0x301)]
    class WebViewCurrentDocumentAsBobyBGMenu : Core2DDrawingMenuBase 
    {
        public WebViewCurrentDocumentAsBobyBGMenu()
        {
            m_tempfiles = new List<string>();
            Application.ApplicationExit += new EventHandler(Application_ApplicationExit);
        }
        void Application_ApplicationExit(object sender, EventArgs e)
        {
            this.FreeTempFile();
        }
        List<string> m_tempfiles;
        /// <summary>
        /// free temp files
        /// </summary>
        void FreeTempFile()
        {
            foreach (string t in m_tempfiles)
            {
            global::System.IO.File.Delete(t);
            }
            m_tempfiles.Clear();
        }
        protected override bool PerformAction()
        {
            FreeTempFile();
            using (ICoreBitmap bmp = this.CurrentSurface.CurrentDocument.ToBitmap ())
            {
                string v_temp = Path.GetTempFileName();
                bmp.Save(v_temp, CoreBitmapFormat.Png);
                string outf = Path.Combine(Path.GetDirectoryName(v_temp),
                    Path.GetFileNameWithoutExtension(v_temp) + ".png");

                System.IO.File.Move(v_temp,outf);
                StringBuilder sb = new StringBuilder();

                string base64  = Convert.ToBase64String(File.ReadAllBytes(outf));

                sb.AppendLine (@"<body id=""body_bgtest"" style=""background-image:url('data:image/png;base64, "+base64
                    +@"');"">Background Preview document </body>");
               
                WebPreviewerCodeManager.Instance.InitSurface ("title.web.BackgroundPreviewEditor".R());
                WebPreviewerCodeManager.Instance.HtmlPreviewSurface.DocumentText = sb.ToString();
                this.m_tempfiles.Add(v_temp);
            }
            return false;
        }
    }
}

