

using IGK.ICore.WinUI;

namespace IGK.ICore
{
    /// <summary>
    /// represent utility function used to initialize project
    /// </summary>
    public static class CoreWorkingProjectUtils
    {
        public static ICoreWorkingSurface CreateAndInit(CoreWorkingProjectTemplateAttribute template) {
            //create a new surface 
            ICoreWorkingSurface s = template.TargetSurfaceType.Assembly.CreateInstance(template.TargetSurfaceType.FullName) as ICoreWorkingSurface;

            if ((s != null))
            {
                s.SetParam(template.GetInitializationParams());     
            }
            return s;
        }
    }
}
