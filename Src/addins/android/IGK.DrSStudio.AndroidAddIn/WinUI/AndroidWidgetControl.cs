

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AndroidWidgetControl.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.WinCore;
using IGK.ICore;using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.DrSStudio.Android.WinUI
{
    using IGK.DrSStudio.Android.JAVA;
    using IGK.DrSStudio.Android.Tools;
    
    using IGK.ICore.Resources;
    using System.IO;
    using System.Runtime.InteropServices;
    using IGK.ICore.WinCore.WinUI.Controls;

   
    public class AndroidWidgetControl : IGKXUserControl
    {
        
        private System.Windows.Forms.WebBrowser c_webbrowser;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.ListBox c_lsb_widget;

        private AndroidTargetInfo m_TargetInfo;

        public AndroidTargetInfo TargetInfo
        {
            get { return m_TargetInfo; }
            set
            {
                if (m_TargetInfo != value)
                {
                    m_TargetInfo = value;
                    OnTargetInfoChanged(EventArgs.Empty);
                }
            }
        }
        public event EventHandler TargetInfoChanged;

        protected virtual void OnTargetInfoChanged(EventArgs e)
        {
            if (TargetInfoChanged != null)
            {
                TargetInfoChanged(this, e);
            }
        }



        private JAVAClass m_SelectedWidget;

        public JAVAClass SelectedWidget
        {
            get { return m_SelectedWidget; }
            set
            {
                if (m_SelectedWidget != value)
                {
                    m_SelectedWidget = value;
                    OnSelectedWidgetChanged(EventArgs.Empty);
                }
            }
        }
        public event EventHandler SelectedWidgetChanged;
        ///<summary>
        ///raise the SelectedWidgetChanged 
        ///</summary>
        protected virtual void OnSelectedWidgetChanged(EventArgs e)
        {
            if (SelectedWidgetChanged != null)
                SelectedWidgetChanged(this, e);
        }

        private class AndroidJavaClassViewItem
        {
            private JAVAClass item;
            public JAVAClass Widget { get { return this.item; } }
            public AndroidJavaClassViewItem(JAVAClass item)
            {
                
                this.item = item;
            }
            public override string ToString()
            {
                var t = this.item.Name.Trim ().Split('.');
                return t[t.Length - 1];
            }
            
        }
        public AndroidWidgetControl()
        {
            this.Load += _Load;
            this.InitializeComponent();
        }

        private void _Load(object sender, EventArgs e)
        {
            //load and init width
            this.c_lsb_widget.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.c_lsb_widget.IntegralHeight = false;
            this.TargetInfoChanged +=_targetinfo_Changed;
            this.c_lsb_widget.SelectedIndexChanged += c_lsb_widget_SelectedIndexChanged;
            this.c_lsb_widget.MultiColumn = false;
            this.SelectedWidgetChanged += AndriodWidgetControl_SelectedWidgetChanged;
            this.c_webbrowser.ObjectForScripting = new JSFunction(this);
            InitInfo();
        }

        void AndriodWidgetControl_SelectedWidgetChanged(object sender, EventArgs e)
        {
            if (this.SelectedWidget != null)
            {
                string d = @"<html><head><title>$title</title></head><body><div>$pagetitle</div><div>$content</div></body></html>";
                d = d.Replace("$title", "title.Information".R());
                d = d.Replace("$pagetitle", this.SelectedWidget.Name);
                d = d.Replace("$content", getDescriptionContent());
                
                this.c_webbrowser.DocumentText = d;
            }
            else {
                this.c_webbrowser.DocumentText = "No widged selected";
            }
        }

        private string getDescriptionContent()
        {
            StringBuilder sb = new StringBuilder();
            IGK.ICore.Xml.CoreXmlElement r = IGK.ICore.Xml.CoreXmlElement.CreateXmlNode ("ul");
            JAVAClass v_cr = this.SelectedWidget.Parent;

            var v_table = IGK.ICore.Xml.CoreXmlElement.CreateXmlNode("table");
            v_table["style"] = "width:100%;";
            AndroidAttributeDefinition v_def = AndroidAttributeDefinitionTool.Instance.GetDefinition(
this.SelectedWidget.Name.LastSegment('.'));
            List<AndroidAttributeDefinition> v_attrdef = new List<AndroidAttributeDefinition>();
            if (v_def != null)
            {

                LoadDef(v_attrdef, v_def);
            }


            while (v_cr!=null)
            {
                var a = r.Add("li").Add("a");
                a["href"] = "#";
                a["onclick"] = "if(window.external){window.external.jsGetDescription('"+v_cr.Name+"')}";
                a.Content = v_cr.Name;


                v_def = AndroidAttributeDefinitionTool.Instance.GetDefinition(
v_cr.Name.LastSegment('.'));
                if (v_def != null)
                {
                    LoadDef(v_attrdef, v_def);
                }
                v_cr = v_cr.Parent;
            }

            sb.AppendLine(r.RenderXML(null));

            sb.Append("<h3>TargetPlatform</h3>");
            sb.Append("<div>"+this.TargetInfo.GetVersion().ToString()+"</div>");
            sb.Append("<h3>attributes</h3>");
            //get definition
            v_attrdef.Sort((Comparison<AndroidAttributeDefinition>)(( (o1,o2)=>{
                return o1.Name.LastSegment ('.').CompareTo (o2.Name.LastSegment ('.'));
            })));
            int i = 1;
            foreach (var item in v_attrdef)
            {
                var tr = v_table.Add("tr");
                tr["class"]="row_"+(((i%2)==0)?"dark":"light");
                tr.Add("td").Content = item.Name;
                tr.Add("td").Content = item.Format;
                tr.Add("td").Content = item.GetValues();
                tr.Add("td").Content = item.GetDescription();
                if (i ==1)
                    i = 2;
                else i = 1;
            }
            sb.AppendLine(v_table.RenderXML(null));
            return sb.ToString() ;
        }

        private void LoadDef(List<AndroidAttributeDefinition> r, AndroidAttributeDefinition v_def)
        {

            foreach (AndroidAttributeDefinition item in v_def.Childs)
            {
                if (item != null)
                {
                    r.Add(item);
                }
            }
        }

        void c_lsb_widget_SelectedIndexChanged(object sender, EventArgs e)
        {
            AndroidJavaClassViewItem c = this.c_lsb_widget.SelectedItem as
                AndroidJavaClassViewItem;

            if (c != null)
            {
                this.SelectedWidget  = c.Widget;
            }
            else
                this.SelectedWidget = null;
        }

        private void _targetinfo_Changed(object sender, EventArgs e)
        {
            if (this.IsHandleCreated)
            {
                InitInfo();
                this.SelectedWidget = null;
            }
        }

        private void InitInfo()
        {
            if (this.TargetInfo != null)
            {
                JAVAClass[] android = AndroidSystemManager.GetWidgets(this.TargetInfo);
                this.c_lsb_widget.Items.Clear();
                if (android != null)
                {
                    foreach (JAVAClass item in android)
                    {
                        this.c_lsb_widget.Items.Add(new AndroidJavaClassViewItem(item));
                    }
                    this.c_lsb_widget.Sorted = true;
                }
            }

        }

        private void InitializeComponent()
        {
            this.c_lsb_widget = new System.Windows.Forms.ListBox();
            this.c_webbrowser = new System.Windows.Forms.WebBrowser();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.SuspendLayout();
            // 
            // c_lsb_widget
            // 
            this.c_lsb_widget.Dock = System.Windows.Forms.DockStyle.Left;
            this.c_lsb_widget.FormattingEnabled = true;
            this.c_lsb_widget.IntegralHeight = false;
            this.c_lsb_widget.Location = new System.Drawing.Point(0, 0);
            this.c_lsb_widget.Name = "c_lsb_widget";
            this.c_lsb_widget.Size = new System.Drawing.Size(216, 361);
            this.c_lsb_widget.TabIndex = 0;
            // 
            // c_webbrowser
            // 
            this.c_webbrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c_webbrowser.IsWebBrowserContextMenuEnabled = false;
            this.c_webbrowser.Location = new System.Drawing.Point(219, 0);
            this.c_webbrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.c_webbrowser.Name = "c_webbrowser";
            this.c_webbrowser.ScrollBarsEnabled = true;
            this.c_webbrowser.Size = new System.Drawing.Size(464, 361);
            this.c_webbrowser.TabIndex = 1;
            this.c_webbrowser.WebBrowserShortcutsEnabled = false;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(216, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 361);
            this.splitter1.TabIndex = 2;
            this.splitter1.TabStop = false;
            // 
            // AndriodWidgetControl
            // 
            this.Controls.Add(this.c_webbrowser);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.c_lsb_widget);
            this.Name = "AndriodWidgetControl";
            this.Size = new System.Drawing.Size(683, 361);
            this.ResumeLayout(false);

        }
        [Serializable()]
         [ComVisible(true)]
        public sealed  class JSFunction {
             private AndroidWidgetControl c_owner;
             public JSFunction(AndroidWidgetControl ctrl)
             {
                 this.c_owner = ctrl;
             }
             public  JSFunction() { 
             }
            public void jsGetDescription(string name) {
                this.c_owner.SelectItem(name);
            }
        }

         internal void SelectItem(string name)
         {
             foreach (AndroidJavaClassViewItem item in this.c_lsb_widget.Items)
             {
                 if (item.Widget.Name == name)
                 {
                     this.c_lsb_widget.SelectedItem = item;
                     break;
                 }
             }
         }
    }
}
