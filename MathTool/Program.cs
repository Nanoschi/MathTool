﻿using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace MathTool
{

    class Program
    {
        static void Main(string[] args)
        {
            string src = "2 * 10 + 2";
            Expr e = Expr.GenTree(src);
            Console.WriteLine(e);
            Console.WriteLine(e.Eval());

            Stopwatch stopwatch = new Stopwatch();
            Console.WriteLine("Start!");
            stopwatch.Start();

            for (int i = 0; i < 10_000_000; i++)
            {
                e.Eval();
            }
            stopwatch.Stop();
            Console.WriteLine("Done! Elapsed: " + stopwatch.ElapsedMilliseconds + "ms");

        }
    }
}