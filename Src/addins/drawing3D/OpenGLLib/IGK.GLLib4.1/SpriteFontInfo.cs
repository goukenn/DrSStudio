

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: SpriteFontInfo.cs
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
file:SpriteFontInfo.cs
*/
using System;
namespace IGK.GLLib
{
	/// <summary>
	/// Sprite font info.
	/// </summary>
	public class SpriteFontInfo : ISpriteFontInfo 
	{
		private string m_name;
		public string Name{get{return this.m_name;} internal set{this.m_name = value;}}
		public SpriteFontInfo ()
		{
		}
	}
}

