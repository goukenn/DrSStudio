

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreApplication.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV - [2008 - 2014]
author: C.A.D . BONDJE DOUE
file:CoreApplication.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace IGK.DrSStudio.WinLauncher
{
    [CoreApplication("DrSStudio")]
    class CoreApplication : CoreApplicationBase, ICoreApplication 
    {
        public event EventHandler ApplicationExit {
            add {
                Application.ApplicationExit += value;
            }
            remove {
                Application.ApplicationExit -= value;
            }
        }
        public ICoreResourceManager m_ResourcesManager;
        public void Close()
        {
            CoreSystem.Instance.Workbench.MainForm.Close();
        }
        public string CurrentWorkingPath
        {
            get { return Environment.CurrentDirectory; }
        }
        public ICoreResourceManager ResourcesManager
        {
            get {
                if (m_ResourcesManager == null)
                {
                    m_ResourcesManager = new WinCoreResourceManager() ;
                }
                return m_ResourcesManager;
            }
        }
    }
}

