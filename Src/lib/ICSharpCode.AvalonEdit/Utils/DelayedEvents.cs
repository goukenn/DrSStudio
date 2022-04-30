

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: DelayedEvents.cs
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
file:DelayedEvents.cs
*/
// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under the GNU LGPL (for details please see \doc\license.txt)
using System;
using System.Collections.Generic;
namespace ICSharpCode.AvalonEdit.Utils
{
	/// <summary>
	/// Maintains a list of delayed events to raise.
	/// </summary>
	sealed class DelayedEvents
	{
		struct EventCall
		{
			EventHandler handler;
			object sender;
			EventArgs e;
			public EventCall(EventHandler handler, object sender, EventArgs e)
			{
				this.handler = handler;
				this.sender = sender;
				this.e = e;
			}
			public void Call()
			{
				handler(sender, e);
			}
		}
		Queue<EventCall> eventCalls = new Queue<EventCall>();
		public void DelayedRaise(EventHandler handler, object sender, EventArgs e)
		{
			if (handler != null) {
				eventCalls.Enqueue(new EventCall(handler, sender, e));
			}
		}
		public void RaiseEvents()
		{
			while (eventCalls.Count > 0)
				eventCalls.Dequeue().Call();
		}
	}
}

