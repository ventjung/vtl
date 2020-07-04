using System;
using System.Collections.Generic;
using System.Text;

namespace VTL
{
    public class Interpreter
    {
        static Dictionary<string, int> Variables = new Dictionary<string, int>(); // simulasiin variable dsb

        public static int ProcessExpression(Node node)
        {
            var result = 0;

            if(node.Value == "isi")
            {
                Variables[node.Children[0].Value] = ProcessExpression(node.Children[1]);
            }
            else if(node.Value == "tambah")
            {
                return int.Parse(node.Children[0].Value) + int.Parse(node.Children[1].Value);
            }

            return result;
        }

        public static void Interpret(Node AST)
        {
            foreach (var node in AST.Children)
            {
                if(node.Value == "buat")
                {
                    Variables.Add(node.Children[0].Value, 0);
                }
                else if(node.Value == "isi")
                {
                    ProcessExpression(node);
                }
                else if(node.Value == "tampilkan")
                {
                    Console.WriteLine(Variables[node.Children[0].Value]);
                }
            }
        }
    }
}
