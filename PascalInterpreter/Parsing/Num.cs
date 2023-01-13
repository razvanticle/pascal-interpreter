namespace PascalInterpreter.Parsing;

public class Num : Ast
{
    private readonly Token token;

    public Num(Token token)
    {
        this.token = token;
        Value = token.Value;
    }

    public object Value { get; }
}