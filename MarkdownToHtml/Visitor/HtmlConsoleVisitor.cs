using Irony.Interpreter.Ast;
using MarkdownToHtml.Ast;

namespace MarkdownToHtml.Visitor;

public class HtmlConsoleVisitor : IAstWriteableVisitor
{
    public void BeginVisit(IVisitableNode node)
    {
        var nodex = (node as BaseAst);
        Write(nodex!.StartTag);
    }

    public void EndVisit(IVisitableNode node)
    {
        var nodex = (node as BaseAst);
        Write(nodex!.EndTag);
    }

    public void Write(string code)
    {
        Console.Write(code);
    }

    public void WriteLine(string code)
    {
        Console.WriteLine(code);
    }
}
