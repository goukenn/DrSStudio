using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.WinUI
{
    /// <summary>
    /// represent the core project solution base
    /// </summary>
    public abstract class CoreWorkingProjectSolutionBase : ICoreWorkingProjectSolution
    {
        public string Name { get; set; }
        public string ImageKey { get; set; }

        /// <summary>
        /// get or set the solution entry filename
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// get or set the default surface type
        /// </summary>
        public virtual Type DefaultSurfaceType { get; set; }

        public ICoreWorkingProjectSolutionItemCollections Items => throw new NotImplementedException();

        public string Id => throw new NotImplementedException();

        /// <summary>
        /// close this current surface
        /// </summary>
        public virtual void Close()
        {
        }

        public ICoreSaveAsInfo GetSolutionSaveAsInfo()
        {
            throw new NotImplementedException();
        }

        public abstract IEnumerable GetSolutionToolActions();


        public virtual void Open(ICoreSystemWorkbench coreWorkbench, ICoreWorkingProjectSolutionItem item)
        {
         
        }

        public virtual void Save()
        {
            
        }

        public virtual void SaveAs(string p)
        {
        }
    }
}
