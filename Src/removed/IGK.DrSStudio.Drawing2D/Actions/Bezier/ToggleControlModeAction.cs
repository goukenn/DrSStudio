

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ToggleControlModeAction.cs
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
file:ToggleControlModeAction.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.Drawing2D.Actions.Bezier
{
    class ToggleControlModeAction : BezierActionBase 
    {
        protected override bool PerformAction()
        {
            switch (this.Mecanism.ControlMode)
            {
                case enuBezierPathControlMode.Free:
                    this.Mecanism.ControlMode = enuBezierPathControlMode.StartPoint;
                    break;
                case enuBezierPathControlMode.StartPoint:
                    this.Mecanism.ControlMode = enuBezierPathControlMode.EndPoint;
                    break;
                case enuBezierPathControlMode.EndPoint:
                    this.Mecanism.ControlMode = enuBezierPathControlMode.CenterPoint;
                    break;
                case enuBezierPathControlMode.CenterPoint:
                    this.Mecanism.ControlMode = enuBezierPathControlMode.BothMiddle;
                    break;
                case enuBezierPathControlMode.BothMiddle:
                    this.Mecanism.ControlMode = enuBezierPathControlMode.Free;
                    break;
                default:
                    this.Mecanism.ControlMode = enuBezierPathControlMode.Free;
                    break;
            }
            return true;
        }
    }
}

