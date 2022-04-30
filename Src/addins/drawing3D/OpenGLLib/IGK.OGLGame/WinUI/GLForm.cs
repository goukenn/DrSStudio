

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GLForm.cs
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
file:GLForm.cs
*/
/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igkdev.be
Application : DrSStudio
powered by IGK - DEV &copy; 2008-2012
THIS FILE IS A PART OF THE DRSSTUDIO APPLICATION. SEE "License.txt"
FOR MORE INFORMATION ABOUT THE LICENSE
------------------------------------------------------------------- 
*/
/* 
-------------------------------------------------------------
This file is part of iGK-DEV-DrawingStudio
-------------------------------------------------------------
-------------------------------------------------------------
-------------------------------------------------------------
view license file in Documentation folder to get more info
Copyright (c) 2008-2010 
Author  : Charles A.D. BONDJE DOUE 
mail : bondje.doue@hotmail.com
-------------------------------------------------------------
-------------------------------------------------------------
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace IGK.OGLGame.WinUI
{
    
    using IGK.ICore;
    using IGK.OGLGame.Input ;
    using IGK.OGLGame.Graphics.Native;
    using IGK.OGLGame.WinUI;
    public partial class GLForm : Form
    {
        internal const int CS_NOBORDER = 0x0000;
        internal const int  CS_VREDRAW   =  0x0001;
        internal const int  CS_HREDRAW          =0x0002;
        internal const int  CS_DBLCLKS          =0x0008;
        internal const int  CS_OWNDC            =0x0020;
        internal const int  CS_CLASSDC          =0x0040;
        internal const int  CS_PARENTDC         =0x0080;
        internal const int  CS_NOCLOSE          =0x0200; //disable the close button - and remove "close" from sys menu
        internal const int  CS_SAVEBITS         =0x0800;
        internal const int  CS_BYTEALIGNCLIENT = 0x1000;
        internal const int  CS_BYTEALIGNWINDOW=  0x2000;
        internal const int  CS_GLOBALCLASS   =   0x4000;
        internal const int  CS_IME          =    0x00010000;
        #if _WIN32_VISTA_FAMILLY
        internal const int  CS_DROPSHADOW  =     0x00020000;
        #endif
        private GLGameWindow m_GameWindowHost;
        public GLGameWindow GameWindow { get { return this.m_GameWindowHost; } }
        internal GLForm(GLGameWindow game):base()
        {
            this.InitializeComponent();
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.Opaque, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, false    );
            this.SetStyle(ControlStyles.ResizeRedraw, true);
			this.DoubleBuffered = false ;
            this.StartPosition = FormStartPosition.CenterScreen;            
            this.m_GameWindowHost = game;  
        }
		protected override void OnPaint (PaintEventArgs e)
		{
			 this.m_GameWindowHost.Game.Tick ();
		}
		protected override void OnPaintBackground (PaintEventArgs e)
		{
		}
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GLForm));
            this.SuspendLayout();
            // 
            // GLForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Name = "GLForm";
            this.ResumeLayout(false);
        }
		protected override void WndProc (ref Message m)
		{
			switch(m.Msg)
			{
                case 0x216://moving
                    this.m_GameWindowHost.Game.Tick();
                    break;
			    case 0x14: //filter message
			    {
				    return ;
			    }
			}
			 base.WndProc (ref m);
		}
    }
}

