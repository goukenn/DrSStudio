using IGK.DrSStudio.Android;
using IGK.DrSStudio.Android.AndroidMipmap;
using IGK.DrSStudio.Android.AndroidMipmap.WinUI;
using IGK.ICore;

[assembly: CoreWorkingProjectItemTemplate(
    AndroidConstant.PROJECT_NAME+".AndroidMimpap", 
    Group = "Android",
    ConfigType = typeof(AndroidMipmapConfig),
    TargetSurfaceType = typeof(AndroidMipmapSurface))] 