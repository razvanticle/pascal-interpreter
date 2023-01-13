namespace PascalInterpreter;

public enum TokenType
{
    INTEGER,
    PLUS,
    MINUS,
    MUL,
    DIV,
    EOF,
    LPAREN,
    RPAREN,
    
    BEGIN,
    END,
    ID,
    ASSIGN,
    SEMICOLON,
    DOT
}