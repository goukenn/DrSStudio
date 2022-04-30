using IGK.DrSStudio.Android;
using IGK.DrSStudio.Android.AndroidThemeBuilder.WinUI;
using IGK.ICore;


[assembly: CoreWorkingProjectItemTemplate(
    AndroidConstant.PROJECT_NAME + ".Theme",
    TargetSurfaceType = typeof(AndroidThemeFileEditorSurface),
    Params= "Parent: Theme; FileName: androidtheme; Platform: android-28")]

