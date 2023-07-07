using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace MathTool
{
    enum NodeType
    {
        NUMBER,
        IDNTIFIER,
        BIN_EXPR
    }
    abstract class Stmt
    {
        public NodeType Type;
    }

    abstract class Expr : Stmt
    { 
        public static Expr GenTree(string expr)
        {
            return GenTree(Token.Tokenize(expr));
        }

        public static Expr GenTree(List<Token> tokens)
        {
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

            for (int i = tokens.Count - 1; i >= 0; i--)
            {
                Token token = tokens[i];

                if (token.IsOperator())
                {
                    List<Token> left = tokens.GetRange(0, i);
                    List<Token> right = tokens.GetRange(i + 1, tokens.Count - i - 1);
                    return new BinaryExpr(token.Value, GenTree(left), GenTree(right));
                }
            }

            return new IdentExpr(tokens[0].Value);
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
