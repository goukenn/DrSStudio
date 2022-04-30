using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Android.Xamarin
{
    public static  class XamarinConstant
    {
        public const string PROJETC_REF_TAG = "ProjectReference";
        public const string PROJECT_TAG = "Project";
        public const string PROJECT_PROPERTY_GROUP_TAG = "PropertyGroup";
        public const string PROJECT_PROPERTY_ITEM_GROUP_TAG = "ItemGroup";
        public const string MENU_BUILD = "XamarinBuild";


        public const string PROJECT_TYPES_GUIDS = "{EFBA0AD7-5A72-4C68-AF49-83D382785DCF};{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}";
        public const string CSPROJ_EXT = ".csproj";
        public const string SETTING_GROUP_NAME = "Xamarin";
        public const string MSBUID_ANDROID = @"$(MSBuildExtensionsPath)\Novell\Novell.MonoDroid.CSharp.targets";
        public const string XAMARIN_TEMPLATE_RES = "xamarin_cs_{0}.txt";
        public const string XMLELEMENT_CLASS_FORMAT = "XamarinProject{0}Item";
        public const string PROJET_IMPORT_TAG = "Import";

        public const string PROJET_FOLDER_TAG = "Folder";
        public const string CORELIB_RES = "xamarin/resources/corelib.zip";
    }
}
