

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WiXProject.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
using IGK.ICore;
using IGK.ICore.Codec;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.DrSStudio;
using IGK.DrSStudio.Menu;
using IGK.DrSStudio.WinUI;
using IGK.DrSStudio.Drawing2D;
using IGK.DrSStudio.Drawing2D.WinUI;
using IGK.ICore.IO;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:WiXProject.cs
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace IGK.DrSStudio.WiXAddIn
{
    /// <summary>
    /// represent a wix project
    /// </summary>
    public class WiXProject : ICoreWorkingObject 
    {
        //WiXDirectory m_programFileDir;
        //WiXDirectory m_startMenuDir;
        //WiXDirectory m_desktoDir;
        private string m_Name;
        private string m_Manufacteur;
        private string m_Copyright;
        private Guid m_UpdrageCode;
        private Guid  m_Id;
        private bool m_UseGUI;
        private Icon m_Icon;
        private int m_InstallerVersion;
        private string m_StartMenuDir;
        private string m_ProgramFileDir;
        private string m_LicenseFile;
        private bool m_AutoStartApplication;
        private bool m_AutoStartApplicationChecked;
        private string m_AutoStartApplicationCheckedText;
        private string m_AutoStartApplicationPath;
        private WiXProjectExtensionCollection m_Extension;
        [Browsable (false )]
        [CoreXMLElement()]
        /// <summary>
        /// get or set the extension
        /// </summary>
        public WiXProjectExtensionCollection Extension
        {
            get { 
                return m_Extension; 
            }
        }
        [Category("AutoStart")]
        [Description("Chemin vers le fichier � lancer. exemple [APPDIR]mon.exe ou APPDIR est les sysbol par defaut identifant l'application")]
        [CoreXMLElement()]
        public string AutoStartApplicationPath
        {
            get { return m_AutoStartApplicationPath; }
            set
            {
                if (m_AutoStartApplicationPath != value)
                {
                    m_AutoStartApplicationPath = value;
                }
            }
        }
        [Category("AutoStart")]
        [Description("texte qui sera afficher lors du lancement quand on demandera")]
        [CoreXMLElement()]
        public string AutoStartApplicationCheckedText
        {
            get { return m_AutoStartApplicationCheckedText; }
            set
            {
                if (m_AutoStartApplicationCheckedText != value)
                {
                    m_AutoStartApplicationCheckedText = value;
                }
            }
        }
        [Category("AutoStart")]
        [Description("Cocher ou non la demande de lancement du fichier")]
        [CoreXMLElement()]
        public bool AutoStartApplicationChecked
        {
            get { return m_AutoStartApplicationChecked; }
            set
            {
                if (m_AutoStartApplicationChecked != value)
                {
                    m_AutoStartApplicationChecked = value;
                }
            }
        }
        [Category("AutoStart")]
        [Description("Autoriser ou non le demarrage d'une instance � la fin de l'intallation")]
        [CoreXMLElement()]
        public bool AutoStartApplication
        {
            get { return m_AutoStartApplication; }
            set
            {
                if (m_AutoStartApplication != value)
                {
                    m_AutoStartApplication = value;
                }
            }
        }
        [Category("Application")]
        [Editor(typeof(WiXSelectLicenceFileEditor), typeof (UITypeEditor ))]
        [CoreXMLElement()]
        /// <summary>
        /// la license
        /// </summary>
        public string LicenseFile
        {
            get { return m_LicenseFile; }
            set
            {   
                if (m_LicenseFile != value)
                {
                    m_LicenseFile = value;
                }
            }
        }
        [Category("Folder")]
        [CoreXMLElement()]
        public string ProgramFileDir
        {
            get { return m_ProgramFileDir; }
            set
            {
                if (m_ProgramFileDir != value)
                {
                    m_ProgramFileDir = value;
                }
            }
        }
        [Category("Folder")]
        [CoreXMLElement()]
        public string StartMenuDir
        {
            get { return m_StartMenuDir; }
            set
            {
                if (m_StartMenuDir != value)
                {
                    m_StartMenuDir = value;
                }
            }
        }

        [Category("Folder")]
        [CoreXMLElement()]
        public string DesktopMenuDir
        {
            get { return m_DesktopMenuDir; }
            set
            {
                if (m_DesktopMenuDir != value)
                {
                    m_DesktopMenuDir = value;
                }
            }
        }
      
        [Category("Application")]
        [CoreXMLElement()]
        public int InstallerVersion
        {
            get { return m_InstallerVersion; }
            set
            {
                if (m_InstallerVersion != value)
                {
                    m_InstallerVersion = value;
                }
            }
        }

        /// <summary>
        /// get or set the wix icon variable
        /// </summary>
        [Category("File")]
        [CoreXMLElement(false)]        
        public Icon Icon
        {
            get { return m_Icon; }
            set
            {
                if (m_Icon != value)
                {
                    m_Icon = value;
                }
            }
        }
        [Category("Configs")]
        [CoreXMLElement(true)]      
        public WiXProjectVariableCollections ProjectVariables
        {
            get {
                return this.m_WiXProjectVariables;
            }
        }
        [Category("Configs")]
        [CoreXMLElement(true)]
        public WiXProjectVariables Variables
        {
            get
            {
                return this.m_WiXVariables;
            }
        }
        [Category("Properties")]
        [Description("autoriser ou non l'installation à travers un insterface utilisateur")]
        [CoreXMLElement()]
        public bool UseGUI
        {
            get { return m_UseGUI; }
            set
            {
                if (m_UseGUI != value)
                {
                    m_UseGUI = value;
                }
            }
        }
        private string m_InstallDirName;
        [Category("Properties")]
        [Description("Nom qui fera ref�rence au repertoire d'installation de l'application")]
        [CoreXMLElement()]
        /// <summary>
        /// 
        /// </summary>
        public string InstallDirName
        {
            get { return m_InstallDirName; }
            set
            {
                if (m_InstallDirName != value)
                {
                    m_InstallDirName = value;
                }
            }
        }
        [Category("General")]
        [TypeConverter(typeof(WiXProjectGuidConverter))]
        [Editor(typeof(WiXProjectGuidEditor), typeof (UITypeEditor))]
        [CoreXMLElement()]
        public Guid  Id
        {
            get { return m_Id; }
            set
            {
                if (m_Id != value)
                {
                    m_Id = value;
                }
            }
        }
        [Category("Application")]
        [TypeConverter(typeof(WiXProjectGuidConverter))]
        [Editor(typeof(WiXProjectGuidEditor), typeof(UITypeEditor))]
        [CoreXMLElement()]
        public Guid UpdrageCode
        {
            get { return m_UpdrageCode; }
            set {
                m_UpdrageCode = value;
            }
        }
        [Category("General")]
        [Description("get or set the application information")]
        [CoreXMLElement()]
        public string Copyright
        {
            get { return m_Copyright; }
            set
            {
                if (m_Copyright != value)
                {
                    m_Copyright = value;
                }
            }
        }
        [Category("General")]
        [Description("get or set the application information")]
        [CoreXMLElement ()]
        public string Manufacteur
        {
            get { return m_Manufacteur; }
            set
            {
                if (m_Manufacteur != value)
                {
                    m_Manufacteur = value;
                }
            }
        }
        [Description("get or set the application name")]
        [Category("General")]
        [CoreXMLElement()]
        public string Name
        {
            get { return m_Name; }
            set
            {
                if (m_Name != value)
                {
                    m_Name = value;
                }
            }
        }
        [Category("Application")]
        [TypeConverter(typeof(WiXProjectVersionConverter))]
        [CoreXMLElement()]
        public Version Version { get; set; }
        [Category("Application")]
        [CoreXMLElement()]
        public string Language { get; set; }

        private FileCollections m_ProgramFiles;
        private FileCollections m_DesktopFiles;
        private FileCollections m_StartMenuFiles;
        private ConditionCollections m_conditions;
        private string m_WixFileFilter;

        [Category("Utility")]
        [CoreXMLElement()]
        /// <summary>
        /// represent the wix file filter
        /// </summary>
        public string WixFileFilter
        {
            get { return m_WixFileFilter; }
            set
            {
                if (m_WixFileFilter != value)
                {
                    m_WixFileFilter = value;
                }
            }
        }
        [Browsable(false)]
        [CoreXMLElement()]
        public FileCollections StartMenuFiles
        {
            get { return m_StartMenuFiles; }
        }
        [Browsable(false)]
        [CoreXMLElement()]
        public FileCollections DesktopFiles
        {
            get { return m_DesktopFiles; }
        }
        [Browsable(false)]
        [CoreXMLElement()]
        public FileCollections ProgramFiles
        {
            get { return m_ProgramFiles; }
        }
        [Browsable(false)]
        [CoreXMLElement()]
        public ConditionCollections Conditions
        {
            get { return this.m_conditions; }
        }
        [Category("Application")]
        [CoreXMLElement()]
        public string Description { get; set; }
        [Category("Application")]
        [CoreXMLElement()]
        public string Comments { get; set; }


        internal void GenerateWixObject(string filename)
        {
            string v_tempDir = Path.Combine(Path.GetTempPath(), "__WixProjectTemp");
            string v_ofile = Path.Combine(v_tempDir, "out." + WiXConstant.EXTENSION);
            WiXProductDocument d = this.BuildProduct(v_tempDir);
            try
            {
                StringBuilder sb = new StringBuilder();
                WiXWriter writer = WiXWriter.Create(sb);
                writer.Visit(d);
                writer.Flush();
                System.IO.File.WriteAllText(v_ofile, sb.ToString());

                PathUtils.MoveFile(v_ofile, filename, true);
                Directory.Delete(v_tempDir, true);
            }
            catch (Exception ex)
            {
                CoreLog.WriteLine(ex.Message);
            }
            
        }
        /// <summary>
        /// generate method to
        /// </summary>
        /// <param name="filename"></param>
        public bool GenerateTo(string filename)
        {
            string v_candle = WiXUtils.CANDLE;
            string v_light = WiXUtils.Light;
            if (!WiXUtils.CheckEnvironment())
            {
                MessageBox.Show(CoreSystem.GetString("WiX.Msg.FileNotExists"), CoreSystem.GetString("WiX.Title.FileNotExists"));
                return false;
            }
            string v_tempDir = Path.Combine (Path.GetTempPath(), "__WixProjectTemp");
            string v_ofile = Path.Combine(v_tempDir, "out." + WiXConstant.EXTENSION);
            WiXProductDocument d = this.BuildProduct(v_tempDir);

                //SAVE the wxs file definition
                StringBuilder sb = new StringBuilder();
                WiXWriter writer = WiXWriter.Create(sb);
                writer.Visit(d);
                writer.Flush();
                System.IO.File.WriteAllText(v_ofile, sb.ToString());
                bool v_error = this.Build(v_ofile, filename);
                if (File.Exists(v_ofile))
                    File.Delete(v_ofile);
                try
                {
                    Directory.Delete(v_tempDir, true);
                }
                catch(Exception ex) {
                    CoreLog.WriteLine(ex.Message);
                }
                if (!v_error)
                {
                    this.Save(Path.Combine(PathUtils.GetDirectoryName(filename), Path.GetFileNameWithoutExtension(filename) + "." + WiXConstant.WIXPROJECTFILE_EXTENSION));
                }
                
                //save the generate solution in output folder
                File.WriteAllText(Path.Combine(PathUtils.GetDirectoryName(filename), Path.GetFileName(v_ofile)), sb.ToString());                
                return !v_error;
            }

        private WiXProductDocument BuildProduct(string v_tempDir)
        {
            //WiXDirectoryComponent component = null;
            WiXProductDocument d = new WiXProductDocument();
            WiXFeatureEntry fet = d.GetFeature(0);
            // d.Package.InstallerVersion = this.InstallerVersion;//200
            d.Manufacturer = this.Manufacteur;
            d.Name = this.Name;
            d.UpgradeCode = this.UpdrageCode.ToString();
            d.Version = this.Version;
            d.Language = this.Language;
            d.Id = this.Id.ToString();

            //setup package
            d.Package.InstallerVersion = this.InstallerVersion;
            d.Package.Manufacturer = this.Manufacteur;
            d.Package.Description = this.Description;
            d.Package.Comments = this.Comments;





            if (PathUtils.CreateDir(v_tempDir))
            {
                //save environment
                Environment.CurrentDirectory = v_tempDir;
                if (this.Icon != null)
                {
                    FileStream fs = File.Create("appIco.ico");
                    this.Icon.Save(fs);
                    fs.Flush();
                    fs.Close();
                    //add icon for add remove program
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

                //laod variable
                foreach (KeyValuePair<string, object> item in this.ProjectVariables)
                {
                    d.Features.Add(new WiXVariable()
                    {
                        Id = item.Key,
                        Value = item.Value.ToString()
                    });
                }

                foreach (var p in this.Variables.GetType().GetProperties())
                {
                    var obj = p.GetValue(this.Variables, null);
                    if ((obj != null) && !string.IsNullOrEmpty(obj.ToString()))
                    {
                        //d.Features.Add(new WiXVariable()
                        //{
                        //    Id = p.Name ,
                        //    Value = obj.ToString()
                        //});

                        string f = obj.ToString().Trim();
                        const string VARIALBLE_FOLDER = "__variables";
                        if (PathUtils.CreateDir(Path.Combine(v_tempDir, VARIALBLE_FOLDER)))
                        {
                            PathUtils.CopyFile(
                                    f,
                                Path.Combine(v_tempDir,
                                        VARIALBLE_FOLDER,
                                        Path.GetFileName(f)), true);

                            d.Children.Add(new WiXVariable()
                            {
                                Id = p.Name,// "WixUILicenseRtf",
                                Value = Path.Combine(VARIALBLE_FOLDER, Path.GetFileName(f))
                            });
                        }
                    }
                }

                BuildProgramFileDir(d, v_tempDir);

                d.SourceTempDir = v_tempDir;
                BuildStartMenu(d, Path.Combine(v_tempDir, "__StartMenu"));
                BuildDesktopFileDir(d, Path.Combine(v_tempDir, "__DesktopMenu"));
                if (this.UseGUI)
                {
                    //gui management
                    WiXUI v_ui = new WiXUI();
                    v_ui.Children.Add(new WiXUIRef()
                    {
                        Id = WiXUIDialogName.WiXUI_INSTALLDIR
                    });
                    d.Features.Add(v_ui);
                    //ui auto start definition struct definition
                    if (this.AutoStartApplication)
                    {
                        BuildAutoStart(d, v_ui);
                    }
                }
                //build licence file 
                if (System.IO.File.Exists(this.LicenseFile))
                {
                    const string LICENCEFOLDER = "__License";
                    if (PathUtils.CreateDir(Path.Combine(v_tempDir, LICENCEFOLDER)))
                    {
                        File.Copy(this.LicenseFile,
                            Path.Combine(v_tempDir, LICENCEFOLDER,
                                    "license.rtf"));
                        d.Children.Add(new WiXVariable()
                        {
                            Id = "WixUILicenseRtf",
                            Value = Path.Combine(LICENCEFOLDER, "license.rtf")
                        });
                    }
                }
                foreach (IWiXProjectExtension item in this.m_Extension)
                {
                    item.Build(d);
                }
         
            }
            return d;
        }

        private void BuildProgramFileDir(WiXProductDocument d, string v_tempDir)
        {
            if (!string.IsNullOrEmpty(this.ProgramFileDir))
            {
                WiXDirectory v_dir = null;
                WiXDirectory v_tdir = d.Directory.AddDir(WiXDirectoryName.PROGRAMFILESFOLDER, "ProgDir");
                foreach (string item in this.ProgramFileDir.Split(Path.DirectorySeparatorChar))
                {
                    v_dir = v_tdir.AddDir(item, item);
                }
                v_dir.Id = this.InstallDirName;
                WiXDirectoryComponent component = new WiXDirectoryComponent();
                component.Guid = Guid.NewGuid().ToString();
                WiXUtils.DirectoryAccess(component, d.FeatureEntry,
                    new WiXPermission()
                    {
                        Id = null,
                        User = "Everyone",
                        GenericAll = WiXYesOrNoValue.Yes
                    }
                    );


                //add root files
                string v_target = string.Empty;
                d.SourceTempDir = v_tempDir;
                foreach (WiXProjectFile item in this.ProgramFiles)
                {
                    if (item.IsFile)
                    {
                        v_target = Path.Combine(v_tempDir,
                            Path.GetFileName(item.FileName));
                        File.Copy(item.FileName, v_target, true);
                        component.Children.Add(new WiXFileEntry()
                        {
                            Source = WiXUtils.GetSourceDir(v_tempDir, v_target),
                            Name = item.Id,
                            DiskId = "1"
                        });
                    }
                    else if (item.IsDirectory)
                    {
                        if (item.FileType == enuWiXFileType.Shortcut)
                        {
                            BuildShortcut(d, item, null, v_tempDir, component, true);
                        }
                        else
                        {
                            BuildDirectory(d, v_dir.AddDir(item.Id, item.Id), Path.Combine(v_tempDir, "__ProgramFiles"), item);
                        }
                    }
                }
                if (component.Children.Count > 0)
                {
                    v_dir.Children.Add(component);
                    d.FeatureEntry.Add(component);
                }
                //for directory modification
                d.Features.Add(new WiXProperty()
                {
                    Id = WiXPropertyName.WIXUI_INSTALLDIR,
                    Value = this.InstallDirName // "APPDIR"
                });
                //build directory component
            }
        }
        private void BuildStartMenu(WiXProductDocument d, string v_tempDir)
        {
            WiXDirectoryComponent component = null;
            WiXDirectory v_dir = null;
            v_dir = d.Directory.AddDir(WiXDirectoryName.PROGRAMMENUFOLDER, null);

            if (!string.IsNullOrEmpty(this.StartMenuDir))
            {

                string topId = string.Empty;
                foreach (string item in this.StartMenuDir.Split(Path.DirectorySeparatorChar))
                {
                    if (string.IsNullOrEmpty(topId))
                        topId = "DESK_" + item;
                    v_dir = v_dir.AddDir("DESK_" + item, item);
                    component = new WiXDirectoryComponent()
                    {
                        Guid = Guid.NewGuid().ToString()
                    };
                    v_dir.Children.Add(component);
                    component.Children.Add(new WiXRemoveFolder()
                    {
                        Id = v_dir.Id,
                        On = enuWiXRemoveFolderOn.uninstall
                    });
                  //  //register intall
                    component.Children.Add(new WiXRegistryValue()
                    {
                        Key = "Software\\" + this.Manufacteur + "\\" + this.Name + "\\" + v_dir.Id,
                        Name = "installed",
                        Value = "1",
                        Type = "integer",
                        Id = null
                    });
               
                    //add to features
                    if (component.Children.Count > 0)
                    {
                        d.FeatureEntry.Add(component);
                    }
                }
            }
            else {
                component = new WiXDirectoryComponent()
                {
                    Guid = Guid.NewGuid().ToString()
                };
                component.Children.Add(new WiXRemoveFolder()
                {
                    Id = v_dir.Id,
                    On = enuWiXRemoveFolderOn.uninstall
                });
                //register intall
                component.Children.Add(new WiXRegistryValue()
                {
                    Key = "Software\\" + this.Manufacteur + "\\" + this.Name + "\\" + v_dir.Id,
                    Name = "installed",
                    Value = "1",
                    Type = "integer",
                    Id = null
                });
                v_dir.Children.Add(component);
                d.FeatureEntry.Add(component);
            }

                string v_startmenuTemp = Path.Combine(v_tempDir, "StartMenu");
                PathUtils.CreateDir(v_startmenuTemp);
                string v_target = string.Empty;
                foreach (WiXProjectFile item in this.StartMenuFiles)
                {
                    if (item.FileType == enuWiXFileType.Shortcut)
                    {
                        this.BuildShortcut(d, item, v_dir, v_startmenuTemp, component, false);                     
                    }
                    else if (item.IsFile)
                    {
                        v_target = Path.Combine(v_startmenuTemp,
                            Path.GetFileName(item.FileName));
                        File.Copy(item.FileName, v_target, true);
                        component.Children.Add(new WiXFileEntry()
                        {
                            Source = WiXUtils.GetSourceDir(v_tempDir, v_target),
                            Name = item.Id,
                            DiskId = "1"
                        });
                    }
                    else if (item.IsDirectory)
                    {
                        WiXDirectory v_dir2 = v_dir.AddDir(item.Id, item.FileName);
                        BuildDirectory(d, v_dir2, v_startmenuTemp, item);
                      //  v_dir.Children.Add(component);
                        var ccomp = v_dir2.Children.ToArray()[0] as WiXDirectoryComponent;
                        ccomp.Children.Add(new WiXRemoveFolder()
                        {
                            Id = v_dir2.Id,
                            On = enuWiXRemoveFolderOn.uninstall
                        });
                        //  //register intall
                        ccomp.Children.Add(new WiXRegistryValue()
                        {
                            Key = "Software\\" + this.Manufacteur + "\\" + this.Name + "\\" + v_dir2.Id,
                            Name = "installed",
                            Value = "1",
                            Type = "integer",
                            Id = null
                        });
                        if (v_dir2.Children.Count == 0)
                        {
                            v_dir.Children.Remove(v_dir2);
                        }
                    }
                }
            
        }
        private void BuildDesktopFileDir(WiXProductDocument d, string v_tempDir)
        {
            this.BuildSystemFolder(d, v_tempDir,
                WiXDirectoryName.DESKTOPFILESFOLDER,
                this.DesktopMenuDir,
                this.m_DesktopFiles);
            
        }

        private void BuildSystemFolder(WiXProductDocument d, string v_tempDir, string wixfolderName, string p, FileCollections fileCollections)
        {

            WiXDirectoryComponent component = null;
            WiXDirectory v_dir = null;
            v_dir = d.Directory.AddDir(wixfolderName, null);

            if (!string.IsNullOrEmpty(this.StartMenuDir))
            {

                string topId = string.Empty;
                foreach (string item in this.StartMenuDir.Split(Path.DirectorySeparatorChar))
                {
                    if (string.IsNullOrEmpty(topId))
                        topId = "DESK_" + item;
                    v_dir = v_dir.AddDir("DESK_" + item, item);
                    component = new WiXDirectoryComponent()
                    {
                        Guid = Guid.NewGuid().ToString()
                    };
                    v_dir.Children.Add(component);
                    component.Children.Add(new WiXRemoveFolder()
                    {
                        Id = v_dir.Id,
                        On = enuWiXRemoveFolderOn.uninstall
                    });
                    //  //register intall
                    component.Children.Add(new WiXRegistryValue()
                    {
                        Key = "Software\\" + this.Manufacteur + "\\" + this.Name + "\\" + v_dir.Id,
                        Name = "installed",
                        Value = "1",
                        Type = "integer",
                        Id = null
                    });

                    //add to features
                    if (component.Children.Count > 0)
                    {
                        d.FeatureEntry.Add(component);
                    }
                }
            }
            else
            {
                component = new WiXDirectoryComponent()
                {
                    Guid = Guid.NewGuid().ToString()
                };
                component.Children.Add(new WiXRemoveFolder()
                {
                    Id = v_dir.Id,
                    On = enuWiXRemoveFolderOn.uninstall
                });
                //register intall
                component.Children.Add(new WiXRegistryValue()
                {
                    Key = "Software\\" + this.Manufacteur + "\\" + this.Name + "\\" + v_dir.Id,
                    Name = "installed",
                    Value = "1",
                    Type = "integer",
                    Id = null
                });
                v_dir.Children.Add(component);
                d.FeatureEntry.Add(component);
            }

            string v_startmenuTemp = Path.Combine(v_tempDir, "StartMenu");
            PathUtils.CreateDir(v_startmenuTemp);
            string v_target = string.Empty;
            foreach (WiXProjectFile item in fileCollections )
            {
                if (item.FileType == enuWiXFileType.Shortcut)
                {
                    this.BuildShortcut(d, item, v_dir, v_startmenuTemp, component, false);
                }
                else if (item.IsFile)
                {
                    v_target = Path.Combine(v_startmenuTemp,
                        Path.GetFileName(item.FileName));
                    File.Copy(item.FileName, v_target, true);
                    component.Children.Add(new WiXFileEntry()
                    {
                        Source = WiXUtils.GetSourceDir(v_tempDir, v_target),
                        Name = item.Id,
                        DiskId = "1"
                    });
                }
                else if (item.IsDirectory)
                {
                    WiXDirectory v_dir2 = v_dir.AddDir(item.Id, item.FileName);
                    BuildDirectory(d, v_dir2, v_startmenuTemp, item);
                    //  v_dir.Children.Add(component);
                    var ccomp = v_dir2.Children.ToArray()[0] as WiXDirectoryComponent;
                    ccomp.Children.Add(new WiXRemoveFolder()
                    {
                        Id = v_dir2.Id,
                        On = enuWiXRemoveFolderOn.uninstall
                    });
                    //  //register intall
                    ccomp.Children.Add(new WiXRegistryValue()
                    {
                        Key = "Software\\" + this.Manufacteur + "\\" + this.Name + "\\" + v_dir2.Id,
                        Name = "installed",
                        Value = "1",
                        Type = "integer",
                        Id = null
                    });
                    if (v_dir2.Children.Count == 0)
                    {
                        v_dir.Children.Remove(v_dir2);
                    }
                }
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="d">project document</param>
        /// <param name="dir">directory to build</param>
        /// <param name="v_tempDir">temp folder</param>
        /// <param name="file">file item</param>
        private void BuildDirectory(
            WiXProductDocument d, 
            WiXDirectory dir, 
            string v_tempDir, 
            WiXProjectFile file)
        {
            if (file.FileType == enuWiXFileType.Shortcut)
            {
                return;
            }
            string dirpath = 
                Path.Combine(v_tempDir,
                       dir.Name);
            if (!PathUtils.CreateDir(dirpath)) 
                return;
            string v_target = string.Empty;
            WiXDirectoryComponent comp = new WiXDirectoryComponent();
            comp.Guid = Guid.NewGuid().ToString();
            WiXUtils.DirectoryAccess(comp, d.FeatureEntry,
                 new WiXPermission()
                 {
                     Id = null,
                     User = "Everyone",
                     GenericAll = WiXYesOrNoValue.Yes
                 }
             );

            string v_topDir = string.Empty;
            ////permissions
            //comp.Children.Add(new WiXCreateFolder()
            //{
            //    Id = null,
            //    Directory = dir.Id // "[" + this.InstallDirName + "]/"+dir.Id 
            //});
            
            foreach (WiXProjectFile item in file)
            {
                if (item.IsFile)
                {
                    v_target = Path.Combine(v_tempDir,
                        dir.Name , 
                        Path.GetFileName(item.FileName));
                    File.Copy(item.FileName, v_target, true);
                    comp.Children.Add(new WiXFileEntry()
                    {
                        Source = WiXUtils.GetSourceDir(d.SourceTempDir, v_target),
                        Name = item.Id,
                        DiskId = "1"
                    });
                }
                else if (item.IsDirectory)
                {
                    if (item.FileType == enuWiXFileType.Directory)
                    {
                        WiXDirectory v_dir =  dir.AddDir(item.Id, item.Id);
                        v_dir.Id = "Dir_" + v_dir.GetHashCode();
                        BuildDirectory(d, 
                            v_dir , 
                             Path.Combine(v_tempDir, dir.Name) ,
                            item);
                        if (v_dir.Children.Count == 0)
                        {//remove empty directory children
                            dir.Children.Remove(v_dir);
                        }
                    }
                    else if (item.FileType == enuWiXFileType.Shortcut )
                    {
                        this.BuildShortcut(d, item, dir, v_tempDir,comp, true);                      
                    }
                }
            }

            //if (unistall)
            //{
            //    comp.Children.Add(new WiXRemoveFolder()
            //    {
            //        Id = dir.Id,
            //        On = enuWiXRemoveFolderOn.uninstall
            //    });
            //    //register unisntall
            //    comp.Children.Add(new WiXRegistryValue()
            //    {
            //        Key = "Software\\" + this.Manufacteur + "\\" + this.Name + "\\" + dir.Id,
            //        Name = "installed",
            //        Value = "1",
            //        Type = "integer",
            //        Id = null
            //    });
            //}
            if (comp.Children.Count > 0)
            {
                dir.Children.Add(comp);
                d.FeatureEntry.Add(comp);
            }   
        }

        private void BuildShortcut(WiXProductDocument d, WiXProjectFile file, WiXDirectory dir,
            string v_tempDir, WiXDirectoryComponent comp, bool unistall = false)
        {
            var s = new WiXShortCut()
            {
                Id = file.Id, // "DrSStudioShortCut",
                Name = file.FileName, // "DrSStudio",
                Description = file.Description,// "DrSStudio Shortcut",
                Target = file.Target, // "[AppDir]drs.exe",
                WorkingDirectory = file.WorkingDir,// "AppDir"
            };
            comp.Children.Add(s);
            //if (unistall)
            //{
            //    comp.Children.Add(new WiXRemoveFolder()
            //    {
            //        Id = dir.Id,
            //        On = enuWiXRemoveFolderOn.uninstall
            //    });
            //    //register unisntall
            //    comp.Children.Add(new WiXRegistryValue()
            //    {
            //        Key = "Software\\" + this.Manufacteur + "\\" + this.Name + "\\" + dir.Id,
            //        Name = "installed",
            //        Value = "1",
            //        Type = "integer",
            //        Id = null
            //    });
            //}
        }
   
        private void BuildAutoStart(WiXProductDocument d, WiXUI v_ui)
        {
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
            //for start app on installation complete
            d.Features.Add(new WiXProperty()
            {
                Id = "WIXUI_EXITDIALOGOPTIONALCHECKBOX",
                Value = this.AutoStartApplicationChecked ? "1" : "0"
            });
            if (!string.IsNullOrEmpty(this.AutoStartApplicationCheckedText))
            {
                d.Features.Add(new WiXProperty()
                {
                    Id = "WIXUI_EXITDIALOGOPTIONALCHECKBOXTEXT",
                    Value = this.AutoStartApplicationCheckedText
                });
            }
            d.Features.Add(new WiXProperty()
            {
                Id = "WixShellExecTarget",
                Value = this.AutoStartApplicationPath//"[AppDir]drs.exe"
            });

    
        }
        private bool  Build(string obfile, string outfile)
        {
            string v_candle = WiXUtils.CANDLE;
            string v_light = WiXUtils.Light;
            string obj = obfile;
            string v_err_str = string.Empty;
            bool v_err = false;
            ProcessStartInfo info = new ProcessStartInfo();
            info.WindowStyle = ProcessWindowStyle.Hidden;
            info.CreateNoWindow = false ;
            info.FileName = v_candle;
            info.Arguments = "" + obj;
            info.RedirectStandardOutput = true;
            info.RedirectStandardError = true;
            info.UseShellExecute = false;
            info.WorkingDirectory = PathUtils.GetDirectoryName(obj);
            Process p = Process.Start(info);
            p.WaitForExit();
            if (p.ExitCode == 0)
            {
                info = new ProcessStartInfo();
                info.WindowStyle = ProcessWindowStyle.Hidden;
                info.CreateNoWindow = false;
                info.FileName = v_light;
                obj = Path.Combine(PathUtils.GetDirectoryName(obj), Path.GetFileNameWithoutExtension(obj) +"."+ WiXConstant.WIXOBJEXTENSION);
                if (File.Exists(obj))
                {
                    info.Arguments = WiXUtils.getCommandLine("\"" + obj + "\" -out \"" + outfile + "\"");
                    //info.WorkingDirectory = PathUtils.GetDirectoryName(lib);
                    info.RedirectStandardOutput = false  ;
                    info.RedirectStandardError = false   ;
                    info.UseShellExecute = false;
                    Process h = new Process();
                    h.StartInfo = info;
                    h.Start();
                    h.WaitForExit();
                    if (h.ExitCode != 0)
                    {
                        try
                        {
                            v_err_str = "Error light.exe " + h.ExitCode+"\n";
                            v_err_str = "Light code message : " + GetLightMessage(h.ExitCode);
                            //if (h.StandardError.BaseStream != null)
                            //{
                            //    StreamReader r = new StreamReader(h.StandardError.BaseStream);
                            //    v_err_str += r.ReadToEnd();
                            //    r.Close();
                            //    r = new StreamReader(h.StandardOutput.BaseStream);
                            //    v_err_str += r.ReadToEnd();
                            //}
                        }
                        catch(Exception ex)
                        {
                            v_err_str += "\n" + ex.Message;
                        }
                        v_err = true;
                    }
                }
                else
                    v_err = true;
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
            if (v_err)
            {
                MessageBox.Show(v_err_str, "title.Error".R());
            }
            return v_err;
        }

        private string GetLightMessage(int p)
        {
            switch (p)
            {
                case 92:
                    return "identifiant dupliquer trouver";                    
                default:
                    break;
            }
            return string.Empty;
        }
        
        public WiXProject()
        {
            this.m_Extension = new WiXProjectExtensionCollection(this);
            this.Language = "1033";
            this.m_InstallDirName = "APPDIR";
            this.m_WixFileFilter = "*.exe|*.dll|*.txt|*.ico|*.jpg";
            this.Version = new Version("1.0.0.0");
            this.InstallerVersion = 200;
            this.m_UseGUI = true;
            //this.m_desktoDir = new WiXDirectory();
            //this.m_programFileDir = new WiXDirectory();
            //this.m_startMenuDir = new WiXDirectory();
            this.m_Id = Guid.NewGuid();
            this.m_UpdrageCode = Guid.NewGuid();
            this.m_DesktopFiles = new FileCollections(this);
            this.m_ProgramFiles = new FileCollections(this);
            this.m_StartMenuFiles = new FileCollections(this);
            this.m_conditions = new ConditionCollections(this);
            this.m_WiXProjectVariables = new WiXProjectVariableCollections(this);
            this.m_WiXVariables = new WiXProjectVariables(this);
        }



        protected virtual void OnFileCollectionChanged(WiXProjectFileCollectionEventArgs e)
        {
            if (FileCollectionChanged != null)
                this.FileCollectionChanged(this, e);
        }
        /// <summary>
        /// load project from file
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static WiXProject LoadFromFile(string filename)
        {
            if (!File.Exists(filename))
                return null;

            
            //StringBuilder sb = new StringBuilder ();
            //sb.Append (File.ReadAllText (filename ));
            StreamReader sr = new StreamReader(filename );
            System.Xml.XmlReader v_xreader = null;

            try
            {
                v_xreader = System.Xml.XmlReader.Create(sr, new System.Xml.XmlReaderSettings()
                {
                    IgnoreComments = true
                }
                );
                if (v_xreader.ReadToDescendant("IGKWixProject"))
                {
                    WiXProject v_prg = new WiXProject();
                    CoreXMLDeserializer deseri = CoreXMLDeserializer.Create(v_xreader);
                    deseri.MoveToContent();
                    CoreXMLSerializerUtility.ReadElements(v_prg, deseri);
                    return v_prg;
                }
                else {
                    return null;
                }
            }
            catch
            {
            }
            finally {
                if (sr != null)
                    sr.Close();
                sr = null;
            }




            return null;
        }
        /// <summary>
        /// save the current project
        /// </summary>
        /// <param name="filename"></param>
        public void Save(string filename)
        {
            if (string.IsNullOrEmpty(filename))
                return;
            //save the current project
            try
            {
                StringBuilder sb = new StringBuilder();
                System.Xml.XmlWriterSettings s = new System.Xml.XmlWriterSettings();
                s.Indent = true;
                WiXProjectWriter writer = new WiXProjectWriter(sb, s);
                writer.Visit(this);
                writer.Flush();

                File.WriteAllText(filename, sb.ToString());
            }
            catch (Exception ex)
            {
                CoreLog.WriteDebug("Exception wix on visitor :" + ex.Message);
            }
            finally
            {

            }
        }     
        /// <summary>
        /// represent the file collection
        /// </summary>
        public class FileCollections : IEnumerable,             
        ICoreDeserializable
        {
            List<WiXProjectFile> m_files;
            WiXProject m_project;
            public FileCollections(WiXProject project)
            {
                m_files = new List<WiXProjectFile>();
                this.m_project = project;
            }
            public IEnumerator GetEnumerator()
            {
                return m_files.GetEnumerator();
            }
            internal void AddRange(WiXProjectFile[] wiXProjectFile)
            {
                this.m_files.AddRange(wiXProjectFile);
                this.m_project.OnFileCollectionChanged(new WiXProjectFileCollectionEventArgs(this, this.m_project ));
            }
            public void Remove(WiXProjectFile f)
            {
                this.m_files.Remove(f);
            }
            internal void Clear()
            {
                this.m_files.Clear();
            }
         
            void ICoreDeserializable.Deserialize(IXMLDeserializer xreader)
            {
                List<WiXProjectFile> v_files = new List<WiXProjectFile>();
                WiXProjectFile p = null;
                xreader.MoveToContent();
                while (xreader.Read())
                {
                  
                    switch (xreader.NodeType)
                    {
                        case System.Xml.XmlNodeType.EndElement :
                            if ((p!=null) && (xreader.Name == "Directory"))
                            { 
                                p = p.Parent;
                            }
                            break;
                        case System.Xml.XmlNodeType.Element:

                            switch (xreader.Name)
                            {
                                case "File":
                                    {
                                    string n = xreader.GetAttribute("Name");
                                        string f = xreader.GetAttribute("SourcePath");
                                        WiXProjectFile wdir = new WiXProjectFile (f);
                                        wdir.Id = n;
                                        if (p != null)
                                        {
                                            p.AddChild(wdir);
                                        }
                                        else {
                                            m_files.Add(wdir);
                                        }
                                    }
                                    break;
                                case "Shortcut":
                                    {
                                        string n = xreader.GetAttribute("Name");
                                        var c = WiXProjectFile.CreateShortcut(n,
                                            xreader.GetAttribute("Target"),
                                            xreader.GetAttribute("Description"),
                                            xreader.GetAttribute("WorkingDirectory"));
                                        
                                        if (p != null)
                                            p.AddChild(c);
                                        else {
                                            m_files.Add(c);
                                        }
                                    }
                                    break;
                                case "Directory":
                                    {
                                        string n = xreader.GetAttribute("Name");
                                        if (!string.IsNullOrEmpty(n))
                                        {
                                            WiXProjectFile wdir = new WiXProjectFile(n);
                                    
                                            if (p != null) {
                                                p.AddChild(wdir);
                                            }
                                            else
                                                v_files.Add(wdir);

                                            p = wdir;
                                        }
                                    }

                                    break;
                                default:
                                    break;
                            }

                            break;
                        default:
                            break;
                    }
                    
                }
                this.AddRange(v_files.ToArray());
            }
        }
        /// <summary>
        /// Get or set the wix project extension
        /// </summary>
        public class WiXProjectExtensionCollection : IEnumerable 
        {
            List<IWiXProjectExtension> m_extension;
            WiXProject m_project;
            public WiXProjectExtensionCollection(WiXProject project)
            {
                m_extension = new List<IWiXProjectExtension>();
                m_project = project;
            }
            /// <summary>
            /// get the number of the wix extension
            /// </summary>
            public int Count {
                get {
                    return this.m_extension.Count;
                }
            }
            public IEnumerator GetEnumerator()
            {
                return this.m_extension.GetEnumerator();
            }
        }
        /// represent the file collection
        /// </summary>
        public class ConditionCollections : IEnumerable
        {
            List<WiXCondition> m_conditions;
            WiXProject m_project;
            public ConditionCollections(WiXProject project)
            {
                m_conditions = new List<WiXCondition>();
                this.m_project = project;
            }
            public IEnumerator GetEnumerator()
            {
                return m_conditions.GetEnumerator();
            }
            internal void AddRange(WiXCondition[] conditions)
            {
                this.m_conditions.AddRange(conditions);               
            }
            internal void Clear()
            {
                this.m_conditions.Clear();
            }
            internal void Add(WiXCondition wiXCondition)
            {
                this.m_conditions.Add(wiXCondition);
                //IGK.DrSStudio.Drawing2D.
                //this.m_project.OnConditionAdded(new CoreElementChangedEventArgs<WiXCondition>(wiXCondition));
            }
            internal void Remove(WiXCondition wixCondition)
            {
                this.m_conditions.Remove(wixCondition);
                //this.m_project.OnConditionRemoved(new CoreElementChangedEventArgs<WiXCondition>(wixCondition));
            }
        }

      
        public event EventHandler<WiXProjectFileCollectionEventArgs> FileCollectionChanged;
        private WiXProjectVariableCollections m_WiXProjectVariables;
        private WiXProjectVariables m_WiXVariables;
        private string m_DesktopMenuDir;


        string ICoreIdentifier.Id
        {
            get {
                return this.Id.ToString ();
            }
        }

    }
}

