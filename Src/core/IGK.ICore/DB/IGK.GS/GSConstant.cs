using IGK.ICore;
using IGK.ICore.Codec;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.ICore.WinUI.Configuration;
using IGK.ICore.Settings;






﻿using IGK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IGK.GS.DataTable;

namespace IGK.GS
{
    public static class GSConstant
    {

        public const string VARCHAR = "VarChar";
        public const int VARCHAR_NAME_SIZE = 30;
        public const string ASM_DYNAMIC_NAME = "IGK.Data.DynamicDataTable";

        public const string TITLE_CHOOSEATASK = "Title.ChooseATask";
       

        //system table
        public const string SYS_TABLE_USERS = GSSystemDataTables.Users;
        public const string SYS_TABLE_GROUPS =  "system:://groups";
        public const string SYS_TABLE_USER_GROUPS = "system:://usergroups";
        public const string SYS_TABLE_AUTH = "system:://authorisations";
        public const string SYS_TABLE_GROUP_AUTH = "system:://groupautorisation";

        public const string PREFIX_FORMAT = "{0}";//"TTS-{1}/{0}";
        public const string GS_VERSION = "3.1";
        public const string GS_RELEASE_DATE = "24.07.14";
        public const string GS_AUHTOR = "C.A.D. BONDJE DOUE";
        public const string GS_WEBSITE = "http://www.igkdev.com";


        public const string CL_ID = "clId";
        public const string CL_DATETIME = "clDateTime";
        public const string CL_NAME = "clName";
        public const string CL_REF = "clRef";
        public const string CL_FIRSTNAME = "clFirstName";
        public const string CL_LASTNAME = "clLastName";
        public const string CL_LEVEL = "clLevel";
        public const string CL_TITLE = "clTitle";
        public const string CL_LOGIN = "clLogin";


        public const string AC_GOTOPREVIOUSFRAME = "GoToPreviousFrame";
        public const string AC_GOTONEXTFRAME = "GoToNextFrame";

        public const string ICO_PRINT = "togo_ico_print";
        public const string ICO_PRINT_ALL = "togo_ico_print_all";
        public static readonly string ICO_PRINT_sINGLE = "togo_print";
        public static string APP_MUTEX_NAME = "TogoGS";
        public static string IMG_HELP = "btn_help";
        public static readonly Colorf good_Color = Colorf.FromFloat(0.8f, .9f, 0.3f);
        public static readonly Colorf bad_Color = Colorf.FromFloat(0.9f, .5f, 0.3f);
        public const string MODULE_DIR = "Modules";


        public const  string RES_SPASHSCREEN_IMG = "SpashScreen";
        public const  string DEFAULT_MAIN_THREAD_NAME = "GSSystemMainThread";
        public const  string  REGULAR_DATETIME_CONSTANT  = "hh:mm:ss dd-MM-yyyy";

        public const int TASK_WIDTH = 180;
        public const int TASK_HEIGHT = 180;

        public const int SPACE = 10;

        public const int BUTTON_WIDTH = 128;
        public const int BUTTON_HEIGHT = 128;
        public const string AC_SHOW_OPTION_AND_SETTING = "Tools.ShowOptionAndSetting";

        public const string BTN_IMG_DROP = "btn_drop";
        public const string BTN_IMG_ADDNEW = "btn_addNew";
        public const string BTN_IMG_ADDNEWUSER = "btn_addUser";
        public const string BTN_IMG_ADDNEWCLIENT = "btn_addclient";
        public const string BTN_IMG_WEB = "btn_web";
        public const string BTN_IMG_SEARCH = "btn_search";
        public const string BTN_IMG_PRINT = "btn_print";
        public const string BTN_IMG_SEARCH_ALL = "btn_search_all";
        public const string BTN_IMG_LIST = "btn_action_list";
        public const string BTN_IMG_MLIST = "btn_action_mlist";
        public const string BTN_IMG_NEWTASK = "btn_newtask";
        public const string BTN_IMG_TASK = "btn_task";
        public const string BTN_IMG_EDIT = "btn_Edit";
        public const string DATA_DEFINITON_TAG = "DataDefinition";
        
        public const string AUTH_ADMINISTRATION = "Administration";
        public const string CL_TABLENAME = "clTableName";
        public const string BTN_IMG_PREVIOUS = "btn_previous";
        public const string NULL = "NULL";
        public const string DATA_SCHEMAS_TAG = "data-schemas";
        
    }
}
