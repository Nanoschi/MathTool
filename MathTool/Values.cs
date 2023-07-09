using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathTool
{
    struct Values
    {
        public int n;

        public Values(int v) 
        {
            n = v;
        }

        public void incr(int v)
        {
            n += v;
        }
    }
}
