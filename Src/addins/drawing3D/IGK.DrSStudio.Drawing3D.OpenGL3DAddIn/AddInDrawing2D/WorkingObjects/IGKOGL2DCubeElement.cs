

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKOGL2DCubeElement.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/

using IGK.ICore;
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
using IGK.ICore.Codec;
using IGK.ICore.WinUI.Configuration;
using IGK.GLLib;
using IGK.OGLGame.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.DrSStudio.Drawing3D;

namespace IGK.DrSStudio.Drawing2D.OpenGL
{
    [IGKOGL2DItemAttribute(
        "Cube", 
        typeof(Mecanism),
        ImageKey= OGLImageKeys.DE_CUBE_GKDS)]
    /// <summary>
    /// represent a cube element in a working object
    /// </summary>
    public class IGKOGL2DCubeElement : IGKOGL2DWorkingObjectBase 
    {
        private Vector3f m_Size;
        [CoreConfigurableProperty()]
        [CoreXMLAttribute()]
        public Vector3f Size
        {
            get { return m_Size; }
            set
            {
                if (m_Size != value)
                {
                    m_Size = value;
                    OnPropertyChanged(Core2DDrawingChangement.Definition);
                }
            }
        }
        protected override void Visit(IIGKOGL2DDrawingDeviceVisitor visitor)
        {
            Rectanglei rc = Rectanglei.Round(this.Bounds);
            if (rc.IsEmpty)
                return;
            object obj = visitor.Save();            
            this.Setting.BindSetting(visitor);
            
            visitor.Device.SetColor(FillBrush.Colors[0]);            
            visitor.Device.DrawSolidCube(Vector3f.Zero, new Vector3f( 1.0f));           
            visitor.Device.SetColor(StrokeBrush.Colors[0]);
            visitor.Device.DrawWiredCube(Vector3f.Zero, new Vector3f(1.0f));
            this.Setting.UnbindSetting(visitor);
            visitor.Restore(obj);
        }

        public new class Mecanism : RectangleElement.Mecanism 
        { 

        }
      
    }
}