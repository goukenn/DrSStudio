

using IGK.ICore.Codec;
/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Core2DDrawingLayerDocument.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:Core2DDrawingLayerDocument.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.ICore.Drawing2D
{
    [Core2DDrawingDocumentAttribute(CoreConstant.LAYEREDDOCUEMENT)]
    public class Core2DDrawingLayerDocument : Core2DDrawingDocumentBase, ICoreParameterDefinition
    {
        Dictionary<Type, object> m_params = new Dictionary<Type, object> ();
        public T GetParam<T>() {
            var t = typeof(T);
            if (m_params.ContainsKey(t)) {
                return (T)m_params[t];

            }
            return default(T);
        }


        [CoreXMLAttribute()]
        [CoreXMLDefaultAttributeValue(null)]
        /// <summary>
        /// get or set the id of this document
        /// </summary>
        public override string Id
        {
            get
            {
                return base.Id;
            }
            set
            {
                if (!string.IsNullOrEmpty (value ) && (this.Container != null)) {
                    var ss = this.Container.GetElementById(value);
                    if (ss !=null)
                        return ;
                }
                base.Id = value;
            }
        }
        public override void Dispose()
        {
            base.Dispose();
        }
        public Core2DDrawingLayerDocument():base("400px", "300px") {
            
        }
        public Core2DDrawingLayerDocument(CoreUnit width, CoreUnit height)
            : base(width, height)
        { 
        }
        /// <summary>
        /// create a new layer
        /// </summary>
        /// <returns></returns>
        protected override ICore2DDrawingLayer CreateNewLayer()
        {
            return new Core2DDrawingLayer();
        }

        public static Core2DDrawingLayerDocument CreateFromBitmap(ICoreBitmap bmp)
        {
            ImageElement v_img = ImageElement.CreateFromBitmap(bmp);
            if (v_img  == null)
                return null;
            Core2DDrawingLayerDocument v_doc = new Core2DDrawingLayerDocument();
            v_doc.SetSize(v_img.Width, v_img.Height);
            v_doc.CurrentLayer.Elements.Add(v_img);
            return v_doc;
            
        }

        public static Core2DDrawingLayerDocument CreateForm(PathElement p)
        {
            var v_rc = p.GetBound();
            Core2DDrawingLayerDocument doc = new Core2DDrawingLayerDocument(v_rc.Width, v_rc.Height);
            p.Translate(-v_rc.X, -v_rc.Y, enuMatrixOrder.Append );
            doc.CurrentLayer.Elements.Add(p);
            return doc;
        }
    }
}

