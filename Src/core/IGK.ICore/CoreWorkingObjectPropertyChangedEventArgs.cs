

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreWorkingObjectPropertyChangedEventArgs.cs
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
file:CoreWorkingObjectPropertyChangedEventArgs.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore
{
    public delegate void CoreWorkingObjectPropertyChangedEventHandler(object o, CoreWorkingObjectPropertyChangedEventArgs e); 
    public class CoreWorkingObjectPropertyChangedEventArgs :
        EventArgs 
    {
        private enuPropertyChanged  m_Id;
        private object[] m_params;
        public enuPropertyChanged ID
        {
            get { return m_Id; }
        }
        public object[] Params {
            get {
                return this.m_params;
            }
        }
        public object GetParam(int index)
        { 
            if( (this.m_params !=null) &&  (index>=0) && (index < this.m_params.Length ))
            {
                return this.m_params[index];
            }
            return null;
        }
        public static readonly CoreWorkingObjectPropertyChangedEventArgs Definition;
        public static readonly CoreWorkingObjectPropertyChangedEventArgs Id;
        public new static readonly CoreWorkingObjectPropertyChangedEventArgs Empty;
        static CoreWorkingObjectPropertyChangedEventArgs()
        {
            Definition  = new CoreWorkingObjectPropertyChangedEventArgs(enuPropertyChanged.Definition);
            Id = new CoreWorkingObjectPropertyChangedEventArgs(enuPropertyChanged.Id );
            Empty = new CoreWorkingObjectPropertyChangedEventArgs(enuPropertyChanged.Definition );
        }
        public CoreWorkingObjectPropertyChangedEventArgs(int id, params object[] param)
        {
            this.m_Id = (enuPropertyChanged )id;
            this.m_params = param;
        }
        public CoreWorkingObjectPropertyChangedEventArgs(enuPropertyChanged  id, params object[] param)
        {
            this.m_Id = id;
            this.m_params = param;
        }
    }
}

