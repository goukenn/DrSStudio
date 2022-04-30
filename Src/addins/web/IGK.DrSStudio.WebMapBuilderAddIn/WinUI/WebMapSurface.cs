

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WebMapSurface.cs
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
file:WebMapSurface.cs
*/
using IGK.DrSStudio.Web.WorkingObject;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.Web.WinUI
{
    [CoreSurface ("WebMapSurface",
        EnvironmentName = WebMapConstant.SURFACE_ENVIRONMENT)]
    public class WebMapSurface : 
        IGKD2DDrawingSurfaceBase
    {
        
        public override Type DefaultTool
        {
            get
            {
                return typeof(WebMapSelection);
            }
        }
        public new WebMapDocument  CurrentDocument{
            get {
                return base.CurrentDocument as WebMapDocument;
            }
        }

        public override bool IsToolValid(System.Type t)
        {
            return base.IsToolValid(t);
        }
     
        public WebMapSurface():base()
        {
        }
        public override void Save()
        {
            base.Save();
        }
        public override void SaveAs(string filename)
        {
            base.SaveAs(filename);
        }
        protected override IGKD2DDrawingSurfaceBase.IGKD2DDrawingDocumentCollections CreateDocumentCollections()
        {
            return  new WebDocumentCollection(this);
        }
        

        protected override Core2DDrawingDocumentBase CreateNewDocument()
        {
            return new WebMapDocument();
        }
        public static WebMapSurface CreateSurface(ICoreProject prInfo, ICoreWorkingDocument[] documents)
        {
            WebMapSurface v_s = new WebMapSurface();
            WebMapDocument[] d =  new ObjectConverter<WebMapDocument>().ToArray(documents);
            //List<WebMapDocument> t = new List<WebMapDocument>();
            //for (int i = 0; i < documents.Length; i++)
            //{
            //    if (documents[i] is WebMapDocument)
            //    {
            //        t.Add(documents[i] as WebMapDocument);
            //    }
            //}
            v_s.FileName = prInfo["FileName"].Value;
            (v_s.Documents as WebDocumentCollection).Replace(d);
            return v_s;
        }
        class ObjectConverter<T> 
        {
            public T[] ToArray(Array array)
            {
                List<T> t = new List<T>();
                for (int i = 0; i < array.Length; i++)
                {
                    var obj = array.GetValue(i);
                    if (obj is T)
                    {
                        t.Add((T)obj);
                    }
                }
                return t.ToArray();
            }
        }
        public override bool AllowMultiDocument
        {
            get
            {
                return false;
            }
        }
        class WebDocumentCollection : IGKD2DDrawingSurfaceBase.IGKD2DDrawingDocumentCollections
        {
            private WebMapSurface webMapSurface;
            public WebDocumentCollection(WebMapSurface webMapSurface):base(webMapSurface )
            {
                this.webMapSurface = webMapSurface;
            }
           
            internal void Replace(WebMapDocument[] doc)
            {
                if ((doc == null) || (doc.Length == 0)) 
                    return;
                
                
            }
        }
        /// <summary>
        /// export map
        /// </summary>
        /// <param name="p"></param>
        public  void ExportMap(string target)
        {
            //create to tempory folder
            string dir = string.Join(@"", Path.GetTempPath(), Guid.NewGuid().ToString());
            DirectoryInfo f = Directory.CreateDirectory(dir);
            if ((f != null) && f.Exists)
            {
                string file = string.Join("\\", dir, "index.html");
                File.WriteAllText(file, this.CurrentDocument.Render(null));
                //copy folder contains to target
                try
                {
                    string outdir = target + "/outpp";
                    if (Directory.Exists(outdir ))
                    {
                        Directory.Delete(outdir, true);
                    }
                    Directory.Move(dir, outdir);
                }
                catch (Exception ex){
                    CoreLog.WriteDebug("Move dir failed: "+ex.Message );
                }
            }
        }
    }
}

