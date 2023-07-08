using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace MathTool
{
    public enum NodeType
    {
        NUMBER,
        IDNTIFIER,
        BIN_EXPR
    }
    public abstract class Stmt
    {
        public NodeType Type;
    }

    public abstract class Expr : Stmt
    {
        public static Expr GenTree(string expr)
        {
            return GenTree(Token.Tokenize(expr));
        }

        public static Expr GenTree(List<Token> tokens)
        {
            TrimParens(tokens);

            if (tokens.Count == 1)
            {
                if (tokens[0].Type == TokenType.NUMBER)
                {
                    return new NumberExpr(tokens[0].Value);
                }

                else if (tokens[0].Type == TokenType.IDENTIFIER)
                {
                    return new IdentExpr(tokens[0].Value);
                }
            }

            int first_high_prio = -1;
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

                if (token.IsLowPrioOp())
                {
                    left = tokens.GetRange(0, i);
                    right = tokens.GetRange(i + 1, tokens.Count - i - 1);
                    return new BinaryExpr(token.Value, GenTree(left), GenTree(right));
                }

                else if (token.IsHighPrioOp() && first_high_prio == -1)
                {
                    first_high_prio = i;
                }
            }

            left = tokens.GetRange(0, first_high_prio);
            right = tokens.GetRange(first_high_prio + 1, tokens.Count - first_high_prio - 1);
            return new BinaryExpr(tokens[first_high_prio].Value, GenTree(left), GenTree(right));

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
            this.Type = NodeType.BIN_EXPR;
            this.left = left;
            this.right = right;
            this.op = op;

        }

        public override string ToString()
        {
            return $"({left} {op} {right})";
        }

        public override double Eval()
        {
            switch (op)
            {
                case "+":
                    return left.Eval() + right.Eval();
                case "*":
                    return left.Eval() * right.Eval();
                case "/":
                    return left.Eval() / right.Eval();
                case "-":
                    return left.Eval() - right.Eval();
                default:
                    Console.WriteLine("Unknown operator " + op);
                    return 0;
            }
        }
    }

    class IdentExpr : Expr
    {
        string ident;

        public IdentExpr(string ident)
        {
            this.Type = NodeType.IDNTIFIER;
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
        string number;

        public NumberExpr(string number)
        {
            this.Type = NodeType.NUMBER;
            this.number = number;

        }

        public override double Eval()
        {
            return Convert.ToDouble(number);
        }

        public override string ToString()
        {
            return number;
        }
    }


}
