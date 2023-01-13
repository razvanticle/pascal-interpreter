namespace PascalInterpreter.Parsing;

public class Compound : Ast
{
    public Compound()
    {
        Children = new List<Ast>();
    }

    public List<Ast> Children { get; }
}