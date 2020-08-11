using System;
using UnityEngine;

public class FloatBasedMoney : Money<float>
{
	private static readonly string[] METRIC_PREFIXES = { "", "K", "M", "G", "T", "P", "E", "Z", "Y" };
	private static readonly int EXP_PER_METRIC = 3;

	public static string ToString(float amount)
	{
		int exp = Mathf.FloorToInt(Mathf.Log10(Mathf.Abs(amount)));
		int metricOrder = exp / EXP_PER_METRIC;
		string prefix = "";

		if (metricOrder >= 0)
		{
			if (metricOrder < METRIC_PREFIXES.Length)
			{
				prefix = METRIC_PREFIXES[metricOrder];
			}
			else
			{
				metricOrder = METRIC_PREFIXES.Length - 1;
				prefix = METRIC_PREFIXES[METRIC_PREFIXES.Length - 1];
			}
			amount /= Mathf.Pow(10f, metricOrder * EXP_PER_METRIC);
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
