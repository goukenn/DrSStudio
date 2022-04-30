

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GridTool.cs
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
file:GridTool.cs
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
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
namespace IGK.DrSStudio.Drawing2D.GridManager
{
    using IGK.ICore.WinCore;
using IGK.ICore.Settings;
    using IGK.ICore.Tools;
    using IGK.ICore.WinUI;
    using IGK.ICore.Drawing2D.Tools;
    using IGK.ICore.WinUI.Configuration;
    using IGK.ICore.Drawing2D.WinUI;
    using IGK.ICore.GraphicModels;
    using IGK.ICore.Drawing2D;
    using IGK.ICore;
    using IGK.DrSStudio.Drawing2D.WinUI;
    [CoreTools("Tool.Drawing2D.Grid")]
    class GridTool : Core2DDrawingToolBase,
        ICoreWorkingConfigurableObject 
    {
        private static GridTool sm_instance;
        private GridSetting m_setting;
        private bool m_ShowGrid;
        private GridFrame m_GridFrames;

        class GridFrame : ICore2DDrawingFrameRenderer
        {
            private GridTool m_gridTool;

            private bool ShowGrid { get { return this.m_gridTool.ShowGrid; }}
            private ICore2DDrawingSurface CurrentSurface { get { return this.m_gridTool.CurrentSurface; } }
            public GridFrame(GridTool gridTool)
            {
                this.m_gridTool = gridTool;
            }
            public void Render(ICoreGraphics device)
            {
                if ((this.CurrentSurface == null) || (!ShowGrid))
                    return;
                Pen v_p = CoreBrushRegisterManager.GetPen<Pen>(GridRenderer.GridColor);
                //get large space
                float v_L = this.m_gridTool.m_setting.LargerSpace;
                //get smal space
                float v_S = this.m_gridTool.m_setting.SmallerSpace;

                v_p.DashStyle = DashStyle.Dot;
                DrawGrid(device, v_p,
                    this.CurrentSurface.GetZoom() * v_S);
                v_p.DashStyle = DashStyle.Solid;
                

                if (v_L > v_S)
                {
                    v_p.DashStyle = DashStyle.Solid;
                    DrawGrid(device, v_p, this.CurrentSurface.GetZoom() * v_L);
                }
            }
            private void DrawGrid(ICoreGraphics g, Pen line, float size)
            {
                float v_pixelMinSize = size;
                if (v_pixelMinSize <= 5.0f)
                    return;
                Rectanglef v_rcdoc = this.CurrentSurface.GetScreenBound(
                    new Rectanglef(Vector2f.Zero,
                    new Size2f(this.CurrentSurface.CurrentDocument.Width,
                        this.CurrentSurface.CurrentDocument.Height
                        )));
                //where to draw
                Rectanglef v_clipRc = v_rcdoc;
                Rectanglef v_display = this.CurrentSurface.DisplayArea;
                v_clipRc.Intersect(v_display);


                //g.DrawRectangle(Colorf.Red, v_clipRc.X, v_clipRc.Y, v_clipRc.Width , v_clipRc.Height);


                float v_offsetx = (-v_rcdoc.X + v_clipRc.X);
                float v_offsety = (-v_rcdoc.Y + v_clipRc.Y);
                float ZoomX = this.CurrentSurface.ZoomX;
                float ZoomY = this.CurrentSurface.ZoomY;
                float PosX = this.CurrentSurface.PosX;
                float PosY = this.CurrentSurface.PosY;
                float h = 0.0f;
                //float ex = this.CurrentSurface.PosX + (this.CurrentSurface.CurrentDocument.Width * ZoomX);
                //float ey = this.CurrentSurface.PosY + (this.CurrentSurface.CurrentDocument.Height * ZoomY);
                object v_state = g.Save();
                g.SmoothingMode = enuSmoothingMode.None;
                Rectanglef v_rc = new Rectanglef(PosX, PosY,
                    this.CurrentSurface.CurrentDocument.Width * ZoomX,
                this.CurrentSurface.CurrentDocument.Height * ZoomY);
                Pen vpn = line;
                g.IntersectClip(v_clipRc);
                g.ResetTransform();
                //g.TranslateTransform(v_offsetx, v_offsety, MatrixOrder.Append);
                //draw conventional grid according to one grid per pixel
                float end = v_clipRc.X + v_clipRc.Width;
                ////float h = (float)Math.Ceiling (v_offsetx % v_pixelMinSize);
                h = (float)Math.Floor(v_offsetx % v_pixelMinSize);
                for (float i = v_pixelMinSize + (v_clipRc.X) - h; i < end; i += v_pixelMinSize)
                {
                    if (i == 0) continue;
                    g.DrawLine(vpn,
                        i,
                        v_clipRc.Y,
                        i,
                        (v_clipRc.Y + v_clipRc.Height ));
                }
                end = v_clipRc.Y + v_clipRc.Height;
                h = (float)Math.Floor(v_offsety % v_pixelMinSize);
                for (float i = v_pixelMinSize + (v_clipRc.Y) - h; i < end; i += v_pixelMinSize)
                {
                    if (i == 0) continue;
                    g.DrawLine(vpn,
                        v_clipRc.X,
                        i,
                        (v_clipRc.X + (v_clipRc.Width)),
                        i);
                }
                vpn.DashStyle = DashStyle.Solid;
                g.ExcludeClip(v_clipRc);
                g.Restore(v_state);
            }
    
        }
        public bool ShowGrid
        {
            get { return m_ShowGrid; }
            set { this.m_ShowGrid  = value ;}
        }
        private GridTool()
        {
            this.m_setting = GridSetting.Instance;
            this.m_GridFrames = new GridFrame(this);
        }
        public static GridTool Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static GridTool()
        {
            sm_instance = new GridTool();
        }
        protected override void RegisterSurfaceEvent(ICore2DDrawingSurface surface)
        {
            base.RegisterSurfaceEvent(surface);
            var s = surface as IGKD2DDrawingSurface;
            if (s!=null)
            s.Scene.OverlayFrames.Add(this.m_GridFrames);
        }

       
        protected override void UnRegisterSurfaceEvent(ICore2DDrawingSurface surface)
        {
            var s = surface as IGKD2DDrawingSurface;
            if (s != null)
            s.Scene.OverlayFrames.Remove(this.m_GridFrames);
            base.UnRegisterSurfaceEvent(surface);
        }
        private void surface_Paint(object sender, CorePaintEventArgs e)
        {                   
          
        }      
        [CoreAppSetting(Name="GridTool")]
        internal sealed class GridSetting : CoreSettingBase
        {
            private static GridSetting sm_instance;
            private GridSetting():base()
            {
                this.SmallerSpace = 1;
                this.LargerSpace = 10;
            }
            public static GridSetting Instance
            {
                get
                {
                    return sm_instance;
                }
            }
            static GridSetting()
            {
                sm_instance = new GridSetting();
            }
            protected override void InitDefaultProperty(System.Reflection.PropertyInfo prInfo, CoreSettingDefaultValueAttribute attrib)
            {
                base.InitDefaultProperty(prInfo, attrib);
            }
            [CoreSettingDefaultValue(1)]
            public int SmallerSpace
            {
                get
                {
                    return (int)this["SmallerSpace"].Value;
                }
                set
                {
                    this["SmallerSpace"].Value = value;
                }
            }
            [CoreSettingDefaultValue(10)]
            public int LargerSpace
            {
                get
                {
                    return (int)this["LargerSpace"].Value;
                }
                set
                {
                    this["LargerSpace"].Value = value;
                }
            }
            
        }
        #region ICoreWorkingConfigurableObject Members
        public enuParamConfigType GetConfigType()
        {
            return enuParamConfigType.ParameterConfig;
        }
        public ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
        {
            ICoreParameterGroup group = parameters.AddGroup ("Size");
            group.AddItem ("SmallGrid", "SmallGrid",this.m_setting .SmallerSpace , enuParameterType .Text , SmallSizeChanged);
            group.AddItem("LargeGrid", "LargeGrid",this.m_setting.LargerSpace , enuParameterType.Text, LargeSizeChanged);
            return parameters;
        }
        void SmallSizeChanged(object o, CoreParameterChangedEventArgs e) {
            CoreUnit v_u = e.Value.ToString();
            this.m_setting.SmallerSpace =
                (int)Math.Ceiling (((ICoreUnitPixel )v_u ).Value );
            this.CurrentSurface.RefreshScene();
        }
        void LargeSizeChanged(object o, CoreParameterChangedEventArgs e)
        {
            CoreUnit v_u = e.Value.ToString();
            this.m_setting.LargerSpace  =
                           (int)Math.Ceiling (((ICoreUnitPixel)v_u).Value);
            this.CurrentSurface.RefreshScene();
        }
        public  ICoreControl GetConfigControl()
        {
            return null;
        }
        #endregion
    }
}

