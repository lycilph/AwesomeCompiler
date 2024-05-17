﻿using Core.NFA.Algorithms;
using Core.RegularExpressions;

namespace AwesomeCompiler;

internal class Program
{
    static void Main()
    {
        var regex = new Regex("(a|b)*abb");
        var nfa = regex.ConvertToNFA();
        var sc = new SubsetConstruction();
        var node = sc.Execute(nfa);

        var minimizer = new StateMinimization();
        minimizer.Execute(node);

        //Console.Write("Press any key to continue...");
        //Console.ReadKey();
    }
}
