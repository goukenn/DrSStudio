

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CircleTextEditor.cs
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
file:CircleTextEditor.cs
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
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IGK.ICore;using IGK.DrSStudio.WinUI;
namespace IGK.DrSStudio.Drawing2D.WinUI
{
    public partial class CircleTextEditor : UIXConfigControlBase
    {
        private CircleTextElement m_element;
        private bool m_configure;
        public CircleTextEditor(CircleTextElement element)
        {
            InitializeComponent();
            this.m_element = element;
            this.InitControl();
        }
        private void InitControl()
        {
            this.m_configure = true;
            foreach (CircleTextOrientation o in Enum.GetValues(typeof(CircleTextOrientation)))
            {
                this.c_cmb_orientation.Items.Add(o);
            }
            this.c_cmb_orientation.SelectedIndex = 0;
            this.c_cmb_orientation.DropDownStyle = ComboBoxStyle.DropDownList;
            this.txb_text.Text = this.m_element.Text;
            this.c_xnumtxb_scaley.Value = new decimal (this.m_element.ScaleY);
            this.c_xnumtxb_scalex.Value = new decimal (this.m_element.ScaleX);
            this.c_cmb_orientation.SelectedItem = this.m_element.Orientation;            
            this.c_num_PortionAngle.Value = (Decimal)this.m_element.PortionAngle;
            this.c_numoffsetAngle.Value = (Decimal)this.m_element.OffsetAngle;
            this.c_num_orientationAngle.Value = (Decimal)this.m_element.OrientationAngle;
            this.txb_text.TextChanged += new EventHandler(txb_text_TextChanged);
            this.c_numoffsetAngle.ValueChanged += new EventHandler(c_numoffsetAngle_ValueChanged);
            this.c_num_PortionAngle.ValueChanged += new EventHandler(c_num_PortionAngle_ValueChanged);
            this.c_cmb_orientation.SelectedIndexChanged += new EventHandler(c_cmb_orientation_SelectedIndexChanged);
            this.c_num_orientationAngle.ValueChanged += new EventHandler(c_num_orientationAngle_ValueChanged);
            this.c_xnumtxb_scalex.ValueChanged += new EventHandler(c_xnumtxb_scalex_ValueChanged);
            this.c_xnumtxb_scaley.ValueChanged += new EventHandler(c_xnumtxb_scaley_ValueChanged);
            this.m_configure = false;
        }
        void c_xnumtxb_scaley_ValueChanged(object sender, EventArgs e)
        {
            if (this.m_configure) return;
            this.m_element.ScaleY = Convert.ToSingle(this.c_xnumtxb_scaley.Value);
        }
        void c_xnumtxb_scalex_ValueChanged(object sender, EventArgs e)
        {
            if (this.m_configure) return;
            this.m_element.ScaleX = Convert.ToSingle(this.c_xnumtxb_scalex.Value);
        }
        void c_num_orientationAngle_ValueChanged(object sender, EventArgs e)
        {
            if (this.m_configure) return;
            this.m_element.Invalidate(false);
            this.m_element.OrientationAngle = Convert.ToSingle (this.c_num_orientationAngle.Value);
            this.m_element.Invalidate(true);
        }
        void c_cmb_orientation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_configure) return;
            this.m_element.Invalidate(false);
            this.m_element.Orientation = (CircleTextOrientation)this.c_cmb_orientation.SelectedItem;
            this.m_element.Invalidate(true);
        }
        void c_num_PortionAngle_ValueChanged(object sender, EventArgs e)
        {
            if (this.m_configure) return;
            this.m_element.Invalidate(false);
            this.m_element .PortionAngle = Convert.ToSingle (this.c_num_PortionAngle.Value);
            this.m_element.Invalidate(true );
        }
        void c_numoffsetAngle_ValueChanged(object sender, EventArgs e)
        {
            if (this.m_configure) return;
            this.m_element.Invalidate(false);
            this.m_element.OffsetAngle  = Convert.ToSingle(this.c_numoffsetAngle .Value);
            this.m_element.Invalidate(true);
        }
        void txb_text_TextChanged(object sender, EventArgs e)
        {
            if (this.m_configure) return;
            this.m_configure = true;
            this.m_element.Text = this.txb_text.Text;
            this.m_configure = false;
        }
    }
}

