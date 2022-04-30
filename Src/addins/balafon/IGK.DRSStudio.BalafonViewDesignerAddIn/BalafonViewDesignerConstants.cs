using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DRSStudio.BalafonDesigner
{
    /// <summary>
    /// store all constants
    /// </summary>
    internal sealed class BalafonViewDesignerConstants
    {
        internal const string SOLUTIONFILE = "app.json"; //solution file. is a json presentation
        internal const string PROJECTSTOREDIR = ".bapps"; //where to store balafon application app

        // Application solution directory Solution 
        /// <summary>
        /// 
        /// </summary>
        internal const string VIEWSDIR = "Views"; // app view dir 
        internal const string LIBDIR = "Lib"; // app lib dir 
        internal const string DATADIR = "Data"; // app data dir   
        internal const string ARTICLES = "Articles"; // app data dir   
        internal const string SCRIPTSDIR = "Scripts"; // app data dir   
        internal const string STYLESDIR = "Styles"; // app data dir   


        internal const string SURFACE_GUID = "{703041DF-F6C0-49EC-B422-B8CAE964AACB}";

        // single applicataion view define only one application for the system balafon
        // define constant IGK_SINGLE_APP_VIEW
        // must setup the default controller will be the only application for the balafon application
    }
}
