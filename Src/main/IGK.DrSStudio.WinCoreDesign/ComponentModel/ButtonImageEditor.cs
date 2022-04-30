

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ButtonImageEditor.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore;
using IGK.ICore.Codec;
using IGK.ICore.Drawing2D;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGK.DrSStudio.WinUI.Design
{
    class ButtonImageEditor : System.Drawing.Design.ImageEditor
    {
        public ButtonImageEditor()
            : base()
        {
        }
        protected override Type[] GetImageExtenders()
        {
            return base.GetImageExtenders();
        }
        public override System.Drawing.Design.UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return System.Drawing.Design.UITypeEditorEditStyle.Modal;
        }
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "gkds pictures files | *.gkds; *.png;";
                if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    try
                    {
                        ICoreWorkingDocument[] c = CoreDecoder.Instance.GetDocuments(System.IO.File.ReadAllBytes(ofd.FileName));
                        ICore2DDrawingDocument[] docs = null;// (c);
                        CoreButtonDocument doc = CoreButtonDocument.Create(docs);
                        return doc;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message + "\n" + ex.StackTrace, "Error in XBUTTON");
                    }
                }
            }
            return null;
        }
        protected override string GetFileDialogDescription()
        {
            return "Drs Pictures files";
        }
        protected override string[] GetExtensions()
        {
            return new string[] { "gkds", "png" };
        }
        protected override Image LoadFromStream(System.IO.Stream stream)
        {
            return base.LoadFromStream(stream);
        }
    }
     
}
