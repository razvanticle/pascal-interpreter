namespace PascalInterpreter.Parsing;

public class BinaryOperator : Ast
{
    public BinaryOperator(Ast left, Token op, Ast right)
    {
        Left = left;
        Op = op;
        Right = right;
    }

    public Ast Left { get; }
    public Token Op { get; }
    public Ast Right { get; }
}