

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: InkSinkPlugin.cs
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
file:InkSinkPlugin.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.StylusInput ;
namespace IGK.DrSStudio.Drawing2D.Ink
{
    /// <summary>
    /// represent a plugin
    /// </summary>
    class InkSinkPlugin : IStylusSyncPlugin 
    {
        public void CustomStylusDataAdded(RealTimeStylus sender, Microsoft.StylusInput.PluginData.CustomStylusData data)
        {
        }
        public virtual DataInterestMask DataInterest
        {
            get { return DataInterestMask.Packets | DataInterestMask.StylusButtonDown | DataInterestMask.StylusButtonUp | DataInterestMask.StylusDown | DataInterestMask.StylusDown; }
        }
        public void Error(RealTimeStylus sender, Microsoft.StylusInput.PluginData.ErrorData data)
        {
        }
        public void InAirPackets(RealTimeStylus sender, Microsoft.StylusInput.PluginData.InAirPacketsData data){           
        }
        public virtual void Packets(RealTimeStylus sender, Microsoft.StylusInput.PluginData.PacketsData data)
        {            
        }
        public void RealTimeStylusDisabled(RealTimeStylus sender, Microsoft.StylusInput.PluginData.RealTimeStylusDisabledData data)
        {
        }
        public void RealTimeStylusEnabled(RealTimeStylus sender, Microsoft.StylusInput.PluginData.RealTimeStylusEnabledData data){            
        }
        public virtual void StylusButtonDown(RealTimeStylus sender, Microsoft.StylusInput.PluginData.StylusButtonDownData data){            
        }
        public virtual void StylusButtonUp(RealTimeStylus sender, Microsoft.StylusInput.PluginData.StylusButtonUpData data)
        {
        }
        public virtual void StylusDown(RealTimeStylus sender, Microsoft.StylusInput.PluginData.StylusDownData data)
        {
        }
        public virtual void StylusInRange(RealTimeStylus sender, Microsoft.StylusInput.PluginData.StylusInRangeData data)
        {
        }
        public void StylusOutOfRange(RealTimeStylus sender, Microsoft.StylusInput.PluginData.StylusOutOfRangeData data)
        {
        }
        public void StylusUp(RealTimeStylus sender, Microsoft.StylusInput.PluginData.StylusUpData data)
        {
        }
        public void SystemGesture(RealTimeStylus sender, Microsoft.StylusInput.PluginData.SystemGestureData data)
        {
        }
        public void TabletAdded(RealTimeStylus sender, Microsoft.StylusInput.PluginData.TabletAddedData data)
        {
        }
        public void TabletRemoved(RealTimeStylus sender, Microsoft.StylusInput.PluginData.TabletRemovedData data)
        {
        }
    }
}

