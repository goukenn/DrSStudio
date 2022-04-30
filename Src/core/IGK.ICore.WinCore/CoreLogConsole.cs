using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore
{
    public static class CoreLogConsole
    {
        static ConsoleColor sm_info;
        static ConsoleColor sm_warning;
        static ConsoleColor sm_error;
        static ConsoleColor sm_message;

        static CoreLogConsole() {
            sm_info = ConsoleColor.Cyan;
            sm_error =  ConsoleColor.Red;
            sm_warning = ConsoleColor.Yellow ;
            sm_message = Console.ForegroundColor;
        }

        public static void WriteInfo(string msg, params object[] dparams) {

           var bck =   Console.ForegroundColor;
            Console.ForegroundColor = sm_info;
            Console.WriteLine (msg, dparams);
            Console.ForegroundColor = bck;
        }
        public static void WriteError(string msg, params object[] dparams)
        {

            var bck = Console.ForegroundColor;
            Console.ForegroundColor = sm_error;
            Console.WriteLine(msg, dparams);
            Console.ForegroundColor = bck;
        }
        public static void WriteWarning(string msg, params object[] dparams)
        {

            var bck = Console.ForegroundColor;
            Console.ForegroundColor = sm_warning;
            Console.WriteLine(msg, dparams);
            Console.ForegroundColor = bck;
        }

        public static void WriteMessage(string msg, params object[] dparams)
        {

            var bck = Console.ForegroundColor;
            Console.ForegroundColor = sm_message;
            Console.WriteLine(msg, dparams);
            Console.ForegroundColor = bck;
        }
    }
}
