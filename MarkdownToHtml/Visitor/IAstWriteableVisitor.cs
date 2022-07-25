using Irony.Interpreter.Ast;

namespace MarkdownToHtml.Visitor;

public interface IAstWriteableVisitor : IAstVisitor
{
    void Write(string code);
    void WriteLine(string code);
}
