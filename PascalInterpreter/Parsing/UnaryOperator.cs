namespace PascalInterpreter.Parsing;

public class UnaryOperator : Ast
{
    public UnaryOperator(Token op, Ast expression)
    {
        Op = op;
        Expression = expression;
    }

    public Token Op { get; }
    public Ast Expression { get; }
}