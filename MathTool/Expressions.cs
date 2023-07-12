using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace MathTool
{
    public enum ExprType
    {
        NUMBER,
        IDNTIFIER,
        BIN_EXPR
    }

    public abstract class Expr
    {
        public ExprType Type;

        private static Expr CreateValueExpr(Token token)
        {
            if (token.Type == TokenType.NUMBER)
            {
                return new NumberExpr(token.Value);
            }

            else
            {
                return new IdentExpr(token.Value);
            }
        }
        public static Expr CreateTree(string expr)
        {
            return CreateTree(Token.Tokenize(expr));
        }

        public static Expr CreateTree(List<Token> tokens)
        {
            TrimParens(tokens);

            if (tokens.Count == 1)
            {
                return CreateValueExpr(tokens[0]);
            }
            else if (tokens.Count == 2)
            {
                return new UnaryExpr(tokens[0].Value, CreateValueExpr(tokens[1]));
            }

            int first_high_prio = -1;
            int first_exponent = -1;
            int paren_depth = 0;

            List<Token> left;
            List<Token> right;

            for (int i = tokens.Count - 1; i >= 0; i--)
            {
                Token token = tokens[i];

                if (token.Type == TokenType.R_PAREN)
                {
                    paren_depth++;
                }
                else if (token.Type == TokenType.L_PAREN)
                {
                    paren_depth--;
                }

                if (paren_depth > 0)
                {
                    continue;
                }


                else if (token.IsLowPrioOp())
                {
                    left = tokens.GetRange(0, i);
                    right = tokens.GetRange(i + 1, tokens.Count - i - 1);
                    return new BinaryExpr(token.Value, CreateTree(left), CreateTree(right));
                }

                else if (token.IsHighPrioOp() && first_high_prio == -1)
                {
                    first_high_prio = i;
                }
                else if (token.Type == TokenType.EXPONENT && first_exponent == -1)
                {
                    first_exponent = i;
                }

                else if (token.IsSign() && tokens[i + 1].Type == TokenType.L_PAREN && i == 0)
                {
                    right = tokens.GetRange(i + 1, tokens.Count - i - 1);
                    return new UnaryExpr(token.Value, CreateTree(right));
                }
            }

            if (first_high_prio != -1)
            {
            left = tokens.GetRange(0, first_high_prio);
            right = tokens.GetRange(first_high_prio + 1, tokens.Count - first_high_prio - 1);
            return new BinaryExpr(tokens[first_high_prio].Value, CreateTree(left), CreateTree(right));
            }

            else
            {
                left = tokens.GetRange(0, first_exponent);
                right = tokens.GetRange(first_exponent + 1, tokens.Count - first_exponent - 1);
                return new BinaryExpr(tokens[first_exponent].Value, CreateTree(left), CreateTree(right));
            }

        }

        public static void TrimParens(List<Token> tokens)
        {
            int start_parens = 0;
            int end_parens = 0;
            int inner_paren_depth = 0;

            for (int i = 0; tokens[i].Type == TokenType.L_PAREN && i < tokens.Count - 1; i++)
            {
                start_parens++; ;
            }

            for (int i = tokens.Count - 1; tokens[i].Type == TokenType.R_PAREN && i > 0; i--)
            {
                end_parens++; ;
            }

            for (int i = start_parens; i < tokens.Count - end_parens; i++)
            {
                if (tokens[i].Type == TokenType.L_PAREN)
                {
                    inner_paren_depth++;
                }
                else if (tokens[i].Type == TokenType.R_PAREN && inner_paren_depth > 0)
                {
                    inner_paren_depth--;
                }
                else if (tokens[i].Type == TokenType.R_PAREN && inner_paren_depth == 0)
                {
                    start_parens--;
                }
            }

            tokens.RemoveRange(0, start_parens);
            tokens.RemoveRange(tokens.Count - start_parens, start_parens);
        }

        public abstract double Eval();
    }

    class BinaryExpr : Expr
    {
        string op;
        Expr left;
        Expr right;

        public BinaryExpr(string op, Expr left, Expr right)
        {
            this.Type = ExprType.BIN_EXPR;
            this.left = left;
            this.right = right;
            this.op = op;

        }

        public override double Eval()
        {
            switch (op)
            {
                case "*":
                    return left.Eval() * right.Eval();
                case "+":
                    return left.Eval() + right.Eval();
                case "-":
                    return left.Eval() - right.Eval();
                case "/":
                    return left.Eval() / right.Eval();
                case "**":
                    return Math.Pow(left.Eval(), right.Eval());
                default:
                    Console.WriteLine("Unknown operator " + op);
                    return 0;
            }
        }
        public override string ToString()
        {
            return $"({left} {op} {right})";
        }
    }

    class UnaryExpr : Expr
    {
        string op;
        Expr value;

        public UnaryExpr(string op, Expr value)
        {
            this.Type = ExprType.BIN_EXPR;
            this.value = value;
            this.op = op;

        }

        public override double Eval()
        {
            switch (op)
            {
                case "+":
                    return +(value.Eval());
                case "-":
                    return -(value.Eval());
                default:
                    Console.WriteLine("Unknown operator " + op);
                    return 0;
            }
        }
        public override string ToString()
        {
            return $"({op}{value})";
        }
    }

    class IdentExpr : Expr
    {
        string ident;

        public IdentExpr(string ident)
        {
            this.Type = ExprType.IDNTIFIER;
            this.ident = ident;

        }

        public override double Eval()
        {
            return 0;
        }
        public override string ToString()
        {
            return ident;
        }
    }

    class NumberExpr : Expr
    {
        public double Number;

        public NumberExpr(string number)
        {
            this.Type = ExprType.NUMBER;
            this.Number = ExprConversions.StringToDouble(number);

        }

        public NumberExpr(double number)
        {
            this.Type = ExprType.NUMBER;
            this.Number = number;

        }

    public override double Eval()
    {
        return Number;
    }

    public override string ToString()
    {
        return Number.ToString();
    }

    }

    public static class ExprConversions
    {
        public static double StringToDouble(string str)
        {
            double total = 0;
            int period_pos = -1;
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == '.')
                {
                    period_pos = i;
                    break;
                }
            }

            if (period_pos == -1)
            {
                return double.Parse(str);
            }

            // Decimal digits
            for (int i = 1; i < str.Length - period_pos; i++)
            {
                total += (str[i + period_pos] - '0') / Math.Pow(10.0, i);
            }
            // Whole digits
            for (int i = 0; i < period_pos; i++)
            {
                total += (str[i] - '0') * Math.Pow(10.0, i);
            }
            return total;
        }
    }


}
