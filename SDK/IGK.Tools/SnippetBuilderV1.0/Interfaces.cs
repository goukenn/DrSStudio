using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace WinSnippetBuilder
{
    public interface ISnippetBuilder
    {
        ICodeSnippetCollection CodeSnippets { get; }
        void Save(string filename);
        void Open(string filename);
    }
    public interface ISnippetReader //snippet items
    {
        void WriteXml(XmlWriter write);
        void ReadXml(XmlReader read);
    }
    public interface ISnippetHeader : ISnippetReader
    {

        string Author { get; set; }
        ISnippetKeywordCollection Keywords { get; }
        string HelpUrl { get; set; }
        string Description { get; set; }
        string Shortcut { get; set; }
        ISnippetTypeCollection SnippetTypes { get; }
        string Title { get; set; }

    }
    /// <summary>
    /// represent the snippet type
    /// </summary>
    public interface ISnippetType : ISnippetReader
    {
        /// <summary>
        /// get or set the value of the snippet
        /// </summary>
        string Value { get; set; }
    }
    public interface ISnippetKeyWord : ISnippetReader
    {
        /// <summary>
        /// get or set the value of the key word
        /// </summary>
        string Value { get; set; }
    }
    /// <summary>
    /// represent the snippet type collection
    /// </summary>
    public interface ISnippetTypeCollection : IEnumerable, ISnippetItemCollection
    {
        void Add(ISnippetType snippettype);
        void Remove(ISnippetType snippettype);
        //int Count { get; }
        ISnippetType this[int index] { get; }
    }

    public interface ISnippetKeywordCollection : IEnumerable, ISnippetItemCollection
    {
        void Add(ISnippetKeyWord snippettype);
        void Remove(ISnippetKeyWord snippettype);
        ISnippetKeyWord this[int index] { get; }
    }

    public interface ISnippetDefinition : ISnippetReader
    {
        ISnippetReferenceCollection References { get; }
        ISnippetCode Code { get; }
        ISnippetImportCollection Imports { get; }
        ISnippetDeclarationCollection Declarations { get; }
    }
    public interface ICodeSnippet : ISnippetReader
    {
        string Format { get; set; }
        ISnippetHeader Header { get; }
        ISnippetDefinition Snippet { get; }
    }
    public interface ICodeSnippetCollection : IEnumerable
    {
        void Add(ICodeSnippet snippet);
        void Remove(ICodeSnippet snippet);
        int Count { get; }
        ICodeSnippet this[int index] { get; }

        void Clear();
    }
    public interface ISnippetReferenceCollection : IEnumerable, ISnippetItemCollection
    {
        void Add(ISnippetReference snippet);
        void Remove(ISnippetReference snippet);
        //int Count { get; }
        ISnippetReference this[int index] { get; }
        //void Clear();
    }


    public interface ISnippetImportCollection : IEnumerable
    {
        void Add(ISnippetImport snippet);
        void Remove(ISnippetImport snippet);
        int Count { get; }
        ISnippetImport this[int index] { get; }

        void Clear();
    }
    public interface ISnippetImport : ISnippetReader
    {
        string Value { get; set; }

    }
    public interface ISnippetReference : ISnippetReader
    {
        /// <summary>
        /// get the type of the reference
        /// </summary>
        string Type { get; }
        /// <summary>
        /// get or set the snippet value
        /// </summary>
        string Value { get; set; }
    }
    public interface ISnippetCode : ISnippetReader
    {
        enuSnippetCode Language { get; set; }
        string Text { get; set; }
    }

    public interface ISnippetDeclarationItem : ISnippetReader
    {
        string ID { get; set; }
        string ToolTip { get; set; }
        string Default { get; set; }
        string Type { get; set; }
    }

    public interface ISnippetItemCollection : IEnumerable
    {
        void Add(ISnippetReader item);
        void Remove(ISnippetReader item);
        void Clear();
        int Count { get; }

    }
    public interface ISnippetDeclarationCollection : IEnumerable
    {
        void Add(ISnippetDeclarationItem item);
        void Remove(ISnippetDeclarationItem item);
        int Count { get; }
        ISnippetDeclarationItem this[int index] { get; }

    }
}
