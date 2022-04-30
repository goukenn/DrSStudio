using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
namespace IGK.GS.Configuration
{
    /// <summary>
    /// represent a configuration mode
    /// </summary>
    public class GSConfigs
    {
        static Dictionary<string, IGSConfigCommand> m_commands;
        static GSConfigs() {
            m_commands = new Dictionary<string, IGSConfigCommand>();
            //loading command
            LoadingCommands();
            AppDomain.CurrentDomain.AssemblyLoad += CurrentDomain_AssemblyLoad;
        }

        private static void LoadingCommands()
        {
  
            foreach (Assembly item in AppDomain.CurrentDomain.GetAssemblies ())
            {
                LoadAssembly(item);
              
            }
            
        }

        private static void LoadAssembly(Assembly item)
        {
            Type v_attribType = typeof(GSConfigCommandAttribute);
            GSConfigCommandAttribute h = null;
            foreach (Type t in item.GetTypes())
            {
                h = GSConfigCommandAttribute.GetCustomAttribute(
                    t,
                    v_attribType) as GSConfigCommandAttribute;
                if (h != null)
                {
                    if (!m_commands.ContainsKey(h.Name))
                    {
                        m_commands.Add(h.Name.ToLower(),
                            t.Assembly.CreateInstance(t.FullName) as IGSConfigCommand);
                    }
                }
            }
        }

        static void CurrentDomain_AssemblyLoad(object sender, AssemblyLoadEventArgs args)
        {
            LoadAssembly(args.LoadedAssembly);
        }
        internal static void ExecCmd(string cmd)
        {

            if (!string.IsNullOrEmpty(cmd) && m_commands.ContainsKey(cmd.ToLower()))
            {
                m_commands[cmd].DoAction();
            }
            else {
                GSLog.EWriteLine("not a valid command : "+cmd);
            }
        }
    }
}
