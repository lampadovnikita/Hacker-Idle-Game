using System;
using UnityEngine;


[CreateAssetMenu]
public class GeneratorData : ScriptableObject
{
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

	public float PurchaseCost { get => purchaseCost; }

	public float UpgradeCostGrowthRate { get => upgradeCostGrowthRate; set => upgradeCostGrowthRate = value; }

	public float ProductionRate { get => productionRate; set => productionRate = value; }

	public float ProductionMultiplier { get => productionMultiplier; set => productionMultiplier = value; }

	public float UpgradeCost { get => upgradeCost; set => upgradeCost = value; }

	public float BaseProductionRate { get => baseProductionRate; }

	public int Level { get => level; set => level = value; }

	private void OnValidate()
	{
		ValidateNonNegative(ref purchaseCost);
		ValidateNonNegative(ref upgradeCost);
		ValidateNonNegative(ref upgradeCostGrowthRate);
		ValidateNonNegative(ref productionRate);
		ValidateNonNegative(ref productionMultiplier);

		baseProductionRate = productionRate;
	}

	public bool isPurchased()
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
