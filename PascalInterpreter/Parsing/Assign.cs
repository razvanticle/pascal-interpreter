namespace PascalInterpreter.Parsing;

public class Assign : Ast
{
    public Assign(Var left, Token op, Ast right)
    {
        Left = left;
        Op = op;
        Right = right;
    }

    public Var Left { get; }
    public Token Op { get; }
    public Ast Right { get; }
}