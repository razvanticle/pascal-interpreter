// See https://aka.ms/new-console-template for more information

using PascalInterpreter;

while (true)
{
    try
    {
        Console.Write("calc> ");
        var input = Console.ReadLine();
        if (!string.IsNullOrEmpty(input))
        {
            var lexer = new Lexer(input);
            var interpreter = new Interpreter(lexer);
            var result = interpreter.Expr();
            Console.WriteLine($"Result: {result}");
        }

    }
    catch (Exception e)
    {
        Console.WriteLine(e);
        throw;
    }
}