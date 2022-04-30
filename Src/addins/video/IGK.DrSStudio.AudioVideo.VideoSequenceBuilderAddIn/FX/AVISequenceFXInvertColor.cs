using IGK.ICore.Drawing2D;
using IGK.ICore.GraphicModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.AudioVideo.FX
{
    using IGK.ICore;
    using IGK.ICore.WinCore;
    
    public class AVISequenceFXInvertColor : AVISequenceFXBase
    {
        public override void Update(IAVISequenceUpdateInfo update)
        {
            base.Update(update);
        }
        public override void Render(ICoreGraphics graphics)
        {
            ICoreBitmap g = graphics.Copy(new Rectanglei(0,0, this.Chain.DocumentWidth, this.Chain.DocumentHeight));
            g.InvertColor();
            g.Draw(graphics);
            g.Dispose();
            graphics.FillRectangle(Colorf.Black, new Rectanglef(100, 0, 1, this.Chain.DocumentHeight));
        }
    }
}
