

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKXExpenderBoxGroupItem.cs
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
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace IGK.ICore.WinCore.WinUI
{
    /// <summary>
    /// represent the base group
    /// </summary>
    public class IGKXExpenderBoxGroupItem : IGKXExpenderBoxItem 
    {
        [Browsable(false)]
        [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden)]
        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
            }
        }
    
        protected override System.Drawing.Size DefaultSize
        {
            get
            {
                return new System.Drawing.Size(100, 18);
            }
        }
        public IGKXExpenderBoxGroupItem():base()
        {
            
            this.TabStop = false;
            CoreMouseStateManager.Register(this);
            this.Paint += _Paint;
        }
        
        IGKXExpenderBoxRenderer Renderer {
            get {
                if (this.ParentGroup != null)
                {
                    var e = this.ParentGroup.ExpenderBox;
                    if (e!=null)
                        return e.Renderer ;
                }
                return null;
            }
        }
    
        void _Paint(object sender, CorePaintEventArgs e)
        {
            var r = Renderer ;
            if (r!=null)
                r.RenderBoxItem (this,  e);

        }   
        
    }
}
