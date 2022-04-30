using IGK.ICore;
using IGK.ICore.Codec;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.ICore.WinUI.Configuration;
using IGK.ICore.Settings;








using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using IGK.ICore.Actions;

namespace IGK.GS.Settings
{
    [GSSettingAttribute(Name = "GS.GSGlobal.Setting", Index = 0x80, ImageKey = "Img_gsApp")]
    public class GSSettings : CoreSettingBase
    {
        private static GSSettings sm_instance;

        [CoreSettingDefaultValue("server={0};user id={1}; password={2}; database={3}; pooling=false")]
        public string ConnectionString
        {
            get
            {
                return ((string)(this["ConnectionString"].Value));
            }
        }
        private GSSettings()
        {
            this.Add("CurrentLang", "fr", null);
            this.Add("ConnectionString", "server={0};user id={1}; password={2}; database={3}; pooling=false", null);
            this.Add("ShowTip", true, null);
        }

        protected override void InitDefaultProperty(System.Reflection.PropertyInfo prInfo, CoreSettingDefaultValueAttribute attrib)
        {
            base.InitDefaultProperty(prInfo, attrib);

        }
        public static GSSettings Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static GSSettings()
        {
            sm_instance = new GSSettings();

        }
        public static bool ShowTip
        {
            get
            {
                return (bool)sm_instance.GetValue("ShowTip", true);
            }
        }



        public override ICoreControl GetConfigControl()
        {
            return null;
        }

        public override enuParamConfigType GetConfigType()
        {
            return enuParamConfigType.ParameterConfig;
        }
        public override ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
        {
            parameters = base.GetParameters(parameters);
            var g = parameters.AddGroup("ServerInfo");
            Type t = GetType();
            g.AddItem(t.GetProperty("AdapterName"), "lb.adapterName");
            g.AddItem(t.GetProperty("Server"), "lb.server");
            g.AddItem(t.GetProperty("DataBase"), "lb.database");
            g.AddItem(t.GetProperty("Login"), "lb.login");
            var i = g.AddItem(t.GetProperty("Password"), enuParameterType.Password);
            i.CaptionKey = "lb.pwd";

            g.AddActions (new ConnectToServer(this));

            g = parameters.AddGroup("Apps");
            i = g.AddItem(t.GetProperty("Prefix"), enuParameterType.Text);           
            i.CaptionKey =  "lb.prefix";
            return parameters;
        }

        public string Password { get { return (string)this["Pwd"].Value; } set { this["Pwd"].Value = value; } }
        [CoreSettingDefaultValue("gsdatabase")]
        public string DataBase { 
            get { 
                return (string)this["DBName"].Value; 
            } 
            set { this["DBName"].Value = value; } }
        public string Server { get { return (string)this["Server"].Value; } set { this["Server"].Value = value; } }
        public string Login { get { return (string)this["Login"].Value; } set { this["Login"].Value = value; } }
        [CoreSettingDefaultValue("MySQL")]
        public string AdapterName { get { return (string)this["AdapterName"].Value; } set { this["AdapterName"].Value = value; } }
        public string Prefix { get { 
            return GSConfiguration.GetValue("GS/ApplicationData.Prefix"); } 
            set { 
                GSConfiguration.SetVAlue("GS/ApplicationData.Prefix", value); } }        

        protected override void OnSettingChanged(CoreParameterChangedEventArgs e)
        {
            base.OnSettingChanged(e);
        }
        protected override void OnSettingLoaded(EventArgs e)
        {
            base.OnSettingLoaded(e);
        }
        protected override void OnSettingChanged(CoreSettingChangedEventArgs e)
        {
            base.OnSettingChanged(e);
        }


        class ConnectToServer : CoreParameterActionBase 
        {
            private GSSettings gSSettings;
            class ConnectToServerAction : CoreActionBase
            {
                private ConnectToServer connectToServer;

                public ConnectToServerAction(ConnectToServer connectToServer)
                {
                    this.connectToServer = connectToServer;
                }
                protected override bool PerformAction()
                {
                    var s =   GSSystem.CreateAdapter();
                    if (s == null)
                    {
                        CoreMessageBox.Show("e.dataAdapterCreationFailed".R());
                        return false;
                    }
                    else {
                        GSSystem.Instance.DataAdapter = s;
                        var u = GSSystem.User;
                        if (u !=null){
                            var login = u.GetValue<string>("clLogin"); 
                            var pwd =   u.GetValue<string>("clPwd"); 
                            IGSDataQueryResult r = GSDB.SelectAll(IGK.GS.DataTable.GSSystemDataTables.Users, new Dictionary<string,object>(){
                {"clLogin",login },
                {"clPwd", pwd },
            });
                            if (r.RowCount == 1)
                            {
                                bool t = GSSystem.iConnect(r.GetRowAt(0));
                            }
                            else 
                             CoreMessageBox.Show("e.connexionfailed".R(), "title.error".R());
                         
                        }
                    }
                    return true;
                }
            }
            public ConnectToServer(GSSettings gSSettings):base()
            {
                this.gSSettings = gSSettings;
                this.Name = "ConnectToServer";
                this.CaptionKey = "btn.ConnectToServer";
                this.Action = new ConnectToServerAction(this);
            }
            public override ICoreDialogToolRenderer Host
            {
                get
                {
                    return base.Host;
                }
            }
        }
    }
}
