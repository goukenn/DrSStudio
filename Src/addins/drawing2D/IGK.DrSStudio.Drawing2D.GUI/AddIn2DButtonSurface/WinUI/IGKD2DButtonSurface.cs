

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKD2DButtonSurface.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
using IGK.ICore;
using IGK.ICore.Drawing2D;
using IGK.ICore.WinUI;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:IGKD2DButtonSurface.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.ICore.Drawing2D.WinUI;
namespace IGK.DrSStudio.Drawing2D.WinUI
{
    [CoreSurface("ButtonSurface", EnvironmentName = CoreConstant.DRAWING2D_ENVIRONMENT)]
    public class IGKD2DButtonSurface : IGKD2DDrawingSurface 
    {
        public static new IGKD2DButtonSurface CreateSurface(GKDSElement element)
        {
            IGKD2DButtonSurface c = new IGKD2DButtonSurface();
            DocumentElement v_documents =  element.getElementTagObjectByTagName(CoreConstant.TAG_DOCUMENTS) as DocumentElement ;
            
            List<Core2DDrawingDocumentBase> doc = new List<Core2DDrawingDocumentBase>();
            doc.AddRange(v_documents.Documents.ToArray().ConvertTo<Core2DDrawingDocumentBase>());
            if (doc.Count == 5)
            {               
                c.Documents.Replace(doc.ToArray());
            }
            ProjectElement v_projectInfo = element.GetProject ();
            if (v_projectInfo != null)
            {
                //c.FileName = v_projectInfo["FileName"];
                c.FileName = v_projectInfo.GetValue("FileName");
            }
            c.NeedToSave = false;
            return c;
        }

        public static IGKD2DButtonSurface CreateSurface(CoreButtonDocument btn)
        {
            if (btn == null)
                return null;
            IGKD2DButtonSurface v_s = new IGKD2DButtonSurface();
            v_s.Documents.Replace(btn.GetDocuments().ConvertTo<Core2DDrawingDocumentBase>());
            v_s.NeedToSave = false;
            return v_s;

        }
        
        public IGKD2DButtonSurface()
        {
        }
        public override bool AllowMultiDocument
        {
            get
            {
                return false;
            }
        }
        public override void SetParam(ICoreInitializatorParam p)
        {
            int w = (int)Math.Ceiling(((CoreUnit)p["width"]).GetPixel());
            int h = (int)Math.Ceiling( ((CoreUnit ) p["width"]).GetPixel());
            foreach (Core2DDrawingDocumentBase  item in this.Documents)
            {
                item.SetSize(w, h);
            }
        }
        protected override IGKD2DDrawingSurfaceBase.IGKD2DDrawingDocumentCollections CreateDocumentCollections()
        {
            return new IGK2DDButtonSurfaceDocumentCollection(this);
        }
        class IGK2DDButtonSurfaceDocumentCollection : IGKD2DDrawingSurfaceBase.IGKD2DDrawingDocumentCollections
        {           
            public IGK2DDButtonSurfaceDocumentCollection(IGKD2DButtonSurface surface):base(surface)
            {
                //add 4 document
                base.Add(surface.CreateNewDocument());
                base.Add(surface.CreateNewDocument());
                base.Add(surface.CreateNewDocument());
                base.Add(surface.CreateNewDocument());
            } 
        }
    }
}

