using PascalInterpreter.Parsing;

namespace PascalInterpreter;

public class AstInterpreter : NodeVisitor
{
    private readonly Parser parser;
    public IDictionary<string, object> GLOBAL_SCOPE { get; }

    public AstInterpreter(Parser parser)
    {
        this.parser = parser;

        GLOBAL_SCOPE = new Dictionary<string, object>();
    }

    private int VisitBinaryOperator(BinaryOperator node)
    {
        return node.Op.Type switch
        {
            TokenType.PLUS => (int)Visit(node.Left) + (int)Visit(node.Right),
            TokenType.MINUS => (int)Visit(node.Left) - (int)Visit(node.Right),
            TokenType.MUL => (int)Visit(node.Left) * (int)Visit(node.Right),
            TokenType.DIV => (int)Visit(node.Left) / (int)Visit(node.Right),
            _ => throw new Exception("Error parsing input")
        };
    }

    private int VisitNum(Num num)
    {
        return (int)num.Value;
    }

    private int VisitUnaryOperator(UnaryOperator node)
    {
        var op = node.Op.Type;
        if (op == TokenType.PLUS)
        {
            return +1 * (int)Visit(node.Expression);
        }

        if (op == TokenType.MINUS)
        {
            return -1 * (int)Visit(node.Expression);
        }

        throw new Exception("Error parsing input");
    }

    private void VisitCompound(Compound node)
    {
        foreach (var child in node.Children)
        {
            Visit(child);
        }
    }
    
    private void VisitNoOp(NoOp node){}

    private void VisitAssign(Assign node)
    {
        var varName = node.Left.Value.ToString();
        GLOBAL_SCOPE[varName] = Visit(node.Right);
    }

    private int VisitVar(Var node)
    {
        var varName = node.Value.ToString();
        GLOBAL_SCOPE.TryGetValue(varName, out var value);
        if (value == null)
        {
            throw new Exception("Error visiting variable");
        }

        return (int)value;
    }

    public object? Interpret()
    {
        var tree = parser.Parse();
        return Visit(tree);
    }
}