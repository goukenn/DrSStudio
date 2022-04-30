

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WinCoreDesigner.cs
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
file:Designer.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
using System.Windows.Forms;
using System.ComponentModel;
using IGK.ICore;
namespace IGK.DrSStudio.WinUI.Design
{
    /// <summary>
    /// represent the base core designer
    /// </summary>
    public class WinCoreDesigner
    {
        static readonly string debug_folder = Path.GetFullPath(CoreConstant.DRS_SRC+ @"\..\Bin\x86\Debug");
        private static bool sm_initialize;
        static WinCoreDesigner() {
            sm_initialize = false;
        }
        public static void Init()
        {            
            if (CoreSystemEnvironment.DesignMode)
            {
                if (!sm_initialize)
                {
                    try
                    {
                        //manually register application
                        CoreApplicationManager.RegisterApplication(typeof(WinCoreDesignerApplication));
                        //initialize application
                        CoreSystem.Init();
                        CoreSystem.Instance.LoadAssemblyDir(debug_folder);
                    }
                    catch(Exception ex) 
                    {
                        MessageBox.Show("Error Append On Designer \n"+ex.GetType()+"\n"+ex.Message );
                        sm_initialize = false ;
                        return;
                    }       
                    //initialize allways
                    sm_initialize = false;
                }
            }
        }
    }
}

