using System;
using System.Collections.Generic;
using System.Text;

namespace 策略模式
{
    public  class 收费上下文
    {
        收费抽象 _收费抽象 = null;
        public 收费上下文(收费抽象 收费抽象)
        {
            _收费抽象 = 收费抽象;
        }

        public double 获取结果()
        {
            return _收费抽象.收费();
        }
    }

    public abstract class 收费抽象
    {
        public double _原价;
        public abstract double 收费();
    }

    public class 原价 : 收费抽象
    {
        public 原价(double 原价)
        {
            _原价 = 原价;
        }

        public override double 收费()
        {
            return _原价;
        }
    }
    public class 打折 : 收费抽象
    {
        double _折扣率;
        public 打折(double 原价,double 折扣率)
        {
            _原价 = 原价;
            _折扣率 = 折扣率;
        }

        public override double 收费()
        {
            return _原价* _折扣率;
        }
    }
}
