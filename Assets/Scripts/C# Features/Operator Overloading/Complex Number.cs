using System;

public struct ComplexNumber
{
    public int a;
    public int b;

    public ComplexNumber(int a, int b)
    {
        this.a = a;
        this.b = b;
    }

    public static ComplexNumber operator +(ComplexNumber num1, ComplexNumber num2)
    {
        return new ComplexNumber(num1.a + num2.a, num1.b + num2.b);
    }

    public static ComplexNumber operator -(ComplexNumber num1, ComplexNumber num2)
    {
        return new ComplexNumber(num1.a - num2.a, num1.b - num2.b);
    }

    public static ComplexNumber operator *(ComplexNumber num1, ComplexNumber num2)
    {
        int realPart = num1.a * num2.a - num1.b * num2.b;
        int imaginaryPart = num1.a * num2.b + num1.b * num2.a;

        return new ComplexNumber(realPart, imaginaryPart);
    }

    public static ComplexNumber operator /(ComplexNumber num1, ComplexNumber num2)
    {
        int denominator = num2.a * num2.a + num2.b * num2.b;

        if (denominator == 0)
        {
            throw new DivideByZeroException("Division by zero is not allowed");
        }

        int realPart = num1.a * num2.a + num1.b * num2.b;
        int imaginaryPart = num1.b * num2.a - num1.a * num2.b;

        return new ComplexNumber(realPart / denominator, imaginaryPart / denominator);
    }

    public static ComplexNumber operator -(ComplexNumber num) => new ComplexNumber(-num.a, -num.b);
    public static bool operator ==(ComplexNumber num1, ComplexNumber num2) => num1.a == num2.a && num1.b == num2.b ? true : false;
    public static bool operator !=(ComplexNumber num1, ComplexNumber num2) => !(num1 == num2);

    public override string ToString()
    {
        return $"{a} + {b}i";
    }
}
