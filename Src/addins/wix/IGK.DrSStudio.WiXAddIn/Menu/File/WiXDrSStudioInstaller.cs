using IGK.ICore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IGK.DrSStudio.WiXAddIn.Menu.File
{
    sealed class WiXDrSStudioInstaller
    {
        internal bool Generate(string filename)
        {
            string v_msifile = filename;
            bool v_err = false;
            string v_err_str = string.Empty;


            string v_candle = WiXUtils.CANDLE;
            string v_light = WiXUtils.Light;

            string outfile = "out." + WiXConstant.EXTENSION;
            string obj = "out." + WiXConstant.WIXOBJEXTENSION;
            bool b = CreateDocument(v_msifile, outfile);

            //string lib = Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.ProgramFilesX86 ), "Windows Installer XML v3.5\\bin\\");
            ProcessStartInfo info = new ProcessStartInfo();
            info.WindowStyle = ProcessWindowStyle.Hidden;
            info.CreateNoWindow = false;
            info.FileName = v_candle;
            info.Arguments = "-v " + outfile;
            info.RedirectStandardOutput = true;
            info.RedirectStandardError = true;
            info.UseShellExecute = false;
            Process p = Process.Start(info);
            p.WaitForExit();
            if (p.ExitCode == 0)
            {
                info = new ProcessStartInfo();
                info.WindowStyle = ProcessWindowStyle.Normal;
                info.CreateNoWindow = false;
                info.FileName = v_light;
                info.Arguments = "-v " + WiXUtils.getCommandLine("\"" + obj + "\" -out \"" + v_msifile + "\"");
                info.RedirectStandardOutput = false;
                info.RedirectStandardError = false;
                info.UseShellExecute = true;
                Process h = new Process();
                h.StartInfo = info;
                h.Start();
                h.WaitForExit();
                if (h.ExitCode != 0)
                {
                    try
                    {
                        v_err_str = "Error light.exe " + h.ExitCode;
                        StreamReader r = new StreamReader(h.StandardError.BaseStream);
                        v_err_str += r.ReadToEnd();
                        r.Close();
                        r = new StreamReader(h.StandardOutput.BaseStream);
                        v_err_str += r.ReadToEnd();
                    }
                    catch (Exception Exception)
                    {
                        v_err_str += "\n" + Exception.Message;
                    }
                    v_err = true;
                }
            }
            else
            {
                try
                {
                    v_err_str = "Error candle.exe " + p.ExitCode + "\n";
                    StreamReader r = new StreamReader(p.StandardError.BaseStream);
                    v_err_str = r.ReadToEnd();
                    r.Close();
                    r = new StreamReader(p.StandardOutput.BaseStream);
                    v_err_str += r.ReadToEnd();
                }
                catch
                {
                }
                v_err = true;
            }
            System.IO.File.Delete(outfile);
            if (v_err)
            {
#if DEBUG
                MessageBox.Show(v_err_str, "title.Error".R());
#endif
                return false;
            }
            else
            {
#if DEBUG
                MessageBox.Show("wix.drsstudio.msigenerated".R(), "title.App".R());
#endif
            }
            return true;
        }

        private bool CreateDocument(string v_outfile, string outfile)
        {
            WiXProductDocument d = new WiXProductDocument();
            WiXFeatureEntry fet = d.GetFeature(0);
            d.Package.InstallerVersion = 305;
            d.Manufacturer = "IGKDEV";
            d.Name = "IGKDEV-DRSStudio " + CoreConstant.VERSION;
            //d["Title"] = "";
            string dir = Application.StartupPath;
            WiXDirectory tdir = d.Features.GetElementById("TARGETDIR") as WiXDirectory;
            WiXDirectory xdir = null;
            xdir = tdir.AddDir("ProgramFilesFolder", "ProgFile")
                .AddDir("ManufacturerDir", "IGKDEV")
                .AddDir("AppDir", string.Format("{0} {1}", "DrSStudio", CoreConstant.VERSION));
            //build targetDir
            // BuildDir(xdir, dir, dir, @"(\.(vshost.exe|config|manifest|application)$)");
            WiXDirectoryComponent component = null;
            component = new WiXDirectoryComponent();
            component.Guid = Guid.NewGuid().ToString();

            //
            //- set permissions to the application folder permissions
            //
            WiXCreateFolder v_createFolder = new WiXCreateFolder();
            v_createFolder.Id = null;
            v_createFolder.Children.Add(new WiXPermission()
            {
                Id = null,
                User = "Everyone",
                GenericAll = WiXYesOrNoValue.Yes
            });
            component.Children.Add(v_createFolder);

            if (component.Children.Count > 0)
            {
                xdir.Children.Add(component);
                d.FeatureEntry.Add(component);
            }

            WiXUtility.LoadDir(xdir, component, dir, "\\.(exe|dll|png|ico|txt|rtf|html|jpg|js|css|php)$", null,
                (s) =>
                {
                    if (s.ToLower().EndsWith("vhost.exe"))
                        return false;
                    return true;
                });

            //add application shortcut menu directory
            xdir = tdir.AddDir(WiXDirectoryName.PROGRAMMENUFOLDER, null).AddDir("PROGMenuIGKDEV", "IGKDEV");
            component = new WiXDirectoryComponent()
            {
                Guid = Guid.NewGuid().ToString()
            };
            xdir.Children.Add(component);


            component.Children.Add(new WiXShortCut()
            {
                Id = "DrSStudioShortCut",
                Name = "DrSStudio",
                Description = "DrSStudio Shortcut",
                Target = "[AppDir]\\drs.exe",
                WorkingDirectory = "AppDir"
            });
            component.Children.Add(new WiXRemoveFolder()
            {
                Id = "PROGMenuIGKDEV",
                On = enuWiXRemoveFolderOn.uninstall
            });
            component.Children.Add(new WiXRegistryValue()
            {
                Key = "Software\\IGKDEV\\DrSStudio",
                Name = "installed",
                Value = "1",
                Type = "integer",
                Id = null
            });
            xdir.GetDocument().GetFeature(0).Add(component);
            //add icon for add remove program
            string v_file = "appIco.ico";
            if (System.IO.File.Exists(v_file))
            {
                d.Children.Add(new WiXIcon()
                {
                    Id = "appIco",
                    SourceFile = "appIco.ico"
                });
                d.Children.Add(new WiXProperty()
                {
                    Id = WiXPropertyName.WIXUI_ARPPRODUCTICON,
                    Value = "appIco"
                });
            }
            WiXUI v_ui = new WiXUI();
            v_ui.Children.Add(new WiXUIRef()
            {
                Id = WiXUIDialogName.WiXUI_INSTALLDIR
            });
            //Auto start application
            v_ui.Children.Add(new WiXPublish()
            {
                Dialog = "ExitDialog",
                Control = "Finish",
                Event = "DoAction",
                Value = "LaunchApplication",
                Condition = new WiXStringElement("WIXUI_EXITDIALOGOPTIONALCHECKBOX = 1 and NOT Installed")
            });
            d.Features.Add(new WiXCustomAction()
            {
                Id = "LaunchApplication"
            });
            d.Features.Add(v_ui);
            //for start app on installation complete
            d.Features.Add(new WiXProperty()
            {
                Id = "WIXUI_EXITDIALOGOPTIONALCHECKBOX",
                Value = "1"
            });
            d.Features.Add(new WiXProperty()
            {
                Id = "WIXUI_EXITDIALOGOPTIONALCHECKBOXTEXT",
                Value = "start DrSStudio"
            });
            d.Features.Add(new WiXProperty()
            {
                Id = "WixShellExecTarget",
                Value = "[AppDir]\\drs.exe"
            });
            //for directory modification
            d.Features.Add(new WiXProperty()
            {
                Id = WiXPropertyName.WIXUI_INSTALLDIR,
                Value = "AppDir"
            });
            //for licences files
            string v_license = Path.Combine(Application.StartupPath, "license.txt");
            if (System.IO.File.Exists(v_license))
            {
                d.Features.Add(new WiXVariable()
                {
                    Id = "WixUILicenseRtf",
                    Value = "license.txt"
                });
            }
            //---------------------------------replace bitmap
            //d.Features.Add(new WiXVariable()
            //{
            //    Id = "WixUIBannerBmp",
            //    Value = @"D:\temp\WiXBarner.png"
            //});
            //d.Features.Add(new WiXVariable()
            //{
            //    Id = "WixUIDialogBmp",
            //    Value = @"D:\temp\WiXDialogBg.png"
            //});

            //for framework requirement
            //<PropertyRef Id="NETFRAMEWORK45"/>
            //<Condition Message="This application requires .NET Framework 4.5. Please install the .NET Framework then run this installer again.">
            //    <![CDATA[Installed OR NETFRAMEWORK45]]>
            //</Condition>


            d.Features.Add(new WiXPropertyRef()
            {
                Id = WiXPropertyName.NETFRAMEWORK40FULL
            });
            d.Features.Add(new WiXPropertyRef()
            {
                Id = WiXPropertyName.NETFRAMEWORK45
            });
            d.Features.Add(new WiXCondition()
            {
                Message = "DrSStudio require .NET Framework 4.5 in order to work properly. Please install the .NET Framework then run this installer again.",
                Value = "Installed OR (" + WiXPropertyName.NETFRAMEWORK40FULL + " AND NETFRAMEWORK45 )"
            });
            //check for os 
            //<Condition Message="Win7 or 2008 R2 required"><![CDATA[Installed OR VersionNT >= 601]]></Condition>
            d.Features.Add(new WiXCondition()
            {
                Message = "DrSStudio : Operating System Requirement Microsoft Window 7 or greather.",
                Value = "Installed OR (VersionNT >= 601)"
            });
            var mm = Environment.OSVersion.Platform;
            //generate a wix document

            string v_err_str = string.Empty;
            StringBuilder sb = new StringBuilder();
            WiXWriter writer = WiXWriter.Create(sb);
            writer.Visit(d);
            writer.Flush();
            Environment.CurrentDirectory = dir;
            System.IO.File.WriteAllText(outfile, sb.ToString());
            return true;
        }  
    }
}
