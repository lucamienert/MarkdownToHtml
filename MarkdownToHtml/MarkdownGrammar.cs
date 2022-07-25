using Irony.Parsing;
using MarkdownToHtml.Ast;

namespace MarkdownToHtml;

public class MarkdownGrammar : Grammar
{
    private readonly NonTerminal Bold = new NonTerminalMarkdown(nameof(Bold), typeof(BoldAst));
    private readonly NonTerminal Italics = new NonTerminalMarkdown(nameof(Italics), typeof(ItalicAst));
    private readonly NonTerminal Underscore = new NonTerminalMarkdown(nameof(Underscore), typeof(UnderscoreAst));
    private readonly NonTerminal StyledText = new NonTerminalMarkdown(nameof(StyledText));
    private readonly NonTerminal Text = new NonTerminalMarkdown(nameof(Text));

    private readonly NonTerminal H1 = new NonTerminalMarkdown(nameof(H1), typeof(H1Ast));
    private readonly NonTerminal H2 = new NonTerminalMarkdown(nameof(H2), typeof(H2Ast));
    private readonly NonTerminal H3 = new NonTerminalMarkdown(nameof(H3), typeof(H3Ast));

    private readonly NonTerminal ListItem = new NonTerminalMarkdown(nameof(ListItem), typeof(ListItemAst));

    private readonly NonTerminal MarkdownElement = new NonTerminalMarkdown(nameof(MarkdownElement));
    private readonly NonTerminal MarkdownContent = new NonTerminalMarkdown(nameof(MarkdownContent), typeof(RootAst));

    private readonly Terminal PlainText = new StringLiteral(nameof(PlainText), "\"", StringOptions.AllowsLineBreak, typeof(TextAst));

    public MarkdownGrammar()
    {
        Bold.Rule = "*" + Text + "*";
        Italics.Rule = "/" + Text + "/";
        Underscore.Rule = "_" + Text + "_";
        StyledText.Rule = Bold | Italics | Underscore;
        Text.Rule = PlainText | StyledText;

        H1.Rule = "#" + Text;
        H2.Rule = "##" + Text;
        H3.Rule = "###" + Text;

        ListItem.Rule = "-" + Text;

        MarkdownElement.Rule = Text | H1 | H2 | H3 | ListItem;
        MarkdownContent.Rule = MakeStarRule(MarkdownContent, MarkdownElement);
        Root = MarkdownContent;

        MarkTransient(StyledText, Text, MarkdownElement);

        LanguageFlags = LanguageFlags.CreateAst;
    }
}
