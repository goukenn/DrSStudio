

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreConstant.cs
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
file:CoreConstant.cs
*/

﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
namespace IGK.ICore
{
    public static partial class CoreConstant
    {
        public const string FONT_SIZES = "6;7;8;9;10;11;12;13;14;15;18;20;22;25;30;40;50;60;70;100";
        public static readonly string DebugTempFolder = Path.Combine(Path.GetDirectoryName(
            Assembly.GetEntryAssembly().Location), "temp");
        #region "Message Keys"
        public const string MSG_DONE = "msg.done";//message key

        #endregion
        public const string LAYEREDDOCUEMENT = "LayerDocument";//Layer document
        public const string LAYER = "Layer"; //Layer tag
        public const string RS_ENDEXTENSION = "_gkds";//resource end extension


        //Constant for mecanism creation
        internal const int ST_NONE = 0;
        internal const int ST_CREATING = 1;
        internal const int ST_EDITING = 2;

        //constant for company creation 
        public const string COMPANY = "IGKDEV";
        public const string DRAWING2D_NAMESPACE = WEBSITE +"/drawing2d";
        public const string MSBOX = "MSBOX";//messagebox default string constant
        public const string MUTEX_CONTEXT = "DRS";//name for mutext exclusion
        public const string CHANNEL_NAME = "CoreSystem";//consystem chanel name
        public const int APP_CHANNEL_PORT = 4083;//channel for interop communication
        public const String SYSTEM_APPINFO = "This is a gkds ICore file " + VERSION;       
        public const String AUTHOR = "Charles A.D. BONDJE DOUE";
        public const string BASEVERSION = "9.7"; //base library version
        public const String ASSEMBLYVERSION = BASEVERSION +".0.2131";
        public const String ASSEMBLYFILEVERSION = ASSEMBLYVERSION;
        public const String ASSEMBLYRELEASEDATE = "26.08.14";
        public const String SURFACE_CHANGED_CHAR = "~";

        public const string LIB_NAME = "IGKDEV ICore";




        public const string DRAWING2D_PROJECT  = "GKDSDrawing2D";
		public const string RES_LIB = "CoreResources";
#if DEBUG 
        //debug version
        public const String VERSION = BASEVERSION + ".d";
#else
        //release version
        public const String VERSION = BASEVERSION +".r";
#endif
#if DEMO
        public const bool DEMO_VERSION = true;           
#else
        public const bool DEMO_VERSION = false;           
#endif
        public const String DEFAULT_NAMESPACE = WEBSITE +"/drawing2d";
        public const String DEFAULTFILEEXTENTION = ".gkds";
        public const String DEFAULTFILEEXTENTION2 = ".igkds";
        public const String DEFAULT_STROKEBRUSH = "Type:Solid;Colors:#0000;";
        public const String DEFAULT_FILLBRUSH = "Type:Solid;Colors:#FFFF;";
        public const String DEFAULT_GROUPNAME = "Basics";
        public const String DEFAULT_CONFIGURATION_GROUP = "DEFINITION";                    
        public const String DEFAULT_ACTION_CATEGORY = "Default";
        public const String DEFAULT_FONT_NAME = "consolas";//default font name
        public const String DEFAULT_FONT_DEFINITION = "FontName:consolas; size:8pt";

        public const String CONTACT = "igkdev@hotmail.com";
        public const String WEBSITE = "http://www.igkdev.com";
        public const String PRODUCT = "igkdev ICore";
        public const String YEAR_RANGE = "2008-2016";
        public const String COPYRIGHT = "igkdev \xA9 " + YEAR_RANGE;
        public const String SETTING_FILE = "app.setting.conf";
        public const String SETTING_GENERAL_CAT = "General";
        public const String SKIN_FILE = "app.skin.xml";
   


        public const String EMPTY_FILE = "empty" + DEFAULTFILEEXTENTION;
        public const String VALUESEPARATOR = ";";
        public const String DECIMALSEPARATOR = ".";
     
        public const String DRAWING2D_ENVIRONMENT = "Drawing2D";
        public const String DRAWING2D_SURFACE_TYPE = "2DDrawingSurface";
        public const String METHOD_CREATESURFACE = "CreateSurface";
        public const String METHOD_CHECKADDING = "Check";
        public const String METHOD_INITASSEMBLY = "InitAssembly"; //init assembly method after it's loaded. for requirement
        public const String INSTANCE_PROPERTY = "Instance";
        public const string MAIN_THREAD_NAME = "IGK.ICore.MAINTHREAD";
        public const string STARTFORM_THREAD_NAME = "IGK.ICore.StartFormThread";

