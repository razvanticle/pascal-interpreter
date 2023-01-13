namespace PascalInterpreter.Parsing;

public class Var : Ast
{
    public Var(Token token)
    {
        Token = token;
        Value = token.Value;
    }

    public Token Token { get; }

    public object Value { get; }
}