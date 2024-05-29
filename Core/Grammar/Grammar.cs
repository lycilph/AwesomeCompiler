using Core.RegularExpressions;

namespace Core.Grammar;

public class Grammar
{
    //private readonly Lexer lexer;

    //public Grammar()
    //{
    //    var lexer_generator = new LexerGenerator();
    //    lexer_generator.Add(new Regex(@"[a-zA-Z_][a-zA-Z_0-9]*"), "Identifier");
    //    lexer_generator.Add(new Regex("\"[^\"]*\""), "String");
    //    lexer_generator.Add(new Regex("[ \n\r\t]+"), "Whitespace", skip: true);
    //    lexer_generator.Add(new Regex(@"//[^\n]*\n"), "Comment", skip: true);
    //    lexer_generator.Add(new Regex(@"\|"), "Choice");
    //    lexer_generator.Add(new Regex(@"\*"), "Star");
    //    lexer_generator.Add(new Regex(@"\+"), "Plus");
    //    lexer_generator.Add(new Regex(@"\?"), "Optional");
    //    lexer_generator.Add(new Regex(@"\("), "Left_parenthesis");
    //    lexer_generator.Add(new Regex(@"\)"), "Right_parenthesis");
    //    lexer_generator.Add(new Regex(":"), "Colon");
    //    lexer_generator.Add(new Regex(";"), "Semi_colon");
    //    lexer = lexer_generator.Generate();
    //}

    //public List<string> Tokenize(string input, bool verbose_output = false)
    //{
    //    return lexer.Run(input, verbose_output);
    //}
}
