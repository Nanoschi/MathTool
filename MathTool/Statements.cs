using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathTool
{
    public enum StmtType
    {
        DEFINE_VAR,
        EXPRESSION,
    }
    public abstract class Stmt
    {
        public StmtType Type;
    }

    public class DefineVarStmt : Stmt
    {
        public string Variable;
        public Expr Value;

        public DefineVarStmt(string variable, Expr value)
        {
            Variable = variable;
            Value = value;
            Type = StmtType.DEFINE_VAR;
        }
    }

    public class ExprStmt : Stmt
    {

    }
}