        //REGION FOlDERS
        public const String ADDIN_FOLDER = "AddIn";
        public const String ERR_SURFACE_MUSTCREATEADOCUMENT = "ERR.SURFACE.MUSTCREATEADOCUMENT";
        public const String ERR_SURFACE_MUSTCREATEADOCUMENTCOLLECTIONS = "ERR.SURFACE.MUSTCREATEADOCUMENTCOLLECTION";
        public const String ERR_DOCUMENT_MUSTCREATEALAYER = "ERR.DOCUMENT.MUSTCREATEALAYER";
        public const String ERR_SURFACE_MUSTCREATEAPROJECT = "ERR.SURFACE.MUSTCREATEAPROJECT";
        public const String ERR_ENCODER_ATTR_MUST_BE_SET = "ERR.ENCODERATTRMUSTBESET";
        public const String ERR_DECODER_ATTR_MUST_BE_SET = "ERR.DECODERATTRMUSTBESET";
        public const String ERR_LAYOUTMANAGER_STATUSREQUIRE = "ERR.LAYOUTMANAGER.STATUSREQUIRE";
        public const String ERR_UNABLE_TO_LOAD_SKIN = "ERR.UNABLE.TO_LOAD_SKIN";
        public const String ERR_FILE_NOT_OPENED = "ERR.FILE_NOT_OPENED";
        public const string ERR_MENUNOTREGISTERED_1 = "ERR.MENUNOTREGISTRATED_1";
        public const string ERR_ONSAVE = "ERROR.ONSAVE";
        public const int ERROR_CODE = 0x0001;
        public const int ERROR_DOCUMENTNOTVALIDE = ERROR_CODE + 1;
        public const int ERROR_SURFACENOTVALID = ERROR_CODE + 2;
        public const int ERROR_DOCUMENT_MUSTCREATEALAYER = ERROR_CODE + 3;
        public const int ERROR_SURFACE_MUSTCREATEAPROJECT = ERROR_CODE + 4;
        public const int ERROR_ENCODER_ATTR_MUST_BE_SET = ERROR_CODE + 5;
        public const int ERROR_DECODER_ATTR_MUST_BE_SET = ERROR_CODE + 6;
        //image menu keys constant
        //public const string MENU_CLEAR = "Menu_Clear";
        //public const string MENU_CLOSE = "Menu_Close";
        //public const string MENU_BRINGUP = "Menu_BringUp";
        //public const string MENU_BRINGDOWN = "Menu_BringDOWN";
        
        public const string MENU_FIND = "Menu_Find";
        public const string MENU_ADD = "Menu_ADD";
        public const string MENU_VIEW = "MENU_VIEW";
        public const string MENU_RULE = "Menu_Rule";
        public const string REG_SKINS = "Skins";
        public const string REG_ADDINS = "AddIns";
        public const string STARTUP_ENVIRONMENT = "StartUp";
        public const string STARTUP_SURFACE_NAME = "StartPage";
        public const string BTN_OK = "btn.ok.caption";
        public const string BTN_CANCEL = "btn.cancel.caption";
        public const string BTN_RESET = "btn.reset.caption";
        public const string BTN_APPLY = "btn.apply.caption";
        public const string BTN_BROWSE = "btn.browse.caption";
        public const string BTN_NEXT = "btn.next.caption";
        public const string BTN_DOC_RADIOBUTTON = "RADIO_BUTTON";
        public const string BTN_PREVIOUS = "btn.previous.Caption";
        public const string BTN_PREFERENCE = "btn.preference.caption";
        public const string BTN_SAVE = "btn.save.caption";
        public const string BTN_CLOSE = "btn.close.caption";
        public const string TSBTN_ADDORREMOVE_CAPTION = "btn.AddOrRemoveButton.caption";
        
        //IMAGES
        public const string IMG_PROGRESSBAR_BG = "BG_PROGRESSBAR";

        public const string IMG_BRING_DOWN = "img_bring_down";
        public const string IMG_BRING_UP = "img_bringDown";
        public const string IMG_BRINGTO_START = "img_bringStart";
        public const string IMG_BRINGTO_END = "img_bringEnd";
        public const string IMG_POLICE = "Police";


        public const string MENU_INSERT = "Insert";

        public const string TAG_PROJECT = "Project";
        public const string TAG_DOCUMENTS = "Documents";
        public const string TAG_FILENAME = "FileName";
        public const string TAG_SURFACETYPE = "SurfaceType";
        public const string TAG_RESOURCES = "Resources";
        public const String TAG_GKDS_HEADER = "gkds";
        public const string LB_CAPTION = "lb.{0}.caption";
        public const string LB_CONFIGURE_KEY = "lb.configurekey.caption";
        public const string PARAM_DEFINITION = "Definition";
        public const string PARAM_ID = "ID";
        public const string PARAM_GROUP_DESCRIPTION = "Description";
        public const string PARAM_GROUP_DEFINITION = "Definition";
        public const string PARAM_GROUP_DEFAULT = "Default";
        public const string COLLECTION_TOSTRING = "[Count:{0}]";
        public const string FILTER_SMOOTHING = "BlurAndOpacity";
        public const string FILTER_INVERTCOLOR = "InvertColor";
        public const string FILTER_NOISE = "NoiseFilter";
        public const string SPLASHSCREEN = "SplashScreen";
        public const string WARN_TITLE = "WARN.TITLE";
        public const string WARN_YOUCANCELSAVE = "WARN.YOUCANCELSAVING";
        public const string LANGRESOURCES = "{0}/Lang.{1}.resources";//language resource file model
        public const string OBJECTRESOURCES = "object.resources";
        /*codec category */
        public  const string CAT_PICTURE = "PICTURE";
        public  const string CAT_VIDEO = "VIDEO";
        public  const string CAT_AUDIO = "AUDIO";
        public  const string CAT_TEXT = "TEXT";
        public  const string CAT_ANIMATION = "ANIMATION";
        public const string CAT_FILE= "FILE";
        public const string ENUMVALUE = "enum.{0}";

