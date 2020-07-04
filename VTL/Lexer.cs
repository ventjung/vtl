using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Text;

namespace VTL
{
    public static class Lexer
    {
        public static bool CheckForKeyword(string str)
        {
            bool result = false;
            switch (str)
            {
                case "buat":
                    {
                        return true;
                    };
                case "tampilkan":
                    {
                        return true;
                    };
                case "titik":
                    {
                        return true;
                    };
                default:
                    break;
            }
            return result;
        }

        public static bool CheckForOperator(string str)
        {
            bool result = false;
            switch(str)
            {
                case "isi":
                    {
                        return true;
                    };
                case "tambah":
                    {
                        return true;
                    };
                default:
                    break;
            }
            return result;
        }

        public static bool CheckForInteger(string str)
        {
            bool result = false;

            // check for integer
            int intValue = 0;
            bool intParse = int.TryParse(str, out intValue);
            if(intParse == true)
            {
                result = true;
            }

            return result;
        }

        public static List<Token> Tokenize(string SourceCode)
        {
            List<Token> Result = new List<Token>();

            string scanned = string.Empty;
            foreach(char EachChar in SourceCode)
            {
                // proses semua karakter kecuali spasi, \r dan \n
                if(EachChar != ' ' && EachChar != '\r' && EachChar != '\n')
                {
                    scanned += EachChar;
                }
                else
                {
                    if(scanned != string.Empty)
                    {
                        if (CheckForInteger(scanned) == true)
                        {
                            // Literal - Integer
                            Result.Add(new Token() { Name = "Integer", Value = scanned });
                            scanned = string.Empty;
                        }
                        else
                        {
                            // identifier
                            Result.Add(new Token() { Name = "Identifier", Value = scanned });
                            scanned = string.Empty;
                        }
                    }
                }

                if (CheckForOperator(scanned) == true)
                {
                    // check for operator
                    Result.Add(new Token() { Name = "Operator", Value = scanned });
                    scanned = string.Empty;
                }
                else if (CheckForKeyword(scanned) == true)
                {
                    // check for keyword
                    Result.Add(new Token() { Name = "Keyword", Value = scanned });
                    scanned = string.Empty;
                }
            }

            return Result;
        }
    }
}
