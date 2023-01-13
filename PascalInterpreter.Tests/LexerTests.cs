namespace PascalInterpreter.Tests;

public class LexerTests
{
    [Test]
    public void WhenVariableIsAssignedInCompoundStatement_ReturnProperTokens()
    {
        // arrange
        var statement = "BEGIN a:=2; END.";

        var sut = new Lexer(statement);
        
        // assert
        Assert.That(sut.GetNextToken(), Is.EqualTo(new Token(TokenType.BEGIN, "BEGIN")));
        Assert.That(sut.GetNextToken(), Is.EqualTo(new Token(TokenType.ID, "a")));
        Assert.That(sut.GetNextToken(), Is.EqualTo(new Token(TokenType.ASSIGN, ":=")));
        Assert.That(sut.GetNextToken(), Is.EqualTo(new Token(TokenType.INTEGER, 2)));
        Assert.That(sut.GetNextToken(), Is.EqualTo(new Token(TokenType.SEMICOLON, ';')));
        Assert.That(sut.GetNextToken(), Is.EqualTo(new Token(TokenType.END, "END")));
        Assert.That(sut.GetNextToken(), Is.EqualTo(new Token(TokenType.DOT, '.')));
        Assert.That(sut.GetNextToken(), Is.EqualTo(new Token(TokenType.EOF, 0)));
    }
}