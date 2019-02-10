## RPN Kata

* Given a RPN Calculator when a digit is sent it should display the same digit
* Given a RPN Calculator when some digits are sent it should display the number formed by those digits
* Given a RPN Calculator when an operation (* + / -) is sent after two numbers it should display the result of that operation

## Examples

```
7 = 7
7 2 - 3 4 = 5 3 4
20 5 / => (20/5) = 4
4 2 + 3 - => (4+2)-3 = 3
3 5 8 * 7 + * => 3*((5*8)+7) = 141
```

## FSharp

* [Implementation](FSharp/UnitTest1.fs#L5)
* [Tests](FSharp/UnitTest1.fs#L32)

## CSharp

* [Implementation](CSharp\src\Calculator\RPNCalculator.cs)
* [Tests](CSharp\tests\Calculator.Tests\RPNCalculatorShould.cs)