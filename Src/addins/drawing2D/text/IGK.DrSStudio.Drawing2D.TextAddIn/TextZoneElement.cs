

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: TextZoneElement.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/

using IGK.ICore;
using IGK.ICore.Codec;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.DrSStudio;
using IGK.DrSStudio.Menu;
using IGK.DrSStudio.WinUI;
using IGK.DrSStudio.Drawing2D;
using IGK.DrSStudio.Drawing2D.WinUI;
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Drawing2D
{
    //[Core2DDrawingGroupElement("TextZone",
    //    CoreConstant.GROUP_TEXT , 
    //    typeof(Mecanism))]
    //public class TextZoneElement : RectangleElement, ICoreTextValueElement, ICore2DDrawingVisitable
    //{
    //    private string m_Text;
    //    private CoreFont m_Font;
    //    private ICoreBrush m_FontBrush;

    //    [CoreXMLAttribute ()]
    //    public ICoreBrush FontBrush
    //    {
    //        get { return m_FontBrush; }
           
    //    }

    //    public CoreFont Font
    //    {
    //        get { return m_Font; }
          
    //    }
    //    [CoreXMLElement]
    //    public string Text
    //    {
    //        get { return m_Text; }
    //        set
    //        {
    //            if (m_Text != value)
    //            {
    //                m_Text = value;
    //                OnPropertyChanged(Core2DDrawingChangement.Definition );
    //            }
    //        }
    //    }
    //    public TextZoneElement()
    //    {
    //        this.m_FontBrush = new CoreBrush(this);
    //        this.m_Font = CoreFont.CreateFont(this, CoreConstant.DEFAULT_FONT_NAME);
    //        this.m_Font.FontSize = 12; 
    //        this.Text = "Sample text zone";
    //        this.m_Font.HorizontalAlignment = enuStringAlignment.Center;
    //        this.m_Font.VerticalAlignment = enuStringAlignment.Center;
    //        this.m_Font.FontDefinitionChanged += m_Font_FontDefinitionChanged;
    //        this.m_FontBrush.BrushDefinitionChanged += m_FontBrush_BrushDefinitionChanged;
    //    }

    //    void m_FontBrush_BrushDefinitionChanged(object sender, EventArgs e)
    //    {
    //        this.OnPropertyChanged(Core2DDrawingChangement.Brush);
    //    }

    //    void m_Font_FontDefinitionChanged(object sender, EventArgs e)
    //    {
    //        OnPropertyChanged(Core2DDrawingChangement.FontChanged);
    //    }

       

    //    public bool Accept(ICore2DDrawingVisitor visitor)
    //    {
    //        return visitor != null;
    //    }

    //    public void Visit(ICore2DDrawingVisitor visitor)
    //    {
    //        object obj = visitor.Save();
    //        visitor.SetupGraphicsDevice(this);
           
    //        CoreGraphicsPath p = this.GetPath ();

           
    //        visitor.FillPath(this.FillBrush, p);

    //        object sobj = visitor.Save();

    //        visitor.MultiplyTransform(this.GetMatrix(), enuMatrixOrder.Prepend );
    //        visitor.DrawString(this.Text,
    //            this.m_Font,
    //            this.m_FontBrush,
    //            this.Bounds);
    //        visitor.Restore(sobj);

    //        visitor.DrawPath(this.StrokeBrush, p);
    //        visitor.Restore(obj);
    //    }

    //    public new class Mecanism : RectangleElement.Mecanism
    //    {
    //        IXTextControl c_textControl;
    //        public new TextZoneElement Element {
    //            get {
    //                return base.Element as TextZoneElement;
    //            }
    //        }
    //        public Mecanism():base()
    //        {

    //        }
    //        public override bool Register(ICore2DDrawingSurface t)
    //        {
    //            if (base.Register(t))
    //            {
    //                this.c_textControl = CoreControlFactory.CreateControl("IGKXTextBox")
    //                    as IXTextControl;
    //                InitControlProperties();
    //                t.Controls.Add(this.c_textControl);
    //                return true;
    //            }

    //            return false ;
    //        }

    //        private void InitControlProperties()
    //        {

                
    //            this.c_textControl.Multiline = true;
    //            this.c_textControl.TextChanged += _textChanged;      
    //        }
    //        private void _textChanged(object o, EventArgs e)
    //        {
    //            if (this.Element !=null)
    //            this.Element.Text = this.c_textControl.Text;
    //        }
    //        public override bool UnRegister()
    //        {
    //            this.CurrentSurface.Controls.Remove(this.c_textControl);
    //            return base.UnRegister();
    //        }
    //        public override void Dispose()
    //        {
    //            if (this.c_textControl !=null)
    //            {
    //                this.c_textControl.Dispose();
    //                this.c_textControl  = null;
    //            }
    //            base.Dispose();
    //        }

    //        protected override void GenerateActions()
    //        {
    //            base.GenerateActions();
    //            //replace the mecanism action
    //            this.Actions[enuKeys.Escape] = new TextZoneEspaceAction(this);
    //        }
    //        protected override void GenerateSnippets()
    //        {
    //            base.GenerateSnippets();
    //        }
    //        protected override void RegisterElementEvent(RectangleElement element)
    //        {
    //            base.RegisterElementEvent(element);
    //        }
    //        protected override void OnElementPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs e)
    //        {
    //            if (e.ID == Core2DDrawingChangement.FontChanged.ID)
    //            {
    //                SetControlElementFont();
    //            }
    //            base.OnElementPropertyChanged(e);
    //        }
    //        protected override void OnElementChanged(CoreWorkingElementChangedEventArgs<RectangleElement> e)
    //        {
    //            base.OnElementChanged(e);
              
    //                this.c_textControl.Visible = (this.Element != null);
               
    //        }
    //        protected override void InitSnippetsLocation()
    //        {
    //            base.InitSnippetsLocation();
    //            this.c_textControl.Bounds = this.CurrentSurface.GetScreenBound(this.Element.Bounds);
    //            this.c_textControl.Text = this.Element.Text;
    //            this.SetControlElementFont();
    //        }

    //        private void SetControlElementFont()
    //        {

    //            ICoreFont f = this.Element.Font;
    //            float fsize = Math.Abs (f.FontSize * this.CurrentSurface.ZoomY );
    //            this.c_textControl.SetFont(this.Element.Font, fsize, f.FontStyle);
    //        }
            
    //    }
    //}



}
