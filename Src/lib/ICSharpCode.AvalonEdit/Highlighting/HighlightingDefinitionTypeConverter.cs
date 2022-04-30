

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: HighlightingDefinitionTypeConverter.cs
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
file:HighlightingDefinitionTypeConverter.cs
*/
// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under the GNU LGPL (for details please see \doc\license.txt)
using System;
using System.ComponentModel;
using System.Globalization;
namespace ICSharpCode.AvalonEdit.Highlighting
{
	/// <summary>
	/// Converts between strings and <see cref="IHighlightingDefinition"/> by treating the string as the definition name
	/// and calling <c>HighlightingManager.Instance.<see cref="HighlightingManager.GetDefinition">GetDefinition</see>(name)</c>.
	/// </summary>
	public sealed class HighlightingDefinitionTypeConverter : TypeConverter
	{
		/// <inheritdoc/>
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			if (sourceType == typeof(string))
				return true;
			else
				return base.CanConvertFrom(context, sourceType);
		}
		/// <inheritdoc/>
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			string definitionName = value as string;
			if (definitionName != null)
				return HighlightingManager.Instance.GetDefinition(definitionName);
			else
				return base.ConvertFrom(context, culture, value);
		}
		/// <inheritdoc/>
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			if (destinationType == typeof(string))
				return true;
			else
				return base.CanConvertTo(context, destinationType);
		}
		/// <inheritdoc/>
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			IHighlightingDefinition definition = value as IHighlightingDefinition;
			if (definition != null && destinationType == typeof(string))
				return definition.Name;
			else
				return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}

