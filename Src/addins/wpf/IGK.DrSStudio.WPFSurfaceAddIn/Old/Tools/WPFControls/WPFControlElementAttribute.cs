

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WPFControlElementAttribute.cs
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
file:WPFControlElementAttribute.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.WPFSurfaceAddIn.Tools.WPFControls
{
    [AttributeUsage (AttributeTargets.Class , AllowMultiple = false , Inherited = false )]
    public class WPFControlElementAttribte : WPFElementAttribute
    {
        private string m_From;
        /// <summary>
        /// get the assembly
        /// </summary>
        public string From
        {
            get { return m_From; }
            set
            {
                if (m_From != value)
                {
                    m_From = value;
                }
            }
        }
        public override string GroupName
        {
            get
            {
                return "Control";
            }
        }
        public WPFControlElementAttribte(string name, Type mecanism):base(name, mecanism )
        {
            this.IsVisible = true;
            this.ImageKey = name;
            this.CaptionKey = name;
        }
    }
}

