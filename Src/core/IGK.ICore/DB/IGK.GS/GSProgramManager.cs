using IGK.GS.Configuration;
using IGK.ICore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace IGK.GS
{
    public static class GSProgramManager
    {
        public static string DEFAULTACTION;
        /// <summary>
        /// running a console command. 
        /// </summary>
        public static void RunConsoleCommand()
        {
            if (!CoreUtils.IsInConsoleMode())
                return;
            Cls();
            GSLog.CWriteLine(GSSystemParams.WelcomeMessage);
            string cmd = string.Empty;
            bool quit = true;

#if DEBUG
            ///primary command
            GSSystem.ExecCmd(DEFAULTACTION);

            Console.WriteLine ("testing values");
            
#endif
            while (quit)
            {
                global::System.Console.Write(GSSystemParams.Prompt);
                cmd = global ::System.Console.ReadLine();

                
                switch (cmd)
                {
                    case "?":
                    case "help":
                        //show help 
                        GSLog.CWriteLine(" ---- help ---- ");
                        GSSystem.ExecCmd("actions.list");
                        break;
                    
                    case "exit":
                    case "quit":
                    case "q":
                        quit = false;
                        break;
                    case "clr":
                        Cls();
                        break;
                    default:
                        GSSystem.ExecCmd(cmd);
                        break;
                }
            }
        }
        public static void RunConfigurationCommand()
        {
            if (!CoreUtils.IsInConsoleMode())
                return;
            bool quit = true;
            string cmd = string.Empty;
            while (quit)
            {
                global::System.Console.Write(GSSystemParams.Prompt);
                cmd = global ::System.Console.ReadLine();
                switch (cmd)
                {
                    case "exit":
                    case "q":
                        quit = false;
                        break;
                    case "clr":
                        Cls();
                        break;
                    default:
                        GSConfigs.ExecCmd(cmd);
                        break;
                }
            }
        }

        private static void Cls()
        {
            IntPtr cld = Marshal.StringToCoTaskMemAnsi("cls");
            SysCmd(cld);
            Marshal.FreeCoTaskMem(cld);
        }
        //note: retirer le stack imbalance CallingConvention.Cdecl
        [DllImport("msvcrt.dll", EntryPoint = "system", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
        extern static bool SysCmd(IntPtr cmd);

    }
}
