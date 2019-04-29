using System;

namespace ComplexEquation
{
    public class ComplexNumber
    {
        public readonly Fractional imaginaryPart;
        public readonly Fractional realPart;

        public ComplexNumber()
        {
            realPart = 0;
            imaginaryPart = 0;
        }

        public ComplexNumber(string num)
        {
            realPart = 0;
            imaginaryPart = 0;

            string[] complexStr;
            if (num.Contains("+"))
            {
                complexStr = num.Replace(" ", "").Split('+');
            }
            else if (num.Contains("-"))
            {
                complexStr = num.Replace(" ", "").Split('-');
                complexStr[complexStr.Length - 1] = "-" + complexStr[complexStr.Length - 1];
            }
            else
            {
                complexStr = new[] {num};
            }

            if (complexStr.Length == 0)
            {
                imaginaryPart = 0;
                realPart = 0;
            }

            if (complexStr.Length == 1)
            {
                if (complexStr[0].EndsWith("i"))
                    imaginaryPart = complexStr[0].Equals("i")
                        ? 1
                        : new Fractional(complexStr[0].Remove(complexStr[0].Length - 1));
                else
                    realPart = new Fractional(complexStr[0]);
            }
            else if (complexStr.Length == 2)
            {
                if (complexStr[0].EndsWith("i"))
                {
                    imaginaryPart = complexStr[0].Equals("i")
                        ? 1
                        : new Fractional(complexStr[0].Remove(complexStr[0].Length - 1));
                    realPart = new Fractional(complexStr[1]);
                }

                if (complexStr[1].EndsWith("i"))
                {
                    imaginaryPart = complexStr[0].Equals("i")
                        ? 1
                        : new Fractional(complexStr[1].Remove(complexStr[1].Length - 1));
                    realPart = new Fractional(complexStr[0]);
                }
            }
            else if (complexStr.Length > 2)
            {
                throw new Exception("未能识别的复数形式：" + num + "\n请使用以下格式：\"a + bi\"");
            }
        }

        public ComplexNumber(Fractional real)
        {
            realPart = real;
            imaginaryPart = 0;
        }

        public ComplexNumber(Fractional real, Fractional imaginary)
        {
            realPart = real;
            imaginaryPart = imaginary;
        }

        public static ComplexNumber operator +(ComplexNumber num1, ComplexNumber num2)
        {
            return new ComplexNumber(num1.realPart + num2.realPart, num1.imaginaryPart + num2.imaginaryPart);
        }

        public static ComplexNumber operator -(ComplexNumber num)
        {
            return new ComplexNumber(-num.realPart, -num.imaginaryPart);
        }

        public static ComplexNumber operator -(ComplexNumber num1, ComplexNumber num2)
        {
            return num1 + -num2;
        }

        public static ComplexNumber operator *(ComplexNumber num1, ComplexNumber num2)
        {
            return new ComplexNumber(
                num1.realPart * num2.realPart - num1.imaginaryPart * num2.imaginaryPart,
                num1.realPart * num2.imaginaryPart + num1.imaginaryPart * num2.realPart);
        }

        public static ComplexNumber operator /(ComplexNumber num1, Fractional num2)
        {
            return new ComplexNumber(num1.realPart / num2, num1.imaginaryPart / num2);
        }

        public static ComplexNumber operator /(ComplexNumber num1, ComplexNumber num2)
        {
            return num1 * num2.GetConjugate() /
                   (num2.realPart * num2.realPart + num2.imaginaryPart * num2.imaginaryPart);
        }

        public ComplexNumber GetConjugate()
        {
            return new ComplexNumber(realPart, -imaginaryPart);
        }

        public static implicit operator ComplexNumber(Fractional num)
        {
            return new ComplexNumber(num, 0);
        }

        public static implicit operator ComplexNumber(int num)
        {
            return new ComplexNumber(num, 0);
        }

        public override string ToString()
        {
            return realPart + "+(" + imaginaryPart + ")i";
        }
    }
}