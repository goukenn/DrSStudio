

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AndroidProjectManager.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/

using IGK.ICore;using IGK.ICore.IO;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:AndroidProjectManager.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.Android
{
    /// <summary>
    /// save resources
    /// </summary>
    public class AndroidProjectManager
    {
        /// <summary>
        /// represent the android project
        /// </summary>
        /// <param name="project"></param>
        public static void SaveResources(IAndroidSolution project)
        {
            string v_d = string.Format("{0}/res", project.TargetLocation);
            string v_values = string.Format("{0}/values", v_d);
            if (PathUtils.CreateDir(v_values))
            {//save resources project
                project.Resources["values"].SaveTo(v_values);
            }
            v_values = string.Format("{0}/drawable", v_d);
            if (PathUtils.CreateDir(v_values))
            {//save resources project
                project.Resources["drawable"].SaveTo(v_values);
            }
            foreach (IAndroidResourceContainer item in project.Resources)
            {
                item.SaveTo(v_d);
            }
        }
    }
}

