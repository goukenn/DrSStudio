

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AndroidAttributeDefinitionTool.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.WinCore;
using IGK.ICore;using IGK.ICore.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Android.Tools
{
    [CoreTools("Tool.AndroidAttributeDefinition")]
    /*
     * used to store android xml attribute definition to help edito
     * */
    public class AndroidAttributeDefinitionTool : AndroidToolBase
    {
        private static AndroidAttributeDefinitionTool sm_instance;
        private bool m_buzy; // get or set if this is buzy
        private object  m_sync_obj = new object();
        private Dictionary<string, AndroidAttributeDefinition> m_attributeDefinitions;

        private AndroidAttributeDefinitionTool()
        {
            m_attributeDefinitions = new Dictionary<string, AndroidAttributeDefinition>();
            AndroidTargetManagerTool.Instance.TargetInfoChanged += Instance_TargetInfoChanged;
        }

        void Instance_TargetInfoChanged(object sender, EventArgs e)
        {
            this.Loading();
        }

        public static AndroidAttributeDefinitionTool Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static AndroidAttributeDefinitionTool()
        {
            sm_instance = new AndroidAttributeDefinitionTool();            
        }
        protected override void GenerateHostedControl()
        {
            base.GenerateHostedControl();
            
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        //loading attributes
        void Loading()
        {
            if (this.m_buzy)
                return;
            lock (this.m_sync_obj)
            {
                this.m_buzy = true;
                m_attributeDefinitions.Clear();
                AndroidTargetManagerTool.Instance.LoadAttributes("values.attrs", m_attributeDefinitions);
                
            }
            this.m_buzy = false;
        }

        public AndroidAttributeDefinition GetDefinition(string defName)
        {
            if (this.m_attributeDefinitions.ContainsKey (defName ))
                return this.m_attributeDefinitions[defName];
            return null;
        }
    }
}
