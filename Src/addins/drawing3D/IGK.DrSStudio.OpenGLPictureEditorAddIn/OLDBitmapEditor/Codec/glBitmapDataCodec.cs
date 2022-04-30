

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: glBitmapDataCodec.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV - [2008 - 2014]
author: C.A.D . BONDJE DOUE
file:glBitmapDataCodec.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace IGK.DrSStudio.OGLBitmapEditor
{
    
using IGK.ICore;using IGK.DrSStudio.Drawing2D;
    [IGK.DrSStudio.Codec.CoreCodec ("glBitmapData", "gkds/gl-bitmap-data", "data", Category=CoreConstant.CAT_Picture )]
    class glBitmapDataEncoder : IGK.DrSStudio.Codec.CoreEncoderBase
    {
        public override bool Save(IGK.DrSStudio.WinUI.ICoreWorkingSurface surface, string filename, ICoreWorkingDocument[] documents)
        {
            bool save = false;
            foreach (ICoreWorkingDocument  doc in documents)
            {
                ICore2DDrawingDocument document = doc as ICore2DDrawingDocument ;
                if (document ==null)
                    continue ;
                save = true;
                SaveDocument(filename, document);
            }
            return save ;
        }
        private void SaveDocument(string filename, ICore2DDrawingDocument document)
        {
            List<WorkingElements.PixelMarquer> v_pix = new List<IGK.DrSStudio.OGLBitmapEditor.WorkingElements.PixelMarquer>();
            List<Vector2i> v_points = new List<Vector2i>();
            foreach (ICore2DDrawingLayer l in document.Layers)
            {
                foreach (ICore2DDrawingLayeredElement  item in l.Elements)
                {
                    if (item is WorkingElements.PixelMarquer)
                    {
                        v_points.AddRange ((item as WorkingElements.PixelMarquer).Marks );
                    }
                }
            }
            if (v_points.Count > 0)
            {
                v_points.Sort(new MatrixValComparer());
                //get bitarray
                int w = document.Width;
                int h = document.Height ;
                System.Collections.BitArray bitArray = new System.Collections.BitArray(document.Width * document.Height);
                foreach (Vector2i item in v_points)
                {
                    bitArray[item.X + (h -item.Y-1)*w] = true; 
                }
                System.IO.BinaryWriter binW = new System.IO.BinaryWriter(File.Create(filename));
                int v = 0;
                int i = 0;
                for (; i < bitArray.Length  ; i++)
                {
                    if ((i != 0) && ((i % 8) == 0))
                    {
                        binW.Write((byte)v);
                        v = 0;
                    }
                    v = (v << 1);
                    if (bitArray[i])
                    {
                        v = v  + 1;
                    }
                }
                    binW.Write((byte)v);
                binW.Flush();
                binW.Close();
            }
        }
        class MatrixValComparer : IComparer<Vector2i >
        {
            #region IComparer<Vector2i> Members
            public int Compare(Vector2i x, Vector2i y)
            {
                /*int i = (x.X + x.Y * x.X);
                int j = (y.X + y.Y * y.X);
                return i.CompareTo(j);*/
                int c = x.Y.CompareTo(y.Y); 
                if (c == 0)
                    return (x.X.CompareTo(y.X));
                return c;
                //int i = (x.X + x.Y * x.X);
                //int j = (y.X + y.Y * y.X);
                //return i.CompareTo(j);
            }
            #endregion
        }
    }
}

