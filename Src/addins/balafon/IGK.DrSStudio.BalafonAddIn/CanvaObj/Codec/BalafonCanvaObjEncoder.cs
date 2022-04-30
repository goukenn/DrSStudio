using IGK.ICore;
using IGK.ICore.Codec;
using IGK.ICore.Drawing2D.WinUI;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Balafon.Codec
{
    /// <summary>
    /// gcobj for gkds canva object encoder. save as json array
    /// </summary>
    [CoreCodec("balafon-project", "balafon/canvas-object", "gcobj", Category=CoreConstant.CAT_PICTURE )]
    class BalafonCanvaObjEncoder : CoreEncoderBase 
    {
        public override bool Save(ICoreWorkingSurface surface, string filename, params ICore.ICoreWorkingDocument[] documents)
        {
            if (surface is ICore2DDrawingSurface)
            {
                var c  = new BalafonCanvaObjEncoderVisitor();
                c.Visit((surface as ICore2DDrawingSurface).Documents.ToArray());
                File.WriteAllText(filename, c.Data);
                return true;
            }
            return false;
        }
    }
}
