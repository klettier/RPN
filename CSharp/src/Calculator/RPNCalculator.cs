using System;
using System.Collections.Generic;
using System.Linq;

namespace RPN.Calculator
{
  public static class RPNCalculator
  {
    public static int Plus(int d1, int d2) => d1 + d2;
    public static int Minus(int d1, int d2) => d1 - d2;
    public static int Multiply(int d1, int d2) => d1 * d2;
    public static int Divide(int d1, int d2) => d1 / d2;
    public static string Resolve(
        string exp,
        params (Predicate<string> isOperationApply, Func<int, int, int> applyF)[] operations)
    {
      Stack<string> buff = new Stack<string>();
      bool hasApplied = false;
      foreach (var c in exp.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries))
      {
        foreach ((Predicate<string> isOperationApply, Func<int, int, int> applyF) in operations)
        {
          if (isOperationApply(c))
          {
            var d2 = int.Parse(buff.Pop());
            var d1 = int.Parse(buff.Pop());

            var applied = applyF(d1, d2);

            buff.Push(applied.ToString());

            hasApplied = true;

            break;
          }

          hasApplied = false;
        }

        if(!hasApplied)
            buff.Push(c);
      }

      return string.Join(" ", buff.Reverse().Select(s => s));
    }
  }
}
