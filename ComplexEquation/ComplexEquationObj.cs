using System.Collections.Generic;

namespace ComplexEquation
{
    public class ComplexEquationObj
    {
        public readonly int equationCount;

        public ComplexEquationObj(int n)
        {
            equationCount = n;

            args = new List<ComplexNumber>(equationCount * equationCount);
            for (var i = 0; i < args.Capacity; ++i)
                args.Add(new ComplexNumber(0));

            b = new List<ComplexNumber>(equationCount);
            for (var i = 0; i < b.Capacity; ++i)
                b.Add(new ComplexNumber(0));
        }

        public List<ComplexNumber> args { get; }
        public List<ComplexNumber> b { get; }

        public void Set(int equationId, int valueId, ComplexNumber num)
        {
            Set(args, equationId, valueId, num);
        }

        public void Set(List<ComplexNumber> list, int equationId, int valueId, ComplexNumber num)
        {
            list[equationId * equationCount + valueId] = num;
        }

        public ComplexNumber Get(List<ComplexNumber> list, int equationId, int valueId)
        {
            return list[equationId * equationCount + valueId];
        }

        public List<ComplexNumber> Solve()
        {
            var argsCpy = new List<ComplexNumber>(args.Count);
            var bCpy = new List<ComplexNumber>(b.Count);

            foreach (var arg in args)
                argsCpy.Add(arg);

            foreach (var num in b)
                bCpy.Add(num);

            //第一组初等行变换
            for (var subtrahendEquationId = 0; subtrahendEquationId != equationCount; ++subtrahendEquationId)
            for (var i = subtrahendEquationId + 1; i < equationCount; ++i)
            {
                //减去的倍数
                var k = Get(argsCpy, i, subtrahendEquationId) /
                        Get(argsCpy, subtrahendEquationId, subtrahendEquationId);

                for (var j = subtrahendEquationId; j < equationCount; ++j)
                {
                    var newArg = Get(argsCpy, i, j) - k * Get(argsCpy, subtrahendEquationId, j);
                    Set(argsCpy, i, j, newArg);
                }

                bCpy[i] -= k * bCpy[subtrahendEquationId];
            }

            //第二组初等行变换
            for (var subtrahendEquationId = equationCount - 1; subtrahendEquationId >= 0; --subtrahendEquationId)
            for (var i = subtrahendEquationId - 1; i >= 0; --i)
            {
                var k = Get(argsCpy, i, subtrahendEquationId) /
                        Get(argsCpy, subtrahendEquationId, subtrahendEquationId);
                var newArg = Get(argsCpy, i, subtrahendEquationId) -
                             k * Get(argsCpy, subtrahendEquationId, subtrahendEquationId);
                Set(argsCpy, i, subtrahendEquationId, newArg);

                bCpy[i] -= k * bCpy[subtrahendEquationId];
            }

            //计算结果
            for (var i = 0; i < equationCount; ++i)
                bCpy[i] /= Get(argsCpy, i, i);

            return bCpy;
        }
    }
}