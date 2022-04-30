using IGK.ICore;
using IGK.ICore.Codec;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DRSStudio.BalafonDesigner.Codec
{
    class BalafonViewDesignerEncoder : CoreEncoderBase
    {
        private BalafonViewDesignerEncoderVisitor visitor;

        public override bool Save(ICoreWorkingSurface surface, string filename, params ICoreWorkingDocument[] documents)
        {
            foreach (var item in documents)
            {
                if (item is BalafonViewDesignerDocument doc) {
                    this.SaveDocument(doc);
                }
            }
            return false;
        }

        private void SaveDocument(BalafonViewDesignerDocument doc)
        {
            string outDir = Path.Combine(doc.SolutionDir, BalafonViewDesignerConstants.PROJECTSTOREDIR);

            visitor = visitor ?? new BalafonViewDesignerEncoderVisitor(this);
            visitor.Visit(doc);

            File.WriteAllText(Path.Combine(doc.SolutionDir,
                BalafonViewDesignerConstants.VIEWSDIR,
                doc.Name + "phtml"),
                visitor.Output);



        }

      
    }
}
