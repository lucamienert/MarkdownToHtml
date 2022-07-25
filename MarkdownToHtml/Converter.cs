using Irony.Parsing;
using MarkdownToHtml.Ast;
using MarkdownToHtml.Visitor;
using System.Text;

namespace MarkdownToHtml;

public class Converter
{
    private readonly MarkdownGrammar _markdownGrammar;
    private readonly LanguageData _languageData;
    private readonly Parser _parser;

    public Converter()
    {
        _markdownGrammar = new MarkdownGrammar();
        _languageData = new LanguageData(_markdownGrammar);
        _parser = new Parser(_languageData);
    }

    public string ConvertToHtml(string str)
    {
        var parseTree = _parser.Parse(str);

        if (parseTree.HasErrors())
            return $"{_parser.Context.Status}";

        var root = parseTree.Root.AstNode as BaseAst;
        var visitor = new HtmlConcreteVisitor(new StringBuilder());
        root!.AcceptVisitor(visitor);
        return visitor.CompiledHtml;
    }
}
