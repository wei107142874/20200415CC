using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 简单工厂模式
{
    
    public class 计算工厂
    {
         static 计算抽象 _计算;

        public static 计算抽象 创建工厂(string 类型,double numA,double numB)
        {
            switch (类型)
            {
                case "+":
                    _计算 = new 加(numA, numB);
                    break;
                case "-":
                    _计算 = new 减(numA, numB);
                    break;
                case "*":
                    _计算 = new 乘(numA, numB);
                    break;
                case "/":
                    _计算 = new 除(numA, numB);
                    break;
            }
            return _计算;
        }
    }

    public abstract class 计算抽象: 计算工厂
    {
        public abstract double 获取结果();
    }

    public class 加 : 计算抽象
    {
        public double _numA;
        public double _numB;
        public 加(double numA,double numB)
        {
            _numA = numA;
            _numB = numB;
        }

        public override double 获取结果()
        {
            return _numA + _numB;
        }
    }
    public class 减 : 计算抽象
    {
        public double _numA;
        public double _numB;
        public 减(double numA, double numB)
        {
            _numA = numA;
            _numB = numB;
        }

        public override double 获取结果()
        {
            return _numA -_numB;
        }
    }
    public class 乘 : 计算抽象
    {
        public double _numA;
        public double _numB;
        public 乘(double numA, double numB)
        {
            _numA = numA;
            _numB = numB;
        }

        public override double 获取结果()
        {
            return _numA * _numB;
        }
    }
    public class 除 : 计算抽象
    {
        public double _numA;
        public double _numB;
        public 除(double numA, double numB)
        {
            if (numB == 0)
            {
                Console.WriteLine("除数不能为0");
                return;
            }

            _numA = numA;
            _numB = numB;
        }

        public override double 获取结果()
        {
            return _numA / _numB;
        }
    }

}
