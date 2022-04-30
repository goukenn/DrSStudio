


using IGK.ICore;
/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AndroidConstant.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:AndroidConstant.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.Android
{
    /// <summary>
    /// represent DrSStudio android constants
    /// </summary>
    public static class AndroidConstant
    {
        public const string MANIFEST_FILE = "AndroidManifest.xml";
        public const string ANDROID_XMLDECLARATION = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>";
        public const string ANDROID_NAMESPACE = "http://schemas.android.com/apk/res/android";       
        
        //android tag
        public const string RESOURCE_TAG = "resources";
        public const string STYLE_TAG = "style";
        public const string MANIFEST_TAG = "manifest";
        public const string ITEM_TAG = "item";
        public const string MENU_TAG = "menu";

        public const string PROJECT_NAME = "Android";
        public const string NAME_SPACE = CoreConstant.WEBSITE + "/drsstudio/android";
        public const string ENVIRONMENT = "AndroidEnvironment";
        public const string ENVIRONMENT_GUID = "{5DD24377-0EFF-4E4C-B782-68D41371E980}";
        public const int ANDROIDVEWINDEX = 0x200;
        public const string ANDROID_SLN_FILE_EXTENSION = "igkandroidsln";
        

        public const string AC_BUILDPROJECT = "Android.BuildProject";
        public const string AC_BUILDANDRUNPROJECT = "Android.BuildANDRunProject";
        public const string AC_BUILDEBUGPROJECT = "Android.DEBUGBuildProject";
        public const string AC_BUILDEBUGANDRUNPROJECT = "Android.DEBUGANDRUNBuildProject";
        public const string AC_REBOOTDEVICE = "Android.RebootDevice";

        public const string ANDROID_IMG_APP_ANDROID = "app_Android";
        public const string ANDROID_IMG_BINARYFOLDER = "android_img_folder";
        public const string ANDROID_IMG_APP_DEPLOY = "android_img_deploy";
        public const string ANDROID_IMG_PACKAGEFOLDER = "android_img_package";
        public const string ANDROID_IMG_JSCRIPTFILE = "android_img_jsfile";
        public const string ANDROID_IMG_SOLUTION =ANDROID_IMG_APP_ANDROID;// "img_android_solution";
        public const string ANDROID_IMG_RESFOLDER = ANDROID_IMG_BINARYFOLDER;
        public const string ANDROID_IMG_MANIFEST= "android_img_manifest";
        public const string ANDROID_IMG_FOLDER = "android_img_folder";
        public const string ANDROID_IMG_SDK_MANAGER = "android_img_sdkmanager";
        public const string ANDROID_IMG_ADV_MANAGER = "android_img_virtualdevicemanager";
        public const string ANDROID_IMG_DEVICE_KITKAT = "android_device_kitkat";

        public const string ANDROID_BROADCAST_ACTION_FILE = "broadcast_actions.txt";
        public const string ANDROID_CATEGORIES_FILE = "categories.txt";
        public const string ANDROID_ACTIVITY_ACTIONS_FILE = "activity_actions.txt";
        public const string ANDROID_FEATURES_FILE = "features.txt";
        public const string ANDROID_SERVICE_ACTIONS_FILE = "service_actions.txt";
        public const string ANDROID_WIDGETS_FILE = "widgets.txt";


        public const string ANDROID_ENTITIES = "Entities";
        public const string ANDROID_THEMES = "Themes";
        public const string ANDROID_DRAWABLES = "Drawables";
        public const string ANDROID_MENUS = "Menus";
        public const string ANDROID_STRINGS = "String";
        public const string ANDROID_ATTRIBUTES ="Attrs";
        public const string DECLARE_STYLELABLE_TAG = "declare-styleable";
        public const string ANDROID_DRAWING2D_GROUP_NAME = "Android";
        public const string ANDROID_DRAWING2D_NAMESPACE =  CoreConstant.WEBSITE + "/android2D";
        public const string ANDROID_DRAWING2D_NINEPATCHATTRIBUTE = "android:NinePath";
        public const string ANDROID_BULLET_MENU_ITEM = "android_bullet_menu_item";
        public const string ENVIRONMENT_THREAD = "Android_init_threadThread";

        public const string MSG_NOPLATFORMFOUND = "msg.android.notplatformfound";
        public const string ANDROID_SDK_THEME_FILE = "{0}/data/res/values/themes.xml";



        public const string TOOLS_ANDROID_PATH = "tools/android.bat";
        public const string STYLE_VALUE_REGEX = "(@((android|(.)+):)?(style/))?(?<name>(.)+)$";
        public const string ATTR_VALUE_REGEX = "^(((android|(.)+):)?)?(?<name>(.)+)$";


        public const string PLATFORM_SDK = "PlatformSDK";         
        public const string DEFAULT_INSTALL_FOLDER = "c:\\android\\";

    }
}

