public static class Validator
{
	public static void ValidateNonNegative(ref float value)
	{
		if (value < 0)
		{
			value = 0f;
		}
	}

	public static void ValidateNonNegative(ref int value)
	{
		if (value < 0)
		{
			value = 0;
		}
	}
}
