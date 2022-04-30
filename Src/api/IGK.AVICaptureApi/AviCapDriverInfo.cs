

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AviCapDriverInfo.cs
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
file:AviCapDriverInfo.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.AVICaptureApi
{
    /// <summary>
    /// represent a avi cap driver
    /// </summary>
    public class AviCapDriverInfo
    {
        private string m_Name;
        private string m_Version;
        private int m_Id;
        public int Id
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
        public string Version
        {
            get { return m_Version; }
        }
        public string Name
        {
            get { return m_Name; }
        }
        public AviCapDriverInfo(string name, string version, uint id)
        {
            this.m_Name = name;
            this.m_Version = version;
            this.m_Id =(int) id;
        }
        public override string ToString()
        {
            return string.Format("{0} - [{1}]:[{2}]", this.Name, this.Version, this.Id );
        }
    }
}