        public const string DRS_PROJECT_SELECTOR = "ProjectSelector";
        //TAG
        public const string SETTING_TAG = "Settings";
        //GUI CONSTROL CONSTANT
        public const string GUI_APP_SETTINGCONTROL = "ApplicationSettingSurface";
        //MENU INDEX
        public const int SAVE_MENU_INDEX = 0x050;
        public const int PRINT_MENU_INDEX = 0x100;
        //SETTIN NAME CONSTANT
        public const string APP_ROOT_SETTING = "Core";
        public const string APP_SETTING = APP_ROOT_SETTING+".GeneralSetting";
        public const string APP_USER_INFO_SETTING = APP_ROOT_SETTING + ".UserInfo";

        public const string CAT_PROP_GRAPHIC = "GraphicProperty";
        public const string DUMMY_GROUP = "DUMMY_GROUP";
        public const string GROUP_EDITOR = "EDITOR";
        public const string TITLE_EXCEPITON_KEY = "Title.Exception";
        
        
        public const int MECANISM_PRIORITY = 0x0100;
        public const int MENU_NEW_FILE_INDEX = 0X0100;


        static CoreConstant() {
#if DEBUG
            DebugTempFolder = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "temp");
            Directory.CreateDirectory(CoreConstant.DebugTempFolder);
#endif
            UpdateBuild();
        }
        private static void UpdateBuild()
        {
            string f = "build.bconf";
            int i = 0;
            if (File.Exists(f))
            {
                string g = File.ReadAllText(f);
                if (int.TryParse(g.Trim(), out i))
                {
                    i++;
                }
            }
            File.WriteAllText(f, i.ToString());
        }


        public const string MENUI_OPEN = "FILE.OPEN";
        public const string MENUI_SAVE = "FILE.SAVE";
        public const string MENUI_SAVEAS = "FILE.SAVEAS";
        public const string MENUI_WINDOW = "Windows";
        public const string IGK_PREFIX = "IGK";

        public const string REGEX_RES_TARGET = "(@((?<cat>(.)+)/){0,1}){0,1}#(?<id>(.)+)";
        public const string CHECK_ASSEMBLY_DOMAINNAME = "CheckAssemblyDomain";
        public const string DISPATCHER_TYPE_RESOLV_1 = "IGK.ICore.WinUI.Dispatch.Core{0}DispatcherEvent";
        public const string XML_SCHEMA_WEBSITE = WEBSITE +"/schemas";
        public const string GROUP_TEXT = "Text";
        public const string CTRL_MENU_SEPARATOR = "IGK.ICore.WinUI.MenuSeparator";
        public const string XMLELEMENT_CLASS_FORMAT = "IGK.ICore.Xml.CoreXml{0}Element";
        public const string XMLWEBELEMENT_CLASS_FORMAT = "IGK.ICore.Xml.CoreXmlWeb{0}Element";
        public const string IMG_DASH = "dash";
        public const string IMG_BTN_SAVE_BRUSH = "btn_save_brush";
        public const string IMG_BTN_TOGGLE_BRUSH = "btn_toogle_brush";
        public const string ZIP_READER_ASM = "drsZipReader";
        public const string ZIP_READER_TYPE = "IGK.DrSStudio.WinCoreZipReader";



        
        public const string GROUP_CAPTION_FORMAT = "lb.group.{0}";
        public const string LABEL_TEXT_FORMAT = "lb.{0}";
        public const string MENU_FORMAT = "Menu.{0}";
        public const string TEMP_FOLDER = "%startup%/temp";
        public const string RES_APP_ICON = "AppIcon";
        public const string PARAM_CURRENT_ENCODER = "sys://CurrentEncoder";

        public const string TRANSPARENT_COLOR_DEF = "Type: Solid; Colors:#0000;";
        public const string DEBUG_TAG = "ICore";
#if DEBUG 
        public const string DRS_SRC = @"D:\Dev\csharp\DRSStudio 9.0 Src\Src";
#else
        public const string DRS_SRC = @".\";
#endif



    }
}

