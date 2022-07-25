using Irony.Parsing;

namespace MarkdownToHtml;

public class NonTerminalMarkdown : NonTerminal
{
    public NonTerminalMarkdown(string name) : base(name)
    {
    }

    public NonTerminalMarkdown(string name, Type nodeType) : base(name, nodeType)
        => AstConfig.DefaultNodeCreator = () => Activator.CreateInstance(nodeType);
}
