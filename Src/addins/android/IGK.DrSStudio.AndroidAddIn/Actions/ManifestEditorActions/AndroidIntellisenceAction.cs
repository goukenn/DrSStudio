

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AndroidIntellisenceAction.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using ICSharpCode.AvalonEdit.CodeCompletion;

using IGK.ICore;
using IGK.DrSStudio.Android.CodeCompletion;
using IGK.DrSStudio.Android.Tools;
using IGK.ICore.Settings;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.DrSStudio.Android.Actions
{
    /// <summary>
    /// represent intelissence action
    /// </summary>
    public class AndroidIntellisenceAction : AndroidManifestActions
    {
        CompletionWindow m_codeWindow;
        private IList<ICompletionData> m_data;
        private Dictionary<string, AndroidAttributeDefinition> m_attributeDefinitions;

        public AndroidIntellisenceAction()
        {
            this.m_attributeDefinitions = new Dictionary<string, AndroidAttributeDefinition>();
        }
        protected override bool PerformAction()
        {
            if (m_codeWindow == null)           
            {
                m_codeWindow = new CompletionWindow(this.TextArea);
                m_codeWindow.ShowInTaskbar = false;
                m_codeWindow.Closed += m_codeWindow_Closed;
                m_data = m_codeWindow.CompletionList.CompletionData;
                this.Surface.Disposed += Surface_Disposed;
            }
            initCompletionData();
             
            m_codeWindow.Show();                
            return false;
        }

        void Surface_Disposed(object sender, EventArgs e)
        {
            this.Surface.Disposed -= Surface_Disposed;
            if (this.m_codeWindow !=null)
                this.m_codeWindow.Close();
        }

        void m_codeWindow_Closed(object sender, EventArgs e)
        {
            this.Surface.Disposed -= Surface_Disposed;
            this.m_codeWindow = null;
        }

        /*
         * 
         * init completions window from android manifest propertiss list found in 
         * <android-sdk>/platforms/<target-platform>/data/res/manifest_attrs.xml
         * 
         * 
         * */
        private void initCompletionData()
        {
            m_attributeDefinitions.Clear();
            if (AndroidTargetManagerTool.Instance.TargetInfo != null)
            {
                AndroidTargetManagerTool.Instance.LoadAttributes("values.attrs_manifest", m_attributeDefinitions);
               
                foreach (KeyValuePair<string, AndroidAttributeDefinition> item in m_attributeDefinitions.OrderBy(
                    (o)=>{
                        return o.Value;
                    }
                    ))
                {
                    this.m_data.Add(new AndroidManifestCompletionData(item.Value.Name, item.Value));
                }
                
            }
        }


        public override enuKeys Key
        {
            get {return (enuKeys)CoreSettings.GetSettingValue("EnvironmentShortCut.ShowIntellisence", enuKeys.Control | enuKeys.Space);}
        }
    }
}
