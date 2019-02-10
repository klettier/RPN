using NUnit.Framework;
using RPN.Calculator;
using NFluent;
using System;

namespace RPN.Calculator.Tests
{
  public class RPNCalculatorShould
  {
    [Test]
    public void when_a_digit_is_sent_it_should_display_the_same_digit([Random(100)] int digit)
    {
      string expression = digit.ToString();

      var result = RPNCalculator.Resolve(expression);

      Check.That(result).IsEqualTo(expression);
    }

    [Test]
    public void when_some_digits_are_sent_it_should_display_the_number_formed_by_those_digits(
      [Random(10)] int digit1,
      [Random(10)] int digit2)
    {
      string expression = $"{digit1} {digit2}";

      var result = RPNCalculator.Resolve(expression);

      Check.That(result).IsEqualTo(expression);
    }

    [Test]
    public void when_an_operation_plus_is_sent_after_two_numbers_it_should_display_the_addition_of_those_numbers(
      [Random(10)] int digit1,
      [Random(10)] int digit2)
    {
      string operation = "+";
      string expression = $"{digit1} {digit2} {operation}";

     var result = RPNCalculator.Resolve(expression, (s => s == operation, RPNCalculator.Plus));

      Check.That(result).IsEqualTo((digit1 + digit2).ToString());
    }

    [Test]
    public void when_an_operation_minus_is_sent_after_two_numbers_it_should_display_the_substraction_of_those_numbers(
      [Random(10)] int digit1,
      [Random(10)] int digit2)
    {
      string operation = "-";
      string expression = $"{digit1} {digit2} {operation}";

      var result = RPNCalculator.Resolve(expression, (s => s == operation, RPNCalculator.Minus));

      Check.That(result).IsEqualTo((digit1 - digit2).ToString());
    }

    [Test]
    public void when_an_operation_multiply_is_sent_after_two_numbers_it_should_display_the_multiplication_of_those_numbers(
      [Random(10)] int digit1,
      [Random(10)] int digit2)
    {
      string operation = "*";
      string expression = $"{digit1} {digit2} {operation}";

      var result = RPNCalculator.Resolve(expression, (s => s == operation, RPNCalculator.Multiply));

      Check.That(result).IsEqualTo((digit1 * digit2).ToString());
    }

    [Test]
    public void when_an_operation_divide_is_sent_after_two_numbers_it_should_display_the_division_of_those_numbers(
      [Random(10)] int digit1,
      [Random(10)] int digit2)
    {
      string operation = "/";
      string expression = $"{digit1} {digit2} {operation}";

      var result = RPNCalculator.Resolve(expression, (s => s == operation, RPNCalculator.Divide));

      Check.That(result).IsEqualTo((digit1 / digit2).ToString());
    }

    static readonly (Predicate<string> isOperationApply, Func<int, int, int> applyF)[] Operations =
     new (Predicate<string> isOperationApply, Func<int, int, int> applyF)[]
    {
        (s => s == "+", RPNCalculator.Plus),
        (s => s == "-", RPNCalculator.Minus),
        (s => s == "*", RPNCalculator.Multiply),
        (s => s == "/", RPNCalculator.Divide)
    };

    [Test]
    public void Combine()
    {
      string expression = $"4 2 + 3 -";

      var result = RPNCalculator.Resolve(expression, Operations);

      Check.That(result).IsEqualTo("3");
    }

    [Test]
    public void Combine2()
    {
      string expression = $"3 5 8 * 7 + *";

      var result = RPNCalculator.Resolve(expression, Operations);

      Check.That(result).IsEqualTo("141");
    }
  }
}