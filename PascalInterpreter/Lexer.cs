using System.Text;

namespace PascalInterpreter;

public class Lexer
{
    private readonly string text;
    private int position;
    private char? currentChar;

    private IDictionary<string, Token> reservedKeywords = new Dictionary<string, Token>
    {
        { "BEGIN", new Token(TokenType.BEGIN, "BEGIN") },
        { "END", new Token(TokenType.END, "END") }
    };

    public Lexer(string text)
    {
        this.text = text;
        position = 0;
        currentChar = text[position];
    }

    /// <summary>
    ///     Lexical analyzer (also known as scanner or tokenizer)
    ///     This method is responsible for breaking a sentence
    ///     apart into tokens. One token at a time.
    /// </summary>
    public Token GetNextToken()
    {
        while (currentChar != null)
        {
            if (char.IsWhiteSpace(currentChar.Value))
            {
                SkipWhiteSpace();
                continue;
            }

            

            if (currentChar == ':' && Peek() == '=')
            {
                Advance();
                Advance();
                return new Token(TokenType.ASSIGN, ":=");
            }
            
            if (currentChar == ';')
            {
                Advance();
                return new Token(TokenType.SEMICOLON, ';');
            }
            
            if (currentChar == '.')
            {
                Advance();
                return new Token(TokenType.DOT, '.');
            }

            if (char.IsDigit(currentChar.Value))
            {
                return new Token(TokenType.INTEGER, ParseInteger());
            }
            
            if (char.IsLetterOrDigit(currentChar.Value))
            {
                return Identifier();
            }

            if (currentChar == '+')
            {
                var token = new Token(TokenType.PLUS, currentChar);
                Advance();
                return token;
            }

            if (currentChar == '-')
            {
                var token = new Token(TokenType.MINUS, currentChar);
                Advance();
                return token;
            }
            
            if (currentChar == '*')
            {
                var token = new Token(TokenType.MUL, currentChar);
                Advance();
                return token;
            }
            
            if (currentChar == '/')
            {
                var token = new Token(TokenType.DIV, currentChar);
                Advance();
                return token;
            }
            
            if (currentChar == '(')
            {
                var token = new Token(TokenType.LPAREN, currentChar);
                Advance();
                return token;
            }
            
            if (currentChar == ')')
            {
                var token = new Token(TokenType.RPAREN, currentChar);
                Advance();
                return token;
            }
        }

        return new Token(TokenType.EOF, 0);
    }

    private char? Peek()
    {
        var peekPosition = position + 1;
        if (peekPosition > text.Length - 1)
        {
            return null;
        }

        return text[peekPosition];
    }

    private Token Identifier()
    {
        var resultBuilder = new StringBuilder();
        while (currentChar!=null && char.IsLetterOrDigit(currentChar.Value))
        {
            resultBuilder.Append(currentChar);
            Advance();
        }

        var result = resultBuilder.ToString();
        return reservedKeywords.TryGetValue(result, out Token? value) ? 
            value : 
            new Token(TokenType.ID, result);
    }
    
    /// <summary>
    /// Advance the 'position' pointer and set the 'currentChar'.
    /// </summary>
    private void Advance()
    {
        position += 1;

        currentChar = position > text.Length - 1 ? null : text[position];
    }

    private void SkipWhiteSpace()
    {
        while (currentChar != null && char.IsWhiteSpace(currentChar.Value))
        {
            Advance();
        }
    }

    private int ParseInteger()
    {
        var result = new StringBuilder();
        while (currentChar != null && char.IsDigit(currentChar.Value))
        {
            result.Append(currentChar);
            Advance();
        }

        if (int.TryParse(result.ToString(), out var intResult))
        {
            return intResult;
        }

        throw new Exception("Error parsing input");
    }
}