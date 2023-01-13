using PascalInterpreter.Parsing;

namespace PascalInterpreter.Tests;

public class AstInterpreterTests
{
    // [TestCase("2+5", 7)]
    // [TestCase("3+9", 12)]
    // [TestCase("27 - 7", 20)]
    // [TestCase("7 - 3 + 2 - 1", 5)]
    // [TestCase("10 + 1 + 2 - 3 + 4 + 6 - 15", 5)]
    //
    // [TestCase("3 * 7", 21)]
    // [TestCase("20 / 2", 10)]
    // [TestCase("7 * 4 / 2", 14)]
    // [TestCase("7 * 4 / 2 * 3", 42)]
    // [TestCase("10 * 4  * 2 * 3 / 8", 30)]
    //
    // [TestCase("2 + 7 * 4", 30)]
    // [TestCase(" 7 - 8 / 4", 5)]
    // [TestCase("14 + 2 * 3 - 6 / 2", 17)]
    //
    // [TestCase("7 + 3 * (10 / (12 / (3 + 1) - 1))", 22)]
    // [TestCase("7 + 3 * (10 / (12 / (3 + 1) - 1)) / (2 + 3) - 5 - 3 + (8)", 10)]
    // [TestCase(" 7 + (((3 + 2)))", 12)]
    //
    // [TestCase(" - 3 ", -3)]
    // [TestCase(" + 3", 3)]
    // [TestCase("5 - - - + - 3", 8)]
    // [TestCase("5 - - - + - (3 + 4) - +2", 10)]
    // public void WhenArithmeticExpression_ReturnResult(string input, int expected)
    // {
    //     // arrange
    //     var lexer = new Lexer(input);
    //     var parser = new Parser(lexer);
    //     var sut = new AstInterpreter(parser);
    //     
    //     // act
    //     var actual = sut.Interpret();
    //     
    //     // assert
    //     Assert.That(actual, Is.EqualTo(expected));
    // }

    [Test]
    public void ProgramTests()
    {
        // arrange
        var input = @"BEGIN
                        BEGIN
                            number := 2;
                            a := number;
                            b := 10 * a + 10 * number / 4;
                            c := a - - b
                        END;
                        x := 11;
                    END.";
        
        var lexer = new Lexer(input);
        var parser = new Parser(lexer);
        var sut = new AstInterpreter(parser);
        
        // act
        var actual = sut.Interpret();
    }
}