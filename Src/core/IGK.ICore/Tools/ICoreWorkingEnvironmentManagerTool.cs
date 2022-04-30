

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreWorkingEnvironmentManagerTool.cs
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
file:ICoreWorkingEnvironmentManagerTool.cs
*/
using System;
namespace IGK.ICore.Tools 
{
	public interface ICoreWorkingEnvironmentManagerTool : ICoreTool
	{
		/// <summary>
		/// Gets or sets the name of the environment.
		/// </summary>
		/// <value>
		/// The name of the environment.
		/// </value>
		string EnvironmentName{get;set;}
		/// <summary>
		/// Occurs when environment changed.
		/// </summary>
		event EventHandler EnvironmentChanged;
	}
}

