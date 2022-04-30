using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.WinUI
{
    /// <summary>
    /// used to identify a project manager workbench
    /// </summary>
    public interface  ICoreProjectManagerWorkbench : ICoreWorkbench 
    {
        
        event EventHandler<CoreProjectOpenedEventArgs> ProjectOpened;

        bool OpenProject(string filename);

        bool IsProjectOpened(string filename);
    }
}
