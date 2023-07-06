﻿using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace MathTool
{
    enum TokenType
    {
        NUMBER,
        IDENTIFIER,
        PLUS,
        MINUS,
        TIMES,
        DIVIDE,
        L_PAREN,
        R_PAREN,
    }

    struct Token
    {
        public TokenType Type;
        public string Value;

        public Token(TokenType type, String value)
        {
            this.Type = type;
            this.Value = value;
        }

        private static bool IsNumberPart(char c)
        {
            return char.IsDigit(c) || c == '.';
        }

        private static bool IsIdentPart(char c)
        {
            return (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z');
        }

        public static List<Token> Tokenize(String expr) 
        {
            int length = expr.Length;
            List<Token> tokens = new List<Token>();

            for (int i = 0;  i < length; i++)
            {
                char c = expr[i];

                if (c == ' ' || c == '\t') 
                {
                    continue;
                }

                else if (c == '(')
                {
                    tokens.Add(new Token(TokenType.L_PAREN, "("));
                }

                else if (c == ')')
                {
                    tokens.Add(new Token(TokenType.R_PAREN, ")"));
                }

                else if (c == '-')
                {
                    tokens.Add(new Token(TokenType.MINUS, "-"));
                }
                else if (c == '*')
                {
                    tokens.Add(new Token(TokenType.TIMES, "*"));
                }
                else if (c == '/')
                {
                    tokens.Add(new Token(TokenType.DIVIDE, "/"));
                }
                else if (c == '+')
                {
                    tokens.Add(new Token(TokenType.PLUS, "+"));
                }

                else if (IsNumberPart(c))
                {
                    StringBuilder literal = new StringBuilder();
                    literal.Append(c);

                    while (IsNumberPart(c))                     
                    {
                        if (i == length - 1)
                        {
                            break;
                        }

                        if (IsNumberPart(expr[i + 1]))
                        {
                            i++;
                            c = expr[i];
                        }
                        else
                        {
                            break;
                        }

                        literal.Append(c);
                    }

                    tokens.Add(new Token(TokenType.NUMBER, literal.ToString()));

                }

                else if (IsIdentPart(c))
                {
                    StringBuilder identifier = new StringBuilder();
                    identifier.Append(c);

                    while (IsIdentPart(c))
                    {
                        if (i == length - 1)
                        {
                            break;
                        }

                        if (IsIdentPart(expr[i + 1]))
                        {
                            i++;
                            c = expr[i];
                        }
                        else
                        {
                            break;
                        }

                        identifier.Append(c);
                    }

                    tokens.Add(new Token(TokenType.IDENTIFIER, identifier.ToString()));

                }

                else
                {
                    Console.WriteLine("Unknown Character '" + c + "'");
                }
            }

            return tokens;
        }


        public override string ToString()
        {
            return Value;
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            string src = "123 + carea";
            Console.WriteLine("Start!");
            Stopwatch sw = new Stopwatch();
            sw.Start();

            for (int i = 0; i < 1_000_000; i++)
            {
                Token.Tokenize(src);
            }
            sw.Stop();
            Console.WriteLine("Done! Elapsed Time: " + sw.ElapsedMilliseconds + "ms");

        }
    }
}