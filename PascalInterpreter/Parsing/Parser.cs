namespace PascalInterpreter.Parsing;

public class Parser
{
    private readonly Lexer lexer;
    private Token currentToken;

    public Parser(Lexer lexer)
    {
        this.lexer = lexer;

        currentToken = lexer.GetNextToken();
    }

    public Ast Parse()
    {
        var node = Program();
        if (currentToken.Type != TokenType.EOF)
        {
            throw new Exception("Error parsing input");
        }
        
        return node;
    }

    private Ast Empty()
    {
        return new NoOp();
    }

    private Var Variable()
    {
        var node = new Var(currentToken);
        Eat(TokenType.ID);

        return node;
    }

    private Ast AssigmentStatement()
    {
        var left = Variable();
        var token = currentToken;
        Eat(TokenType.ASSIGN);
        var right = Expr();
        var node = new Assign(left, token, right);

        return node;
    }

    private Ast Statement()
    {
        if (currentToken.Type == TokenType.BEGIN)
        {
            return CompoundStatement();
        }

        if (currentToken.Type == TokenType.ID)
        {
            return AssigmentStatement();
        }

        return Empty();
    }

    private IEnumerable<Ast> StatementList()
    {
        var node = Statement();
        var results = new List<Ast>
        {
            node
        };

        while (currentToken.Type==TokenType.SEMICOLON)
        {
            Eat(TokenType.SEMICOLON);
            results.Add(Statement());
        }

        if (currentToken.Type == TokenType.ID)
        {
            throw new Exception("Error parsing input");
        }

        return results;
    }

    private Ast CompoundStatement()
    {
        Eat(TokenType.BEGIN);
        var nodes = StatementList();
        Eat(TokenType.END);

        var root = new Compound();
        foreach (var node in nodes)
        {
            root.Children.Add(node);
        }

        return root;
    }

    private Ast Program()
    {
        var node = CompoundStatement();
        Eat(TokenType.DOT);

        return node;
    }

    private void Eat(TokenType tokenType)
    {
        currentToken = currentToken.Type == tokenType ? lexer.GetNextToken() : throw new Exception("Invalid syntax");
    }

    private Ast Factor()
    {
        var token = currentToken;

        if (token.Type == TokenType.PLUS)
        {
            Eat(TokenType.PLUS);

            return new UnaryOperator(token, Factor());
        }

        if (token.Type == TokenType.MINUS)
        {
            Eat(TokenType.MINUS);

            return new UnaryOperator(token, Factor());
        }

        if (token.Type == TokenType.INTEGER)
        {
            Eat(TokenType.INTEGER);
            return new Num(token);
        }

        if (token.Type == TokenType.LPAREN)
        {
            Eat(TokenType.LPAREN);
            var node = Expr();
            Eat(TokenType.RPAREN);

            return node;
        }

        return Variable();
    }

    private Ast Term()
    {
        var node = Factor();

        while (new[] { TokenType.MUL, TokenType.DIV }.Contains(currentToken.Type))
        {
            var token = currentToken;
            if (token.Type == TokenType.MUL) Eat(TokenType.MUL);
            if (token.Type == TokenType.DIV) Eat(TokenType.DIV);

            node = new BinaryOperator(node, token, Factor());
        }

        return node;
    }

    private Ast Expr()
    {
        var node = Term();
        while (new[] { TokenType.PLUS, TokenType.MINUS }.Contains(currentToken.Type))
        {
            var token = currentToken;
            if (token.Type == TokenType.PLUS) Eat(TokenType.PLUS);

            if (token.Type == TokenType.MINUS) Eat(TokenType.MINUS);

            node = new BinaryOperator(node, token, Term());
        }

        return node;
    }
}