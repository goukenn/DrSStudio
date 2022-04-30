using IGK.ICore.Codec;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.ICore.WinUI;
using System.Drawing.Imaging;
using System.IO;
using System.Drawing;

namespace IGK.DrSStudio.Drawing2D.Decoders
{
    [CoreCodecAttribute("Emf Decoder", "image/exif", "exif")]
    public class IGKEmfDecoder : CoreDecoderBase
    {
        public override bool Open(ICoreWorkbench bench, string filename, bool selectCreatedSurface)
        {
            using (var fs = File.Open(filename, FileMode.Open)) {
                //Metafile mfile = new Metafile(mem, hdc)
                Image bmp = Bitmap.FromFile(filename);


            }
            return false;
        }
    }
}
