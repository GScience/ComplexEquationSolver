using System;

namespace ComplexEquation
{
    public class Fractional
    {
        private readonly long _molecular;
        private readonly long _denominator;

        public Fractional(string num)
        {
            if (num.Replace(" ", "").Equals(""))
                num = "0";

            var fractionalStr = num.Replace(" ", "").Split('/');

            switch (fractionalStr.Length)
            {
                case 0:
                    _molecular = 0;
                    _denominator = 1;
                    break;
                case 1:
                    _molecular = long.Parse(fractionalStr[0]);
                    _denominator = 1;
                    break;
                case 2:
                    _molecular = long.Parse(fractionalStr[0]);
                    _denominator = long.Parse(fractionalStr[1]);
                    break;
                default:
                    throw new Exception("未能识别的小数形式：" + num + "\n请使用以下格式：\"分子 / 分母\"");
            }
        }

        public Fractional(int num)
        {
            _molecular = num;
            _denominator = 1;
        }

        public Fractional(long molecular, long denominator)
        {
            if (molecular == 0)
                denominator = 1;

            for (var i = Math.Min(Math.Abs(molecular), Math.Abs(denominator)) / 2 + 1; i > 1; --i)
            {
                if (molecular % i != 0 || denominator % i != 0)
                    continue;
                molecular /= i;
                denominator /= i;
                i = Math.Min(Math.Abs(_molecular), Math.Abs(denominator)) / 2 + 1;
            }

            _molecular = molecular;
            _denominator = denominator;
        }

        public static Fractional operator +(Fractional num1, Fractional num2)
        {
            return new Fractional(
                num1._molecular * num2._denominator + num2._molecular * num1._denominator,
                num1._denominator * num2._denominator);
        }

        public static Fractional operator -(Fractional num)
        {
            return new Fractional(-num._molecular, num._denominator);
        }

        public static Fractional operator -(Fractional num1, Fractional num2)
        {
            return num1 + -num2;
        }

        public static Fractional operator *(Fractional num1, Fractional num2)
        {
            return new Fractional(
                num1._molecular * num2._molecular,
                num1._denominator * num2._denominator);
        }

        public static Fractional operator /(Fractional num1, Fractional num2)
        {
            return new Fractional(
                num1._molecular * num2._denominator,
                num1._denominator * num2._molecular);
        }

        public static implicit operator Fractional(int num)
        {
            return new Fractional(num);
        }

        public override string ToString()
        {
            return _molecular + "/" + _denominator;
        }
    }
}