

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKXNumericTextBox.cs
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
file:IGKXNumericTextBox.cs
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
﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
namespace IGK.ICore.WinUI
{
    [DefaultEvent("ValueChanged")]
    [DefaultProperty("Value")]
    [Designer(typeof(ControlDesigner))]
    /// <summary>
    /// represent text box used to edit numerical value
    /// </summary>
    public class IGKXNumericTextBox : IGKXTextBox
    {
        readonly char separator = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator[0];
        private decimal m_value;
        private bool m_allowDecimalValue;
        private bool m_AllowNegativeValue;
        [DefaultValue(false)]
        public bool AllowNegativeValue
        {
            get { return m_AllowNegativeValue; }
            set
            {
                if (m_AllowNegativeValue != value)
                {
                    m_AllowNegativeValue = value;
                }
            }
        }
        [DefaultValue(false)]
        public bool AllowDecimalValue
        {
            get
            {
                return this.m_allowDecimalValue;
            }
            set
            {
                this.m_allowDecimalValue = value;
                if ((value == false) && (this.Text.Contains(separator.ToString())))
                {
                    this.Text = this.Text.Remove(this.Text.IndexOf(separator), 1);
                }
            }
        }
        protected override bool IsInputKey(System.Windows.Forms.Keys keyData)
        {
            return base.IsInputKey(keyData);
        }
        protected override bool IsInputChar(char charCode)
        {
            return base.IsInputChar(charCode);
        }
        public decimal Value
        {
            get
            {
                return m_value;
            }
            set
            {
                m_value = value;
                this.Text = this.m_value.ToString();
                OnvalueChanged(EventArgs.Empty);
            }
        }
        public event EventHandler ValueChanged;
        private void OnvalueChanged(EventArgs eventArgs)
        {
            if (ValueChanged != null)
                ValueChanged(this, eventArgs);
        }
        protected override void OnTextChanged(EventArgs e)
        {
            decimal o = 0.0M;
            if (this.Text.Length > 0)
            {
                if (decimal.TryParse(this.Text, out o))
                {
                    if (m_value != o)
                    {
                        m_value = o;
                        OnvalueChanged(EventArgs.Empty);
                    }
                    base.OnTextChanged(e);
                }
                else
                {
                    this.Text = m_value.ToString();
                }
            }
        }
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            int v_ss = SelectionStart;
            bool v_allowDecimal = AllowDecimalValue;
            if ((e.KeyChar == '-')&&(this.AllowNegativeValue ))
            {
                if (this.Text.StartsWith("-"))
                {//remove minus simbol
                    this.Text = this.Text.Replace("-", "");
                    this.SelectionStart = v_ss- 1;
                }
                else {
                    this.Text = "-" + this.Text;
                    this.SelectionStart = v_ss + 1;
                }
                e.Handled = true;
                return;
            }
            if (char.IsDigit(e.KeyChar))
            {
                if (this.TextLength == 1)
                {
                    if (this.Text == "0")
                    {
                        this.Text = e.KeyChar.ToString();
                        this.SelectionStart = 1;
                        e.Handled = true;
                        return;
                    }
                }
                else if (this.Text.StartsWith("0."))
                {
                    if (v_allowDecimal)
                    {
                        if ((v_ss == 1) && (e.KeyChar != '0'))
                        {
                            string v = this.Text;
                            v = v.Substring(1);
                            this.Text = e.KeyChar + v;
                            this.SelectionStart = 1;
                            e.Handled = true;
                        }
                    }
                    else
                    {
                        //currently impossible
                        e.Handled = true;
                    }
                }
            }
            else
            {
                string decimalchar = System.Threading .Thread.CurrentThread.CurrentUICulture.NumberFormat.NumberDecimalSeparator;
                switch (e.KeyChar)
                {
                    case '\b'://back space
                        if (this.TextLength == 1)
                        {
                            this.Text = "0";
                            this.SelectionStart = 1;
                            e.Handled = true;
                        }
                        else if ((v_ss == 1) && (Text.IndexOf(".") == 1))
                        {
                            string v = this.Text;
                            v = v.Substring(1);
                            this.Text = "0" + v;
                            this.SelectionStart = 1;
                            e.Handled = true;
                        }
                        break;
                    case '.':
                        e.Handled = !v_allowDecimal || (v_allowDecimal && this.Text.Contains("."));
                        break;
                    default:
                        if (e.ToString() == decimalchar)
                        {
                            e.Handled = !v_allowDecimal || (v_allowDecimal && this.Text.Contains(decimalchar));
                        }
                        e.Handled = true;
                        break;
                }
            }
            base.OnKeyPress(e);
        }
        //.ctr
        public IGKXNumericTextBox()
        {
            this.Text = "0";
            this.HideSelection = true;
            this.MaxLength = 10;
        }
        class ControlDesigner : System.Windows.Forms.Design.ControlDesigner 
        {
            protected override void PreFilterProperties(System.Collections.IDictionary properties)
            {
                properties.Remove("Text");
                properties.Remove("Lines");
                properties.Remove("PasswordChar");
                base.PreFilterProperties(properties);
            }
        }
    }
}

