

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IXMLSerializer.cs
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
file:IXMLSerializer.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.XPath;
namespace IGK.ICore.Codec
{
    public interface IXMLSerializer:
        ICoreDisposableObject 
    {
        //XmlWriter XmlWriter { get; }
        bool EmbedBitmap { get; set; }
        bool Error { get; set; }
        string ErrorMessage { get; set; }
        /// <summary>
        /// represent the serializer resources info
        /// </summary>
        ICoreResourceSerializer Resources { get; }
        /// <summary>
        /// get the serializer base dir. 
        /// </summary>
        string BaseDir { get; }
        // Summary:
        //     Gets the System.Xml.XmlWriterSettings object used to create this System.Xml.XmlWriter
        //     instance.
        //
        // Returns:
        //     The System.Xml.XmlWriterSettings object used to create this writer instance.
        //     If this writer was not created using the Overload:System.Xml.XmlWriter.Create
        //     method, this property returns null.
         XmlWriterSettings Settings { get; }
        //
        // Summary:
        //     When overridden in a derived class, gets the state of the writer.
        //
        // Returns:
        //     One of the System.Xml.WriteState values.
         WriteState WriteState { get; }
        //
        // Summary:
        //     When overridden in a derived class, gets the current xml:lang scope.
        //
        // Returns:
        //     The current xml:lang or null if there is no xml:lang in the current scope.
         string XmlLang { get; }
        //
        // Summary:
        //     When overridden in a derived class, gets an System.Xml.XmlSpace representing
        //     the current xml:space scope.
        //
        // Returns:
        //     An XmlSpace representing the current xml:space scope.Value Meaning NoneThis
        //     is the default if no xml:space scope exists. DefaultThe current scope is
        //     xml:space="default". PreserveThe current scope is xml:space="preserve".
         XmlSpace XmlSpace { get; }
        // Summary:
        //     When overridden in a derived class, closes this stream and the underlying
        //     stream.
        //
        // Exceptions:
        //   System.InvalidOperationException:
        //     A call is made to write more output after Close has been called or the result
        //     of this call is an invalid XML document.
         void Close();
        //
        // Summary:
        //     Creates a new System.Xml.XmlWriter instance using the specified System.IO.TextWriter.
        //
        // Parameters:
        //   output:
        //     The System.IO.TextWriter to which you want to write. The System.Xml.XmlWriter
        //     writes XML 1.0 text syntax and appends it to the specified System.IO.TextWriter.
        //
        // Returns:
        //     An System.Xml.XmlWriter object.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     The text value is null.
        //
        //
        // Summary:
        //     When overridden in a derived class, flushes whatever is in the buffer to
        //     the underlying streams and also flushes the underlying stream.
         void Flush();
        //
        // Summary:
        //     When overridden in a derived class, returns the closest prefix defined in
        //     the current namespace scope for the namespace URI.
        //
        // Parameters:
        //   ns:
        //     The namespace URI whose prefix you want to find.
        //
        // Returns:
        //     The matching prefix or null if no matching namespace URI is found in the
        //     current scope.
        //
        // Exceptions:
        //   System.ArgumentException:
        //     ns is either null or String.Empty.
         string LookupPrefix(string ns);
        //
        // Summary:
        //     When overridden in a derived class, writes out all the attributes found at
        //     the current position in the System.Xml.XmlReader.
        //
        // Parameters:
        //   reader:
        //     The XmlReader from which to copy the attributes.
        //
        //   defattr:
        //     true to copy the default attributes from the XmlReader; otherwise, false.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     reader is null.
        //
        //   System.Xml.XmlException:
        //     The reader is not positioned on an element, attribute or XmlDeclaration node.
         void WriteAttributes(XmlReader reader, bool defattr);
        //
        // Summary:
        //     When overridden in a derived class, writes out the attribute with the specified
        //     local name and value.
        //
        // Parameters:
        //   localName:
        //     The local name of the attribute.
        //
        //   value:
        //     The value of the attribute.
        //
        // Exceptions:
        //   System.InvalidOperationException:
        //     The state of writer is not WriteState.Element or writer is closed.
        //
        //   System.ArgumentException:
        //     The xml:space or xml:lang attribute value is invalid.
        void WriteAttributeString(string localName, string value);
        //
        // Summary:
        //     When overridden in a derived class, writes an attribute with the specified
        //     local name, namespace URI, and value.
        //
        // Parameters:
        //   localName:
        //     The local name of the attribute.
        //
        //   ns:
        //     The namespace URI to associate with the attribute.
        //
        //   value:
        //     The value of the attribute.
        //
        // Exceptions:
        //   System.InvalidOperationException:
        //     The state of writer is not WriteState.Element or writer is closed.
        //
        //   System.ArgumentException:
        //     The xml:space or xml:lang attribute value is invalid.
         void WriteAttributeString(string localName, string ns, string value);
        //
        // Summary:
        //     When overridden in a derived class, writes out the attribute with the specified
        //     prefix, local name, namespace URI, and value.
        //
        // Parameters:
        //   prefix:
        //     The namespace prefix of the attribute.
        //
        //   localName:
        //     The local name of the attribute.
        //
        //   ns:
        //     The namespace URI of the attribute.
        //
        //   value:
        //     The value of the attribute.
        //
        // Exceptions:
        //   System.InvalidOperationException:
        //     The state of writer is not WriteState.Element or writer is closed.
        //
        //   System.ArgumentException:
        //     The xml:space or xml:lang attribute value is invalid.
        void WriteAttributeString(string prefix, string localName, string ns, string value);
        //
        // Summary:
        //     When overridden in a derived class, encodes the specified binary bytes as
        //     Base64 and writes out the resulting text.
        //
        // Parameters:
        //   buffer:
        //     Byte array to encode.
        //
        //   index:
        //     The position in the buffer indicating the start of the bytes to write.
        //
        //   count:
        //     The number of bytes to write.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     buffer is null.
        //
        //   System.ArgumentException:
        //     The buffer length minus index is less than count.
        //
        //   System.ArgumentOutOfRangeException:
        //     index or count is less than zero.
         void WriteBase64(byte[] buffer, int index, int count);
        //
        // Summary:
        //     When overridden in a derived class, encodes the specified binary bytes as
        //     BinHex and writes out the resulting text.
        //
        // Parameters:
        //   buffer:
        //     Byte array to encode.
        //
        //   index:
        //     The position in the buffer indicating the start of the bytes to write.
        //
        //   count:
        //     The number of bytes to write.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     buffer is null.
        //
        //   System.ArgumentException:
        //     The buffer length minus index is less than count.
        //
        //   System.ArgumentOutOfRangeException:
        //     index or count is less than zero.
         void WriteBinHex(byte[] buffer, int index, int count);
        //
        // Summary:
        //     When overridden in a derived class, writes out a <![CDATA[...]]> block containing
        //     the specified text.
        //
        // Parameters:
        //   text:
        //     The text to place inside the CDATA block.
        //
        // Exceptions:
        //   System.ArgumentException:
        //     The text would result in a non-well formed XML document.
         void WriteCData(string text);
        //
        // Summary:
        //     When overridden in a derived class, forces the generation of a character
        //     entity for the specified Unicode character value.
        //
        // Parameters:
        //   ch:
        //     The Unicode character for which to generate a character entity.
        //
        // Exceptions:
        //   System.ArgumentException:
        //     The character is in the surrogate pair character range, 0xd800 - 0xdfff.
         void WriteCharEntity(char ch);
        //
        // Summary:
        //     When overridden in a derived class, writes text one buffer at a time.
        //
        // Parameters:
        //   buffer:
        //     Character array containing the text to write.
        //
        //   index:
        //     The position in the buffer indicating the start of the text to write.
        //
        //   count:
        //     The number of characters to write.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     buffer is null.
        //
        //   System.ArgumentOutOfRangeException:
        //     index or count is less than zero. -or-The buffer length minus index is less
        //     than count; the call results in surrogate pair characters being split or
        //     an invalid surrogate pair being written.
         void WriteChars(char[] buffer, int index, int count);
        //
        // Summary:
        //     When overridden in a derived class, writes out a comment <!--...--> containing
        //     the specified text.
        //
        // Parameters:
        //   text:
        //     Text to place inside the comment.
        //
        // Exceptions:
        //   System.ArgumentException:
        //     The text would result in a non-well formed XML document.
         void WriteComment(string text);
        //
        // Summary:
        //     When overridden in a derived class, writes the DOCTYPE declaration with the
        //     specified name and optional attributes.
        //
        // Parameters:
        //   name:
        //     The name of the DOCTYPE. This must be non-empty.
        //
        //   pubid:
        //     If non-null it also writes PUBLIC "pubid" "sysid" where pubid and sysid are
        //     replaced with the value of the given arguments.
        //
        //   sysid:
        //     If pubid is null and sysid is non-null it writes SYSTEM "sysid" where sysid
        //     is replaced with the value of this argument.
        //
        //   subset:
        //     If non-null it writes [subset] where subset is replaced with the value of
        //     this argument.
        //
        // Exceptions:
        //   System.InvalidOperationException:
        //     This method was called outside the prolog (after the root element).
        //
        //   System.ArgumentException:
        //     The value for name would result in invalid XML.
         void WriteDocType(string name, string pubid, string sysid, string subset);
        //
        // Summary:
        //     When overridden in a derived class, writes an element with the specified
        //     local name and value.
        //
        // Parameters:
        //   localName:
        //     The local name of the element.
        //
        //   value:
        //     The value of the element.
        //
        // Exceptions:
        //   System.ArgumentException:
        //     The localName value is null or an empty string.-or-The parameter values are
        //     not valid.
        void WriteElementString(string localName, string value);
        //
        // Summary:
        //     When overridden in a derived class, writes an element with the specified
        //     local name, namespace URI, and value.
        //
        // Parameters:
        //   localName:
        //     The local name of the element.
        //
        //   ns:
        //     The namespace URI to associate with the element.
        //
        //   value:
        //     The value of the element.
        //
        // Exceptions:
        //   System.ArgumentException:
        //     The localName value is null or an empty string.-or-The parameter values are
        //     not valid.
        void WriteElementString(string localName, string ns, string value);
        //
        // Summary:
        //     Writes an element with the specified local name, namespace URI, and value.
        //
        // Parameters:
        //   prefix:
        //     The prefix of the element.
        //
        //   localName:
        //     The local name of the element.
        //
        //   ns:
        //     The namespace URI of the element.
        //
        //   value:
        //     The value of the element.
        //
        // Exceptions:
        //   System.ArgumentException:
        //     The localName value is null or an empty string.-or-The parameter values are
        //     not valid.
        void WriteElementString(string prefix, string localName, string ns, string value);
        //
        // Summary:
        //     When overridden in a derived class, closes the previous System.Xml.XmlWriter.WriteStartAttribute(System.String,System.String)
        //     call.
         void WriteEndAttribute();
        //
        // Summary:
        //     When overridden in a derived class, closes any open elements or attributes
        //     and puts the writer back in the Start state.
        //
        // Exceptions:
        //   System.ArgumentException:
        //     The XML document is invalid.
         void WriteEndDocument();
        //
        // Summary:
        //     When overridden in a derived class, closes one element and pops the corresponding
        //     namespace scope.
        //
        // Exceptions:
        //   System.InvalidOperationException:
        //     This results in an invalid XML document.
         void WriteEndElement();
        //
        // Summary:
        //     When overridden in a derived class, writes out an entity reference as &name;.
        //
        // Parameters:
        //   name:
        //     The name of the entity reference.
        //
        // Exceptions:
        //   System.ArgumentException:
        //     name is either null or String.Empty.
         void WriteEntityRef(string name);
        //
        // Summary:
        //     When overridden in a derived class, closes one element and pops the corresponding
        //     namespace scope.
         void WriteFullEndElement();
        //
        // Summary:
        //     When overridden in a derived class, writes out the specified name, ensuring
        //     it is a valid name according to the W3C XML 1.0 recommendation (http://www.w3.org/TR/1998/REC-xml-19980210#NT-Name).
        //
        // Parameters:
        //   name:
        //     The name to write.
        //
        // Exceptions:
        //   System.ArgumentException:
        //     name is not a valid XML name; or name is either null or String.Empty.
         void WriteName(string name);
        //
        // Summary:
        //     When overridden in a derived class, writes out the specified name, ensuring
        //     it is a valid NmToken according to the W3C XML 1.0 recommendation (http://www.w3.org/TR/1998/REC-xml-19980210#NT-Name).
        //
        // Parameters:
        //   name:
        //     The name to write.
        //
        // Exceptions:
        //   System.ArgumentException:
        //     name is not a valid NmToken; or name is either null or String.Empty.
         void WriteNmToken(string name);
        //
        // Summary:
        //     When overridden in a derived class, copies everything from the reader to
        //     the writer and moves the reader to the start of the next sibling.
        //
        // Parameters:
        //   reader:
        //     The System.Xml.XmlReader to read from.
        //
        //   defattr:
        //     true to copy the default attributes from the XmlReader; otherwise, false.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     reader is null.
         void WriteNode(XmlReader reader, bool defattr);
        //
        // Summary:
        //     Copies everything from the System.Xml.XPath.XPathNavigator object to the
        //     writer. The position of the System.Xml.XPath.XPathNavigator remains unchanged.
        //
        // Parameters:
        //   navigator:
        //     The System.Xml.XPath.XPathNavigator to copy from.
        //
        //   defattr:
        //     true to copy the default attributes; otherwise, false.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     navigator is null.
         void WriteNode(XPathNavigator navigator, bool defattr);
        //
        // Summary:
        //     When overridden in a derived class, writes out a processing instruction with
        //     a space between the name and text as follows: <?name text?>.
        //
        // Parameters:
        //   name:
        //     The name of the processing instruction.
        //
        //   text:
        //     The text to include in the processing instruction.
        //
        // Exceptions:
        //   System.ArgumentException:
        //     The text would result in a non-well formed XML document.name is either null
        //     or String.Empty.This method is being used to create an XML declaration after
        //     System.Xml.XmlWriter.WriteStartDocument() has already been called.
         void WriteProcessingInstruction(string name, string text);
        //
        // Summary:
        //     When overridden in a derived class, writes out the namespace-qualified name.
        //     This method looks up the prefix that is in scope for the given namespace.
        //
        // Parameters:
        //   localName:
        //     The local name to write.
        //
        //   ns:
        //     The namespace URI for the name.
        //
        // Exceptions:
        //   System.ArgumentException:
        //     localName is either null or String.Empty.localName is not a valid name.
         void WriteQualifiedName(string localName, string ns);
        //
        // Summary:
        //     When overridden in a derived class, writes raw markup manually from a string.
        //
        // Parameters:
        //   data:
        //     String containing the text to write.
         void WriteRaw(string data);
        //
        // Summary:
        //     When overridden in a derived class, writes raw markup manually from a character
        //     buffer.
        //
        // Parameters:
        //   buffer:
        //     Character array containing the text to write.
        //
        //   index:
        //     The position within the buffer indicating the start of the text to write.
        //
        //   count:
        //     The number of characters to write.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     buffer is null.
        //
        //   System.ArgumentOutOfRangeException:
        //     index or count is less than zero. -or-The buffer length minus index is less
        //     than count.
         void WriteRaw(char[] buffer, int index, int count);
        //
        // Summary:
        //     Writes the start of an attribute with the specified local name.
        //
        // Parameters:
        //   localName:
        //     The local name of the attribute.
        void WriteStartAttribute(string localName);
        //
        // Summary:
        //     Writes the start of an attribute with the specified local name and namespace
        //     URI.
        //
        // Parameters:
        //   localName:
        //     The local name of the attribute.
        //
        //   ns:
        //     The namespace URI of the attribute.
        void WriteStartAttribute(string localName, string ns);
        //
        // Summary:
        //     When overridden in a derived class, writes the start of an attribute with
        //     the specified prefix, local name, and namespace URI.
        //
        // Parameters:
        //   prefix:
        //     The namespace prefix of the attribute.
        //
        //   localName:
        //     The local name of the attribute.
        //
        //   ns:
        //     The namespace URI for the attribute.
         void WriteStartAttribute(string prefix, string localName, string ns);
        //
        // Summary:
        //     When overridden in a derived class, writes the XML declaration with the version
        //     "1.0".
        //
        // Exceptions:
        //   System.InvalidOperationException:
        //     This is not the first write method called after the constructor.
         void WriteStartDocument();
        //
        // Summary:
        //     When overridden in a derived class, writes the XML declaration with the version
        //     "1.0" and the standalone attribute.
        //
        // Parameters:
        //   standalone:
        //     If true, it writes "standalone=yes"; if false, it writes "standalone=no".
        //
        // Exceptions:
        //   System.InvalidOperationException:
        //     This is not the first write method called after the constructor.
         void WriteStartDocument(bool standalone);
        //
        // Summary:
        //     When overridden in a derived class, writes out a start tag with the specified
        //     local name.
        //
        // Parameters:
        //   localName:
        //     The local name of the element.
        //
        // Exceptions:
        //   System.InvalidOperationException:
        //     The writer is closed.
        void WriteStartElement(string localName);
        //
        // Summary:
        //     When overridden in a derived class, writes the specified start tag and associates
        //     it with the given namespace.
        //
        // Parameters:
        //   localName:
        //     The local name of the element.
        //
        //   ns:
        //     The namespace URI to associate with the element. If this namespace is already
        //     in scope and has an associated prefix, the writer automatically writes that
        //     prefix also.
        //
        // Exceptions:
        //   System.InvalidOperationException:
        //     The writer is closed.
        void WriteStartElement(string localName, string ns);
        //
        // Summary:
        //     When overridden in a derived class, writes the specified start tag and associates
        //     it with the given namespace and prefix.
        //
        // Parameters:
        //   prefix:
        //     The namespace prefix of the element.
        //
        //   localName:
        //     The local name of the element.
        //
        //   ns:
        //     The namespace URI to associate with the element.
        //
        // Exceptions:
        //   System.InvalidOperationException:
        //     The writer is closed.
         void WriteStartElement(string prefix, string localName, string ns);
        //
        // Summary:
        //     When overridden in a derived class, writes the given text content.
        //
        // Parameters:
        //   text:
        //     The text to write.
        //
        // Exceptions:
        //   System.ArgumentException:
        //     The text string contains an invalid surrogate pair.
         void WriteString(string text);
        //
        // Summary:
        //     When overridden in a derived class, generates and writes the surrogate character
        //     entity for the surrogate character pair.
        //
        // Parameters:
        //   lowChar:
        //     The low surrogate. This must be a value between 0xDC00 and 0xDFFF.
        //
        //   highChar:
        //     The high surrogate. This must be a value between 0xD800 and 0xDBFF.
        //
        // Exceptions:
        //   System.Exception:
        //     An invalid surrogate character pair was passed.
         void WriteSurrogateCharEntity(char lowChar, char highChar);
        //
        // Summary:
        //     Writes a System.Boolean value.
        //
        // Parameters:
        //   value:
        //     The System.Boolean value to write.
        //
        // Exceptions:
        //   System.ArgumentException:
        //     An invalid value was specified.
         void WriteValue(bool value);
        //
        // Summary:
        //     Writes a System.DateTime value.
        //
        // Parameters:
        //   value:
        //     The System.DateTime value to write.
        //
        // Exceptions:
        //   System.ArgumentException:
        //     An invalid value was specified.
         void WriteValue(DateTime value);
        //
        // Summary:
        //     Writes a System.Decimal value.
        //
        // Parameters:
        //   value:
        //     The System.Decimal value to write.
        //
        // Exceptions:
        //   System.ArgumentException:
        //     An invalid value was specified.
         void WriteValue(decimal value);
        //
        // Summary:
        //     Writes a System.Double value.
        //
        // Parameters:
        //   value:
        //     The System.Double value to write.
        //
        // Exceptions:
        //   System.ArgumentException:
        //     An invalid value was specified.
         void WriteValue(double value);
        //
        // Summary:
        //     Writes a single-precision floating-Vector2i number.
        //
        // Parameters:
        //   value:
        //     The single-precision floating-Vector2i number to write.
        //
        // Exceptions:
        //   System.ArgumentException:
        //     An invalid value was specified.
         void WriteValue(float value);
        //
        // Summary:
        //     Writes a System.Int32 value.
        //
        // Parameters:
        //   value:
        //     The System.Int32 value to write.
        //
        // Exceptions:
        //   System.ArgumentException:
        //     An invalid value was specified.
         void WriteValue(int value);
        //
        // Summary:
        //     Writes a System.Int64 value.
        //
        // Parameters:
        //   value:
        //     The System.Int64 value to write.
        //
        // Exceptions:
        //   System.ArgumentException:
        //     An invalid value was specified.
         void WriteValue(long value);
        //
        // Summary:
        //     Writes the object value.
        //
        // Parameters:
        //   value:
        //     The object value to write. With the release of the .NET Framework 3.5, this
        //     method accepts System.DateTimeOffset as a parameter.
        //
        // Exceptions:
        //   System.ArgumentException:
        //     An invalid value was specified.
         void WriteValue(object value);
        //
        // Summary:
        //     Writes a System.String value.
        //
        // Parameters:
        //   value:
        //     The System.String value to write.
        //
        // Exceptions:
        //   System.ArgumentException:
        //     An invalid value was specified.
         void WriteValue(string value);
        //
        // Summary:
        //     When overridden in a derived class, writes out the given white space.
        //
        // Parameters:
        //   ws:
        //     The string of white space characters.
        //
        // Exceptions:
        //   System.ArgumentException:
        //     The string contains non-white space characters.
         void WriteWhitespace(string ws);
    }
}

