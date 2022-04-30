

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WsdlBindingOperation.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.DrSStudio.Wsdl
{
    public abstract class WsdlBindingOperation : WsdlItem 
    {
        private string m_soapAction;

        private enuWsdlStyle m_style;
        
        public enuWsdlStyle style
        {
            get { return m_style; }
            set
            {
                if (m_style != value)
                {
                    m_style = value;
                }
            }
        }
        public string soapAction
        {
            get { return m_soapAction; }
            set
            {
                if (m_soapAction != value)
                {
                    m_soapAction = value;
                }
            }
        }
    }
}
