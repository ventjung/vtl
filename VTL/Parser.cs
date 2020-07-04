using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace VTL
{
    public static class Parser
    {
        public static Node Parse(List<Token> TokenList)
        {
            Node tree = new Node();

            // Nyusun statement dulu biar secara semantik gampang dianalisa
            List<Token> statement = new List<Token>();
            foreach (var token in TokenList)
            {
                if(token.Name == "Keyword" && token.Value == "titik")
                {
                    // Process statement

                    // Deteksi semantik variable declaration
                    if(statement.Count > 1 && statement[0].Name == "Keyword" && statement[0].Value == "buat" && statement[1].Name == "Identifier")
                    {
                        // berarti variable declaration statement
                        Node variableDeclarationStatement = new Node();
                        variableDeclarationStatement.Value = statement[0].Value;

                        variableDeclarationStatement.Children.Add(new Node()
                        {
                            Value = statement[1].Value
                        });

                        // tempel di tree
                        tree.Children.Add(variableDeclarationStatement);

                        // reset statement
                        statement = new List<Token>();
                    }

                    // Deteksi semantik expression operator
                    if(statement.Count > 1 && statement[0].Name == "Identifier" && statement[1].Name == "Operator")
                    {
                        // berarti expression statement
                        Node expressionStatement = new Node();
                        expressionStatement.Value = statement[1].Value;

                        expressionStatement.Children.Add(new Node()
                        {
                            Value = statement[0].Value
                        });

                        Node lastNode = expressionStatement;
                        for (int i = 2; i < statement.Count; i+=2)
                        {
                            if(statement[i].Name == "Integer" && i + 1 == statement.Count)
                            {
                                Node childExpression = new Node();
                                childExpression.Value = statement[i].Value;

                                lastNode.Children.Add(childExpression);
                            }
                            else if (statement[i].Name == "Integer" && statement[i + 1].Name == "Operator")
                            {
                                Node childExpression = new Node();
                                childExpression.Value = statement[i + 1].Value;

                                childExpression.Children.Add(new Node()
                                {
                                    Value = statement[i].Value
                                });

                                lastNode.Children.Add(childExpression);
                                lastNode = childExpression;
                            }
                        }

                        // tempel di tree
                        tree.Children.Add(expressionStatement);

                        // reset statement
                        statement = new List<Token>();
                    }

                    // Deteksi semantik tampilan
                    if (statement.Count > 1 && statement[0].Name == "Keyword" && statement[0].Value == "tampilkan" && statement[1].Name == "Identifier")
                    {
                        // berarti variable declaration statement
                        Node tampilkanStatement = new Node();
                        tampilkanStatement.Value = statement[0].Value;

                        tampilkanStatement.Children.Add(new Node()
                        {
                            Value = statement[1].Value
                        });

                        // tempel di tree
                        tree.Children.Add(tampilkanStatement);

                        // reset statement
                        statement = new List<Token>();
                    }
                }
                else
                {
                    statement.Add(token);
                }
            }

            return tree;
        }
    }
}
