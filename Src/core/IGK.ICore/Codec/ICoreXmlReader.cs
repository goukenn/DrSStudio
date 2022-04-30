

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreXmlReader.cs
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
file:ICoreXmlReader.cs
*/
using System;
using System.Text;
using System.Xml;
using System.IO;
using System.Xml.Schema;
namespace IGK.ICore.Codec
{
    /// <summary>
    /// core xml reader  method
    /// </summary>
    public interface ICoreXmlReader
    {
        // Summary:
        //     When overridden in a derived class, gets the number of attributes on the
        //     current node.
        //
        // Returns:
        //     The number of attributes on the current node.
        int AttributeCount { get; }
        //
        // Summary:
        //     When overridden in a derived class, gets the base URI of the current node.
        //
        // Returns:
        //     The base URI of the current node.
        string BaseURI { get; }
        //
        // Summary:
        //     Gets a value indicating whether the System.Xml.XmlReader implements the binary
        //     content read methods.
        //
        // Returns:
        //     true if the binary content read methods are implemented; otherwise false.
        bool CanReadBinaryContent { get; }
        //
        // Summary:
        //     Gets a value indicating whether the System.Xml.XmlReader implements the System.Xml.XmlReader.ReadValueChunk(System.Char[],System.Int32,System.Int32)
        //     method.
        //
        // Returns:
        //     true if the System.Xml.XmlReader implements the System.Xml.XmlReader.ReadValueChunk(System.Char[],System.Int32,System.Int32)
        //     method; otherwise false.
        bool CanReadValueChunk { get; }
        //
        // Summary:
        //     Gets a value indicating whether this reader can parse and resolve entities.
        //
        // Returns:
        //     true if the reader can parse and resolve entities; otherwise, false.
        bool CanResolveEntity { get; }
        //
        // Summary:
        //     When overridden in a derived class, gets the depth of the current node in
        //     the XML document.
        //
        // Returns:
        //     The depth of the current node in the XML document.
        int Depth { get; }
        //
        // Summary:
        //     When overridden in a derived class, gets a value indicating whether the reader
        //     is positioned at the end of the stream.
        //
        // Returns:
        //     true if the reader is positioned at the end of the stream; otherwise, false.
        bool EOF { get; }
        //
        // Summary:
        //     Gets a value indicating whether the current node has any attributes.
        //
        // Returns:
        //     true if the current node has attributes; otherwise, false.
        bool HasAttributes { get; }
        //
        // Summary:
        //     When overridden in a derived class, gets a value indicating whether the current
        //     node can have a System.Xml.XmlReader.Value.
        //
        // Returns:
        //     true if the node on which the reader is currently positioned can have a Value;
        //     otherwise, false. If false, the node has a value of String.Empty.
        bool HasValue { get; }
        //
        // Summary:
        //     When overridden in a derived class, gets a value indicating whether the current
        //     node is an attribute that was generated from the default value defined in
        //     the DTD or schema.
        //
        // Returns:
        //     true if the current node is an attribute whose value was generated from the
        //     default value defined in the DTD or schema; false if the attribute value
        //     was explicitly set.
        bool IsDefault { get; }
        //
        // Summary:
        //     When overridden in a derived class, gets a value indicating whether the current
        //     node is an empty element (for example, <MyElement/>).
        //
        // Returns:
        //     true if the current node is an element (System.Xml.XmlReader.NodeType equals
        //     XmlNodeType.Element) that ends with />; otherwise, false.
        bool IsEmptyElement { get; }
        //
        // Summary:
        //     When overridden in a derived class, gets the local name of the current node.
        //
        // Returns:
        //     The name of the current node with the prefix removed. For example, LocalName
        //     is book for the element <bk:book>.For node types that do not have a name
        //     (like Text, Comment, and so on), this property returns String.Empty.
        string LocalName { get; }
        //
        // Summary:
        //     When overridden in a derived class, gets the qualified name of the current
        //     node.
        //
        // Returns:
        //     The qualified name of the current node. For example, Name is bk:book for
        //     the element <bk:book>.The name returned is dependent on the System.Xml.XmlReader.NodeType
        //     of the node. The following node types return the listed values. All other
        //     node types return an empty string.Node type Name AttributeThe name of the
        //     attribute. DocumentTypeThe document type name. ElementThe tag name. EntityReferenceThe
        //     name of the entity referenced. ProcessingInstructionThe target of the processing
        //     instruction. XmlDeclarationThe literal string xml.
        string Name { get; }
        //
        // Summary:
        //     When overridden in a derived class, gets the namespace URI (as defined in
        //     the W3C Namespace specification) of the node on which the reader is positioned.
        //
        // Returns:
        //     The namespace URI of the current node; otherwise an empty string.
        string NamespaceURI { get; }
        //
        // Summary:
        //     When overridden in a derived class, gets the System.Xml.XmlNameTable associated
        //     with this implementation.
        //
        // Returns:
        //     The XmlNameTable enabling you to get the atomized version of a string within
        //     the node.
        XmlNameTable NameTable { get; }
        //
        // Summary:
        //     When overridden in a derived class, gets the type of the current node.
        //
        // Returns:
        //     One of the System.Xml.XmlNodeType values representing the type of the current
        //     node.
        XmlNodeType NodeType { get; }
        //
        // Summary:
        //     When overridden in a derived class, gets the namespace prefix associated
        //     with the current node.
        //
        // Returns:
        //     The namespace prefix associated with the current node.
        string Prefix { get; }
        //
        // Summary:
        //     When overridden in a derived class, gets the quotation mark character used
        //     to enclose the value of an attribute node.
        //
        // Returns:
        //     The quotation mark character (" or ') used to enclose the value of an attribute
        //     node.
        char QuoteChar { get; }
        //
        // Summary:
        //     When overridden in a derived class, gets the state of the reader.
        //
        // Returns:
        //     One of the System.Xml.ReadState values.
        ReadState ReadState { get; }
        //
        // Summary:
        //     Gets the schema information that has been assigned to the current node as
        //     a result of schema validation.
        //
        // Returns:
        //     An System.Xml.Schema.IXmlSchemaInfo object containing the schema information
        //     for the current node. Schema information can be set on elements, attributes,
        //     or on text nodes with a non-null System.Xml.XmlReader.ValueType (typed values).
        //     If the current node is not one of the above node types, or if the XmlReader
        //     instance does not report schema information, this property returns null.If
        //     this property is called from an System.Xml.XmlTextReader or an System.Xml.XmlValidatingReader
        //     object, this property always returns null. These XmlReader implementations
        //     do not expose schema information through the SchemaInfo property.
        IXmlSchemaInfo SchemaInfo { get; }
        //
        // Summary:
        //     Gets the System.Xml.XmlReaderSettings object used to create this System.Xml.XmlReader
        //     instance.
        //
        // Returns:
        //     The System.Xml.XmlReaderSettings object used to create this reader instance.
        //     If this reader was not created using the Overload:System.Xml.XmlReader.Create
        //     method, this property returns null.
        XmlReaderSettings Settings { get; }
        //
        // Summary:
        //     When overridden in a derived class, gets the text value of the current node.
        //
        // Returns:
        //     The value returned depends on the System.Xml.XmlReader.NodeType of the node.
        //     The following table lists node types that have a value to return. All other
        //     node types return String.Empty.Node type Value AttributeThe value of the
        //     attribute. CDATAThe content of the CDATA section. CommentThe content of the
        //     comment. DocumentTypeThe internal subset. ProcessingInstructionThe entire
        //     content, excluding the target. SignificantWhitespaceThe white space between
        //     markup in a mixed content model. TextThe content of the text node. WhitespaceThe
        //     white space between markup. XmlDeclarationThe content of the declaration.
        string Value { get; }
        //
        // Summary:
        //     Gets The Common Language Runtime (CLR) type for the current node.
        //
        // Returns:
        //     The CLR type that corresponds to the typed value of the node. The default
        //     is System.String.
        Type ValueType { get; }
        //
        // Summary:
        //     When overridden in a derived class, gets the current xml:lang scope.
        //
        // Returns:
        //     The current xml:lang scope.
        string XmlLang { get; }
        //
        // Summary:
        //     When overridden in a derived class, gets the current xml:space scope.
        //
        // Returns:
        //     One of the System.Xml.XmlSpace values. If no xml:space scope exists, this
        //     property defaults to XmlSpace.None.
        XmlSpace XmlSpace { get; }
        // Summary:
        //     When overridden in a derived class, gets the value of the attribute with
        //     the specified index.
        //
        // Parameters:
        //   i:
        //     The index of the attribute.
        //
        // Returns:
        //     The value of the specified attribute.
        string this[int i] { get; }
        //
        // Summary:
        //     When overridden in a derived class, gets the value of the attribute with
        //     the specified System.Xml.XmlReader.Name.
        //
        // Parameters:
        //   name:
        //     The qualified name of the attribute.
        //
        // Returns:
        //     The value of the specified attribute. If the attribute is not found, null
        //     is returned.
        string this[string name] { get; }
        //
        // Summary:
        //     When overridden in a derived class, gets the value of the attribute with
        //     the specified System.Xml.XmlReader.LocalName and System.Xml.XmlReader.NamespaceURI.
        //
        // Parameters:
        //   name:
        //     The local name of the attribute.
        //
        //   namespaceURI:
        //     The namespace URI of the attribute.
        //
        // Returns:
        //     The value of the specified attribute. If the attribute is not found, null
        //     is returned.
        string this[string name, string namespaceURI] { get; }
        // Summary:
        //     When overridden in a derived class, changes the System.Xml.XmlReader.ReadState
        //     to Closed.
        void Close();
        //
        // Summary:
        //     When overridden in a derived class, gets the value of the attribute with
        //     the specified index.
        //
        // Parameters:
        //   i:
        //     The index of the attribute. The index is zero-based. (The first attribute
        //     has index 0.)
        //
        // Returns:
        //     The value of the specified attribute. This method does not move the reader.
        string GetAttribute(int i);
        //
        // Summary:
        //     When overridden in a derived class, gets the value of the attribute with
        //     the specified System.Xml.XmlReader.Name.
        //
        // Parameters:
        //   name:
        //     The qualified name of the attribute.
        //
        // Returns:
        //     The value of the specified attribute. If the attribute is not found, null
        //     is returned.
        string GetAttribute(string name);
        //
        // Summary:
        //     When overridden in a derived class, gets the value of the attribute with
        //     the specified System.Xml.XmlReader.LocalName and System.Xml.XmlReader.NamespaceURI.
        //
        // Parameters:
        //   name:
        //     The local name of the attribute.
        //
        //   namespaceURI:
        //     The namespace URI of the attribute.
        //
        // Returns:
        //     The value of the specified attribute. If the attribute is not found, null
        //     is returned. This method does not move the reader.
        string GetAttribute(string name, string namespaceURI);
        //
        // Summary:
        //     Gets a value indicating whether the string argument is a valid XML name.
        //
        // Parameters:
        //   str:
        //     The name to validate.
        //
        // Returns:
        //     true if the name is valid; otherwise, false.
        bool IsName(string str);
        //
        // Summary:
        //     Gets a value indicating whether or not the string argument is a valid XML
        //     name token.
        //
        // Parameters:
        //   str:
        //     The name token to validate.
        //
        // Returns:
        //     true if it is a valid name token; otherwise false.
        bool IsNameToken(string str);
        //
        // Summary:
        //     Calls System.Xml.XmlReader.MoveToContent() and tests if the current content
        //     node is a start tag or empty element tag.
        //
        // Returns:
        //     true if System.Xml.XmlReader.MoveToContent() finds a start tag or empty element
        //     tag; false if a node type other than XmlNodeType.Element was found.
        //
        // Exceptions:
        //   System.Xml.XmlException:
        //     Incorrect XML is encountered in the input stream.
        bool IsStartElement();
        //
        // Summary:
        //     Calls System.Xml.XmlReader.MoveToContent() and tests if the current content
        //     node is a start tag or empty element tag and if the System.Xml.XmlReader.Name
        //     property of the element found matches the given argument.
        //
        // Parameters:
        //   name:
        //     The string matched against the Name property of the element found.
        //
        // Returns:
        //     true if the resulting node is an element and the Name property matches the
        //     specified string. false if a node type other than XmlNodeType.Element was
        //     found or if the element Name property does not match the specified string.
        //
        // Exceptions:
        //   System.Xml.XmlException:
        //     Incorrect XML is encountered in the input stream.
        bool IsStartElement(string name);
        //
        // Summary:
        //     Calls System.Xml.XmlReader.MoveToContent() and tests if the current content
        //     node is a start tag or empty element tag and if the System.Xml.XmlReader.LocalName
        //     and System.Xml.XmlReader.NamespaceURI properties of the element found match
        //     the given strings.
        //
        // Parameters:
        //   localname:
        //     The string to match against the LocalName property of the element found.
        //
        //   ns:
        //     The string to match against the NamespaceURI property of the element found.
        //
        // Returns:
        //     true if the resulting node is an element. false if a node type other than
        //     XmlNodeType.Element was found or if the LocalName and NamespaceURI properties
        //     of the element do not match the specified strings.
        //
        // Exceptions:
        //   System.Xml.XmlException:
        //     Incorrect XML is encountered in the input stream.
        bool IsStartElement(string localname, string ns);
        //
        // Summary:
        //     When overridden in a derived class, resolves a namespace prefix in the current
        //     element's scope.
        //
        // Parameters:
        //   prefix:
        //     The prefix whose namespace URI you want to resolve. To match the default
        //     namespace, pass an empty string.
        //
        // Returns:
        //     The namespace URI to which the prefix maps or null if no matching prefix
        //     is found.
        string LookupNamespace(string prefix);
        //
        // Summary:
        //     When overridden in a derived class, moves to the attribute with the specified
        //     index.
        //
        // Parameters:
        //   i:
        //     The index of the attribute.
        void MoveToAttribute(int i);
        //
        // Summary:
        //     When overridden in a derived class, moves to the attribute with the specified
        //     System.Xml.XmlReader.Name.
        //
        // Parameters:
        //   name:
        //     The qualified name of the attribute.
        //
        // Returns:
        //     true if the attribute is found; otherwise, false. If false, the reader's
        //     position does not change.
        bool MoveToAttribute(string name);
        //
        // Summary:
        //     When overridden in a derived class, moves to the attribute with the specified
        //     System.Xml.XmlReader.LocalName and System.Xml.XmlReader.NamespaceURI.
        //
        // Parameters:
        //   name:
        //     The local name of the attribute.
        //
        //   ns:
        //     The namespace URI of the attribute.
        //
        // Returns:
        //     true if the attribute is found; otherwise, false. If false, the reader's
        //     position does not change.
        bool MoveToAttribute(string name, string ns);
        //
        // Summary:
        //     Checks whether the current node is a content (non-white space text, CDATA,
        //     Element, EndElement, EntityReference, or EndEntity) node. If the node is
        //     not a content node, the reader skips ahead to the next content node or end
        //     of file. It skips over nodes of the following type: ProcessingInstruction,
        //     DocumentType, Comment, Whitespace, or SignificantWhitespace.
        //
        // Returns:
        //     The System.Xml.XmlReader.NodeType of the current node found by the method
        //     or XmlNodeType.None if the reader has reached the end of the input stream.
        //
        // Exceptions:
        //   System.Xml.XmlException:
        //     Incorrect XML encountered in the input stream.
        XmlNodeType MoveToContent();
        //
        // Summary:
        //     When overridden in a derived class, moves to the element that contains the
        //     current attribute node.
        //
        // Returns:
        //     true if the reader is positioned on an attribute (the reader moves to the
        //     element that owns the attribute); false if the reader is not positioned on
        //     an attribute (the position of the reader does not change).
        bool MoveToElement();
        //
        // Summary:
        //     When overridden in a derived class, moves to the first attribute.
        //
        // Returns:
        //     true if an attribute exists (the reader moves to the first attribute); otherwise,
        //     false (the position of the reader does not change).
        bool MoveToFirstAttribute();
        //
        // Summary:
        //     When overridden in a derived class, moves to the next attribute.
        //
        // Returns:
        //     true if there is a next attribute; false if there are no more attributes.
        bool MoveToNextAttribute();
        //
        // Summary:
        //     When overridden in a derived class, reads the next node from the stream.
        //
        // Returns:
        //     true if the next node was read successfully; false if there are no more nodes
        //     to read.
        //
        // Exceptions:
        //   System.Xml.XmlException:
        //     An error occurred while parsing the XML.
        bool Read();
        //
        // Summary:
        //     When overridden in a derived class, parses the attribute value into one or
        //     more Text, EntityReference, or EndEntity nodes.
        //
        // Returns:
        //     true if there are nodes to return.false if the reader is not positioned on
        //     an attribute node when the initial call is made or if all the attribute values
        //     have been read.An empty attribute, such as, misc="", returns true with a
        //     single node with a value of String.Empty.
        bool ReadAttributeValue();
        //
        // Summary:
        //     Reads the content as an object of the type specified.
        //
        // Parameters:
        //   returnType:
        //     The type of the value to be returned.With the release of the .NET Framework
        //     3.5, the value of the returnType parameter can now be the System.DateTimeOffset
        //     type.
        //
        //   namespaceResolver:
        //     An System.Xml.IXmlNamespaceResolver object that is used to resolve any namespace
        //     prefixes related to type conversion. For example, this can be used when converting
        //     an System.Xml.XmlQualifiedName object to an xs:string.This value can be null.
        //
        // Returns:
        //     The concatenated text content or attribute value converted to the requested
        //     type.
        //
        // Exceptions:
        //   System.FormatException:
        //     The content is not in the correct format for the target type.
        //
        //   System.InvalidCastException:
        //     The attempted cast is not valid.
        //
        //   System.ArgumentNullException:
        //     The returnType value is null.
        //
        //   System.InvalidOperationException:
        //     The current node is not a supported node type. See the table below for details.
        object ReadContentAs(Type returnType, IXmlNamespaceResolver namespaceResolver);
        //
        // Summary:
        //     Reads the content and returns the Base64 decoded binary bytes.
        //
        // Parameters:
        //   buffer:
        //     The buffer into which to copy the resulting text. This value cannot be null.
        //
        //   index:
        //     The offset into the buffer where to start copying the result.
        //
        //   count:
        //     The maximum number of bytes to copy into the buffer. The actual number of
        //     bytes copied is returned from this method.
        //
        // Returns:
        //     The number of bytes written to the buffer.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     The buffer value is null.
        //
        //   System.InvalidOperationException:
        //     System.Xml.XmlReader.ReadContentAsBase64(System.Byte[],System.Int32,System.Int32)
        //     is not supported on the current node.
        //
        //   System.ArgumentOutOfRangeException:
        //     The index into the buffer or index + count is larger than the allocated buffer
        //     Size2i.
        //
        //   System.NotSupportedException:
        //     The System.Xml.XmlReader implementation does not support this method.
        int ReadContentAsBase64(byte[] buffer, int index, int count);
        //
        // Summary:
        //     Reads the content and returns the BinHex decoded binary bytes.
        //
        // Parameters:
        //   buffer:
        //     The buffer into which to copy the resulting text. This value cannot be null.
        //
        //   index:
        //     The offset into the buffer where to start copying the result.
        //
        //   count:
        //     The maximum number of bytes to copy into the buffer. The actual number of
        //     bytes copied is returned from this method.
        //
        // Returns:
        //     The number of bytes written to the buffer.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     The buffer value is null.
        //
        //   System.InvalidOperationException:
        //     System.Xml.XmlReader.ReadContentAsBinHex(System.Byte[],System.Int32,System.Int32)
        //     is not supported on the current node.
        //
        //   System.ArgumentOutOfRangeException:
        //     The index into the buffer or index + count is larger than the allocated buffer
        //     Size2i.
        //
        //   System.NotSupportedException:
        //     The System.Xml.XmlReader implementation does not support this method.
        int ReadContentAsBinHex(byte[] buffer, int index, int count);
        //
        // Summary:
        //     Reads the text content at the current position as a Boolean.
        //
        // Returns:
        //     The text content as a System.Boolean object.
        //
        // Exceptions:
        //   System.InvalidCastException:
        //     The attempted cast is not valid.
        //
        //   System.FormatException:
        //     The string format is not valid.
        bool ReadContentAsBoolean();
        //
        // Summary:
        //     Reads the text content at the current position as a System.DateTime object.
        //
        // Returns:
        //     The text content as a System.DateTime object.
        //
        // Exceptions:
        //   System.InvalidCastException:
        //     The attempted cast is not valid.
        //
        //   System.FormatException:
        //     The string format is not valid.
        DateTime ReadContentAsDateTime();
        //
        // Summary:
        //     Reads the text content at the current position as a System.Decimal object.
        //
        // Returns:
        //     The text content at the current position as a System.Decimal object.
        //
        // Exceptions:
        //   System.InvalidCastException:
        //     The attempted cast is not valid.
        //
        //   System.FormatException:
        //     The string format is not valid.
        decimal ReadContentAsDecimal();
        //
        // Summary:
        //     Reads the text content at the current position as a double-precision floating-Vector2i
        //     number.
        //
        // Returns:
        //     The text content as a double-precision floating-Vector2i number.
        //
        // Exceptions:
        //   System.InvalidCastException:
        //     The attempted cast is not valid.
        //
        //   System.FormatException:
        //     The string format is not valid.
        double ReadContentAsDouble();
        //
        // Summary:
        //     Reads the text content at the current position as a single-precision floating
        //     Vector2i number.
        //
        // Returns:
        //     The text content at the current position as a single-precision floating Vector2i
        //     number.
        //
        // Exceptions:
        //   System.InvalidCastException:
        //     The attempted cast is not valid.
        //
        //   System.FormatException:
        //     The string format is not valid.
        float ReadContentAsFloat();
        //
        // Summary:
        //     Reads the text content at the current position as a 32-bit signed integer.
        //
        // Returns:
        //     The text content as a 32-bit signed integer.
        //
        // Exceptions:
        //   System.InvalidCastException:
        //     The attempted cast is not valid.
        //
        //   System.FormatException:
        //     The string format is not valid.
        int ReadContentAsInt();
        //
        // Summary:
        //     Reads the text content at the current position as a 64-bit signed integer.
        //
        // Returns:
        //     The text content as a 64-bit signed integer.
        //
        // Exceptions:
        //   System.InvalidCastException:
        //     The attempted cast is not valid.
        //
        //   System.FormatException:
        //     The string format is not valid.
        long ReadContentAsLong();
        //
        // Summary:
        //     Reads the text content at the current position as an System.Object.
        //
        // Returns:
        //     The text content as the most appropriate common language runtime (CLR) object.
        //
        // Exceptions:
        //   System.InvalidCastException:
        //     The attempted cast is not valid.
        //
        //   System.FormatException:
        //     The string format is not valid.
        object ReadContentAsObject();
        //
        // Summary:
        //     Reads the text content at the current position as a System.String object.
        //
        // Returns:
        //     The text content as a System.String object.
        //
        // Exceptions:
        //   System.InvalidCastException:
        //     The attempted cast is not valid.
        //
        //   System.FormatException:
        //     The string format is not valid.
        string ReadContentAsString();
        //
        // Summary:
        //     Reads the element content as the requested type.
        //
        // Parameters:
        //   returnType:
        //     The type of the value to be returned.With the release of the .NET Framework
        //     3.5, the value of the returnType parameter can now be the System.DateTimeOffset
        //     type.
        //
        //   namespaceResolver:
        //     An System.Xml.IXmlNamespaceResolver object that is used to resolve any namespace
        //     prefixes related to type conversion.
        //
        // Returns:
        //     The element content converted to the requested typed object.
        //
        // Exceptions:
        //   System.InvalidOperationException:
        //     The System.Xml.XmlReader is not positioned on an element.
        //
        //   System.Xml.XmlException:
        //     The current element contains child elements.-or-The element content cannot
        //     be converted to the requested type.
        //
        //   System.ArgumentNullException:
        //     The method is called with null arguments.
        object ReadElementContentAs(Type returnType, IXmlNamespaceResolver namespaceResolver);
        //
        // Summary:
        //     Checks that the specified local name and namespace URI matches that of the
        //     current element, then reads the element content as the requested type.
        //
        // Parameters:
        //   returnType:
        //     The type of the value to be returned.With the release of the .NET Framework
        //     3.5, the value of the returnType parameter can now be the System.DateTimeOffset
        //     type.
        //
        //   namespaceResolver:
        //     An System.Xml.IXmlNamespaceResolver object that is used to resolve any namespace
        //     prefixes related to type conversion.
        //
        //   localName:
        //     The local name of the element.
        //
        //   namespaceURI:
        //     The namespace URI of the element.
        //
        // Returns:
        //     The element content converted to the requested typed object.
        //
        // Exceptions:
        //   System.InvalidOperationException:
        //     The System.Xml.XmlReader is not positioned on an element.
        //
        //   System.Xml.XmlException:
        //     The current element contains child elements.-or-The element content cannot
        //     be converted to the requested type.
        //
        //   System.ArgumentNullException:
        //     The method is called with null arguments.
        //
        //   System.ArgumentException:
        //     The specified local name and namespace URI do not match that of the current
        //     element being read.
        object ReadElementContentAs(Type returnType, IXmlNamespaceResolver namespaceResolver, string localName, string namespaceURI);
        //
        // Summary:
        //     Reads the element and decodes the Base64 content.
        //
        // Parameters:
        //   buffer:
        //     The buffer into which to copy the resulting text. This value cannot be null.
        //
        //   index:
        //     The offset into the buffer where to start copying the result.
        //
        //   count:
        //     The maximum number of bytes to copy into the buffer. The actual number of
        //     bytes copied is returned from this method.
        //
        // Returns:
        //     The number of bytes written to the buffer.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     The buffer value is null.
        //
        //   System.InvalidOperationException:
        //     The current node is not an element node.
        //
        //   System.ArgumentOutOfRangeException:
        //     The index into the buffer or index + count is larger than the allocated buffer
        //     Size2i.
        //
        //   System.NotSupportedException:
        //     The System.Xml.XmlReader implementation does not support this method.
        //
        //   System.Xml.XmlException:
        //     The element contains mixed-content.
        //
        //   System.FormatException:
        //     The content cannot be converted to the requested type.
        int ReadElementContentAsBase64(byte[] buffer, int index, int count);
        //
        // Summary:
        //     Reads the element and decodes the BinHex content.
        //
        // Parameters:
        //   buffer:
        //     The buffer into which to copy the resulting text. This value cannot be null.
        //
        //   index:
        //     The offset into the buffer where to start copying the result.
        //
        //   count:
        //     The maximum number of bytes to copy into the buffer. The actual number of
        //     bytes copied is returned from this method.
        //
        // Returns:
        //     The number of bytes written to the buffer.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     The buffer value is null.
        //
        //   System.InvalidOperationException:
        //     The current node is not an element node.
        //
        //   System.ArgumentOutOfRangeException:
        //     The index into the buffer or index + count is larger than the allocated buffer
        //     Size2i.
        //
        //   System.NotSupportedException:
        //     The System.Xml.XmlReader implementation does not support this method.
        //
        //   System.Xml.XmlException:
        //     The element contains mixed-content.
        //
        //   System.FormatException:
        //     The content cannot be converted to the requested type.
        int ReadElementContentAsBinHex(byte[] buffer, int index, int count);
        //
        // Summary:
        //     Reads the current element and returns the contents as a System.Boolean object.
        //
        // Returns:
        //     The element content as a System.Boolean object.
        //
        // Exceptions:
        //   System.InvalidOperationException:
        //     The System.Xml.XmlReader is not positioned on an element.
        //
        //   System.Xml.XmlException:
        //     The current element contains child elements.-or-The element content cannot
        //     be converted to a System.Boolean object.
        //
        //   System.ArgumentNullException:
        //     The method is called with null arguments.
        bool ReadElementContentAsBoolean();
        //
        // Summary:
        //     Checks that the specified local name and namespace URI matches that of the
        //     current element, then reads the current element and returns the contents
        //     as a System.Boolean object.
        //
        // Parameters:
        //   localName:
        //     The local name of the element.
        //
        //   namespaceURI:
        //     The namespace URI of the element.
        //
        // Returns:
        //     The element content as a System.Boolean object.
        //
        // Exceptions:
        //   System.InvalidOperationException:
        //     The System.Xml.XmlReader is not positioned on an element.
        //
        //   System.Xml.XmlException:
        //     The current element contains child elements.-or-The element content cannot
        //     be converted to the requested type.
        //
        //   System.ArgumentNullException:
        //     The method is called with null arguments.
        //
        //   System.ArgumentException:
        //     The specified local name and namespace URI do not match that of the current
        //     element being read.
        bool ReadElementContentAsBoolean(string localName, string namespaceURI);
        //
        // Summary:
        //     Reads the current element and returns the contents as a System.DateTime object.
        //
        // Returns:
        //     The element content as a System.DateTime object.
        //
        // Exceptions:
        //   System.InvalidOperationException:
        //     The System.Xml.XmlReader is not positioned on an element.
        //
        //   System.Xml.XmlException:
        //     The current element contains child elements.-or-The element content cannot
        //     be converted to a System.DateTime object.
        //
        //   System.ArgumentNullException:
        //     The method is called with null arguments.
        DateTime ReadElementContentAsDateTime();
        //
        // Summary:
        //     Checks that the specified local name and namespace URI matches that of the
        //     current element, then reads the current element and returns the contents
        //     as a System.DateTime object.
        //
        // Parameters:
        //   localName:
        //     The local name of the element.
        //
        //   namespaceURI:
        //     The namespace URI of the element.
        //
        // Returns:
        //     The element contents as a System.DateTime object.
        //
        // Exceptions:
        //   System.InvalidOperationException:
        //     The System.Xml.XmlReader is not positioned on an element.
        //
        //   System.Xml.XmlException:
        //     The current element contains child elements.-or-The element content cannot
        //     be converted to the requested type.
        //
        //   System.ArgumentNullException:
        //     The method is called with null arguments.
        //
        //   System.ArgumentException:
        //     The specified local name and namespace URI do not match that of the current
        //     element being read.
        DateTime ReadElementContentAsDateTime(string localName, string namespaceURI);
        //
        // Summary:
        //     Reads the current element and returns the contents as a System.Decimal object.
        //
        // Returns:
        //     The element content as a System.Decimal object.
        //
        // Exceptions:
        //   System.InvalidOperationException:
        //     The System.Xml.XmlReader is not positioned on an element.
        //
        //   System.Xml.XmlException:
        //     The current element contains child elements.-or-The element content cannot
        //     be converted to a System.Decimal.
        //
        //   System.ArgumentNullException:
        //     The method is called with null arguments.
        decimal ReadElementContentAsDecimal();
        //
        // Summary:
        //     Checks that the specified local name and namespace URI matches that of the
        //     current element, then reads the current element and returns the contents
        //     as a System.Decimal object.
        //
        // Parameters:
        //   localName:
        //     The local name of the element.
        //
        //   namespaceURI:
        //     The namespace URI of the element.
        //
        // Returns:
        //     The element content as a System.Decimal object.
        //
        // Exceptions:
        //   System.InvalidOperationException:
        //     The System.Xml.XmlReader is not positioned on an element.
        //
        //   System.Xml.XmlException:
        //     The current element contains child elements.-or-The element content cannot
        //     be converted to a System.Decimal.
        //
        //   System.ArgumentNullException:
        //     The method is called with null arguments.
        //
        //   System.ArgumentException:
        //     The specified local name and namespace URI do not match that of the current
        //     element being read.
        decimal ReadElementContentAsDecimal(string localName, string namespaceURI);
        //
        // Summary:
        //     Reads the current element and returns the contents as a double-precision
        //     floating-Vector2i number.
        //
        // Returns:
        //     The element content as a double-precision floating-Vector2i number.
        //
        // Exceptions:
        //   System.InvalidOperationException:
        //     The System.Xml.XmlReader is not positioned on an element.
        //
        //   System.Xml.XmlException:
        //     The current element contains child elements.-or-The element content cannot
        //     be converted to a double-precision floating-Vector2i number.
        //
        //   System.ArgumentNullException:
        //     The method is called with null arguments.
        double ReadElementContentAsDouble();
        //
        // Summary:
        //     Checks that the specified local name and namespace URI matches that of the
        //     current element, then reads the current element and returns the contents
        //     as a double-precision floating-Vector2i number.
        //
        // Parameters:
        //   localName:
        //     The local name of the element.
        //
        //   namespaceURI:
        //     The namespace URI of the element.
        //
        // Returns:
        //     The element content as a double-precision floating-Vector2i number.
        //
        // Exceptions:
        //   System.InvalidOperationException:
        //     The System.Xml.XmlReader is not positioned on an element.
        //
        //   System.Xml.XmlException:
        //     The current element contains child elements.-or-The element content cannot
        //     be converted to the requested type.
        //
        //   System.ArgumentNullException:
        //     The method is called with null arguments.
        //
        //   System.ArgumentException:
        //     The specified local name and namespace URI do not match that of the current
        //     element being read.
        double ReadElementContentAsDouble(string localName, string namespaceURI);
        //
        // Summary:
        //     Reads the current element and returns the contents as single-precision floating-Vector2i
        //     number.
        //
        // Returns:
        //     The element content as a single-precision floating Vector2i number.
        //
        // Exceptions:
        //   System.InvalidOperationException:
        //     The System.Xml.XmlReader is not positioned on an element.
        //
        //   System.Xml.XmlException:
        //     The current element contains child elements.-or-The element content cannot
        //     be converted to a single-precision floating-Vector2i number.
        //
        //   System.ArgumentNullException:
        //     The method is called with null arguments.
        float ReadElementContentAsFloat();
        //
        // Summary:
        //     Checks that the specified local name and namespace URI matches that of the
        //     current element, then reads the current element and returns the contents
        //     as a single-precision floating-Vector2i number.
        //
        // Parameters:
        //   localName:
        //     The local name of the element.
        //
        //   namespaceURI:
        //     The namespace URI of the element.
        //
        // Returns:
        //     The element content as a single-precision floating Vector2i number.
        //
        // Exceptions:
        //   System.InvalidOperationException:
        //     The System.Xml.XmlReader is not positioned on an element.
        //
        //   System.Xml.XmlException:
        //     The current element contains child elements.-or-The element content cannot
        //     be converted to a single-precision floating-Vector2i number.
        //
        //   System.ArgumentNullException:
        //     The method is called with null arguments.
        //
        //   System.ArgumentException:
        //     The specified local name and namespace URI do not match that of the current
        //     element being read.
        float ReadElementContentAsFloat(string localName, string namespaceURI);
        //
        // Summary:
        //     Reads the current element and returns the contents as a 32-bit signed integer.
        //
        // Returns:
        //     The element content as a 32-bit signed integer.
        //
        // Exceptions:
        //   System.InvalidOperationException:
        //     The System.Xml.XmlReader is not positioned on an element.
        //
        //   System.Xml.XmlException:
        //     The current element contains child elements.-or-The element content cannot
        //     be converted to a 32-bit signed integer.
        //
        //   System.ArgumentNullException:
        //     The method is called with null arguments.
        int ReadElementContentAsInt();
        //
        // Summary:
        //     Checks that the specified local name and namespace URI matches that of the
        //     current element, then reads the current element and returns the contents
        //     as a 32-bit signed integer.
        //
        // Parameters:
        //   localName:
        //     The local name of the element.
        //
        //   namespaceURI:
        //     The namespace URI of the element.
        //
        // Returns:
        //     The element content as a 32-bit signed integer.
        //
        // Exceptions:
        //   System.InvalidOperationException:
        //     The System.Xml.XmlReader is not positioned on an element.
        //
        //   System.Xml.XmlException:
        //     The current element contains child elements.-or-The element content cannot
        //     be converted to a 32-bit signed integer.
        //
        //   System.ArgumentNullException:
        //     The method is called with null arguments.
        //
        //   System.ArgumentException:
        //     The specified local name and namespace URI do not match that of the current
        //     element being read.
        int ReadElementContentAsInt(string localName, string namespaceURI);
        //
        // Summary:
        //     Reads the current element and returns the contents as a 64-bit signed integer.
        //
        // Returns:
        //     The element content as a 64-bit signed integer.
        //
        // Exceptions:
        //   System.InvalidOperationException:
        //     The System.Xml.XmlReader is not positioned on an element.
        //
        //   System.Xml.XmlException:
        //     The current element contains child elements.-or-The element content cannot
        //     be converted to a 64-bit signed integer.
        //
        //   System.ArgumentNullException:
        //     The method is called with null arguments.
        long ReadElementContentAsLong();
        //
        // Summary:
        //     Checks that the specified local name and namespace URI matches that of the
        //     current element, then reads the current element and returns the contents
        //     as a 64-bit signed integer.
        //
        // Parameters:
        //   localName:
        //     The local name of the element.
        //
        //   namespaceURI:
        //     The namespace URI of the element.
        //
        // Returns:
        //     The element content as a 64-bit signed integer.
        //
        // Exceptions:
        //   System.InvalidOperationException:
        //     The System.Xml.XmlReader is not positioned on an element.
        //
        //   System.Xml.XmlException:
        //     The current element contains child elements.-or-The element content cannot
        //     be converted to a 64-bit signed integer.
        //
        //   System.ArgumentNullException:
        //     The method is called with null arguments.
        //
        //   System.ArgumentException:
        //     The specified local name and namespace URI do not match that of the current
        //     element being read.
        long ReadElementContentAsLong(string localName, string namespaceURI);
        //
        // Summary:
        //     Reads the current element and returns the contents as an System.Object.
        //
        // Returns:
        //     A boxed common language runtime (CLR) object of the most appropriate type.
        //     The System.Xml.XmlReader.ValueType property determines the appropriate CLR
        //     type. If the content is typed as a list type, this method returns an array
        //     of boxed objects of the appropriate type.
        //
        // Exceptions:
        //   System.InvalidOperationException:
        //     The System.Xml.XmlReader is not positioned on an element.
        //
        //   System.Xml.XmlException:
        //     The current element contains child elements.-or-The element content cannot
        //     be converted to the requested type
        //
        //   System.ArgumentNullException:
        //     The method is called with null arguments.
        object ReadElementContentAsObject();
        //
        // Summary:
        //     Checks that the specified local name and namespace URI matches that of the
        //     current element, then reads the current element and returns the contents
        //     as an System.Object.
        //
        // Parameters:
        //   localName:
        //     The local name of the element.
        //
        //   namespaceURI:
        //     The namespace URI of the element.
        //
        // Returns:
        //     A boxed common language runtime (CLR) object of the most appropriate type.
        //     The System.Xml.XmlReader.ValueType property determines the appropriate CLR
        //     type. If the content is typed as a list type, this method returns an array
        //     of boxed objects of the appropriate type.
        //
        // Exceptions:
        //   System.InvalidOperationException:
        //     The System.Xml.XmlReader is not positioned on an element.
        //
        //   System.Xml.XmlException:
        //     The current element contains child elements.-or-The element content cannot
        //     be converted to the requested type.
        //
        //   System.ArgumentNullException:
        //     The method is called with null arguments.
        //
        //   System.ArgumentException:
        //     The specified local name and namespace URI do not match that of the current
        //     element being read.
        object ReadElementContentAsObject(string localName, string namespaceURI);
        //
        // Summary:
        //     Reads the current element and returns the contents as a System.String object.
        //
        // Returns:
        //     The element content as a System.String object.
        //
        // Exceptions:
        //   System.InvalidOperationException:
        //     The System.Xml.XmlReader is not positioned on an element.
        //
        //   System.Xml.XmlException:
        //     The current element contains child elements.-or-The element content cannot
        //     be converted to a System.String object.
        //
        //   System.ArgumentNullException:
        //     The method is called with null arguments.
        string ReadElementContentAsString();
        //
        // Summary:
        //     Checks that the specified local name and namespace URI matches that of the
        //     current element, then reads the current element and returns the contents
        //     as a System.String object.
        //
        // Parameters:
        //   localName:
        //     The local name of the element.
        //
        //   namespaceURI:
        //     The namespace URI of the element.
        //
        // Returns:
        //     The element content as a System.String object.
        //
        // Exceptions:
        //   System.InvalidOperationException:
        //     The System.Xml.XmlReader is not positioned on an element.
        //
        //   System.Xml.XmlException:
        //     The current element contains child elements.-or-The element content cannot
        //     be converted to a System.String object.
        //
        //   System.ArgumentNullException:
        //     The method is called with null arguments.
        //
        //   System.ArgumentException:
        //     The specified local name and namespace URI do not match that of the current
        //     element being read.
        string ReadElementContentAsString(string localName, string namespaceURI);
        //
        // Summary:
        //     Reads a text-only element.
        //
        // Returns:
        //     The text contained in the element that was read. An empty string if the element
        //     is empty (<item></item> or <item/>).
        //
        // Exceptions:
        //   System.Xml.XmlException:
        //     The next content node is not a start tag; or the element found does not contain
        //     a simple text value.
        string ReadElementString();
        //
        // Summary:
        //     Checks that the System.Xml.XmlReader.Name property of the element found matches
        //     the given string before reading a text-only element.
        //
        // Parameters:
        //   name:
        //     The name to check.
        //
        // Returns:
        //     The text contained in the element that was read. An empty string if the element
        //     is empty (<item></item> or <item/>).
        //
        // Exceptions:
        //   System.Xml.XmlException:
        //     If the next content node is not a start tag; if the element Name does not
        //     match the given argument; or if the element found does not contain a simple
        //     text value.
        string ReadElementString(string name);
        //
        // Summary:
        //     Checks that the System.Xml.XmlReader.LocalName and System.Xml.XmlReader.NamespaceURI
        //     properties of the element found matches the given strings before reading
        //     a text-only element.
        //
        // Parameters:
        //   localname:
        //     The local name to check.
        //
        //   ns:
        //     The namespace URI to check.
        //
        // Returns:
        //     The text contained in the element that was read. An empty string if the element
        //     is empty (<item></item> or <item/>).
        //
        // Exceptions:
        //   System.Xml.XmlException:
        //     If the next content node is not a start tag; if the element LocalName or
        //     NamespaceURI do not match the given arguments; or if the element found does
        //     not contain a simple text value.
        string ReadElementString(string localname, string ns);
        //
        // Summary:
        //     Checks that the current content node is an end tag and advances the reader
        //     to the next node.
        //
        // Exceptions:
        //   System.Xml.XmlException:
        //     The current node is not an end tag or if incorrect XML is encountered in
        //     the input stream.
        void ReadEndElement();
        //
        // Summary:
        //     When overridden in a derived class, reads all the content, including markup,
        //     as a string.
        //
        // Returns:
        //     All the XML content, including markup, in the current node. If the current
        //     node has no children, an empty string is returned.If the current node is
        //     neither an element nor attribute, an empty string is returned.
        //
        // Exceptions:
        //   System.Xml.XmlException:
        //     The XML was not well-formed, or an error occurred while parsing the XML.
        string ReadInnerXml();
        //
        // Summary:
        //     When overridden in a derived class, reads the content, including markup,
        //     representing this node and all its children.
        //
        // Returns:
        //     If the reader is positioned on an element or an attribute node, this method
        //     returns all the XML content, including markup, of the current node and all
        //     its children; otherwise, it returns an empty string.
        //
        // Exceptions:
        //   System.Xml.XmlException:
        //     The XML was not well-formed, or an error occurred while parsing the XML.
        string ReadOuterXml();
        //
        // Summary:
        //     Checks that the current node is an element and advances the reader to the
        //     next node.
        //
        // Exceptions:
        //   System.Xml.XmlException:
        //     System.Xml.XmlReader.IsStartElement() returns false.
        void ReadStartElement();
        //
        // Summary:
        //     Checks that the current content node is an element with the given System.Xml.XmlReader.Name
        //     and advances the reader to the next node.
        //
        // Parameters:
        //   name:
        //     The qualified name of the element.
        //
        // Exceptions:
        //   System.Xml.XmlException:
        //     System.Xml.XmlReader.IsStartElement() returns false or if the System.Xml.XmlReader.Name
        //     of the element does not match the given name.
        void ReadStartElement(string name);
        //
        // Summary:
        //     Checks that the current content node is an element with the given System.Xml.XmlReader.LocalName
        //     and System.Xml.XmlReader.NamespaceURI and advances the reader to the next
        //     node.
        //
        // Parameters:
        //   localname:
        //     The local name of the element.
        //
        //   ns:
        //     The namespace URI of the element.
        //
        // Exceptions:
        //   System.Xml.XmlException:
        //     System.Xml.XmlReader.IsStartElement() returns false, or the System.Xml.XmlReader.LocalName
        //     and System.Xml.XmlReader.NamespaceURI properties of the element found do
        //     not match the given arguments.
        void ReadStartElement(string localname, string ns);
        //
        // Summary:
        //     When overridden in a derived class, reads the contents of an element or text
        //     node as a string.
        //
        // Returns:
        //     The contents of the element or an empty string.
        //
        // Exceptions:
        //   System.Xml.XmlException:
        //     An error occurred while parsing the XML.
        string ReadString();
        //
        // Summary:
        //     Returns a new XmlReader instance that can be used to read the current node,
        //     and all its descendants.
        //
        // Returns:
        //     A new XmlReader instance set to ReadState.Initial. A call to the System.Xml.XmlReader.Read()
        //     method positions the new XmlReader on the node that was current before the
        //     call to ReadSubtree method.
        //
        // Exceptions:
        //   System.InvalidOperationException:
        //     The XmlReader is not positioned on an element when this method is called.
        IXMLDeserializer ReadSubtree();
        //
        // Summary:
        //     Advances the System.Xml.XmlReader to the next descendant element with the
        //     specified qualified name.
        //
        // Parameters:
        //   name:
        //     The qualified name of the element you wish to move to.
        //
        // Returns:
        //     true if a matching descendant element is found; otherwise false. If a matching
        //     child element is not found, the System.Xml.XmlReader is positioned on the
        //     end tag (System.Xml.XmlReader.NodeType is XmlNodeType.EndElement) of the
        //     element.If the System.Xml.XmlReader is not positioned on an element when
        //     System.Xml.XmlReader.ReadToDescendant(System.String) was called, this method
        //     returns false and the position of the System.Xml.XmlReader is not changed.
        bool ReadToDescendant(string name);
        //
        // Summary:
        //     Advances the System.Xml.XmlReader to the next descendant element with the
        //     specified local name and namespace URI.
        //
        // Parameters:
        //   localName:
        //     The local name of the element you wish to move to.
        //
        //   namespaceURI:
        //     The namespace URI of the element you wish to move to.
        //
        // Returns:
        //     true if a matching descendant element is found; otherwise false. If a matching
        //     child element is not found, the System.Xml.XmlReader is positioned on the
        //     end tag (System.Xml.XmlReader.NodeType is XmlNodeType.EndElement) of the
        //     element.If the System.Xml.XmlReader is not positioned on an element when
        //     System.Xml.XmlReader.ReadToDescendant(System.String,System.String) was called,
        //     this method returns false and the position of the System.Xml.XmlReader is
        //     not changed.
        bool ReadToDescendant(string localName, string namespaceURI);
        //
        // Summary:
        //     Reads until an element with the specified qualified name is found.
        //
        // Parameters:
        //   name:
        //     The qualified name of the element.
        //
        // Returns:
        //     true if a matching element is found; otherwise false and the System.Xml.XmlReader
        //     is in an end of file state.
        bool ReadToFollowing(string name);
        //
        // Summary:
        //     Reads until an element with the specified local name and namespace URI is
        //     found.
        //
        // Parameters:
        //   localName:
        //     The local name of the element.
        //
        //   namespaceURI:
        //     The namespace URI of the element.
        //
        // Returns:
        //     true if a matching element is found; otherwise false and the System.Xml.XmlReader
        //     is in an end of file state.
        bool ReadToFollowing(string localName, string namespaceURI);
        //
        // Summary:
        //     Advances the XmlReader to the next sibling element with the specified qualified
        //     name.
        //
        // Parameters:
        //   name:
        //     The qualified name of the sibling element you wish to move to.
        //
        // Returns:
        //     true if a matching sibling element is found; otherwise false. If a matching
        //     sibling element is not found, the XmlReader is positioned on the end tag
        //     (System.Xml.XmlReader.NodeType is XmlNodeType.EndElement) of the parent element.
        bool ReadToNextSibling(string name);
        //
        // Summary:
        //     Advances the XmlReader to the next sibling element with the specified local
        //     name and namespace URI.
        //
        // Parameters:
        //   localName:
        //     The local name of the sibling element you wish to move to.
        //
        //   namespaceURI:
        //     The namespace URI of the sibling element you wish to move to
        //
        // Returns:
        //     true if a matching sibling element is found; otherwise false. If a matching
        //     sibling element is not found, the XmlReader is positioned on the end tag
        //     (System.Xml.XmlReader.NodeType is XmlNodeType.EndElement) of the parent element.
        bool ReadToNextSibling(string localName, string namespaceURI);
        //
        // Summary:
        //     Reads large streams of text embedded in an XML document.
        //
        // Parameters:
        //   buffer:
        //     The array of characters that serves as the buffer to which the text contents
        //     are written. This value cannot be null.
        //
        //   index:
        //     The offset within the buffer where the System.Xml.XmlReader can start to
        //     copy the results.
        //
        //   count:
        //     The maximum number of characters to copy into the buffer. The actual number
        //     of characters copied is returned from this method.
        //
        // Returns:
        //     The number of characters read into the buffer. The value zero is returned
        //     when there is no more text content.
        //
        // Exceptions:
        //   System.InvalidOperationException:
        //     The current node does not have a value (System.Xml.XmlReader.HasValue is
        //     false).
        //
        //   System.ArgumentNullException:
        //     The buffer value is null.
        //
        //   System.ArgumentOutOfRangeException:
        //     The index into the buffer, or index + count is larger than the allocated
        //     buffer Size2i.
        //
        //   System.NotSupportedException:
        //     The System.Xml.XmlReader implementation does not support this method.
        //
        //   System.Xml.XmlException:
        //     The XML data is not well-formed.
        int ReadValueChunk(char[] buffer, int index, int count);
        //
        // Summary:
        //     When overridden in a derived class, resolves the entity reference for EntityReference
        //     nodes.
        //
        // Exceptions:
        //   System.InvalidOperationException:
        //     The reader is not positioned on an EntityReference node; this implementation
        //     of the reader cannot resolve entities (System.Xml.XmlReader.CanResolveEntity
        //     returns false).
        void ResolveEntity();
        //
        // Summary:
        //     Skips the children of the current node.
        void Skip();
    }
}

