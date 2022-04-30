
using IGK.ICore.WinCore;
using IGK.DrSStudio.WinUI;
using IGK.ICore;
using IGK.ICore.Actions;
using IGK.ICore.IO;
using IGK.ICore.Resources;
using IGK.ICore.Tools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace IGK.DrSStudio.CommandWindow.Tools
{
    [CoreTools("Tool.CommandWindows",
        ImageKey=CoreImageKeys.MENU_TOOL_EXEC_GKDS)]
    class CommandWindowTool : 
        CoreToolBase,
        ICoreCommandWindowContext 
    {
        private static CommandWindowTool sm_instance;
        private Dictionary<int, Process> m_lauchProcess;
        private CommandWindowTool()
        {
            m_lauchProcess = new Dictionary<int, Process>();
        }

        public static CommandWindowTool Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static CommandWindowTool()
        {
            sm_instance = new CommandWindowTool();
            CoreApplicationManager.ApplicationExit += _ApplicationExit;
        }

        static void _ApplicationExit(object sender, EventArgs e)
        {
            sm_instance.KillAllProcess();
        }
        public new CommandWindowHostControl HostedControl {
            get {
                return base.HostedControl as CommandWindowHostControl;
            }
        }
        protected override void GenerateHostedControl()
        {
            CommandWindowHostControl c = new CommandWindowHostControl();
            c.Tool = this;
            c.CaptionKey = "title.drsstudio.command.window";
            c.ToolDocument = CoreResources.GetDocument(ToolImageKey);
            base.HostedControl = c;

            c.CommandChanged += c_CommandChanged;
            CoreSystem.RegisterActionContext(
                CWConstant.COMMAND_WINDOW_EXEC_CONTEXT,
                this);
        }

        void c_CommandChanged(object sender, EventArgs e)
        {
            ExecCmd(this.HostedControl.Command);
        }

        public void ExecCmd(string cmd)
        {
            if (string.IsNullOrEmpty(cmd))
            {
                EWriteLine(string.Format("Notvalidcommand [{0}] ", cmd));
                return;
            }

            string[] args = cmd.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            ICoreAction c = CoreSystem.GetAction(args[0]);
            if (c != null)
            {
                if (args.Length > 0)
                {
                    ICoreParameterAction gs = c as ICoreParameterAction;
                    if (gs != null)
                    {
                        gs.Params = string.Join(" ", args, 1, args.Length - 1);
                    }
                }
                c.BindExecutionContext( CWConstant.COMMAND_WINDOW_EXEC_CONTEXT);
                c.DoAction();
                c.BindExecutionContext(null);
            }
            else
            {
                //execute command in the shell
                if (!ExecSysCommand(cmd))
                {
                    EWriteLine(string.Format("command [{0}] not found", cmd));
                }
                
            }
        }

        private bool ExecSysCommand(string cmd)
        {
            if (string.IsNullOrEmpty(cmd))
                return false;
            //check if command exist
            if (ExecuteProcess("where " + cmd.Split(' ')[0]))
            {
                
                System.Threading.Thread v_th = new System.Threading.Thread(() =>
                {
                    //IntPtr hp =  Marshal.StringToCoTaskMemAnsi(cmd);
                    //SysCmd(hp);
                    //Marshal.FreeCoTaskMem(hp);

                    Process.Start(cmd);
                    //ExecCmd(cmd);
                    //ExecuteProcess(cmd);
                });
                v_th.IsBackground = true;
                v_th.Start();
                return true;
            }
            return false;
        }

        public void EWriteLine(string p)
        {
            this.HostedControl.EWriteLine(p);
        }

        public void CWriteLine(string message)
        {
            this.HostedControl.CWriteLine(message);
        }

        void ICoreCommandWindowContext.EWriteLine(string message)
        {
            this.EWriteLine(message);
        }

        public void IWriteLine(string message)
        {
            this.HostedControl.IWriteLine(message);
        }

        public void WriteLine(string message, Colorf color)
        {
            this.HostedControl.IWriteLine(message, color);
        }


        //note: retirer le stack imbalance CallingConvention.Cdecl
        [DllImport("msvcrt.dll", EntryPoint = "system", ExactSpelling = true,
            CallingConvention = CallingConvention.Cdecl)]
        extern static bool SysCmd(IntPtr cmd);

        public bool ExecuteProcess(string command)
        {

            ///TODO: EXECUTE PROCESS IN ANOTHER THREAD 

            string v_c = command.Split(' ')[0];
            if (!Regex.IsMatch (v_c, "([^<>?=]+)"))
                return false ;
            ProcessStartInfo v_startInfo = new ProcessStartInfo();
            v_startInfo.UseShellExecute = false ;//execute in parallèle context
          
            //v_startInfo.FileName = v_c;
            v_startInfo.FileName = "cmd";
            v_startInfo.RedirectStandardInput = true ;
            v_startInfo.RedirectStandardOutput = false ;
            v_startInfo.WindowStyle = ProcessWindowStyle.Normal;
            v_startInfo.CreateNoWindow = true ;
            
            //v_startInfo.Arguments = command.Substring(v_c.Length).Trim ();
            try
            {
                Process p = Process.Start(v_startInfo);
                this.RegProcess(p);
            
                string pd = @"
@echo off
rem disable echo
rem condition testing

set v_cmd=%1
set V_ARGS=
set V_FORWHERE=0
shift

:pfunc
if not %v_cmd%==where goto pefunc
set v_cmd=%1
set V_FORWHERE=1
shift
goto pfunc

:pefunc

if not %V_FORWHERE%==1 goto args
where %v_cmd%
if %ERRORLEVEL%==0 (exit 0) else (exit -1)


:args
if ""%1""=="""" goto exec
set V_ARGS=%V_ARGS% %1
shift
goto args

:exec
rem check for data
where %v_cmd%

if %ERRORLEVEL%==0 %v_cmd% %V_ARGS% 

if not %ERRORLEVEL% ==0 (exit -1) else (exit 0)

:execkill
exit -1";
                string ff = PathUtils.GetTempFileWithExtension("bat");                
                File.WriteAllText(ff, pd);
                
                p.StandardInput.WriteLine("\"" + ff + "\" "+command);
                p.WaitForExit(5000);
                UnRegProcess(p);
                File.Delete(ff);
                return p.ExitCode == 0;
            }
            catch(Exception ex)
            {
                CoreLog.WriteLine(ex.Message);
            }

            return false;
        }

        public void KillAllProcess()
        {
            foreach (KeyValuePair<int, Process > item in this.m_lauchProcess )
            {
                try
                {
                    item.Value.Kill();
                }
                catch { 
                }
            }
        }
        private void UnRegProcess(Process p)
        {
            if (this.m_lauchProcess.ContainsKey(p.Id))
                this.m_lauchProcess.Remove(p.Id);
        }

        private void RegProcess(Process p)
        {
            if (!this.m_lauchProcess.ContainsKey(p.Id))
                this.m_lauchProcess.Add (p.Id, p);
        }

    }
}
