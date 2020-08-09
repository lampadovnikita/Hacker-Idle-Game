using System;
using UnityEngine;

public class FloatBasedMoney : Money<float>
{
	private static readonly string[] METRIC_PREFIXES = { "", "K", "M", "G", "T", "P", "E", "Z", "Y" };
	private static readonly int EXP_PER_METRIC = 3;

	public static string ToString(float amount)
	{
		int exp = Mathf.FloorToInt(Mathf.Log10(Mathf.Abs(amount)));

		string prefix = "";

		if (exp >= 0)
		{
			if (exp < METRIC_PREFIXES.Length * EXP_PER_METRIC)
			{
				prefix = METRIC_PREFIXES[exp / EXP_PER_METRIC];
			}
			else
			{
				exp = (METRIC_PREFIXES.Length - 1) * EXP_PER_METRIC;
				prefix = METRIC_PREFIXES[METRIC_PREFIXES.Length - 1];
			}
			amount /= Mathf.Pow(10f, exp);
		}

		return string.Format("{0:F5}{1}", amount, prefix);
	}

	public override string ToString()
	{
		return ToString(Amount);
	}

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
