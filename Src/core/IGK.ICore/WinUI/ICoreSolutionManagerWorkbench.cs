using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.WinUI
{
    public interface ICoreSolutionManagerWorkbench : ICoreWorkbench 
    {
        /// <summary>
        /// get if the solution filename is already opened
        /// </summary>
        /// <param name="filename">filename</param>
        /// <returns></returns>
        bool IsSolutionOpened(string filename);

        /// <summary>
        /// get or set the solution
        /// </summary>
        ICoreWorkingProjectSolution Solution { get; set; }

        event EventHandler SolutionChanged;
    }
}
