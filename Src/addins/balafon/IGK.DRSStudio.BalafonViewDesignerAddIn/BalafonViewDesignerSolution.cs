using IGK.DRSStudio.BalafonDesigner.WinUI;
using IGK.ICore;
using IGK.ICore.IO;
using IGK.ICore.WinUI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DRSStudio.BalafonDesigner
{
    /// <summary>
    /// represent a solution for balafon designer
    /// </summary>
    public class BalafonViewDesignerSolution :
        CoreWorkingProjectSolutionBase,
        ICoreWorkingProjectSolution
    {
        public string OutFolder { get; private set; }

        public override Type DefaultSurfaceType { get => base.DefaultSurfaceType; set => base.DefaultSurfaceType = value; }

        
        internal static BalafonViewDesignerSolution CreateSolution(string selectedPath)
        {
            BalafonViewDesignerSolution s = new BalafonViewDesignerSolution
            {
                OutFolder = selectedPath
            };
            // initialize the default surface 
            s.DefaultSurfaceType = typeof(BalafonViewDesignerSurface);
            s.Initiliaze();
            return s;
        }

        public override IEnumerable GetSolutionToolActions()
        {
            return null;
        }
        private void Initiliaze() {

            string _storeDir = Path.Combine(this.OutFolder, BalafonViewDesignerConstants.PROJECTSTOREDIR);
            if (!Directory.Exists(_storeDir) && !PathUtils.CreateDir(_storeDir))
                throw new BalafonDesignerException("Failed to initialize the storage directory".R());

            string f = Path.Combine(_storeDir, BalafonViewDesignerConstants.SOLUTIONFILE);

            if (File.Exists(f)) {

                    
            }
        }
    }
}
