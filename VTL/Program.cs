using System;
using System.Collections.Generic;
using System.IO;

namespace VTL
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Length > 0)
            {
                string FileName = args[0];
                string SourceCode = File.ReadAllText(FileName);

                // Lexical Analysis (Lexer)
                List<Token> TokenList = Lexer.Tokenize(SourceCode);

                // Semantic Analysis (Parser)
                Node AST = Parser.Parse(TokenList);

                // Interpret AST (Interpreter)
                Interpreter.Interpret(AST);
            }
        }
    }
}
