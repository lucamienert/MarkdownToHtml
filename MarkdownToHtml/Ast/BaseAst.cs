using Irony.Ast;
using Irony.Interpreter.Ast;
using Irony.Parsing;

namespace MarkdownToHtml.Ast;

public class BaseAst : AstNode
{
    public virtual string StartTag { get; } = "";
    public string EndTag => StartTag.Replace("<", @"</");

    public override void Init(AstContext context, ParseTreeNode treeNode)
    {
        base.Init(context, treeNode);

        Type previousNodeType = null;

        UnorderedListAst lastContainer = null;
        treeNode.ChildNodes
            .Select(x =>
            {
                var abc = x.AstNode as BaseAst;
                var toReturn = abc;

                if (abc is ListItemAst li)
                    toReturn = AddToContainer(li, previousNodeType, ref lastContainer);

                previousNodeType = abc?.GetType();
                return toReturn;
            })
            .Where(x => x != null).ToList()
            .ForEach(childeNode => {
                ChildNodes.Add(childeNode);
                childeNode.Parent = treeNode.AstNode as AstNode;
            });
    }

    private BaseAst? AddToContainer(ListItemAst li, Type previousNodeType, ref UnorderedListAst lastContainer)
    {
        var listitem = li.GetType();
        if (previousNodeType == listitem)
        {
            lastContainer.ChildNodes.Add(li);
            li.Parent = lastContainer;
            return null;
        }
        else
        {
            li.Container = new UnorderedListAst();
            li.Container.ChildNodes.Add(li);
            li.Parent = li.Container;
            lastContainer = li.Container;
            return li.Container;
        }
    }
}
