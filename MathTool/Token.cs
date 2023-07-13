using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathTool
{
    public enum TokenType
    {
        NUMBER,
        IDENTIFIER,
        PLUS,
        MINUS,
        TIMES,
        DIVIDE,
        L_PAREN,
        R_PAREN,
        NEGATIVE,
        POSITIVE,
        EXPONENT,
        STD_FUNCTION ,
    }

    public struct Token
    {
        public TokenType Type;
        public string Value;

        public Token(TokenType type, String value)
        {
            this.Type = type;
            this.Value = value;
        }

        public bool IsOperator()
        {
            return Type == TokenType.PLUS || Type == TokenType.MINUS || Type == TokenType.TIMES || Type == TokenType.DIVIDE;
        }

        public bool IsHighPrioOp()
        {
            return Type == TokenType.TIMES || Type == TokenType.DIVIDE;
        }

        public bool IsLowPrioOp()
        {
            return Type == TokenType.PLUS || Type == TokenType.MINUS;
        }

        public bool IsSign()
        {
            return Type == TokenType.NEGATIVE || Type == TokenType.POSITIVE;
        }

        public bool IsParen()
        {
            return Type == TokenType.R_PAREN || Type == TokenType.L_PAREN;
        }

        private static bool IsNumberPart(char c)
        {
            return char.IsDigit(c) || c == '.';
        }

        private static bool IsIdentPart(char c)
        {
            return (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || (c == '_');
        }

        public static List<Token> Tokenize(String expr)
        {
            int length = expr.Length;
            List<Token> tokens = new List<Token>();

            for (int i = 0; i < length; i++)
            {
                char c = expr[i];

                if (c == ' ' || c == '\t')
                {
                    continue;
                }

                else if (c == '(')
                {
                    if (tokens[tokens.Count - 1].Type == TokenType.IDENTIFIER)
                    {
                        StringBuilder func = new StringBuilder();
                        func.Append(tokens[tokens.Count - 1].Value);
                        tokens.RemoveAt(tokens.Count - 1);
                        func.Append(c);

                        int paren_depth = 1;

                        while (paren_depth > 0)
                        {
                            // Unnecessary in valid function
                            if (i == length - 1)
                            {
                                break;
                            }

                            if (expr[i + 1] == ' ')
                            {
                                i++;
                                continue;
                            }
                            else if (expr[i + 1] == '(')
                            {
                                i++;
                                paren_depth++;
                                c = expr[i];
                            }
                            else if (expr[i + 1] == ')')
                            {
                                i++;
                                paren_depth--;
                                c = expr[i];
                            }
                            else
                            {
                                i++;
                                c = expr[i];
                            }

                            func.Append(c);
                        }

                        tokens.Add(new Token(TokenType.STD_FUNCTION, func.ToString()));
                    }
                    else
                    {
                        tokens.Add(new Token(TokenType.L_PAREN, "("));
                    }
                }

                else if (c == ')')
                {
                    tokens.Add(new Token(TokenType.R_PAREN, ")"));
                }

                else if (c == '-')
                {
                    // Unary
                    if (tokens.Count == 0) 
                    {
                        tokens.Add(new Token(TokenType.NEGATIVE, "-"));
                    }
                    else if (tokens[tokens.Count - 1].IsOperator() || tokens[tokens.Count - 1].Type == TokenType.L_PAREN)
                    {
                        tokens.Add(new Token(TokenType.NEGATIVE, "-"));
                    }
                    // Binary
                    else
                    {
                        tokens.Add(new Token(TokenType.MINUS, "-"));
                    }
                }

                else if (c == '+')
                {
                    // Unary
                    if (tokens.Count == 0)
                    {
                        tokens.Add(new Token(TokenType.POSITIVE, "+"));
                    }
                    else if (tokens[tokens.Count - 1].IsOperator() || tokens[tokens.Count - 1].Type == TokenType.L_PAREN)
                    {
                        tokens.Add(new Token(TokenType.POSITIVE, "+"));
                    }
                    // Binary
                    else
                    {
                        tokens.Add(new Token(TokenType.PLUS, "+"));
                    }
                }
                else if (c == '*' && tokens.Count > 0)
                {
                    if (tokens[tokens.Count - 1].Type == TokenType.TIMES)
                    {
                        tokens.RemoveAt(tokens.Count - 1);
                        tokens.Add(new Token(TokenType.EXPONENT, "**"));

                    }
                    else
                    {
                        tokens.Add(new Token(TokenType.TIMES, "*"));
                    }
                }
                else if (c == '/')
                {
                    tokens.Add(new Token(TokenType.DIVIDE, "/"));
                }
                else if (c == '^')
                {
                    tokens.Add(new Token(TokenType.EXPONENT, "**"));
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

        public static void WriteTokens(List<Token> tokens)
        {
            foreach (Token t in tokens)
            {
                Console.Write(t);
            }
            Console.Write("\n");
        }

        public override string ToString()
        {
            return Type.ToString() + " ";
        }
    }
}
