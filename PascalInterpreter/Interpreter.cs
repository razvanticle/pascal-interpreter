namespace PascalInterpreter;

public class Interpreter
{
    private readonly Lexer lexer;

    private Token currentToken;


    public Interpreter(Lexer lexer)
    {
        this.lexer = lexer;

        currentToken = lexer.GetNextToken();
    }


    /// <summary>
    ///     compare the current token type with the passed token
    ///     type and if they match then "eat" the current token
    ///     and assign the next token to the self.current_token,
    ///     otherwise raise an exception.
    /// </summary>
    private void Eat(TokenType tokenType)
    {
        currentToken = currentToken.Type == tokenType
            ? lexer.GetNextToken()
            : throw new Exception("Error parsing input");
    }

    public object Factor()
    {
        var token = currentToken;

        if (token.Type == TokenType.INTEGER)
        {
            Eat(TokenType.INTEGER);
            return token.Value;
        }

        if(token.Type==TokenType.LPAREN)
        {
            Eat(TokenType.LPAREN);
            var result = Expr();
            Eat(TokenType.RPAREN);

            return result;
        }
        
        throw new Exception("Error parsing input");
    }

    private object Term()
    {
        var result = (int)Factor();

        while (new[] { TokenType.MUL, TokenType.DIV }.Contains(currentToken.Type))
        {
            var token = currentToken;
            if (token.Type == TokenType.MUL)
            {
                Eat(TokenType.MUL);
                result *= (int)Factor();
            }

            if (token.Type == TokenType.DIV)
            {
                Eat(TokenType.DIV);
                result /= (int)Factor();
            }
        }

        return result;
    }

    public int Expr()
    {
        var result = (int)Term();
        while (new[] { TokenType.PLUS, TokenType.MINUS }.Contains(currentToken.Type))
        {
            var token = currentToken;
            if (token.Type == TokenType.PLUS)
            {
                Eat(TokenType.PLUS);
                result += (int)Term();
            }

            if (token.Type == TokenType.MINUS)
            {
                Eat(TokenType.MINUS);
                result -= (int)Term();
            }
        }

        return result;
    }
}