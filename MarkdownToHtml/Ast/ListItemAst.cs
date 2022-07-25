namespace MarkdownToHtml.Ast;

public class ListItemAst : BaseAst
{
    public UnorderedListAst Container { get; set; }
    public override string StartTag => "<li>";
}
