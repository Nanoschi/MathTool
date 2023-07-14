using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathTool
{
    class Runner
    {
        private Dictionary<string, Expr> _variables;

        public Runner()
        {
            _variables = new Dictionary<string, Expr>();
        }
    }
}
