using System;
using UnityEngine;


[CreateAssetMenu]
public class GeneratorData : ScriptableObject
{
	private static String NEGATIVE_VALUE_EXCEPTION_MESSAGE = "The value must be non-negative!";

	[SerializeField]
	private float purchaseCost;

	[SerializeField]
	private float upgradeCostGrowthRate;

	[SerializeField]
	private float productionRate;

	[SerializeField]
	private float productionMultiplier;

	private float upgradeCost;

	private float baseProductionRate;

	private int level;

#region Properties

	public float PurchaseCost
	{
		get => purchaseCost;
	}

	public float UpgradeCostGrowthRate
	{
		get => upgradeCostGrowthRate;

		set
		{
			if (value < 0)
			{
				throw new ArgumentOutOfRangeException("upgradeCostGrowthRate", NEGATIVE_VALUE_EXCEPTION_MESSAGE);
			}
			else
			{
				upgradeCostGrowthRate = value;
			}
		}
	}

	public float ProductionRate
	{
		get => productionRate;

		set
		{
			if (value < 0)
			{
				throw new ArgumentOutOfRangeException("productionRate", NEGATIVE_VALUE_EXCEPTION_MESSAGE);
			}
			else
			{
				productionRate = value;
			}
		}
	}

	public float ProductionMultiplier
	{
		get => productionMultiplier;

		set
		{
			if (value < 0)
			{
				throw new ArgumentOutOfRangeException("productionMultiplier", NEGATIVE_VALUE_EXCEPTION_MESSAGE);
			}
			else
			{
				productionMultiplier = value;
			}
		}
	}

	public float UpgradeCost
	{
		get => upgradeCost;

		set
		{
			if (value < 0)
			{
				throw new ArgumentOutOfRangeException("upgradeCost", NEGATIVE_VALUE_EXCEPTION_MESSAGE);
			}
			else
			{
				upgradeCost = value;
			}
		}
	}

	public float BaseProductionRate
	{
		get => baseProductionRate;
	}

	public int Level
	{
		get => level;

		set
		{
			if (value < 0)
			{
				throw new ArgumentOutOfRangeException("level", NEGATIVE_VALUE_EXCEPTION_MESSAGE);
			}
			else
			{
				level = value;
			}
		}
	}

	#endregion

	private void OnValidate()
	{
		ValidateNonNegative(ref purchaseCost);
		ValidateNonNegative(ref upgradeCostGrowthRate);
		ValidateNonNegative(ref productionRate);
		ValidateNonNegative(ref productionMultiplier);
		ValidateNonNegative(ref upgradeCost);

		baseProductionRate = productionRate;
	}

	public bool IsPurchased()
	{
		if (Level == 0)
		{
			return false;
		}
		else
		{
			return true;
		}
	}

	private void ValidateNonNegative(ref float value)
	{
		if (value < 0)
		{
			value = 0f;
		}
	}
}
