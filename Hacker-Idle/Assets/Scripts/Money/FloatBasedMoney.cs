using System;

public class FloatBasedMoney : Money<float>
{
    protected override float Add(float lhs, float rhs)
    {
        float res;

        try
        {
            res = checked(lhs + rhs);
        }
        catch (OverflowException e)
        {
            res = float.MaxValue;
        }

        return res;
    }

    protected override float Subtract(float lhs, float rhs)
    {
        float res;

        try
        {
            res = checked(lhs - rhs);
        }
        catch (OverflowException e)
        {
            res = float.MinValue;
        }

        return res;
    }
}
